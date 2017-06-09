using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class END_Cidade
    {
        public END_Cidade()
        {
            this.END_Endereco = new List<END_Endereco>();
            this.PES_CertidaoCivil = new List<PES_CertidaoCivil>();
            this.PES_Pessoa = new List<PES_Pessoa>();
            this.SYS_DiaNaoUtil = new List<SYS_DiaNaoUtil>();
        }

        public System.Guid cid_id { get; set; }
        public System.Guid pai_id { get; set; }
        public Nullable<System.Guid> unf_id { get; set; }
        public string cid_nome { get; set; }
        public string cid_ddd { get; set; }
        public byte cid_situacao { get; set; }
        public int cid_integridade { get; set; }
        public virtual END_Pais END_Pais { get; set; }
        public virtual END_UnidadeFederativa END_UnidadeFederativa { get; set; }
        public virtual ICollection<END_Endereco> END_Endereco { get; set; }
        public virtual ICollection<PES_CertidaoCivil> PES_CertidaoCivil { get; set; }
        public virtual ICollection<PES_Pessoa> PES_Pessoa { get; set; }
        public virtual ICollection<SYS_DiaNaoUtil> SYS_DiaNaoUtil { get; set; }
    }
}
