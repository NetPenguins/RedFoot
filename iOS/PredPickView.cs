using Foundation;
using System;
using UIKit;
using PAlert.ViewModels;
using PAlert.iOS;
using MapKit;
using System.Diagnostics;

namespace PAlert.iOS
{
    public partial class PredPickView : UIViewController
    {
        public PredModel @picker { get; set; }
        public MappingView mapping { get; set; }

        public PredPickView (IntPtr handle) : base (handle)
        {
            picker = new PredModel(new UILabel());
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            chooser.Model = picker;
            submitButton.PrimaryActionTriggered += SubmitButton_TouchDown;
            cancelButton.PrimaryActionTriggered += CancelButton_TouchDown;
        }

        private void CancelButton_TouchDown(object sender, EventArgs e)
        {
            DismissViewController(true, null);
        }

        private void SubmitButton_TouchDown(object sender, EventArgs e)
        {
            Debug.WriteLine(sender.GetType().Name);
            mapping.returnedChoice = picker.SelectedItem;
            var annotation = new MKPointAnnotation
            {
                Coordinate = mapping.GetLocation,
                Title = picker.SelectedItem,
                Subtitle = "yay"
            };
            mapping.GetMap.AddAnnotation(annotation);
            DismissViewController(true, null);
        }
    }
}