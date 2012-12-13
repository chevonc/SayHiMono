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
		
		public override void ViewDidLoad ()
		{

			base.ViewDidLoad ();//"sayhi1m,pp";
			this.Title = "Welcome!";
			this.View.AddGestureRecognizer (new UITapGestureRecognizer (OnViewTouchUp));
			if (SayHiBootStrapper.CurrentUser != null)
			{
				signedInHeaderLabel.Hidden = false;
				signedInUserNameLabel.Text = SayHiBootStrapper.CurrentUser.FirstName + " " + 
					SayHiBootStrapper.CurrentUser.LastName;
			}
			else
			{
				signedInHeaderLabel.Hidden = signedInUserNameLabel.Hidden = true;
			}
			//new SayHi.API.SayHiHelper ().LoginUser ("sayhi", "pp");
			//new SayHi.API.SayHiHelper ().RegisterUser ("sayhiuser00011", "password1", "fb", "ln", "sayhi1@sayhi.com", "1/1/1/", "asdasd");
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public void OnViewTouchUp (UITapGestureRecognizer rec)
		{
			m_eventCodeBox.EndEditing (true);
		}

		partial void textChanged (MonoTouch.UIKit.UITextField sender)
		{

		}

		partial void onGoButtonClicked (MonoTouch.UIKit.UIButton sender)
		{
			//navigate
			SayHiHelper sh = new SayHiHelper ();
			sh.OnGetEventInfoCompleted += HandleOnGetEventInfoCompleted;
			sh.GetEventInfo (m_eventCodeBox.Text);
		}

		void HandleOnGetEventInfoCompleted (SayHi.API.Models.Event obj)
		{
			this.CurrentEvent = obj;
			this.InvokeOnMainThread (
				delegate
			{
				PerformSegue (SayHiConstants.SHVCtoESVCSegue, this);
			});
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

