using System;
using UIKit;
using Xamarin.Forms;
namespace PAlert.iOS
{
    /// <summary>
    /// View Controller for viewing information regarding the application
    /// </summary>
    public partial class AboutViewController : UIViewController
    {
        public AboutViewModel ViewModel { get; set; }
        public AboutViewController(IntPtr handle) : base(handle)
        {
            ViewModel = new AboutViewModel();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = ViewModel.Title;
            copyright.Text = "© NetPenguins 2020";

            var browser = new WebView
            {
                Source = "https://apexalertengine.web.app/sightings.html"
            };
            
            
        }

        partial void ReadMoreButton_TouchUpInside(UIButton sender) => ViewModel.OpenWebCommand.Execute(null);
    }
}
