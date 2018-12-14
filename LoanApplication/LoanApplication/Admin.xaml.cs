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

        User selectedUser = new User();

        enum DBOperation
        {
            //The one update button needs to do three things
            Add,
            Modify,
            Delete
        }

        //create an instance of DB Enum
        DBOperation dbOperation = new DBOperation();

        public Admin()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            RefreshUserList();

            lstProviderList.ItemsSource = logs;

            foreach(var log in db.Logs)
            {
                logs.Add(log);

            }
        }
    
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dbOperation == DBOperation.Add)
            {
                User user = new User();
                    user.Password = tbxPassword.Text.Trim();
                    user.FirstName = tbxFirstName.Text.Trim();
                    user.LastName = tbxLastName.Text.Trim();
                    user.Email = tbxEmail.Text.Trim();
                    user.Username = tbxUsername.Text.Trim(); 
                    user.LevelId = cboAccessLevel.SelectedIndex; //selected index is an Int and levelid is also an Int So selcted index here will match the selected index on the db.

                int saveSuccess = SaveUser(user);

                if (saveSuccess == 1)
                {
                    MessageBox.Show("User saved successfully.", "Save to Database", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshUserList();
                    clearUserDetails();
                    stkUserDetails.Visibility = Visibility.Collapsed;
                }
                else
                {
                    MessageBox.Show("Problem saving user record.", "Save to database", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

            if (dbOperation == DBOperation.Modify)
            {
                foreach(var user in db.Users.Where(t => t.UserId == selectedUser.UserId))
                {
                    user.Password = tbxPassword.Text.Trim();
                    user.FirstName = tbxFirstName.Text.Trim();
                    user.LastName = tbxLastName.Text.Trim();
                    user.Email = tbxEmail.Text.Trim();
                    user.Username = tbxUsername.Text.Trim();
                    user.LevelId = cboAccessLevel.SelectedIndex;
                }

                int saveSuccess = db.SaveChanges();

                if (saveSuccess == 1)
                {
                    MessageBox.Show("User modified successfully.", "Save to Database", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshUserList();
                    clearUserDetails();
                    stkUserDetails.Visibility = Visibility.Collapsed;
                }
            }

        }
            
            public int SaveUser(User user)
        {
            db.Entry(user).State = System.Data.Entity.EntityState.Added;
            int saveSuccess = db.SaveChanges();
            return saveSuccess;

        }

        private void RefreshUserList()
        {
            lstUserList.ItemsSource = users;
            users.Clear(); 
            foreach(var user in db.Users)
            {
                users.Add(user);
            }
            lstUserList.Items.Refresh();
        }

        private void clearUserDetails()
        {
            tbxPassword.Text = "";
            tbxFirstName.Text = "";
            tbxLastName.Text = "";
            tbxEmail.Text = "";
            tbxUsername.Text = "";
            tbxAccessLevel.Text = "";
            cboAccessLevel.SelectedIndex = 0;

        }

        private void lstUserList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if(lstUserList.SelectedIndex > 0)
            {
                    selectedUser = users.ElementAt(lstUserList.SelectedIndex); //number corresponding to value on list view
                    submenuModifyUser.IsEnabled = true;
                    submenuDeleteUser.IsEnabled = true;

                //User must click in another field if details do not auto populate
                if (dbOperation == DBOperation.Modify)
                {
                    tbxPassword.Text = selectedUser.Password;
                    tbxFirstName.Text = selectedUser.FirstName;
                    tbxLastName.Text = selectedUser.LastName;
                    tbxEmail.Text = selectedUser.Email;
                    tbxUsername.Text = selectedUser.Username;
                    cboAccessLevel.SelectedIndex = selectedUser.LevelId; //combo box and selcted index in combo box
                }

                //User must click in another field to clear the auto loaded details to add a user
                if (dbOperation == DBOperation.Add)
                {
                    clearUserDetails();
                }
            }
        }

        private void submenuAddUser_Click(object sender, RoutedEventArgs e)
        {
            stkUserDetails.Visibility = Visibility.Visible;
            dbOperation = DBOperation.Add;

        }


        private void submenuModifyUser_Click(object sender, RoutedEventArgs e)
        {
            stkUserDetails.Visibility = Visibility.Visible;
            dbOperation = DBOperation.Modify;
        }

        private void submenuDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            stkUserDetails.Visibility = Visibility.Visible;
            db.Users.RemoveRange(db.Users.Where(t => t.UserId == selectedUser.UserId));
            int saveSuccess = db.SaveChanges();
            if (saveSuccess == 1)
            {
                MessageBox.Show("User deleted successfully.", "Save to Database", MessageBoxButton.OK, MessageBoxImage.Information);
                RefreshUserList();
                clearUserDetails();
                stkUserDetails.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("Problem deleting user record.", "Delete from database", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
