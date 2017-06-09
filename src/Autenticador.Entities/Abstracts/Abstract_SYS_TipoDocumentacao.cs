/*
	Classe gerada automaticamente pelo Code Creator
*/

using CoreLibrary.Data.Common.Abstracts;
using CoreLibrary.Validation;
using System;
using System.ComponentModel;

namespace Autenticador.Entities.Abstracts
{
    /// <summary>
    ///
    /// </summary>
    [Serializable()]
    public abstract class Abstract_SYS_TipoDocumentacao : Abstract_Entity
    {
        [MSNotNullOrEmpty()]
        [DataObjectField(true, false, false)]
        public virtual Guid tdo_id { get; set; }

        [MSValidRange(100)]
        [MSNotNullOrEmpty()]
        public virtual string tdo_nome { get; set; }

        [MSValidRange(10)]
        public virtual string tdo_sigla { get; set; }

        public virtual byte tdo_validacao { get; set; }

        [MSNotNullOrEmpty()]
        public virtual byte tdo_situacao { get; set; }

        [MSNotNullOrEmpty()]
        public virtual DateTime tdo_dataCriacao { get; set; }

        [MSNotNullOrEmpty()]
        public virtual DateTime tdo_dataAlteracao { get; set; }

        [MSNotNullOrEmpty()]
        public virtual int tdo_integridade { get; set; }

        /// <summary>
        /// 1 - CPF, 2 - RG, 3 - PIS, 4 - NIS, 5 - T�tulo de Eleitor, 6 - CNH, 7 - Reservista, 8 - CTPS, 9 - RNE, 10 - Guarda, 99-Outros.
        /// </summary>
        public virtual byte tdo_classificacao { get; set; }

        [MSValidRange(1024)]
        public virtual string tdo_atributos { get; set; }
    }
}