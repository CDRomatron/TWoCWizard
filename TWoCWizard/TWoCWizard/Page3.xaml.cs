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
    /// Interaction logic for Page3.xaml
    /// </summary>
    public partial class Page3 : Page
    {
        Timer timer;
        int localx;
        public Page3(Timer timer)
        {
            InitializeComponent();
            imgExample.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"/graphics/3.png"));
            this.timer = timer;
            this.timer.Elapsed += OnTimedEvent;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (Config.Page == 2)
            {
                try
                {
                    Dispatcher.Invoke(new Action(() =>
                    {
                        localx = Convert.ToInt32(tbxX.Text);
                        Config.W = localx + 100;
                    }
                    ));

                    Bitmap bitmap = new Bitmap(100, 100);
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.CopyFromScreen(Config.X + localx, Config.Y, 0, 0, bitmap.Size);
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

        private void btnXUp_Click(object sender, RoutedEventArgs e)
        {
            int x = Convert.ToInt32(tbxX.Text);
            x++;
            localx = x;
            Config.W = localx + 100;
            tbxX.Text = x.ToString();
        }

        private void btnXDown_Click(object sender, RoutedEventArgs e)
        {
            int x = Convert.ToInt32(tbxX.Text);
            if (x > 0)
            {
                x--;
                localx = x;
                Config.W = localx + 100;
                tbxX.Text = x.ToString();
            }
        }
    }
}
