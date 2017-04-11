using AAF1.Common;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace AAF1
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class WPApps : Page
    {
        private MobileServiceCollection<AAFWP, AAFWP> items;
        private IMobileServiceTable<AAFWP> todoTable = App.MobileService.GetTable<AAFWP>();
        public static MobileServiceCollection<top_apps, top_apps> top_apps;
        private IMobileServiceTable<top_apps> TopAppsTable = App.MobileService.GetTable<top_apps>();

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
   
        
      
        public WPApps()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
      
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
        }
        
            private async void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
             bool Connect;
              Connect = IsInternet();
              if (Connect == true)
              {
                  RefreshTodoItems();
                  applist.Visibility = Visibility.Visible;

              }

            else
              {

                  await new MessageDialog("No Internet Connection , please check your connection and try again ", "Academic AppFactory").ShowAsync();
             
               

              }
              //DataFill df = new DataFill();
              //df.WpData();
              //Topapplist.ItemsSource = df.Data;      

        }
        #region Azure
        public static bool IsInternet()
        {
            ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
            bool internet = connections != null && connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;
            return internet;
        }


        private async void RefreshTodoItems()
        {
            MobileServiceInvalidOperationException exception = null;



            try
            {
                 bool Connect;
              Connect = IsInternet();
              if (Connect == true)
              {
                  items = await todoTable.Where(todoItem => todoItem.Complete == false).ToCollectionAsync();
                  top_apps = await TopAppsTable.Where(todoItem => todoItem.topapps == 1).ToCollectionAsync();

                  prProgress.IsActive = false;
                  prProgress.Visibility = Visibility.Collapsed;
              }
              else
              {

                  await new MessageDialog("No Internet Connection , please check your connection and try again ", "Academic AppFactory").ShowAsync();



              }
            }



            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
            }

            if (exception != null)
            {
                await new MessageDialog(exception.Message, "Internet Problem").ShowAsync();
            }
            else
            {

                applist.ItemsSource = items;
                Data.Source = items;

                Topapplist.ItemsSource = top_apps;
                TopAppData.Source = top_apps;
            }
        }

        #endregion
        #region AppBar
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(HomePage));
        }

      

        private void xContact_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ContactPage));
        }

        private void xAbout_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(About));
        }
        #endregion


        #region Snap 
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

        #endregion
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            Window.Current.SizeChanged += Current_SizeChanged;

            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion
 
       
     
        private async  void xtopapps_Tapped(object sender, TappedRoutedEventArgs e)
        {
            xtopapps.Source = new BitmapImage(new Uri("ms-appx:/Assets/apps/top apps clicked.png"));
            xallapps.Source = new BitmapImage(new Uri("ms-appx:/Assets/apps/all apps.png"));
            applist.Visibility = Visibility.Collapsed;
            Topapplist.Visibility = Visibility.Visible;
            RefreshTodoItems();
            prProgress.Visibility = Visibility.Collapsed;

         
        }

        public static bool TeamMemberClickedFromMainPage;

       
        private void pageTitle_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
        private void applist_ItemClick(object sender, ItemClickEventArgs e)
        {
            PublicId = applist.SelectedIndex;

            this.Frame.Navigate(typeof(wpappview), e.ClickedItem);
        }
        
    

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(HomePage));
        }
        public static int PublicId;

        private void xallapps_Tapped(object sender, TappedRoutedEventArgs e)
        {
            xtopapps.Source = new BitmapImage(new Uri("ms-appx:/Assets/apps/top apps.png"));
            xallapps.Source = new BitmapImage(new Uri("ms-appx:/Assets/apps/all apps clicked.png"));
            RefreshTodoItems();
            applist.Visibility = Visibility.Visible;
            Topapplist.Visibility = Visibility.Collapsed;

           
        }

        private void Topapplist_ItemClick(object sender, ItemClickEventArgs e)
        {

            PublicId = Topapplist.SelectedIndex;
            this.Frame.Navigate(typeof(TopWin8), e.ClickedItem);


           
        }
    }
}
