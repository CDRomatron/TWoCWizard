using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace TWoCWizard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Page> pages;
        List<System.Timers.Timer> timers;

        /*
         * Page 1 - Console
         * Page 2 - Line X Y
         * Page 3 - W
         * Page 4 - H
         * Page 5 - W1 H1
         * Page 6 - Wumpa Screenshot & Threshold
         * Page 7 - W2
         * Page 8 - Loading Screenshot & Threshold
         * Page 9 - H2
         * Page 10 - Midload Screenshot & Threshold
         * Page 11 - Blackscreen Screenshot & Threshold
         */
        public MainWindow()
        {
            InitializeComponent();

            timers = new List<System.Timers.Timer>();

            for(int i = 0; i < 12; i++)
            {
                timers.Add(new System.Timers.Timer(1000 / 60));
            }

            pages = new List<Page>();
            pages.Add(new Page1());
            pages.Add(new Page2(timers[1]));
            pages.Add(new Page3(timers[2]));
            pages.Add(new Page4(timers[3]));
            pages.Add(new Page5(timers[4]));
            pages.Add(new Page6(timers[5]));
            pages.Add(new Page7(timers[6]));
            pages.Add(new Page8(timers[7]));
            pages.Add(new Page9(timers[8]));
            pages.Add(new Page10(timers[9]));
            pages.Add(new Page11(timers[10]));
            pages.Add(new Page12());

            frame.Content = pages[Config.Page];
            btnBack.IsEnabled = false;

            Config.Setup();
        }

        private void btnForward_Click(object sender, RoutedEventArgs e)
        {
            if(Config.Mode != 0)
            {
                Config.Page++;
                frame.Content = pages[Config.Page];
                if (Config.Page == pages.Count - 1)
                {
                    btnForward.IsEnabled = false;
                }
                btnBack.IsEnabled = true;

                for(int i = 0; i < timers.Count-1; i++)
                {
                    if(i == Config.Page)
                    {
                        timers[i].Start();
                    }
                    else
                    {
                        timers[i].Stop();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a console to continue");
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Config.Page--;
            frame.Content = pages[Config.Page];
            if (Config.Page == 0)
            {
                btnBack.IsEnabled = false;
            }
            btnForward.IsEnabled = true;
        }
    }
}
