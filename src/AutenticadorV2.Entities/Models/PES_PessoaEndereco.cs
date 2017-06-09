using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class PES_PessoaEndereco
    {
        public System.Guid pes_id { get; set; }
        public System.Guid pse_id { get; set; }
        public System.Guid end_id { get; set; }
        public string pse_numero { get; set; }
        public string pse_complemento { get; set; }
        public byte pse_situacao { get; set; }
        public System.DateTime pse_dataCriacao { get; set; }
        public System.DateTime pse_dataAlteracao { get; set; }
        public virtual END_Endereco END_Endereco { get; set; }
        public virtual PES_Pessoa PES_Pessoa { get; set; }
    }
}
