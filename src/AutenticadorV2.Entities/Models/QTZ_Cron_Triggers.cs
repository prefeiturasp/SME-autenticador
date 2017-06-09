using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class QTZ_Cron_Triggers
    {
        public string SCHED_NAME { get; set; }
        public string TRIGGER_NAME { get; set; }
        public string TRIGGER_GROUP { get; set; }
        public string CRON_EXPRESSION { get; set; }
        public string TIME_ZONE_ID { get; set; }
        public virtual QTZ_Triggers QTZ_Triggers { get; set; }
    }
}
