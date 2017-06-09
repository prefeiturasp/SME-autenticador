using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class END_UnidadeFederativa
    {
        public END_UnidadeFederativa()
        {
            this.END_Cidade = new List<END_Cidade>();
            this.PES_CertidaoCivil = new List<PES_CertidaoCivil>();
            this.PES_PessoaDocumento = new List<PES_PessoaDocumento>();
            this.SYS_DiaNaoUtil = new List<SYS_DiaNaoUtil>();
        }

        public System.Guid unf_id { get; set; }
        public System.Guid pai_id { get; set; }
        public string unf_nome { get; set; }
        public string unf_sigla { get; set; }
        public byte unf_situacao { get; set; }
        public int unf_integridade { get; set; }
        public virtual ICollection<END_Cidade> END_Cidade { get; set; }
        public virtual END_Pais END_Pais { get; set; }
        public virtual ICollection<PES_CertidaoCivil> PES_CertidaoCivil { get; set; }
        public virtual ICollection<PES_PessoaDocumento> PES_PessoaDocumento { get; set; }
        public virtual ICollection<SYS_DiaNaoUtil> SYS_DiaNaoUtil { get; set; }
    }
}
