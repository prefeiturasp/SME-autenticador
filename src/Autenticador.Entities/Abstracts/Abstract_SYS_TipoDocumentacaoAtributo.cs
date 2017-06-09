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
    public abstract class Abstract_SYS_TipoDocumentacaoAtributo : Abstract_Entity
    {
		
		/// <summary>
		/// Propriedade tda_id.
		/// </summary>
		[MSNotNullOrEmpty]
		[DataObjectField(true, false, false)]
		public virtual byte tda_id { get; set; }

		/// <summary>
		/// Propriedade tda_descricao.
		/// </summary>
		[MSValidRange(64)]
		public virtual string tda_descricao { get; set; }

		/// <summary>
		/// Propriedade tda_nomeObjeto.
		/// </summary>
		[MSValidRange(256)]
		public virtual string tda_nomeObjeto { get; set; }

		/// <summary>
		/// Propriedade tda_default.
		/// </summary>
		public virtual bool tda_default { get; set; }

    }
}