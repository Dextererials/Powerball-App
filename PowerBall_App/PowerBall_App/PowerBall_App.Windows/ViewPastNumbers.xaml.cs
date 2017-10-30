using LotteryApp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class ViewPastNumbers : Page
    {
        public ViewPastNumbers()
        {
            this.InitializeComponent();
            Load();
        }

        async  private void Load()
        {
            double width = (Window.Current.Bounds.Width) / 150;
            try
            {

                string test = await LotteryClass.getFile(true);
                if (test != string.Empty)
                {
                    web.NavigateToString(@"<font color=""white"" size =" + width + ">" + test + "</font>"); // change the text color to white
                }
                else
                {
                    web.NavigateToString(@"<font color=""white"" size =" + width + ">" + "Error Loading File <br> Connect to the internet and try again" + "</font>");
                }

            }
            catch (Exception ex)
            {
                web.NavigateToString(@"<font color=""white"" size =" + width + ">" + "Error Loading File <br> Connect to the internet and try again" + "</font>");
            }

        }

        private void go_Back_Clicked(object sender, RoutedEventArgs e)
        {
            if(Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }
    }
}
