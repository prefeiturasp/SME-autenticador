/*
	Classe gerada automaticamente pelo Code Creator
*/

namespace Autenticador.Entities.Abstracts
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.ComponentModel;
	using CoreLibrary.Data.Common.Abstracts;
	using CoreLibrary.Validation;
	
	/// <summary>
	/// Description: .
	/// </summary>
	[Serializable]
    public abstract class Abstract_SYS_Servico : Abstract_Entity
    {
		
		/// <summary>
		/// Propriedade ser_id.
		/// </summary>
		[MSNotNullOrEmpty]
		[DataObjectField(true, false, false)]
        public virtual Int16 ser_id { get; set; }

		/// <summary>
		/// Propriedade ser_nome.
		/// </summary>
		[MSNotNullOrEmpty]
		public virtual string ser_nome { get; set; }

		/// <summary>
		/// Propriedade ser_nomeProcedimento.
		/// </summary>
		[MSNotNullOrEmpty]
		public virtual string ser_nomeProcedimento { get; set; }

		/// <summary>
		/// Propriedade ser_ativo.
		/// </summary>
		[MSNotNullOrEmpty]
		public virtual bool ser_ativo { get; set; }

    }
}