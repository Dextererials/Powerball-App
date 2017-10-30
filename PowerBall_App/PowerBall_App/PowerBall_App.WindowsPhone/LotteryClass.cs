using MathNet.Numerics.Random;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace LotteryApp
{
    class LotteryClass
    {
        public static async void saveFile(string content)
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            var MyFile = await storageFolder.CreateFileAsync("dataSettings", CreationCollisionOption.ReplaceExisting);
            await Windows.Storage.FileIO.WriteTextAsync(MyFile, content);
        }

        public static async Task<string> getFile(bool convert)   // read the file from the localFolder and return a string
        {

            try
            {
                string fileContent;
                StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync("winnums-text.txt");
                using (StreamReader sRead = new StreamReader(await file.OpenStreamForReadAsync()))
                    fileContent = await sRead.ReadToEndAsync();
                if (convert == true)
                {
                    string newString = fileContent.Replace("\r\n", "<br>");
                    fileContent = newString;
                }

                return fileContent;
            }

            catch (FileNotFoundException)
            {
                return string.Empty;
            }

        }

        public static async Task<StackPanel> doCalculation() // calculate frequency and other calculations that are needed
        {
            
            try
            {
                //***************GET STRING FROM FILE ****************************************************
                string fileContent;
                StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync("winnums-text.txt");
                using (StreamReader sRead = new StreamReader(await file.OpenStreamForReadAsync()))
                    fileContent = (string)await sRead.ReadToEndAsync();
                //************************************************************************************************

                //***************READ EACH LINE TO GET FREQUENCY************************************
                Dictionary<string, int> whiteBall = new Dictionary<string, int>();    // used to keep track of the first 5 balls
                Dictionary<string, int> redBall = new Dictionary<string, int>();      // used to keep track of powerball

                StringReader reader = new StringReader(fileContent);
                string line; // create a variable to hold the value of each line

                while ((line = reader.ReadLine()) != null)  // read each line 
                {
                    var store = line.Split(' ').ToList();   // create a list of the numbers and date to sort

                    int count = 0; // used to keep track of what number is there

                    foreach (var individual in store)   // go through the list and decide if it is needed
                    {
                        DateTime test;  // dummy variables to hold test results 
                        int intTest;

                        if (!DateTime.TryParse(individual.ToString(), out test) && int.TryParse(individual.ToString(), out intTest))     // only accept non-dates and ints
                        {
                            if (count >= 0 && count <= 10)  // if the count is between these numbers then they are the first five numbers
                            {
                                if (whiteBall.ContainsKey(individual.ToString()))
                                {
                                    whiteBall[individual.ToString()] += 1;        // add one to the key if it exists
                                }

                                else
                                {
                                    whiteBall.Add(individual.ToString(), 1);      // create a new list in dictionary and add one
                                }

                            }

                            else if (count == 12)   // if its equal to this number then its the powerball
                            {
                                // this is where the powerball will go
                                if (redBall.ContainsKey(individual.ToString()))
                                {
                                    redBall[individual] += 1;
                                }
                                else
                                {
                                    redBall.Add(individual, 1);
                                }

                            }

                            else       //its not a powerball but powerplay
                            {
                                // do nothing as the number is not important
                            }

                        }

                        count++;    // increment count to keep track of which letter we are reading


                    }



                }

                //***************************DONE ORGANIZING THE NUMBERS IN THE DICTIONARY START ALGORITHM TO FIND NUMBERS***********************************


                StackPanel stack = new StackPanel();
                stack.Orientation = Orientation.Horizontal;
                stack.VerticalAlignment = VerticalAlignment.Top;
                stack.HorizontalAlignment = HorizontalAlignment.Center;
                stack.Width = (Window.Current.Bounds.Width);
                stack.Height = (Window.Current.Bounds.Height) / 10.0;
                bool isRed = false;
                List<string> containsNum = new List<string>();

                double[] redBallDataArray = new double[redBall.Count];
                double[] whiteBallDataArray = new double[whiteBall.Count];

                for (int i = 0; i <= 5; i++)  // make sure there is no number like the one before
                {
                    if (i == 5) // have a different function for the sixth powerball. Here we will collect data from the redball array
                    {

                        int count = 0;
                        foreach (KeyValuePair<string, int> indi in redBall)
                        {
                            try
                            {
                                redBallDataArray[count] = double.Parse(indi.Key);
                                count++;
                            }
                            catch (Exception ex)
                            {

                            }

                        }

                        isRed = true;

                        //CREATE A RANDOM NUMBER GEN HERE
                        Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
                        int rand = rnd.Next(0, redBallDataArray.Length - 1);
                        stack.Children.Add(CreateButton.GenerateButton(redBall.ElementAt(rand).Key, isRed, redBall));
                    }

                    else if (i != 5)        // this is where the whitepowerballs will get generated. From here we will collect data from the whiteball array
                    {

                        int count = 0;
                        foreach (KeyValuePair<string, int> indi in whiteBall)
                        {
                            try
                            {
                                whiteBallDataArray[count] = double.Parse(indi.Key);
                                count++;
                            }
                            catch (Exception ex)
                            {

                            }

                        }

                        Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
                        int rand = rnd.Next(0, whiteBallDataArray.Length - 1);

                        if (containsNum.Contains(whiteBall.ElementAt(rand).Key))
                        {
                            while (containsNum.Contains(whiteBall.ElementAt(rand).Key) == true)
                            {
                                rnd = new Random();
                                rand = rnd.Next(0, whiteBallDataArray.Length - 1);
                            }
                        }

                        containsNum.Add(whiteBall.ElementAt(rand).Key);
                        stack.Children.Add(CreateButton.GenerateButton(whiteBall.ElementAt(rand).Key, isRed, whiteBall));
                    }


                }


                FlyoutClass.RedBallDictionary = redBall;  // save the dictionary to the class
                ApplicationData.Current.LocalSettings.Values["RedBall_stdDEV"] = MathNet.Numerics.Statistics.ArrayStatistics.StandardDeviation(redBallDataArray);
                ApplicationData.Current.LocalSettings.Values["RedBall_MAX"] = MathNet.Numerics.Statistics.ArrayStatistics.Maximum(redBallDataArray);
                ApplicationData.Current.LocalSettings.Values["RedBall_MEAN"] = MathNet.Numerics.Statistics.ArrayStatistics.Mean(redBallDataArray);
                ApplicationData.Current.LocalSettings.Values["RedBall_MIN"] = MathNet.Numerics.Statistics.ArrayStatistics.Minimum(redBallDataArray);
                ApplicationData.Current.LocalSettings.Values["RedBall_VARIANCE"] = MathNet.Numerics.Statistics.ArrayStatistics.Variance(redBallDataArray);
                ApplicationData.Current.LocalSettings.Values["RedBall_RTMEANSQR"] = MathNet.Numerics.Statistics.ArrayStatistics.RootMeanSquare(redBallDataArray);
                ApplicationData.Current.LocalSettings.Values["RedBall_MEDIANINPLACE"] = MathNet.Numerics.Statistics.ArrayStatistics.MedianInplace(redBallDataArray);

                FlyoutClass.WhiteBallDictionary = whiteBall;    // save the dictionary to the class
                ApplicationData.Current.LocalSettings.Values["WhiteBall_stdDEV"] = MathNet.Numerics.Statistics.ArrayStatistics.StandardDeviation(whiteBallDataArray);
                ApplicationData.Current.LocalSettings.Values["WhiteBall_MAX"] = MathNet.Numerics.Statistics.ArrayStatistics.Maximum(whiteBallDataArray);
                ApplicationData.Current.LocalSettings.Values["WhiteBall_MEAN"] = MathNet.Numerics.Statistics.ArrayStatistics.Mean(whiteBallDataArray);
                ApplicationData.Current.LocalSettings.Values["WhiteBall_MIN"] = MathNet.Numerics.Statistics.ArrayStatistics.Minimum(whiteBallDataArray);
                ApplicationData.Current.LocalSettings.Values["WhiteBall_VARIANCE"] = MathNet.Numerics.Statistics.ArrayStatistics.Variance(whiteBallDataArray);
                ApplicationData.Current.LocalSettings.Values["WhiteBall_RTMEANSQR"] = MathNet.Numerics.Statistics.ArrayStatistics.RootMeanSquare(whiteBallDataArray);
                ApplicationData.Current.LocalSettings.Values["WhiteBall_MEDIANINPLACE"] = MathNet.Numerics.Statistics.ArrayStatistics.MedianInplace(whiteBallDataArray);

                return stack;
            }
            catch
            {
                var mr = new MessageDialog("Error Generating numbers \n check your internet connection and try again");
                await mr.ShowAsync();
                StackPanel s = new StackPanel();// used to return a void stackpanel if an error occurs
                return s;
            }


        }

    }
}
