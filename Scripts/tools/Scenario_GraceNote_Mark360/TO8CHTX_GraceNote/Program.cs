using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace TO8CHTX_GraceNote
{
    class ScenarioString {
        public int Pointer;
        public String Jpn;
        public String Eng;

        public ScenarioString(int Pointer, String Jpn, String Eng) {
            this.Pointer = Pointer;
            this.Jpn = Jpn;
            this.Eng = Eng;
        }

        public override string ToString()
        {
            return Eng + " / " + Jpn;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: Scenario_GraceNote_Mark360 File Database (-scenario/-chat/-stringdic)");
                return;
            }

            String Filename = args[0];
            String Database = args[1];
            String Type = args[2];

            byte[] FileBytes = System.IO.File.ReadAllBytes(Filename);
            ScenarioString[] OriginalStrings;
            switch ( Type ) {
                case "-scenario":
                    OriginalStrings = ReadAllStringsFromScenarioText(FileBytes);
                    break;
                case "-chat":
                    ChatFile c = new ChatFile(FileBytes);
                    OriginalStrings = c.ToScenarioStringsFor360Check();
                    break;
                case "-stringdic":
                    TSSFile t = new TSSFile(FileBytes);
                    OriginalStrings = t.ToScenarioStringsFor360Check();
                    break;
                default:
                    return;
            }

            List<KeyValuePair<ScenarioString, bool>> X360Info = new List<KeyValuePair<ScenarioString, bool>>(OriginalStrings.Length);
            foreach (ScenarioString s in OriginalStrings)
            {
                if (String.IsNullOrEmpty(s.Jpn)) continue;

                s.Eng = RemoveVoiceTags(s.Eng);
                s.Jpn = RemoveVoiceTags(s.Jpn);
                if (s.Eng.ToUpperInvariant() == "DUMMY") s.Eng = "";

                if (String.IsNullOrEmpty(s.Eng))
                {
                    // no english text, PS3 exclusive
                    X360Info.Add( new KeyValuePair<ScenarioString,bool>(s, false) );
                }
                else
                {
                    // english text, can assume 360 version
                    X360Info.Add( new KeyValuePair<ScenarioString,bool>(s, true) );
                }
            }

            UpdateTextTableWithX360Info("Data Source=" + Database, X360Info);

            return;
        }

        private static String RemoveVoiceTags(String s)
        {
            s = System.Text.RegularExpressions.Regex.Replace(s, "\t[(][A-Za-z0-9_]+[)]", ""); // voice command
            return s;
        }


        private static ScenarioString[] ReadAllStringsFromScenarioText(byte[] ScenarioText)
        {
            List<ScenarioString> list = new List<ScenarioString>();

            byte[] PointerIdent = { 0x50, 0x4F, 0x49, 0x4E, 0x54, 0x45, 0x52, 0x20, 0x40, 0x20, 0x30, 0x78 };

            for (int i = 0; i < ScenarioText.Length; ++i)
            {
                bool ok = true;
                for ( int j = 0; j < PointerIdent.Length; ++j ) {
                    if (ScenarioText[i + j] != PointerIdent[j])
                    {
                        ok = false;
                        break;
                    }
                }

                if (ok)
                {
                    // block found
                    i += PointerIdent.Length;
                    String PointerStr = Util.GetText(i, ScenarioText);
                    
                    while (ScenarioText[i] != 0x00) ++i;
                    i += 3; // skip past 0x000d0a

                    String[] PointersStr = new String[4];
                    String[] Texts = new String[4];
                    for (int cnt = 0; cnt < 4; ++cnt)
                    {
                        i += 11; // skip past "JPN NAME 0x"
                        PointersStr[cnt] = Util.GetText(i, ScenarioText, 0x20); // grab PointerRef
                        while (ScenarioText[i] != 0x3A) ++i; // skip to start of text
                        ++i;
                        Texts[cnt] = Util.GetText(i, ScenarioText);
                        while (ScenarioText[i] != 0x00) ++i; // skip to end of text
                        i += 3; // skip past 0x000d0a
                    }

                    list.Add(new ScenarioString(Int32.Parse(PointersStr[0], System.Globalization.NumberStyles.AllowHexSpecifier), Texts[0], Texts[2]));
                    list.Add(new ScenarioString(Int32.Parse(PointersStr[1], System.Globalization.NumberStyles.AllowHexSpecifier), Texts[1], Texts[3]));
                }
            }

            return list.ToArray();
        }

        public static String GetJapanese(String ConnectionString, int ID)
        {
            String SQLText = "";

            SQLiteConnection Connection = new SQLiteConnection(ConnectionString);
            Connection.Open();

            using (SQLiteTransaction Transaction = Connection.BeginTransaction())
            using (SQLiteCommand Command = new SQLiteCommand(Connection))
            {
                Command.CommandText = "SELECT string FROM Japanese WHERE ID = " + ID.ToString();
                SQLiteDataReader r = Command.ExecuteReader();
                while (r.Read())
                {
                    try
                    {
                        SQLText = r.GetString(0).Replace("''", "'");
                    }
                    catch (System.InvalidCastException ex)
                    {
                        SQLText = "";
                    }
                }

                Transaction.Rollback();
            }

            return SQLText;
        }

        public static ScenarioFile[] GetSQL(String ConnectionString, int FileNumber, String GracesJapaneseConnectionString)
        {
            List<ScenarioFile> ScenarioFiles = new List<ScenarioFile>();

            SQLiteConnection Connection = new SQLiteConnection(ConnectionString);
            Connection.Open();

            using (SQLiteTransaction Transaction = Connection.BeginTransaction())
            using (SQLiteCommand Command = new SQLiteCommand(Connection))
            {
                if (FileNumber == -1)
                {
                    Command.CommandText = "SELECT english, PointerRef, StringID FROM Text WHERE status != -1 ORDER BY PointerRef";
                }
                else
                {
                    Command.CommandText = "SELECT english, PointerRef, StringID FROM Text WHERE status != -1 AND OriginalFile = " + FileNumber.ToString() + " ORDER BY PointerRef";
                }
                SQLiteDataReader r = Command.ExecuteReader();
                while (r.Read())
                {
                    String SQLText;

                    try
                    {
                        SQLText = r.GetString(0).Replace("''", "'");
                    }
                    catch (System.InvalidCastException ex)
                    {
                        SQLText = null;
                    }

                    int PointerRef = r.GetInt32(1);
                    int StringID = r.GetInt32(2);

                    ScenarioFile sc = new ScenarioFile();

                    if (!String.IsNullOrEmpty(SQLText))
                    {
                        sc.Text = SQLText;
                    }
                    else
                    {
                        sc.Text = GetJapanese(GracesJapaneseConnectionString, StringID);
                    }

                    sc.Pointer = PointerRef;

                    ScenarioFiles.Add(sc);
                }

                Transaction.Rollback();
            }
            return ScenarioFiles.ToArray();
        }



        private static ScenarioFile[] Block(ScenarioFile[] ScenarioStrings)
        {
            List<ScenarioFile> ScenarioBlocks = new List<ScenarioFile>();

            for (int i = 0; i < ScenarioStrings.Length; i++)
            {
                try
                {
                    if (ScenarioStrings[i].Pointer + 4 == ScenarioStrings[i + 1].Pointer)
                    {
                        ScenarioFile s = new ScenarioFile();
                        s.Pointer = ScenarioStrings[i].Pointer - 8;
                        s.Name = ScenarioStrings[i].Text;
                        s.Text = ScenarioStrings[i + 1].Text;
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
                }
                catch (IndexOutOfRangeException)
                {
                    ScenarioFile s = new ScenarioFile();
                    s.Pointer = ScenarioStrings[i].Pointer - 12;
                    s.Name = "";
                    s.Text = ScenarioStrings[i].Text;
                    ScenarioBlocks.Add(s);
                }
            }

            return ScenarioBlocks.ToArray();
        }



        public static bool UpdateTextTableWithX360Info(String ConnectionString, List<KeyValuePair<ScenarioString, bool>> X360Info)
        {
            SQLiteConnection Connection = new SQLiteConnection(ConnectionString);
            Connection.Open();

            using (SQLiteTransaction Transaction = Connection.BeginTransaction())
            using (SQLiteCommand Command = new SQLiteCommand(Connection))
            {
                SQLiteParameter IdentifyStringParam = new SQLiteParameter();
                SQLiteParameter PointerRefParam = new SQLiteParameter();

                using (SQLiteCommand CreateColumnCommand = new SQLiteCommand(Connection))
                {
                    try
                    {
                        CreateColumnCommand.CommandText = "ALTER TABLE Text ADD IdentifyString text";
                        CreateColumnCommand.ExecuteNonQuery();
                    }
                    catch (SQLiteException) { }
                }


                Command.CommandText =
                    "UPDATE Text SET IdentifyString = ? WHERE PointerRef = ? AND status > -1";
                Command.Parameters.Add(IdentifyStringParam);
                Command.Parameters.Add(PointerRefParam);

                foreach (KeyValuePair<ScenarioString, bool> x in X360Info)
                {
                    if (x.Value)
                    {
                        IdentifyStringParam.Value = "(x360)";
                    }
                    else
                    {
                        IdentifyStringParam.Value = "(ps3)";
                    }
                    PointerRefParam.Value = x.Key.Pointer;

                    Command.ExecuteNonQuery();
                }
                Transaction.Commit();
            }
            Connection.Close();

            return true;
        }



        public static bool InsertSQL(ScenarioString[] NewStrings, int FileNumber, String ConnectionString, String ConnectionStringGracesJapanese)
        {
            SQLiteConnection Connection = new SQLiteConnection(ConnectionString);
            SQLiteConnection ConnectionGracesJapanese = new SQLiteConnection(ConnectionStringGracesJapanese);
            Connection.Open();
            ConnectionGracesJapanese.Open();

            using (SQLiteTransaction Transaction = Connection.BeginTransaction())
            using (SQLiteTransaction TransactionGracesJapanese = ConnectionGracesJapanese.BeginTransaction())
            using (SQLiteCommand Command = new SQLiteCommand(Connection))
            using (SQLiteCommand CommandGracesJapanese = new SQLiteCommand(ConnectionGracesJapanese))
            using (SQLiteCommand CommandJapaneseID = new SQLiteCommand(ConnectionGracesJapanese))
            using (SQLiteCommand CommandSearchJapanese = new SQLiteCommand(ConnectionGracesJapanese))
            {
                SQLiteParameter JapaneseIDParam = new SQLiteParameter();
                SQLiteParameter JapaneseParam = new SQLiteParameter();
                SQLiteParameter EnglishIDParam = new SQLiteParameter();
                SQLiteParameter StringIDParam = new SQLiteParameter();
                SQLiteParameter EnglishParam = new SQLiteParameter();
                SQLiteParameter LocationParam = new SQLiteParameter();
                SQLiteParameter JapaneseSearchParam = new SQLiteParameter();
                SQLiteParameter EnglishStatusParam = new SQLiteParameter();

                CommandGracesJapanese.CommandText = "INSERT INTO Japanese (ID, string, debug) VALUES (?, ?, 0)";
                CommandGracesJapanese.Parameters.Add(JapaneseIDParam);
                CommandGracesJapanese.Parameters.Add(JapaneseParam);

                Command.CommandText = "INSERT INTO Text (ID, StringID, english, comment, updated, status, PointerRef, OriginalFile) VALUES (?, ?, ?, null, 0, ?, ?, " + FileNumber.ToString() +")";
                Command.Parameters.Add(EnglishIDParam);
                Command.Parameters.Add(StringIDParam);
                Command.Parameters.Add(EnglishParam);  // Line.SENG
                Command.Parameters.Add(EnglishStatusParam);
                Command.Parameters.Add(LocationParam); // Line.Location

                CommandJapaneseID.CommandText = "SELECT MAX(ID)+1 FROM Japanese";

                CommandSearchJapanese.CommandText = "SELECT ID FROM Japanese WHERE string = ?";
                CommandSearchJapanese.Parameters.Add(JapaneseSearchParam);

                int JPID;
                object JPMaxIDObject = CommandJapaneseID.ExecuteScalar();
                int JPMaxID = Int32.Parse(JPMaxIDObject.ToString()); // wtf why doesn't this work directly?
                int ENID = FileNumber * 1000 + 1;

                foreach (ScenarioString str in NewStrings)
                {
                    // Name
                    JapaneseSearchParam.Value = str.Jpn;
                    object JPIDobj = CommandSearchJapanese.ExecuteScalar();
                    if (JPIDobj != null)
                    {
                        JPID = (int)JPIDobj;
                    }
                    else
                    {
                        JPID = JPMaxID++;
                        JapaneseIDParam.Value = JPID;
                        JapaneseParam.Value = str.Jpn;
                        CommandGracesJapanese.ExecuteNonQuery();
                    }

                    EnglishIDParam.Value = ENID;
                    StringIDParam.Value = JPID;
                    EnglishParam.Value = str.Eng;
                    if (str.Eng == str.Jpn)
                    {
                        EnglishStatusParam.Value = 1;
                    }
                    else
                    {
                        EnglishStatusParam.Value = 0;
                    }
                    LocationParam.Value = str.Pointer;
                    Command.ExecuteNonQuery();

                    ENID++;
                }
                Transaction.Commit();
                TransactionGracesJapanese.Commit();
            }
            ConnectionGracesJapanese.Close();
            Connection.Close();

            return true;
        }







        private static ScenarioString[] RemoveDupesAndEmpty(ScenarioString[] AllStrings)
        {
            List<int> Pointers = new List<int>(AllStrings.Length);
            List<ScenarioString> DupelessStrings = new List<ScenarioString>(AllStrings.Length);

            foreach (ScenarioString str in AllStrings)
            {
                if (String.IsNullOrEmpty(str.Jpn) && String.IsNullOrEmpty(str.Eng)) continue;

                if (!Pointers.Contains(str.Pointer))
                {
                    Pointers.Add(str.Pointer);
                    DupelessStrings.Add(str);
                }
            }

            return DupelessStrings.ToArray();
        }

        private static ScenarioString[] FindAllStrings(int[] PointerList, byte[] Scenario, int TextStart)
        {
            List<ScenarioString> AllStrings = new List<ScenarioString>();

            foreach (int InternalPointer in PointerList)
            {
                int BytePointer = BitConverter.ToInt32(new byte[] {
                    Scenario[InternalPointer+3], Scenario[InternalPointer+2], Scenario[InternalPointer+1], Scenario[InternalPointer]
                }, 0);
                int RealPointer = BytePointer + TextStart + 8;
                int Pointer = RealPointer;

                int Pointer1 = BitConverter.ToInt32(new byte[] {
                    Scenario[Pointer+3], Scenario[Pointer+2], Scenario[Pointer+1], Scenario[Pointer]
                }, 0);
                Pointer += 4;
                int Pointer2 = BitConverter.ToInt32(new byte[] {
                    Scenario[Pointer+3], Scenario[Pointer+2], Scenario[Pointer+1], Scenario[Pointer]
                }, 0);
                Pointer += 4;
                int Pointer3 = BitConverter.ToInt32(new byte[] {
                    Scenario[Pointer+3], Scenario[Pointer+2], Scenario[Pointer+1], Scenario[Pointer]
                }, 0);
                Pointer += 4;
                int Pointer4 = BitConverter.ToInt32(new byte[] {
                    Scenario[Pointer+3], Scenario[Pointer+2], Scenario[Pointer+1], Scenario[Pointer]
                }, 0);

                ScenarioString Name = new ScenarioString
                    (RealPointer - TextStart, Util.GetText(Pointer1 + TextStart, Scenario), Util.GetText(Pointer3 + TextStart, Scenario));
                ScenarioString Text = new ScenarioString
                    ((RealPointer - TextStart) + 4, Util.GetText(Pointer2 + TextStart, Scenario), Util.GetText(Pointer4 + TextStart, Scenario));

                AllStrings.Add(Name);
                AllStrings.Add(Text);
            }

            return AllStrings.ToArray();
        }

        private static ScenarioString[] FindNewStrings(ScenarioString[] AllStrings, String ConnectionString)
        {
            List<ScenarioString> NewStrings = new List<ScenarioString>();





            SQLiteConnection Connection = new SQLiteConnection(ConnectionString);
            Connection.Open();

            SQLiteTransaction Transaction = Connection.BeginTransaction();

                foreach ( ScenarioString str in AllStrings ) {
                    SQLiteCommand Command = new SQLiteCommand(Connection);
                    Command.CommandText = "SELECT english, PointerRef FROM Text WHERE status != -1 AND PointerRef = ?";
                    SQLiteParameter param = new SQLiteParameter();
                    Command.Parameters.Add(param);
                    param.Value = str.Pointer;
                    SQLiteDataReader r = Command.ExecuteReader();
                    if (r.Read())
                    {
                        // ok cool this string exists
                    }
                    else
                    {
                        // haha! a new string!
                        NewStrings.Add(str);
                    }
                }



            return NewStrings.ToArray();
        }


        private static int[] GetLocation(byte[] File, int Max)
        {
            //byte[] PointerBytes = System.BitConverter.GetBytes(Util.SwapEndian((uint)Pointer));
            byte[] SearchBytes = new byte[] { 0x04, 0x0C, 0x00, 0x18 };

            List<int> PointerArrayList = new List<int>();

            for (int i = 0; i < Max; i++)
            {
                if (File[i] == SearchBytes[0]
                    && File[i + 1] == SearchBytes[1]
                    && File[i + 2] == SearchBytes[2]
                    && File[i + 3] == SearchBytes[3])
                {
                    PointerArrayList.Add(i + 4);
                }
            }

            return PointerArrayList.ToArray();
        }
    }
}
