using LoanAppLibraryV4;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        LoanAppDBEntities db = new LoanAppDBEntities();

        public enum DBOperation
        {
            Add,
        }

        //create an instance of DB Enum
        public DBOperation dbOperation = new DBOperation();

        public FinancialServicesRegistration()
        {
            InitializeComponent();
        }

        public int SaveUser(User user)
        {
            db.Entry(user).State = System.Data.Entity.EntityState.Added;
            int saveSuccess = db.SaveChanges();
            return saveSuccess;
        }

        public void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();
            dbOperation = DBOperation.Add;

            user.ProviderName = tbxCompanyName.Text.Trim();
            user.Password = tbxPassword.Text.Trim();
            user.AddressLine1 = tbxAddress.Text.Trim();
            user.Postcode = tbxPostCode.Text.Trim();
            user.CompanyReg = tbxRegistration.Text.Trim();
            user.Telephone = tbxTelephone.Text.Trim();
            user.Email = tbxEmail.Text.Trim();
            user.Username = tbxUsername.Text.Trim();
            user.LevelId = 2;

            int saveSuccess = db.SaveChanges();

            //string CompanyName = tbxCompanyName.Text;
            //string Password = tbxPassword.Text;
            //string AddressLine1 = tbxAddress.Text;
            //string PostCode = tbxPostCode.Text;
            //string CompanyReg = tbxRegistration.Text;
            //string Telephone = tbxTelephone.Text;
            //string Email = tbxEmail.Text;
            //string ProviderName = tbxCompanyName.Text;
            //string Username = tbxUsername.Text;
            //int LevelId = user.LevelId = 2;

            //SqlConnection sqlConnection = new SqlConnection("data source=192.168.1.138;initial catalog=loanappdb;user id=ianl;password = Apples2018");
            // SqlCommand dbReg = new SqlCommand("Insert into Users(LevelId,tbxCompanyName, tbxPassword,tbxAddress,tbxPostCode,tbxRegistration,tbxTelephone,tbxEmail, tbxUsername) values('" + LevelId + "','" + CompanyName + "', '" + Password + "', '" + AddressLine1 + "', '" + PostCode + "', '" + Telephone + "', '" + Email + "', '" + ProviderName + "', '" + CompanyReg + "', '" + Username + "')", sqlConnection);

            if (saveSuccess == 1)
            {
                SaveUser(user);
                //clearUserDetails();
                MessageBox.Show("Welcome! \nRegistration Confirmed!", "Thank you for registering!", MessageBoxButton.OK);

                Dashboard dashboard = new Dashboard();
                dashboard.user = user;
                dashboard.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Please try again", "Registration unsuccessful!", MessageBoxButton.OK);
            }
        }

        private void btnback_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            this.Close();
        }


        //private void clearUserDetails()
        //{
        //    tbxPassword.Text = "";
        //    tbxFirstName.Text = "";
        //    tbxLastName.Text = "";
        //    tbxEmail.Text = "";
        //    tbxUsername.Text = "";
        //    tbxAccessLevel.Text = "";
        //    cboAccessLevel.SelectedIndex = 0;
        //}
    }
}

