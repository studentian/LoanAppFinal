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
    /// Interaction logic for Analysis.xaml
    /// </summary>
    public partial class Analysis : UserControl
    {
        List<User> userList = new List<User>();
        List<UserFinancial> applicantList = new List<UserFinancial>();
        List<Log> logList = new List<Log>();
        List<Offer> offerList = new List<Offer>();

        LoanAppDBEntities db = new LoanAppDBEntities();

        enum AnalysisType
        {
            Summary,

        }

        private AnalysisType analysisType = new AnalysisType();

        enum TableSelected
        {
            User,
            UserFinancial,
            Offers,
            Log
        }

        private TableSelected tableSelected = new TableSelected();

        public Analysis()
        {
            InitializeComponent();
        }

        private void cboAnalysisType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if combobox selected index is greater than zero -- do something. Zero is "Please select" so this hops over it to the next item in the list
            if (cboAnalysisType.SelectedIndex > 0)
            {
                if (cboAnalysisType.SelectedIndex == 1)
                {
                    //enum variable = summary (on comboxbox listing)
                    analysisType = AnalysisType.Summary;
                }
                
            }
        }
        private void cboChooseTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if combobox selected index is greater than zero -- do something. Zero is "Please select" so this hops over it to the next item in the list
            if (cboChooseTable.SelectedIndex > 0)
            {
                if(cboChooseTable.SelectedIndex == 1)
                {
                    tableSelected = TableSelected.User;
                }
                if (cboChooseTable.SelectedIndex == 2)
                {
                    tableSelected = TableSelected.UserFinancial;
                }
                if (cboChooseTable.SelectedIndex == 3)
                {
                    tableSelected = TableSelected.Offers;
                }
                if (cboChooseTable.SelectedIndex == 4)
                {
                    tableSelected = TableSelected.Log;
                }
            }
        }

            //refers to usercontrol in xaml page. 
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
                foreach (var user in db.Users)
                {
                    userList.Add(user);
                }
                foreach (var applicant in db.UserFinancials)
                {
                    applicantList.Add(applicant);
                }
                foreach (var offer in db.Offers)
                {
                    offerList.Add(offer);
                }
                
        }

        private void btnAnalyse_Click(object sender, RoutedEventArgs e)
        {
            //check to see state of enum options
            //check combobox list options
            if (analysisType == AnalysisType.Summary && tableSelected == TableSelected.User)
            {
                //Clear Variables
                int recordCount = 0;
                string output = "";
                tbkAnalysisOutput.Text = "";

                //store count summary. To Count LevelId's in db
                int level1CountSummary = 0;
                int level2CountSummary = 0;
                int level3CountSummary = 0;

                //display message in textblock
                foreach (var item in userList)
                {
                    //increment recordCount
                    recordCount++;

                    //add new line
                    //FullName was created in User.cs Entity Framework combining First Name and Last Name in a get request 
                    output = output + Environment.NewLine +
                        $"Record {recordCount}, Name {item.FullName}, UserId {item.UserId}, Username {item.Username}, " +
                        $"RoleProfile {item.AccessLevel.RoleProfile} " +
                        Environment.NewLine;

                    if (item.LevelId == 1)
                    {
                        level1CountSummary++;
                    }
                    if (item.LevelId == 2)
                    {
                        level2CountSummary++;
                    }
                    if (item.LevelId == 3)
                    {
                        level3CountSummary++;
                    }
                }
                output = output + Environment.NewLine + $"Total users with Applicant level Access {level1CountSummary}." + Environment.NewLine;
                output = output + Environment.NewLine + $"Total users with Provider level Access {level2CountSummary}." + Environment.NewLine;
                output = output + Environment.NewLine + $"Total users with Administrator level Access {level3CountSummary}." + Environment.NewLine;

                output = output + Environment.NewLine + $"Total records = {recordCount}" + Environment.NewLine;
                tbkAnalysisOutput.Text = output;
            }

            if (analysisType == AnalysisType.Summary && tableSelected == TableSelected.UserFinancial)
            {
                //Clear Variables
                int recordCount = 0;
                string output = "";
                tbkAnalysisOutput.Text = "";

                foreach (var applicant in applicantList)
                {
                    recordCount++;
                    output = output + Environment.NewLine + $"Record {recordCount}, QuoteId {applicant.QuoteId},UserId {applicant.UserId}, Salary {applicant.Salary}, " +
                        $"Expenses {applicant.Expenses}, Qualify Amount {applicant.QualifyAmount}, Deposit {applicant.Deposit} " + Environment.NewLine;
                }
                output = output + Environment.NewLine + $"Total records = {recordCount}" + Environment.NewLine;
                tbkAnalysisOutput.Text = output;
            }

            if (analysisType == AnalysisType.Summary && tableSelected == TableSelected.Offers)
            {
                //Clear Variables
                int recordCount = 0;
                string output = "";
                tbkAnalysisOutput.Text = "";

                foreach (var offer in db.Offers)
                {
                    recordCount++;
                    output = output + Environment.NewLine + $"Record {recordCount}, OfferId {offer.OfferId}, ProviderName {offer.ProviderName}, " +
                        $"Offer Amount {offer.OfferAmount}" + Environment.NewLine;
                }
                output = output + Environment.NewLine + $"Total records = {recordCount}" + Environment.NewLine;
                tbkAnalysisOutput.Text = output;

            }
        }
    }
}
