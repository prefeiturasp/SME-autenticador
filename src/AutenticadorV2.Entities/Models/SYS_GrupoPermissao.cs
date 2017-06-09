using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class SYS_GrupoPermissao
    {
        public System.Guid gru_id { get; set; }
        public int sis_id { get; set; }
        public int mod_id { get; set; }
        public bool grp_consultar { get; set; }
        public bool grp_inserir { get; set; }
        public bool grp_alterar { get; set; }
        public bool grp_excluir { get; set; }
        public virtual SYS_Grupo SYS_Grupo { get; set; }
        public virtual SYS_Modulo SYS_Modulo { get; set; }
    }
}
