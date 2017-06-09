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
    public class SYS_TipoUnidadeAdministrativa : Abstract_SYS_TipoUnidadeAdministrativa
    {
        [MSValidRange(100, "Tipo de unidade administrativap pode conter até 100 caracteres.")]
        [MSNotNullOrEmpty("Tipo de unidade administrativa é obrigatório.")]
        public override string tua_nome { get; set; }
        [MSDefaultValue(1)]
        public override byte tua_situacao { get; set; }
        public override DateTime tua_dataCriacao { get; set; }
        public override DateTime tua_dataAlteracao { get; set; }
        public override int tua_integridade { get; set; }
    }
}
