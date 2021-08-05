using EventsManagerLogic.Events;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsManagerLogic.Helpers
{
    public class EventsFactory
    {
        public static IEvent CreateEvent(DataRow data)
        {
            IEvent newEvent = null;

            switch((EventState) data["State"])
            {
                case EventState.Regular:
                    newEvent = new RegularEvent(data);
                    break;

                case EventState.Important:
                    newEvent = new ImportantEvent(data);
                    break;

                case EventState.Issue:
                    newEvent = new IssueEvent(data);
                    break;

                case EventState.Task:
                    newEvent = new TaskEvent(data);
                    break;
            }

            return newEvent;

        }
    }
}
