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

namespace TWoCWizard
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void cbxxbox_Checked(object sender, RoutedEventArgs e)
        {
            Config.Mode = 2;
            cbxps2.IsChecked = false;
        }

        private void cbxps2_Checked(object sender, RoutedEventArgs e)
        {
            Config.Mode = 1;
            cbxxbox.IsChecked = false;
        }

        private void cbxxbox_Unchecked(object sender, RoutedEventArgs e)
        {
            Config.Mode = 0;
            if(cbxps2.IsChecked == true)
            {
                Config.Mode = 1;
            }
        }

        private void cbxps2_Unchecked(object sender, RoutedEventArgs e)
        {
            Config.Mode = 0;
            if (cbxxbox.IsChecked == true)
            {
                Config.Mode = 2;
            }
        }
    }
}
