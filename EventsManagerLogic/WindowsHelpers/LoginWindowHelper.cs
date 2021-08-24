using EventsManager.Classes;
using EventsManagerLogic.Classes;
using EventsManagerLogic.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsManagerLogic.WindowsHelpers
{
    public class LoginWindowHelper
    {
        private Security security = new Security();
        private TextValidator textValidator = new TextValidator();

        public User CheckLoginDetails(string i_Username, string i_Password)
        {
            if (i_Username == null || i_Password == null)
                return null;
            User user = null;
            string hashedPassword = security.Sha1Hash(security.Sha1Hash(i_Password));
            string query = $"SELECT * FROM accounts WHERE username = '{i_Username}' AND password = '{hashedPassword}'";
            DataRowCollection account = Sql.SelectQuery(query);

            if (account.Count != 0)
            {
                DataRow data = account[0];
                string u = data["username"].ToString();
                user = User.CreateUser(data);
            }

            return user;
        }

        public void InsertAccessKey(int i_Id, string i_AccessKey)
        {
            if (i_Id > 0 && !string.IsNullOrEmpty(i_AccessKey))
            {
                string query = "INSERT INTO loggedInAccounts (userId,accessKey,dateCreated)" +
                    $" VALUES('{i_Id}','{i_AccessKey}','{DateTime.Now}')";

                Sql.DoQuery(query);
            }
        }


    }
}
