using LoanAppLibraryV4;
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
     public partial class ClientRegistration : Window
    {
        LoanAppDBEntities db = new LoanAppDBEntities();

        User clientRegUser = new User();

        public ClientRegistration()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {

            clientRegUser.Username = tbxUserName.Text.Trim();
            clientRegUser.Password = tbxPassword.Text.Trim();
            clientRegUser.FirstName = tbxFirstName.Text.Trim();
            clientRegUser.LastName = tbxLastName.Text.Trim();
            clientRegUser.AddressLine1 = tbxAddress.Text.Trim();
            clientRegUser.Postcode = tbxPostCode.Text.Trim();
            clientRegUser.Telephone = tbxTelephone.Text.Trim();
            clientRegUser.Email = tbxEmail.Text.Trim();
            clientRegUser.LevelId = 1;
            clientRegUser.AddressLine2 = "Client";
            clientRegUser.City = "Client";
            clientRegUser.ProviderName = "Client";
            clientRegUser.CompanyReg = "Client";

            //vailidates user inputs
            ValidateUserInput();

            //int saveSuccess contains method SaveOffer(). Offer object contains all details inputted by user above. 
            int saveSuccess = RegisterUser(clientRegUser);

            if (saveSuccess == 1)
            {
                MessageBox.Show("Thank you for registering" + clientRegUser.FirstName, "Please login!", MessageBoxButton.OK);
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
            db.Entry(clientRegUser).State = System.Data.Entity.EntityState.Added;
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
