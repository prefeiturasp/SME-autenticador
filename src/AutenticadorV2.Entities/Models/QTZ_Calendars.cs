using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class QTZ_Calendars
    {
        public string SCHED_NAME { get; set; }
        public string CALENDAR_NAME { get; set; }
        public byte[] CALENDAR { get; set; }
    }
}
