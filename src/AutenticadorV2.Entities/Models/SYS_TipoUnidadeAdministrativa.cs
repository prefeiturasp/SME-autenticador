using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class SYS_TipoUnidadeAdministrativa
    {
        public SYS_TipoUnidadeAdministrativa()
        {
            this.SYS_UnidadeAdministrativa = new List<SYS_UnidadeAdministrativa>();
        }

        public System.Guid tua_id { get; set; }
        public string tua_nome { get; set; }
        public byte tua_situacao { get; set; }
        public System.DateTime tua_dataCriacao { get; set; }
        public System.DateTime tua_dataAlteracao { get; set; }
        public int tua_integridade { get; set; }
        public virtual ICollection<SYS_UnidadeAdministrativa> SYS_UnidadeAdministrativa { get; set; }
    }
}
