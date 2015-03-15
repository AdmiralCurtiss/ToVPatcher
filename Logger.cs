using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HyoutaTools;

namespace ToVPatcher {
	public class Logger {
		public static void LogError( string error ) {
			LogAny( "error.log", error );
		}
		public static void LogAny( string path, string text ) {
			using ( var file = new System.IO.FileStream( path, System.IO.FileMode.Append ) ) {
				DateTime now = System.DateTime.UtcNow;
				string dateTimeString = now.ToShortDateString() + " " + now.ToShortTimeString();
				file.Write( Encoding.UTF8.GetBytes( dateTimeString ) );
				file.WriteByte( (byte)'\n' );
				file.Write( Encoding.UTF8.GetBytes( text ) );
				file.WriteByte( (byte)'\n' );
				file.WriteByte( (byte)'\n' );
				file.Close();
			}
		}
	}
}
