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
using Refashion;

namespace Refashion_WPF_UI
{
    /// <summary>
    /// Interaction logic for BagPage.xaml
    /// </summary>
    public partial class BagPage : UserControl
    {
        // Field
        private Seller seller;

        public BagPage(Seller seller)
        {
            InitializeComponent();
            this.seller = seller;
        }

        private void cancelBagBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void saveBagBtn_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
