using System;
using Foundation;
using UIKit;
using WebKit;

namespace PAlert.iOS
{
    /// <summary>
    /// View Controller for creating the browser element displaying the predator
    /// information in a web browser.
    /// </summary>
    public partial class BrowseItemDetailViewController : UIViewController
    {
        public ItemDetailViewModel ViewModel { get; set; }
        public BrowseItemDetailViewController(IntPtr handle) : base(handle) { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = ViewModel.Title;
            ItemDescriptionLabel.Text = ViewModel.Item.Description;
            web = new WKWebView(View.Frame, new WKWebViewConfiguration());
            View.AddSubview(web);

            var url = new NSUrl(ViewModel.Item.Wiki);
            var request = new NSUrlRequest(url);
            web.LoadRequest(request);

        }
    }
}
