using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using HyoutaTools;
using HyoutaTools.Tales.Vesperia.FPS4;
using HyoutaTools.Tales.tlzc;
using System.ComponentModel;

namespace ToVPatcher {
	class Patcher {
		public static string CalcMd5( string filename ) {
			using ( var md5 = MD5.Create() ) {
				using ( var stream = File.OpenRead( filename ) ) {
					byte[] hash = md5.ComputeHash( stream );
					return BitConverter.ToString( hash ).Replace( "-", "" ).ToLower();
				}
			}
		}
		public static void CompareMd5( string path, string md5 ) {
			string hash = CalcMd5( path );
			if ( hash != md5 ) {
				throw new PatchingException(
					"Source file does not appear to be ripped correctly. " +
					"MD5 should be " + md5 + " but is " + hash + "."
				);
			}
		}
		public static void CompareMd5Output( string path, string md5 ) {
			string hash = CalcMd5( path );
			if ( hash != md5 ) {
				File.Delete( path );
				throw new PatchingException(
					"File did not patch correctly. " +
					"New MD5 should be " + md5 + " but is " + hash + "."
				);
			}
		}

		public static void XdeltaApply( string original, string patched, string patch ) {
			Logger.LogFileData( original, "xdelta, infile" );
			Logger.LogFileData( patch, "xdelta, patch" );
			try {
				if ( !Util.RunProgram( "xdelta", "-d -f -s \"" + original + "\" \"" + patch + "\" \"" + patched + "\"", false, false, true ) ) {
					throw new PatchingException( "Patching failed: " + patch );
				}
			} catch ( Win32Exception e ) {
				throw new PatchingException( "Failed during execution of xdelta. Make sure xdelta can be found at " + Path.GetFullPath( "xdelta.exe" ) + " and try again." );
			}
			Logger.LogFileData( patched, "xdelta, outfile" );
		}

		static void ComptoeDecompress( string infile, string outfile ) {
			Logger.LogFileData( infile, "comptoe decomp, infile" );
			try {
				if ( !Util.RunProgram( "comptoe", "-d \"" + infile + "\" \"" + outfile + "\"", false, false, true ) ) {
					throw new PatchingException( "Decompression failed: " + infile );
				}
			} catch ( Win32Exception e ) {
				throw new PatchingException( "Failed during execution of comptoe. Make sure comptoe can be found at " + Path.GetFullPath( "comptoe.exe" ) + " and try again." );
			}
			Logger.LogFileData( outfile, "comptoe decomp, outfile" );
		}
		static void ComptoeCompress( string infile, string outfile ) {
			Logger.LogFileData( infile, "comptoe comp, infile" );
			try {
				if ( !Util.RunProgram( "comptoe", "-c1 \"" + infile + "\" \"" + outfile + "\"", false, false, true ) ) {
					throw new PatchingException( "Compression failed: " + infile );
				}
			} catch ( Win32Exception e ) {
				throw new PatchingException( "Failed during execution of comptoe. Make sure comptoe can be found at " + Path.GetFullPath( "comptoe.exe" ) + " and try again." );
			}
			Logger.LogFileData( outfile, "comptoe comp, outfile" );
		}

		static void tlzcDecompress( string infile, string outfile ) {
			Logger.LogFileData( infile, "tlzc decomp, infile" );
			byte[] input = File.ReadAllBytes( infile );
			var output = TLZC.Decompress( input );
			File.WriteAllBytes( outfile, output );
			Logger.LogFileData( outfile, "tlzc decomp, outfile" );
		}
		static void tlzcCompress( string infile, string outfile ) {
			Logger.LogFileData( infile, "tlzc comp, infile" );
			byte[] input = File.ReadAllBytes( infile );
			var output = TLZC.Compress( input, 4, 64 );
			File.WriteAllBytes( outfile, output );
			Logger.LogFileData( outfile, "tlzc comp, outfile" );
		}
		static string tlzcDecompressToTempFile( string infile ) {
			string outfile = TempUtil.GetTempFileName();
			tlzcDecompress( infile, outfile );
			return outfile;
		}

		static string svoExtractToTempDir( string infile, bool nometa = false ) {
			string extractPath = TempUtil.GetTempFileName();
			if ( Directory.Exists( extractPath ) ) {
				Util.DeleteDirectoryAggressive( extractPath, true );
			}
			Directory.CreateDirectory( extractPath );
			using ( var fps4 = new FPS4( infile ) ) {
				fps4.Extract( extractPath, nometa );
			}
			Logger.LogDirData( extractPath, "FPS4 extract of " + infile );
			return extractPath;
		}

		static void BlockCopy( string pathFrom, int locationFrom, string pathTo, int locationTo, int count ) {
			Logger.LogFileData( pathFrom, "blockcopy, infile, 0x" + locationFrom.ToString( "X" ) + ", " + count + " bytes" );
			Logger.LogFileData( pathTo, "blockcopy, target, pre-copy, 0x" + locationTo.ToString( "X" ) );
			var fileFrom = System.IO.File.ReadAllBytes( pathFrom );
			var fileTo = System.IO.File.ReadAllBytes( pathTo );
			Util.CopyByteArrayPart( fileFrom, locationFrom, fileTo, locationTo, count );
			System.IO.File.WriteAllBytes( pathTo, fileTo );
			Logger.LogFileData( pathTo, "blockcopy, target, post-copy" );
		}

