using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class PES_TipoDeficiencia
    {
        public PES_TipoDeficiencia()
        {
            this.PES_Pessoa = new List<PES_Pessoa>();
        }

        public System.Guid tde_id { get; set; }
        public string tde_nome { get; set; }
        public byte tde_situacao { get; set; }
        public System.DateTime tde_dataCriacao { get; set; }
        public System.DateTime tde_dataAlteracao { get; set; }
        public int tde_integridade { get; set; }
        public virtual ICollection<PES_Pessoa> PES_Pessoa { get; set; }
    }
}
