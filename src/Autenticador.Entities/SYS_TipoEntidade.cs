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
    public class SYS_TipoEntidade : Abstract_SYS_TipoEntidade
    {
        [MSValidRange(100, "Tipo de entidade pode conter até 100 caracteres.")]
        [MSNotNullOrEmpty("Tipo de entidade é obrigatório.")]
        public override string ten_nome { get; set; }
        [MSDefaultValue(1)]
        public override byte ten_situacao { get; set; }
        public override DateTime ten_dataCriacao { get; set; }
        public override DateTime ten_dataAlteracao { get; set; }
        public override int ten_integridade { get; set; }
    }
}
