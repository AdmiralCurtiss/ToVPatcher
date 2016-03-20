#include <iostream>
#include <fstream>
#include <string.h>
#include <string>
#include <sstream>
#include <list>
using namespace std;

class textblock {
public:
	int blockpointer;
	char * value1; char * value2;
	char * jpnnamepointer; char * jpnname;
	char * jpntextpointer; char * jpntext;
	char * engnamepointer; char * engname;
	char * engtextpointer; char * engtext;

	int jpnnamelen;
	int jpntextlen;
	int engnamelen;
	int engtextlen;

	textblock(int blockpointer,
			  char * value1, char * value2,
			  char * jpnnamepointer, char * jpnname,
			  char * jpntextpointer, char * jpntext,
			  char * engnamepointer, char * engname,
			  char * engtextpointer, char * engtext )
	{
		this->blockpointer = blockpointer;
		this->jpnnamepointer = jpnnamepointer;
		this->jpnname = jpnname;
		this->jpntextpointer = jpntextpointer;
		this->jpntext = jpntext;
		this->engnamepointer = engnamepointer;
		this->engname = engname;
		this->engtextpointer = engtextpointer;
		this->engtext = engtext;
		this->value1 = value1;
		this->value2 = value2;

		jpnnamelen = strlen(jpnname);
		jpntextlen = strlen(jpntext);
		engnamelen = strlen(engname);
		engtextlen = strlen(engtext);
	}
};

void swap_endian(unsigned int& x)
{
    x = (x>>24) | 
        ((x<<8) & 0x00FF0000) |
        ((x>>8) & 0x0000FF00) |
        (x<<24);
}

int getbytestillnull(char * string) {
	for ( int i = 0;; i++ ) {
		if ( string[i] == '\0' ) return i;
	}
}

int findempty(char * buffer, int textstart, int maxsize, int emptyspace, unsigned __int64& filesize, int start_here, char * nullcheck, bool resize_on_lack_of_space) {
	int emptyfound = 0;
	for ( int i = start_here; i < maxsize+textstart; i++ ) {
		if ( buffer[i] == '\0' && nullcheck[i] == 0x00 ) { emptyfound++; }
		else { emptyfound = 0; }

		if ( emptyfound >= emptyspace ) {
			return i - textstart - emptyfound + 1;
		}
	}

	if ( !resize_on_lack_of_space ) { return -1; }

	// increase filesize on lack of space
	int j;
	for ( j = filesize; j < emptyspace+filesize; j++ ) {
		buffer[j] = 0x00;
	}
	int oldsize = filesize;
	filesize = j;

	int textsize = filesize - textstart;

	char * bytes = new char[4];
	memcpy(bytes, &textsize, 4);

	buffer[0x18] = bytes[3];
	buffer[0x19] = bytes[2];
	buffer[0x1A] = bytes[1];
	buffer[0x1B] = bytes[0];

	return oldsize - textstart;
}

void setnullareas(char * buffer, char * nullcheck, unsigned __int64 filesize) {
	const int continous_space = 8;

	unsigned __int64 i;
	int nullfound = 0;
	for ( i = 0; i < filesize; i++ ) {
		if ( buffer[i] == 0x00 ) {
			nullfound++;
			if ( nullfound >= continous_space ) {
				if ( nullfound == continous_space ) {
					memset(&nullcheck[i-continous_space], 1, continous_space);
				}
				nullcheck[i] = 1;
			}
		} else {
			nullfound = 0;
		}
	}
}

void string_to_bytearray(std::string str, unsigned char* &array, int& size)
{
	int length = str.length();
	
	// add a 0 for each length below 8
	while( length < 8 )
	{
		str = "0" + str;
		length++;
	}

	// allocate memory for the output array
	array = new unsigned char[length/2];
	size = length/2;

	std::stringstream sstr(str);
	for(int i=0; i < size; i++)
	{
		char ch1, ch2;
		sstr >> ch1 >> ch2;
		int dig1, dig2;
		if(isdigit(ch1)) dig1 = ch1 - '0';
		else if(ch1>='A' && ch1<='F') dig1 = ch1 - 'A' + 10;
		else if(ch1>='a' && ch1<='f') dig1 = ch1 - 'a' + 10;
		if(isdigit(ch2)) dig2 = ch2 - '0';
		else if(ch2>='A' && ch2<='F') dig2 = ch2 - 'A' + 10;
		else if(ch2>='a' && ch2<='f') dig2 = ch2 - 'a' + 10;
		array[i] = dig1*16 + dig2;
	}
}


