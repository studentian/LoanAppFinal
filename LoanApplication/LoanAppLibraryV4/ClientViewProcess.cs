using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanAppLibraryV4
{
        //make public
        public class ClientViewProcess
        {
            //============notes on ValidateUserInput() method==================================
            //private bool ValidateUserInput() --> it is best to feed into the method the tboxUserId and tboxAffExpenses 
            //if not tboxUserId.Text.Length will flag red. Note also to remove .Text  
            //ClientViews.xaml.cs will not be changed and will remain original though it will be greyed out for teh purposes of the unit test.
            //=================================================================================

            /// <summary>
            /// Validates inputted Client Financial information
            /// </summary>
            /// <param name="tboxUserId">Textbox containing UserId /param>
            /// <param name="tboxAffExpenses">Textbox containing Expenses</param>
            /// <param name="tboxRpmtPurchasePrice">Textbox containing Purchase Price</param>
            /// <param name="tboxRpmtDeposit">Textbox containing Deposit</param>
            /// <param name="tboxAffTerm">Textbox containing Loan Term</param>
            /// <returns></returns>

            public bool ValidateUserInput(int tboxUserId, decimal tboxAffExpenses, decimal tboxRpmtPurchasePrice, decimal tboxRpmtDeposit, int tboxAffTerm)
            {
                bool validated = true;

                if (tboxUserId == 0 || tboxUserId > 10000)
                {
                    validated = false;
                }
                if (tboxAffExpenses <= 0 || tboxAffExpenses > tboxRpmtPurchasePrice)
                {
                    validated = false;
                }
                if (tboxRpmtPurchasePrice <= 0 || tboxRpmtPurchasePrice > 2000000)
                {
                    validated = false;
                }
                if (tboxRpmtDeposit < 0 || tboxRpmtDeposit > 1000000)
                {
                    validated = false;
                }
                if (tboxAffTerm <= 0 || tboxAffTerm > 35)
                {
                    validated = false;
                }
                return validated;
            }
        }
}
