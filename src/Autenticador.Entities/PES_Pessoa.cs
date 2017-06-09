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
    public class PES_Pessoa : Abstract_PES_Pessoa
    {
        [MSValidRange(200, "Nome pode conter até 200 caracteres.")]
        [MSNotNullOrEmpty("Nome é obrigatório.")]
        public override string pes_nome { get; set; }
        [MSValidRange(50, "Nome abreviado pode conter até 50 caracteres.")]
        public override string pes_nome_abreviado { get; set; }
        [MSDefaultValue(1)]
        public override byte pes_situacao { get; set; }
        public override DateTime pes_dataCriacao { get; set; }
        public override DateTime pes_dataAlteracao { get; set; }
        public override int pes_integridade { get; set; }
    }
}
