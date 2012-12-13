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
		MonoTouch.UIKit.UIImageView m_eventImage { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel m_eventNameLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel m_eventSummaryLabel { get; set; }

		[Action ("eventConfirmYes:")]
		partial void eventConfirmYes (MonoTouch.UIKit.UIButton sender);

		[Action ("eventConfirmNo:")]
		partial void eventConfirmNo (MonoTouch.UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
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
		}
	}
}
