using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanAppLibraryV4
{
    public class LoginProcess
    {
        /// <summary>
        /// Validates the user credentials against those in the SQL database
        /// </summary>
        /// <param name="username"></param>
        /// Username entered by user
        /// <param name="password"></param>
        /// Password entered by user
        /// <returns>
        /// Validated user
        /// </returns>

        public bool validatedUserInput(string username, string password)
        {
            //It is easier to set validated to false inside one of the checks 
            //than to validate each check
            bool validated = true;

            try
            {
                if (username.Length == 0 || username.Length > 30)
                {
                    validated = false;
                }
                foreach (char ch in username)
                {
                    if (ch > '0' && ch < '9' || ch == '-')
                    {
                        validated = false;
                    }

                    if (password.Length == 0 || password.Length > 30)
                    {
                        validated = false;
                    }
                }
            }
            catch (Exception)
            {
                validated = false;
            }
            return validated;
        }
    }
}
