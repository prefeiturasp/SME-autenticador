using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class QTZ_Blob_Triggers
    {
        public string SCHED_NAME { get; set; }
        public string TRIGGER_NAME { get; set; }
        public string TRIGGER_GROUP { get; set; }
        public byte[] BLOB_DATA { get; set; }
    }
}
