using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class LOG_UsuarioAPI
    {
        public long lua_id { get; set; }
        public System.Guid usu_id { get; set; }
        public int uap_id { get; set; }
        public byte lua_acao { get; set; }
        public System.DateTime lua_dataHora { get; set; }
        public virtual CFG_UsuarioAPI CFG_UsuarioAPI { get; set; }
        public virtual SYS_Usuario SYS_Usuario { get; set; }
    }
}
