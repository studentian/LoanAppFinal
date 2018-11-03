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
using System.Windows.Shapes;

namespace LoanApplication
{
    /// <summary>
    /// Interaction logic for ClientViews.xaml
    /// </summary>
    public partial class ClientViews : Window
    {
        public ClientViews()
        {
            InitializeComponent();
        }

        private void btnSummarySubmit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Thank you! \nPlease check back in 24 hours. \nReceive quotes by email?", "Client View", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);

            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Close();
        }

        private void btnRepayment_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Saved! \nPlease complete Step 3", "Step.2 Saved!", MessageBoxButton.OK);
        }

        private void btnAffNext_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Saved! \nPlease complete Step 2", "Step.1 Saved!", MessageBoxButton.OK);
        }
    }
}
