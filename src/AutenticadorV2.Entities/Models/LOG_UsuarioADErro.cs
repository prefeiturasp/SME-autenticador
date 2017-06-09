using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class LOG_UsuarioADErro
    {
        public long usa_id { get; set; }
        public string use_descricaoErro { get; set; }
        public virtual LOG_UsuarioAD LOG_UsuarioAD { get; set; }
    }
}
