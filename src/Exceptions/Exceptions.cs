using System;

namespace URPMk2
{
	[Serializable]
	class MissingObjectException : Exception
	{
		public MissingObjectException() { }

		public MissingObjectException(string name)
			: base(String.Format("Fatal Error, {0} object is missing", name))
		{

		}
	}
}
