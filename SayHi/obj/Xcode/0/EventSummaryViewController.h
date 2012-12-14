// WARNING
// This file has been generated automatically by MonoDevelop to
// mirror C# types. Changes in this file made by drag-connecting
// from the UI designer will be synchronized back to C#, but
// more complex manual changes may not transfer correctly.


#import <UIKit/UIKit.h>
#import <MapKit/MapKit.h>
#import <Foundation/Foundation.h>
#import <CoreGraphics/CoreGraphics.h>


@interface EventSummaryViewController : UIViewController {
	UILabel *_m_addressLabel;
	UILabel *_m_organizerLabel;
	UILabel *_m_dateLabel;
	UIImageView *_m_eventImage;
	UILabel *_m_eventNameLabel;
	UILabel *_m_eventSummaryLabel;
	UIButton *_noButton;
	UIButton *_yesButton;
	UILabel *_m_questionLabel;
	UIButton *_checkInButton;
}

@property (nonatomic, retain) IBOutlet UILabel *m_addressLabel;

@property (nonatomic, retain) IBOutlet UILabel *m_organizerLabel;

@property (nonatomic, retain) IBOutlet UILabel *m_dateLabel;

@property (nonatomic, retain) IBOutlet UIImageView *m_eventImage;

@property (nonatomic, retain) IBOutlet UILabel *m_eventNameLabel;

@property (nonatomic, retain) IBOutlet UILabel *m_eventSummaryLabel;

@property (nonatomic, retain) IBOutlet UIButton *noButton;

@property (nonatomic, retain) IBOutlet UIButton *yesButton;

@property (nonatomic, retain) IBOutlet UILabel *m_questionLabel;

@property (nonatomic, retain) IBOutlet UIButton *checkInButton;

- (IBAction)eventConfirmNo:(UIButton *)sender;

- (IBAction)eventConfirmYes:(UIButton *)sender;

- (IBAction)eventCheckInClicked:(UIButton *)sender;

@end
