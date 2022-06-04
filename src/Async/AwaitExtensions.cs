using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace URPMk2
{
	// credits to http://www.stevevermeulen.com/index.php/2017/09/using-async-await-in-unity3d-2017/
	public static class AwaitExtensions
	{
		public static TaskAwaiter GetAwaiter(this TimeSpan timeSpan)
        {
			return Task.Delay(timeSpan).GetAwaiter();
        }
	}
}
