using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class SYS_DiaNaoUtil
    {
        public System.Guid dnu_id { get; set; }
        public string dnu_nome { get; set; }
        public byte dnu_abrangencia { get; set; }
        public string dnu_descricao { get; set; }
        public System.DateTime dnu_data { get; set; }
        public Nullable<bool> dnu_recorrencia { get; set; }
        public System.DateTime dnu_vigenciaInicio { get; set; }
        public Nullable<System.DateTime> dnu_vigenciaFim { get; set; }
        public Nullable<System.Guid> cid_id { get; set; }
        public Nullable<System.Guid> unf_id { get; set; }
        public byte dnu_situacao { get; set; }
        public System.DateTime dnu_dataCriacao { get; set; }
        public System.DateTime dnu_dataAlteracao { get; set; }
        public virtual END_Cidade END_Cidade { get; set; }
        public virtual END_UnidadeFederativa END_UnidadeFederativa { get; set; }
    }
}
