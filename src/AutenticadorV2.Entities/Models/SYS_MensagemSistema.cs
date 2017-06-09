using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class SYS_MensagemSistema
    {
        public int mss_id { get; set; }
        public string mss_chave { get; set; }
        public string mss_valor { get; set; }
        public string mss_descricao { get; set; }
        public byte mss_situacao { get; set; }
        public System.DateTime mss_dataCriacao { get; set; }
        public System.DateTime mss_dataAlteracao { get; set; }
    }
}
