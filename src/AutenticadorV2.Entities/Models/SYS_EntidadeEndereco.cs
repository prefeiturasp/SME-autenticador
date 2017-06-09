using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class SYS_EntidadeEndereco
    {
        public System.Guid ent_id { get; set; }
        public System.Guid ene_id { get; set; }
        public System.Guid end_id { get; set; }
        public string ene_numero { get; set; }
        public string ene_complemento { get; set; }
        public byte ene_situacao { get; set; }
        public System.DateTime ene_dataCriacao { get; set; }
        public System.DateTime ene_dataAlteracao { get; set; }
        public virtual END_Endereco END_Endereco { get; set; }
        public virtual Entidade SYS_Entidade { get; set; }
    }
}
