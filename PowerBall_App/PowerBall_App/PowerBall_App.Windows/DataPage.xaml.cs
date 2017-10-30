using LotteryApp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class DataPage : Page
    {
        public DataPage()
        {
            this.InitializeComponent();
            load();
        }

        async private void load()
        {
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey("RedBall_stdDEV") && ApplicationData.Current.LocalSettings.Values.ContainsKey("RedBall_MAX") && ApplicationData.Current.LocalSettings.Values.ContainsKey("RedBall_MEAN") && ApplicationData.Current.LocalSettings.Values.ContainsKey("RedBall_MIN") && ApplicationData.Current.LocalSettings.Values.ContainsKey("RedBall_VARIANCE") && ApplicationData.Current.LocalSettings.Values.ContainsKey("RedBall_RTMEANSQR") && ApplicationData.Current.LocalSettings.Values.ContainsKey("RedBall_MEDIANINPLACE"))
            {
                WstandardDeviationTextBlock.Text = string.Format("{0:0.00}", (string)ApplicationData.Current.LocalSettings.Values["RedBall_stdDEV"].ToString());
                WMaximumNumberTextBlock.Text = string.Format("{0:0.00}", (string)ApplicationData.Current.LocalSettings.Values["RedBall_MAX"].ToString());
                WMeanTextBlock.Text = string.Format("{0:0.00}", (string)ApplicationData.Current.LocalSettings.Values["RedBall_MEAN"].ToString());
                WMinimumNumberTextBlock.Text = string.Format("{0:0.00}", (string)ApplicationData.Current.LocalSettings.Values["RedBall_MIN"].ToString());
                WVarianceTextBlock.Text = string.Format("{0:0.00}", (string)ApplicationData.Current.LocalSettings.Values["RedBall_VARIANCE"].ToString());
                WRootMeanSquareTextBlock.Text = string.Format("{0:0.00}", (string)ApplicationData.Current.LocalSettings.Values["RedBall_RTMEANSQR"].ToString());
                WMedianInPlaceTextBlock.Text = string.Format("{0:0.00}", (string)ApplicationData.Current.LocalSettings.Values["RedBall_MEDIANINPLACE"].ToString());
            }

            if (ApplicationData.Current.LocalSettings.Values.ContainsKey("WhiteBall_stdDEV") && ApplicationData.Current.LocalSettings.Values.ContainsKey("WhiteBall_MAX") && ApplicationData.Current.LocalSettings.Values.ContainsKey("WhiteBall_MEAN") && ApplicationData.Current.LocalSettings.Values.ContainsKey("WhiteBall_MIN") && ApplicationData.Current.LocalSettings.Values.ContainsKey("WhiteBall_VARIANCE") && ApplicationData.Current.LocalSettings.Values.ContainsKey("WhiteBall_RTMEANSQR") && ApplicationData.Current.LocalSettings.Values.ContainsKey("WhiteBall_MEDIANINPLACE"))
            {
                string a = string.Format("{0:0.00}", (string)ApplicationData.Current.LocalSettings.Values["WhiteBall_stdDEV"].ToString());
                RstandardDeviationTextBlock.Text = string.Format("{0:0.00}", (string)ApplicationData.Current.LocalSettings.Values["WhiteBall_stdDEV"].ToString());
                RMaximumNumberTextBlock.Text = string.Format("{0:0.00}", (string)ApplicationData.Current.LocalSettings.Values["WhiteBall_MAX"].ToString());
                RMeanTextBlock.Text = string.Format("{0:0.00}", (string)ApplicationData.Current.LocalSettings.Values["WhiteBall_MEAN"].ToString());
                RMinimumNumberTextBlock.Text = string.Format("{0:0.00}", (string)ApplicationData.Current.LocalSettings.Values["WhiteBall_MIN"].ToString());
                RVarianceTextBlock.Text = string.Format("{0:0.00}", (string)ApplicationData.Current.LocalSettings.Values["WhiteBall_VARIANCE"].ToString());
                RRootMeanSquareTextBlock.Text = string.Format("{0:0.00}", (string)ApplicationData.Current.LocalSettings.Values["WhiteBall_RTMEANSQR"].ToString());
                RMedianInPlaceTextBlock.Text = string.Format("{0:0.00}", (string)ApplicationData.Current.LocalSettings.Values["WhiteBall_MEDIANINPLACE"].ToString());
            }
            else
            {
                var mess = new MessageDialog("Generate Numbers To Collect Data");
                await mess.ShowAsync();
            }
        }

        private async void RedNumber_Clicked(object sender, RoutedEventArgs e)
        {
            if (FlyoutClass.RedBallDictionary != null)
            {
                Dictionary<string, int> r = FlyoutClass.RedBallDictionary;

                Flyout a = FrequencyGrid.createGrid(r, "REDBALL");
                a.Placement = FlyoutPlacementMode.Full;
                a.ShowAt(RedBall_Button);
            }
            else
            {
                var mess = new MessageDialog("Generate Lottery Numbers in the home page to Create Data");
                await mess.ShowAsync();
            }

        }

        private async void WhiteBallButton_Clicked(object sender, RoutedEventArgs e)
        {
            // create flyout to display frequeny table for whiteBalls
            if (FlyoutClass.WhiteBallDictionary != null)
            {
                Dictionary<string, int> r = FlyoutClass.WhiteBallDictionary;
                Flyout a = FrequencyGrid.createGrid(r, "WHITEBALL");
                a.Placement = FlyoutPlacementMode.Full;
                a.ShowAt(WhiteBall_Button);
            }
            else
            {
                var mess = new MessageDialog("Generate Lottery Numbers in the home page to Create Data");
                await mess.ShowAsync();
            }
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            if(Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }
    }
}
