using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsManagerLogic.Events
{
    public enum EventState
    {
        Regular,
        Important,
        Issue,
        Task
    }

    public interface IEvent
    {
        string DateCreated { get; set; }
        string Description { get; set; }
        string Options { get; set; }
        string BgColor { get; }
    }
}
