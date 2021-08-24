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
    public enum EstateFilter
    {
        AllOpen,
        AllEvents,
        AllClosed
    }

    public class EventMode
    {
        public EEventMode? Mode { get; set; }
        public string Title { get; set; }
        public string Color { get; set; }

        public static EventMode CreateEventMode(EEventMode i_Mode)
        {
            string bgColor = null;
            string title = i_Mode.ToString();

            switch (i_Mode)
            {
                case EEventMode.Important:
                    bgColor = "red";
                    break;

                case EEventMode.Regular:
                    bgColor = "#2ecc71";
                    break;

                case EEventMode.Issue:
                    bgColor = "#f1c40f";
                    break;

                case EEventMode.Task:
                    bgColor = "#0984e3";
                    break;
            }

            EventMode newMode = new EventMode();
            newMode.Mode = i_Mode;
            newMode.Title = title;
            newMode.Color = bgColor;

            return newMode;
        }

    }
}
