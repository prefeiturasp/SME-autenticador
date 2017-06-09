using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class END_Endereco
    {
        public END_Endereco()
        {
            this.PES_PessoaEndereco = new List<PES_PessoaEndereco>();
            this.SYS_EntidadeEndereco = new List<SYS_EntidadeEndereco>();
            this.SYS_UnidadeAdministrativaEndereco = new List<SYS_UnidadeAdministrativaEndereco>();
        }

        public System.Guid end_id { get; set; }
        public string end_cep { get; set; }
        public string end_logradouro { get; set; }
        public string end_bairro { get; set; }
        public string end_distrito { get; set; }
        public Nullable<byte> end_zona { get; set; }
        public System.Guid cid_id { get; set; }
        public byte end_situacao { get; set; }
        public System.DateTime end_dataCriacao { get; set; }
        public System.DateTime end_dataAlteracao { get; set; }
        public int end_integridade { get; set; }
        public virtual END_Cidade END_Cidade { get; set; }
        public virtual ICollection<PES_PessoaEndereco> PES_PessoaEndereco { get; set; }
        public virtual ICollection<SYS_EntidadeEndereco> SYS_EntidadeEndereco { get; set; }
        public virtual ICollection<SYS_UnidadeAdministrativaEndereco> SYS_UnidadeAdministrativaEndereco { get; set; }
    }
}
