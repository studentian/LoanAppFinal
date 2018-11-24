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
            Environment.Exit(0);

        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string currentUser = tbxUserName.Text;
            string currentPassword = pbxPassword.Password;
            foreach (var userRecord in db.Users)
            {
                if (userRecord.Username == currentUser && userRecord.Password == currentPassword)
                {
                    MessageBox.Show("Good day, thank you for visiting!", "Login confirmed!", MessageBoxButton.OK);

                    if (userRecord.LevelId == 3)
                    {
                        Dashboard dashboard = new Dashboard();
                        dashboard.Owner = this;
                        dashboard.ShowDialog(); //unlike dashboard.Show(), the showdialog opens the dashboard and does not allow flicking between windows. 
                        this.Hide();

                        dashboard.Uid = currentUser;
                    }
                    else if (userRecord.LevelId == 1)
                    {
                        DashboardApplicant dashboardApplicant = new DashboardApplicant();
                        dashboardApplicant.Owner = this;
                        dashboardApplicant.ShowDialog(); //unlike dashboard.Show(), the showdialog opens the dashboard and does not allow flicking between windows. 
                        this.Hide();

                        dashboardApplicant.Uid = currentUser;
                    }
                    else if (userRecord.LevelId == 2)
                    {
                        DashboardProvider dashboardProvider = new DashboardProvider();
                        dashboardProvider.Owner = this;
                        dashboardProvider.ShowDialog(); //unlike dashboard.Show(), the showdialog opens the dashboard and does not allow flicking between windows. 
                        this.Hide();

                        dashboardProvider.Uid = currentUser;
                    }

                }
                if (userRecord.Username != currentUser && userRecord.Password != currentPassword)
                {
                    MessageBox.Show("Username or Password incorrect. Please try again!", "Notice!", MessageBoxButton.OK);
                }
            }
        }
    }
}
