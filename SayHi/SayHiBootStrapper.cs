using System;
using SayHi.API.Models;

namespace SayHi
{
	public class SayHiBootStrapper
	{
		public SayHiBootStrapper ()
		{
		}

		public static UserModel CurrentUser {
			get;
			private set;
		}

		public static Event CurrentEvent {
			get;
			private set;
		}

		public static void SetCurrentUser (UserModel user)
		{
			CurrentUser = user;
		}

		public static void SetCurrentEvent (Event evnt)
		{
			CurrentEvent = evnt;
		}
	}
}

