using LotteryApp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Connectivity;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PowerBall_App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            load();
        }

        public async void load()
        {

            ConnectionProfile profile = NetworkInformation.GetInternetConnectionProfile();

            if (profile.GetNetworkConnectivityLevel() >= NetworkConnectivityLevel.InternetAccess)
            {
                if (ApplicationData.Current.LocalSettings.Values.ContainsKey("Downloaded?"))
                {
                    bool dow = (bool)ApplicationData.Current.LocalSettings.Values["Downloaded?"];
                    if (dow == false)
                    {
                        try
                        {
                            await Download();
                            VideoAd.play();
                        }
                        catch
                        {

                        }

                    }

                }
                else
                {
                    try
                    {
                        await Download();
                        VideoAd.play();
                    }
                    catch
                    {

                    }

                }
            }
            else
            {

                var mess = new MessageDialog("Unable to Collect date from website\n Connect to the internet and try again");
                await mess.ShowAsync();


            }
        }


        private async Task Download()         // download the file and save the file name as winnums-text.txt
        {
            HttpClient client = new HttpClient();

            HttpResponseMessage message = await client.GetAsync("http://www.powerball.com/powerball/winnums-text.txt");

            StorageFolder myfolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            StorageFile sampleFile = await myfolder.CreateFileAsync("winnums-text.txt", CreationCollisionOption.ReplaceExisting);
            byte[] file = await message.Content.ReadAsByteArrayAsync();

            await FileIO.WriteBytesAsync(sampleFile, file);
            client.Dispose();

            ApplicationData.Current.LocalSettings.Values["Downloaded?"] = true;

        }

        

        private async void NewClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                numberStackPanel.Children.Add(await LotteryClass.doCalculation());
            }
            catch
            {

            }



        }





        private void data_Page_Cliked(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(DataPage));
        }

        private void clear_clicked(object sender, RoutedEventArgs e)
        {
            numberStackPanel.Children.Clear();
        }

        private void view_PastNumbers_Clicked(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ViewPastNumbers));
        }

    }
}
