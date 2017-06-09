using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class SYS_Modulo
    {
        public SYS_Modulo()
        {
            this.SYS_GrupoPermissao = new List<SYS_GrupoPermissao>();
            this.SYS_ModuloSiteMap = new List<SYS_ModuloSiteMap>();
            this.SYS_VisaoModulo = new List<SYS_VisaoModulo>();
        }

        public int sis_id { get; set; }
        public int mod_id { get; set; }
        public string mod_nome { get; set; }
        public string mod_descricao { get; set; }
        public Nullable<int> mod_idPai { get; set; }
        public bool mod_auditoria { get; set; }
        public byte mod_situacao { get; set; }
        public System.DateTime mod_dataCriacao { get; set; }
        public System.DateTime mod_dataAlteracao { get; set; }
        public virtual ICollection<SYS_GrupoPermissao> SYS_GrupoPermissao { get; set; }
        public virtual SYS_Sistema SYS_Sistema { get; set; }
        public virtual ICollection<SYS_ModuloSiteMap> SYS_ModuloSiteMap { get; set; }
        public virtual ICollection<SYS_VisaoModulo> SYS_VisaoModulo { get; set; }
    }
}
