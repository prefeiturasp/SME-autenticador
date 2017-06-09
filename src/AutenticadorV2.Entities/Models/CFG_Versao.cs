using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class CFG_Versao
    {
        public int ver_id { get; set; }
        public string ver_Versao { get; set; }
        public System.DateTime ver_DataCriacao { get; set; }
        public System.DateTime ver_DataAlteracao { get; set; }
    }
}
