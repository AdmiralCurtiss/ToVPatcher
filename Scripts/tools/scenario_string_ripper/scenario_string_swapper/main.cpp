#include <iostream>
#include <fstream>
#include <string.h>
using namespace std;

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

int main(int argc, char ** argv) {

	bool print_additional_pointers = false;
	
	if ( argc != 2 && argc != 3 ) {
		cout << "Usage: " << argv[0] << " file_to_extract [-morepointers]" << endl;
		return -1;
	}

	if ( argc == 3 ) {
		if ( strcmp(argv[2], "-morepointers") == 0 ) {
			print_additional_pointers = true;
		}
	}
	
	char * filename = argv[1];


	unsigned long long filesize;

	std::ifstream file;
	file.open(filename, std::ios::binary);

	if ( !file.is_open() ) return -1;

	file.seekg (0, ios::end);
	filesize = file.tellg();
	file.seekg (0, ios::beg);

	char * buffer = new char[filesize+35];
	memset( &buffer[filesize], 0, 35 );

	file.read(buffer, filesize);
	file.close();

	
	unsigned int * textstartp = (unsigned int *)&buffer[0x0C];
	unsigned int textstart = *textstartp;
	swap_endian( textstart );

	unsigned int * textsizep = (unsigned int *)&buffer[0x18];
	unsigned int textsize = *textsizep;
	swap_endian( textsize );

	/*
	char textstartbyte0 = buffer[0x0C];
	char textstartbyte1 = buffer[0x0D];
	char textstartbyte2 = buffer[0x0E];
	char textstartbyte3 = buffer[0x0F];
	*/

	/*
	char * newfilename = new char[300];
	newfilename[0] = 0;
	strcat( newfilename, filename );
	strcat( newfilename, ".swap" );
	ofstream outfile(newfilename, std::ios::binary);
	*/

	char * textdumpfilename = new char[300];
	textdumpfilename[0] = 0;
	strcat( textdumpfilename, filename );
	strcat( textdumpfilename, ".txt" );
	ofstream textdumpfile(textdumpfilename, std::ios::binary);

	char * charrayLocation = new char[100];
	char * charrayPointer = new char[100];
	unsigned int lastpointer = 0;

	for ( int cur = 0x20; cur < textstart; cur += 4 ) {
		unsigned int * identifyerp = (unsigned int *)&buffer[cur];
		unsigned int identifyer = *identifyerp;
		swap_endian( identifyer );

		if ( identifyer == 0x040C0018 ) {
			unsigned int * blockptrp = (unsigned int *)&buffer[cur+4];
			unsigned int blockptr = *blockptrp;
			swap_endian( blockptr );

			unsigned int * ptr1p = (unsigned int *)&buffer[blockptr+textstart+8];
			unsigned int ptr1 = *ptr1p;
			swap_endian( ptr1 );
			unsigned int * ptr2p = (unsigned int *)&buffer[blockptr+textstart+8+4];
			unsigned int ptr2 = *ptr2p;
			swap_endian( ptr2 );
			unsigned int * ptr3p = (unsigned int *)&buffer[blockptr+textstart+8+8];
			unsigned int ptr3 = *ptr3p;
			swap_endian( ptr3 );
			unsigned int * ptr4p = (unsigned int *)&buffer[blockptr+textstart+8+12];
			unsigned int ptr4 = *ptr4p;
			swap_endian( ptr4 );

			int len1 = getbytestillnull(&buffer[ptr1+textstart]);
			int len2 = getbytestillnull(&buffer[ptr2+textstart]);
			int len3 = getbytestillnull(&buffer[ptr3+textstart]);
			int len4 = getbytestillnull(&buffer[ptr4+textstart]);

			// dump the string contents
			sprintf(charrayLocation, "%x", cur+4);
			textdumpfile.write( "POINTER @ 0x", 12);
			textdumpfile.write( charrayLocation, getbytestillnull(charrayLocation) );
			textdumpfile.write( "\0\r\n", 3);
			
			textdumpfile.write( "JPN NAME ", 9);
			if ( print_additional_pointers ) {
				textdumpfile.write( "0x", 2);
				sprintf(charrayPointer, "%x", blockptr+8);
				textdumpfile.write( charrayPointer, getbytestillnull(charrayPointer) );
				textdumpfile.write( " ", 1);
			}
			textdumpfile.write( "@ 0x", 4);
			sprintf(charrayPointer, "%x", ptr1+textstart);
			textdumpfile.write( charrayPointer, getbytestillnull(charrayPointer) );
			textdumpfile.write( ":", 1);
			textdumpfile.write( &buffer[ptr1+textstart], getbytestillnull(&buffer[ptr1+textstart]));
			textdumpfile.write( "\0\r\n", 3);

			textdumpfile.write( "JPN TEXT ", 9);
			if ( print_additional_pointers ) {
				textdumpfile.write( "0x", 2);
				sprintf(charrayPointer, "%x", blockptr+8+4);
				textdumpfile.write( charrayPointer, getbytestillnull(charrayPointer) );
				textdumpfile.write( " ", 1);
			}
			textdumpfile.write( "@ 0x", 4);
			sprintf(charrayPointer, "%x", ptr2+textstart);
			textdumpfile.write( charrayPointer, getbytestillnull(charrayPointer) );
			textdumpfile.write( ":", 1);
			textdumpfile.write( &buffer[ptr2+textstart], getbytestillnull(&buffer[ptr2+textstart]));
			textdumpfile.write( "\0\r\n", 3);

			textdumpfile.write( "ENG NAME ", 9);
			if ( print_additional_pointers ) {
				textdumpfile.write( "0x", 2);
				sprintf(charrayPointer, "%x", blockptr+8+8);
				textdumpfile.write( charrayPointer, getbytestillnull(charrayPointer) );
				textdumpfile.write( " ", 1);
			}
			textdumpfile.write( "@ 0x", 4);
			sprintf(charrayPointer, "%x", ptr3+textstart);
			textdumpfile.write( charrayPointer, getbytestillnull(charrayPointer) );
			textdumpfile.write( ":", 1);
			textdumpfile.write( &buffer[ptr3+textstart], getbytestillnull(&buffer[ptr3+textstart]));
			textdumpfile.write( "\0\r\n", 3);

			textdumpfile.write( "ENG TEXT ", 9);
			if ( print_additional_pointers ) {
				textdumpfile.write( "0x", 2);
				sprintf(charrayPointer, "%x", blockptr+8+12);
				textdumpfile.write( charrayPointer, getbytestillnull(charrayPointer) );
				textdumpfile.write( " ", 1);
			}
			textdumpfile.write( "@ 0x", 4);
			sprintf(charrayPointer, "%x", ptr4+textstart);
			textdumpfile.write( charrayPointer, getbytestillnull(charrayPointer) );
			textdumpfile.write( ":", 1);
			textdumpfile.write( &buffer[ptr4+textstart], getbytestillnull(&buffer[ptr4+textstart]));
			textdumpfile.write( "\0\r\n", 3);

			textdumpfile.write( "----------------", 16);
			textdumpfile.write( "\r\n", 2);
		}
			//
		/*
		unsigned int * ptr5p = (unsigned int *)&buffer[cur+16];
		unsigned int ptr5 = *ptr5p;
		swap_endian( ptr5 );
		unsigned int * ptr6p = (unsigned int *)&buffer[cur+20];
		unsigned int ptr6 = *ptr6p;
		swap_endian( ptr6 );

		unsigned int * ptrn1p = (unsigned int *)&buffer[cur-4];
		unsigned int ptrn1 = *ptrn1p;
		swap_endian( ptrn1 );
		unsigned int * ptrn2p = (unsigned int *)&buffer[cur-8];
		unsigned int ptrn2 = *ptrn2p;
		swap_endian( ptrn2 );

		
		 check if pointers are: 
		   - not null
		   - smaller than the file
		   - smaller than the current location (since all pointers seem to be AFTER the corresponding text)
		   - in order
		
		if (   ptr1 != 0 && ptr1 < textsize && ptr1 < cur-textstart
			&& ptr2 != 0 && ptr2 < textsize && ptr2 < cur-textstart
			&& ptr3 != 0 && ptr3 < textsize && ptr3 < cur-textstart
			&& ptr4 != 0 && ptr4 < textsize && ptr4 < cur-textstart
			&& lastpointer < ptr1
			&& ptr1 < ptr2
			&& ptr2 < ptr3
			&& ptr3 < ptr4
			//&& ptr5 != 0 
			//&& ptr6 != 0 
			) {
			// we have (hopefully) found one of those textblock's pointers...
			// I kinda doubt this is completely correct but let's go with it for now.
			// swap pointer1 with pointer3, pointer2 with pointer4

				// more checks
				// get length of all strings
				int len1 = getbytestillnull(&buffer[ptr1+textstart]);
				int len2 = getbytestillnull(&buffer[ptr2+textstart]);
				int len3 = getbytestillnull(&buffer[ptr3+textstart]);
				int len4 = getbytestillnull(&buffer[ptr4+textstart]);
				// and check if the length of each string matches the next pointer
				if (   ptr1+len1+1 != ptr2
					|| ptr2+len2+1 != ptr3
					|| ptr3+len3+1 != ptr4 ) {
					outfile.write( &buffer[cur], 4 );
					continue;
				}
				



				// swap JPN with ENG pointers in the written outfile
				char * pointers = new char[16];
				memcpy(pointers, &buffer[cur], 16);
				buffer[cur]    = pointers[0x08];
				buffer[cur+1]  = pointers[0x09];
				buffer[cur+2]  = pointers[0x0A];
				buffer[cur+3]  = pointers[0x0B];
				buffer[cur+4]  = pointers[0x0C];
				buffer[cur+5]  = pointers[0x0D];
				buffer[cur+6]  = pointers[0x0E];
				buffer[cur+7]  = pointers[0x0F];
				buffer[cur+8]  = pointers[0x00];
				buffer[cur+9]  = pointers[0x01];
				buffer[cur+10] = pointers[0x02];
				buffer[cur+11] = pointers[0x03];
				buffer[cur+12] = pointers[0x04];
				buffer[cur+13] = pointers[0x05];
				buffer[cur+14] = pointers[0x06];
				buffer[cur+15] = pointers[0x07];

				
				lastpointer = ptr4;

				outfile.write( &buffer[cur], 16 );
				cur += 16;
		}
		outfile.write( &buffer[cur], 4 );
		*/
	}

	textdumpfile.close();
	

	delete[] buffer;
	return 0;
}