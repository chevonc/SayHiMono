// WARNING
// This file has been generated automatically by MonoDevelop to
// mirror C# types. Changes in this file made by drag-connecting
// from the UI designer will be synchronized back to C#, but
// more complex manual changes may not transfer correctly.


#import "RegisterUserViewController.h"

@implementation RegisterUserViewController

@synthesize firstNameTextField = _firstNameTextField;
@synthesize lastNameTextField = _lastNameTextField;
@synthesize interest1TextField = _interest1TextField;
@synthesize interest2TextField = _interest2TextField;
@synthesize interest3TextField = _interest3TextField;

- (void)dealloc {
    [_emailAddressTextField release];
    [_passwordTextField release];
    [super dealloc];
}
- (IBAction)OnSubmitRegistrationClicked:(UIButton *)sender {
}
@end
