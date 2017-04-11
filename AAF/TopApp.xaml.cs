using AAF1.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Connectivity;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Core;
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
    public sealed partial class TopApp : Page
    {

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        public TopApp()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
        }

        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private async void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            //LoadPageData();
        }

        //public void LoadPageData()
        //{
        //    AppName.Text = Win8Apps.top_apps[Win8Apps.PublicId].topappname;
        //    DevName.Text = Win8Apps.top_apps[Win8Apps.PublicId].topdevelopername;
        //    AppDesc.Text = Win8Apps.top_apps[Win8Apps.PublicId].topappdescription;
        //    AppCat.Text = Win8Apps.top_apps[Win8Apps.PublicId].topappsection;
        //    AppType.Text = Win8Apps.top_apps[Win8Apps.PublicId].topapptype;
        //    applink.Text = Win8Apps.top_apps[Win8Apps.PublicId].topapplink;




        //    Uri myUri = new Uri(Win8Apps.top_apps[Win8Apps.PublicId].topapplogo, UriKind.Absolute);
        //    BitmapImage bmi = new BitmapImage();
        //    bmi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
        //    bmi.UriSource = myUri;
        //    InfoImage.Source = bmi;

        //}
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }


      
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            InfoShowUp.DataContext = (top_apps)e.Parameter;

            Window.Current.SizeChanged += Current_SizeChanged;
            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();

            dataTransferManager.DataRequested += new TypedEventHandler<DataTransferManager,

                DataRequestedEventArgs>(this.ShareTextHandler);

        }

        private async void ShareTextHandler(DataTransferManager sender, DataRequestedEventArgs e)
        {

            bool Connect;
            Connect = IsInternet();
            if (Connect == true)
            {
                DataRequest request = e.Request;

                request.Data.Properties.Title = AppName.Text;

                request.Data.Properties.Description = AppName.Text;


                request.Data.SetText(AppDesc.Text + "                             " + applink.Text);
            }
            else
            {
                await new MessageDialog("No Internet Connection , please check your connection and try again ", "Academic AppFactory").ShowAsync();

            }
        }

        public static bool IsInternet()
        {
            ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
            bool internet = connections != null && connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;
            return internet;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }
        private void Current_SizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            Snap();
        }

        private void Snap()
        {
            //   ApplicationView current = ApplicationView.GetForCurrentView;
            ApplicationView current = ApplicationView.GetForCurrentView();
            if (current.IsFullScreen)
            {
                SnapStack.Visibility = Visibility.Collapsed;

            }
            else
            {
                SnapStack.Visibility = Visibility.Visible;



            }

        }



        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Win8Apps));
            
        }

        private void shre_Click(object sender, RoutedEventArgs e)
        {
            DataTransferManager.ShowShareUI();

        }






    
      

    }
}
