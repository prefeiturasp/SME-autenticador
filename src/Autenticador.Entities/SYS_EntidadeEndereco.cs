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
    public class SYS_EntidadeEndereco : Abstract_SYS_EntidadeEndereco
    {
        [MSNotNullOrEmpty("Endereço é obrigatório.")]
        public override Guid end_id { get; set; }
        [MSValidRange(20, "Número pode conter até 20 caracteres.")]
        [MSNotNullOrEmpty("Número é obrigatório.")]
        public override string ene_numero { get; set; }
        [MSValidRange(100, "Complemento pode conter até 100 caracteres.")]
        public override string ene_complemento { get; set; }
        [MSDefaultValue(1)]
        public override byte ene_situacao { get; set; }
        public override DateTime ene_dataCriacao { get; set; }
        public override DateTime ene_dataAlteracao { get; set; }
    }
}
