﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace HyoutaTools.Tales.Vesperia.FPS4 {
	public class FPS4 : IDisposable {
		public FPS4() {
			ContentBitmask = 0x000F;
			Alignment = 0x800;
		}
		public FPS4( string inFilename ) {
			if ( !LoadFile( inFilename ) ) {
				throw new Exception( "Failed loading FPS4: " + inFilename );
			}
		}
		~FPS4() {
			Close();
		}

		FileStream infile = null;
		uint FileCount;
		uint HeaderSize;
		uint FirstFileStart;
		ushort EntrySize;
		public ushort ContentBitmask;
		public uint Unknown2;
		uint ArchiveNameLocation;

		public string ArchiveName = "";
		public uint Alignment;

		// -- bitmask examples --
		// 0x000F -> loc, end, size, name
		// 0x0007 -> loc, end, size
		// 0x0047 -> loc, end, size, ptr to path? attributes? something like that
		public bool ContainsStartPointers { get { return ( ContentBitmask & 0x0001 ) == 0x0001; } }
		public bool ContainsEndPointers { get { return ( ContentBitmask & 0x0002 ) == 0x0002; } }
		public bool ContainsFileSizes { get { return ( ContentBitmask & 0x0004 ) == 0x0004; } }
		public bool ContainsFilenames { get { return ( ContentBitmask & 0x0008 ) == 0x0008; } }
		public bool ContainsFiletypes { get { return ( ContentBitmask & 0x0020 ) == 0x0020; } }
		public bool ContainsFileMetadata { get { return ( ContentBitmask & 0x0040 ) == 0x0040; } }

		private bool LoadFile( string inFilename ) {
			try {
				infile = new FileStream( inFilename, FileMode.Open );
			} catch ( Exception ) {
				Console.WriteLine( "ERROR: can't open " + inFilename );
				return false;
			}

			infile.Seek( 0x00, SeekOrigin.Begin );
			string magic = infile.ReadAscii( 4 );
			if ( magic != "FPS4" ) {
				Console.WriteLine( "Not an FPS4 file!" );
				return false;
			}

			FileCount = infile.ReadUInt32().SwapEndian();
			HeaderSize = infile.ReadUInt32().SwapEndian();
			FirstFileStart = infile.ReadUInt32().SwapEndian();
			EntrySize = infile.ReadUInt16().SwapEndian();
			ContentBitmask = infile.ReadUInt16().SwapEndian();
			Unknown2 = infile.ReadUInt32().SwapEndian();
			ArchiveNameLocation = infile.ReadUInt32().SwapEndian();
			infile.Position = ArchiveNameLocation;
			if ( ArchiveNameLocation > 0 ) {
				ArchiveName = infile.ReadShiftJisNullterm();
			}

			Alignment = FirstFileStart;

			Console.WriteLine( "Content Bitmask: 0x" + ContentBitmask.ToString( "X4" ) );

			return true;
		}

		public void Extract( string dirname, bool noMetadataParsing = false ) {
			System.IO.Directory.CreateDirectory( dirname );

			for ( uint i = 0; i < FileCount - 1; ++i ) {
				infile.Seek( HeaderSize + ( i * EntrySize ), SeekOrigin.Begin );

				uint fileloc = infile.ReadUInt32().SwapEndian();
				uint fileend = fileloc + infile.ReadUInt32().SwapEndian();
				uint filesize = infile.ReadUInt32().SwapEndian();
				if ( fileloc == 0xFFFFFFFF || fileloc == 0x00 ) {
					Console.WriteLine( "Skipped #" + i.ToString( "D4" ) + ", can't find file" );
					continue;
				}

				string filename = "";
				if ( ContainsFilenames ) {
					filename = infile.ReadAsciiNullterm();
				}

				string extension = "";
				if ( ContainsFiletypes ) {
					extension = '.' + infile.ReadAscii( 4 ).TrimNull();
				}

				string path = "";
				if ( ContainsFileMetadata && !noMetadataParsing ) {
					uint pathLocation = infile.ReadUInt32().SwapEndian();
					if ( pathLocation != 0x00 ) {
						long tmp = infile.Position;
						infile.Seek( pathLocation, SeekOrigin.Begin );
						path = infile.ReadAsciiNullterm();
						infile.Seek( tmp, SeekOrigin.Begin );

						if ( path.StartsWith( "name=" ) ) {
							path = path.Substring( 5 );
						}

						int finaldir = path.LastIndexOf( '/' );
						if ( filename == "" ) {
							filename = path.Substring( finaldir + 1 ) + '.' + i.ToString( "D4" );
						} else {
							filename = path.Substring( finaldir + 1 ) + '.' + filename;
						}

						if ( finaldir == -1 ) {
							path = "";
						} else {
							path = path.Substring( 0, finaldir );
						}
					}
				}
				//string description = Util.GetText((int)(Util.SwapEndian(BitConverter.ToUInt32(b, (int)(headersize + (i * entrysize) + 0x2C)))), b);

				if ( filename == "" ) {
					filename = i.ToString( "D4" );
				}

				FileStream outfile;
				try {
					System.IO.Directory.CreateDirectory( dirname + '/' + path );
					outfile = new FileStream( dirname + '/' + path + '/' + filename + extension, FileMode.Create );
				} catch ( Exception ) {
					try {
						outfile = new FileStream( dirname + '/' + path + '/' + i.ToString( "D4" ) + extension, FileMode.Create );
					} catch ( Exception ) {
						try {
							outfile = new FileStream( dirname + '/' + i.ToString( "D4" ) + extension, FileMode.Create );
						} catch ( Exception ) {
							outfile = new FileStream( dirname + '/' + i.ToString( "D4" ), FileMode.Create );
						}
					}
				}

				Console.WriteLine( "Extracting #" + i.ToString( "D4" ) + ": " + path + '/' + System.IO.Path.GetFileName( outfile.Name ) );

				try {
					infile.Seek( fileloc, SeekOrigin.Begin );
					Util.CopyStream( infile, outfile, (int)filesize );
				} catch ( Exception ) {
					try {
						infile.Seek( fileloc, SeekOrigin.Begin );
						Util.CopyStream( infile, outfile, (int)( fileend - fileloc ) );
					} catch ( Exception ) {
						Console.WriteLine( "ERROR on file " + outfile.Name );
					}
				}
				outfile.Close();
			}
		}

		public void Pack( string inPath, string outFilename, string metadata = null ) {
			var files = Util.DirectoryGetFilesWorkaround( inPath );
			Pack( files, outFilename, metadata );
		}
		public void Pack( string[] files, string outFilename, string metadata = null ) {
			FileCount = (uint)files.Length + 1;
			HeaderSize = 0x1C;

			EntrySize = 0;
			if ( ContainsStartPointers ) { EntrySize += 4; }
			if ( ContainsEndPointers ) { EntrySize += 4; }
			if ( ContainsFileSizes ) { EntrySize += 4; }
			if ( ContainsFilenames ) { EntrySize += 0x20; }
			if ( ContainsFiletypes ) { EntrySize += 4; }
			if ( ContainsFileMetadata ) { EntrySize += 4; }

			uint HeaderEnd = HeaderSize + EntrySize * FileCount;
			if ( !String.IsNullOrEmpty( ArchiveName ) ) {
				ArchiveNameLocation = HeaderEnd;
			}

			if ( infile == null ) { // <- this is hacky and may create broken files when using -o with changed filenames/filecount/metadata/etc
				FirstFileStart = HeaderEnd.Align( Alignment ); // <- this is inaccurate and will break with metadata
			}

			using ( FileStream f = new FileStream( outFilename, FileMode.Create ) ) {
				// header
				f.Write( Encoding.ASCII.GetBytes( "FPS4" ), 0, 4 );
				f.WriteUInt32( FileCount.SwapEndian() );
				f.WriteUInt32( HeaderSize.SwapEndian() );
				f.WriteUInt32( FirstFileStart.SwapEndian() );
				f.WriteUInt16( EntrySize.SwapEndian() );
				f.WriteUInt16( ContentBitmask.SwapEndian() );
				f.WriteUInt32( Unknown2.SwapEndian() );
				f.WriteUInt32( ArchiveNameLocation.SwapEndian() );

				// file list
				uint ptr = FirstFileStart;
				for ( int i = 0; i < files.Length; ++i ) {
					var fi = new System.IO.FileInfo( files[i] );
					if ( ContainsStartPointers ) { f.WriteUInt32( ptr.SwapEndian() ); }
					if ( ContainsEndPointers ) { f.WriteUInt32( ( (uint)( fi.Length.Align( (int)Alignment ) ) ).SwapEndian() ); }
					if ( ContainsFileSizes ) { f.WriteUInt32( ( (uint)( fi.Length ) ).SwapEndian() ); }
					if ( ContainsFilenames ) {
						string filename = fi.Name.Truncate( 0x1F );
						byte[] fnbytes = Util.ShiftJISEncoding.GetBytes( filename );
						f.Write( fnbytes, 0, fnbytes.Length );
						for ( int j = fnbytes.Length; j < 0x20; ++j ) {
							f.WriteByte( 0 );
						}
					}
					if ( ContainsFiletypes ) {
						string extension = fi.Extension.TrimStart( '.' ).Truncate( 4 );
						byte[] extbytes = Util.ShiftJISEncoding.GetBytes( extension );
						f.Write( extbytes, 0, extbytes.Length );
						for ( int j = extbytes.Length; j < 4; ++j ) {
							f.WriteByte( 0 );
						}
					}
					if ( ContainsFileMetadata ) {
						if ( infile != null ) {
							// copy this from original file, very hacky when filenames/filecount/metadata changes
							infile.Position = f.Position;
							f.WriteUInt32( infile.ReadUInt32() );
						} else {
							// place a null for now and go back to fix later
							f.WriteUInt32( 0 );
						}
					}
					ptr += (uint)fi.Length.Align( (int)Alignment );
				}

				// at the end of the file list, a final entry pointing to the end of the container
				f.WriteUInt32( ptr.SwapEndian() );
				for ( int j = 4; j < EntrySize; ++j ) {
					f.WriteByte( 0 );
				}

				// the idea is to write a pointer here
				// and at the target of the pointer you have a nullterminated string
				// with all the metadata in a param=data format separated by spaces
				// maybe including a filepath at the start without a param=
				// strings should be after the filelist block but before the actual files
				if ( ContainsFileMetadata && metadata != null ) {
					for ( int i = 0; i < files.Length; ++i ) {
						var fi = new System.IO.FileInfo( files[i] );
						long ptrPos = 0x1C + ( ( i + 1 ) * EntrySize ) - 4;

						// remember pos
						uint oldPos = (uint)f.Position;
						// jump to metaptr
						f.Position = ptrPos;
						// write remembered pos
						f.WriteUInt32( oldPos.SwapEndian() );
						// jump to remembered pos
						f.Position = oldPos;
						// write meta + nullterm
						if ( metadata.Contains( 'p' ) ) {
							string relativePath = GetRelativePath( f.Name, fi.FullName );
							f.Write( Util.ShiftJISEncoding.GetBytes( relativePath ) );
						}
						if ( metadata.Contains( 'n' ) ) {
							f.Write( Util.ShiftJISEncoding.GetBytes( "name=" + Path.GetFileNameWithoutExtension( fi.FullName ) ) );
						}
						f.WriteByte( 0 );
					}
				}

				// write original archive filepath
				if ( !String.IsNullOrEmpty( ArchiveName ) ) {
					byte[] archiveNameBytes = Util.ShiftJISEncoding.GetBytes( ArchiveName );
					f.Write( archiveNameBytes, 0, archiveNameBytes.Length );
					f.WriteByte( 0 );
				}

				// fix up header if we inserted metadata
				if ( ContainsFileMetadata && metadata != null ) {
					long pos = f.Position;

					FirstFileStart = ( (uint)( f.Position ) ).Align( Alignment );
					f.Position = 0xC;
					f.WriteUInt32( FirstFileStart.SwapEndian() );

					ptr = FirstFileStart;
					for ( int i = 0; i < files.Length; ++i ) {
						f.Position = 0x1C + ( i * EntrySize );
						var fi = new System.IO.FileInfo( files[i] );
						if ( ContainsStartPointers ) { f.WriteUInt32( ptr.SwapEndian() ); }
						if ( ContainsEndPointers ) { f.WriteUInt32( ( (uint)( fi.Length.Align( (int)Alignment ) ) ).SwapEndian() ); }
						ptr += (uint)fi.Length.Align( (int)Alignment );
					}
					f.Position = 0x1C + ( files.Length * EntrySize );
					f.WriteUInt32( ptr.SwapEndian() );

					f.Position = pos;
				}

				// pad until files
				if ( infile != null ) {
					infile.Position = f.Position;
					for ( long i = f.Length; i < FirstFileStart; ++i ) {
						f.WriteByte( (byte)infile.ReadByte() );
					}
				} else {
					for ( long i = f.Length; i < FirstFileStart; ++i ) {
						f.WriteByte( 0 );
					}
				}

				// actually write files
				for ( int i = 0; i < files.Length; ++i ) {
					using ( var fs = new System.IO.FileStream( files[i], FileMode.Open ) ) {
						Console.WriteLine( "Packing #" + i.ToString( "D4" ) + ": " + files[i] );
						Util.CopyStream( fs, f, (int)fs.Length );
						while ( f.Length % Alignment != 0 ) { f.WriteByte( 0 ); }
					}
				}
			}
		}

		public void Close() {
			if ( infile != null ) {
				infile.Close();
				infile = null;
			}
		}

		public void Dispose() {
			Close();
		}

		// FIXME: this works only with pretty specific input, good enough for what I need but certainly not usable for generic cases
		private string GetRelativePath( string fromDirectory, string toFile ) {
			fromDirectory = System.IO.Path.GetDirectoryName( System.IO.Path.GetFullPath( fromDirectory ) );
			string relative = toFile.Substring( fromDirectory.Length );
			relative = String.Join( "/", relative.Split( new char[] { '\\', '/' }, StringSplitOptions.RemoveEmptyEntries ).Skip( 1 ).ToArray() );
			if ( relative.Contains( '.' ) ) {
				relative = relative.Substring( 0, relative.LastIndexOf( '.' ) );
			}

			return relative;
		}
	}
}
