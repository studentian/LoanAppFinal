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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LoanApplication
{
    /// <summary>
    /// Interaction logic for Offers.xaml
    /// </summary>
    public partial class Offers : Page
    {
        LoanAppDBEntities db = new LoanAppDBEntities();

        List<Offer> offerList = new List<Offer>();

        Offer selectedAccept = new Offer();

        public Offers()
        {
            InitializeComponent();
        }

        enum DBOperation
        {
            Modify,
        }

        DBOperation dbOperationAccept = new DBOperation();

        //when the user clicks on offers button the client views page loads which willl enable a user to enter their financial details
        private void btnOffers_Click(object sender, RoutedEventArgs e)
        {
            ClientViews clientViews = new ClientViews();
            clientViews.Show();
        }

        //This method refreshes the offers view with offers currently in the Offers Table in the DB
        private void RefreshOfferList()
        {
            lstAcceptList.ItemsSource = offerList;
            offerList.Clear();
            
            foreach (var offVar in db.Offers)
            {
                offerList.Add(offVar);
            }
            lstAcceptList.Items.Refresh();
        }
        //page loaded comes from xaml page and will refresh the offers list from the DB when the page is loaded
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshOfferList();
        }

        //to accept an offer the stack panel need to load to enable a user to accept an offer
        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            stkAcceptOffer.Visibility = Visibility.Visible;
            dbOperationAccept = DBOperation.Modify;

        }

        //If the user clicks update here, their acceptance of the offer will feed through to the DB
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dbOperationAccept == DBOperation.Modify)
            {
                Offer accept = new Offer();
                foreach (var acc in db.Offers.Where(t => t.OfferId == selectedAccept.OfferId))
                {
                    //In Offers table, where OfferId's match, Input cboAcceptor (Combobox selection from xaml) into UserAccept column
                    acc.UserAccept = cboAcceptor.SelectedIndex; 
                }

                int saveSuccess = db.SaveChanges();

                if (saveSuccess == 1)
                {
                    MessageBox.Show("User modified successfully.", "Save to Database", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshOfferList();
                    stkAcceptOffer.Visibility = Visibility.Collapsed;
                }

                else
                {
                    MessageBox.Show("Problem saving user record.", "Save to database", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }

        }
        public int SaveAccept(Offer accept)
        {
            db.Entry(accept).State = System.Data.Entity.EntityState.Added;
            int saveSuccess = db.SaveChanges();
            return saveSuccess;
        }

        //to accept an offer the stack panel need to load to enable a user to accept an offer
        //same as button above, this option is selected from the right click
        private void submenuAcceptOffer_Click(object sender, RoutedEventArgs e)
        {
            stkAcceptOffer.Visibility = Visibility.Visible;
            dbOperationAccept = DBOperation.Modify;
        }

        private void lstOfferList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstAcceptList.SelectedIndex > 0)
            {

                selectedAccept = offerList.ElementAt(lstAcceptList.SelectedIndex); //number corresponding to value on list view
                submenuAcceptOffer.IsEnabled = true;

                //User must click in another field if details do not auto populate
                if (dbOperationAccept == DBOperation.Modify)
                {
                    cboAcceptor.SelectedIndex = selectedAccept.OfferStatusId;
                }

            }
        }
    }
}
