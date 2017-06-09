using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class CFG_TemaPadrao
    {
        public CFG_TemaPadrao()
        {
            this.CFG_TemaPaleta = new List<CFG_TemaPaleta>();
        }

        public int tep_id { get; set; }
        public string tep_nome { get; set; }
        public string tep_descricao { get; set; }
        public byte tep_tipoMenu { get; set; }
        public bool tep_exibeLinkLogin { get; set; }
        public byte tep_tipoLogin { get; set; }
        public bool tep_exibeLogoCliente { get; set; }
        public byte tep_situacao { get; set; }
        public System.DateTime tep_dataCriacao { get; set; }
        public System.DateTime tep_dataAlteracao { get; set; }
        public virtual ICollection<CFG_TemaPaleta> CFG_TemaPaleta { get; set; }
    }
}
