using EventsManager.Classes;
using EventsManagerLogic.Classes;
using EventsManagerLogic.Events;
using EventsManagerLogic.Helpers;
using EventsManagerLogic.Validators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsManagerLogic.WindowsHelpers
{
    public class MainWindowHelper
    {
        private Sql sql = new Sql();
        private MainWindowValidator validator = new MainWindowValidator();

        // getters
        public ObservableCollection<Comment> GetEventComments(int i_EventId)
        {
            ObservableCollection<Comment> commentsList = new ObservableCollection<Comment>();

            if (i_EventId > 0)
            {
                string query = $"SELECT * FROM comments WHERE eventId = '{i_EventId}'";
                DataRowCollection drc = sql.SelectQuery(query);

                if (drc.Count > 0)
                {
                    foreach (DataRow data in drc)
                    {
                        commentsList.Add(new Comment(data));
                    }
                }
                else
                {
                    commentsList.Add(new Comment { Details = "There are no comments to show" });
                }
            }

            return commentsList;
        }
        public ObservableCollection<EventInList> GetEvents()
        {
            ObservableCollection<EventInList> events = new ObservableCollection<EventInList>();
            string query = "SELECT * FROM events ORDER BY id DESC";
            DataRowCollection allData = sql.SelectQuery(query);

            foreach (DataRow data in allData)
            {
                EventInList newEvent = new EventInList();
                newEvent.MyEvent = EventsFactory.CreateEvent(data);
                newEvent.EventStatus = (EEventStatus)data["isClosed"];

                events.Add(newEvent);
            }

            return events;
        }

        //Mode filters
        public ObservableCollection<EventInList> GetTextFilteredEvents(string i_TxtSearch, EeventModeSearchFilter i_EventModeSearchFilter)
        {
            ObservableCollection<EventInList> list = new ObservableCollection<EventInList>();

            if (!string.IsNullOrEmpty(i_TxtSearch))
            {
                string query;
                DataRowCollection drc;

                switch (i_EventModeSearchFilter)
                {
                    case EeventModeSearchFilter.AllOpen:
                        query = $"SELECT * FROM events WHERE details LIKE '%{i_TxtSearch}%' AND isClosed = 0";
                        break;

                    case EeventModeSearchFilter.AllClosed:
                        query = $"SELECT * FROM events WHERE details LIKE '%{i_TxtSearch}%' AND isClosed = 1";
                        break;

                    case EeventModeSearchFilter.AllEvents:
                        query = $"SELECT * FROM events WHERE details LIKE '%{i_TxtSearch}%'";
                        break;

                    default:
                        query = $"SELECT * FROM events WHERE mode = {(int)i_EventModeSearchFilter} AND details LIKE '%{i_TxtSearch}%'";
                        break;
                }

                drc = sql.SelectQuery(query);

                if (drc.Count > 0)
                {
                    foreach (DataRow data in drc)
                    {
                        EventInList newEvent = new EventInList();
                        newEvent.MyEvent = EventsFactory.CreateEvent(data);
                        newEvent.EventStatus = (EEventStatus)data["isClosed"];

                        list.Add(newEvent);
                    }
                }
            }

            return list;
        }
        public ObservableCollection<EventInList> GetEventsByQuery(EeventModeSearchFilter i_CurrModeFilter)
        {
            ObservableCollection<EventInList> newList = new ObservableCollection<EventInList>();
            DataRowCollection drc;
            string query;

            // show open events
            if (i_CurrModeFilter == EeventModeSearchFilter.AllOpen)
            {
                query = "SELECT * FROM events WHERE isClosed = 0 ORDER BY id DESC";
            }
            // show closed events
            else if (i_CurrModeFilter == EeventModeSearchFilter.AllClosed)
            {
                query = "SELECT * FROM events WHERE isClosed = 1 ORDER BY id DESC";
            }
            else
            {
                // show events by mode
                query = $"SELECT * FROM events WHERE mode = {(int)i_CurrModeFilter} AND isClosed = 0 ORDER BY id DESC";
            }

            drc = sql.SelectQuery(query);

            if (drc.Count > 0)
            {
                foreach (DataRow data in drc)
                {
                    EventInList newEvent = new EventInList();
                    newEvent.MyEvent = EventsFactory.CreateEvent(data);
                    newEvent.EventStatus = (EEventStatus)data["isClosed"];

                    newList.Add(newEvent);
                }
            }

            return newList;
        }
        public ObservableCollection<EventMode> GetEventFiltersModes()
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

            modesList.Insert(0, new EventMode { Title = "Open Events", Mode = EeventModeSearchFilter.AllOpen });
            modesList.Insert(1, new EventMode { Title = "All Events", Mode = EeventModeSearchFilter.AllEvents });
            modesList.Insert(2, new EventMode { Title = "Closed Events", Mode = EeventModeSearchFilter.AllClosed });


            return modesList;
        }

        public User CheckAccessKey(string i_AccessKey)
        {
            User user = null;

            if (!string.IsNullOrEmpty(i_AccessKey))
            {
                string query = $"SELECT * FROM loggedInAccounts WHERE accessKey = '{i_AccessKey}'";
                DataRow data = sql.SelectOneItemQuery(query);

                if (data != null)
                {
                    query = $"SELECT * FROM accounts WHERE id = '{data["userId"]}'";
                    data = sql.SelectOneItemQuery(query);

                    if (data != null)
                    {
                        user = User.CreateUser(data);
                    }
                }
            }

            return user;
        }

        public Comment AddNewComment(int i_UserId, string i_Username, int i_EventId, string i_CommentDetails, out string i_Errors)
        {
            MainWindowValidator validator = new MainWindowValidator();
            Comment newComment = null;
            i_Errors = validator.CheckAddNewComment(i_UserId, i_Username, i_EventId, i_CommentDetails);

            if (i_Errors == "")
            {
                string safeCommentDetails = i_CommentDetails;
                DateTime dateCreated = DateTime.Now;

                string query = "INSERT INTO comments (userId,eventId,usernamePosted,details,dateCreated)" +
                    $" VALUES('{i_UserId}','{i_EventId}','{i_Username}','{safeCommentDetails}','{dateCreated}')";

                sql.DoQuery(query);

                query = $"SELECT id FROM comments WHERE userId = {i_UserId} AND eventId = {i_EventId} AND dateCreated = '{dateCreated}' ORDER BY id DESC";
                DataRow data = sql.SelectOneItemQuery(query);
                int id = (data != null) ? (int)data["id"] : 0;

                if (id != 0)
                    newComment = new Comment(id, i_UserId, i_Username, i_EventId, safeCommentDetails, dateCreated.ToString());
            }

            return (newComment != null) ? newComment : new Comment();
        }



    }
}
