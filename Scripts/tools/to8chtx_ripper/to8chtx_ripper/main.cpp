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

typedef struct skittext_s {
	unsigned int name;
	unsigned int jpn;
	unsigned int eng;
	unsigned int something;
} skittext;

int main(int argc, char ** argv) {
	
	if ( argc != 2 ) {
		cout << "Usage: " << argv[0] << " file_to_rip" << endl;
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

	
	unsigned int * textstartp = (unsigned int *)&buffer[0x14];
	unsigned int textstart = *textstartp;
	swap_endian( textstart );

	unsigned int * entriesp = (unsigned int *)&buffer[0x0C];
	unsigned int entries = *entriesp;
	swap_endian( entries );

	char * textdumpfilename = new char[300];
	textdumpfilename[0] = 0;
	strcat( textdumpfilename, filename );
	strcat( textdumpfilename, ".txt" );
	ofstream textdumpfile(textdumpfilename, std::ios::binary);

	for ( int i = 0; i < entries; i++ ) {
		skittext skitline;
		memcpy(&skitline, &buffer[0x20+(i*0x10)], 0x10);

		swap_endian(skitline.name);
		swap_endian(skitline.jpn);
		swap_endian(skitline.eng);
		swap_endian(skitline.something);

		textdumpfile.write("NAME:", 5);
		textdumpfile.write(&buffer[textstart + skitline.name], getbytestillnull(&buffer[textstart + skitline.name]));
		textdumpfile.write("\0\n", 2);
		textdumpfile.write("JPN:", 4);
		textdumpfile.write(&buffer[textstart + skitline.jpn], getbytestillnull(&buffer[textstart + skitline.jpn]));
		textdumpfile.write("\0\n", 2);
		textdumpfile.write("ENG:", 4);
		textdumpfile.write(&buffer[textstart + skitline.eng], getbytestillnull(&buffer[textstart + skitline.eng]));
		textdumpfile.write("\0\n", 2);
		textdumpfile.write("--------", 8);
		textdumpfile.write("\0\n", 2);
	}

	textdumpfile.close();
	

	delete[] buffer;
	return 0;
}