using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class SYS_UsuarioSenhaHistorico
    {
        public System.Guid usu_id { get; set; }
        public string ush_senha { get; set; }
        public Nullable<byte> ush_criptografia { get; set; }
        public System.Guid ush_id { get; set; }
        public System.DateTime ush_data { get; set; }
        public virtual SYS_Usuario SYS_Usuario { get; set; }
    }
}
