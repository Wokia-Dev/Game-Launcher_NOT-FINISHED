using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using Microsoft.Azure.Cosmos;
using System.Net;
using Newtonsoft.Json;
using Game_Launcher.MVVM.ViewModel;
using System.Threading;

namespace Game_Launcher.MVVM.View
{
    /// <summary>
    /// Logique d'interaction pour BibliotequeView.xaml
    /// </summary>
    public partial class BibliotequeView : UserControl
    {
        public BibliotequeView()
        {
            InitializeComponent();
        }


        private void Grid_Initialized(object sender, EventArgs e)
        {
            //this.Content = DisplayGames();
        }

        // Create rectangle for a game
        private Rectangle CreateRectangle(string cover_image, string name, int index = 1)
        {
            // Rectangle property
            //-------------------------//
            Rectangle rect = new();
            rect.Name = name;
            rect.Margin = new Thickness(10, 10, 0, 0);

            if (index == 0)
            {
                rect.Margin = new Thickness(0, 10, 0, 0);
            }
            rect.Width = 150;
            rect.Height = 220;
            rect.RadiusX = 10;
            rect.RadiusY = 10;
            rect.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(cover_image))
            };
            //-------------------------//

            // Click Event
            rect.MouseLeftButtonDown += new MouseButtonEventHandler(rect_click);
            return rect;
        }

        //when any game is clicked
        private void rect_click(object sender, RoutedEventArgs e)
        {
            //change content
            StackPanel mainStackPannel = new();

            Image goBackImage = new();
            goBackImage.Source = new BitmapImage(new Uri("\\Ressources\\Images\\Icons\\goBack.png", UriKind.Relative));

            Button goBackButton = new();
            goBackButton.Height = 25;
            goBackButton.Width = 25;
            goBackButton.Background = Brushes.Transparent;
            goBackButton.BorderThickness = new Thickness(0);
            goBackButton.VerticalAlignment = VerticalAlignment.Top;
            goBackButton.HorizontalAlignment = HorizontalAlignment.Left;
            goBackButton.Margin = new Thickness(20, 20, 0, 0);
            goBackButton.Content = goBackImage;
            goBackButton.Click += returnClick;

            this.Content = goBackButton;

            
        }

        private void returnClick(object sender, RoutedEventArgs e)
        {
            Trace.WriteLine("ze");
            this.Content = DisplayGames();
        }


        private object DisplayGames()
        {
            if (!File.Exists("db.json"))
            {
                Trace.WriteLine("no DB File");
                Application.Current.Shutdown();
            }

            string json = File.ReadAllText("db.json");
            dynamic db = JsonConvert.DeserializeObject(json);


            // stats
            //-------------------------//
            int numGames = db.num_games;
            int fullLine = numGames / 7;
            int numLastLine = numGames % 7;
            int lines = (int)Math.Ceiling(numGames / 7f);
            //-------------------------//

            ScrollViewer gamesScrollViewer = new();
            gamesScrollViewer.CanContentScroll = false;

            StackPanel MainStackPanel = new();

            int global = 0;
            // add items to full lines
            //-------------------------//
            for (int i = 0; i < fullLine; i++)
            {
                // child stackPannel
                StackPanel stackPanel = new();
                stackPanel.Orientation = Orientation.Horizontal;

                // add 7 items to each lines into stackPannel
                for (int y = 0; y < 7; y++)
                {
                    string coverImage = db.Games_infos[global].cover_image;
                    string name = db.Games_infos[global].name;
                    global++;
                    stackPanel.Children.Add(CreateRectangle(coverImage, name.Replace(" ", "_"), y));
                }
                // add each child stackPannel -> main stackPannel
                MainStackPanel.Children.Add(stackPanel);
            }
            //-------------------------//

            // add items to last lines if too much
            //-------------------------//
            if (numLastLine > 0)
            {
                StackPanel lastLineStackPannel = new();
                lastLineStackPannel.Orientation = Orientation.Horizontal;

                for (int y = 0; y < numLastLine; y++)
                {
                    string coverImage = db.Games_infos[global].cover_image;
                    string name = db.Games_infos[global].name;
                    global++;
                    lastLineStackPannel.Children.Add(CreateRectangle(coverImage, name.Replace(" ", "_").Replace("’", ""), y));
                }

                // add child stackPannel -> main stackPannel
                MainStackPanel.Children.Add(lastLineStackPannel);
            }
            //-------------------------//

            // add the main stackPannel into scrollViewer content
            gamesScrollViewer.Content = MainStackPanel;

            // add scroll Viewer into Grid Content
            return gamesScrollViewer;


        }

    }
}
