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
    public abstract class Abstract_CFG_UsuarioAPI : Abstract_Entity
    {
		
		/// <summary>
		/// Propriedade uap_id.
		/// </summary>
		[MSNotNullOrEmpty]
		[DataObjectField(true, true, false)]
		public virtual int uap_id { get; set; }

		/// <summary>
		/// Propriedade uap_username.
		/// </summary>
		[MSValidRange(100)]
		[MSNotNullOrEmpty]
		public virtual string uap_username { get; set; }

		/// <summary>
		/// Propriedade uap_password.
		/// </summary>
		[MSValidRange(256)]
		[MSNotNullOrEmpty]
		public virtual string uap_password { get; set; }

		/// <summary>
		/// Propriedade uap_situacao.
		/// </summary>
		[MSNotNullOrEmpty]
		public virtual byte uap_situacao { get; set; }

		/// <summary>
		/// Propriedade uap_dataCriacao.
		/// </summary>
		[MSNotNullOrEmpty]
		public virtual DateTime uap_dataCriacao { get; set; }

		/// <summary>
		/// Propriedade uap_dataAlteracao.
		/// </summary>
		[MSNotNullOrEmpty]
		public virtual DateTime uap_dataAlteracao { get; set; }

    }
}