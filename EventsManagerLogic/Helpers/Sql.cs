using EventsManagerLogic.Classes;
using EventsManagerLogic.Events;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsManagerLogic.Helpers
{
    public class Sql
    {
        private string m_ConnectionString = "Data Source=DESKTOP-NQK6RD1\\SQLSERVER; Initial Catalog=EventsManager; User id=sa; Password=1234;";
        private Security security = new Security();

        public DataRowCollection SelectQuery(string query)
        {
            DataTable dataTable = null;
            using (SqlConnection connection = new SqlConnection(m_ConnectionString))
            {
                try
                {
                    connection.Open();

                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, connection);
                    dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);

                }
                catch (Exception e)
                {

                }
            }

            return dataTable.Rows;
        }

        public string CreateAccount(string i_Username, string i_Password, string i_RePassword, string i_Email)
        {
            string error = "";
            string query;
            DataRowCollection drc;


            if (i_Username.Length < 2)
                error += "username must be atleast 2 characters\n";
            else if(i_Username.Length > 30)
                error += "username must be maximum 30 characters\n";

            if (i_Password.Length < 4)
                error += "username must be atleast 4 characters\n";
            else if (i_Password.Length > 200)
                error += "username must be maximum 200 characters\n";

            if (i_Password != i_RePassword)
                error += "Password doesn't match\n";

            //check if username is allready taken
            query = $"SELECT id FROM accounts WHERE username = '{i_Username}'";
            drc = this.SelectQuery(query);
            if (drc.Count != 0)
                error += "username is allready taken\n";

            //check if email is allready taken
            query = $"SELECT id FROM accounts WHERE email = '{i_Email}'";
            drc = this.SelectQuery(query);
            if (drc.Count != 0)
                error += "email is allready taken\n";

            //check if there are no error, and then create account
            if(error == "")
            {
                string safeUsername = i_Username;
                string safePassword = security.Sha1Hash(security.Sha1Hash(i_Password));
                string safeEmail = i_Email;

                using (SqlConnection connection = new SqlConnection(m_ConnectionString))
                {
                    connection.Open();
                    query = "INSERT INTO accounts (username,password,email,dateCreated) VALUES(@param1,@param2,@param3,@param4)";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.Add("@param1", SqlDbType.VarChar, 30).Value = safeUsername;
                        cmd.Parameters.Add("@param2", SqlDbType.VarChar, 40).Value = safePassword;
                        cmd.Parameters.Add("@param3", SqlDbType.VarChar, 200).Value = safeEmail;
                        cmd.Parameters.Add("@param4", SqlDbType.VarChar, 50).Value = DateTime.Now;
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            return error;
        }

        public List<IEvent> getEvents()
        {
            List<IEvent> events = new List<IEvent>();
            string query = "SELECT * FROM events";
            DataRowCollection allData = this.SelectQuery(query);

            foreach (DataRow data in allData)
            {
                events.Add(EventsFactory.CreateEvent(data));
            }

            return events;
        }

        public User checkLoginDetails(string i_Username, string i_Password)
        {
            if (i_Username == null || i_Password == null)
                return null;

            User user = null;
            string hashedPassword = security.Sha1Hash(security.Sha1Hash(i_Password));
            string query = $"SELECT * FROM accounts WHERE username = '{i_Username}' AND password = '{hashedPassword}'";
            DataRowCollection account = this.SelectQuery(query);

            if(account.Count != 0)
            {
                DataRow data = account[0];
                string u = data["username"].ToString();
                user = new User
                {
                    Username = data["username"].ToString(),
                    Email = data["email"].ToString(),
                    AccessKey = "not have yet"
                };
            }

            return user;
        }
    }
}
