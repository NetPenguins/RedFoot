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
    [Register ("MappingView")]
    partial class MappingView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILongPressGestureRecognizer initPoint { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        MapKit.MKMapView map { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (initPoint != null) {
                initPoint.Dispose ();
                initPoint = null;
            }

            if (map != null) {
                map.Dispose ();
                map = null;
            }
        }
    }
}