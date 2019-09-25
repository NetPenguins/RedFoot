using System;
using UIKit;
using Xamarin.Forms;
namespace PAlert.iOS
{
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
            AboutText.Text = "This mobile application is provided to help people around the world identify dangerous predators and share the knowledge with others.";
            copyright.Text = "© Chad Wilson 2019";

            var browser = new WebView
            {
                Source = "https://apexalertengine.web.app/sightings.html"
            };
            
            
        }

        partial void ReadMoreButton_TouchUpInside(UIButton sender) => ViewModel.OpenWebCommand.Execute(null);
    }
}
