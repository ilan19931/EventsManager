using EventsManagerLogic.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsManagerLogic.Events
{
    public class Event
    {
        public int Id { get; }
        public string DateCreated { get; }
        public string Title { get; set; }
        public string Details { get; set; }
        public string Options { get; set; }
        public string BgColor { get; set; }
        public int Category { get; set; }
        public int IsClosed { get; set; }

        public EEventMode EeventMode { get; set; }

        public Event(DataRow dr)
        {
            Id = (int)dr["id"];
            DateCreated = dr["DateCreated"].ToString();
            Details = dr["details"].ToString();
            EeventMode = (EEventMode)dr["mode"];
            Category = (int)dr["categoryId"];
            Title = dr["title"].ToString();
            IsClosed = (int)IsClosed;
            //Options = dr["Options"].ToString();
        }

        public Event(int i_Id, string i_Title, string i_Details, int i_Mode, int i_Category, string i_DateCreated, int i_IsClosed)
        {
            Id = i_Id;
            DateCreated = i_DateCreated;
            Title = i_Title;
            Details = i_Details;
            IsClosed = i_IsClosed;
        }
    }
}