int main(int argc, char ** argv) {
	
	if ( argc != 3 ) {
		cout << "Usage: " << argv[0] << " TSSFile TextToInsert" << endl;
		return -1;
	}

	char * filename = argv[1];
	char * filenameText = argv[2];

	
	std::list<textblock*> * textblocklist = new std::list<textblock*>();


	unsigned long long filesize;

	std::ifstream file;
	file.open(filename, std::ios::binary);

	if ( !file.is_open() ) return -1;

	file.seekg (0, ios::end);
	filesize = file.tellg();
	file.seekg (0, ios::beg);

	char * buffer = new char[filesize+16777216];
	memset( &buffer[filesize], 0xFF, 16777216 );

	file.read(buffer, filesize);
	file.close();
	
	// array that holds info about which areas are NULL in the original files and may not be overwritten
	char * nullcheck = new char[filesize+16777216];
	memset( &nullcheck[0], 0x00, filesize+16777216 );
	setnullareas(buffer, nullcheck, filesize);
	
	unsigned long long filesizeText;

	std::ifstream fileText;
	fileText.open(filenameText, std::ios::binary);

	if ( !fileText.is_open() ) return -1;

	fileText.seekg (0, ios::end);
	filesizeText = fileText.tellg();
	fileText.seekg (0, ios::beg);

	char * bufferText = new char[filesizeText+35];
	memset( &bufferText[filesizeText], 0xFF, 35 );

	fileText.read(bufferText, filesizeText);
	fileText.close();


	
	unsigned int * textstartp = (unsigned int *)&buffer[0x0C];
	unsigned int textstart = *textstartp;
	swap_endian( textstart );

	unsigned int * textsizep = (unsigned int *)&buffer[0x18];
	unsigned int textsize = *textsizep;
	swap_endian( textsize );

	char * newfilename = new char[300];
	newfilename[0] = 0;
	strcat( newfilename, filename );
	strcat( newfilename, ".new" );
	ofstream outfile(newfilename, std::ios::binary);

	char * charrayLocation = new char[100];
	char * charrayPointer = new char[100];
	unsigned int lastpointer = 0;
	
	
	
	std::string pointerstring;
	std::string jpnnamepointerstring;
	std::string jpntextpointerstring;
	std::string engnamepointerstring;
	std::string engtextpointerstring;
	unsigned char* pointerbytes = NULL;
	unsigned char* jpnnamepointerbytes = NULL;
	unsigned char* jpntextpointerbytes = NULL;
	unsigned char* engnamepointerbytes = NULL;
	unsigned char* engtextpointerbytes = NULL;
	int pointerbytessize; //should always be 4
	int jpnnamepointerbytessize; //should always be 4
	int jpntextpointerbytessize; //should always be 4
	int engnamepointerbytessize; //should always be 4
	int engtextpointerbytessize; //should always be 4
	
	for ( int cur = 0; cur < filesizeText; cur++ ) {
		if (   bufferText[cur]   == 'P'
			&& bufferText[cur+1] == 'O'
			&& bufferText[cur+2] == 'I'
			&& bufferText[cur+3] == 'N'
			&& bufferText[cur+4] == 'T'
			&& bufferText[cur+5] == 'E'
			&& bufferText[cur+6] == 'R'
			&& bufferText[cur+7] == ' '
			&& bufferText[cur+8] == '@'
			&& bufferText[cur+9] == ' '
			&& bufferText[cur+10] == '0'
			&& bufferText[cur+11] == 'x' )
		{
			cur += 12;
			int start = cur;
			for ( ; bufferText[cur] != '\0'; cur++ );
			pointerstring.assign(&bufferText[start], &bufferText[cur]);
			string_to_bytearray(pointerstring, pointerbytes, pointerbytessize);
			
			// cur is now at the NULL after POINTER and before JPN NAME
			for ( ; bufferText[cur] != 'J'; cur++ );

			if (   bufferText[cur]   == 'J'
				&& bufferText[cur+1] == 'P'
				&& bufferText[cur+2] == 'N'
				&& bufferText[cur+3] == ' '
				&& bufferText[cur+4] == 'N'
				&& bufferText[cur+5] == 'A'
				&& bufferText[cur+6] == 'M'
				&& bufferText[cur+7] == 'E'
				&& bufferText[cur+8] == ' '
				&& bufferText[cur+9] == '@'
				&& bufferText[cur+10] == ' '
				&& bufferText[cur+11] == '0'
				&& bufferText[cur+12] == 'x' )
			{
				cur += 13;
				start = cur;
				for ( ; bufferText[cur] != ':'; cur++ );
				jpnnamepointerstring.assign(&bufferText[start], &bufferText[cur]);
				string_to_bytearray(jpnnamepointerstring, jpnnamepointerbytes, jpnnamepointerbytessize);
				
				cur++;

				// cur is now at start of jpn name string
				start = cur;
				for ( ; bufferText[cur] != '\0'; cur++ );
				char * jpnname = new char[cur-start+1];
				memcpy(jpnname, &bufferText[start], cur-start+1);
			
				// cur is now at the NULL after JPNNAME and before JPNTEXT
				for ( ; bufferText[cur] != 'J'; cur++ );

				if (   bufferText[cur]   == 'J'
					&& bufferText[cur+1] == 'P'
					&& bufferText[cur+2] == 'N'
					&& bufferText[cur+3] == ' '
					&& bufferText[cur+4] == 'T'
					&& bufferText[cur+5] == 'E'
					&& bufferText[cur+6] == 'X'
					&& bufferText[cur+7] == 'T'
					&& bufferText[cur+8] == ' '
					&& bufferText[cur+9] == '@'
					&& bufferText[cur+10] == ' '
					&& bufferText[cur+11] == '0'
					&& bufferText[cur+12] == 'x' )
				{
					cur += 13;
					start = cur;
					for ( ; bufferText[cur] != ':'; cur++ );
					jpntextpointerstring.assign(&bufferText[start], &bufferText[cur]);
					string_to_bytearray(jpntextpointerstring, jpntextpointerbytes, jpntextpointerbytessize);
					
					cur++;

					// cur is now at start of jpn text string
					start = cur;
					for ( ; bufferText[cur] != '\0'; cur++ );
					char * jpntext = new char[cur-start+1];
					memcpy(jpntext, &bufferText[start], cur-start+1);
				
					// cur is now at the NULL after JPNTEXT and before ENGNAME
					for ( ; bufferText[cur] != 'E'; cur++ );

					if (   bufferText[cur]   == 'E'
						&& bufferText[cur+1] == 'N'
						&& bufferText[cur+2] == 'G'
						&& bufferText[cur+3] == ' '
						&& bufferText[cur+4] == 'N'
						&& bufferText[cur+5] == 'A'
						&& bufferText[cur+6] == 'M'
						&& bufferText[cur+7] == 'E'
						&& bufferText[cur+8] == ' '
						&& bufferText[cur+9] == '@'
						&& bufferText[cur+10] == ' '
						&& bufferText[cur+11] == '0'
						&& bufferText[cur+12] == 'x' )
					{
						cur += 13;
						start = cur;
						for ( ; bufferText[cur] != ':'; cur++ );
						engnamepointerstring.assign(&bufferText[start], &bufferText[cur]);
						string_to_bytearray(engnamepointerstring, engnamepointerbytes, engnamepointerbytessize);
						
						cur++;

						// cur is now at start of eng name string
						start = cur;
						for ( ; bufferText[cur] != '\0'; cur++ );
						char * engname = new char[cur-start+1];
						memcpy(engname, &bufferText[start], cur-start+1);
					
						// cur is now at the NULL after ENGNAME and before ENGTEXT
						for ( ; bufferText[cur] != 'E'; cur++ );

						if (   bufferText[cur]   == 'E'
							&& bufferText[cur+1] == 'N'
							&& bufferText[cur+2] == 'G'
							&& bufferText[cur+3] == ' '
							&& bufferText[cur+4] == 'T'
							&& bufferText[cur+5] == 'E'
							&& bufferText[cur+6] == 'X'
							&& bufferText[cur+7] == 'T'
							&& bufferText[cur+8] == ' '
							&& bufferText[cur+9] == '@'
							&& bufferText[cur+10] == ' '
							&& bufferText[cur+11] == '0'
							&& bufferText[cur+12] == 'x' )
						{
							cur += 13;
							start = cur;
							for ( ; bufferText[cur] != ':'; cur++ );
							engtextpointerstring.assign(&bufferText[start], &bufferText[cur]);
							string_to_bytearray(engtextpointerstring, engtextpointerbytes, engtextpointerbytessize);
							
							cur++;

							// cur is now at start of eng name string
							start = cur;
							for ( ; bufferText[cur] != '\0'; cur++ );
							char * engtext = new char[cur-start+1];
							memcpy(engtext, &bufferText[start], cur-start+1);
							
							// fetch the two values before the pointers
							unsigned int pointer;
							memcpy(&pointer, pointerbytes, 4);
							swap_endian(pointer);

							unsigned int * blockptrp = (unsigned int *)&buffer[pointer];
							unsigned int blockptr = *blockptrp;
							swap_endian( blockptr );

							char * value1 = new char[4];
							char * value2 = new char[4];
							value1[0] = buffer[blockptr+textstart];
							value1[1] = buffer[blockptr+textstart+1];
							value1[2] = buffer[blockptr+textstart+2];
							value1[3] = buffer[blockptr+textstart+3];
							value2[0] = buffer[blockptr+textstart+4];
							value2[1] = buffer[blockptr+textstart+5];
							value2[2] = buffer[blockptr+textstart+6];
							value2[3] = buffer[blockptr+textstart+7];

							// -----------------------------------------------
							// quickfix thing
							// also fetch the actual pointers cause fuck it the text files are broken already
							jpnnamepointerbytes = new unsigned char[4];
							jpntextpointerbytes = new unsigned char[4];
							engnamepointerbytes = new unsigned char[4];
							engtextpointerbytes = new unsigned char[4];
							jpnnamepointerbytes[0] = buffer[blockptr+textstart+8];
							jpnnamepointerbytes[1] = buffer[blockptr+textstart+9];
							jpnnamepointerbytes[2] = buffer[blockptr+textstart+10];
							jpnnamepointerbytes[3] = buffer[blockptr+textstart+11];
							jpntextpointerbytes[0] = buffer[blockptr+textstart+12];
							jpntextpointerbytes[1] = buffer[blockptr+textstart+13];
							jpntextpointerbytes[2] = buffer[blockptr+textstart+14];
							jpntextpointerbytes[3] = buffer[blockptr+textstart+15];
							engnamepointerbytes[0] = buffer[blockptr+textstart+16];
							engnamepointerbytes[1] = buffer[blockptr+textstart+17];
							engnamepointerbytes[2] = buffer[blockptr+textstart+18];
							engnamepointerbytes[3] = buffer[blockptr+textstart+19];
							engtextpointerbytes[0] = buffer[blockptr+textstart+20];
							engtextpointerbytes[1] = buffer[blockptr+textstart+21];
							engtextpointerbytes[2] = buffer[blockptr+textstart+22];
							engtextpointerbytes[3] = buffer[blockptr+textstart+23];

							unsigned int tempshit;
							memcpy( &tempshit, jpnnamepointerbytes, 4);
							swap_endian(tempshit);
							tempshit += textstart;
							swap_endian(tempshit);
							memcpy( jpnnamepointerbytes, &tempshit, 4);

							memcpy( &tempshit, jpntextpointerbytes, 4);
							swap_endian(tempshit);
							tempshit += textstart;
							swap_endian(tempshit);
							memcpy( jpntextpointerbytes, &tempshit, 4);

							memcpy( &tempshit, engnamepointerbytes, 4);
							swap_endian(tempshit);
							tempshit += textstart;
							swap_endian(tempshit);
							memcpy( engnamepointerbytes, &tempshit, 4);

							memcpy( &tempshit, engtextpointerbytes, 4);
							swap_endian(tempshit);
							tempshit += textstart;
							swap_endian(tempshit);
							memcpy( engtextpointerbytes, &tempshit, 4);
							// -----------------------------------------------

							// create textblock and insert into list
							textblock * currentblock = new textblock(pointer,
								value1, value2,
								(char*)jpnnamepointerbytes, jpnname,
								(char*)jpntextpointerbytes, jpntext,
								(char*)engnamepointerbytes, engname,
								(char*)engtextpointerbytes, engtext  );
							textblocklist->push_back(currentblock);
						}
					}
				}

			}
		}
	}

	char * null0x18 = new char[0x18];
	memset(null0x18, 0x00, 0x18);

	unsigned int pointer;

	int first_deleted_text = filesize;

	for (std::list<textblock*>::iterator i = textblocklist->begin(); i != textblocklist->end(); ++i) {
		unsigned int * bptrp = (unsigned int *)&buffer[(*i)->blockpointer];
		unsigned int bptr = *bptrp;
		swap_endian( bptr );

		memcpy( &buffer[bptr+textstart], null0x18, 0x18 );

		
		memcpy(&pointer, (*i)->jpnnamepointer, 4);
		swap_endian(pointer);
		
		if ( pointer < first_deleted_text ) first_deleted_text = pointer;

		for ( ; buffer[pointer] != '\0'; pointer++) buffer[pointer] = '\0';
		memcpy(&pointer, (*i)->jpntextpointer, 4);
		swap_endian(pointer);
		for ( ; buffer[pointer] != '\0'; pointer++) buffer[pointer] = '\0';
		memcpy(&pointer, (*i)->engnamepointer, 4);
		swap_endian(pointer);
		for ( ; buffer[pointer] != '\0'; pointer++) buffer[pointer] = '\0';
		memcpy(&pointer, (*i)->engtextpointer, 4);
		swap_endian(pointer);
		for ( ; buffer[pointer] != '\0'; pointer++) buffer[pointer] = '\0';
	}

	int emptyamount;
	int emptyptr;
	unsigned int emptyptrswap;

	// put text
	for (std::list<textblock*>::iterator i = textblocklist->begin(); i != textblocklist->end(); ++i) {
		emptyamount = (*i)->engnamelen;
		emptyamount += (*i)->engtextlen;
		emptyamount += 33; // 16 bytes puffer on each side plus one for the null between the strings
		emptyptr = findempty(&buffer[0], textstart, textsize, emptyamount, filesize, first_deleted_text, &nullcheck[0], true);
		
		emptyptr += 16;

		emptyptrswap = (unsigned int)emptyptr;
		swap_endian(emptyptrswap);

		memcpy( &buffer[emptyptr+textstart], (*i)->engname, (*i)->engnamelen );
		memcpy( (*i)->jpnnamepointer, &emptyptrswap, 4 );

		emptyptr += (*i)->engnamelen;
		emptyptr++;
		emptyptrswap = (unsigned int)emptyptr;
		swap_endian(emptyptrswap);

		memcpy( &buffer[emptyptr+textstart], (*i)->engtext, (*i)->engtextlen );
		memcpy( (*i)->jpntextpointer, &emptyptrswap, 4 );
	}

	// put pointers
	emptyamount = textblocklist->size();

	if ( textblocklist->size() != 0 ) {

		int blockCount = textblocklist->size();

		while ( !textblocklist->empty() ) {
			int spaceToFind = blockCount;
			spaceToFind *= 4; //size of one pointer
			spaceToFind *= 6; //amount of pointers
			spaceToFind += 36; //buffer
			emptyptr = findempty(&buffer[0], textstart, textsize, spaceToFind, filesize, first_deleted_text, &nullcheck[0], false); // try to find memory in existing region

			if ( emptyptr == -1 ) { // not enough contiguous memory available
				blockCount--; // decrement the amount of blocks we put at once
				if ( blockCount == 0 ) { // welp guess we actually have to increase our filesize!
					blockCount = textblocklist->size();
					spaceToFind = blockCount;
					spaceToFind *= 4; //size of one pointer
					spaceToFind *= 6; //amount of pointers
					spaceToFind += 36; //buffer
					emptyptr = findempty(&buffer[0], textstart, textsize, spaceToFind, filesize, first_deleted_text, &nullcheck[0], true);
				} else {
					continue; // and restart loop
				}
			}

			emptyptr += 16;

			int fourbytecheck = emptyptr%4;
			for ( ; fourbytecheck%4 != 0; fourbytecheck++) emptyptr++;

			for ( int cnt = 0; cnt < blockCount; ++cnt ) {
				textblock* tbptr = textblocklist->front();
				textblocklist->pop_front();

				memcpy( &buffer[emptyptr+textstart], tbptr->value1, 4);
				memcpy( &buffer[emptyptr+textstart+4], tbptr->value2, 4);
				memcpy( &buffer[emptyptr+textstart+8], tbptr->jpnnamepointer, 4);
				memcpy( &buffer[emptyptr+textstart+12], tbptr->jpntextpointer, 4);
				// putting the same pointers here so that both pointers of each type point at the same string
				memcpy( &buffer[emptyptr+textstart+16], tbptr->jpnnamepointer, 4);
				memcpy( &buffer[emptyptr+textstart+20], tbptr->jpntextpointer, 4);
				unsigned int location = tbptr->blockpointer;
				unsigned int newpointer = emptyptr;
				swap_endian(newpointer);
				memcpy( &buffer[location], &newpointer, 4);
				emptyptr += 0x18;
			}

			blockCount = textblocklist->size(); // try to find max again if still necessary
		}
	}
	// put pointers end

	outfile.write(buffer, filesize);
	outfile.close();
	

	delete[] buffer;
	return 0;
}