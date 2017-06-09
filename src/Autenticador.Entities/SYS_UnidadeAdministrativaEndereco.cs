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
    public class SYS_UnidadeAdministrativaEndereco : Abstract_SYS_UnidadeAdministrativaEndereco
    {
        [MSNotNullOrEmpty("Endereço é obrigatório.")]
        public override Guid end_id { get; set; }
        [MSValidRange(20, "Número pode conter até 20 caracteres.")]
        [MSNotNullOrEmpty("Número é obrigatório.")]
        public override string uae_numero { get; set; }
        [MSValidRange(100, "Complemento pode conter até 100 caracteres.")]
        public override string uae_complemento { get; set; }
        [MSDefaultValue(1)]
        public override byte uae_situacao { get; set; }
        public override DateTime uae_dataCriacao { get; set; }
        public override DateTime uae_dataAlteracao { get; set; }
        public override bool uae_enderecoPrincipal {get;set;}
        public override decimal uae_latitude {get;set;}
        public override decimal uae_longitude {get;set;}
    }
}
