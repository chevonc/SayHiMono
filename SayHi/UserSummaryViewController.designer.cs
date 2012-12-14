// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace SayHi
{
	[Register ("UserSummaryViewController")]
	partial class UserSummaryViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIActivityIndicatorView m_busyIndicator { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel m_summaryLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel m_nameLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel m_usernameLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel m_interest1Label { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel m_interest2Label { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (m_busyIndicator != null) {
				m_busyIndicator.Dispose ();
				m_busyIndicator = null;
			}

			if (m_summaryLabel != null) {
				m_summaryLabel.Dispose ();
				m_summaryLabel = null;
			}

			if (m_nameLabel != null) {
				m_nameLabel.Dispose ();
				m_nameLabel = null;
			}

			if (m_usernameLabel != null) {
				m_usernameLabel.Dispose ();
				m_usernameLabel = null;
			}

			if (m_interest1Label != null) {
				m_interest1Label.Dispose ();
				m_interest1Label = null;
			}

			if (m_interest2Label != null) {
				m_interest2Label.Dispose ();
				m_interest2Label = null;
			}
		}
	}
}
