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
    public class PES_PessoaContato : Abstract_PES_PessoaContato
    {
        [MSNotNullOrEmpty("Tipo de contato é obrigatório.")]
        public override Guid tmc_id { get; set; }
        [MSValidRange(200, "Contato pode conter até 200 caracteres.")]
        [MSNotNullOrEmpty("Contato é obrigatório.")]
        public override string psc_contato { get; set; }
        [MSDefaultValue(1)]
        public override byte psc_situacao { get; set; }
        public override DateTime psc_dataCriacao { get; set; }
        public override DateTime psc_dataAlteracao { get; set; }
    }
}
