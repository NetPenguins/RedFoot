using System;
using UIKit;
namespace PAlert
{
    public class Item
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Wiki { get; set; }
        public UIImage Image { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Date { get; internal set; }
    }
}
