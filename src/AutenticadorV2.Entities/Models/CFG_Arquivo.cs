using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class CFG_Arquivo
    {
        public CFG_Arquivo()
        {
            this.PES_Pessoa = new List<PES_Pessoa>();
        }

        public long arq_id { get; set; }
        public string arq_nome { get; set; }
        public long arq_tamanhoKB { get; set; }
        public string arq_typeMime { get; set; }
        public byte[] arq_data { get; set; }
        public byte arq_situacao { get; set; }
        public System.DateTime arq_dataCriacao { get; set; }
        public System.DateTime arq_dataAlteracao { get; set; }
        public virtual ICollection<PES_Pessoa> PES_Pessoa { get; set; }
    }
}
