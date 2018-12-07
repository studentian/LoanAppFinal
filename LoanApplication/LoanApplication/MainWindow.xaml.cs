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

        //method to save logs
        //below created the structure to cave log
        private void createLogEntry(int UserId, string Action, string Status, string Description)
        {
            string comment = $"{Description} user credentials = {UserId}";

            //create log event, fill in information for it and send it to the database
            Log log = new Log();
            log.UserId = UserId;
            log.Action = Action;
            log.Description = Description;
            log.Status = Status;
            log.Date = DateTime.Now;

            SaveLog(log);
        }

        private void SaveLog(Log log)
        {
            db.Entry(log).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
        }


        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            User validatedUser = new User();
            bool login = false;

            string currentUser = tbxUserName.Text;
            string currentPassword = pbxPassword.Password;

            foreach (var user in db.Users)
            {
                if (user.Username == currentUser && user.Password == currentPassword)
                {
                    login = true;
                    validatedUser = user;

                    //createLogEntry(validatedUser.UserId, "Login", " Successful", "login successful");

                    //MessageBox.Show("Welcome " + validatedUser.First_name, "Login confirmed!", MessageBoxButton.OK);

                    //Dashboard dashboard = new Dashboard();
                    //dashboard.user = validatedUser;
                    //dashboard.ShowDialog();
                    //this.Hide();

                }

                if (user.Username != currentUser && user.Password != currentPassword)
                {
                    MessageBox.Show("Username or Password incorrect. Please try again!", "Notice!", MessageBoxButton.OK);
                }
            }

            if (login)
            {
                createLogEntry(validatedUser.UserId, "Login", " Successful", "login successful");

                MessageBox.Show("Welcome " + validatedUser.FirstName, "Login confirmed.", MessageBoxButton.OK);
                Dashboard dashboard = new Dashboard();
                dashboard.user = validatedUser;
                dashboard.Show();
                this.Hide();

            }
            //else
            //{
            //    createLogEntry(0, "Login", "Not Successful", "Login unsuccessful");
            //}

        }
    }
}


