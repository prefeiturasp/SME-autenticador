using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class QTZ_Simprop_Triggers
    {
        public string SCHED_NAME { get; set; }
        public string TRIGGER_NAME { get; set; }
        public string TRIGGER_GROUP { get; set; }
        public string STR_PROP_1 { get; set; }
        public string STR_PROP_2 { get; set; }
        public string STR_PROP_3 { get; set; }
        public Nullable<int> INT_PROP_1 { get; set; }
        public Nullable<int> INT_PROP_2 { get; set; }
        public Nullable<long> LONG_PROP_1 { get; set; }
        public Nullable<long> LONG_PROP_2 { get; set; }
        public Nullable<decimal> DEC_PROP_1 { get; set; }
        public Nullable<decimal> DEC_PROP_2 { get; set; }
        public Nullable<bool> BOOL_PROP_1 { get; set; }
        public Nullable<bool> BOOL_PROP_2 { get; set; }
        public virtual QTZ_Triggers QTZ_Triggers { get; set; }
    }
}
