using LoanAppClassLibrary;
using LoanApplication;
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
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public User user = new User();

        public Dashboard()
        {
            InitializeComponent();
        }

        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            checkUserAccess(user);
        }

        private void checkUserAccess(User user)
        {

            if (user.LevelId == 3)
            {
                btnClient.Visibility = Visibility.Visible;
                btnProvider.Visibility = Visibility.Visible;
                btnAdmin.Visibility = Visibility.Visible;

            }

            if (user.LevelId == 1)
                {
                    btnClient.Visibility = Visibility.Visible;


                }
                if (user.LevelId == 2)
                {

                    btnProvider.Visibility = Visibility.Visible;
                    mnuProvider.Visibility = Visibility.Visible;


                }


        }

        /*Create a new instance of the screen then create a new instance to show on the screen*/
        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            Admin admin = new Admin();
            //now assign the admin instance into the frame
            frmMain.Navigate(admin);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }

        private void btnProvider_Click(object sender, RoutedEventArgs e)
        {
            Provider provider = new Provider();
            frmMain.Navigate(provider);
        }

        private void btnClient_Click(object sender, RoutedEventArgs e)
        {

            Offers offers = new Offers();
            frmMain.Navigate(offers);

        }

        private void btnlogout_Click(object sender, RoutedEventArgs e)
        {

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            this.Close();
            Environment.Exit(0);
        }
        
       
    }
}
