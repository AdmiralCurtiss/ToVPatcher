﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TO8CHTX_GraceNote
{
    class ScenarioFile : IComparable<ScenarioFile>
    {
        public int Pointer;
        public int[] PointerArray;
        public String Name;
        public String Text;

		public string OriginalNameJpn;
		public string OriginalTextJpn;
		public string OriginalNameEng;
		public string OriginalTextEng;

		public int StringIdName;
		public int StringIdText;

        public override string ToString()
        {
            return Name + ": " + Text;
        }

        #region IComparable<ScenarioFile> Members

        public int CompareTo(ScenarioFile other)
        {
            return Pointer.CompareTo(other.Pointer);
        }

        #endregion
    }
}
