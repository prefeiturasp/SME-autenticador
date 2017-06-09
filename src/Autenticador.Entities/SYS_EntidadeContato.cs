using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autenticador.Entities.Abstracts;
using CoreLibrary.Validation;
using System.ComponentModel;

namespace Autenticador.Entities
{
    [Serializable]
    public class SYS_EntidadeContato : Abstract_SYS_EntidadeContato
    {
        [MSNotNullOrEmpty()]
        [DataObjectField(true, false, false)]
        public override Guid enc_id { get; set; }
        [MSNotNullOrEmpty("Tipo de contato é obrigatório.")]
        public override Guid tmc_id { get; set; }
        [MSValidRange(200, "Contato pode conter até 200 caracteres.")]
        [MSNotNullOrEmpty("Contato é obrigatório.")]
        public override string enc_contato { get; set; }
        [MSDefaultValue(1)]
        public override byte enc_situacao { get; set; }
        public override DateTime enc_dataCriacao { get; set; }
        public override DateTime enc_dataAlteracao { get; set; }
    }
}
