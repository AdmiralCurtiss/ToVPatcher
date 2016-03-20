using System;
namespace TO8CHTX_GraceNote
{
	internal class ScenarioString
	{
		public int Pointer;
		public string Jpn;
		public string Eng;
		public ScenarioString(int Pointer, string Jpn, string Eng)
		{
			this.Pointer = Pointer;
			this.Jpn = Jpn;
			this.Eng = Eng;
		}
	}
}
