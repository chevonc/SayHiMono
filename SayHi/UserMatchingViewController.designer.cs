// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace SayHi
{
	[Register ("UserMatchingViewController")]
	partial class UserMatchingViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIActivityIndicatorView busyIndicator { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel m_topCaption { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel m_bottomCaption { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel m_toLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView m_matchedUsersImage { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel m_matchedUsersName { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel m_matchedUsersSummary { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel m_matchedUsersInterest1 { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel m_matchedUsersInterest2 { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton m_placeHolderImageButton { get; set; }

		[Action ("m_onMatchedUserButtonClicked:")]
		partial void m_onMatchedUserButtonClicked (MonoTouch.UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (busyIndicator != null) {
				busyIndicator.Dispose ();
				busyIndicator = null;
			}

			if (m_topCaption != null) {
				m_topCaption.Dispose ();
				m_topCaption = null;
			}

			if (m_bottomCaption != null) {
				m_bottomCaption.Dispose ();
				m_bottomCaption = null;
			}

			if (m_toLabel != null) {
				m_toLabel.Dispose ();
				m_toLabel = null;
			}

			if (m_matchedUsersImage != null) {
				m_matchedUsersImage.Dispose ();
				m_matchedUsersImage = null;
			}

			if (m_matchedUsersName != null) {
				m_matchedUsersName.Dispose ();
				m_matchedUsersName = null;
			}

			if (m_matchedUsersSummary != null) {
				m_matchedUsersSummary.Dispose ();
				m_matchedUsersSummary = null;
			}

			if (m_matchedUsersInterest1 != null) {
				m_matchedUsersInterest1.Dispose ();
				m_matchedUsersInterest1 = null;
			}

			if (m_matchedUsersInterest2 != null) {
				m_matchedUsersInterest2.Dispose ();
				m_matchedUsersInterest2 = null;
			}

			if (m_placeHolderImageButton != null) {
				m_placeHolderImageButton.Dispose ();
				m_placeHolderImageButton = null;
			}
		}
	}
}
