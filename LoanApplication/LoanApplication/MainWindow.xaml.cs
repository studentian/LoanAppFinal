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
using LoanAppClassLibrary;
using LoanApplication;

namespace LoanApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //NB!  
        //BEFORE ==> Remove below from App.config in DBCLassEFLibrary
        //<add name="loanappdbEntities" connectionString="metadata=res://*/LoanAppModel.csdl|res://*/LoanAppModel.ssdl|res://*/LoanAppModel.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=192.168.1.138;initial catalog=loanappdb;user id=ianl;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />
        //AFTER ==> Change and past into MainWindow.xaml.cs
        //"metadata=res://*/LoanAppModel.csdl|res://*/LoanAppModel.ssdl|res://*/LoanAppModel.msl;provider=System.Data.SqlClient;provider 
        //connection string='data source=192.168.1.138;initial catalog=loanappdb;
        //user id = ianl; MultipleActiveResultSets=True;App=EntityFramework'"

        LoanAppDBEntities db = new LoanAppDBEntities();

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

            foreach (var user in db.Users)
            {
                if (user.Username == currentUser && user.Password == currentPassword)
                {

                    MessageBox.Show("Good day, thank you for visiting!", "Login confirmed!", MessageBoxButton.OK);

                        Dashboard dashboard = new Dashboard();
                        dashboard.user = user;
                        dashboard.ShowDialog();
                        this.Hide();

                }

                if (user.Username != currentUser && user.Password != currentPassword)
                {
                    MessageBox.Show("Username or Password incorrect. Please try again!", "Notice!", MessageBoxButton.OK);
                }
            }
        }
    }
}


