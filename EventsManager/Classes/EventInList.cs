using EventsManagerLogic.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsManager.Classes
{
    public class EventInList
    {
        public IEvent Event { get; set; }
        public int ContactType { get; set; } = 1;
    }
}
