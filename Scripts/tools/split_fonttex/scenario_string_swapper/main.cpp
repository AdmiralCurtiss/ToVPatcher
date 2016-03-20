#include <iostream>
#include <fstream>
#include <string.h>
#include <string>
#include <sstream>
#include <list>
using namespace std;

void writedxt5header(std::ostream& os) {
	static char* dxt5h = "\x44\x44\x53\x20\x7C\x00\x00\x00\x07\x10\x0A\x00\x00\x02\x00\x00\x00\x02\x00\x00\x00\x00\x04\x00\x00\x00\x00\x00\x01\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x20\x00\x00\x00\x04\x00\x00\x00\x44\x58\x54\x35\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x08\x10\x40\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00";
	for ( int i = 0; i < 0x80; i++ ) {
		os << dxt5h[i];
	}
}

void split(char * filename) {
	unsigned long long filesize;

	std::ifstream file;
	file.open(filename, std::ios::binary);

	if ( !file.is_open() ) return;

	file.seekg (0, ios::end);
	filesize = file.tellg();
	file.seekg (0, ios::beg);

	char * buffer = new char[filesize];
	file.read(buffer, filesize);
	file.close();
	


	int i = 0;
	int block = 0;
	while ( i < filesize ) {
		char fn1[512];
		char fn2[512];
		char fn3[512];
		char fn4[512];

		sprintf(fn1, "%s_%d.dds", filename, block*4);
		sprintf(fn2, "%s_%d.dds", filename, block*4+1);
		sprintf(fn3, "%s_%d.dds", filename, block*4+2);
		sprintf(fn4, "%s_%d.dds", filename, block*4+3);

		std::ofstream file1;
		std::ofstream file2;
		std::ofstream file3;
		std::ofstream file4;
		file1.open(fn1, std::ios::binary);
		file2.open(fn2, std::ios::binary);
		file3.open(fn3, std::ios::binary);
		file4.open(fn4, std::ios::binary);

		writedxt5header(file1);
		writedxt5header(file2);
		writedxt5header(file3);
		writedxt5header(file4);

		for ( int x = 0; x < 128; x++ ) {
		for ( int y = 0; y < 128; y++ ) {
			/*
			Each tile is 16 bytes (4x4 pixels)

			0 1 2 3 0 1 2 3 0 1 2 3 ....... 4 5 6 7 4 5 6 7 4 5 6 7 ...... 8 9 10 11 8 9 10 11 .... 12 13 14 15 12 13 14 15 .....

			And tiles are arranged sequentially in one image block:
			0[0] 0[1] 0[2] 0[3] (x512/4)
			0[4] 0[5] 0[6] 0[7]
			*/

			for ( int j = 0; j < 16; j++ ) {
				file1 << buffer[i+j];
			}
			i += 16;
			for ( int j = 0; j < 16; j++ ) {
				file2 << buffer[i+j];
			}
			i += 16;
			for ( int j = 0; j < 16; j++ ) {
				file3 << buffer[i+j];
			}
			i += 16;
			for ( int j = 0; j < 16; j++ ) {
				file4 << buffer[i+j];
			}
			i += 16;
		}
		}

		file1.close();
		file2.close();
		file3.close();
		file4.close();

		block++;
	}



	delete[] buffer;
}

void combine(char** filenames, int files) {
		char fn[512];

		sprintf(fn, "%s_new.txv", filenames[0]);

		std::ofstream file;
		file.open(fn, std::ios::binary);

		
		for ( int i = 0; i < files; i += 4 ) {
			std::ifstream infile1;
			std::ifstream infile2;
			std::ifstream infile3;
			std::ifstream infile4;
			infile1.open(filenames[i], std::ios::binary);
			infile2.open(filenames[i+1], std::ios::binary);
			infile3.open(filenames[i+2], std::ios::binary);
			infile4.open(filenames[i+3], std::ios::binary);

			if ( infile1.is_open() && infile2.is_open() && infile3.is_open() && infile4.is_open() ) {
				infile1.seekg(0, ios::end);
				int filesize = infile1.tellg();

				infile1.seekg(0x80);
				infile2.seekg(0x80);
				infile3.seekg(0x80);
				infile4.seekg(0x80);


				for ( int k = 0; k < filesize-0x80; k += 16 )
				{
					char x;
					for (int j = 0; j < 16; j++) {
						infile1.get(x);
						file.put(x);
					}
					for (int j = 0; j < 16; j++) {
						infile2.get(x);
						file.put(x);
					}
					for (int j = 0; j < 16; j++) {
						infile3.get(x);
						file.put(x);
					}
					for (int j = 0; j < 16; j++) {
						infile4.get(x);
						file.put(x);
					}
				}
			}

		}


		file.close();
}

int main(int argc, char ** argv) {
	
	if ( argc == 2 ) {
		split(argv[1]);
	} else if ( argc == 17 ) {
		combine(&argv[1], 16);
	} else {
		cout << "Usage for split: " << argv[0] << " FONTTEX.TXV" << endl;
		cout << "Usage for recombine: " << argv[0] << " file1.dds file2.dds file3.dds ... (16 files)" << endl;
		return -1;
	}

	return 0;
}
