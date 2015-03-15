using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using HyoutaTools;

namespace ToVPatcher {
	static class TempUtil {
		private static int TempFileCounter = 0;

		public static string GetTempFileName() {
			int number = System.Threading.Interlocked.Increment( ref TempFileCounter );
			return Path.Combine( GetTempFolder(), number.ToString( "X8" ) );
		}

		public static string GetTempFolder() {
			string path = "_TEMP_";
			if ( !Directory.Exists( path ) ) {
				Directory.CreateDirectory( path );
			}
			return path;
		}

		public static void RemoveTempFolder() {
			if ( Directory.Exists( GetTempFolder() ) ) {
				Util.DeleteDirectoryAggressive( GetTempFolder(), true );
			}
		}
	}
}
