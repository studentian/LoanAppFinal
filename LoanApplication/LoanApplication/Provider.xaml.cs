using LoanAppLibraryV4;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
        //conects application to the Entity Framework
        LoanAppDBEntities db = new LoanAppDBEntities();
        
        List<Offer> offerList = new List<Offer>();
        List<UserFinancial> applicantList = new List<UserFinancial>(); 

        List<LoanAppLibraryV4.Offer> offerListType = new List <LoanAppLibraryV4.Offer> ();

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
                offer.FirstName = tbxFirstName.Text.Trim();
                offer.LastName = tbxLastName.Text.Trim();
                offer.ProviderName = tbxProviderName.Text.Trim();
                offer.CompanyReg = tbxCompanyReg.Text.Trim();

                offer.OfferStatusId = cboOfferStatus.SelectedIndex;

                bool validation = ValidateUserInput();

                if (validation == true)
                {
                    //int saveSuccess contains method SaveOffer(). Offer object contains all details inputted by user above. 
                    int saveSuccess = SaveOffer(offer);

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
                else
                {
                    MessageBox.Show("Validation failed.", "Please try again", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                    offer.FirstName = tbxFirstName.Text.Trim();
                    offer.LastName = tbxLastName.Text.Trim();
                    offer.ProviderName = tbxProviderName.Text.Trim();
                    offer.CompanyReg = tbxCompanyReg.Text.Trim();

                    offer.OfferStatusId = cboOfferStatus.SelectedIndex;

                }

                bool validation = ValidateUserInput();

                if (validation == true)
                {

                    //This is dfferent to Add above. 
                    //Here in modify, db.SaveChanges saves all changes made in this context to the underlying database.
                    int saveSuccess = db.SaveChanges();

                    if (saveSuccess == 1)
                    {
                        MessageBox.Show("User modified successfully.", "Save to Database", MessageBoxButton.OK, MessageBoxImage.Information);
                        RefreshOfferList();
                        clearOfferDetails();
                        stkMakeOffer.Visibility = Visibility.Collapsed;
                    }
                }
                else
                {
                    MessageBox.Show("Validation failed.", "Please try again", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void submenuAdd_Click(object sender, RoutedEventArgs e)
        {
            stkMakeOffer.Visibility = Visibility.Visible;
        }

        public int SaveOffer(Offer Offer)
        {

                db.Entry(Offer).State = System.Data.Entity.EntityState.Added;
                int saveSuccess = db.SaveChanges();
                return saveSuccess;

        }

        //Refresh the offer list
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

        //Refresh the Applicant List
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
            tbxFirstName.Text = "";
            tbxLastName.Text = "";
            tbxProviderName.Text = "";
            tbxCompanyReg.Text = "";

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
                    tbxFirstName.Text = tbxFirstName.Text.Trim();
                    tbxLastName.Text = tbxLastName.Text.Trim();
                    tbxProviderName.Text = tbxProviderName.Text;
                    tbxCompanyReg.Text = tbxCompanyReg.Text;

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


        private void submenuModifyOffer_Click(object sender, RoutedEventArgs e)
        {
            stkMakeOffer.Visibility = Visibility.Visible;
            dbOperationOffer = DBOperation.Modify;
        }

        //the Applicant List is for providers to veiw a list of applicants only 
        //to make an offer the user would been to make an offer using the Offers TabItem
        private void lstApplicantList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            stkMakeOffer.Visibility = Visibility.Collapsed; 

        }

        private void submenuDeleteOffer_Click(object sender, RoutedEventArgs e)
        {
            stkMakeOffer.Visibility = Visibility.Visible;

            db.Offers.RemoveRange(db.Offers.Where(t => t.OfferId == selectedOffer.OfferId));
            int saveSuccess = db.SaveChanges();
            if (saveSuccess == 1)
            {
                MessageBox.Show("Offer deleted successfully.", "Save to Database", MessageBoxButton.OK, MessageBoxImage.Information);
                RefreshOfferList();
                clearOfferDetails();
                stkMakeOffer.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("Problem deleting offer record.", "Delete from database", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        //validation for user inputs into fields on Provider.xaml
        private bool ValidateUserInput()
        {
            bool validated = true;

            if(tbxUserId.Text.Length == 0 || tbxUserId.Text.Length > 8)
            {
                validated = false;
            }

            if (tbxQuoteId.Text.Length == 0 || tbxQuoteId.Text.Length > 8)
            {
                validated = false;
            }

            if (tbxOfferAmount.Text.Length == 0 || tbxOfferAmount.Text.Length > 8)
            {
                validated = false;
            }

            if (tbxOfferTerm.Text.Length == 0 || tbxOfferTerm.Text.Length > 3)
            {
                validated = false;
            }

            if (tbxOfferIntRate.Text.Length == 0 || tbxOfferIntRate.Text.Length > 4)
            {
                validated = false;
            }

            if (tbxFirstName.Text.Length == 0 || tbxFirstName.Text.Length > 15)
            {
                validated = false;
            }

            if (tbxLastName.Text.Length == 0 || tbxLastName.Text.Length > 15)
            {
                validated = false;
            }
            if (tbxProviderName.Text.Length == 0 || tbxProviderName.Text.Length > 15)
            {
                validated = false;
            }

            return validated;
        }
    }
}
