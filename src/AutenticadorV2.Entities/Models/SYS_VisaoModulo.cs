using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class SYS_VisaoModulo
    {
        public SYS_VisaoModulo()
        {
            this.SYS_VisaoModuloMenu = new List<SYS_VisaoModuloMenu>();
        }

        public int vis_id { get; set; }
        public int sis_id { get; set; }
        public int mod_id { get; set; }
        public virtual SYS_Modulo SYS_Modulo { get; set; }
        public virtual SYS_Visao SYS_Visao { get; set; }
        public virtual ICollection<SYS_VisaoModuloMenu> SYS_VisaoModuloMenu { get; set; }
    }
}
