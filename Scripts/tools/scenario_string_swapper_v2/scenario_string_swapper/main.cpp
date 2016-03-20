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
	
	if ( argc != 2 ) {
		cout << "Usage: " << argv[0] << " file_to_swap" << endl;
		return -1;
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

	
	char * newfilename = new char[300];
	newfilename[0] = 0;
	strcat( newfilename, filename );
	strcat( newfilename, ".swap" );
	ofstream outfile(newfilename, std::ios::binary);
	


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

			unsigned int whatever = blockptr+textstart+8;

			if ( len4 != 0 ) {
				// swap JPN with ENG pointers in the written outfile
				char * pointers = new char[16];
				memcpy(pointers, &buffer[whatever], 16);
				buffer[whatever]    = pointers[0x08];
				buffer[whatever+1]  = pointers[0x09];
				buffer[whatever+2]  = pointers[0x0A];
				buffer[whatever+3]  = pointers[0x0B];
				buffer[whatever+4]  = pointers[0x0C];
				buffer[whatever+5]  = pointers[0x0D];
				buffer[whatever+6]  = pointers[0x0E];
				buffer[whatever+7]  = pointers[0x0F];
				buffer[whatever+8]  = pointers[0x00];
				buffer[whatever+9]  = pointers[0x01];
				buffer[whatever+10] = pointers[0x02];
				buffer[whatever+11] = pointers[0x03];
				buffer[whatever+12] = pointers[0x04];
				buffer[whatever+13] = pointers[0x05];
				buffer[whatever+14] = pointers[0x06];
				buffer[whatever+15] = pointers[0x07];
			}
		}
	}

	outfile.write(buffer, filesize);
	outfile.close();
	

	delete[] buffer;
	return 0;
}