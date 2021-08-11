using EventsManagerLogic.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsManagerLogic.Events
{
    public class RegularEvent : Event, IEvent
    {
        public RegularEvent(DataRow data) : base(data)
        {
            base.BgColor = "#2ecc71";
        }

        public RegularEvent(int i_Id, string i_Title, string i_Details, int i_Mode, int i_Category, string i_DateCreated, int i_IsClosed)
                            : base(i_Id, i_Title, i_Details, i_Mode, i_Category, i_DateCreated, i_IsClosed)
        {
            base.BgColor = "#2ecc71";
        }
    }
}
