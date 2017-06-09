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
    public abstract class Abstract_SYS_UsuarioSenhaHistorico : Abstract_Entity
    {
		
		/// <summary>
		/// ID do usu�rio.
		/// </summary>
		[MSNotNullOrEmpty]
		[DataObjectField(true, false, false)]
		public virtual Guid usu_id { get; set; }

		/// <summary>
		/// Senha do usu�rio.
		/// </summary>
		[MSValidRange(256)]
		[MSNotNullOrEmpty]
		public virtual string ush_senha { get; set; }

		/// <summary>
		/// Tipo de criptografia da senha.
		/// </summary>
		public virtual short ush_criptografia { get; set; }

		/// <summary>
		/// ID do hist�rico de senha.
		/// </summary>
		[MSNotNullOrEmpty]
		[DataObjectField(true, false, false)]
		public virtual Guid ush_id { get; set; }

		/// <summary>
		/// Data do hist�rico de senha.
		/// </summary>
		[MSNotNullOrEmpty]
		public virtual DateTime ush_data { get; set; }

    }
}