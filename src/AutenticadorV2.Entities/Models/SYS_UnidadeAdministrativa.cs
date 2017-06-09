using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class SYS_UnidadeAdministrativa
    {
        public SYS_UnidadeAdministrativa()
        {
            this.SYS_UnidadeAdministrativa1 = new List<SYS_UnidadeAdministrativa>();
            this.SYS_UnidadeAdministrativaContato = new List<SYS_UnidadeAdministrativaContato>();
            this.SYS_UnidadeAdministrativaEndereco = new List<SYS_UnidadeAdministrativaEndereco>();
            this.SYS_UsuarioGrupoUA = new List<SYS_UsuarioGrupoUA>();
        }

        public System.Guid ent_id { get; set; }
        public System.Guid uad_id { get; set; }
        public System.Guid tua_id { get; set; }
        public string uad_codigo { get; set; }
        public string uad_nome { get; set; }
        public string uad_sigla { get; set; }
        public Nullable<System.Guid> uad_idSuperior { get; set; }
        public byte uad_situacao { get; set; }
        public System.DateTime uad_dataCriacao { get; set; }
        public System.DateTime uad_dataAlteracao { get; set; }
        public int uad_integridade { get; set; }
        public string uad_codigoIntegracao { get; set; }
        public virtual Entidade SYS_Entidade { get; set; }
        public virtual SYS_TipoUnidadeAdministrativa SYS_TipoUnidadeAdministrativa { get; set; }
        public virtual ICollection<SYS_UnidadeAdministrativa> SYS_UnidadeAdministrativa1 { get; set; }
        public virtual SYS_UnidadeAdministrativa SYS_UnidadeAdministrativa2 { get; set; }
        public virtual ICollection<SYS_UnidadeAdministrativaContato> SYS_UnidadeAdministrativaContato { get; set; }
        public virtual ICollection<SYS_UnidadeAdministrativaEndereco> SYS_UnidadeAdministrativaEndereco { get; set; }
        public virtual ICollection<SYS_UsuarioGrupoUA> SYS_UsuarioGrupoUA { get; set; }
    }
}