		static void PatchGeneric( string originalPath, string patchDir, string outDir, string filename, string patchname, string md5, string outMd5 = null ) {
			if ( !File.Exists( originalPath ) ) {
				throw new PatchingException( "File not found: " + originalPath );
			}
			// if patched file exists and matches, exit early
			string outPath = Path.Combine( outDir, filename );
			try { CompareMd5Output( outPath, outMd5 ); return; } catch ( PatchingException ) { } catch ( FileNotFoundException ) { }
			CompareMd5( originalPath, md5 );

			// then patch!
			System.IO.Directory.CreateDirectory( outDir );
			XdeltaApply(
				originalPath,
				outPath,
				Path.Combine( patchDir, patchname )
			);

			if ( outMd5 != null ) { CompareMd5Output( outPath, outMd5 ); }
		}

		public static void PatchString( string stringSvo, string patchDir, string outDir, string outMd5 = null, BackgroundWorker worker = null ) {
			PatchGeneric( stringSvo, patchDir, outDir, "string.svo", "string.svo.xdelta3", "831bf148d6c1e2002a6a94b43cfe8f6c", outMd5 );
		}
		public static void PatchMenu( string menuSvo, string patchDir, string outDir, string outMd5 = null, BackgroundWorker worker = null ) {
			PatchGeneric( menuSvo, patchDir, outDir, "menu.svo", "menu.svo.xdelta3", "28bcf828e03b708683a55db7c9306274", outMd5 );
		}
		public static void PatchScenario( string scenarioPath, string patchDir, string outDir, string outMd5 = null, BackgroundWorker worker = null ) {
			if ( !File.Exists( scenarioPath ) ) {
				throw new PatchingException( "File not found: " + scenarioPath );
			}
			if ( worker != null ) { worker.ReportProgress( 0, "Confirming source file..." ); }
			// if patched file exists and matches, exit early
			string outPath = Path.Combine( outDir, "scenario.dat" );
			try { CompareMd5Output( outPath, outMd5 ); return; } catch ( PatchingException ) { } catch ( FileNotFoundException ) { }
			CompareMd5( scenarioPath, "4ef82c6ebc5f1303b07c97aa848db123" );
			Logger.LogFileData( scenarioPath, "scenario.dat" );
			Logger.LogDirData( patchDir, "scenario patches" );

			// extract scenario.dat
			if ( worker != null ) { worker.ReportProgress( 0, "Extracting source file..." ); }
			string extractPath = TempUtil.GetTempFileName();
			if ( Directory.Exists( extractPath ) ) {
				Util.DeleteDirectoryAggressive( extractPath, true );
			}
			Directory.CreateDirectory( extractPath );
			var scenario = new HyoutaTools.Tales.Vesperia.Scenario.ScenarioDat( new System.IO.FileStream( scenarioPath, System.IO.FileMode.Open ) );
			scenario.Extract( extractPath );

			// patch all files
			int i = 0;
			var files = Util.DirectoryGetFilesWorkaround( extractPath );
			foreach ( var f in files ) {
				if ( worker != null ) {
					worker.ReportProgress( ( i / files.Length ) * 100, "Patching file " + ( i + 1 ) + " of " + files.Length + "..." );
				}

				var tempfileDecomp = TempUtil.GetTempFileName();
				var tempfilePatched = TempUtil.GetTempFileName();

				ComptoeDecompress( f, tempfileDecomp );
				XdeltaApply(
					tempfileDecomp,
					tempfilePatched,
					Path.Combine( patchDir, Path.GetFileNameWithoutExtension( f ) + ".xdelta3" )
				);
				ComptoeCompress( tempfilePatched, f );

				File.Delete( tempfileDecomp );
				File.Delete( tempfilePatched );

				++i;
			}

			// pack it back up
			if ( worker != null ) { worker.ReportProgress( 100, "Packing modified file..." ); }
			Logger.LogDirData( extractPath, "scenario dir patched" );
			using ( var scenarioNew = new HyoutaTools.Tales.Vesperia.Scenario.ScenarioDat() ) {
				scenarioNew.Import( extractPath );
				scenarioNew.Write( outPath );
			}
			Logger.LogFileData( outPath, "scenario.dat patched" );

			// clean up
			Util.DeleteDirectoryAggressive( extractPath, true );

			if ( outMd5 != null ) { CompareMd5Output( outPath, outMd5 ); }
		}
		public static void PatchBtl( string btlPath, string patchDir, string outDir, string outMd5 = null, BackgroundWorker worker = null ) {
			if ( !File.Exists( btlPath ) ) {
				throw new PatchingException( "File not found: " + btlPath );
			}
			if ( worker != null ) { worker.ReportProgress( 0, "Confirming source file..." ); }
			// if patched file exists and matches, exit early
			string outPath = Path.Combine( outDir, "btl.svo" );
			try { CompareMd5Output( outPath, outMd5 ); return; } catch ( PatchingException ) { } catch ( FileNotFoundException ) { }
			CompareMd5( btlPath, "37bed259717dd27e5145d8899e7c36d9" );
			Logger.LogFileData( btlPath, "btl.svo" );
			Logger.LogDirData( patchDir, "btl patches" );

			// extract
			if ( worker != null ) { worker.ReportProgress( 0, "Extracting source file..." ); }
			string extractPath = svoExtractToTempDir( btlPath );
			string btlPackPath = svoExtractToTempDir( Path.Combine( extractPath, "BTL_PACK.DAT" ) );
			string file3Path = svoExtractToTempDir( Path.Combine( btlPackPath, "0003" ), nometa: true );

			// patch
			int i = 0;
			var files = Util.DirectoryGetFilesWorkaround( patchDir );
			foreach ( string patch in files ) {
				if ( worker != null ) {
					worker.ReportProgress( ( i / files.Length ) * 100, "Patching file " + ( i + 1 ) + " of " + files.Length + "..." );
				}
				string fileName = Path.GetFileNameWithoutExtension( patch );

				string sourcePath = Path.Combine( file3Path, "0" + fileName );
				string decompressedPath = tlzcDecompressToTempFile( sourcePath );
				string patchedPath = TempUtil.GetTempFileName();
				XdeltaApply( decompressedPath, patchedPath, patch );
				tlzcCompress( patchedPath, sourcePath );

				System.IO.File.Delete( decompressedPath );
				System.IO.File.Delete( patchedPath );

				++i;
			}

			// pack
			if ( worker != null ) { worker.ReportProgress( 100, "Packing modified file..." ); }
			Logger.LogDirData( file3Path, "btl/btl_pack/3 dir patched" );
			using ( var fps4file3 = new FPS4( Path.Combine( btlPackPath, "0003" ) ) ) {
				fps4file3.Alignment = 0x80;
				fps4file3.Pack( file3Path, Path.Combine( btlPackPath, "0003.new" ) );
			}
			File.Delete( Path.Combine( btlPackPath, "0003" ) );
			File.Move( Path.Combine( btlPackPath, "0003.new" ), Path.Combine( btlPackPath, "0003" ) );
			Util.DeleteDirectoryAggressive( file3Path, true );

			using ( var fps4btlPack = new FPS4( Path.Combine( extractPath, "BTL_PACK.DAT" ) ) ) {
				fps4btlPack.Alignment = 0x80;
				fps4btlPack.Pack( btlPackPath, Path.Combine( extractPath, "BTL_PACK.DAT.new" ) );
			}
			File.Delete( Path.Combine( extractPath, "BTL_PACK.DAT" ) );
			File.Move( Path.Combine( extractPath, "BTL_PACK.DAT.new" ), Path.Combine( extractPath, "BTL_PACK.DAT" ) );
			Util.DeleteDirectoryAggressive( btlPackPath, true );

			Logger.LogDirData( extractPath, "btl dir patched" );
			using ( var fps4btl = new FPS4( btlPath ) ) {
				fps4btl.Alignment = 0x800;
				fps4btl.Pack( extractPath, outPath );
			}
			Logger.LogFileData( outPath, "btl.svo patched" );
			Util.DeleteDirectoryAggressive( extractPath, true );

			if ( outMd5 != null ) { CompareMd5Output( outPath, outMd5 ); }
		}
		public static void PatchChat( string chatPath, string patchDir, string outDir, string outMd5 = null, BackgroundWorker worker = null ) {
			if ( !File.Exists( chatPath ) ) {
				throw new PatchingException( "File not found: " + chatPath );
			}
			if ( worker != null ) { worker.ReportProgress( 0, "Confirming source file..." ); }
			// if patched file exists and matches, exit early
			string outPath = Path.Combine( outDir, "chat.svo" );
			try { CompareMd5Output( outPath, outMd5 ); return; } catch ( PatchingException ) { } catch ( FileNotFoundException ) { }
			CompareMd5( chatPath, "7f0992514818e791ba64a987b6accf88" );
			Logger.LogFileData( chatPath, "chat.svo" );
			Logger.LogDirData( patchDir, "chat patches" );

			if ( worker != null ) { worker.ReportProgress( 0, "Extracting source file..." ); }
			string extractPath = svoExtractToTempDir( chatPath );
			var files = Util.DirectoryGetFilesWorkaround( extractPath );

			int i = 0;
			int filecount = files.Count( x => Path.GetFileName( x ).StartsWith( "VC" ) && Path.GetFileName( x ).EndsWith( ".DAT" ) );
			foreach ( string file in files ) {
				string filename = Path.GetFileName( file );
				if ( filename.StartsWith( "VC" ) && filename.EndsWith( ".DAT" ) ) {
					if ( worker != null ) {
						worker.ReportProgress( ( i / filecount ) * 100, "Patching file " + ( i + 1 ) + " of " + filecount + "..." );
					}

					string decompPath = tlzcDecompressToTempFile( file );
					string skitExtractPath = svoExtractToTempDir( decompPath );

					XdeltaApply(
						Path.Combine( skitExtractPath, "0003" ),
						Path.Combine( skitExtractPath, "0003.new" ),
						Path.Combine( patchDir, filename + ".xdelta3" )
					);
					File.Delete( Path.Combine( skitExtractPath, "0003" ) );
					File.Move( Path.Combine( skitExtractPath, "0003.new" ), Path.Combine( skitExtractPath, "0003" ) );

					using ( var fps4Skit = new FPS4( decompPath ) ) {
						fps4Skit.Alignment = 0x80;
						fps4Skit.Pack( skitExtractPath, Path.Combine( extractPath, filename + ".new" ) );
					}
					tlzcCompress( Path.Combine( extractPath, filename + ".new" ), file );

					File.Delete( decompPath );
					File.Delete( Path.Combine( extractPath, filename + ".new" ) );
					Util.DeleteDirectoryAggressive( skitExtractPath, true );

					++i;
				}
			}

			if ( worker != null ) { worker.ReportProgress( 100, "Packing modified file..." ); }
			Logger.LogDirData( extractPath, "chat dir patched" );
			using ( var fps4 = new FPS4( chatPath ) ) {
				fps4.Alignment = 0x800;
				fps4.Pack( extractPath, outPath );
			}
			Logger.LogFileData( outPath, "chat.svo patched" );
			Util.DeleteDirectoryAggressive( extractPath, true );

			if ( outMd5 != null ) { CompareMd5Output( outPath, outMd5 ); }
		}
		public static void PatchUI( string uiPath, string patchDir, string outDir, string outMd5 = null, BackgroundWorker worker = null ) {
			if ( !File.Exists( uiPath ) ) {
				throw new PatchingException( "File not found: " + uiPath );
			}
			if ( worker != null ) { worker.ReportProgress( 0, "Confirming source file..." ); }
			// if patched file exists and matches, exit early
			string outPath = Path.Combine( outDir, "UI.svo" );
			try { CompareMd5Output( outPath, outMd5 ); return; } catch ( PatchingException ) { } catch ( FileNotFoundException ) { }
			CompareMd5( uiPath, "9d0a479c838c4811e5df5f6a6815071d" );
			Logger.LogFileData( uiPath, "UI.svo" );
			Logger.LogDirData( patchDir, "UI patches" );

			// extract
			if ( worker != null ) { worker.ReportProgress( 0, "Extracting source file..." ); }
			string extractPath = svoExtractToTempDir( uiPath );

			// patch
			int i = 0;
			var files = Util.DirectoryGetFilesWorkaround( patchDir );
			foreach ( string patch in files ) {
				if ( worker != null ) {
					worker.ReportProgress( ( i / files.Length ) * 100, "Patching file " + ( i + 1 ) + " of " + files.Length + "..." );
				}
				string fileName = Path.GetFileNameWithoutExtension( patch );

				string sourcePath = Path.Combine( extractPath, fileName );
				string patchedPath = TempUtil.GetTempFileName();
				XdeltaApply( sourcePath, patchedPath, patch );
				File.Delete( sourcePath );
				File.Move( patchedPath, sourcePath );

				++i;
			}

			// copy unchanged 360 english files over japanese ones
			File.Copy( Path.Combine( extractPath, "MINIGAMEISHI_E.TXV" ), Path.Combine( extractPath, "MINIGAMEISHI.TXV" ), true );
			File.Copy( Path.Combine( extractPath, "EVENTMAP_E.TXV" ), Path.Combine( extractPath, "EVENTMAP.TXV" ), true );

			// copy english poker text textures to new PS3 poker deck
			BlockCopy( Path.Combine( extractPath, "MINIGAMEPOKER_E.TXV" ), 0x0, Path.Combine( extractPath, "MINIGAMEPOKER.TXV" ), 0x0, 0x6AB900 );
			BlockCopy( Path.Combine( extractPath, "MINIGAMEPOKER_E.TXV" ), 0x14519E0, Path.Combine( extractPath, "MINIGAMEPOKER.TXV" ), 0x14519E0, 0x5E620 );

			// pack
			if ( worker != null ) { worker.ReportProgress( 100, "Packing modified file..." ); }
			Logger.LogDirData( extractPath, "UI dir patched" );
			using ( var fps4btl = new FPS4( uiPath ) ) {
				fps4btl.Alignment = 0x800;
				fps4btl.Pack( extractPath, outPath );
			}
			Logger.LogFileData( outPath, "UI.svo patched" );
			Util.DeleteDirectoryAggressive( extractPath, true );

			if ( outMd5 != null ) { CompareMd5Output( outPath, outMd5 ); }
		}
		public static void PatchEffect( string effectPath, string patchDir, string outDir, string outMd5 = null, BackgroundWorker worker = null ) {
			if ( !File.Exists( effectPath ) ) {
				throw new PatchingException( "File not found: " + effectPath );
			}
			if ( worker != null ) { worker.ReportProgress( 0, "Confirming source file..." ); }
			// if patched file exists and matches, exit early
			string outPath = Path.Combine( outDir, "effect.svo" );
			try { CompareMd5Output( outPath, outMd5 ); return; } catch ( PatchingException ) { } catch ( FileNotFoundException ) { }
			CompareMd5( effectPath, "ada3bdb2e2ca481b44bc9e209b019dc8" );
			Logger.LogFileData( effectPath, "effect.svo" );
			Logger.LogDirData( patchDir, "effect patches" );

			// extract effect.svo
			if ( worker != null ) { worker.ReportProgress( 0, "Extracting source file..." ); }
			string extractPath = svoExtractToTempDir( effectPath );

			if ( worker != null ) {
				worker.ReportProgress( 0, "Patching files..." );
			}

			// image assets for SURPRISE ENCOUNTER! and so on
			string surpriseJpn = tlzcDecompressToTempFile( Path.Combine( extractPath, "E_A023.DAT" ) );
			string surpriseEng = tlzcDecompressToTempFile( Path.Combine( extractPath, "E_A034.DAT" ) );
			BlockCopy( surpriseEng, 0x100, surpriseJpn, 0x100, 0x80000 );
			tlzcCompress( surpriseJpn, Path.Combine( extractPath, "E_A023.DAT" ) );
			System.IO.File.Delete( surpriseJpn );
			System.IO.File.Delete( surpriseEng );

			// image assets for game over screen
			string gameover = tlzcDecompressToTempFile( Path.Combine( extractPath, "E_A104_GAMEOVER.DAT" ) );
			BlockCopy( gameover, 0x55D80, gameover, 0xD80, 0x55000 );
			BlockCopy( gameover, 0x55D80, gameover, 0xAAD80, 0x55000 );
			tlzcCompress( gameover, Path.Combine( extractPath, "E_A104_GAMEOVER.DAT" ) );
			System.IO.File.Delete( gameover );

			// image assets for dice minigame
			string dice1 = tlzcDecompressToTempFile( Path.Combine( extractPath, "E_MG_STONERESULT01.DAT" ) );
			BlockCopy( dice1, 0x100, dice1, 0x100100, 0x100000 );
			tlzcCompress( dice1, Path.Combine( extractPath, "E_MG_STONERESULT01.DAT" ) );
			System.IO.File.Delete( dice1 );

			string dice2 = tlzcDecompressToTempFile( Path.Combine( extractPath, "E_MG_STONERESULT02.DAT" ) );
			BlockCopy( dice2, 0x100, dice2, 0x100100, 0x100000 );
			tlzcCompress( dice2, Path.Combine( extractPath, "E_MG_STONERESULT02.DAT" ) );
			System.IO.File.Delete( dice2 );

			// remaining assets with xdelta patches
			foreach ( string patch in Util.DirectoryGetFilesWorkaround( patchDir ) ) {
				string fileName = Path.GetFileNameWithoutExtension( patch );

				string sourcePath = Path.Combine( extractPath, fileName + ".DAT" );
				string decompressedPath = tlzcDecompressToTempFile( sourcePath );
				string patchedPath = TempUtil.GetTempFileName();
				XdeltaApply( decompressedPath, patchedPath, patch );
				tlzcCompress( patchedPath, sourcePath );

				System.IO.File.Delete( decompressedPath );
				System.IO.File.Delete( patchedPath );
			}

			// pack up modified effect.svo
			if ( worker != null ) { worker.ReportProgress( 100, "Packing modified file..." ); }
			Logger.LogDirData( extractPath, "effect dir patched" );
			using ( var fps4 = new FPS4( effectPath ) ) {
				fps4.Alignment = 0x800;
				fps4.Pack( extractPath, outPath );
			}
			Logger.LogFileData( outPath, "effect.svo patched" );

			// clean up
			Util.DeleteDirectoryAggressive( extractPath, true );

			if ( outMd5 != null ) { CompareMd5Output( outPath, outMd5 ); }
		}
		public static void PatchChara( string charaPath, string patchDir, string outDir, string outMd5 = null, BackgroundWorker worker = null ) {
			if ( !File.Exists( charaPath ) ) {
				throw new PatchingException( "File not found: " + charaPath );
			}
			if ( worker != null ) { worker.ReportProgress( 0, "Confirming source file..." ); }
			// if patched file exists and matches, exit early
			string outPath = Path.Combine( outDir, "chara.svo" );
			try { CompareMd5Output( outPath, outMd5 ); return; } catch ( PatchingException ) { } catch ( FileNotFoundException ) { }
			CompareMd5( charaPath, "38984a5656b7a2faac3a7e24c962607e" );
			Logger.LogFileData( charaPath, "chara.svo" );
			Logger.LogDirData( patchDir, "chara patches" );

			if ( worker != null ) { worker.ReportProgress( 0, "Extracting source file..." ); }
			string extractPath = svoExtractToTempDir( charaPath );

			if ( worker != null ) {
				worker.ReportProgress( 0, "Patching files..." );
			}

			foreach ( var patchDirG in Directory.GetDirectories( patchDir ) ) {
				string charaFilePath = Path.Combine( extractPath, Path.GetFileName( patchDirG ) + ".DAT" );
				string decompPath = TempUtil.GetTempFileName();
				tlzcDecompress( charaFilePath, decompPath );
				string subDir = svoExtractToTempDir( decompPath );

				foreach ( var patchDirH in Directory.GetDirectories( patchDirG ) ) {
					string subPath = Path.Combine( subDir, Path.GetFileName( patchDirH ) );
					string subSubDir = svoExtractToTempDir( subPath, true );

					foreach ( var patch in Util.DirectoryGetFilesWorkaround( patchDirH ) ) {
						string toPatchCompressed = Path.Combine( subSubDir, Path.GetFileNameWithoutExtension( patch ) );
						string toPatch = TempUtil.GetTempFileName();
						tlzcDecompress( toPatchCompressed, toPatch );

						string patched = TempUtil.GetTempFileName();
						XdeltaApply( toPatch, patched, patch );
						File.Delete( toPatch );

						tlzcCompress( patched, toPatchCompressed );
						File.Delete( patched );
					}

					string newSubPath = TempUtil.GetTempFileName();
					using ( var fps4 = new FPS4( subPath ) ) {
						fps4.Alignment = 0x80;
						fps4.Pack( subSubDir, newSubPath );
					}
					File.Delete( subPath );
					File.Move( newSubPath, subPath );
					Util.DeleteDirectoryAggressive( subSubDir, true );
				}

				string newPath = TempUtil.GetTempFileName();
				using ( var fps4 = new FPS4( decompPath ) ) {
					fps4.Alignment = 0x80;
					fps4.Pack( subDir, newPath );
				}
				tlzcCompress( newPath, charaFilePath );
				File.Delete( newPath );
				File.Delete( decompPath );
				Util.DeleteDirectoryAggressive( subDir, true );
			}

			// dice minigame textures
			{
				string EP_0670_010 = Path.Combine( extractPath, "EP_0670_010.DAT" );
				string EP_0670_010decomp = tlzcDecompressToTempFile( EP_0670_010 );
				string EP_0670_010extract = svoExtractToTempDir( EP_0670_010decomp );
				{
					string EP_0670_010e_0002 = Path.Combine( EP_0670_010extract, "0002" );
					string EP_0670_010e_0002extract = svoExtractToTempDir( EP_0670_010e_0002, true );
					{
						string EP_0670_010e_0002e_0005 = Path.Combine( EP_0670_010e_0002extract, "0005" );
						string EP_0670_010e_0002e_0005decomp = tlzcDecompressToTempFile( EP_0670_010e_0002e_0005 );
						BlockCopy( EP_0670_010e_0002e_0005decomp, 0x100, EP_0670_010e_0002e_0005decomp, 0x100100, 0x100000 );
						tlzcCompress( EP_0670_010e_0002e_0005decomp, EP_0670_010e_0002e_0005 );
						File.Delete( EP_0670_010e_0002e_0005decomp );

						string EP_0670_010e_0002e_0006 = Path.Combine( EP_0670_010e_0002extract, "0006" );
						string EP_0670_010e_0002e_0006decomp = tlzcDecompressToTempFile( EP_0670_010e_0002e_0006 );
						BlockCopy( EP_0670_010e_0002e_0006decomp, 0x100, EP_0670_010e_0002e_0006decomp, 0x100100, 0x100000 );
						tlzcCompress( EP_0670_010e_0002e_0006decomp, EP_0670_010e_0002e_0006 );
						File.Delete( EP_0670_010e_0002e_0006decomp );
					}
					string EP_0670_010e_0002new = TempUtil.GetTempFileName();
					using ( var fps4 = new FPS4( EP_0670_010e_0002 ) ) {
						fps4.Alignment = 0x80;
						fps4.Pack( EP_0670_010e_0002extract, EP_0670_010e_0002new );
					}
					File.Delete( EP_0670_010e_0002 );
					File.Move( EP_0670_010e_0002new, EP_0670_010e_0002 );
					Util.DeleteDirectoryAggressive( EP_0670_010e_0002extract, true );
				}
				string EP_0670_010new = TempUtil.GetTempFileName();
				using ( var fps4 = new FPS4( EP_0670_010decomp ) ) {
					fps4.Alignment = 0x80;
					fps4.Pack( EP_0670_010extract, EP_0670_010new );
				}
				tlzcCompress( EP_0670_010new, EP_0670_010 );
				File.Delete( EP_0670_010decomp );
				File.Delete( EP_0670_010new );
				Util.DeleteDirectoryAggressive( EP_0670_010extract, true );
			}

			// "and they were never heard from again" textures
			{
				string GAMEOVER = Path.Combine( extractPath, "GAMEOVER.DAT" );
				string GAMEOVERdecomp = tlzcDecompressToTempFile( GAMEOVER );
				string GAMEOVERextract = svoExtractToTempDir( GAMEOVERdecomp );
				{
					string GAMEOVERe_0002 = Path.Combine( GAMEOVERextract, "0002" );
					string GAMEOVERe_0002extract = svoExtractToTempDir( GAMEOVERe_0002, true );
					{
						string GAMEOVERe_0002e_0001 = Path.Combine( GAMEOVERe_0002extract, "0001" );
						string GAMEOVERe_0002e_0001decomp = tlzcDecompressToTempFile( GAMEOVERe_0002e_0001 );
						BlockCopy( GAMEOVERe_0002e_0001decomp, 0x55D80, GAMEOVERe_0002e_0001decomp, 0xD80, 0x55000 );
						BlockCopy( GAMEOVERe_0002e_0001decomp, 0x55D80, GAMEOVERe_0002e_0001decomp, 0xAAD80, 0x55000 );
						tlzcCompress( GAMEOVERe_0002e_0001decomp, GAMEOVERe_0002e_0001 );
						File.Delete( GAMEOVERe_0002e_0001decomp );
					}
					string GAMEOVERe_0002new = TempUtil.GetTempFileName();
					using ( var fps4 = new FPS4( GAMEOVERe_0002 ) ) {
						fps4.Alignment = 0x80;
						fps4.Pack( GAMEOVERe_0002extract, GAMEOVERe_0002new );
					}
					File.Delete( GAMEOVERe_0002 );
					File.Move( GAMEOVERe_0002new, GAMEOVERe_0002 );
					Util.DeleteDirectoryAggressive( GAMEOVERe_0002extract, true );
				}
				string GAMEOVERnew = TempUtil.GetTempFileName();
				using ( var fps4 = new FPS4( GAMEOVERdecomp ) ) {
					fps4.Alignment = 0x80;
					fps4.Pack( GAMEOVERextract, GAMEOVERnew );
				}
				tlzcCompress( GAMEOVERnew, GAMEOVER );
				File.Delete( GAMEOVERdecomp );
				File.Delete( GAMEOVERnew );
				Util.DeleteDirectoryAggressive( GAMEOVERextract, true );
			}

			// more dice minigame textures why are there so many copies of those
			{
				string POR_C = Path.Combine( extractPath, "POR_C.DAT" );
				string POR_Cdecomp = tlzcDecompressToTempFile( POR_C );
				string POR_Cextract = svoExtractToTempDir( POR_Cdecomp, true );
				{
					string POR_Ce_0002 = Path.Combine( POR_Cextract, "0002" );
					string POR_Ce_0002extract = svoExtractToTempDir( POR_Ce_0002, true );
					{
						string POR_Ce_0002e_0026 = Path.Combine( POR_Ce_0002extract, "0026" );
						string POR_Ce_0002e_0026decomp = tlzcDecompressToTempFile( POR_Ce_0002e_0026 );
						BlockCopy( POR_Ce_0002e_0026decomp, 0x100, POR_Ce_0002e_0026decomp, 0x100100, 0x100000 );
						tlzcCompress( POR_Ce_0002e_0026decomp, POR_Ce_0002e_0026 );
						File.Delete( POR_Ce_0002e_0026decomp );

						string POR_Ce_0002e_0027 = Path.Combine( POR_Ce_0002extract, "0027" );
						string POR_Ce_0002e_0027decomp = tlzcDecompressToTempFile( POR_Ce_0002e_0027 );
						BlockCopy( POR_Ce_0002e_0027decomp, 0x100, POR_Ce_0002e_0027decomp, 0x100100, 0x100000 );
						tlzcCompress( POR_Ce_0002e_0027decomp, POR_Ce_0002e_0027 );
						File.Delete( POR_Ce_0002e_0027decomp );
					}
					string POR_Ce_0002new = TempUtil.GetTempFileName();
					using ( var fps4 = new FPS4( POR_Ce_0002 ) ) {
						fps4.Alignment = 0x80;
						fps4.Pack( POR_Ce_0002extract, POR_Ce_0002new );
					}
					File.Delete( POR_Ce_0002 );
					File.Move( POR_Ce_0002new, POR_Ce_0002 );
					Util.DeleteDirectoryAggressive( POR_Ce_0002extract, true );
				}
				string POR_Cnew = TempUtil.GetTempFileName();
				using ( var fps4 = new FPS4( POR_Cdecomp ) ) {
					fps4.Alignment = 0x80;
					fps4.Pack( POR_Cextract, POR_Cnew );
				}
				tlzcCompress( POR_Cnew, POR_C );
				File.Delete( POR_Cdecomp );
				File.Delete( POR_Cnew );
				Util.DeleteDirectoryAggressive( POR_Cextract, true );
			}

			// extract yuri towel costume into its own archive
			{
				string EP_1320_060 = Path.Combine( extractPath, "EP_1320_060.DAT" );
				string EP_1320_060decomp = tlzcDecompressToTempFile( EP_1320_060 );
				string EP_1320_060extract = svoExtractToTempDir( EP_1320_060decomp );
				{
					string EP_1320_060e_0002 = Path.Combine( EP_1320_060extract, "0002" );
					string EP_1320_060e_0002extract = svoExtractToTempDir( EP_1320_060e_0002 );
					{
						foreach ( string file in Util.DirectoryGetFilesWorkaround( EP_1320_060e_0002extract ) ) {
							if ( !file.EndsWith( "4" ) ) { File.Delete( file ); }
						}

						string EP_1320_060e_0002new = Path.Combine( EP_1320_060extract, "0002.new" );
						using ( var fps4 = new FPS4() ) {
							fps4.Alignment = 0x80;
							fps4.ContentBitmask = 0x0047;
							fps4.Pack( Util.DirectoryGetFilesWorkaround( EP_1320_060e_0002extract ), EP_1320_060e_0002new, "n" );
						}
						File.Delete( EP_1320_060e_0002 );
						File.Move( EP_1320_060e_0002new, EP_1320_060e_0002 );
						Util.DeleteDirectoryAggressive( EP_1320_060e_0002extract, true );
					}

					File.Delete( Path.Combine( EP_1320_060extract, "0001" ) );
					File.Copy( Path.Combine( EP_1320_060extract, "0003" ), Path.Combine( EP_1320_060extract, "0001" ) );

					string EP_1320_060e_0000 = Path.Combine( EP_1320_060extract, "0000" );
					string EP_1320_060e_0000extract = Path.Combine( EP_1320_060extract, "0000.ext" );
					if ( Directory.Exists( EP_1320_060e_0000extract ) ) { Util.DeleteDirectoryAggressive( EP_1320_060e_0000extract, true ); }
					Directory.CreateDirectory( EP_1320_060e_0000extract );
					using ( var fps4 = new FPS4( EP_1320_060e_0000 ) ) {
						fps4.Extract( EP_1320_060e_0000extract );
					}
					{
						foreach ( string dir in Directory.GetDirectories( EP_1320_060e_0000extract ) ) {
							if ( !Path.GetFileName( dir ).StartsWith( "P" ) ) { Util.DeleteDirectoryAggressive( dir, true ); }
						}
						foreach ( string dir in Directory.GetDirectories( Directory.GetDirectories( EP_1320_060e_0000extract ).First() ) ) {
							string dirname = Path.GetFileName( dir );
							if ( !( dirname.StartsWith( "Y" ) || dirname.EndsWith( "2" ) ) ) { Util.DeleteDirectoryAggressive( dir, true ); }
						}
						foreach ( string dir in Directory.GetDirectories( Directory.GetDirectories( Directory.GetDirectories( EP_1320_060e_0000extract ).First() ).First() ) ) {
							string dirname = Path.GetFileName( dir );
							if ( !dir.EndsWith( "M" ) ) { Util.DeleteDirectoryAggressive( dir, true ); }
						}

						string EP_1320_060e_0000new = Path.Combine( EP_1320_060extract, "0000.new" );
						using ( var fps4 = new FPS4() ) {
							fps4.Alignment = 0x80;
							fps4.ContentBitmask = 0x0047;
							var files = Util.DirectoryGetFilesWorkaround( EP_1320_060e_0000extract, "*", System.IO.SearchOption.AllDirectories ).OrderBy( x => x.Split( '.' ).Last() ).ToArray();
							fps4.Pack( files, EP_1320_060e_0000new, "p" );
						}
						File.Delete( EP_1320_060e_0000 );
						File.Move( EP_1320_060e_0000new, EP_1320_060e_0000 );
						Util.DeleteDirectoryAggressive( EP_1320_060e_0000extract, true );
					}
				}
				string YUR_C201new = Path.Combine( extractPath, "YUR_C201.DAT.dec.new" );
				using ( var fps4 = new FPS4() ) {
					fps4.Alignment = 0x80;
					fps4.ContentBitmask = 0x0007;
					fps4.ArchiveName = "YUR_C201";
					fps4.Pack( Util.DirectoryGetFilesWorkaround( EP_1320_060extract ), YUR_C201new );
				}
				Util.DeleteDirectoryAggressive( EP_1320_060extract, true );

				tlzcCompress( YUR_C201new, Path.Combine( extractPath, "YUR_C201.DAT" ) );

				File.Delete( EP_1320_060decomp );
				File.Delete( YUR_C201new );
			}

			if ( worker != null ) { worker.ReportProgress( 100, "Packing modified file..." ); }
			Logger.LogDirData( extractPath, "chara dir patched" );
			using ( var fps4 = new FPS4() ) {
				using ( var oldfps4 = new FPS4( charaPath ) ) {
					fps4.Unknown2 = oldfps4.Unknown2;
					fps4.ArchiveName = oldfps4.ArchiveName;
				}
				fps4.Alignment = 0x800;
				fps4.Pack( extractPath, outPath );
			}
			Logger.LogFileData( outPath, "chara.svo patched" );
			Util.DeleteDirectoryAggressive( extractPath, true );

			if ( outMd5 != null ) { CompareMd5Output( outPath, outMd5 ); }
		}
		public static void PatchParam( string paramPath, string dummy, string outDir, string outMd5 = null, BackgroundWorker worker = null ) {
			PatchParam( paramPath, outDir, outMd5 );
		}
		public static void PatchParam( string paramPath, string outDir, string outMd5 = null ) {
			if ( !File.Exists( paramPath ) ) {
				throw new PatchingException( "File not found: " + paramPath );
			}
			// if patched file exists and matches, exit early
			string outPath = Path.Combine( outDir, "PARAM.SFO" );
			try { CompareMd5Output( outPath, outMd5 ); return; } catch ( PatchingException ) { } catch ( FileNotFoundException ) { }

			try {
				CompareMd5( paramPath, "d5dd7447f08ae0431e9039f89bed8118" );
			} catch ( PatchingException ) {
				CompareMd5( paramPath, "be7c9c7996b02f7aa6afc3b54b5b7ab4" );
			}

			byte[] p = System.IO.File.ReadAllBytes( paramPath );

			// clear attributes
			p[0x158] = 0;

			// clear name field
			for ( int i = 0x378; i < 0x3A0; ++i ) {
				p[i] = 0;
			}

			// copy over english name
			byte[] engName = Encoding.UTF8.GetBytes( "Tales of Vesperia" );
			Util.CopyByteArrayPart( engName, 0, p, 0x378, engName.Length );

			// set english name length
			p[0xA8] = (byte)( engName.Length + 1 );

			System.IO.File.WriteAllBytes( outPath, p );

			if ( outMd5 != null ) { CompareMd5Output( outPath, outMd5 ); }
		}
		public static void PatchTrophy( string trophyTrp, string patchDir, string outDir, string outMd5 = null, BackgroundWorker worker = null ) {
			PatchGeneric( trophyTrp, patchDir, outDir, "TROPHY.TRP", "TROPHY.TRP.xdelta3", "ccb079c16b72d61b585d72f18d3f3283", outMd5 );
		}

		public static void PatchAllDefault() {
			var outDir = System.IO.Directory.CreateDirectory( "new/patched" );

			ElfPatcher.PatchElf( "EBOOT.BIN", "new/patches", outDir.FullName );
			PatchString( "string.svo", "new/patches", outDir.FullName );
			PatchScenario( "scenario.dat", "new/patches/scenario", outDir.FullName );
			PatchBtl( "btl.svo", "new/patches/btl", outDir.FullName );
			PatchChat( "chat.svo", "new/patches/chat", outDir.FullName );
			PatchUI( "UI.svo", "new/patches/UI", outDir.FullName );
			PatchEffect( "effect.svo", "new/patches/effect", outDir.FullName );
			PatchChara( "chara.svo", "new/patches/chara", outDir.FullName );
			PatchParam( "PARAM.SFO", outDir.FullName );
			PatchTrophy( "TROPHY.TRP", "new/patches", outDir.FullName );
		}
	}
}
