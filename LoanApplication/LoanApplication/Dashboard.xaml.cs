using LoanAppLibraryV4;
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
            if (user.LevelId > 0) //refactoring and improving program code
            {
                if (user.LevelId == 3)
                {
                    mnuMain.Visibility = Visibility.Visible;
                    mnuClientView.Visibility = Visibility.Visible;
                    mnuProviderView.Visibility = Visibility.Visible;
                    mnuAdministratorView.Visibility = Visibility.Visible;
                    mnuAnalysisView.Visibility = Visibility.Visible;

                }

                if (user.LevelId == 1)
                {
                    mnuMain.Visibility = Visibility.Visible;
                    mnuClientView.Visibility = Visibility.Visible;
                    mnuProviderView.Visibility = Visibility.Collapsed;
                    mnuAdministratorView.Visibility = Visibility.Collapsed;
                    mnuAnalysisView.Visibility = Visibility.Collapsed;
                }
                if (user.LevelId == 2)
                {
                    mnuMain.Visibility = Visibility.Visible;
                    mnuClientView.Visibility = Visibility.Collapsed;
                    mnuProviderView.Visibility = Visibility.Visible;
                    mnuAdministratorView.Visibility = Visibility.Collapsed;
                    mnuAnalysisView.Visibility = Visibility.Collapsed;
                }
            }

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }

        private void btnlogout_Click(object sender, RoutedEventArgs e)
        {

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            this.Close();
            Environment.Exit(0);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }

        private void mnuAnalysisView_Click(object sender, RoutedEventArgs e)
        {
            Analysis analysis = new Analysis();
            frmMain.Navigate(analysis);
        }

        private void mnuClientView_Click(object sender, RoutedEventArgs e)
        {
            Offers offers = new Offers();
            frmMain.Navigate(offers);
        }

        private void mnuProviderView_Click(object sender, RoutedEventArgs e)
        {
            Provider provider = new Provider();
            frmMain.Navigate(provider);
        }

        private void mnuAdministratorView_Click(object sender, RoutedEventArgs e)
        {
            Admin admin = new Admin();
            //now assign the admin instance into the frame
            frmMain.Navigate(admin);
        }
    }
}
