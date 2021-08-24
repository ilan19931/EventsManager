using EventsManager.Classes;
using EventsManagerLogic.Classes;
using EventsManagerLogic.Helpers;
using EventsManagerLogic.Validators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsManagerLogic.WindowsHelpers
{
    public class CreateAccountWindowHelper
    {
        private CreateAccountWindowValidator validator = new CreateAccountWindowValidator();
        private Security security = new Security();

        public string CreateAccount(string i_Username, string i_Password, string i_RePassword, string i_Email)
        {
            string errors = "";
            string query;

            errors = validator.CheckCreateAccount(i_Username, i_Password, i_RePassword, i_Email);

            //check if there are no errors, and then create account
            if (errors == "")
            {
                string safeUsername = i_Username;
                string safePassword = security.Sha1Hash(security.Sha1Hash(i_Password));
                string safeEmail = i_Email;
                query = "INSERT INTO accounts (username,password,email,dateCreated)" +
                    $" VALUES('{safeUsername}','{safePassword}','{safeEmail}','{DateTime.Now}')";


                Sql.DoQuery(query);
            }

            return errors;
        }


    }
}
