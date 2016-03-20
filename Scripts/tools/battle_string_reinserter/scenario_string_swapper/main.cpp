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
	char * engtextpointer; char * engtext;

	unsigned int textlocation;
	unsigned int englishtextlocation;
	int engtextlen;

	textblock(int blockpointer,
			  char * engtextpointer, char * engtext, unsigned int textlocation, unsigned int englishtextlocation )
	{
		this->blockpointer = blockpointer;
		this->engtextpointer = engtextpointer;
		this->engtext = engtext;
		this->textlocation = textlocation;
		this->englishtextlocation = englishtextlocation;
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

bool filesize_increased = false;

int findempty(char * buffer, int textstart, int maxsize, int emptyspace, unsigned __int64& filesize) {
	if ( !filesize_increased ) {
		int emptyfound = 0;
		for ( int i = textstart; i < maxsize+textstart; i++ ) {
			if ( buffer[i] == '\0' ) { emptyfound++; }
			else { emptyfound = 0; }

			if ( emptyfound >= emptyspace ) {
				return i - textstart - emptyfound + 1;
			}
		}

		// if lack of space: Allocate new space
		std::cout << "WARNING: Text too long for original area, performing experimental filesize increase." << std::endl;
		filesize_increased = true;
	}
	
	int j;
	for ( j = filesize; j < emptyspace+filesize; j++ ) {
		buffer[j] = 0x00;
	}
	int oldsize = filesize;
	filesize = j;

	int textsize = j;

	char * bytes = new char[4];
	memcpy(bytes, &textsize, 4);

	buffer[0x0C] = bytes[3];
	buffer[0x0D] = bytes[2];
	buffer[0x0E] = bytes[1];
	buffer[0x0F] = bytes[0];

	return oldsize - textstart;

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
		cout << "Usage: " << argv[0] << " battlefile TextToInsert" << endl;
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

	char * buffer = new char[filesize+32000];
	memset( &buffer[filesize], 0xFF, 32000 );

	file.read(buffer, filesize);
	file.close();

	
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


	char * newfilename = new char[300];
	newfilename[0] = 0;
	strcat( newfilename, filename );
	strcat( newfilename, ".new" );
	ofstream outfile(newfilename, std::ios::binary);

	char * charrayLocation = new char[100];
	char * charrayPointer = new char[100];
	unsigned int lastpointer = 0;
	



	unsigned int pointerdifference = 0;
	memcpy( &pointerdifference, &buffer[0x24], 4 );
	swap_endian(pointerdifference);
	pointerdifference -= 0x400;




	
	
	std::string engtextpointerstring;
	unsigned char* engtextpointerbytes = NULL;
	int engtextpointerbytessize; //should always be 4
	
	int start = 0;

	for ( int cur = 0; cur < filesizeText; cur++ ) {
		if (   bufferText[cur]   == 'T'
			&& bufferText[cur+1] == 'E'
			&& bufferText[cur+2] == 'X'
			&& bufferText[cur+3] == 'T'
			&& bufferText[cur+4] == ' '
			&& bufferText[cur+5] == '@'
			&& bufferText[cur+6] == ' '
			&& bufferText[cur+7] == '0'
			&& bufferText[cur+8] == 'x' )
		{
			cur += 9;
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

			unsigned int pointer;
			memcpy(&pointer, engtextpointerbytes, 4);
			swap_endian(pointer);		




			unsigned int textlocation;
			memcpy( &textlocation, &buffer[pointer], 4 );
			swap_endian(textlocation);
			textlocation += pointerdifference;


			unsigned int englishtextlocation;
			memcpy( &englishtextlocation, &buffer[pointer+8], 4 );
			swap_endian(englishtextlocation);
			englishtextlocation += pointerdifference;



			// create textblock and insert into list
			textblock * currentblock = new textblock(
				pointer, (char*)engtextpointerbytes, engtext, textlocation, englishtextlocation
				);
			textblocklist->push_back(currentblock);
		}
	}

	char * null0x18 = new char[0x18];
	memset(null0x18, 0x00, 0x18);

	unsigned int pointer;

	std::list<textblock*>::iterator firstentry = textblocklist->begin();
	unsigned int firsttextlocation = (*firstentry)->textlocation;

	//empty out old text
	for (std::list<textblock*>::iterator i = textblocklist->begin(); i != textblocklist->end(); ++i) {
		pointer = (*i)->textlocation;
		for ( ; buffer[pointer] != '\0'; pointer++) buffer[pointer] = '\0';
		pointer = (*i)->englishtextlocation;
		for ( ; buffer[pointer] != '\0'; pointer++) buffer[pointer] = '\0';
	}

	int emptyamount;
	int emptyptr;
	unsigned int emptyptrswap;

	// saves one byte at the start of the textblock
	firsttextlocation--;

	// put new text
	for (std::list<textblock*>::iterator i = textblocklist->begin(); i != textblocklist->end(); ++i) {
		emptyamount = (*i)->engtextlen;
		if ( emptyamount == 0 ) {
			(*i)->textlocation = firsttextlocation;
			(*i)->textlocation -= pointerdifference;
			continue;
		}
		
		// See if currently inserting string is identical to a previous one,
		// if yes set current textlocation to that and don't actually insert anything
		bool identical = false;
		for (std::list<textblock*>::iterator j = textblocklist->begin(); j != i; ++j) {
			if ( (*i)->engtextlen == (*j)->engtextlen && strcmp( (*i)->engtext, (*j)->engtext ) == 0 ) {
				(*i)->textlocation = (*j)->textlocation;
				identical = true;
			}
		}
		if (identical) continue;

		emptyamount += 2; // 1 null byte before and after the string
		emptyptr = findempty(&buffer[0], firsttextlocation, filesize, emptyamount, filesize);
		
		emptyptr += 1;

		emptyptrswap = (unsigned int)emptyptr;
		swap_endian(emptyptrswap);

		memcpy( &buffer[emptyptr+firsttextlocation], (*i)->engtext, (*i)->engtextlen );
		memcpy( &(*i)->textlocation, &emptyptr, 4 );
		(*i)->textlocation += firsttextlocation;
		(*i)->textlocation -= pointerdifference;
	}

	// put pointers
	//*
	for (std::list<textblock*>::iterator i = textblocklist->begin(); i != textblocklist->end(); ++i) {
		unsigned int textloc;
		// if the english text is empty, set the text location to something that's actually pointing at a null
		// in this case firsttextlocation points at the null right before the first inserted line so we'll just use that
		if ( (*i)->engtext[0] != '\0' ) { 
			textloc = (*i)->textlocation;
		} else {
			textloc = firsttextlocation;
			textloc -= pointerdifference;
		}
		unsigned int pointerloc = (*i)->blockpointer;
		swap_endian(textloc);

		memcpy( &buffer[ pointerloc ], &textloc, 4); // put new pointer over old JPN pointer
		memcpy( &buffer[pointerloc+8], &textloc, 4); // put new pointer over old ENG pointer
	}

	//*/

	outfile.write(buffer, filesize);
	outfile.close();
	

	delete[] buffer;
	return 0;
}