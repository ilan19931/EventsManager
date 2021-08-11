using EventsManagerLogic.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsManagerLogic.Validators
{
    public class CreateAccountWindowValidator
    {
        public string CheckCreateAccount(string i_Username, string i_Password, string i_RePassword, string i_Email)
        {
            Sql sql = new Sql();
            string errors = "";
            string query;
            DataRowCollection drc;

            if (i_Username.Length < 2)
                errors += "username must be atleast 2 characters\n";
            else if (i_Username.Length > 30)
                errors += "username must be maximum 30 characters\n";

            if (i_Password.Length < 4)
                errors += "username must be atleast 4 characters\n";
            else if (i_Password.Length > 200)
                errors += "username must be maximum 200 characters\n";

            if (i_Password != i_RePassword)
                errors += "Password doesn't match\n";

            //check if username is allready taken
            query = $"SELECT id FROM accounts WHERE username = '{i_Username}'";
            drc = sql.SelectQuery(query);
            if (drc.Count != 0)
                errors += "username is allready taken\n";

            //check if email is allready taken
            query = $"SELECT id FROM accounts WHERE email = '{i_Email}'";
            drc = sql.SelectQuery(query);
            if (drc.Count != 0)
                errors += "email is allready taken\n";

            return errors;
        }

    }
}
