using EventsManagerLogic.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsManagerLogic.Events
{
    public class EventStateFilter
    {
        public EstateFilter State { get; set; }
        public string Title { get; set; }

        public static EventStateFilter CreateStateFilter(EstateFilter i_State)
        {
            EventStateFilter stateFilter = new EventStateFilter();
            stateFilter.State = i_State;

            switch (i_State)
            {
                case EstateFilter.AllEvents:
                    stateFilter.Title = "All Events";
                    break;

                case EstateFilter.AllOpen:
                    stateFilter.Title = "Open Events";
                    break;

                case EstateFilter.AllClosed:
                    stateFilter.Title = "Closed Events";
                    break;
            }

            return stateFilter;
        }
    }
}
