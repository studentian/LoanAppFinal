using LoanAppLibrary2;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        loanappdbEntities db = new loanappdbEntities();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnProviderRegister_Click(object sender, RoutedEventArgs e)
        {
            FinancialServicesRegistration providerReg = new FinancialServicesRegistration();
            providerReg.Show();
            this.Close();
        }

        private void btnClientRegister_Click(object sender, RoutedEventArgs e)
        {
            ClientRegistration clientReg = new ClientRegistration();
            clientReg.Show();
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

            string currentUser = tbxUserName.Text;
            string currentPassword = pbxPassword.Password;
            foreach (var userRecord in db.Users.Where(t => t.Username == currentUser && t.Password == currentPassword))
            {


                MessageBox.Show("Good day, thank you for visiting!", "Login confirmed!", MessageBoxButton.OK);

                Dashboard dashboard = new Dashboard();
                dashboard.Show();

            }
            MessageBox.Show("Ooops!", "Username or Password incorrect. \nPlease try again!", MessageBoxButton.OK);


        }
    }
}
