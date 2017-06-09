using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class SYS_ModuloSiteMap
    {
        public SYS_ModuloSiteMap()
        {
            this.SYS_VisaoModuloMenu = new List<SYS_VisaoModuloMenu>();
        }

        public int sis_id { get; set; }
        public int mod_id { get; set; }
        public int msm_id { get; set; }
        public string msm_nome { get; set; }
        public string msm_descricao { get; set; }
        public string msm_url { get; set; }
        public string msm_informacoes { get; set; }
        public string msm_urlHelp { get; set; }
        public virtual SYS_Modulo SYS_Modulo { get; set; }
        public virtual ICollection<SYS_VisaoModuloMenu> SYS_VisaoModuloMenu { get; set; }
    }
}
