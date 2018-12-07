
using LoanAppClassLibrary;
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
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Page
    {
        LoanAppDBEntities db = new LoanAppDBEntities();

        //all methods need to access the list
        List<User> users = new List<User>();

        //open logs
        List<Log> logs = new List<Log>();


        public Admin()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            lstUserList.ItemsSource = users; //admin link for admin user fails here
            lstProviderList.ItemsSource = logs;

            foreach (var user in db.Users)
            {
                users.Add(user);
                
            }

            foreach(var log in db.Logs)
            {
                logs.Add(log);

            }
        }

        //private void submenuAddUser_Click(object sender, RoutedEventArgs e)
        //{
        //    stkUserDetails.Visibility = Visibility.Visible;
        //}

        //private void btnUpdate_Click(object sender, RoutedEventArgs e)
        //{
        //    stkUserDetails.Visibility = Visibility.Collapsed;
        //}

        private void cboAccessLevel_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void lstUserList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void lstProviderList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
