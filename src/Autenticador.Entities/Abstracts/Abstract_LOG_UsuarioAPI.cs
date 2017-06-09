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
    public abstract class Abstract_LOG_UsuarioAPI : Abstract_Entity
    {
		
		/// <summary>
		/// Propriedade lua_id.
		/// </summary>
		[MSNotNullOrEmpty]
		[DataObjectField(true, true, false)]
		public virtual long lua_id { get; set; }

		/// <summary>
		/// Propriedade usu_id.
		/// </summary>
		[MSNotNullOrEmpty]
		public virtual Guid usu_id { get; set; }

		/// <summary>
		/// Propriedade uap_id.
		/// </summary>
		[MSNotNullOrEmpty]
		public virtual int uap_id { get; set; }

		/// <summary>
		/// Propriedade lua_acao.
		/// </summary>
		[MSNotNullOrEmpty]
		public virtual byte lua_acao { get; set; }

		/// <summary>
		/// Propriedade lua_dataHora.
		/// </summary>
		[MSNotNullOrEmpty]
		public virtual DateTime lua_dataHora { get; set; }

    }
}