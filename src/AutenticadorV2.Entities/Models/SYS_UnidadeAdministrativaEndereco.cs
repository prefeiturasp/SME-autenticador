using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class SYS_UnidadeAdministrativaEndereco
    {
        public System.Guid ent_id { get; set; }
        public System.Guid uad_id { get; set; }
        public System.Guid uae_id { get; set; }
        public System.Guid end_id { get; set; }
        public string uae_numero { get; set; }
        public string uae_complemento { get; set; }
        public byte uae_situacao { get; set; }
        public System.DateTime uae_dataCriacao { get; set; }
        public System.DateTime uae_dataAlteracao { get; set; }
        public virtual END_Endereco END_Endereco { get; set; }
        public virtual SYS_UnidadeAdministrativa SYS_UnidadeAdministrativa { get; set; }
    }
}
