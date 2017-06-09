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
    public class SYS_UnidadeAdministrativaContato : Abstract_SYS_UnidadeAdministrativaContato
    {
        [MSNotNullOrEmpty("Tipo de contato é obrigatório.")]
        public override Guid tmc_id { get; set; }
        [MSValidRange(200, "Contato pode conter até 200 caracteres.")]
        [MSNotNullOrEmpty("Contato é obrigatório.")]
        public override string uac_contato { get; set; }
        [MSDefaultValue(1)]
        public override byte uac_situacao { get; set; }
        public override DateTime uac_dataCriacao { get; set; }
        public override DateTime uac_dataAlteracao { get; set; }
    }
}
