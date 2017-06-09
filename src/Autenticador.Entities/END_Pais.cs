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
    public class END_Pais : Abstract_END_Pais
    {
        [MSValidRange(100, "Nome pode conter até 100 caracteres.")]
        [MSNotNullOrEmpty("Nome é obrigatório.")]
        public override string pai_nome { get; set; }
        [MSValidRange(10, "Sigla pode conter até 10 caracteres.")]
        public override string pai_sigla { get; set; }
        [MSValidRange(3, "DDI pode conter até 3 caracteres.")]
        public override string pai_ddi { get; set; }
        [MSValidRange(100, "Naturalidade masculino pode conter até 100 caracteres.")]
        public override string pai_naturalMasc { get; set; }
        [MSValidRange(100, "Naturalidade feminino pode conter até 100 caracteres.")]
        public override string pai_naturalFem { get; set; }
        [MSDefaultValue(1)]
        public override byte pai_situacao { get; set; }
        public override int pai_integridade { get; set; }
    }
}
