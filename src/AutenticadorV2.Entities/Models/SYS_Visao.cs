using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class SYS_Visao
    {
        public SYS_Visao()
        {
            this.SYS_Grupo = new List<SYS_Grupo>();
            this.SYS_VisaoModulo = new List<SYS_VisaoModulo>();
        }

        public int vis_id { get; set; }
        public string vis_nome { get; set; }
        public virtual ICollection<SYS_Grupo> SYS_Grupo { get; set; }
        public virtual ICollection<SYS_VisaoModulo> SYS_VisaoModulo { get; set; }
    }
}
