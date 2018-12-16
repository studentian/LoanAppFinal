using LoanAppLibV1;
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
    /// Interaction logic for ClientViews.xaml
    /// </summary>
    public partial class ClientViews : Window
    {

        LoanAppDBEntities db = new LoanAppDBEntities();

        List<User> listUser = new List<User>();
        List<UserFinancial> UserFinancial = new List<UserFinancial>();

        UserFinancial userFinancial = new UserFinancial();
        User user = new User();

        public ClientViews()
        {
            InitializeComponent();
        }

        private void btnSummarySubmit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Thank you! \nPlease check back in 24 hours. \nReceive quotes by email?", "Client View", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);

            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Close();
        }

        private void btnRepayment_Click(object sender, RoutedEventArgs e)
        {
                userFinancial.PurchasePrice = decimal.Parse(tboxRpmtPurchasePrice.Text);
                userFinancial.Deposit = decimal.Parse(tboxRpmtDeposit.Text);
                userFinancial.RpmtIntRate = float.Parse(tboxRpmtInterestRate.Text);
                userFinancial.RpmtLoanTerm = int.Parse(tboxRpmtLoanTerm.Text);

            int saveSuccess = saveUser();
            if (saveSuccess == 1)
            {
                MessageBox.Show("Saved! \nPlease complete Step 2", "Step.1 Saved!", MessageBoxButton.OK);
            }
        }

        private void btnAffNext_Click(object sender, RoutedEventArgs e)
        {
                //userFinancial.UserId = user.UserId;
                userFinancial.Salary = decimal.Parse(tboxAffSalary.Text); // I am trying here to add the user input here into the db.UserFinancial
                userFinancial.Expenses = decimal.Parse(tboxAffExpenses.Text);
                userFinancial.LoanTerm = int.Parse(tboxAffTerm.Text);
                userFinancial.AffIntRate = float.Parse(tboxAffIntRate.Text);

            int saveSuccess = saveUser();
            if (saveSuccess == 1)
            {
                MessageBox.Show("Saved! \nPlease complete Step 2", "Step.1 Saved!", MessageBoxButton.OK);
            }
        }

        public int saveUser()
        {
            db.Entry(userFinancial).State = System.Data.Entity.EntityState.Added;
            int saveSuccess = db.SaveChanges();
            return saveSuccess;
        }
    }
}
