using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class END_Pais
    {
        public END_Pais()
        {
            this.END_Cidade = new List<END_Cidade>();
            this.END_UnidadeFederativa = new List<END_UnidadeFederativa>();
            this.PES_Pessoa = new List<PES_Pessoa>();
        }

        public System.Guid pai_id { get; set; }
        public string pai_nome { get; set; }
        public string pai_sigla { get; set; }
        public string pai_ddi { get; set; }
        public string pai_naturalMasc { get; set; }
        public string pai_naturalFem { get; set; }
        public byte pai_situacao { get; set; }
        public int pai_integridade { get; set; }
        public virtual ICollection<END_Cidade> END_Cidade { get; set; }
        public virtual ICollection<END_UnidadeFederativa> END_UnidadeFederativa { get; set; }
        public virtual ICollection<PES_Pessoa> PES_Pessoa { get; set; }
    }
}
