using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
namespace TO8CHTX_GraceNote
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			if (args.Length != 3)
			{
				Console.WriteLine("Usage: Scenario_GraceNote ScenarioFilename NewDBFilename GracesJapanese");
			}
			else
			{
				string path = args[0];
				string str = args[1];
				string str2 = args[2];
				byte[] array = File.ReadAllBytes(path);
				byte[] value = new byte[]
				{
					array[15],
					array[14],
					array[13],
					array[12]
				};
				int num = BitConverter.ToInt32(value, 0);
				int[] location = Program.GetLocation(array, num);
				ScenarioString[] allStrings = Program.FindAllStrings(location, array, num);
				ScenarioString[] newStrings = Program.RemoveDupesAndEmpty(allStrings);
				Program.InsertSQL(newStrings, "Data Source=" + str, "Data Source=" + str2);
			}
		}
		public static bool InsertSQL(ScenarioString[] NewStrings, string ConnectionString, string ConnectionStringGracesJapanese)
		{
			SQLiteConnection sQLiteConnection = new SQLiteConnection(ConnectionString);
			SQLiteConnection sQLiteConnection2 = new SQLiteConnection(ConnectionStringGracesJapanese);
			sQLiteConnection.Open();
			sQLiteConnection2.Open();
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
									sQLiteCommand2.Parameters.Add( sQLiteParameter2 );
									sQLiteCommand.CommandText = "INSERT INTO Text (ID, StringID, english, comment, updated, status, PointerRef) VALUES (?, ?, ?, null, 0, ?, ?)";
									sQLiteCommand.Parameters.Add( sQLiteParameter3 );
									sQLiteCommand.Parameters.Add( sQLiteParameter4 );
									sQLiteCommand.Parameters.Add( sQLiteParameter5 );
									sQLiteCommand.Parameters.Add( sQLiteParameter8 );
									sQLiteCommand.Parameters.Add( sQLiteParameter6 );
									sQLiteCommand3.CommandText = "SELECT MAX(ID)+1 FROM Japanese";
									sQLiteCommand4.CommandText = "SELECT ID FROM Japanese WHERE string = ?";
									sQLiteCommand4.Parameters.Add( sQLiteParameter7 );
									object obj = sQLiteCommand3.ExecuteScalar();
									int num = int.Parse(obj.ToString());
									int num2 = 1;
									for (int i = 0; i < NewStrings.Length; i++)
									{
										ScenarioString scenarioString = NewStrings[i];
										sQLiteParameter7.Value = scenarioString.Jpn;
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
											sQLiteParameter2.Value = scenarioString.Jpn;
											sQLiteCommand2.ExecuteNonQuery();
										}
										sQLiteParameter3.Value = num2;
										sQLiteParameter4.Value = num3;
										sQLiteParameter5.Value = scenarioString.Eng;
										if (scenarioString.Eng == scenarioString.Jpn)
										{
											sQLiteParameter8.Value = 1;
										}
										else
										{
											sQLiteParameter8.Value = 0;
										}
										sQLiteParameter6.Value = scenarioString.Pointer;
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
		private static ScenarioString[] RemoveDupesAndEmpty(ScenarioString[] AllStrings)
		{
			List<int> list = new List<int>(AllStrings.Length);
			List<ScenarioString> list2 = new List<ScenarioString>(AllStrings.Length);
			for (int i = 0; i < AllStrings.Length; i++)
			{
				ScenarioString scenarioString = AllStrings[i];
				if (!string.IsNullOrEmpty(scenarioString.Jpn) || !string.IsNullOrEmpty(scenarioString.Eng))
				{
					if (!list.Contains(scenarioString.Pointer))
					{
						list.Add(scenarioString.Pointer);
						list2.Add(scenarioString);
					}
				}
			}
			return list2.ToArray();
		}
		private static ScenarioString[] FindAllStrings(int[] PointerList, byte[] Scenario, int TextStart)
		{
			List<ScenarioString> list = new List<ScenarioString>();
			for (int i = 0; i < PointerList.Length; i++)
			{
				int num = PointerList[i];
				int num2 = BitConverter.ToInt32(new byte[]
				{
					Scenario[num + 3],
					Scenario[num + 2],
					Scenario[num + 1],
					Scenario[num]
				}, 0);
				int num3 = num2 + TextStart + 8;
				int num4 = num3;
				int num5 = BitConverter.ToInt32(new byte[]
				{
					Scenario[num4 + 3],
					Scenario[num4 + 2],
					Scenario[num4 + 1],
					Scenario[num4]
				}, 0);
				num4 += 4;
				int num6 = BitConverter.ToInt32(new byte[]
				{
					Scenario[num4 + 3],
					Scenario[num4 + 2],
					Scenario[num4 + 1],
					Scenario[num4]
				}, 0);
				num4 += 4;
				int num7 = BitConverter.ToInt32(new byte[]
				{
					Scenario[num4 + 3],
					Scenario[num4 + 2],
					Scenario[num4 + 1],
					Scenario[num4]
				}, 0);
				num4 += 4;
				int num8 = BitConverter.ToInt32(new byte[]
				{
					Scenario[num4 + 3],
					Scenario[num4 + 2],
					Scenario[num4 + 1],
					Scenario[num4]
				}, 0);
				ScenarioString item = new ScenarioString( num3 - TextStart, Util.GetTextUTF8( Scenario, num5 + TextStart ), Util.GetTextUTF8( Scenario, num7 + TextStart ) );
				ScenarioString item2 = new ScenarioString( num3 - TextStart + 4, Util.GetTextUTF8( Scenario, num6 + TextStart ), Util.GetTextUTF8( Scenario, num8 + TextStart ) );
				list.Add(item);
				list.Add(item2);
			}
			return list.ToArray();
		}
		private static ScenarioString[] FindNewStrings(ScenarioString[] AllStrings, string ConnectionString)
		{
			List<ScenarioString> list = new List<ScenarioString>();
			SQLiteConnection sQLiteConnection = new SQLiteConnection(ConnectionString);
			sQLiteConnection.Open();
			SQLiteTransaction sQLiteTransaction = sQLiteConnection.BeginTransaction();
			for (int i = 0; i < AllStrings.Length; i++)
			{
				ScenarioString scenarioString = AllStrings[i];
				SQLiteCommand sQLiteCommand = new SQLiteCommand(sQLiteConnection);
				sQLiteCommand.CommandText = "SELECT english, PointerRef FROM Text WHERE status != -1 AND PointerRef = ?";
				SQLiteParameter sQLiteParameter = new SQLiteParameter();
				sQLiteCommand.Parameters.Add( sQLiteParameter );
				sQLiteParameter.Value = scenarioString.Pointer;
				SQLiteDataReader sQLiteDataReader = sQLiteCommand.ExecuteReader();
				if (!sQLiteDataReader.Read())
				{
					list.Add(scenarioString);
				}
			}
			return list.ToArray();
		}
		private static int[] GetLocation(byte[] File, int Max)
		{
			byte[] array = new byte[]
			{
				4,
				12,
				0,
				24
			};
			List<int> list = new List<int>();
			for (int i = 0; i < Max; i++)
			{
				if (File[i] == array[0] && File[i + 1] == array[1] && File[i + 2] == array[2] && File[i + 3] == array[3])
				{
					list.Add(i + 4);
				}
			}
			return list.ToArray();
		}
	}
}
