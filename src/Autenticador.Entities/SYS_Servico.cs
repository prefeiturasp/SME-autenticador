/*
	Classe gerada automaticamente pelo Code Creator
*/

namespace Autenticador.Entities
{
    using System;
    using System.ComponentModel;
    using Autenticador.Entities.Abstracts;
    using CoreLibrary.Validation;
		
	/// <summary>
	/// Description: .
	/// </summary>
    [Serializable]
	public class SYS_Servico : Abstract_SYS_Servico
	{
        /// <summary>
        /// Propriedade ser_id.
        /// </summary>
        [MSNotNullOrEmpty("ID do servi�o � obrigat�rio.")]
        [DataObjectField(true, false, false)]
        public virtual Int16 ser_id { get; set; }

        /// <summary>
        /// Propriedade ser_nome.
        /// </summary>
        [MSNotNullOrEmpty("Nome do servi�o � obrigat�rio.")]
        public virtual string ser_nome { get; set; }

        /// <summary>
        /// Propriedade ser_nomeProcedimento.
        /// </summary>
        [MSNotNullOrEmpty("Nome do procedimento do servi�o � obrigat�rio.")]
        public virtual string ser_nomeProcedimento { get; set; }

        /// <summary>
        /// Propriedade ser_ativo.
        /// </summary>
        [MSNotNullOrEmpty("Situa��o do servi�o � obrigat�rio.")]
        public virtual bool ser_ativo { get; set; }

        public string ser_ativoDescricao { get; set; }
	}
}