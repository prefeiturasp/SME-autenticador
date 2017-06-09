using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class CFG_Configuracao
    {
        public System.Guid cfg_id { get; set; }
        public string cfg_chave { get; set; }
        public string cfg_valor { get; set; }
        public string cfg_descricao { get; set; }
        public byte cfg_situacao { get; set; }
        public System.DateTime cfg_dataCriacao { get; set; }
        public System.DateTime cfg_dataAlteracao { get; set; }
    }
}
