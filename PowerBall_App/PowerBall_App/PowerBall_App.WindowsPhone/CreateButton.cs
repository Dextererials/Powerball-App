using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace LotteryApp
{
    class CreateButton
    {
        public static AppBarButton GenerateButton(string number, bool isRed, Dictionary<string, int> dictionary)
        {
            try
            {
                AppBarButton button = new AppBarButton();

                button.Width = (Window.Current.Bounds.Width) / 6;
                FontIcon text = new FontIcon();

                if (isRed == true)
                {
                    button.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
                    button.Background = new SolidColorBrush(Windows.UI.Colors.Red);
                }
                else
                {
                    button.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
                    button.Background = new SolidColorBrush(Windows.UI.Colors.White);
                }

                text.Glyph = number.ToString();
                button.Icon = text;


                return button;
            }
            catch
            {
                AppBarButton b = new AppBarButton();
                return b;
            }


        }
    }
}
