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
        LoanAppDBEntities db = new LoanAppDBEntities();

        List<Offers> offerList = new List<Offers>();

        //open logs
        List<Log> logs = new List<Log>();

        Offer offer = new Offer();

        enum DBOperation
        {
            //The one update button needs to do three things
            Add,
            Modify,
            Delete
        }

        DBOperation dbOperationOffer = new DBOperation();

        public Provider()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //RefreshProviderList();

            lstProviderList.ItemsSource = logs;

            foreach (var log in db.Logs)
            {
                logs.Add(log);

            }
        }

        public void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dbOperationOffer == DBOperation.Add)
            {

                offer.QuoteId = int.Parse(tbxQuoteId.Text.Trim());
                offer.FirstName = tbxFirstName.Text.Trim();
                offer.LastName = tbxLastName.Text.Trim();
                offer.OfferAmount = decimal.Parse(tbxOfferAmount.Text.Trim());
                offer.Term = int.Parse(tbxTerm.Text.Trim());
                offer.InterestRate = float.Parse(tbxIntRate.Text.Trim());
                offer.ProviderName = tbxProviderName.Text.Trim();

                offer.OfferStatus = cboOfferStatus.SelectionBoxItemStringFormat; 

                int saveSuccess = SaveOffer(offer);

                if (saveSuccess == 1)
                {
                    MessageBox.Show("User saved successfully.", "Save to Database", MessageBoxButton.OK, MessageBoxImage.Information);
                   //RefreshProviderList();
                    clearProviderDetails();
                    stkProviderDetails.Visibility = Visibility.Collapsed;
                } 
                else
                {
                    MessageBox.Show("Problem saving user record.", "Save to database", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

            if (dbOperationOffer == DBOperation.Modify)
            {
                foreach (var user in db.Users.Where(t => t.UserId == offer.UserId))
                {
                    offer.QuoteId = int.Parse(tbxQuoteId.Text.Trim());
                    offer.FirstName = tbxFirstName.Text.Trim();
                    offer.LastName = tbxLastName.Text.Trim();
                    offer.OfferAmount = decimal.Parse(tbxOfferAmount.Text.Trim());
                    offer.Term = int.Parse(tbxTerm.Text.Trim());
                    offer.InterestRate = float.Parse(tbxIntRate.Text.Trim());
                    offer.ProviderName = tbxProviderName.Text.Trim();

                    offer.OfferStatus = cboOfferStatus.SelectedIndex;
                }

                int saveSuccess = db.SaveChanges();

                if (saveSuccess == 1)
                {
                    MessageBox.Show("User modified successfully.", "Save to Database", MessageBoxButton.OK, MessageBoxImage.Information);
                    //RefreshProviderList();
                    clearProviderDetails();
                    stkProviderDetails.Visibility = Visibility.Collapsed;
                }
            }

        }

        private void submenuAdd_Click(object sender, RoutedEventArgs e)
        {
            stkProviderDetails.Visibility = Visibility.Visible;  
        }

        //private void btnUpdate_Click(object sender, RoutedEventArgs e)
        //{
        //    stkProviderDetails.Visibility = Visibility.Collapsed;
        //}

        public int SaveOffer(Offer saveOffer)
        {
            db.Entry(saveOffer).State = System.Data.Entity.EntityState.Added;
            int saveSuccess = db.SaveChanges();
            return saveSuccess;

        }

        //private void RefreshProviderList()
        //{
        //    lstProviderList.ItemsSource = offer;
        //    offer.Clear();
        //    foreach (var user in db.Offers)
        //    {
        //        offer.Add(offer);
        //    }
        //    lstProviderList.Items.Refresh();
        //}

        private void clearProviderDetails()
        {
            tbxQuoteId.Text = "";
            tbxFirstName.Text = "";
            tbxLastName.Text = "";
            tbxTerm.Text = "";
            tbxIntRate.Text= "";
            tbxProviderName.Text = "";

            cboOfferStatus.SelectedIndex = 0;

        }

        private void lstProviderList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (lstUserList.SelectedIndex > 0)
            {
                offer = cboOfferStatus.ElementAt(lstProviderList.SelectedIndex); //number corresponding to value on list view
                submenuModifyOffer.IsEnabled = true;
                submenuDeleteOffer.IsEnabled = true;

                //User must click in another field if details do not auto populate
                if (dbOperationOffer == DBOperation.Modify)
                {
                    offer.QuoteId = int.Parse(tbxQuoteId.Text.Trim());
                    offer.FirstName = tbxFirstName.Text.Trim();
                    offer.LastName = tbxLastName.Text.Trim();
                    offer.OfferAmount = decimal.Parse(tbxOfferAmount.Text.Trim());
                    offer.Term = int.Parse(tbxTerm.Text.Trim());
                    offer.InterestRate = float.Parse(tbxIntRate.Text.Trim());
                    offer.ProviderName = tbxProviderName.Text.Trim(); //combo box and selcted index in combo box
                }

                //User must click in another field to clear the auto loaded details to add a user
                if (dbOperationOffer == DBOperation.Add)
                {
                    clearProviderDetails();
                }
            }
        }

        private void submenuAddUser_Click(object sender, RoutedEventArgs e)
        {
            stkProviderDetails.Visibility = Visibility.Visible;
            dbOperationOffer = DBOperation.Add;

        }


        private void submenuModifyUser_Click(object sender, RoutedEventArgs e)
        {
            stkProviderDetails.Visibility = Visibility.Visible;
            dbOperationOffer = DBOperation.Modify;
        }

        private void submenuDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            stkProviderDetails.Visibility = Visibility.Visible;
            db.Users.RemoveRange(db.Users.Where(t => t.UserId == offer.UserId));
            int saveSuccess = db.SaveChanges();
            if (saveSuccess == 1)
            {
                MessageBox.Show("User deleted successfully.", "Save to Database", MessageBoxButton.OK, MessageBoxImage.Information);
                //RefreshProviderList();
                clearProviderDetails();
                stkProviderDetails.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("Problem deleting user record.", "Delete from database", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
