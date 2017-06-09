using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class PES_PessoaContato
    {
        public System.Guid pes_id { get; set; }
        public System.Guid psc_id { get; set; }
        public System.Guid tmc_id { get; set; }
        public string psc_contato { get; set; }
        public byte psc_situacao { get; set; }
        public System.DateTime psc_dataCriacao { get; set; }
        public System.DateTime psc_dataAlteracao { get; set; }
        public virtual PES_Pessoa PES_Pessoa { get; set; }
        public virtual SYS_TipoMeioContato SYS_TipoMeioContato { get; set; }
    }
}
