using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace string_svo_reader
{
    class TSSFile
    {
        public TSSHeader Header;
        public TSSEntry[] Entries;
        public byte[] File;

        public TSSFile(byte[] File)
        {
            this.File = File;
            // set header
            Header = new TSSHeader(File.Take(0x20).ToArray());

            if (Header.Magic != 0x54535300)
            {
                MessageBox.Show("File is not a TSS file!");
                return;
            }

            // convert all instructions into a List of uint
            int CurrentLocation = 0x20;
            UInt32 EntriesEnd = Header.TextStart;
            List<uint> EntryUIntList = new List<uint>();

            while (CurrentLocation < EntriesEnd)
            {
                uint Instruction = BitConverter.ToUInt32(File, CurrentLocation);
                EntryUIntList.Add(Util.SwapEndian(Instruction));
                CurrentLocation += 4;
            }

            // split the full instruction list into blocks seperated by 0xFFFFFFFF ( == TSSEntry )
            // and put them into the Entry list
            // complete with text it's pointing at
            CurrentLocation = 0;
            uint[] EntryUIntArray = EntryUIntList.ToArray();
            int ListSize = EntryUIntArray.Length;
            List<TSSEntry> EntryList = new List<TSSEntry>(ListSize/10);
            int i = 0;
            while (CurrentLocation < ListSize)
            {
                uint[] OneEntry = EntryUIntArray.Skip(CurrentLocation).TakeWhile(subject => subject != 0xFFFFFFFF).ToArray();

                int JPNPointer = -1;
                int ENGPointer = -1;
                int JPNIndex = -1;
                int ENGIndex = -1;
                try
                {
                    for (i = 0; i < OneEntry.Length; i++)
                    {
                        if (OneEntry[i] == 0x02820000)
                        {
                            break;
                        }
                    }
                    JPNIndex = ++i;
                    JPNPointer = (int)(OneEntry[JPNIndex] + Header.TextStart);
                }
                catch (Exception)
                {
                    JPNPointer = -1;
                }
                try
                {
                    for (; i < OneEntry.Length; i++)
                    {
                        if (OneEntry[i] == 0x02820000)
                        {
                            break;
                        }
                    }
                    ENGIndex = i + 1;
                    ENGPointer = (int)(OneEntry[ENGIndex] + Header.TextStart);
                }
                catch (Exception)
                {
                    ENGPointer = -1;
                }
                EntryList.Add(new TSSEntry(OneEntry.ToArray(), GetText(JPNPointer), GetText(ENGPointer), JPNIndex, ENGIndex));
                CurrentLocation += OneEntry.Length;
                CurrentLocation++;
            }
            Entries = EntryList.ToArray();
        }




        public String GetText(int Pointer)
        {
            if (Pointer == -1) return null;

            try
            {
                int i = Pointer;
                while (File[i] != 0x00) {
                    i++;
                }
                String Text = Util.ShiftJISEncoding.GetString(File, Pointer, i-Pointer);
                return Text;
            }
            catch (Exception)
            {
                return null;
            }
        }



        public byte[] ExportText()
        {
            List<string> Lines = new List<string>(Entries.Length * 5);

            char[] newline = { '\n' };
            String newlineS = new String(newline);
            char[] backslashn = { '\\', 'n' };
            String backslashnS = new String(backslashn);
            byte[] newlinereturn = { 0x0D, 0x0A };

            char[] backslash = { '\\' };
            char[] backslashescaped = { '\\', '\\' };
            char[] singlequote = { '\'' };
            char[] singlequoteescaped = { '\'', '\'' };
            char[] quote = { '\"' };
            char[] quotescaped = { '\\', '\"' };
            String Sbackslash = new String(backslash);
            String Sbackslashescaped = new String(backslashescaped);
            String Ssinglequote = new String(singlequote);
            String Ssinglequoteescaped = new String(singlequoteescaped);
            String Squote = new String(quote);
            String Squotescaped = new String(quotescaped);

            for (int i = 0; i < Entries.Length; i++)
            {
                if (Entries[i].StringJPN != null && Entries[i].StringENG != null)
                {
                    String English = Entries[i].StringENG.Replace(Ssinglequote, Ssinglequoteescaped);
                    String Japanese = Entries[i].StringJPN.Replace(Ssinglequote, Ssinglequoteescaped);
                    Lines.Add("UPDATE Text SET english = \'" + English + "\', updated = 1, status = 1 ");
                    Lines.Add("WHERE StringID IN ");
                    Lines.Add("( SELECT ID FROM Japanese WHERE ID >= 54998 AND ID <= 62550 AND ");
                    Lines.Add("string = \'" + Japanese + "\' );");
                }
                Lines.Add("");
            }

            List<byte> Serialized = new List<byte>((int)(Header.TextLength));

            foreach (String s in Lines)
            {
                Serialized.AddRange(Encoding.UTF8.GetBytes(s));
                Serialized.AddRange(newlinereturn);
            }

            return Serialized.ToArray();
        }

        public void ImportText(byte[] TextFile)
        {
            List<string> Lines = new List<string>(Entries.Length * 3);

            char[] newline = { '\n' };
            String newlineS = new String(newline);
            char[] backslashn = { '\\', 'n' };
            String backslashnS = new String(backslashn);
            byte[] newlinereturn = { 0x0D, 0x0A };

            int LineStart = 0;
            for ( int i = 0; i < TextFile.Length-1; i++ ) {
                if (TextFile[i] == 0x0D && TextFile[i + 1] == 0x0A)
                {
                    try
                    {
                        int len = (i - LineStart);
                        if (len < 1)
                        {
                            LineStart = i + 2;
                            continue;
                        }
                        String CurrLine = Encoding.UTF8.GetString(TextFile, LineStart, len);
                        CurrLine = CurrLine.Replace(backslashnS, newlineS);
                        Lines.Add(CurrLine);
                        LineStart = i + 2;
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            String CurrLine2 = Encoding.UTF8.GetString(TextFile, LineStart, TextFile.Length - LineStart);
            CurrLine2 = CurrLine2.Replace(backslashnS, newlineS);
            Lines.Add(CurrLine2);

            foreach ( String s in Lines )
            {
                try
                {
                    int location = s.IndexOf(':');
                    String StringNumber = s.Substring(0, location-1);
                    char language = s[location-1];
                    int number = Int32.Parse(StringNumber);
                    if ( language == 'j' ) {
                        Entries[number].StringJPN = s.Substring(location+1);
                    } else if ( language == 'e' ) {
                        Entries[number].StringENG = s.Substring(location+1);
                    }
                } catch ( Exception ) { }
            }

        }


        public byte[] Serialize()
        {
            uint TextStartBuffer = 0x20;

            // recalculate all pointers
            uint CurrentPointer = Header.TextStart + TextStartBuffer;
            foreach (TSSEntry e in Entries)
            {
                if (e.StringJPN != null)
                {
                    e.SetJPNPointer(CurrentPointer - Header.TextStart);
                    CurrentPointer += (uint)Util.StringToBytes(e.StringJPN).Length + 1;
                }
                if (e.StringENG != null)
                {
                    e.SetENGPointer(CurrentPointer - Header.TextStart);
                    CurrentPointer += (uint)Util.StringToBytes(e.StringENG).Length + 1;
                }
            }


            Header.TextLength = CurrentPointer - Header.TextStart;

            List<byte> Serialized = new List<byte>((int)(Header.TextStart + Header.TextLength));
            //serialize
            Serialized.AddRange(Header.Serialize());
            byte[] delimiter = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF };
            foreach (TSSEntry e in Entries)
            {
                Serialized.AddRange(e.SerializeScript());
                Serialized.AddRange(delimiter);
            }
            Serialized.RemoveRange(Serialized.Count - 4, 4);
            for (int i = 0; i < TextStartBuffer; i++)
            {
                Serialized.Add(0x00);
            }
            foreach (TSSEntry e in Entries)
            {
                if (e.StringJPN != null)
                {
                    Serialized.AddRange(Util.StringToBytes(e.StringJPN));
                    Serialized.Add(0x00);
                }
                if (e.StringENG != null)
                {
                    Serialized.AddRange(Util.StringToBytes(e.StringENG));
                    Serialized.Add(0x00);
                }
            }

            return Serialized.ToArray();
        }
    }
}
