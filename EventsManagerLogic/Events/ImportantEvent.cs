using EventsManagerLogic.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsManagerLogic.Events
{
    public class ImportantEvent : Event, IEvent
    {

        public ImportantEvent(DataRow data) : base(data)
        {
            base.BgColor = "red";
        }

        public ImportantEvent(int i_Id, string i_Title, string i_Details, int i_Mode, int i_Category, string i_DateCreated, int i_IsClosed)
                                : base (i_Id, i_Title, i_Details, i_Mode, i_Category, i_DateCreated, i_IsClosed)
        {
            base.BgColor = "red";
        }
    }
}
