using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HyoutaTools;

namespace ToVPatcher {
	public class Logger {
		public static bool LoggingEnabled = false;

		public static void LogError( string error ) {
			LogAny( "error.log", error );
		}
		public static void LogPatching( string text ) {
			if ( !LoggingEnabled ) { return; }
			LogAny( "patching.log", text );
		}
		private static void LogAny( string path, string text ) {
			using ( var file = new System.IO.FileStream( path, System.IO.FileMode.Append ) ) {
				DateTime now = System.DateTime.UtcNow;
				string dateTimeString = now.ToShortDateString() + " " + now.ToShortTimeString();
				file.Write( Encoding.UTF8.GetBytes( dateTimeString ) );
				file.WriteByte( (byte)'\t' );
				file.Write( Encoding.UTF8.GetBytes( text ) );
				file.WriteByte( (byte)'\n' );
				file.Close();
			}
		}

		public static void LogDirData( string path, string comment = null ) {
			if ( !LoggingEnabled ) { return; }

			if ( comment != null ) {
				LogAny( "patching.log", comment );
			}
			foreach ( var file in System.IO.Directory.GetFiles( path ) ) {
				LogFileData( file );
			}
			foreach ( var dir in System.IO.Directory.GetDirectories( path ) ) {
				LogDirData( dir );
			}
		}
		public static void LogFileData( string path, string comment = null ) {
			if ( !LoggingEnabled ) { return; }

			var sb = new StringBuilder();
			try {
				sb.Append( Patcher.CalcMd5( path ) );
			} catch ( Exception ex ) {
				sb.Append( ex.ToString() );
			}
			sb.Append( '\t' );
			sb.Append( path );

			if ( comment != null ) {
				sb.Append( '\t' );
				sb.Append( comment );
			}
			LogAny( "patching.log", sb.ToString() );
		}
	}
}
