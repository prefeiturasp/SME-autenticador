using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class QTZ_Simple_Triggers
    {
        public string SCHED_NAME { get; set; }
        public string TRIGGER_NAME { get; set; }
        public string TRIGGER_GROUP { get; set; }
        public int REPEAT_COUNT { get; set; }
        public long REPEAT_INTERVAL { get; set; }
        public int TIMES_TRIGGERED { get; set; }
        public virtual QTZ_Triggers QTZ_Triggers { get; set; }
    }
}
