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
using System.Windows.Shapes;

namespace LoanApplication
{
    /// <summary>
    /// Interaction logic for ClientViews.xaml
    /// </summary>
    public partial class ClientViews : Window
    {

        LoanAppDBEntities db = new LoanAppDBEntities();

        List<UserFinancial> applicantList = new List<UserFinancial>();

        UserFinancial applicant = new UserFinancial();

        public ClientViews()
        {
            InitializeComponent();
        }

        private void btnSummarySubmit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Thank you! \nPlease check back in 24 hours. \nReceive quotes by email?", "Client View", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            this.Close();
        }

        private void btnAffNext_Click(object sender, RoutedEventArgs e)
        {
                applicant.Salary = decimal.Parse(tboxAffSalary.Text);
                applicant.Expenses = decimal.Parse(tboxAffExpenses.Text);
                applicant.PurchasePrice = decimal.Parse(tboxRpmtPurchasePrice.Text);
                applicant.Deposit = decimal.Parse(tboxRpmtDeposit.Text);
                applicant.AffIntRate = float.Parse(tboxAffIntRate.Text);
                applicant.LoanTerm = int.Parse(tboxAffTerm.Text);

                //qualify amount calculation
                applicant.QualifyAmount = QualifyAmount();

                //monthly mortgage repayment amount
                applicant.RpmtLoanTerm = MonthlyRepayment();

            int saveSuccess = saveUser(applicant);

            if (saveSuccess == 1)
            {
                MessageBox.Show("Saved! \nSummary", "Details Saved!", MessageBoxButton.OK);
                tbSummary.IsSelected = true;
            }

            else
            {
                MessageBox.Show("Problem saving user record.", "Save to database", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        //qualify amount calculation
        public decimal? QualifyAmount()
        {
            decimal? deposit = applicant.Deposit;
            decimal? salary = applicant.Salary;
            decimal? qualifyAmount = ((salary * 3) + deposit);

            return qualifyAmount;
        }

        //monthly mortgage repayment amount
        public int? MonthlyRepayment()
        {
            int? MonthlyRepayment = 0;

            decimal? qualifyAmount = QualifyAmount();
            int? convertedQualifyAmount = Convert.ToInt32(qualifyAmount);
            int? term = applicant.LoanTerm;

            return MonthlyRepayment = (convertedQualifyAmount / (term * 12));

        }

        public int saveUser(UserFinancial applicant)
        {
            db.Entry(applicant).State = System.Data.Entity.EntityState.Added;
            int saveSuccess = db.SaveChanges();
            return saveSuccess;
        }

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            //create OpenFileDialog
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();

            //set filter for file extensionand default file extensions
            dialog.DefaultExt = ".pdf";
            dialog.Filter = "pdf files (*.pdf) | *.pdf";

            //display OpenFileDialog by calling ShowDialog method
            //question mark means that it does not need to contain anything
            bool? result = dialog.ShowDialog();

            if(result.HasValue && result.Value)
            {
                tbxDocumentPath.Text = dialog.FileName.Trim();
            }
        }
    }
}
