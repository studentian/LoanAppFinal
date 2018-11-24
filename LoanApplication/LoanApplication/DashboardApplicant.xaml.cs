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
    /// Interaction logic for DashboardApplicant.xaml
    /// </summary>
    public partial class DashboardApplicant : Window
    {
        public DashboardApplicant()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }

        private void btnClient_Click(object sender, RoutedEventArgs e)
        {
            Offers offers = new Offers();
            frmMain.Navigate(offers);

        }
    }
}
