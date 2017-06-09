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
    public class PES_PessoaEndereco : Abstract_PES_PessoaEndereco
    {
        [MSNotNullOrEmpty("Endereço é obrigatório.")]
        public override Guid end_id { get; set; }
        [MSValidRange(20, "Número pode conter até 20 caracteres.")]
        [MSNotNullOrEmpty("Número é obrigatório.")]
        public override string pse_numero { get; set; }
        [MSValidRange(100, "Complemento pode conter até 100 caracteres.")]
        public override string pse_complemento { get; set; }
        [MSDefaultValue(1)]
        public override byte pse_situacao { get; set; }
        public override DateTime pse_dataCriacao { get; set; }
        public override DateTime pse_dataAlteracao { get; set; }
    }
}
