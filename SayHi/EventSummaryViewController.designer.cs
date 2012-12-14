// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace SayHi
{
	[Register ("EventSummaryViewController")]
	partial class EventSummaryViewController
	{
		[Outlet]
		MonoTouch.UIKit.UILabel m_addressLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel m_organizerLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel m_dateLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView m_eventImage { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel m_eventNameLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel m_eventSummaryLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton noButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton yesButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel m_questionLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton checkInButton { get; set; }

		[Action ("eventConfirmNo:")]
		partial void eventConfirmNo (MonoTouch.UIKit.UIButton sender);

		[Action ("eventConfirmYes:")]
		partial void eventConfirmYes (MonoTouch.UIKit.UIButton sender);

		[Action ("eventCheckInClicked:")]
		partial void eventCheckInClicked (MonoTouch.UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (m_addressLabel != null) {
				m_addressLabel.Dispose ();
				m_addressLabel = null;
			}

			if (m_organizerLabel != null) {
				m_organizerLabel.Dispose ();
				m_organizerLabel = null;
			}

			if (m_dateLabel != null) {
				m_dateLabel.Dispose ();
				m_dateLabel = null;
			}

			if (m_eventImage != null) {
				m_eventImage.Dispose ();
				m_eventImage = null;
			}

			if (m_eventNameLabel != null) {
				m_eventNameLabel.Dispose ();
				m_eventNameLabel = null;
			}

			if (m_eventSummaryLabel != null) {
				m_eventSummaryLabel.Dispose ();
				m_eventSummaryLabel = null;
			}

			if (noButton != null) {
				noButton.Dispose ();
				noButton = null;
			}

			if (yesButton != null) {
				yesButton.Dispose ();
				yesButton = null;
			}

			if (m_questionLabel != null) {
				m_questionLabel.Dispose ();
				m_questionLabel = null;
			}

			if (checkInButton != null) {
				checkInButton.Dispose ();
				checkInButton = null;
			}
		}
	}
}
