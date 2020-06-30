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
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        Timer timer;
        public Page2(Timer timer)
        {
            InitializeComponent();
            this.timer = timer;
            imgExample.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"/graphics/2.png"));
            this.timer.Elapsed += OnTimedEvent;

        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if(Config.Page == 1)
            {
                try
                {
                    Dispatcher.Invoke(new Action(() =>
                    {
                        Config.X = Convert.ToInt32(tbxX.Text);
                        Config.Y = Convert.ToInt32(tbxY.Text);
                    }
                    ));

                    Bitmap bitmap = new Bitmap(100, 100);
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.CopyFromScreen(Config.X, Config.Y, 0, 0, bitmap.Size);
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

        private void tbxX_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

        private void tbxY_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

        private void btnXUp_Click(object sender, RoutedEventArgs e)
        {
            int x = Convert.ToInt32(tbxX.Text);
            x++;
            Config.X = x;
            tbxX.Text = x.ToString();
        }

        private void btnXDown_Click(object sender, RoutedEventArgs e)
        {
            int x = Convert.ToInt32(tbxX.Text);
            if(x>0)
            {
                x--;
                Config.X = x;
                tbxX.Text = x.ToString();
            }
        }

        private void btnYUp_Click(object sender, RoutedEventArgs e)
        {
            int y = Convert.ToInt32(tbxY.Text);
            y++;
            Config.Y = y;
            tbxY.Text = y.ToString();
        }

        private void btnYDown_Click(object sender, RoutedEventArgs e)
        {
            int y = Convert.ToInt32(tbxY.Text);
            if (y > 0)
            {
                y--;
                Config.Y = y;
                tbxY.Text = y.ToString();
            }
        }
    }
}
