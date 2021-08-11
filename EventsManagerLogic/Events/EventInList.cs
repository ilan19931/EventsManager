using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsManagerLogic.Events
{
    public enum EEventStatus
    {
        Open,
        Closed
    }

    public class EventInList
    {
        public Event MyEvent { set; get; }
        public string EventBgColor { get; set; } = "white";

        public EEventStatus EventStatus
        {
            get { return EventStatus; }

            set
            {
                if (value == EEventStatus.Closed)
                {
                    EventBgColor = "LightGray";
                }
                else
                {
                    EventBgColor = "white";
                }
            }
        }
    }
}
