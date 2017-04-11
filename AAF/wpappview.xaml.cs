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
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace AAF1
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class wpappview : Page
    {

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

      
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        public wpappview()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
        }

        
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

         
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

      

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            Window.Current.SizeChanged += Current_SizeChanged;

            InfoShowUp.DataContext = (AAFWP)e.Parameter;
            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();

            dataTransferManager.DataRequested += new TypedEventHandler<DataTransferManager,

                DataRequestedEventArgs>(this.ShareTextHandler);
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


        private async void ShareTextHandler(DataTransferManager sender, DataRequestedEventArgs e)
        {                         
              bool Connect;
            Connect = IsInternet();
            if (Connect == true)
            {
                DataRequest request = e.Request;

                request.Data.Properties.Title = AppName.Text;
                request.Data.Properties.ApplicationName = AppName.Text;
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

        #endregion

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(WPApps));
        }

        

      

        private void xshare_Tapped(object sender, TappedRoutedEventArgs e)
        {
            DataTransferManager.ShowShareUI();
        }

     
    }
        
    }
