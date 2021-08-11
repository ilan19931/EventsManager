using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsManagerLogic.Classes
{
    public enum EEventMode
    {
        Regular,
        Important,
        Issue,
        Task
    }
    public enum EeventModeSearchFilter
    {
        Regular,
        Important,
        Issue,
        Task,
        AllOpen,
        AllClosed,
        AllEvents
    }

    public class EventMode
    {
        public EeventModeSearchFilter Mode { get; set; }
        public string Title { get; set; }
        public string Color { get; set; }

    }
}
