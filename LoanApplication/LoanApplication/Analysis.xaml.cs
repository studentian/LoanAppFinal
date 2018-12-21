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
            Count,
            Statistics
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
                if (cboAnalysisType.SelectedIndex == 2)
                {
                    analysisType = AnalysisType.Count;
                }
                if (cboAnalysisType.SelectedIndex == 3)
                {
                    analysisType = AnalysisType.Statistics;
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
                foreach (var log in db.Logs)
                {
                    logList.Add(log);
                }
        }

        private void btnAnalyse_Click(object sender, RoutedEventArgs e)
        {
            //check to see state of enum options
            //check combobox list options
            if (analysisType == AnalysisType.Summary && tableSelected == TableSelected.User)
            {
                int recordCount = 0;
                string output = "";

                //display message in textblock
                foreach (var item in userList)
                {
                    //increment recordCount
                    recordCount++;

                    //add new line
                    output = output + Environment.NewLine + 
                        $"Record {recordCount} is for user named {item.FullName}, Username {item.Username}, {item.UserId}" + 
                        Environment.NewLine;
                                        
                }

            }

        }
    }
}
