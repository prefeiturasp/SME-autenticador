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
    public class END_UnidadeFederativa : Abstract_END_UnidadeFederativa
    {
        [MSNotNullOrEmpty("País é obrigatório.")]
        public override Guid pai_id { get; set; }
        [MSValidRange(100, "Nome pode conter até 100 caracteres.")]
        [MSNotNullOrEmpty("Nome é obrigatório.")]
        public override string unf_nome { get; set; }
        [MSValidRange(2, 2, "Sigla deve conter 2 caracteres.")]
        [MSNotNullOrEmpty("Sigla é obrigatório.")]
        public override string unf_sigla { get; set; }
        [MSDefaultValue(1)]
        public override byte unf_situacao { get; set; }
        public override int unf_integridade { get; set; }
    }
}
