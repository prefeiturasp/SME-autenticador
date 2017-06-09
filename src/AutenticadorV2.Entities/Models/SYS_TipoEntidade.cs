using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class TipoEntidade
    {
        public TipoEntidade()
        {
            this.SYS_Entidade = new List<Entidade>();
        }

        public System.Guid ten_id { get; set; }
        public string ten_nome { get; set; }
        public byte ten_situacao { get; set; }
        public System.DateTime ten_dataCriacao { get; set; }
        public System.DateTime ten_dataAlteracao { get; set; }
        public int ten_integridade { get; set; }
        public virtual ICollection<Entidade> SYS_Entidade { get; set; }
    }
}