using EventsManagerLogic.Classes;
using EventsManagerLogic.Events;
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
    public class AddNewEventWindowHelper
    {

        Sql sql = new Sql();
        AddNewEventWindowValidator validator = new AddNewEventWindowValidator();

        public Event InsertEvent(string title, string eventDetails, int eventMode, int category, out string i_Errors)
        {
            Event newEvent = null;
            i_Errors = validator.CheckInsertNewEvent(eventDetails, eventMode, category);

            if (i_Errors == "")
            {
                string safeTitle = title;
                string safeEventDetails = eventDetails;
                DateTime dateCreated = DateTime.Now;
                string query = "INSERT INTO events (title,dateCreated,mode,categoryId,details)" +
                    $" VALUES('{safeTitle}','{dateCreated}','{eventMode}','{category}','{safeEventDetails}')";

                sql.DoQuery(query);

                query = $"SELECT id FROM events WHERE dateCreated = '{dateCreated}' AND mode = {eventMode} ORDER BY id DESC";
                int id = 0;
                DataRow data = sql.SelectOneItemQuery(query);
                if (data != null)
                {
                    id = (int)data["id"];
                }

                newEvent = EventsFactory.CreateEvent(id, safeTitle, safeEventDetails, eventMode, category, dateCreated.ToString());
            }

            return newEvent;
        }

        public List<Category> GetCategories(int i_Id = -1)

        {
            List<Category> categories = new List<Category>();
            string query;

            if (i_Id != -1)
            {
                query = $"SELECT * FROM categories WHERE parentId = '{i_Id}'";
            }
            else
            {
                query = $"SELECT * FROM categories WHERE parentId IS NULL";
            }

            DataRowCollection drc = sql.SelectQuery(query);

            if (drc.Count > 0)
            {
                foreach (DataRow data in drc)
                {
                    categories.Add(
                        new Category
                        {
                            Id = (int)data["id"],
                            Title = data["title"].ToString(),
                        });
                }
            }

            return categories;
        }

        public ObservableCollection<EventMode> GetEventModes()
        {
            ObservableCollection<EventMode> modesList = new ObservableCollection<EventMode>();

            string query = "SELECT * FROM eventModes";
            DataRowCollection modes = sql.SelectQuery(query);

            foreach (DataRow mode in modes)
            {
                EventMode newMode = new EventMode();
                newMode.Mode = (EeventModeSearchFilter)mode["id"];
                newMode.Title = mode["title"].ToString();
                newMode.Color = mode["bgColor"].ToString();

                modesList.Add(newMode);
            }

            return modesList;
        }

    }
}
