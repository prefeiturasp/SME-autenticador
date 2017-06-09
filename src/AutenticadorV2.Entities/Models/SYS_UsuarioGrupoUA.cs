using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class SYS_UsuarioGrupoUA
    {
        public System.Guid usu_id { get; set; }
        public System.Guid gru_id { get; set; }
        public System.Guid ugu_id { get; set; }
        public System.Guid ent_id { get; set; }
        public System.Guid uad_id { get; set; }
        public virtual Entidade SYS_Entidade { get; set; }
        public virtual SYS_UnidadeAdministrativa SYS_UnidadeAdministrativa { get; set; }
        public virtual SYS_UsuarioGrupo SYS_UsuarioGrupo { get; set; }
    }
}
