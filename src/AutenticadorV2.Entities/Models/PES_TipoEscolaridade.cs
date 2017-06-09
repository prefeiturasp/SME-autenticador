using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class PES_TipoEscolaridade
    {
        public PES_TipoEscolaridade()
        {
            this.PES_Pessoa = new List<PES_Pessoa>();
        }

        public System.Guid tes_id { get; set; }
        public string tes_nome { get; set; }
        public int tes_ordem { get; set; }
        public byte tes_situacao { get; set; }
        public System.DateTime tes_dataCriacao { get; set; }
        public System.DateTime tes_dataAlteracao { get; set; }
        public int tes_integridade { get; set; }
        public virtual ICollection<PES_Pessoa> PES_Pessoa { get; set; }
    }
}
