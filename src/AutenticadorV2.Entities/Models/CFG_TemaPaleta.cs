using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class CFG_TemaPaleta
    {
        public CFG_TemaPaleta()
        {
            this.SYS_Entidade = new List<Entidade>();
        }

        public int tep_id { get; set; }
        public int tpl_id { get; set; }
        public string tpl_nome { get; set; }
        public string tpl_caminhoCSS { get; set; }
        public string tpl_imagemExemploTema { get; set; }
        public byte tpl_situacao { get; set; }
        public System.DateTime tpl_dataCriacao { get; set; }
        public System.DateTime tpl_dataAlteracao { get; set; }
        public virtual CFG_TemaPadrao CFG_TemaPadrao { get; set; }
        public virtual ICollection<Entidade> SYS_Entidade { get; set; }
    }
}
