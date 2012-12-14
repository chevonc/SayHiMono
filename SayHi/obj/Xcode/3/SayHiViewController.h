// WARNING
// This file has been generated automatically by MonoDevelop to
// mirror C# types. Changes in this file made by drag-connecting
// from the UI designer will be synchronized back to C#, but
// more complex manual changes may not transfer correctly.


#import <UIKit/UIKit.h>
#import <MapKit/MapKit.h>
#import <Foundation/Foundation.h>
#import <CoreGraphics/CoreGraphics.h>


@interface SayHiViewController : UIViewController {
	UILabel *_signedInHeaderLabel;
	UITextField *_m_eventCodeBox;
	UIButton *_m_signedInUserButton;
}

@property (nonatomic, retain) IBOutlet UILabel *signedInHeaderLabel;

@property (nonatomic, retain) IBOutlet UITextField *m_eventCodeBox;

@property (nonatomic, retain) IBOutlet UIButton *m_signedInUserButton;

- (IBAction)onViewSignedInUser:(UIButton *)sender;

- (IBAction)onRegisterClicked:(UIButton *)sender;

- (IBAction)textChanged:(UITextField *)sender;

- (IBAction)onGoButtonClicked:(UIButton *)sender;

@end
