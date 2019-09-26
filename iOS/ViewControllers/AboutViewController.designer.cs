// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace PAlert.iOS
{
    [Register ("AboutViewController")]
    partial class AboutViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView AboutImage { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView AboutImageView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView AboutTextView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel copyright { get; set; }

        [Action ("ReadMoreButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void ReadMoreButton_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (AboutImage != null) {
                AboutImage.Dispose ();
                AboutImage = null;
            }

            if (AboutImageView != null) {
                AboutImageView.Dispose ();
                AboutImageView = null;
            }

            if (AboutTextView != null) {
                AboutTextView.Dispose ();
                AboutTextView = null;
            }

            if (copyright != null) {
                copyright.Dispose ();
                copyright = null;
            }
        }
    }
}