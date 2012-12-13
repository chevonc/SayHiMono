
using System;
using System.Drawing;

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

		partial void eventConfirmNo (MonoTouch.UIKit.UIButton sender)
		{
			NavigationController.PopViewControllerAnimated (true);
		}

		partial void eventConfirmYes (MonoTouch.UIKit.UIButton sender)
		{
			//go register if notr logged in
			//else go to event details
			if (!false)//not logged in
			{
				PerformSegue (SayHiConstants.ESVCtoRUVCSegue, this);
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
			if (Mode == EventSummaryMode.CheckIn)
			{
				noButton.Hidden = yesButton.Hidden = true;
				checkInButton.Hidden = false;
			}
			else
			{
				noButton.Hidden = yesButton.Hidden = false;
				checkInButton.Hidden = true;
			}
			// Perform any additional setup after loading the view, typically from a nib.
		}

		partial void eventCheckInClicked (MonoTouch.UIKit.UIButton sender)
		{

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

