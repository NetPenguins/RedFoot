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
    [Register ("PredPickView")]
    partial class PredPickView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton cancelButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIPickerView chooser { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton submitButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (cancelButton != null) {
                cancelButton.Dispose ();
                cancelButton = null;
            }

            if (chooser != null) {
                chooser.Dispose ();
                chooser = null;
            }

            if (submitButton != null) {
                submitButton.Dispose ();
                submitButton = null;
            }
        }
    }
}