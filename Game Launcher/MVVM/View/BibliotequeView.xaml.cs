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

        private void GameStackPannel_Initialized(object sender, EventArgs e)
        {
            int numGames = 60;

            int fullLine = numGames / 7;
            int numLastLine = numGames % 7;
            int lines = (int)Math.Ceiling(numGames / 7f);

            StackPanel[] childStackPanels = new StackPanel[lines];

            Trace.WriteLine(lines);
            Trace.WriteLine(fullLine);

            StackPanel stackPanelParent = new StackPanel();

            for (int a = 0; a < lines; a++)
            {
                childStackPanels[a] = new StackPanel();
                childStackPanels[a].Orientation = Orientation.Horizontal;
                childStackPanels[a].HorizontalAlignment = HorizontalAlignment.Center;
            }

            for (int b = 0; b < fullLine; b++)
            {

                for (int i = 0; i < 7; i++)
                {
                    Rectangle rectangle = new Rectangle();
                    rectangle.Width = 150;
                    rectangle.Height = 220;
                    rectangle.Fill = Brushes.Aqua;
                    rectangle.RadiusX = 10;
                    rectangle.RadiusY = 10;
                    rectangle.VerticalAlignment = VerticalAlignment.Top;
                    rectangle.Margin = new Thickness(10, 10, 0, 0);
                        if (i == 0)
                            rectangle.Margin = new Thickness(0, 10, 0, 0);

                    childStackPanels[b].Children.Add(rectangle);
                }

                
            }

            for (int c = 0; c < numLastLine; c++)
            {
                Rectangle rectangle = new Rectangle();
                rectangle.Width = 150;
                rectangle.Height = 220;
                rectangle.Fill = Brushes.Aqua;
                rectangle.RadiusX = 10;
                rectangle.RadiusY = 10;
                rectangle.VerticalAlignment = VerticalAlignment.Top;
                rectangle.Margin = new Thickness(10, 10, 0, 0);
                if (c == 0)
                    rectangle.Margin = new Thickness(0, 10, 0, 0);

                childStackPanels[lines - 1].HorizontalAlignment = HorizontalAlignment.Left;
                childStackPanels[lines - 1].Children.Add(rectangle);
            }

            stackPanelParent.Children.Add(childStackPanels[0]);
            stackPanelParent.Children.Add(childStackPanels[1]);
            stackPanelParent.Children.Add(childStackPanels[2]);
            stackPanelParent.Children.Add(childStackPanels[3]);


        }

        private void Grid_Initialized(object sender, EventArgs e)
        {
            // stats
            //-------------------------//
            int numGames = 60;
            int fullLine = numGames / 7;
            int numLastLine = numGames % 7;
            int lines = (int)Math.Ceiling(numGames / 7f);
            //-------------------------//

            ScrollViewer gamesScrollViewer = new();
            gamesScrollViewer.CanContentScroll = false;

            StackPanel MainStackPanel = new();

            // add items to full lines
            //-------------------------//
            for (int i = 0; i < fullLine; i++)
            {
                // child stackPannel
                StackPanel stackPanel = new();
                stackPanel.Orientation = Orientation.Horizontal;

                // add 7 items to each lines into stackPannel
                for(int y = 0; y < 7; y++)
                {
                    stackPanel.Children.Add(CreateRectangle(y));
                }
                // add each child stackPannel -> main stackPannel
                MainStackPanel.Children.Add(stackPanel);
            }
            //-------------------------//

            // add items to last lines if too much
            //-------------------------//
            if(numLastLine > 0)
            {
                StackPanel lastLineStackPannel = new();
                lastLineStackPannel.Orientation = Orientation.Horizontal;

                for (int y = 0; y < numLastLine; y++)
                {
                    lastLineStackPannel.Children.Add(CreateRectangle(y));
                }

                // add child stackPannel -> main stackPannel
                MainStackPanel.Children.Add(lastLineStackPannel);
            }
            //-------------------------//

            // add the main stackPannel into scrollViewer content
            gamesScrollViewer.Content = MainStackPanel;

            // add scroll Viewer into Grid Content
            this.Content = gamesScrollViewer;

        }

        private Rectangle CreateRectangle(int index = 1)
        {
            Rectangle rect = new();
            rect.Margin = new Thickness(10, 10, 0, 0);

            if (index == 0)
            {
                rect.Margin = new Thickness(0, 10, 0, 0);
            }
            rect.Width = 150;
            rect.Height = 220;
            rect.Fill = Brushes.Aqua;

            return rect;
        }
    }
}
