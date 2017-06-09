using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class SYS_ParametroGrupoPerfil
    {
        public System.Guid pgs_id { get; set; }
        public string pgs_chave { get; set; }
        public System.Guid gru_id { get; set; }
        public byte pgs_situacao { get; set; }
        public System.DateTime pgs_dataCriacao { get; set; }
        public System.DateTime pgs_dataAlteracao { get; set; }
        public virtual SYS_Grupo SYS_Grupo { get; set; }
    }
}
