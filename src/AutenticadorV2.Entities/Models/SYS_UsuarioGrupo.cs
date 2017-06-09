using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class SYS_UsuarioGrupo
    {
        public SYS_UsuarioGrupo()
        {
            this.SYS_UsuarioGrupoUA = new List<SYS_UsuarioGrupoUA>();
        }

        public System.Guid usu_id { get; set; }
        public System.Guid gru_id { get; set; }
        public byte usg_situacao { get; set; }
        public virtual SYS_Grupo SYS_Grupo { get; set; }
        public virtual SYS_Usuario SYS_Usuario { get; set; }
        public virtual ICollection<SYS_UsuarioGrupoUA> SYS_UsuarioGrupoUA { get; set; }
    }
}
