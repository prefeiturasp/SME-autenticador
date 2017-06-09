using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class SYS_UsuarioFalhaAutenticacao
    {
        public System.Guid usu_id { get; set; }
        public int ufl_qtdeFalhas { get; set; }
        public System.DateTime ufl_dataUltimaTentativa { get; set; }
        public virtual SYS_Usuario SYS_Usuario { get; set; }
    }
}
