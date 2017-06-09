using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class SYS_SistemaEntidade
    {
        public int sis_id { get; set; }
        public System.Guid ent_id { get; set; }
        public string sen_chaveK1 { get; set; }
        public string sen_urlAcesso { get; set; }
        public string sen_logoCliente { get; set; }
        public string sen_urlCliente { get; set; }
        public byte sen_situacao { get; set; }
        public virtual Entidade SYS_Entidade { get; set; }
        public virtual SYS_Sistema SYS_Sistema { get; set; }
    }
}
