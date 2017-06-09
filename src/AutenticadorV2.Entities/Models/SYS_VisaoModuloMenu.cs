using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class SYS_VisaoModuloMenu
    {
        public int vis_id { get; set; }
        public int sis_id { get; set; }
        public int mod_id { get; set; }
        public int msm_id { get; set; }
        public int vmm_ordem { get; set; }
        public virtual SYS_ModuloSiteMap SYS_ModuloSiteMap { get; set; }
        public virtual SYS_VisaoModulo SYS_VisaoModulo { get; set; }
    }
}
