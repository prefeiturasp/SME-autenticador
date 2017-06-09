using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class PES_PessoaDocumento
    {
        public System.Guid pes_id { get; set; }
        public System.Guid tdo_id { get; set; }
        public string psd_numero { get; set; }
        public Nullable<System.DateTime> psd_dataEmissao { get; set; }
        public string psd_orgaoEmissao { get; set; }
        public Nullable<System.Guid> unf_idEmissao { get; set; }
        public string psd_infoComplementares { get; set; }
        public byte psd_situacao { get; set; }
        public System.DateTime psd_dataCriacao { get; set; }
        public System.DateTime psd_dataAlteracao { get; set; }
        public virtual END_UnidadeFederativa END_UnidadeFederativa { get; set; }
        public virtual PES_Pessoa PES_Pessoa { get; set; }
        public virtual SYS_TipoDocumentacao SYS_TipoDocumentacao { get; set; }
    }
}
