using System;

namespace SayHi
{
	public class ResponseBase
	{
		public ResponseBase ()
		{

		}

		public ResponseBase (bool isSuccess, string message)
		{

		}

		public bool IsSucess {
			get;
			private set;
		}

		public string Message {
			get;
			private set;
		}

	}
}

