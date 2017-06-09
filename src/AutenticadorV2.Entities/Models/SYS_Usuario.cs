using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class SYS_Usuario
    {
        public SYS_Usuario()
        {
            this.LOG_UsuarioAD = new List<LOG_UsuarioAD>();
            this.LOG_UsuarioAPI = new List<LOG_UsuarioAPI>();
            this.SYS_UsuarioGrupo = new List<SYS_UsuarioGrupo>();
            this.SYS_UsuarioSenhaHistorico = new List<SYS_UsuarioSenhaHistorico>();
        }

        public System.Guid usu_id { get; set; }
        public string usu_login { get; set; }
        public string usu_dominio { get; set; }
        public string usu_email { get; set; }
        public string usu_senha { get; set; }
        public Nullable<byte> usu_criptografia { get; set; }
        public byte usu_situacao { get; set; }
        public System.DateTime usu_dataCriacao { get; set; }
        public System.DateTime usu_dataAlteracao { get; set; }
        public Nullable<System.Guid> pes_id { get; set; }
        public int usu_integridade { get; set; }
        public System.Guid ent_id { get; set; }
        public byte usu_integracaoAD { get; set; }
        public virtual ICollection<LOG_UsuarioAD> LOG_UsuarioAD { get; set; }
        public virtual ICollection<LOG_UsuarioAPI> LOG_UsuarioAPI { get; set; }
        public virtual PES_Pessoa PES_Pessoa { get; set; }
        public virtual Entidade SYS_Entidade { get; set; }
        public virtual SYS_UsuarioFalhaAutenticacao SYS_UsuarioFalhaAutenticacao { get; set; }
        public virtual ICollection<SYS_UsuarioGrupo> SYS_UsuarioGrupo { get; set; }
        public virtual ICollection<SYS_UsuarioSenhaHistorico> SYS_UsuarioSenhaHistorico { get; set; }
    }
}
