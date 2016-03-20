using System;
namespace TO8CHTX_GraceNote
{
	internal struct ChatFileHeader
	{
		public ulong Identify;
		public uint Filesize;
		public uint Lines;
		public uint Unknown;
		public uint TextStart;
		public ulong Empty;
	}
}
