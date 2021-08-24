using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsManagerLogic.Classes
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string DateCreated { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public int GroupId { get; set; }

        public static User CreateUser(DataRow data)
        {
            User user = new User();
            user.Id = (int) data["id"];
            user.Username = data["username"].ToString();
            user.DateCreated = data["dateCreated"].ToString();
            user.Email = data["email"].ToString();
            user.IsAdmin = ((int)data["isAdmin"] == 1) ? true : false;
            user.GroupId = (int)data["groupId"];

            return user;
        }
    }
}
