
using System;
using System.Drawing;
using SayHi.API;
using SayHi.API.Models;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace SayHi
{
	public partial class EventSummaryViewController : UIViewController
	{
		public EventSummaryViewController (IntPtr handle) : base (handle)
		{
		}


		public EventSummaryViewController () : base ("EventSummaryViewController", null)
		{
		}

		public EventSummaryMode Mode {
			get;
			set;
		}

		public Event CurrentEvent {
			get;
			set;
		}

		partial void eventConfirmNo (MonoTouch.UIKit.UIButton sender)
		{
			NavigationController.PopViewControllerAnimated (true);
		}

		partial void eventConfirmYes (MonoTouch.UIKit.UIButton sender)
		{
			//go register if notr logged in
			//else go to event details

			if (SayHiBootStrapper.CurrentUser == null)//not logged in
			{
				PerformSegue (SayHiConstants.ESVCtoRUVCSegue, this);
			}
			else
			{
				PerformSegue (SayHiConstants.ESVCtoUMVC, this);
			}
		}

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue (segue, sender);
		
			if (segue.Identifier == SayHiConstants.ESVCtoRUVCSegue)
			{
				RegisterUserViewController vc = (RegisterUserViewController)segue.DestinationViewController;
				vc.SourceSegue = segue.Identifier;
				vc.Mode = RegistrationMode.EventDetailDestination;
				vc.CallingEventVC = this;
			}
			else
			if (segue.Identifier == SayHiConstants.ESVCtoUMVC)
			{
				UserMatchingViewController vc = (UserMatchingViewController)segue.DestinationViewController;

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
			this.Title = "Event Summary";
			if (CurrentEvent != null)
			{
				m_eventSummaryLabel.Text = CurrentEvent.Summary;
				m_dateLabel.Text = CurrentEvent.Date;
				m_eventNameLabel.Text = CurrentEvent.Name;
				m_organizerLabel.Text = CurrentEvent.Organizer;
				m_addressLabel.Text = CurrentEvent.Address;
			}
			//TODO: load all data from CurrentEvent property


			//Setup ();

			// Perform any additional setup after loading the view, typically from a nib.
		}

		void Setup ()
		{

			if (Mode == EventSummaryMode.Normal)
			{
				m_questionLabel.Hidden = false;
				noButton.Hidden = yesButton.Hidden = false;
				checkInButton.Hidden = true;
			}
			else
			{
				m_questionLabel.Hidden = true;
				noButton.Hidden = yesButton.Hidden = true;
				checkInButton.Hidden = false;
			}
			if (Mode == EventSummaryMode.CheckIn)
			{
				checkInButton.TitleLabel.Text = "Check In";
			}
			else
			if (Mode == EventSummaryMode.MatchUser)
			{
				checkInButton.TitleLabel.Text = "Say Hi!";
			}
		}

		partial void eventCheckInClicked (MonoTouch.UIKit.UIButton sender)
		{
			if (SayHiBootStrapper.CurrentUser == null || SayHiBootStrapper.CurrentEvent == null)
			{
				return;
			}

			SayHiHelper sh = new SayHiHelper ();

			if (Mode == EventSummaryMode.CheckIn)
			{
				sh.OnCheckIntoEventCompleted += HandleOnCheckIntoEventCompleted;
				sh.CheckIntoEvent (SayHiBootStrapper.CurrentUser.ID, CurrentEvent.Code);
			}
			else
			if (Mode == EventSummaryMode.MatchUser)
			{
				PerformSegue (SayHiConstants.ESVCtoUMVC, this);
			}
		}

		void HandleOnCheckIntoEventCompleted (ResponseBase obj)
		{
			//take to sayhi screen
			if (obj.IsSucess)
			{
				InvokeOnMainThread (delegate
				{
					PerformSegue (SayHiConstants.ESVCtoUMVC, this);
				});

			}
			else
			{
				string title = "Error";
				string msg = string.Format ("Can't Check-in with event code: {0}", CurrentEvent.Code);
				this.InvokeOnMainThread (
					delegate
				{
					SayHiBootStrapper.ShowAlertMessage (title, msg);
				});
			}
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			Setup ();
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

