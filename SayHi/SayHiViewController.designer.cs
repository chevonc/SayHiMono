// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace SayHi
{
	[Register ("SayHiViewController")]
	partial class SayHiViewController
	{
		[Outlet]
		MonoTouch.UIKit.UILabel signedInHeaderLabel { get; set; }
		
		[Outlet]
		MonoTouch.UIKit.UITextField m_eventCodeBox { get; set; }
		
		[Outlet]
		MonoTouch.UIKit.UIButton m_signedInUserButton { get; set; }
		
		[Action ("onViewSignedInUser:")]
		partial void onViewSignedInUser (MonoTouch.UIKit.UIButton sender);
		
		[Action ("onRegisterClicked:")]
		partial void onRegisterClicked (MonoTouch.UIKit.UIButton sender);
		
		[Action ("textChanged:")]
		partial void textChanged (MonoTouch.UIKit.UITextField sender);
		
		[Action ("onGoButtonClicked:")]
		partial void onGoButtonClicked (MonoTouch.UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (signedInHeaderLabel != null)
			{
				signedInHeaderLabel.Dispose ();
				signedInHeaderLabel = null;
			}
			
			if (m_eventCodeBox != null)
			{
				m_eventCodeBox.Dispose ();
				m_eventCodeBox = null;
			}
			
			if (m_signedInUserButton != null)
			{
				m_signedInUserButton.Dispose ();
				m_signedInUserButton = null;
			}
		}
	}
}