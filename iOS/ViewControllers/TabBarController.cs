using System;
using UIKit;

namespace PAlert.iOS
{
    /// <summary>
    /// Controller for the main tab controls used to navigate the application
    /// </summary>
    public partial class TabBarController : UITabBarController
    {
        public TabBarController(IntPtr handle) : base(handle)
        {
            TabBar.Items[0].Title = "Browse";
            TabBar.Items[1].Title = "Map";
            TabBar.Items[2].Title = "About";
            
        }
    }
}
