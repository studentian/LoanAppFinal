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
using DBClassEFLibrary;
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

        loanappdbEntities db = new loanappdbEntities("metadata=res://*/LoanAppModel.csdl|res://*/LoanAppModel.ssdl|res://*/LoanAppModel.msl;provider=System.Data.SqlClient;provider connection string = 'data source=192.168.1.138;initial catalog=loanappdb;user id = ianl; password = Apples2018;MultipleActiveResultSets=True;App=EntityFramework'");

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
                    //User validatedUser = new User(); //validatedUser is made to be a user object so pass db columns. See createlogentery IF statement below 
                    //loginlog = true;

                    MessageBox.Show("Good day, thank you for visiting!", "Login confirmed!", MessageBoxButton.OK);

                    //if (userRecord.LevelId == 3)
                    //{
                        Dashboard dashboard = new Dashboard();
                        dashboard.Owner = this;
                        dashboard.ShowDialog();
                        this.Hide();

                        dashboard.Uid = currentUser;


                    //}

                    //    else if (userRecord.LevelId == 1)
                    //    {
                    //        DashboardApplicant dashboardApplicant = new DashboardApplicant();
                    //        dashboardApplicant.Owner = this;
                    //        dashboardApplicant.Show();
                    //        this.Hide();

                    //        dashboardApplicant.Uid = currentUser;



                    //    }
                    //    else if (userRecord.LevelId == 2)
                    //    {
                    //        DashboardProvider dashboardProvider = new DashboardProvider();
                    //        dashboardProvider.Owner = this;
                    //        dashboardProvider.ShowDialog();
                    //        this.Hide();

                    //        dashboardProvider.Uid = currentUser;


                    //    }

                    //}

                }

                if (userRecord.Username != currentUser && userRecord.Password != currentPassword)
                {
                    MessageBox.Show("Username or Password incorrect. Please try again!", "Notice!", MessageBoxButton.OK);
                }
            }
        }
    }
}


