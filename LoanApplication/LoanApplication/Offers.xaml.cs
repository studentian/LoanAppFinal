using LoanAppLibraryV3;
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

        Offer selectedOffer = new Offer();

        public Offers()
        {
            InitializeComponent();
        }

        private void btnOffers_Click(object sender, RoutedEventArgs e)
        {
            ClientViews clientViews = new ClientViews();
            clientViews.Show();
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

        private void lstOfferList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstOfferList.SelectedIndex > 0)
            {
                selectedOffer = offerList.ElementAt(lstOfferList.SelectedIndex); //number corresponding to value on list view
             }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshOfferList();
        }
    }
}
