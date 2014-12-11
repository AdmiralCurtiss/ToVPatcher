using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToVPatcher {
	public class PatchingException : Exception {
		public PatchingException() { }
		public PatchingException( string message ) : base( message ) { }
		public PatchingException( string message, Exception inner ) : base( message, inner ) { }
	}
}
