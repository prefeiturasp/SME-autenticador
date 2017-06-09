using Autenticador.Entities.Abstracts;
using CoreLibrary.Validation;
using System;

namespace Autenticador.Entities
{
    [Serializable]
    public class SYS_TipoDocumentacao : Abstract_SYS_TipoDocumentacao
    {
        [MSValidRange(100, "Tipo de documentação pode conter até 100 caracteres.")]
        [MSNotNullOrEmpty("Tipo de documentação obrigatório.")]
        public override string tdo_nome { get; set; }

        [MSValidRange(10, "Sigla pode conter até 10 caracteres.")]
        public override string tdo_sigla { get; set; }

        [MSDefaultValue(1)]
        public override byte tdo_situacao { get; set; }

        public override DateTime tdo_dataCriacao { get; set; }

        public override DateTime tdo_dataAlteracao { get; set; }

        public override int tdo_integridade { get; set; }

        /// <summary>
        /// 1 - CPF, 2 - RG, 3 - PIS, 4 - NIS, 5 - Título de Eleitor, 6 - CNH, 7 - Reservista, 8 -
        /// CTPS, 9 - RNE, 10 - Guarda, 99-Outros.
        /// </summary>
        public override byte tdo_classificacao { get; set; }

        [MSValidRange(1024)]
        public override string tdo_atributos { get; set; }
    }
}