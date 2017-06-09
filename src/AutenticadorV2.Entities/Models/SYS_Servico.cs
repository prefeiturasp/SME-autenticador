using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class SYS_Servico
    {
        public short ser_id { get; set; }
        public string ser_nome { get; set; }
        public string ser_nomeProcedimento { get; set; }
        public bool ser_ativo { get; set; }
    }
}
