using Foundation;
using System;
using UIKit;
using PAlert.ViewModels;
using PAlert.iOS;
using MapKit;
using System.Diagnostics;
using GameplayKit;
using NLog;
using System.Net;

namespace PAlert.iOS
{
    /// <summary>
    /// View Controller for picking the spotted predator.
    /// </summary>
    public partial class PredPickView : UIViewController
    {
        public PredModel @picker { get; set; }
        public MappingView mapping { get; set; }
        private CloudDataStore dataStore = new CloudDataStore();
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

        private async void SubmitButton_TouchDown(object sender, EventArgs e)
        {
            try
            {
                Debug.WriteLine(sender.GetType().Name);
                mapping.returnedChoice = picker.SelectedName;

                var item = new Item
                {
                    Name = picker.SelectedName,
                    Latitude = mapping.GetLocation.Latitude,
                    Longitude = mapping.GetLocation.Longitude,
                    Id = Guid.NewGuid().ToString(),
                    Date = DateTime.Now,
                    Description = picker.SelectedItem.Description,
                    Image = picker.SelectedItem.Image
                };
                var result = await dataStore.AddItemAsync(item, picker.SelectedName.ToLowerInvariant(), picker.SelectedItem.Id);
                if(!(result.Equals(HttpStatusCode.Accepted )) || !(result.Equals(HttpStatusCode.OK)) || !(result.Equals(HttpStatusCode.Processing))){
                    Debug.WriteLine($"Status code returned with {result}");
                    //return;
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
            var annotation = new MKPointAnnotation
            {
                Coordinate = mapping.GetLocation,
                Title = picker.SelectedName
            };
            
            mapping.GetMap.AddAnnotation(annotation);
            DismissViewController(true, null);
        }
    }
}