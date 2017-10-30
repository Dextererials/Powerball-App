using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace LotteryApp
{
    class FrequencyGrid
    {
        public static Flyout createGrid(Dictionary<string, int> dictionary, string ball)
        {

            Grid mainGrid = new Grid();
            var rowDef1 = new RowDefinition();
            rowDef1.Height = new GridLength(80, GridUnitType.Pixel);
            var rowDef2 = new RowDefinition();
            mainGrid.RowDefinitions.Add(rowDef1);
            mainGrid.RowDefinitions.Add(rowDef2);

            StackPanel stack = new StackPanel();
            stack.Orientation = Orientation.Vertical;
            Grid.SetRow(stack, 1);

            Flyout flyout = new Flyout();

            Grid titleGrid = new Grid();
            var col1 = new ColumnDefinition();
            var col2 = new ColumnDefinition();
            titleGrid.ColumnDefinitions.Add(col1);
            titleGrid.ColumnDefinitions.Add(col2);
            Grid.SetRow(titleGrid, 0);

            TextBlock Title1Textblock = new TextBlock();
            Border border1 = new Border();
            border1.BorderThickness = new Thickness(1);
            border1.BorderBrush = new SolidColorBrush(Colors.Black);
            border1.Child = Title1Textblock;
            Grid.SetColumn(border1, 0);
            Title1Textblock.TextAlignment = TextAlignment.Center;
            Title1Textblock.Text = "Number";
            Title1Textblock.FontSize = 28;
            Title1Textblock.FontFamily.Equals("Kristen ITC");
            titleGrid.Children.Add(border1);

            TextBlock Title2Textblock = new TextBlock();
            Border border2 = new Border();
            border2.BorderThickness = new Thickness(1);
            border2.BorderBrush = new SolidColorBrush(Colors.Black);
            border2.Child = Title2Textblock;
            Grid.SetColumn(border2, 1);
            Grid.SetColumn(Title2Textblock, 1);
            Title2Textblock.TextAlignment = TextAlignment.Center;
            Title2Textblock.Text = "Frequency";
            Title2Textblock.FontSize = 28;
            Title2Textblock.FontFamily.Equals("Kristen ITC");
            titleGrid.Children.Add(border2);

            mainGrid.Children.Add(titleGrid);



            for (int i = 0; i < dictionary.Count; i++)
            {
                Grid rowGrid = new Grid();
                var column1 = new ColumnDefinition();
                var column2 = new ColumnDefinition();
                rowGrid.ColumnDefinitions.Add(column1);
                rowGrid.ColumnDefinitions.Add(column2);

                TextBlock numberTextblock = new TextBlock();
                Grid.SetColumn(numberTextblock, 0);
                numberTextblock.Text = dictionary.ElementAt(i).Key;
                numberTextblock.FontSize = 28;
                numberTextblock.FontFamily.Equals("Kristen ITC");
                rowGrid.Children.Add(numberTextblock);

                TextBlock frequencyTextblock = new TextBlock();
                Grid.SetColumn(frequencyTextblock, 1);
                frequencyTextblock.Text = dictionary.ElementAt(i).Value.ToString();
                frequencyTextblock.FontSize = 28;
                frequencyTextblock.FontFamily.Equals("Kristen ITC");
                rowGrid.Children.Add(frequencyTextblock);

                stack.Children.Add(rowGrid);

            }

            mainGrid.Children.Add(stack);

            flyout.Content = mainGrid;




            return flyout;

        }
    }
}
