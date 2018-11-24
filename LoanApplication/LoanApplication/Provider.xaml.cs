using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for Provider.xaml
    /// </summary>
    public partial class Provider : Page
    {
        public Provider()
        {
            InitializeComponent();
        }

        //method created with James. This will record log entries. Create log event if logon successful or not.  
        //private void CreateLogEntry(string category, string description, string userID, string userName)
        //{
        //    string comment = $"{description} user that logged in = {UserName}";

        //    Log log = new Log();
        //    log.UserId = userID;
        //    Log.DateTime = DateTime.Now; 


        //}

        //private void SaveLog(Log log)
        //{
        //    db.Entry(log).State = System.Data.EntityState.Added;
        //    db.SaveChanges();
        //}

        private void submenuAdd_Click(object sender, RoutedEventArgs e)
        {
            stkProviderDetails.Visibility = Visibility.Visible;  
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            stkProviderDetails.Visibility = Visibility.Collapsed;
        }

    }
}
