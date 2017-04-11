using AAF1.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Connectivity;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace AAF1
{
   

    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        ApplicationDataContainer _roamingData = ApplicationData.Current.RoamingSettings;
        ApplicationDataContainer _localData = ApplicationData.Current.LocalSettings;

        ApplicationDataContainer _roamingNumberOfVisit = ApplicationData.Current.RoamingSettings;
        ApplicationDataContainer _localNumberOfVisit = ApplicationData.Current.LocalSettings;

       
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

       
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        public HomePage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;

         //   this.SizeChanged += WindowingPage_SizeChanged;

        //   Current_SizeChanged +=Current_SizeChanged;
        }




        //void WindowingPage_SizeChanged(object sender, VariableSizedWrapGrid e)
        //{
        //    if (e.ActualWidth < 500)
        //    {
        //        VisualStateManager.GoToState(this, "PortraitLayout", true);
                
        //        //v.Visibility = Visibility = Visibility.Visible;
        //    }
        //    else
        //    {
        //        VisualStateManager.GoToState(this, "DefaultLayout", true);
               
        //    }
        //}


        public static bool CheckInternetConnectivity()
        {
            var internetProfile = NetworkInformation.GetInternetConnectionProfile();
            if (internetProfile == null)
                return false;

            return (internetProfile.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess);
        }

        private bool ReadAppRatingSetting()
        {
            if (_localData.Values["AppRatingDone"] != null)
                return (bool)_localData.Values["AppRatingDone"];

            if (_roamingData.Values["AppRatingDone"] != null)
                return (bool)_roamingData.Values["AppRatingDone"];

            return false;
        }

        private void IgnoreRating(IUICommand command)
        {
            //SaveAppRatingSetting();
        }

        private async void OpenStoreRating(IUICommand command)
        {
            string name = Package.Current.Id.FamilyName;
            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store:REVIEW?PFN=" + name));
            //await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store:REVIEW?PFN=6b7b722b-a69d-42fc-8fba-f1b30776edec"));
            //SaveAppRatingSetting();
        }
        public static bool IsInternet()
        {
            ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
            bool internet = connections != null && connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;
            return internet;
        }

        private async void rate_Click(object sender, RoutedEventArgs e)
        {
            bool Connect;
            Connect = IsInternet();
            if (Connect == true)
            {
                if (!CheckInternetConnectivity())
                    return;

                if (ReadAppRatingSetting())
                    return;

                MessageDialog msg;
                bool retry = false;
                msg = new MessageDialog("Can you help us in rating application");
                msg.Commands.Add(new UICommand("Rate", new UICommandInvokedHandler(this.OpenStoreRating)));
                msg.Commands.Add(new UICommand("Not Now", (uiCommand) => { }));
                msg.Commands.Add(new UICommand("No Thanks", new UICommandInvokedHandler(this.IgnoreRating)));
                msg.DefaultCommandIndex = 0;
                msg.CancelCommandIndex = 1;
                await msg.ShowAsync();

                while (retry)
                {
                    try { await msg.ShowAsync(); }
                    catch { }
                }
            }
            else
            {

                await new MessageDialog("No Internet Connection , please check your connection and try again ", "Academic AppFactory").ShowAsync();

            }
        }

    
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {


           
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Frame.Navigate(typeof(HomePage));
            
            Window.Current.SizeChanged +=Current_SizeChanged;

        }

        private void Current_SizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            Snap();
        }

        private void Snap()
        {
         //   ApplicationView current = ApplicationView.GetForCurrentView;
            ApplicationView current = ApplicationView.GetForCurrentView();
            if(current.IsFullScreen)
            {
                SnapStack.Visibility = Visibility.Collapsed;
                w8.Visibility = Visibility.Visible;
                wp.Visibility = Visibility.Visible;

            }
            else  
            {
                SnapStack.Visibility = Visibility.Visible;
                w8.Visibility = Visibility.Collapsed;
                wp.Visibility = Visibility.Collapsed;


            }
           
        }

    
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
        }

        #endregion
        private void Home_Click(object sender, RoutedEventArgs e)
        {
           
        }

       
        private void xContact_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ContactPage));
        }

        private void xAbout_Click(object sender, RoutedEventArgs e)
        {
            About updatesFlyout = new About();
            updatesFlyout.ShowIndependent();
        }

        private async void wp_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            wpImage.Source = new BitmapImage(new Uri("ms-appx:/Assets/source/wp.png"));
            wpani.Begin();
            await Task.Delay(TimeSpan.FromSeconds(0.4));
            wpani.Pause();
        }

        private void wp_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            wpImage.Source = new BitmapImage(new Uri("ms-appx:/Assets/source/wp shade.png"));
            wpani.Resume();
        }

        private async void w8_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            w8Image.Source = new BitmapImage(new Uri("ms-appx:/Assets/source/w8.png"));
            w8ani.Begin();
            await Task.Delay(TimeSpan.FromSeconds(0.4));
            w8ani.Pause();
        }

        private void w8_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            w8Image.Source = new BitmapImage(new Uri("ms-appx:/Assets/source/w8 shade.png"));
            w8ani.Resume();
        }

        private void wp_Tapped(object sender, TappedRoutedEventArgs e)
        {
            wpnavi.Begin();
        }

        private void w8_Tapped(object sender, TappedRoutedEventArgs e)
        {
            w8navi.Begin();
        }

        private void wpnavi_Completed(object sender, object e)
        {
            Frame.Navigate(typeof(WPApps));
        }

        private void w8navi_Completed(object sender, object e)
        {
            Frame.Navigate(typeof(Win8Apps));
        }
    }
}
