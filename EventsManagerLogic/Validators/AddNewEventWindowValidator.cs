using EventsManager.Classes;
using EventsManagerLogic.Classes;
using EventsManagerLogic.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsManagerLogic.Validators
{
    public class AddNewEventWindowValidator
    {
        TextValidator textValidator = new TextValidator();
        public string CheckInsertNewEvent(string i_EventDetails, int i_EventMode, int i_Category)
        {
            string errors = "";

            if (textValidator.basicTextCheck(i_EventDetails, 1, 500) == false)
                errors += "Invalid event details\n";
            if (i_EventMode < 0 || i_EventMode >= Enum.GetValues(typeof(EEventMode)).Length)
                errors += "Invalid Event Mode\n";

            //check if category is valid
            string query = $"SELECT id FROM categories WHERE id = {i_Category}";
            DataRowCollection drc = Sql.SelectQuery(query);
            if (drc.Count == 0)
                errors += "Invalid category\n";

            return errors;
        }

    }
}
