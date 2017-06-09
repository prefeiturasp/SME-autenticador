using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class QTZ_Scheduler_State
    {
        public string SCHED_NAME { get; set; }
        public string INSTANCE_NAME { get; set; }
        public long LAST_CHECKIN_TIME { get; set; }
        public long CHECKIN_INTERVAL { get; set; }
    }
}
