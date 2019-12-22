using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UIKit;
namespace PAlert
{
    public class Item
    {
        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "Description")]
        public string Description { get; set; }
        public string Wiki { get; set; }
        public UIImage Image { get; set; }
        [JsonProperty(PropertyName = "Latitude")]
        public double Latitude { get; set; }
        [JsonProperty(PropertyName = "Longitude")]
        public double Longitude { get; set; }
        [JsonProperty(PropertyName = "Date")]
        public DateTime Date { get; internal set; }
    }
}
