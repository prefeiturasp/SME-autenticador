using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class CFG_Relatorio
    {
        public CFG_Relatorio()
        {
            this.CFG_ServidorRelatorio = new List<CFG_ServidorRelatorio>();
        }

        public int rlt_id { get; set; }
        public string rlt_nome { get; set; }
        public byte rlt_situacao { get; set; }
        public System.DateTime rlt_dataCriacao { get; set; }
        public System.DateTime rlt_dataAlteracao { get; set; }
        public int rlt_integridade { get; set; }
        public virtual ICollection<CFG_ServidorRelatorio> CFG_ServidorRelatorio { get; set; }
    }
}
