using System;
using SayHi.API.Models;
using MonoTouch.UIKit;

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

		public static UserModel CurrentMatchUser {
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

		public static void SetCurrentMatchUser (UserModel user)
		{
			CurrentMatchUser = user;
		}

		public static void ShowAlertMessage (string title, string msg, string firstButtonText = "OK", params string[] otherButtonsTexts)
		{
			UIAlertView error = new UIAlertView (title, msg, null, firstButtonText, otherButtonsTexts);
			error.Show ();
		}

		private static readonly Random sr_rand = new Random ();

		public static string GenerateMockInterest (string skip = "")
		{
			string ret = "";
			do
			{
				ret = MockInterests [sr_rand.Next (MockInterests.Length)];
			}
			while(ret == skip);

			return ret;
		}

		public static string[] MockInterests = new string[]{
			"Computer Science", ".NET", "Diving", "Mac OSX", "JAVA", "C#", "Windows Phone", "Objective-c",
			"Computers", "WP8", "Hardware", "Big Data", "Android", "APIs", "JavaScript", "Rails", "iOS", "Django",
			"Ruby", "Mobile", "C++", "PHP", "C", "Everything"
		};
	}
}

