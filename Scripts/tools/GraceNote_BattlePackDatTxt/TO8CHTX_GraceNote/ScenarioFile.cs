using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TO8CHTX_GraceNote
{
    class ScenarioFile : IComparable<ScenarioFile>
    {
        public int Pointer;
        public String Text;

		public string OriginalTextJpn;
		public string OriginalTextEng;
		public int StringIdText;

        #region IComparable<ScenarioFile> Members

        public int CompareTo(ScenarioFile other)
        {
            return Pointer.CompareTo(other.Pointer);
        }

        #endregion
    }
}
