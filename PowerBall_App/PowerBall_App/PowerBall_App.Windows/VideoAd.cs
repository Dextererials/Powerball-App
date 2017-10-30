using Microsoft.Advertising.WinRT.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryApp
{

    class VideoAd
    {

        public static void play()
        {
            var MyAppId = "6a39a90d-e6b3-49cf-ab97-07211d1abe1c";
            var MyAdUnitId = "11571343";

            Microsoft.Advertising.WinRT.UI.InterstitialAd MyVideoAd = new Microsoft.Advertising.WinRT.UI.InterstitialAd();
            MyVideoAd.RequestAd(AdType.Video, MyAppId, MyAdUnitId);

            MyVideoAd.AdReady += (sender, args) =>
            {
                if (MyVideoAd.State == InterstitialAdState.Ready)
                {
                    MyVideoAd.Show();
                }
            };

            MyVideoAd.ErrorOccurred += (s, a) =>
            {
                MyVideoAd.RequestAd(AdType.Video, MyAppId, MyAdUnitId);
            };
            MyVideoAd.Completed += (se, ar) =>
            {
                //do nothing
            };
            MyVideoAd.Cancelled += (saa, de) =>
            {
                // do nothing
            };

        }


    }
}
