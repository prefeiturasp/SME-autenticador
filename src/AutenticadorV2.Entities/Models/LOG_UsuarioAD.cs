using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class LOG_UsuarioAD
    {
        public long usa_id { get; set; }
        public System.Guid usu_id { get; set; }
        public byte usa_acao { get; set; }
        public byte usa_status { get; set; }
        public System.DateTime usa_dataAcao { get; set; }
        public byte usa_origemAcao { get; set; }
        public Nullable<System.DateTime> usa_dataProcessado { get; set; }
        public string usa_dados { get; set; }
        public virtual SYS_Usuario SYS_Usuario { get; set; }
        public virtual LOG_UsuarioADErro LOG_UsuarioADErro { get; set; }
    }
}
