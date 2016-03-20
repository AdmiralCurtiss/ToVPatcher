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
    }

    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 5)
            {
                Console.WriteLine("Usage: Scenario_GraceNote_FindMissing ScenarioFilename NewDBFilename ExistingDBFilename GracesJapanese ScenarioNumber");
                return;
            }

            
            String Filename = args[0];
            String NewDB = args[1];
            String ExistingDB = args[2];
            String GracesDB = args[3];
            int ScenarioNumber = Int32.Parse(args[4]);
            
            byte[] Scenario = System.IO.File.ReadAllBytes(Filename);
            byte[] TextStartBytes = { Scenario[0x0F], Scenario[0x0E], Scenario[0x0D], Scenario[0x0C] };
            int Max = BitConverter.ToInt32(TextStartBytes, 0);
            int[] PointerList = GetLocation(Scenario, Max);


            ScenarioString[] AllStrings = FindAllStrings(PointerList, Scenario, Max);
            ScenarioString[] DuplicatelessStrings = RemoveDupesAndEmpty(AllStrings);
            ScenarioString[] NewStrings = FindNewStrings(DuplicatelessStrings, "Data Source=" + ExistingDB);

            InsertSQL(NewStrings, ScenarioNumber, "Data Source=" + NewDB, "Data Source=" + GracesDB);

            return;
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
