
using System;
using System.Drawing;
using SayHi.API;
using SayHi.API.Models;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace SayHi
{
	public partial class UserSummaryViewController : UIViewController
	{
		public UserSummaryViewController (IntPtr handle) : base(handle)
		{

		}

		public UserModel TargetUser {
			get;
			set;
		}

		public UserSummaryViewController () : base ("UserSummaryViewController", null)
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
			
			// Perform any additional setup after loading the view, typically from a nib.
			if (TargetUser == null)
			{
				SayHiBootStrapper.ShowAlertMessage ("Error", "Cannot load user information");
				NavigationController.PopViewControllerAnimated (true);
			}
			else
			{
				m_nameLabel.Text = TargetUser.FirstName + TargetUser.LastName;
				m_usernameLabel.Text = TargetUser.Username;
				m_interest1Label.Text = TargetUser.InterestOne;
				m_interest2Label.Text = TargetUser.InterestTwo;
				m_summaryLabel.Text = TargetUser.Summary;
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

