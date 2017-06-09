using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class SYS_EntidadeContato
    {
        public System.Guid ent_id { get; set; }
        public System.Guid enc_id { get; set; }
        public System.Guid tmc_id { get; set; }
        public string enc_contato { get; set; }
        public byte enc_situacao { get; set; }
        public System.DateTime enc_dataCriacao { get; set; }
        public System.DateTime enc_dataAlteracao { get; set; }
        public virtual Entidade SYS_Entidade { get; set; }
        public virtual SYS_TipoMeioContato SYS_TipoMeioContato { get; set; }
    }
}
