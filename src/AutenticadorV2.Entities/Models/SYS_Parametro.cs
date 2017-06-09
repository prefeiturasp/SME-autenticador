using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class SYS_Parametro
    {
        public System.Guid par_id { get; set; }
        public string par_chave { get; set; }
        public string par_valor { get; set; }
        public string par_descricao { get; set; }
        public byte par_situacao { get; set; }
        public System.DateTime par_vigenciaInicio { get; set; }
        public Nullable<System.DateTime> par_vigenciaFim { get; set; }
        public System.DateTime par_dataCriacao { get; set; }
        public System.DateTime par_dataAlteracao { get; set; }
        public Nullable<bool> par_obrigatorio { get; set; }
    }
}
