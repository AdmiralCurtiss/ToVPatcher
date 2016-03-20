using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace TO8CHTX_GraceNote {
	class Program {
		public static String GetText( byte[] File, uint UPointer ) {
			try {
				int Pointer = (int)UPointer;
				int i = Pointer;

				while ( File[i] != 0x00 ) {
					i++;
				}
				String Text = Util.ShiftJISEncoding.GetString( File, Pointer, i - Pointer );

				return Text;
			} catch ( Exception ) {
				return null;
			}
		}


		public static String GetJapanese( String ConnectionString, int ID ) {
			String SQLText = "";

			SQLiteConnection Connection = new SQLiteConnection( ConnectionString );
			Connection.Open();

			using ( SQLiteTransaction Transaction = Connection.BeginTransaction() )
			using ( SQLiteCommand Command = new SQLiteCommand( Connection ) ) {
				Command.CommandText = "SELECT string FROM Japanese WHERE ID = " + ID.ToString();
				SQLiteDataReader r = Command.ExecuteReader();
				while ( r.Read() ) {
					try {
						SQLText = r.GetString( 0 ).Replace( "''", "'" );
					} catch ( System.InvalidCastException ) {
						SQLText = "";
					}
				}

				Transaction.Rollback();
			}

			return SQLText;
		}

		public static ScenarioFile[] GetSQL( String ConnectionString, String GracesJapaneseConnectionString ) {
			List<ScenarioFile> ScenarioFiles = new List<ScenarioFile>();

			SQLiteConnection Connection = new SQLiteConnection( ConnectionString );
			Connection.Open();

			using ( SQLiteTransaction Transaction = Connection.BeginTransaction() )
			using ( SQLiteCommand Command = new SQLiteCommand( Connection ) ) {
				Command.CommandText = "SELECT english, PointerRef, StringID FROM Text WHERE status != -1 ORDER BY PointerRef";
				SQLiteDataReader r = Command.ExecuteReader();
				while ( r.Read() ) {
					String SQLText;

					try {
						SQLText = r.GetString( 0 ).Replace( "''", "'" );
					} catch ( System.InvalidCastException ) {
						SQLText = null;
					}

					int PointerRef = r.GetInt32( 1 );
					int StringID = r.GetInt32( 2 );

					ScenarioFile sc = new ScenarioFile();

					if ( !String.IsNullOrEmpty( SQLText ) ) {
						sc.Text = SQLText;
					} else {
						sc.Text = GetJapanese( GracesJapaneseConnectionString, StringID );
					}

					sc.Pointer = PointerRef;
					sc.StringIdText = StringID;

					ScenarioFiles.Add( sc );
				}

				Transaction.Rollback();
			}
			return ScenarioFiles.ToArray();
		}

		static void Main( string[] args ) {
			String Database;
			String TxtFilename;
			String GracesJapanese;
			byte[] File;
			bool updateGracesEnglish = false;

			if ( args.Length < 4 ) {
				Console.WriteLine( "Usage: GraceNote_ScenarioDatTxt DBFilename TxtFilename GracesJapanese BtlFile" );
				return;
			}

			Database = args[0];
			TxtFilename = args[1];
			GracesJapanese = args[2];
			File = System.IO.File.ReadAllBytes( args[3] );
			updateGracesEnglish = args.Contains( "--updateGracesEnglish" );


			ScenarioFile[] ScenarioStrings;
			try {
				ScenarioStrings = GetSQL( "Data Source=" + Database, "Data Source=" + GracesJapanese );

				List<ScenarioFile> FullStrings = new List<ScenarioFile>( ScenarioStrings );
				FullStrings.Sort();
				ScenarioStrings = FullStrings.ToArray();
			} catch ( Exception ) {
				return;
			}
			ScenarioFile[] ScenarioBlocks = ScenarioStrings;


			List<byte> bytes = new List<byte>( ScenarioBlocks.Length * 1000 );

			byte[] JpnTextString = Util.StringToBytes( "\0\r\nTEXT @ 0x" );
			byte[] ColonString = Util.StringToBytes( ":" );
			byte[] DelimiterString = Util.StringToBytes( "\0\r\n----------------" );


			foreach ( ScenarioFile B in ScenarioBlocks ) {
				//if (String.IsNullOrEmpty(B.Text) && String.IsNullOrEmpty(B.Name)) continue;

				int point = B.Pointer;
				if ( point == -1 ) continue;
				bytes.AddRange( JpnTextString );
				bytes.AddRange( Util.StringToBytes( point.ToString( "x" ) ) );
				bytes.AddRange( ColonString );
				bytes.AddRange( Util.StringToBytes( B.Text ) );
				bytes.AddRange( DelimiterString );
			}
			bytes.Add( 0x00 );
			bytes.Add( 0x0D );
			bytes.Add( 0x0A );

			System.IO.File.WriteAllBytes( TxtFilename, bytes.ToArray() );

			if ( updateGracesEnglish ) {
				SQLiteConnection connectionGracesEnglish = null;
				SQLiteTransaction transactionGracesEnglish = null;
				connectionGracesEnglish = new SQLiteConnection( "Data Source=db/GracesEnglish" );
				connectionGracesEnglish.Open();
				transactionGracesEnglish = connectionGracesEnglish.BeginTransaction();

				foreach ( ScenarioFile entry in ScenarioBlocks ) {
					ReadOriginalStrings( File, entry );
					UpdateGracesJapanese( transactionGracesEnglish, entry.StringIdText, entry.OriginalTextEng, 0 );
				}

				transactionGracesEnglish.Commit();
				connectionGracesEnglish.Close();
			}

			return;
		}
		private static void UpdateGracesJapanese( SQLiteTransaction ta, int id, string originalString, int debug ) {
			// CREATE TABLE Japanese(ID INT PRIMARY KEY, string TEXT, debug INT)
			if ( id < 0 ) {
				return;
			}
			long exists = (long)SqliteUtil.SelectScalar( ta, "SELECT COUNT(1) FROM Japanese WHERE ID = ?", new object[1] { id } );

			if ( exists > 0 ) {
				SqliteUtil.Update( ta, "UPDATE Japanese SET string = ?, debug = ? WHERE ID = ?", new object[3] { originalString, debug, id } );
			} else {
				SqliteUtil.Update( ta, "INSERT INTO Japanese (ID, string, debug) VALUES (?, ?, ?)", new object[3] { id, originalString, debug } );
			}
		}

		private static void ReadOriginalStrings( byte[] file, ScenarioFile entry ) {
			int ptrOffset = (int)Util.SwapEndian( BitConverter.ToUInt32( file, 0x24 ) ) - 0x400;

			int jpnTextPtr = (int)Util.SwapEndian( BitConverter.ToUInt32( file, entry.Pointer + 0x00 ) );
			int engTextPtr = (int)Util.SwapEndian( BitConverter.ToUInt32( file, entry.Pointer + 0x08 ) );

			entry.OriginalTextJpn = Util.GetText( ptrOffset + jpnTextPtr, file );
			entry.OriginalTextEng = Util.GetText( ptrOffset + engTextPtr, file );
		}

		/*
		private static ScenarioFile[] Block(ScenarioFile[] ScenarioStrings)
		{
			List<ScenarioFile> ScenarioBlocks = new List<ScenarioFile>();

			for (int i = 0; i < ScenarioStrings.Length; i++)
			{
				try {
					if (ScenarioStrings[i].Pointer + 4 == ScenarioStrings[i + 1].Pointer)
					{
						ScenarioFile s = new ScenarioFile();
						s.Pointer = ScenarioStrings[i].Pointer - 8;
						s.Name = ScenarioStrings[i].Text;
						s.Text = ScenarioStrings[i+1].Text;
						ScenarioBlocks.Add(s);
						i++;
					}
					else
					{
						ScenarioFile s = new ScenarioFile();
						s.Pointer = ScenarioStrings[i].Pointer - 12;
						s.Name = "";
						s.Text = ScenarioStrings[i].Text;
						ScenarioBlocks.Add(s);
					}
				} catch (IndexOutOfRangeException) {
					ScenarioFile s = new ScenarioFile();
					s.Pointer = ScenarioStrings[i].Pointer - 12;
					s.Name = "";
					s.Text = ScenarioStrings[i].Text;
					ScenarioBlocks.Add(s);
				}
			}

			return ScenarioBlocks.ToArray();
		}
		 * */

		private static int[] GetLocation( byte[] File, int Max, int Pointer ) {
			byte[] PointerBytes = System.BitConverter.GetBytes( Util.SwapEndian( (uint)Pointer ) );
			byte[] SearchBytes = new byte[] { 0x04, 0x0C, 0x00, 0x18, PointerBytes[0], PointerBytes[1], PointerBytes[2], PointerBytes[3] };

			List<int> PointerArrayList = new List<int>();

			for ( int i = 0; i < Max; i++ ) {
				if ( File[i] == SearchBytes[0]
					&& File[i + 1] == SearchBytes[1]
					&& File[i + 2] == SearchBytes[2]
					&& File[i + 3] == SearchBytes[3]
					&& File[i + 4] == SearchBytes[4]
					&& File[i + 5] == SearchBytes[5]
					&& File[i + 6] == SearchBytes[6]
					&& File[i + 7] == SearchBytes[7] ) {
					PointerArrayList.Add( i + 4 );
				}
			}

			return PointerArrayList.ToArray();
		}
	}
}
