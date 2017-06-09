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
    public abstract class Abstract_LOG_UsuarioADErro : Abstract_Entity
    {
		
		/// <summary>
		/// Propriedade usa_id.
		/// </summary>
		[MSNotNullOrEmpty]
		[DataObjectField(true, false, false)]
		public virtual long usa_id { get; set; }

		/// <summary>
		/// Propriedade use_descricaoErro.
		/// </summary>
		[MSNotNullOrEmpty]
		public virtual string use_descricaoErro { get; set; }

    }
}