using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class SYS_UnidadeAdministrativaContato
    {
        public System.Guid ent_id { get; set; }
        public System.Guid uad_id { get; set; }
        public System.Guid uac_id { get; set; }
        public System.Guid tmc_id { get; set; }
        public string uac_contato { get; set; }
        public byte uac_situacao { get; set; }
        public System.DateTime uac_dataCriacao { get; set; }
        public System.DateTime uac_dataAlteracao { get; set; }
        public virtual SYS_TipoMeioContato SYS_TipoMeioContato { get; set; }
        public virtual SYS_UnidadeAdministrativa SYS_UnidadeAdministrativa { get; set; }
    }
}
