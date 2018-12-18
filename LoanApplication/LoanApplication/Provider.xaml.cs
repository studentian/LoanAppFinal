using LoanAppLibraryV3;
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

        List<Offer> offerList = new List<Offer>();
        List<UserFinancial> applicantList = new List<UserFinancial>();

        Offer selectedOffer = new Offer();
        UserFinancial applicant = new UserFinancial();

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
            RefreshOfferList();
            RefreshApplicantList();
        }

        public void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dbOperationOffer == DBOperation.Add)
            {
                Offer offer = new Offer();
                offer.QuoteId = int.Parse(tbxQuoteId.Text.Trim());
                offer.UserId = int.Parse(tbxUserId.Text.Trim());
                offer.OfferAmount = decimal.Parse(tbxOfferAmount.Text.Trim());
                offer.Term = int.Parse(tbxOfferTerm.Text.Trim());
                offer.InterestRate = float.Parse(tbxOfferIntRate.Text.Trim());
                offer.ProviderName = tbxProviderName.Text.Trim();

                offer.OfferStatusId = cboOfferStatus.SelectedIndex;

                int saveSuccess = db.SaveChanges();

                if (saveSuccess == 1)
                {
                    MessageBox.Show("User saved successfully.", "Save to Database", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshOfferList();
                    clearOfferDetails();
                    stkMakeOffer.Visibility = Visibility.Collapsed;
                }
                else
                {
                    MessageBox.Show("Problem saving user record.", "Save to database", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

            if (dbOperationOffer == DBOperation.Modify)
            {
                foreach (var offer in db.Offers.Where(t => t.OfferId == selectedOffer.OfferId))
                {
                    offer.QuoteId = int.Parse(tbxQuoteId.Text.Trim());
                    offer.UserId = int.Parse(tbxUserId.Text.Trim());
                    offer.OfferAmount = decimal.Parse(tbxOfferAmount.Text.Trim());
                    offer.Term = int.Parse(tbxOfferTerm.Text.Trim());
                    offer.InterestRate = float.Parse(tbxOfferIntRate.Text.Trim());
                    offer.ProviderName = tbxProviderName.Text.Trim();

                    offer.OfferStatusId = cboOfferStatus.SelectedIndex;

                }

                int saveSuccess = db.SaveChanges();

                if (saveSuccess == 1)
                {
                    MessageBox.Show("User modified successfully.", "Save to Database", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshOfferList();
                    clearOfferDetails();
                    stkMakeOffer.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void submenuAdd_Click(object sender, RoutedEventArgs e)
        {
            stkMakeOffer.Visibility = Visibility.Visible;
        }

        public int SaveOffer(Offers saveOffer)
        {
            db.Entry(saveOffer).State = System.Data.Entity.EntityState.Added;
            int saveSuccess = db.SaveChanges();
            return saveSuccess;
        }

        private void RefreshOfferList()
        {
            lstOfferList.ItemsSource = offerList;


            offerList.Clear();
            foreach (var offVar in db.Offers)
            {
                offerList.Add(offVar);
            }
            lstOfferList.Items.Refresh();
        }

        private void RefreshApplicantList()
        {
            lstApplicantList.ItemsSource = applicantList;

            applicantList.Clear();
            foreach (var applict in db.UserFinancials)
            {
                applicantList.Add(applict);
            }
            lstApplicantList.Items.Refresh();
        }

        private void clearOfferDetails()
        {
            tbxQuoteId.Text = "";
            tbxUserId.Text = "";
            tbxOfferAmount.Text = "";
            tbxOfferTerm.Text = "";
            tbxOfferIntRate.Text = "";
            tbxProviderName.Text = "";

            cboOfferStatus.SelectedIndex = 0;

        }

        private void lstOfferList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (lstOfferList.SelectedIndex > 0)
            {

                selectedOffer = offerList.ElementAt(lstOfferList.SelectedIndex); //number corresponding to value on list view
                submenuModifyOffer.IsEnabled = true;
                submenuDeleteOffer.IsEnabled = true;

                //User must click in another field if details do not auto populate
                if (dbOperationOffer == DBOperation.Modify)
                {
                    //  tbxPassword.Text = selectedUser.Password;
                    tbxQuoteId.Text = tbxQuoteId.Text;
                    tbxUserId.Text = tbxUserId.Text;
                    tbxOfferAmount.Text = tbxOfferAmount.Text;
                    tbxOfferTerm.Text = tbxOfferTerm.Text;
                    tbxOfferIntRate.Text = tbxOfferIntRate.Text;
                    tbxProviderName.Text = tbxProviderName.Text;

                    //cboAccessLevel.SelectedIndex = selectedUser.LevelId;
                    cboOfferStatus.SelectedIndex = selectedOffer.OfferStatusId;
                }

                //User must click in another field to clear the auto loaded details to add a user
                if (dbOperationOffer == DBOperation.Add)
                {
                    clearOfferDetails();
                }
            }
        }

        private void submenuAddUser_Click(object sender, RoutedEventArgs e)
        {
            stkMakeOffer.Visibility = Visibility.Visible;
            dbOperationOffer = DBOperation.Add;

        }


        private void submenuModifyUser_Click(object sender, RoutedEventArgs e)
        {
            stkMakeOffer.Visibility = Visibility.Visible;
            dbOperationOffer = DBOperation.Modify;
        }

        private void submenuDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            stkMakeOffer.Visibility = Visibility.Visible;
            
            db.Offers.RemoveRange(db.Offers.Where(t => t.OfferId == selectedOffer.OfferId));
            int saveSuccess = db.SaveChanges();
            if (saveSuccess == 1)
            {
                MessageBox.Show("User deleted successfully.", "Save to Database", MessageBoxButton.OK, MessageBoxImage.Information);
                RefreshOfferList();
                clearOfferDetails();
                stkMakeOffer.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("Problem deleting user record.", "Delete from database", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void lstApplicantList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            stkMakeOffer.Visibility = Visibility.Collapsed; //check if this works

        }
    }
}
