using System;
using System.Drawing;
using SayHi.API;
using SayHi.API.Models;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace SayHi
{
	public partial class SayHiViewController : UIViewController
	{
		public SayHiViewController (IntPtr handle) : base (handle)
		{
		}
		
		public Event CurrentEvent {
			get;
			private set;
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}
		
		#region View lifecycle
		
		bool initialSetup = false;
		void Setup ()
		{
			if (SayHiBootStrapper.CurrentUser != null)
			{
				signedInHeaderLabel.Hidden = false;
				m_signedInUserButton.TitleLabel.Text = SayHiBootStrapper.CurrentUser.FirstName + " " + SayHiBootStrapper.CurrentUser.LastName;
			}
			else
			{
				signedInHeaderLabel.Hidden = m_signedInUserButton.Hidden = true;
				if (!initialSetup)
				{
					initialSetup = true;
					SayHiBootStrapper.ShowAlertMessage ("Info", "Enter an Event Code to get started");
				}
			}
		}
		
		public override void ViewDidLoad ()
		{
			
			base.ViewDidLoad ();//"sayhi1m,pp";
			this.CurrentEvent = null;
			this.Title = "Welcome!";
			this.View.AddGestureRecognizer (new UITapGestureRecognizer (OnViewTouchUp));
			//Setup ();
			//new SayHi.API.SayHiHelper ().LoginUser ("sayhi", "pp");
			//new SayHi.API.SayHiHelper ().RegisterUser ("sayhiuser00011", "password1", "fb", "ln", "sayhi1@sayhi.com", "1/1/1/", "asdasd");
			// Perform any additional setup after loading the view, typically from a nib.
		}
		
		public void OnViewTouchUp (UITapGestureRecognizer rec)
		{
			m_eventCodeBox.EndEditing (true);
		}
		
		
		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue (segue, sender);
			if (segue.Identifier == SayHiConstants.ESVCtoRUVCSegue)
			{
				RegisterUserViewController vc = (RegisterUserViewController)segue.DestinationViewController;
				vc.SourceSegue = segue.Identifier;
				vc.Mode = RegistrationMode.HomePageDestination;
			}
			else
			if (segue.Identifier == SayHiConstants.SHVCtoESVCSegue)
			{
				EventSummaryViewController vc = (EventSummaryViewController)segue.DestinationViewController;
				vc.Mode = EventSummaryMode.Normal;
				vc.CurrentEvent = this.CurrentEvent;
			}
			else
			if (segue.Identifier == SayHiConstants.SHVCtoUSVC)
			{
				UserSummaryViewController vc = (UserSummaryViewController)segue.DestinationViewController;
				vc.TargetUser = SayHiBootStrapper.CurrentUser;
			}
		}
		
		partial void onViewSignedInUser (MonoTouch.UIKit.UIButton sender)
		{
			PerformSegue (SayHiConstants.SHVCtoUSVC, this);
		}
		
		partial void onRegisterClicked (MonoTouch.UIKit.UIButton sender)
		{
			PerformSegue (SayHiConstants.SHVCtoRUVC, this);
		}
		
		partial void textChanged (MonoTouch.UIKit.UITextField sender)
		{
			
		}
		
		bool isWaiting = false;
		partial void onGoButtonClicked (MonoTouch.UIKit.UIButton sender)
		{
			//navigate
			if (isWaiting)
			{
				return;
			}
			
			SayHiHelper sh = new SayHiHelper ();
			sh.OnGetEventInfoCompleted += HandleOnGetEventInfoCompleted;
			isWaiting = true;
			sh.GetEventInfo (m_eventCodeBox.Text);
		}
		
		void HandleOnGetEventInfoCompleted (SayHi.API.Models.Event obj)
		{
			this.CurrentEvent = obj;
			
			if (obj.IsSucess)
			{
				this.InvokeOnMainThread (
					delegate
				{
					PerformSegue (SayHiConstants.SHVCtoESVCSegue, this);
				});
			}
			else
			{
				this.InvokeOnMainThread (
					delegate
				{
					SayHiBootStrapper.ShowAlertMessage ("Error", "Could not retrieve event. Be sure that the code is correct and you are connected to a network");
				});
			}
			
			isWaiting = false;
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
		
		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}
		
		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			Setup ();
		}
		
		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
		}
		
		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
		}
		
#endregion
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
		}
	}
}
