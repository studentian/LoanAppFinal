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
    /// Interaction logic for FinancialServicesRegistration.xaml
    /// </summary>
    public partial class FinancialServicesRegistration : Window
    {
        public FinancialServicesRegistration()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Welcome! \nRegistration Confirmed!", "Thank you for registering!", MessageBoxButton.OK);

            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Close();
        }

        private void btnback_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            this.Close();
        }
    }
}
