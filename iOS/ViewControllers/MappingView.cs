using Foundation;
using PAlert.ViewModels;
using System;
using UIKit;
using WebKit;

namespace PAlert.iOS
{
    public partial class MappingView : UIViewController
    {
        public MappingViewModel ViewModel { get; set; }
        public MappingView (IntPtr handle) : base (handle)
        {
            ViewModel = new MappingViewModel();
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = ViewModel.Title;
            WebView = new WKWebView(View.Frame, new WKWebViewConfiguration());
            View.AddSubview(WebView);

            var url = new NSUrl("https://apexalertengine.web.app/sightings.html");
            var request = new NSUrlRequest(url);
            WebView.LoadRequest(request);
        }
    }
}