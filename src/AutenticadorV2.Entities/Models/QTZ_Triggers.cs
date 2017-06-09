using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class QTZ_Triggers
    {
        public string SCHED_NAME { get; set; }
        public string TRIGGER_NAME { get; set; }
        public string TRIGGER_GROUP { get; set; }
        public string JOB_NAME { get; set; }
        public string JOB_GROUP { get; set; }
        public string DESCRIPTION { get; set; }
        public Nullable<long> NEXT_FIRE_TIME { get; set; }
        public Nullable<long> PREV_FIRE_TIME { get; set; }
        public Nullable<int> PRIORITY { get; set; }
        public string TRIGGER_STATE { get; set; }
        public string TRIGGER_TYPE { get; set; }
        public long START_TIME { get; set; }
        public Nullable<long> END_TIME { get; set; }
        public string CALENDAR_NAME { get; set; }
        public Nullable<int> MISFIRE_INSTR { get; set; }
        public byte[] JOB_DATA { get; set; }
        public virtual QTZ_Cron_Triggers QTZ_Cron_Triggers { get; set; }
        public virtual QTZ_Job_Details QTZ_Job_Details { get; set; }
        public virtual QTZ_Simple_Triggers QTZ_Simple_Triggers { get; set; }
        public virtual QTZ_Simprop_Triggers QTZ_Simprop_Triggers { get; set; }
    }
}
