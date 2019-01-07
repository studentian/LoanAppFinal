using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using LoanAppLibraryV4;

namespace LoanAppLibrary.Tests
{
    public class ClientViewProcessTest
    {
        [Theory] //is better than fact when method has too much information entering it.
        [InlineData (1000, 5000, 10000, 500000, 20, true )] //normal data input
        [InlineData(1000, 100000, 100000, 5000000000, 20, false)] //abnormal expenses
        [InlineData(int.MinValue, 100000, 100000, int.MaxValue, 20, false)] //account number and expenses abnormal
        [InlineData(int.MinValue, -5, 100000, int.MaxValue, 20, false)]//negative expenses should fail
        
        //Test method
        public void ValidateClientViewInput_ValuesShouldVerify(int tboxUserId, decimal tboxAffExpenses, decimal tboxRpmtPurchasePrice, decimal tboxRpmtDeposit, int tboxAffTerm, bool expected) //bool expected is the value we expect to come out of the test
        {
            //arrange
            ClientViewProcess clientViewProcess = new ClientViewProcess();//creates an instance of the clientViewProcess

            //Act
            bool actual = clientViewProcess.ValidateUserInput( tboxUserId,  tboxAffExpenses, tboxRpmtPurchasePrice,  tboxRpmtDeposit,  tboxAffTerm); //actual - the strings or actual values are being feed into the method here. Note that data types are not needed here. 

            //Assert
            //checks to see if the test has happened or not.
            //Assert.True(actual == false); //assertion that actual will fail
            Assert.Equal(expected, actual); // assertion that expected will match actual which is a fail.
        }
    }
}
