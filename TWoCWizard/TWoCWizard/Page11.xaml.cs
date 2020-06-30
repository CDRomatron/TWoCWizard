using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TWoCWizard
{
    /// <summary>
    /// Interaction logic for Page11.xaml
    /// </summary>
    public partial class Page11 : Page
    {
        Timer timer;
        Bitmap image;
        bool screenshot;
        public Page11(Timer timer)
        {
            InitializeComponent();
            image = new Bitmap(100, 100);
            screenshot = false;
            this.timer = timer;
            this.timer.Interval = 1000 / 240;
            this.timer.Elapsed += OnTimedEvent;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (Config.Page == 10)
            {
                try
                {
                    Bitmap bitmap = new Bitmap(Config.W2, Config.H2);
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.CopyFromScreen(Config.X + Config.W - Config.W2, Config.Y + Config.H - Config.H2, 0, 0, bitmap.Size);
                    }

                    if (screenshot)
                    {
                        image = new Bitmap(bitmap);
                        Config.black = new Bitmap(bitmap);
                        screenshot = false;
                    }


                    Dispatcher.Invoke(new Action(() =>
                    {
                        imgPreview.Source = BMPExtend.BitmapToImageSource(new Bitmap(bitmap, new System.Drawing.Size(100, 100)));
                        imgScreenshot.Source = BMPExtend.BitmapToImageSource(new Bitmap(image, new System.Drawing.Size(100, 100)));
                    }
                    ));

                    if (bitmap.Size == image.Size)
                    {
                        Dispatcher.Invoke(new Action(() =>
                        {
                            int value = Convert.ToInt32((compare(new Bitmap(bitmap, new System.Drawing.Size(bitmap.Width / 2, bitmap.Height / 2)), new Bitmap(image, new System.Drawing.Size(image.Width / 2, image.Height / 2))) + 1) / 10000000);
                            Config.tblack = Convert.ToInt32(tbxThreshold.Text);
                            tbxValue.Text = value.ToString();
                            cbxWumpa.IsChecked = (value < Convert.ToInt32(tbxThreshold.Text));
                        }));
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private float compare(Bitmap bmp1, Bitmap bmp2)
        {
            List<Color> im1 = new List<Color>(HistoGram(bmp1));
            List<Color> im2 = new List<Color>(HistoGram(bmp2));

            float val = 0;
            for (int i = 0; i < im1.Count; i++)
            {
                val += Math.Abs((float)(im1.ElementAt(i).ToArgb() - im2.ElementAt(i).ToArgb()));
            }

            return val;
        }

        private List<Color> HistoGram(Bitmap bitmap)
        {
            Bitmap newbitmap = (Bitmap)bitmap.Clone();
            // Store the histogram in a dictionary          
            List<Color> histo = new List<Color>();
            for (int x = 0; x < newbitmap.Width; x++)
            {
                for (int y = 0; y < newbitmap.Height; y++)
                {
                    // Get pixel color 
                    Color c = newbitmap.GetPixel(x, y);
                    // If it exists in our 'histogram' increment the corresponding value, or add new
                    histo.Add(c);
                }
            }
            return histo;
        }

        private void btnXUp_Click(object sender, RoutedEventArgs e)
        {
            int x = Convert.ToInt32(tbxThreshold.Text);
            x++;
            Config.tblack = x;
            tbxThreshold.Text = x.ToString();
        }

        private void btnXDown_Click(object sender, RoutedEventArgs e)
        {
            int x = Convert.ToInt32(tbxThreshold.Text);
            if (x > 0)
            {
                x--;
                Config.tblack = 0;
                tbxThreshold.Text = x.ToString();
            }
        }

        private void btnScreenshot_Click(object sender, RoutedEventArgs e)
        {
            screenshot = true;
        }
    }
}
