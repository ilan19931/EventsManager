using EventsManagerLogic.Classes;
using EventsManagerLogic.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsManagerLogic.WindowsHelpers.AdminPanel
{
    public class ManageUsersWindowHelper
    {
        public List<User> GetAllUsers()
        {
            List<User> allUsers = new List<User>();
            string query = $"SELECT * FROM accounts";

            DataRowCollection data = Sql.SelectQuery(query);
            if(data.Count > 0)
            {
                foreach(DataRow user in data)
                {
                    allUsers.Add(User.CreateUser(user));
                }
            }

            return allUsers;
        }
    }
}
