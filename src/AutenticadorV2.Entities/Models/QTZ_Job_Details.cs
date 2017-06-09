using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class QTZ_Job_Details
    {
        public QTZ_Job_Details()
        {
            this.QTZ_Triggers = new List<QTZ_Triggers>();
        }

        public string SCHED_NAME { get; set; }
        public string JOB_NAME { get; set; }
        public string JOB_GROUP { get; set; }
        public string DESCRIPTION { get; set; }
        public string JOB_CLASS_NAME { get; set; }
        public bool IS_DURABLE { get; set; }
        public bool IS_NONCONCURRENT { get; set; }
        public bool IS_UPDATE_DATA { get; set; }
        public bool REQUESTS_RECOVERY { get; set; }
        public byte[] JOB_DATA { get; set; }
        public virtual ICollection<QTZ_Triggers> QTZ_Triggers { get; set; }
    }
}
