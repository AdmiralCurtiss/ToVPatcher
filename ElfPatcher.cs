using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using HyoutaTools;
using System.ComponentModel;

namespace ToVPatcher {
	class ElfPatcher {
		public static void PatchElf( string ebootPath, string patchDir, string outDir, string outMd5 = null, BackgroundWorker worker = null ) {
			
			string patcherPath = "eboot_tools/unself";
			if (Util.isRunningOnWindows()) {
				patcherPath = Path.GetFullPath ("ebootmod/ebootMOD.exe");
			}
			ebootPath = Path.GetFullPath( ebootPath );
			patchDir = Path.GetFullPath( patchDir );
			outDir = Path.GetFullPath( outDir );

			if ( !File.Exists( ebootPath ) ) {
				throw new PatchingException( "File not found: " + ebootPath );
			}
			if ( worker != null ) { worker.ReportProgress( 0, "Confirming source file..." ); }
			Patcher.CompareMd5( ebootPath, "3171173bba33c43be95e840733ca40a8" );

			// decrypt
			if ( worker != null ) { worker.ReportProgress( 0, "Decrypting..." ); }
			string elfPath = Path.Combine( Path.GetDirectoryName( patcherPath ), Path.GetFileName( ebootPath ) + "-mod.ELF" );
			int tries = 5;
			while ( !( File.Exists( elfPath ) && Patcher.CalcMd5( elfPath ) == "a424aa775b707539dbff08cdb2e61ff5" ) ) {
				if ( --tries < 0 ) {
					throw new PatchingException( "Could not decrypt EBOOT. Confirm that EBOOT is correctly ripped and ebootMOD is working correctly." );
				}

				try {
					if (!Util.isRunningOnWindows()) {
						Util.RunProgram(patcherPath, "\"" + ebootPath + "\" \"" + elfPath + "\"", false, false);
					}
					else {
						RunEbootModAndKill( patcherPath, "\"" + ebootPath + "\"" );
					}
				} catch ( Exception e ) {
					throw new PatchingException( "Failed during execution of ebootMOD or unself. Make sure you copied _everything_ from the ebootmod archive into the ebootmod folder and try again.", e );
				}
				// sleep a bit to reduce chance of ebootMod still having the file handle
				System.Threading.Thread.Sleep( 250 );
			}

			// patch the elf
			if ( worker != null ) { worker.ReportProgress( 0, "Patching..." ); }
			string patchedElf = TempUtil.GetTempFileName();
			Patcher.XdeltaApply( elfPath, patchedElf, Path.Combine( patchDir, "ToV.elf.xdelta3" ) );

			if ( outMd5 != null ) { Patcher.CompareMd5Output( patchedElf, outMd5 ); }

			// encrypt 
			if ( worker != null ) { worker.ReportProgress( 0, "Encrypting..." ); }
			string outPath = Path.Combine( outDir, "EBOOT.BIN" );
			if (!Util.isRunningOnWindows()) {
				RunElfPatcher (Path.GetFullPath ("PS3Py/fself.py"), ebootPath, patchedElf, outPath);
			} else {
				RunElfPatcher (patcherPath, ebootPath, patchedElf, outPath);
			}
			File.Delete( patchedElf );
		}

		private static void RunElfPatcher( string ebootMod, string originalEboot, string modifiedElf, string modifiedEboot ) {

			try {
				if (!Util.isRunningOnWindows()) {
					if (!Util.RunProgram( ebootMod, modifiedElf + " " + modifiedEboot, false, false, true )) {
						throw new PatchingException("fself.py failed");
					}
				}
				else {
					if ( !Util.RunProgram( ebootMod, "\"" + originalEboot + "\" \"" + modifiedEboot + "\" \"" + modifiedElf + "\"", false, false, true ) ) {
						throw new PatchingException( "ebootMOD failed: " + originalEboot + " + " + modifiedElf + " -> " + modifiedEboot );
					}
				}
			} catch ( Win32Exception e ) {
				throw new PatchingException( "Failed during execution of ebootMOD. Make sure ebootMOD can be found at " + ebootMod + " and try again." );
			}
		}
		private static void RunEbootModAndKill( string prog, string args ) {
			System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
			startInfo.CreateNoWindow = true;
			startInfo.UseShellExecute = false;
			startInfo.FileName = prog;
			startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
			startInfo.Arguments = args;
			startInfo.RedirectStandardOutput = true;
			startInfo.RedirectStandardError = true;

			using ( System.Diagnostics.Process exeProcess = System.Diagnostics.Process.Start( startInfo ) ) {
				while ( !exeProcess.HasExited ) {
					string line = exeProcess.StandardOutput.ReadLine();
					if ( line.Contains( "unself" ) ) {
						exeProcess.Kill();
						return;
					}
				}
			}
		}
	}
}
