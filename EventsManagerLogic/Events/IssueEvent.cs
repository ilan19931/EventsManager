﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsManagerLogic.Events
{
    public class IssueEvent : IEvent
    {
        public string DateCreated { get; set; }
        public string Description { get; set; }
        public string Options { get; set; }
        public string BgColor { get; } = "Yellow";

        public IssueEvent(DataRow dr)
        {
            DateCreated = dr["DateCreated"].ToString();
            Description = dr["Description"].ToString();
            Options = dr["Options"].ToString();
        }
    }
}