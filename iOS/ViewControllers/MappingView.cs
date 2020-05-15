using CoreLocation;
using MapKit;
using PAlert.ViewModels;
using System;
using UIKit;
using Foundation;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PAlert.iOS
{
    /// <summary>
    /// View Controller for creating the Map element used to select locations and view
    /// previous spottings.
    /// </summary>
    public partial class MappingView : UIViewController
    {
        public MappingViewModel ViewModel { get; set; }
        public bool IsEnabled { get; private set; }
        public Action longPress;
        public string returnedChoice;
        private int count = 1;
        private MKPointAnnotation annotation;

        private CLLocationCoordinate2D tappedLocationCoord;
        public CLLocationCoordinate2D GetLocation => tappedLocationCoord;
        public MappingView (IntPtr handle) : base (handle)
        {
            ViewModel = new MappingViewModel();
        }

        public MKMapView GetMap => map;

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();
            
            Title = ViewModel.Title;
            map = new MKMapView(UIScreen.MainScreen.Bounds)
            {
                ShowsUserLocation = true,
                MapType = MKMapType.Hybrid,
                ShowsCompass = true
            };
            
            View = map;
            map.MapLoaded += Map_MapLoaded;
            var tapRecogniser = new UILongPressGestureRecognizer(this, new ObjCRuntime.Selector("MapTapSelector:"));
            map.AddGestureRecognizer(tapRecogniser);
            await LoadPredators(map);  
        }

        /// <summary>
        /// Async Method for obtaining data from the database
        /// </summary>
        /// <param name="map">The current map object <see cref="GetMap"/></param>
        /// <returns></returns>
        private async Task LoadPredators(MKMapView map)
        {
            CloudDataStore data = new CloudDataStore();
            var returns = await data.GetItemsAsync("/");
            if (returns == null)
            {
                return; 
            }
            foreach (var p in returns)
            {
                var annotation = new MKPointAnnotation //TODO: Make custom annotation that has all information presented to user
                {
                    Coordinate = new CLLocationCoordinate2D(p.Value.Latitude, p.Value.Longitude),
                    Title = p.Value.Name,
                    Subtitle = p.Value.Date.ToString()
                };
                map.AddAnnotation(annotation);
            }
        }

        private void CustomDelegate_OnRegionChanged(object sender, MKMapViewChangeEventArgs e)
        {
            var lat = map.Region.Center.Latitude;
            var lon = map.Region.Center.Longitude;
            CoreGraphics.CGPoint location = new CoreGraphics.CGPoint(lat, lon);
            //var coord = map.ConvertPoint(location, map);
            map.AddAnnotation(annotation);
            map.ReloadInputViews();
           
        }

        [Export("MapTapSelector:")]
        protected void OnMapTapped(UIGestureRecognizer sender)
        {
            try
            {
                //predChoice(map);
                tappedLocationCoord = map.ConvertPoint(sender.LocationInView(map), map);
                //start annotation flow 
                PredChoice(map, tappedLocationCoord);
            }
            catch(Exception e)
            {
                Debug.WriteLine($"CaughtException: {e}");
            }
        }

        private void PredChoice(MKMapView mK, CLLocationCoordinate2D tappedLoc)
        {
            PerformSegue("choiceSegue", this);
        }

        private void Map_MapLoaded(object sender, EventArgs e)
        {
            if (count == 1)
            {
                try
                {
                    var span = new MKCoordinateSpan(0.075, 0.075);
                    
                    var region = new MKCoordinateRegion(new CLLocationCoordinate2D(map.UserLocation.Location.Coordinate.Latitude, map.UserLocation.Location.Coordinate.Longitude), span);
                    map.SetRegion(region, true);
                }
                catch (NullReferenceException nre)
                {
                    Debug.WriteLine($"Exception found: {nre}");
                    return;
                }
                catch(SystemException se)
                {
                    Debug.WriteLine($"System Exception found: {se}");
                    return;
                }
            }
            else if (count > 1)
            {
                return;
            }
            count++;
         }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            base.PrepareForSegue(segue, sender);

            // do first a control on the Identifier for your segue
            if (segue.Identifier.Equals("choiceSegue"))
            {
                var viewController = (PredPickView)segue.DestinationViewController;
                viewController.mapping = this;
            }
        }
    }
}