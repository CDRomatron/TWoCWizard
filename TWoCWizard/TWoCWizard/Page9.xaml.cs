﻿using System;
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
    /// Interaction logic for Page9.xaml
    /// </summary>
    public partial class Page9 : Page
    {
        Timer timer;
        public Page9(Timer timer)
        {
            InitializeComponent();
            imgExample.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"/graphics/7.png"));
            this.timer = timer;
            this.timer.Elapsed += OnTimedEvent;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (Config.Page == 8)
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    switch (Config.Mode)
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
                        Config.H2 = Convert.ToInt32(tbxX.Text);
                    }
                    ));

                    Bitmap bitmap = new Bitmap(Config.W2, Config.H2);
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.CopyFromScreen(Config.X + Config.W - Config.W2, Config.Y+Config.H-Config.H2, 0, 0, bitmap.Size);
                    }

                    Dispatcher.Invoke(new Action(() =>
                    {
                        imgPreview.Source = BMPExtend.BitmapToImageSource(new Bitmap(bitmap, new System.Drawing.Size(100, 100)));
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
            Config.H2 = x;
            tbxX.Text = x.ToString();
        }

        private void btnXDown_Click(object sender, RoutedEventArgs e)
        {
            int x = Convert.ToInt32(tbxX.Text);
            if (x > 0)
            {
                x--;
                Config.H2 = x;
                tbxX.Text = x.ToString();
            }
        }
    }
}
