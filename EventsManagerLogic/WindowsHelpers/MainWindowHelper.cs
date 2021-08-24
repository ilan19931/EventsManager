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
        private MainWindowValidator validator = new MainWindowValidator();
        public int GroupId { get; set; }

        public MainWindowHelper()
        {

        }

        // getters
        public ObservableCollection<Comment> GetEventComments(int i_EventId)
        {
            ObservableCollection<Comment> commentsList = new ObservableCollection<Comment>();

            if (i_EventId > 0)
            {
                string query = $"SELECT * FROM comments WHERE eventId = '{i_EventId}'";
                DataRowCollection drc = Sql.SelectQuery(query);

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
        public ObservableCollection<Event> GetEvents()
        {
            ObservableCollection<Event> events = new ObservableCollection<Event>();
            string query = $"SELECT * FROM events WHERE (groupId = {GroupId} OR groupId = 0) ORDER BY groupId ASC , id DESC";
            DataRowCollection allData = Sql.SelectQuery(query);

            foreach (DataRow data in allData)
            {
                Event newEvent = EventsFactory.CreateEvent(data);

                events.Add(newEvent);
            }

            return events;
        }

        //Mode filters
        public ObservableCollection<Event> GetTextFilteredEvents(string i_TxtSearch, EEventMode? i_CurrModeFilter, EstateFilter i_CurrStateFilter)
        {
            ObservableCollection<Event> list = new ObservableCollection<Event>();
            int isClosed = (i_CurrStateFilter == EstateFilter.AllClosed) ? 1 : 0;

            if (!string.IsNullOrEmpty(i_TxtSearch))
            {
                string query;
                DataRowCollection drc;

                if(i_CurrModeFilter != null)
                {
                    query = $"SELECT * FROM events WHERE details LIKE '%{i_TxtSearch}%' AND mode = {(int)i_CurrModeFilter} AND (groupId = {GroupId} OR groupId = 0) AND isClosed = {isClosed} ORDER BY groupId ASC";
                }
                else
                {
                    query = $"SELECT * FROM events WHERE details LIKE '%{i_TxtSearch}%' AND (groupId = {GroupId} OR groupId = 0) AND isClosed = {isClosed} ORDER BY groupId ASC";
                }

                drc = Sql.SelectQuery(query);

                if (drc.Count > 0)
                {
                    foreach (DataRow data in drc)
                    {
                        Event newEvent = EventsFactory.CreateEvent(data);

                        list.Add(newEvent);
                    }
                }
            }
            else
            {
                list = this.GetEventsByStateFilter(i_CurrModeFilter, i_CurrStateFilter, null);
            }

            return list;
        }
        public ObservableCollection<EventInList> GetEventsByQuery(EEventMode i_CurrModeFilter)
        {
            ObservableCollection<EventInList> newList = new ObservableCollection<EventInList>();
            DataRowCollection drc;
            string query = null;


            drc = Sql.SelectQuery(query);

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

            foreach (EEventMode eventMode in Enum.GetValues(typeof(EEventMode)))
            {
                EventMode mode = EventMode.CreateEventMode(eventMode);
                modesList.Add(mode);
            }

            modesList.Insert(0, new EventMode { Title = "All Modes", Mode = null });

            return modesList;
        }

        public User CheckAccessKey(string i_AccessKey)
        {
            User user = null;

            if (!string.IsNullOrEmpty(i_AccessKey))
            {
                string query = $"SELECT * FROM loggedInAccounts WHERE accessKey = '{i_AccessKey}'";
                DataRow data = Sql.SelectOneItemQuery(query);

                if (data != null)
                {
                    query = $"SELECT * FROM accounts WHERE id = '{data["userId"]}'";
                    data = Sql.SelectOneItemQuery(query);

                    if (data != null)
                    {
                        user = User.CreateUser(data);
                    }
                }
            }

            return user;
        }

        public ObservableCollection<Event> GetEventsByModeFilter(EEventMode? i_CurrModeFilter, EstateFilter i_CurrStateFilter, string i_CurrTextFilter)
        {
            ObservableCollection<Event> list = new ObservableCollection<Event>();
            string query;
            int isClosed = (i_CurrStateFilter == EstateFilter.AllClosed) ? 1 : 0;

            if (i_CurrModeFilter == null)
            {
                if (string.IsNullOrEmpty(i_CurrTextFilter))
                {
                    query = $"SELECT * FROM events WHERE (groupId = {GroupId} OR groupId = 0) AND isClosed = {isClosed} ORDER BY groupId ASC";
                }
                else
                {
                    query = $"SELECT * FROM events WHERE details LIKE '%{i_CurrTextFilter}%' AND (groupId = {GroupId} OR groupId = 0) AND isClosed = {isClosed} ORDER BY groupId ASC";
                }
            }
            else
            {
                if (string.IsNullOrEmpty(i_CurrTextFilter))
                {
                    query = $"SELECT * FROM events WHERE mode = {(int)i_CurrModeFilter} AND (groupId = {GroupId} OR groupId = 0) AND isClosed = {isClosed} ORDER BY groupId ASC";
                }
                else
                {
                    query = $"SELECT * FROM events WHERE details LIKE '%{i_CurrTextFilter}%' AND (groupId = {GroupId} OR groupId = 0) AND mode = {(int)i_CurrModeFilter} AND isClosed = {isClosed} ORDER BY groupId ASC";

                }
            }

            DataRowCollection drc = Sql.SelectQuery(query);

            if(drc.Count > 0)
            {
                foreach (DataRow data in drc)
                {
                    Event newEvent = EventsFactory.CreateEvent(data);
                    list.Add(newEvent);
                }
            }

            return list;
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

                Sql.DoQuery(query);

                query = $"SELECT id FROM comments WHERE userId = {i_UserId} AND eventId = {i_EventId} AND dateCreated = '{dateCreated}' ORDER BY id DESC";
                DataRow data = Sql.SelectOneItemQuery(query);
                int id = (data != null) ? (int)data["id"] : 0;

                if (id != 0)
                    newComment = new Comment(id, i_UserId, i_Username, i_EventId, safeCommentDetails, dateCreated.ToString());
            }

            return (newComment != null) ? newComment : new Comment();
        }

        public ObservableCollection<Event> GetEventsByStateFilter(EEventMode? i_CurrModeFilter, EstateFilter i_CurrStateFilter, string i_CurrTextFilter)
        {
            ObservableCollection<Event> list = new ObservableCollection<Event>();
            string query;
            int isClosed = (i_CurrStateFilter == EstateFilter.AllClosed) ? 1 : 0;

            if (i_CurrModeFilter == null)
            {
                if (string.IsNullOrEmpty(i_CurrTextFilter))
                {
                    if (i_CurrStateFilter == EstateFilter.AllEvents)
                    {
                        query = $"SELECT * FROM events WHERE groupId = {GroupId} OR groupId = 0 ORDER BY groupId ASC";
                    }
                    else
                    {
                        query = $"SELECT * FROM events WHERE isClosed = {isClosed} AND (groupId = {GroupId} OR groupId = 0) ORDER BY groupId ASC";
                    }
                }
                else
                {
                    if (i_CurrStateFilter == EstateFilter.AllEvents)
                    {
                        query = $"SELECT * FROM events WHERE details LIKE '%{i_CurrTextFilter}%' AND (groupId = {GroupId} OR groupId = 0) ORDER BY groupId ASC";
                    }
                    else
                    {
                        query = $"SELECT * FROM events WHERE details LIKE '%{i_CurrTextFilter}%' AND isClosed = {isClosed} AND (groupId = {GroupId} OR groupId = 0) ORDER BY groupId ASC";
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(i_CurrTextFilter))
                {
                    if (i_CurrStateFilter == EstateFilter.AllEvents)
                    {
                        query = $"SELECT * FROM events WHERE mode = {(int)i_CurrModeFilter} AND (groupId = {GroupId} OR groupId = 0) ORDER BY groupId ASC";
                    }
                    else
                    {
                        query = $"SELECT * FROM events WHERE mode = {(int)i_CurrModeFilter} AND (groupId = {GroupId} OR groupId = 0) AND isClosed = {isClosed}  ORDER BY groupId ASC";
                    }
                }
                else
                {
                    if (i_CurrStateFilter == EstateFilter.AllEvents)
                    {
                        query = $"SELECT * FROM events WHERE details LIKE '%{i_CurrTextFilter}%' AND mode = {(int)i_CurrModeFilter} AND (groupId = {GroupId} OR groupId = 0) ORDER BY groupId ASC";
                    }
                    else
                    {
                        query = $"SELECT * FROM events WHERE details LIKE '%{i_CurrTextFilter}%' AND mode = {(int)i_CurrModeFilter} AND (groupId = {GroupId} OR groupId = 0) AND isClosed = {isClosed} ORDER BY groupId ASC";
                    }
                }
            }

            DataRowCollection drc = Sql.SelectQuery(query);

            if (drc.Count > 0)
            {
                foreach (DataRow data in drc)
                {
                    Event newEvent = EventsFactory.CreateEvent(data);
                    list.Add(newEvent);
                }
            }

            return list;
        }
    }
}
