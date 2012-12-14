
using System;
using System.Drawing;
using SayHi.API;
using SayHi.API.Models;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace SayHi
{
	public partial class UserMatchingViewController : UIViewController
	{
		public UserMatchingViewController (IntPtr handle)  : base (handle)
		{

		}

		public UserMatchingViewController () : base ("UserMatchingViewController", null)
		{
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			busyIndicator.StartAnimating ();
			StartGettingMatch ();
			// Perform any additional setup after loading the view, typically from a nib.
		}

		private void StartGettingMatch ()
		{
			SayHiHelper sh = new SayHiHelper ();
			sh.OnMatchUserCompleted += HandleOnMatchUserCompleted;
			sh.MatchUser (SayHiBootStrapper.CurrentUser.ID, SayHiBootStrapper.CurrentEvent.Code);
		}

		void HandleOnMatchUserCompleted (UserModel obj)
		{
			busyIndicator.StopAnimating ();
			busyIndicator.Hidden = true;

			if (obj.IsSucess)
			{
				m_bottomCaption.Hidden = m_topCaption.Hidden = true;
				m_placeHolderImage.Hidden = true;

				//load ui elements with data;
				m_matchedUsersName.Text = string.Format ("{0} {1}", obj.FirstName, obj.LastName);
				m_matchedUsersInterest1.Text = obj.InterestOne;
				m_matchedUsersInterest2.Text = obj.InterestTwo;
				m_matchedUsersSummary.Text = obj.Summary;

				m_matchedUsersImage.Hidden = false;
				m_matchedUsersName.Hidden = false;
				m_matchedUsersInterest1.Hidden = false;
				m_matchedUsersInterest2.Hidden = false;
				m_matchedUsersSummary.Hidden = false;
			}
			else
			{
				SayHiBootStrapper.ShowAlertMessage ("Error", "Could not find match. :(. Go Back and try again?");
			}
		}
		
		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();
			
			// Clear any references to subviews of the main view in order to
			// allow the Garbage Collector to collect them sooner.
			//
			// e.g. myOutlet.Dispose (); myOutlet = null;
			
			ReleaseDesignerOutlets ();
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
		}
	}
}

