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
        AddNewEventWindowValidator validator = new AddNewEventWindowValidator();

        public Event InsertEvent(string title, string eventDetails, int eventMode, int category, int groupId, out string i_Errors)
        {
            Event newEvent = null;
            i_Errors = validator.CheckInsertNewEvent(eventDetails, eventMode, category);

            if (i_Errors == "")
            {
                string safeTitle = title;
                string safeEventDetails = eventDetails;
                DateTime dateCreated = DateTime.Now;
                string query = "INSERT INTO events (title,dateCreated,mode,categoryId,details,groupId)" +
                    $" VALUES('{safeTitle}','{dateCreated}','{eventMode}','{category}','{safeEventDetails}', {groupId})";

                Sql.DoQuery(query);

                query = $"SELECT * FROM events WHERE dateCreated = '{dateCreated}' AND mode = {eventMode} ORDER BY id DESC";
                int id = 0;
                DataRow data = Sql.SelectOneItemQuery(query);
                if (data != null)
                {
                    id = (int)data["id"];
                }

                newEvent = EventsFactory.CreateEvent(data);
            }

            return newEvent;
        }

        public ObservableCollection<Category> GetCategories(int i_Id = -1)

        {
            ObservableCollection<Category> categories = new ObservableCollection<Category>();
            string query;

            if (i_Id != -1)
            {
                query = $"SELECT * FROM categories WHERE parentId = '{i_Id}'";
            }
            else
            {
                query = $"SELECT * FROM categories WHERE parentId IS NULL";
            }

            DataRowCollection drc = Sql.SelectQuery(query);

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
            DataRowCollection modes = Sql.SelectQuery(query);

            foreach (DataRow mode in modes)
            {
                EventMode newMode = new EventMode();
                newMode.Mode = (EEventMode)mode["id"];
                newMode.Title = mode["title"].ToString();
                newMode.Color = mode["bgColor"].ToString();

                modesList.Add(newMode);
            }

            return modesList;
        }

    }
}
