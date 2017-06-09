using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class SYS_TipoMeioContato
    {
        public SYS_TipoMeioContato()
        {
            this.PES_PessoaContato = new List<PES_PessoaContato>();
            this.SYS_EntidadeContato = new List<SYS_EntidadeContato>();
            this.SYS_UnidadeAdministrativaContato = new List<SYS_UnidadeAdministrativaContato>();
        }

        public System.Guid tmc_id { get; set; }
        public string tmc_nome { get; set; }
        public Nullable<byte> tmc_validacao { get; set; }
        public byte tmc_situacao { get; set; }
        public System.DateTime tmc_dataCriacao { get; set; }
        public System.DateTime tmc_dataAlteracao { get; set; }
        public int tmc_integridade { get; set; }
        public virtual ICollection<PES_PessoaContato> PES_PessoaContato { get; set; }
        public virtual ICollection<SYS_EntidadeContato> SYS_EntidadeContato { get; set; }
        public virtual ICollection<SYS_UnidadeAdministrativaContato> SYS_UnidadeAdministrativaContato { get; set; }
    }
}
