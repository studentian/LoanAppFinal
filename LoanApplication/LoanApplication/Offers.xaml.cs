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

namespace LoanApplication
{
    /// <summary>
    /// Interaction logic for Offers.xaml
    /// </summary>
    public partial class Offers : Page
    {
        public Offers()
        {
            InitializeComponent();
        }

        private void btnOffers_Click(object sender, RoutedEventArgs e)
        {
            ClientViews clientViews = new ClientViews();
            clientViews.Show();
           
        }
    }
}
