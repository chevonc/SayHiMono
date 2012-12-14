// WARNING
// This file has been generated automatically by MonoDevelop to
// mirror C# types. Changes in this file made by drag-connecting
// from the UI designer will be synchronized back to C#, but
// more complex manual changes may not transfer correctly.


#import <UIKit/UIKit.h>
#import <MapKit/MapKit.h>
#import <Foundation/Foundation.h>
#import <CoreGraphics/CoreGraphics.h>


@interface UserMatchingViewController : UIViewController {
	UIActivityIndicatorView *_busyIndicator;
	UILabel *_m_topCaption;
	UILabel *_m_bottomCaption;
	UIImageView *_m_placeHolderImage;
}

@property (nonatomic, retain) IBOutlet UIActivityIndicatorView *busyIndicator;

@property (nonatomic, retain) IBOutlet UILabel *m_topCaption;

@property (nonatomic, retain) IBOutlet UILabel *m_bottomCaption;

@property (nonatomic, retain) IBOutlet UIImageView *m_placeHolderImage;
@property (retain, nonatomic) IBOutlet UILabel *m_toLabel;
@property (retain, nonatomic) IBOutlet UIImageView *m_matchedUsersImage;
@property (retain, nonatomic) IBOutlet UILabel *m_matchedUsersName;
@property (retain, nonatomic) IBOutlet UILabel *m_matchedUsersSummary;
@property (retain, nonatomic) IBOutlet UILabel *m_matchedUsersInterest1;
@property (retain, nonatomic) IBOutlet UILabel *m_matchedUsersInterest2;

@end
