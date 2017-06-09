using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class Entidade
    {
        public Entidade()
        {
            this.SYS_Entidade1 = new List<Entidade>();
            this.SYS_EntidadeContato = new List<SYS_EntidadeContato>();
            this.SYS_EntidadeEndereco = new List<SYS_EntidadeEndereco>();
            this.SYS_SistemaEntidade = new List<SYS_SistemaEntidade>();
            this.SYS_UnidadeAdministrativa = new List<SYS_UnidadeAdministrativa>();
            this.SYS_Usuario = new List<SYS_Usuario>();
            this.SYS_UsuarioGrupoUA = new List<SYS_UsuarioGrupoUA>();
        }

        public System.Guid ent_id { get; set; }
        public System.Guid ten_id { get; set; }
        public string ent_codigo { get; set; }
        public string ent_nomeFantasia { get; set; }
        public string ent_razaoSocial { get; set; }
        public string ent_sigla { get; set; }
        public string ent_cnpj { get; set; }
        public string ent_inscricaoMunicipal { get; set; }
        public string ent_inscricaoEstadual { get; set; }
        public Nullable<System.Guid> ent_idSuperior { get; set; }
        public byte ent_situacao { get; set; }
        public System.DateTime ent_dataCriacao { get; set; }
        public System.DateTime ent_dataAlteracao { get; set; }
        public int ent_integridade { get; set; }
        public string ent_urlAcesso { get; set; }
        public bool ent_exibeLogoCliente { get; set; }
        public string ent_logoCliente { get; set; }
        public Nullable<int> tep_id { get; set; }
        public Nullable<int> tpl_id { get; set; }
        public virtual CFG_TemaPaleta CFG_TemaPaleta { get; set; }
        public virtual ICollection<Entidade> SYS_Entidade1 { get; set; }
        public virtual Entidade SYS_Entidade2 { get; set; }
        public virtual TipoEntidade SYS_TipoEntidade { get; set; }
        public virtual ICollection<SYS_EntidadeContato> SYS_EntidadeContato { get; set; }
        public virtual ICollection<SYS_EntidadeEndereco> SYS_EntidadeEndereco { get; set; }
        public virtual ICollection<SYS_SistemaEntidade> SYS_SistemaEntidade { get; set; }
        public virtual ICollection<SYS_UnidadeAdministrativa> SYS_UnidadeAdministrativa { get; set; }
        public virtual ICollection<SYS_Usuario> SYS_Usuario { get; set; }
        public virtual ICollection<SYS_UsuarioGrupoUA> SYS_UsuarioGrupoUA { get; set; }
    }
}