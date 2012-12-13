// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace SayHi
{
	[Register ("RegisterUserViewController")]
	partial class RegisterUserViewController
	{
		[Outlet]
		MonoTouch.UIKit.UITextField emailAddressTextField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField passwordTextField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField firstNameTextField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField lastNameTextField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField interest1TextField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField interest2TextField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField interest3TextField { get; set; }

		[Action ("OnSubmitRegistrationClicked:")]
		partial void OnSubmitRegistrationClicked (MonoTouch.UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (emailAddressTextField != null) {
				emailAddressTextField.Dispose ();
				emailAddressTextField = null;
			}

			if (passwordTextField != null) {
				passwordTextField.Dispose ();
				passwordTextField = null;
			}

			if (firstNameTextField != null) {
				firstNameTextField.Dispose ();
				firstNameTextField = null;
			}

			if (lastNameTextField != null) {
				lastNameTextField.Dispose ();
				lastNameTextField = null;
			}

			if (interest1TextField != null) {
				interest1TextField.Dispose ();
				interest1TextField = null;
			}

			if (interest2TextField != null) {
				interest2TextField.Dispose ();
				interest2TextField = null;
			}

			if (interest3TextField != null) {
				interest3TextField.Dispose ();
				interest3TextField = null;
			}
		}
	}
}
