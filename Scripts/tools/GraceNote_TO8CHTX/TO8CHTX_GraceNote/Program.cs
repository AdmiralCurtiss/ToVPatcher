using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TO8CHTX_GraceNote {
	class Program {
		static void Main( string[] args ) {
			String Filename;
			String Database;
			String NewFilename;
			bool updateGracesEnglish = false;

			if ( args.Length < 3 ) {
				Console.WriteLine( "Usage: GraceNote_TO8CHTX ChatFilename DBFilename NewChatFilename" );
				return;
			}

			Filename = args[0];
			Database = args[1];
			NewFilename = args[2];
			updateGracesEnglish = args.Contains( "--updateGracesEnglish" );

			ChatFile c = new ChatFile( System.IO.File.ReadAllBytes( Filename ) );

			c.GetSQL( "Data Source=" + Database, updateGracesEnglish );

			c.RecalculatePointers();
			System.IO.File.WriteAllBytes( NewFilename, c.Serialize() );

			return;
		}
	}
}
