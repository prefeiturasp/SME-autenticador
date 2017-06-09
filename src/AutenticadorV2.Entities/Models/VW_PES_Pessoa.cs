using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class VW_PES_Pessoa
    {
        public System.Guid pes_id { get; set; }
        public string pes_nome { get; set; }
        public Nullable<System.DateTime> pes_dataNascimento { get; set; }
    }
}
