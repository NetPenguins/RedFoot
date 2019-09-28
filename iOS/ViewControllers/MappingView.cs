using CoreLocation;
using MapKit;
using PAlert.ViewModels;
using System;
using UIKit;
using MonoTouch;
using Foundation;

namespace PAlert.iOS
{
    public partial class MappingView : UIViewController 
    {
        public MappingViewModel ViewModel { get; set; }
        public bool IsEnabled { get; private set; }
        public Action longPress;
        public MappingView (IntPtr handle) : base (handle)
        {
            ViewModel = new MappingViewModel();
            map.MapLoaded += Map_MapLoaded;
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = ViewModel.Title;
            //var customDelegate = new CustomMapViewDelegate();
            map = new MKMapView(UIScreen.MainScreen.Bounds);
            map.ShowsUserLocation = true;
            View = map;
            //customDelegate.OnRegionChanged += CustomDelegate_OnRegionChanged;
            //map.Delegate = customDelegate;
            
            var tapRecogniser = new UILongPressGestureRecognizer(this, new ObjCRuntime.Selector("MapTapSelector:"));
            map.AddGestureRecognizer(tapRecogniser);
            //WebView = new WKWebView(View.Frame, new WKWebViewConfiguration());
            //View.AddSubview(WebView);

            //var url = new NSUrl("https://apexalertengine.web.app/sightings.html");
            //var request = new NSUrlRequest(url);
            //WebView.Configuration.Preferences.JavaScriptCanOpenWindowsAutomatically = true;
            //WebView.LoadRequest(request);

        }

        private void CustomDelegate_OnRegionChanged(object sender, MKMapViewChangeEventArgs e)
        {
            var lat = map.Region.Center.Latitude;
            var lon = map.Region.Center.Longitude;

            CoreGraphics.CGPoint location = new CoreGraphics.CGPoint(lat, lon);
            var coord = map.ConvertPoint(location, map);
            var annotation = new MKPointAnnotation(coord, "working", "yay");
            
            map.AddAnnotation(annotation);
            map.ReloadInputViews();
           
        }

        [Export("MapTapSelector:")]
        protected void OnMapTapped(UIGestureRecognizer sender)
        {
            CLLocationCoordinate2D tappedLocationCoord = map.ConvertPoint(sender.LocationInView(map), map);
            //start annotation flow 
            var annotation = new MKPointAnnotation{
                Coordinate = tappedLocationCoord,
                Title = "working",
                Subtitle = "yay"
            };
            map.AddAnnotation(annotation);
        }

        private void Map_MapLoaded(object sender, EventArgs e)
        {
            
            var span = new MKCoordinateSpan(0.075, 0.075); 
            var region = new MKCoordinateRegion(new CLLocationCoordinate2D(map.UserLocation.Location.Coordinate.Latitude, map.UserLocation.Location.Coordinate.Longitude), span);
            map.SetRegion(region, true);
           
         }

        //public class CustomMapViewDelegate : MKMapViewDelegate
        //{
        //    public event EventHandler<MKMapViewChangeEventArgs> OnRegionChanged;
        //    public override void RegionChanged(MKMapView mapView, bool animated)
        //    {
        //        OnRegionChanged?.Invoke(mapView, new MKMapViewChangeEventArgs(animated));
        //    }
        //}
    }

}