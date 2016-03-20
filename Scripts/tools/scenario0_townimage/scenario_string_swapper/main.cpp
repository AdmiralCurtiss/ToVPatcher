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
		cout << "Usage: " << argv[0] << " file_to_edit" << endl;
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
	
	char * newfilename = new char[300];
	newfilename[0] = 0;
	strcat( newfilename, filename );
	strcat( newfilename, ".new" );
	ofstream outfile(newfilename, std::ios::binary);
	
	char swap;

	// swap US Pointer to tile data structure with JPN Pointer
	swap =            buffer[0x156DC];
	buffer[0x156DC] = buffer[0x15710];
	buffer[0x15710] = swap;
	swap =            buffer[0x156DD];
	buffer[0x156DD] = buffer[0x15711];
	buffer[0x15711] = swap;
	swap =            buffer[0x156DE];
	buffer[0x156DE] = buffer[0x15712];
	buffer[0x15712] = swap;
	swap =            buffer[0x156DF];
	buffer[0x156DF] = buffer[0x15713];
	buffer[0x15713] = swap;

	// swap amount of tiles displayed between US and JP
	for ( int cur = 0x25104; cur < 0x27144; cur += 0xAC ) {
		swap = buffer[cur+0xA7];
		buffer[cur+0xA7] = buffer[cur+0xAB];
		buffer[cur+0xAB] = swap;
	}

	// swap the "tiles" offset
	for ( int cur = 0x24F7C; cur < 0x25104; cur += 0x08 ) {
		for ( int stuff = 0; stuff < 4; stuff++ ) {
			swap = buffer[cur+stuff];
			buffer[cur+stuff] = buffer[cur+stuff+0x04];
			buffer[cur+stuff+0x04] = swap;
		}
	}

	outfile.write(buffer, filesize);
	outfile.close();
	

	delete[] newfilename;
	delete[] buffer;
	return 0;
}