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
    public class PES_TipoDeficiencia : Abstract_PES_TipoDeficiencia
    {
        [MSValidRange(100, "Tipo de deficiência pode conter até 100 caracteres.")]
        [MSNotNullOrEmpty("Tipo deficiência obrigatório.")]
        public override string tde_nome { get; set; }
        [MSDefaultValue(1)]
        public override byte tde_situacao { get; set; }
        public override DateTime tde_dataCriacao { get; set; }
        public override DateTime tde_dataAlteracao { get; set; }
        public override int tde_integridade { get; set; }
    }
}
