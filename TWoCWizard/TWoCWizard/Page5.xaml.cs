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
    /// Interaction logic for Page5.xaml
    /// </summary>
    public partial class Page5 : Page
    {
        Timer timer;
        public Page5(Timer timer)
        {
            InitializeComponent();
            imgExample.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"/graphics/5.png"));
            this.timer = timer;
            this.timer.Elapsed += OnTimedEvent;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (Config.Page == 4)
            {
                try
                {
                    Dispatcher.Invoke(new Action(() =>
                    {
                        Config.W1 = Convert.ToInt32(tbxX.Text);
                        Config.H1 = Convert.ToInt32(tbxY.Text);
                    }
                    ));

                    Bitmap bitmap = new Bitmap(Config.W1, Config.H1);
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.CopyFromScreen(Config.X, Config.Y, 0, 0, bitmap.Size);
                    }

                    Dispatcher.Invoke(new Action(() =>
                    {
                        imgPreview.Source = BMPExtend.BitmapToImageSource(new Bitmap(bitmap, new System.Drawing.Size(100,100)));
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
            Config.W1 = x;
            tbxX.Text = x.ToString();
        }

        private void btnXDown_Click(object sender, RoutedEventArgs e)
        {
            int x = Convert.ToInt32(tbxX.Text);
            if (x > 0)
            {
                x--;
                Config.W1 = x;
                tbxX.Text = x.ToString();
            }
        }

        private void btnYUp_Click(object sender, RoutedEventArgs e)
        {
            int y = Convert.ToInt32(tbxY.Text);
            y++;
            Config.H1 = y;
            tbxY.Text = y.ToString();
        }

        private void btnYDown_Click(object sender, RoutedEventArgs e)
        {
            int y = Convert.ToInt32(tbxY.Text);
            if (y > 0)
            {
                y--;
                Config.H1 = y;
                tbxY.Text = y.ToString();
            }
        }
    }
}
