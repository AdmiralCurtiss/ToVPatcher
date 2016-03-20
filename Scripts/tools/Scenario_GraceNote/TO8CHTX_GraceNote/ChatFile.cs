using System;
using System.Data.SQLite;
namespace TO8CHTX_GraceNote
{
	internal class ChatFile
	{
		private ChatFileHeader Header;
		private ChatFileLine[] Lines;
		public ChatFile(byte[] TO8CHTX)
		{
			this.Header = default(ChatFileHeader);
			this.Header.Identify = Util.SwapEndian(BitConverter.ToUInt64(TO8CHTX, 0));
			this.Header.Filesize = Util.SwapEndian(BitConverter.ToUInt32(TO8CHTX, 8));
			this.Header.Lines = Util.SwapEndian(BitConverter.ToUInt32(TO8CHTX, 12));
			this.Header.Unknown = Util.SwapEndian(BitConverter.ToUInt32(TO8CHTX, 16));
			this.Header.TextStart = Util.SwapEndian(BitConverter.ToUInt32(TO8CHTX, 20));
			this.Header.Empty = Util.SwapEndian(BitConverter.ToUInt64(TO8CHTX, 24));
			this.Lines = new ChatFileLine[this.Header.Lines];
			int num = 0;
			while ((long)num < (long)((ulong)this.Header.Lines))
			{
				this.Lines[num] = default(ChatFileLine);
				this.Lines[num].Location = 32 + num * 16;
				this.Lines[num].Name = Util.SwapEndian(BitConverter.ToUInt32(TO8CHTX, 32 + num * 16));
				this.Lines[num].JPN = Util.SwapEndian(BitConverter.ToUInt32(TO8CHTX, 36 + num * 16));
				this.Lines[num].ENG = Util.SwapEndian(BitConverter.ToUInt32(TO8CHTX, 40 + num * 16));
				this.Lines[num].Unknown = Util.SwapEndian(BitConverter.ToUInt32(TO8CHTX, 44 + num * 16));
				this.Lines[num].SName = this.GetText(TO8CHTX, this.Lines[num].Name + this.Header.TextStart);
				this.Lines[num].SJPN = this.GetText(TO8CHTX, this.Lines[num].JPN + this.Header.TextStart);
				this.Lines[num].SENG = this.GetText(TO8CHTX, this.Lines[num].ENG + this.Header.TextStart).Replace('@', ' ');
				num++;
			}
		}
		public string GetText(byte[] File, uint UPointer)
		{
			string result;
			try
			{
				int num = (int)UPointer;
				while (File[num] != 0)
				{
					num++;
				}
				string @string = Util.ShiftJISEncoding.GetString(File, (int)UPointer, num - (int)UPointer);
				result = @string;
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}
		public bool InsertSQL(string ConnectionString, string ConnectionStringGracesJapanese)
		{
			SQLiteConnection sQLiteConnection = new SQLiteConnection(ConnectionString);
			SQLiteConnection sQLiteConnection2 = new SQLiteConnection(ConnectionStringGracesJapanese);
			sQLiteConnection.Open();
			sQLiteConnection2.Open();
			new SQLiteCommand(sQLiteConnection)
			{
				CommandText = "DELETE FROM Text"
			}.ExecuteNonQuery();
			using (SQLiteTransaction sQLiteTransaction = sQLiteConnection.BeginTransaction())
			{
				using (SQLiteTransaction sQLiteTransaction2 = sQLiteConnection2.BeginTransaction())
				{
					using (SQLiteCommand sQLiteCommand = new SQLiteCommand(sQLiteConnection))
					{
						using (SQLiteCommand sQLiteCommand2 = new SQLiteCommand(sQLiteConnection2))
						{
							using (SQLiteCommand sQLiteCommand3 = new SQLiteCommand(sQLiteConnection2))
							{
								using (SQLiteCommand sQLiteCommand4 = new SQLiteCommand(sQLiteConnection2))
								{
									SQLiteParameter sQLiteParameter = new SQLiteParameter();
									SQLiteParameter sQLiteParameter2 = new SQLiteParameter();
									SQLiteParameter sQLiteParameter3 = new SQLiteParameter();
									SQLiteParameter sQLiteParameter4 = new SQLiteParameter();
									SQLiteParameter sQLiteParameter5 = new SQLiteParameter();
									SQLiteParameter sQLiteParameter6 = new SQLiteParameter();
									SQLiteParameter sQLiteParameter7 = new SQLiteParameter();
									SQLiteParameter sQLiteParameter8 = new SQLiteParameter();
									sQLiteCommand2.CommandText = "INSERT INTO Japanese (ID, string, debug) VALUES (?, ?, 0)";
									sQLiteCommand2.Parameters.Add(sQLiteParameter);
									sQLiteCommand2.Parameters.Add(sQLiteParameter2);
									sQLiteCommand.CommandText = "INSERT INTO Text (ID, StringID, english, comment, updated, status, PointerRef) VALUES (?, ?, ?, null, 0, ?, ?)";
									sQLiteCommand.Parameters.Add(sQLiteParameter3);
									sQLiteCommand.Parameters.Add(sQLiteParameter4);
									sQLiteCommand.Parameters.Add(sQLiteParameter5);
									sQLiteCommand.Parameters.Add(sQLiteParameter8);
									sQLiteCommand.Parameters.Add(sQLiteParameter6);
									sQLiteCommand3.CommandText = "SELECT MAX(ID)+1 FROM Japanese";
									sQLiteCommand4.CommandText = "SELECT ID FROM Japanese WHERE string = ?";
									sQLiteCommand4.Parameters.Add(sQLiteParameter7);
									object obj = sQLiteCommand3.ExecuteScalar();
									int num = int.Parse(obj.ToString());
									int num2 = 1;
									ChatFileLine[] lines = this.Lines;
									for (int i = 0; i < lines.Length; i++)
									{
										ChatFileLine chatFileLine = lines[i];
										sQLiteParameter7.Value = chatFileLine.SName;
										object obj2 = sQLiteCommand4.ExecuteScalar();
										int num3;
										if (obj2 != null)
										{
											num3 = (int)obj2;
										}
										else
										{
											num3 = num++;
											sQLiteParameter.Value = num3;
											sQLiteParameter2.Value = chatFileLine.SName;
											sQLiteCommand2.ExecuteNonQuery();
										}
										sQLiteParameter3.Value = num2;
										sQLiteParameter4.Value = num3;
										sQLiteParameter5.Value = chatFileLine.SName;
										sQLiteParameter8.Value = 1;
										sQLiteParameter6.Value = chatFileLine.Location;
										sQLiteCommand.ExecuteNonQuery();
										num2++;
										sQLiteParameter7.Value = chatFileLine.SJPN;
										obj2 = sQLiteCommand4.ExecuteScalar();
										if (obj2 != null)
										{
											num3 = (int)obj2;
										}
										else
										{
											num3 = num++;
											sQLiteParameter.Value = num3;
											sQLiteParameter2.Value = chatFileLine.SJPN;
											sQLiteCommand2.ExecuteNonQuery();
										}
										sQLiteParameter3.Value = num2;
										sQLiteParameter4.Value = num3;
										if (chatFileLine.SENG == "Dummy" || chatFileLine.SENG == "")
										{
											sQLiteParameter5.Value = null;
											sQLiteParameter8.Value = 0;
										}
										else
										{
											sQLiteParameter5.Value = chatFileLine.SENG;
											sQLiteParameter8.Value = 1;
										}
										sQLiteParameter6.Value = chatFileLine.Location + 4;
										sQLiteCommand.ExecuteNonQuery();
										num2++;
									}
									sQLiteTransaction.Commit();
									sQLiteTransaction2.Commit();
								}
							}
						}
					}
				}
			}
			sQLiteConnection2.Close();
			sQLiteConnection.Close();
			return true;
		}
	}
}
