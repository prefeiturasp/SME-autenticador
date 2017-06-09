/*
	Classe gerada automaticamente pelo Code Creator
*/
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.ComponentModel;
	using CoreLibrary.Data.Common.Abstracts;
	using CoreLibrary.Validation;

namespace Autenticador.Entities.Abstracts
{

	
	/// <summary>
	/// Description: .
	/// </summary>
	[Serializable]
    public abstract class Abstract_SYS_UsuarioLoginProvider : Abstract_Entity
    {
		
		/// <summary>
		/// Propriedade LoginProvider.
		/// </summary>
		[MSValidRange(128)]
		[MSNotNullOrEmpty]
		[DataObjectField(true, false, false)]
		public virtual string LoginProvider { get; set; }

		/// <summary>
		/// Propriedade ProviderKey.
		/// </summary>
		[MSValidRange(512)]
		[MSNotNullOrEmpty]
		[DataObjectField(true, false, false)]
		public virtual string ProviderKey { get; set; }

		/// <summary>
		/// Propriedade usu_id.
		/// </summary>
		[MSNotNullOrEmpty]
		[DataObjectField(true, false, false)]
		public virtual Guid usu_id { get; set; }

		/// <summary>
		/// Propriedade Username.
		/// </summary>
		[MSValidRange(128)]
		public virtual string Username { get; set; }

    }
}