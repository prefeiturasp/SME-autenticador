using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class SYS_TipoDocumentacao
    {
        public SYS_TipoDocumentacao()
        {
            this.PES_PessoaDocumento = new List<PES_PessoaDocumento>();
        }

        public System.Guid tdo_id { get; set; }

        public string tdo_nome { get; set; }

        public string tdo_sigla { get; set; }

        public Nullable<byte> tdo_validacao { get; set; }

        public byte tdo_situacao { get; set; }

        public DateTime tdo_dataCriacao { get; set; }

        public DateTime tdo_dataAlteracao { get; set; }

        public int tdo_integridade { get; set; }

        public byte tdo_classificacao { get; set; }

        public string tdo_atributos { get; set; }

        public virtual ICollection<PES_PessoaDocumento> PES_PessoaDocumento { get; set; }
    }
}