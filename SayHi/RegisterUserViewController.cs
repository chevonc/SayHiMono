
using System;
using System.Drawing;
using SayHi.API;
using SayHi.API.Models;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace SayHi
{
	public partial class RegisterUserViewController : UIViewController
	{
		public string SourceSegue {
			get;
			set;
		}

		public string EventCode {
			get;
			set;
		}

		public RegistrationMode Mode {
			get;
			set;
		}

		public EventSummaryViewController CallingEventVC {
			get;
			set;
		}


		public RegisterUserViewController (IntPtr handle) : base(handle)
		{

		}
		public RegisterUserViewController () : base ("RegisterUserViewController", null)
		{
		}

		partial void OnSubmitRegistrationClicked (MonoTouch.UIKit.UIButton sender)
		{
			SayHiHelper sh = new SayHi.API.SayHiHelper ();
			sh.OnRegisterUserCompleted += HandleOnRegisterUserCompleted;
			sh.RegisterUser (emailAddressTextField.Text, passwordTextField.Text, firstNameTextField.Text,
			                lastNameTextField.Text, emailAddressTextField.Text, "2012-12-12", "", interest1TextField.Text,
			                interest2TextField.Text);
		}

		void HandleOnRegisterUserCompleted (UserModel obj)
		{
			if (Mode == RegistrationMode.EventDetailDestination)
			{
				if (CallingEventVC != null)
				{
					CallingEventVC.Mode = EventSummaryMode.CheckIn;
				}
				//take back to detail screen of event
				//override prepare for segue so that i can show checkin button
				//PerformSegue(SayHiConstants.)
			}
			else
			if (Mode == RegistrationMode.HomePageDestination)
			{
				//take to home screen
				SayHiBootStrapper.SetCurrentUser (obj);
				InvokeOnMainThread (delegate
				{
					NavigationController.PopToRootViewController (true);
				});
			}
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
			this.View.AddGestureRecognizer (new UITapGestureRecognizer (OnViewTouchUp));
			// Perform any additional setup after loading the view, typically from a nib.
		}

		private void OnViewTouchUp (UITapGestureRecognizer rec)
		{
			this.View.EndEditing (true);
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

