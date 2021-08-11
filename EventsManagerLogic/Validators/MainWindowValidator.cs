using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsManager.Classes;
using EventsManagerLogic.Classes;
using EventsManagerLogic.Helpers;

namespace EventsManagerLogic.Validators
{
    public class MainWindowValidator
    {
        Sql sql = new Sql();
        TextValidator textValidator= new TextValidator();

        public string CheckAddNewComment(int i_UserId, string i_Username, int i_EventId, string i_CommentDetails)
        {
            string errors = "";
            string query;
            DataRowCollection drc;
            DataRow data;

            //check if userid is valid
            query = $"SELECT id,username FROM accounts WHERE id = {i_UserId}";
            drc = Helper.sql.SelectQuery(query);
            data = (drc.Count != 0) ? drc[0] : null;

            if (data == null)
            {
                errors += "wrong user id\n";
            }

            //check if username is valid
            if (data == null || data["username"].ToString() != i_Username)
            {
                errors += "wrong username\n";
            }

            //check if eventId is valid
            query = $"SELECT id FROM events WHERE id = {i_EventId}";
            drc = Helper.sql.SelectQuery(query);
            data = (drc.Count != 0) ? drc[0] : null;

            if (data == null)
            {
                errors += "wrong event id\n";
            }

            //check comment details
            if (string.IsNullOrEmpty(i_CommentDetails))
            {
                errors += "wrong comment details\n";
            }

            return errors;
        }

    }
}
