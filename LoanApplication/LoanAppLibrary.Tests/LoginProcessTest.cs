using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanAppLibraryV4;
using Xunit;

namespace LoanAppLibrary.Tests
{
    //to import test open Nuget package manager and download 
    //Xunit
    //Xunit.runner.console
    //Xunit.runner.visualstudio

    public class LoginProcessTest
    {
        [Fact]  //could also be changed to [Theory] to test different variations of username and password.
        //very important to be able to click Test, Windows and select Test explorer
        //To run the test cimply rightclin on test in test explorer and click run selected test
        public void ValidateUserInput_StringsShouldVerify()
        {
            //Arrange phase
            LoginProcess loginProcess = new LoginProcess();
            bool expected = true;

            //Act
            bool actual = loginProcess.validatedUserInput("ianl", "apples"); //did it return a bool value

            //Assert
            Assert.Equal(expected, actual); //tests and will pass if conditions pass

            //================================

            //[Theory]  
            //[InlineData ("i","a",true)]
            //[InlineData("iii", "aaa", true)]
            //[InlineData("1", "password", true)]
            //[InlineData("dddd", "ddddd", true)]
            //[InlineData("qqqqqqqqqqqqqqqqqqqqqqqqpppppppppp", "apples", true)]
            //[InlineData("ianl", "ppppppppppppppppppppppppppppppppppppppppppppp", true)]
            //[InlineData("-user", "apples", true)]
            //public void ValidateUserInput_StringsShouldVerify(string username, string password, bool expected)
            //{
            //    //Arrange phase
            //    LoginProcess loginProcess = new LoginProcess();

            //    //Act
            //    bool actual = loginProcess.validatedUserInput("ianl", "apples"); //did it return a bool value

            //    //Assert
            //    Assert.Equal(expected, actual); //tests and will pass if conditions pass

            //}
        }
    }
}
