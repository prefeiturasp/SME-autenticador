using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class PES_Pessoa
    {
        public PES_Pessoa()
        {
            this.PES_CertidaoCivil = new List<PES_CertidaoCivil>();
            this.PES_Pessoa1 = new List<PES_Pessoa>();
            this.PES_Pessoa11 = new List<PES_Pessoa>();
            this.PES_PessoaContato = new List<PES_PessoaContato>();
            this.PES_PessoaDocumento = new List<PES_PessoaDocumento>();
            this.PES_PessoaEndereco = new List<PES_PessoaEndereco>();
            this.SYS_Usuario = new List<SYS_Usuario>();
            this.PES_TipoDeficiencia = new List<PES_TipoDeficiencia>();
        }

        public System.Guid pes_id { get; set; }
        public string pes_nome { get; set; }
        public string pes_nome_abreviado { get; set; }
        public Nullable<System.Guid> pai_idNacionalidade { get; set; }
        public Nullable<bool> pes_naturalizado { get; set; }
        public Nullable<System.Guid> cid_idNaturalidade { get; set; }
        public Nullable<System.DateTime> pes_dataNascimento { get; set; }
        public Nullable<byte> pes_estadoCivil { get; set; }
        public Nullable<byte> pes_racaCor { get; set; }
        public Nullable<byte> pes_sexo { get; set; }
        public Nullable<System.Guid> pes_idFiliacaoPai { get; set; }
        public Nullable<System.Guid> pes_idFiliacaoMae { get; set; }
        public Nullable<System.Guid> tes_id { get; set; }
        public byte[] pes_foto { get; set; }
        public byte pes_situacao { get; set; }
        public System.DateTime pes_dataCriacao { get; set; }
        public System.DateTime pes_dataAlteracao { get; set; }
        public int pes_integridade { get; set; }
        public Nullable<long> arq_idFoto { get; set; }
        public virtual CFG_Arquivo CFG_Arquivo { get; set; }
        public virtual END_Cidade END_Cidade { get; set; }
        public virtual END_Pais END_Pais { get; set; }
        public virtual ICollection<PES_CertidaoCivil> PES_CertidaoCivil { get; set; }
        public virtual ICollection<PES_Pessoa> PES_Pessoa1 { get; set; }
        public virtual PES_Pessoa PES_Pessoa2 { get; set; }
        public virtual ICollection<PES_Pessoa> PES_Pessoa11 { get; set; }
        public virtual PES_Pessoa PES_Pessoa3 { get; set; }
        public virtual PES_TipoEscolaridade PES_TipoEscolaridade { get; set; }
        public virtual ICollection<PES_PessoaContato> PES_PessoaContato { get; set; }
        public virtual ICollection<PES_PessoaDocumento> PES_PessoaDocumento { get; set; }
        public virtual ICollection<PES_PessoaEndereco> PES_PessoaEndereco { get; set; }
        public virtual ICollection<SYS_Usuario> SYS_Usuario { get; set; }
        public virtual ICollection<PES_TipoDeficiencia> PES_TipoDeficiencia { get; set; }
    }
}
