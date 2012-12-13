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
	UITextField *_m_eventCodeBox;
	UIImageView *_backgroundImage;
}

@property (nonatomic, retain) IBOutlet UITextField *m_eventCodeBox;

@property (nonatomic, retain) IBOutlet UIImageView *backgroundImage;

- (IBAction)textChanged:(UITextField *)sender;

- (IBAction)onGoButtonClicked:(UIButton *)sender;

@end
