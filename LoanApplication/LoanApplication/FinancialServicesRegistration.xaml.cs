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

        User finRegUser = new User();

        public FinancialServicesRegistration()
        {
            InitializeComponent();
        }

        public void btnRegister_Click(object sender, RoutedEventArgs e)
        {
          
                finRegUser.Username = tbxUsername.Text.Trim();
                finRegUser.ProviderName = tbxCompanyName.Text.Trim();
                finRegUser.CompanyReg = tbxRegistration.Text.Trim();
                finRegUser.Password= tbxPassword.Text.Trim();
                finRegUser.AddressLine1 = tbxAddress.Text.Trim();
                finRegUser.Postcode = tbxPostCode.Text.Trim();
                finRegUser.Telephone = tbxTelephone.Text.Trim();
                finRegUser.Email = tbxEmail.Text.Trim();
                finRegUser.LevelId = 2;
                finRegUser.LastName = "Provider";
                finRegUser.FirstName = "Provider";
                finRegUser.AddressLine2 = "Provider";
                finRegUser.City = "Provider";
                
                //vailidates user inputs
                ValidateUserInput();

                //int saveSuccess contains method SaveOffer(). Offer object contains all details inputted by user above. 
            int saveSuccess = RegisterUser(finRegUser);

                if (saveSuccess == 1)
                {
                    MessageBox.Show("Thank you for registering" + finRegUser.FirstName, "Please login!", MessageBoxButton.OK);
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();

                    this.Close();

            }
                else
                {
                    MessageBox.Show("Problem saving user record.", "Save to database", MessageBoxButton.OK, MessageBoxImage.Warning);
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();

                    this.Close();
            }
              
        }

        public int RegisterUser(User finRegUser)
        {
                db.Entry(finRegUser).State = System.Data.Entity.EntityState.Added;
                int saveSuccess = db.SaveChanges();
                return saveSuccess;
        }


        private void btnback_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            this.Close();
        }

        private bool ValidateUserInput()
        {
            bool validated = true;

            if (tbxUsername.Text.Length == 0 || tbxUsername.Text.Length > 20)
            {
                validated = false;
            }

            if (tbxCompanyName.Text.Length == 0 || tbxCompanyName.Text.Length > 20)
            {
                validated = false;
            }

            if (tbxPassword.Text.Length == 0 || tbxPassword.Text.Length > 20)
            {
                validated = false;
            }

            if (tbxAddress.Text.Length == 0 || tbxAddress.Text.Length > 50)
            {
                validated = false;
            }

            if (tbxPostCode.Text.Length == 0 || tbxPostCode.Text.Length > 10)
            {
                validated = false;
            }

            if (tbxRegistration.Text.Length == 0 || tbxRegistration.Text.Length > 20)
            {
                validated = false;
            }

            if (tbxTelephone.Text.Length == 0 || tbxTelephone.Text.Length > 20)
            {
                validated = false;
            }
            if (tbxEmail.Text.Length == 0 || tbxEmail.Text.Length > 50)
            {
                validated = false;
            }

            return validated;
        }

    }
}

