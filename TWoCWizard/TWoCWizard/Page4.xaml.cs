using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
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
    /// Interaction logic for Page4.xaml
    /// </summary>
    public partial class Page4 : Page
    {
        Timer timer;
        int localy;
        public Page4(Timer timer)
        {
            InitializeComponent();
            imgExample.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"/graphics/4.png"));
            this.timer = timer;
            this.timer.Elapsed += OnTimedEvent;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (Config.Page == 3)
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    switch(Config.Mode)
                    {
                        case 1:
                            labelPS2.Visibility = Visibility.Visible;
                            labelXBOX.Visibility = Visibility.Hidden;
                            return;
                        case 2:
                            labelPS2.Visibility = Visibility.Hidden;
                            labelXBOX.Visibility = Visibility.Visible;
                            return;
                    }
                }));
                try
                {
                    Dispatcher.Invoke(new Action(() =>
                    {
                        localy = Convert.ToInt32(tbxY.Text);
                        Config.H = localy + 100;
                    }
                    ));

                    Bitmap bitmap = new Bitmap(100, 100);
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.CopyFromScreen(Config.X + Config.W - 100, Config.Y + Config.H - 100, 0, 0, bitmap.Size);
                    }

                    Dispatcher.Invoke(new Action(() =>
                    {
                        imgPreview.Source = BMPExtend.BitmapToImageSource(bitmap);
                    }
                    ));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void tbxY_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

        private void btnYUp_Click(object sender, RoutedEventArgs e)
        {
            int y = Convert.ToInt32(tbxY.Text);
            y++;
            localy = y;
            Config.H = localy + 100;
            tbxY.Text = y.ToString();
        }

        private void btnYDown_Click(object sender, RoutedEventArgs e)
        {
            int y = Convert.ToInt32(tbxY.Text);
            if (y > 0)
            {
                y--;
                localy = y;
                Config.H = localy + 100;
                tbxY.Text = y.ToString();
            }
        }
    }
}
