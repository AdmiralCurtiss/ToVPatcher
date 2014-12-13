using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using HyoutaTools;

namespace ToVPatcher {
	class ElfPatcher {
		public static void PatchElf( string ebootPath, string patchDir, string outDir ) {
			string ebootModPath = Path.GetFullPath( "ebootmod/ebootMOD.exe" );
			ebootPath = Path.GetFullPath( ebootPath );
			patchDir = Path.GetFullPath( patchDir );
			outDir = Path.GetFullPath( outDir );

			// decrypt
			string elfPath = Path.Combine( Path.GetDirectoryName( ebootModPath ), Path.GetFileName( ebootPath ) + "-mod.ELF" );
			while ( !File.Exists( elfPath ) ) {
				// this is super ugly but the only sensible way since calling unself directly searches the keys who-knows-where
				RunEbootModAndKill( ebootModPath, "\"" + ebootPath + "\"" );
			}

			// patch the elf
			string patchedElf = Path.GetTempFileName();
			Patcher.XdeltaApply( elfPath, patchedElf, Path.Combine( patchDir, "ToV.elf.xdelta3" ) );

			// encrypt 
			string outPath = Path.Combine( outDir, "EBOOT.BIN" );
			RunEbootMod( ebootModPath, ebootPath, patchedElf, outPath );

			File.Delete( patchedElf );
		}

		private static void RunEbootMod( string ebootMod, string originalEboot, string modifiedElf, string modifiedEboot ) {
			Util.RunProgram( ebootMod, "\"" + originalEboot + "\" \"" + modifiedEboot + "\" \"" + modifiedElf + "\"", false, false, true );
		}
		private static void RunEbootModAndKill( string prog, string args ) {
			System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
			startInfo.CreateNoWindow = false;
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
