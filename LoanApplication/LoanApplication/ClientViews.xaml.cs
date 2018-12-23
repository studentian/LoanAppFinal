﻿using LoanAppLibraryV4;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        List<User> userList = new List<User>();
        List<Offer> offerList = new List<Offer>();

        UserFinancial applicant = new UserFinancial();

        public ClientViews()
        {
            InitializeComponent();
        }

        private void btnSummarySubmit_Click(object sender, RoutedEventArgs e)
        {
            
            foreach (var applicant in db.UserFinancials)
            {
                applicantList.Add(applicant);

                //declare string variables to update DB output into appropriate grid position
                string outputUserId, salary, expenses, loan, intRate, price, deposit, QAmount, Repayment, loanCost = "";
                
                outputUserId = $"{applicant.UserId}";
                tboxSummmaryUserId.Text = outputUserId;

                salary = $"{applicant.Salary}";
                tboxSummmarySalary.Text = salary;

                expenses = $"{applicant.Expenses}";
                tboxSummmaryExpenses.Text = expenses;

                loan = $"{applicant.LoanTerm}";
                tboxSummmaryTerm.Text = loan;

                intRate = $"{applicant.AffIntRate}";
                tboxSummmaryInterestRate.Text = intRate;

                price = $"{applicant.PurchasePrice}";
                tboxRpmtSummaryPrice.Text = price;

                deposit = $"{applicant.Deposit}";
                tboxSummaryDeposit.Text = deposit;

                QAmount = $"{applicant.QualifyAmount}";
                tboxSummaryMaxQualify.Text = QAmount;

                Repayment = $"{applicant.MonthlyRepayment}";
                tboxSummaryMaxRepayment.Text = Repayment;

                loanCost = $"{applicant.TotalDueInclInterest}";
                tboxSummaryLoanCost.Text = loanCost;
                                             
            }
            
        }

        private void btnAffNext_Click(object sender, RoutedEventArgs e)
        {
                //User created here to push userid into the DB table
                User user = new User(); 
                applicant.UserId = int.Parse(tboxUserId.Text);

                //text inputs from users parsed to write to db
                applicant.Salary = decimal.Parse(tboxAffSalary.Text);
                applicant.Expenses = decimal.Parse(tboxAffExpenses.Text);
                applicant.PurchasePrice = decimal.Parse(tboxRpmtPurchasePrice.Text);
                applicant.Deposit = decimal.Parse(tboxRpmtDeposit.Text);
                applicant.AffIntRate = float.Parse(tboxAffIntRate.Text);
                applicant.LoanTerm = int.Parse(tboxAffTerm.Text);
                
                //qualify amount calculation
                applicant.QualifyAmount = QualifyAmount();

                //monthly mortgage repayment amount
                applicant.MonthlyRepayment = MonthlyRepayment();
                
                //Total due including interest
                applicant.TotalDueInclInterest = TotalDueInclInterest();

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
            //salary less expenses + deposit makes up qualify amount
            decimal? deposit = applicant.Deposit;
            decimal? salary = applicant.Salary;
            decimal? expenses = applicant.Expenses;
            decimal? net = salary - expenses;
            decimal? qualifyAmount = ((net * 3 * 12) + deposit);
            
            return qualifyAmount;
        }

       //monthly mortgage repayment amount
        public decimal? MonthlyRepayment()
        {
            decimal? MonthlyRepayment = 0;

            //Qualify amount becomes the principle amount
            decimal? principle = QualifyAmount();            
            decimal? term = Convert.ToDecimal(applicant.LoanTerm);

            decimal? interestRate = Convert.ToDecimal(applicant.AffIntRate);

            //Compound interest calculation
            decimal? nt = 1 / (12 * term);
            decimal? brackets = (1 + (interestRate / 12));
            decimal? CompoundInterest = principle * (brackets * nt);

            //Rounds up decimal or result will contain too many characters to write to DB
            double MonthlyR = (double)CompoundInterest;
            double? x = Math.Truncate(MonthlyR * 100 / 100);
            decimal? Amount = (decimal) x;

            MonthlyRepayment = (Amount * 3);

            return MonthlyRepayment;

        }

        //Total due including interest
        public decimal? TotalDueInclInterest()
        {
            decimal? totDu;
            decimal term = Convert.ToDecimal(applicant.LoanTerm) * 12;

            totDu = MonthlyRepayment() * term;

            //Rounds up decimal or result will contain too many characters to write to DB
            double td = (double)totDu;
            double? x = Math.Truncate(td * 100 / 100);
            decimal? totalDue = (decimal) x;
                        
            return totalDue;
        }

        public int saveUser(UserFinancial applicant)
        {
            db.Entry(applicant).State = System.Data.Entity.EntityState.Added;
            int saveSuccess = db.SaveChanges();
            return saveSuccess;
        }
    }
}
