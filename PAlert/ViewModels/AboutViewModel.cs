using System.Windows.Input;

namespace PAlert
{
    /// <summary>
    /// Model for the about view
    /// </summary>
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";

            OpenWebCommand = new Command(() => Plugin.Share.CrossShare.Current.OpenBrowser("https://apexalertengine.web.app/sightings.html"));
        }

        public ICommand OpenWebCommand { get; }
    }
}
