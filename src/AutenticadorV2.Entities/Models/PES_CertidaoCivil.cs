using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class PES_CertidaoCivil
    {
        public System.Guid pes_id { get; set; }
        public System.Guid ctc_id { get; set; }
        public byte ctc_tipo { get; set; }
        public string ctc_numeroTermo { get; set; }
        public string ctc_folha { get; set; }
        public string ctc_livro { get; set; }
        public Nullable<System.DateTime> ctc_dataEmissao { get; set; }
        public string ctc_nomeCartorio { get; set; }
        public Nullable<System.Guid> cid_idCartorio { get; set; }
        public Nullable<System.Guid> unf_idCartorio { get; set; }
        public string ctc_distritoCartorio { get; set; }
        public byte ctc_situacao { get; set; }
        public System.DateTime ctc_dataCriacao { get; set; }
        public System.DateTime ctc_dataAlteracao { get; set; }
        public string ctc_matricula { get; set; }
        public virtual END_Cidade END_Cidade { get; set; }
        public virtual END_UnidadeFederativa END_UnidadeFederativa { get; set; }
        public virtual PES_Pessoa PES_Pessoa { get; set; }
    }
}
