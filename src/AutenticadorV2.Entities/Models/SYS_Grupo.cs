using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class SYS_Grupo
    {
        public SYS_Grupo()
        {
            this.SYS_GrupoPermissao = new List<SYS_GrupoPermissao>();
            this.SYS_ParametroGrupoPerfil = new List<SYS_ParametroGrupoPerfil>();
            this.SYS_UsuarioGrupo = new List<SYS_UsuarioGrupo>();
        }

        public System.Guid gru_id { get; set; }
        public string gru_nome { get; set; }
        public byte gru_situacao { get; set; }
        public System.DateTime gru_dataCriacao { get; set; }
        public System.DateTime gru_dataAlteracao { get; set; }
        public int vis_id { get; set; }
        public int sis_id { get; set; }
        public int gru_integridade { get; set; }
        public virtual SYS_Sistema SYS_Sistema { get; set; }
        public virtual SYS_Visao SYS_Visao { get; set; }
        public virtual ICollection<SYS_GrupoPermissao> SYS_GrupoPermissao { get; set; }
        public virtual ICollection<SYS_ParametroGrupoPerfil> SYS_ParametroGrupoPerfil { get; set; }
        public virtual ICollection<SYS_UsuarioGrupo> SYS_UsuarioGrupo { get; set; }
    }
}
