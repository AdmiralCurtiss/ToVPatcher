using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using HyoutaTools;

namespace ToVPatcher {
	static class Program {
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main( string[] args ) {
		        if (Util.isRunningOnWindows()) {
			        Util.exeSuffix = ".exe";
		        }
			if ( args.Contains( "--default" ) ) {
				Patcher.PatchAllDefault();
			} else {
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault( false );
				Application.Run( new PatchForm() );
			}
		}
	}
}
