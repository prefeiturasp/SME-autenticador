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
    public abstract class AbstractLOG_UsuarioAD : Abstract_Entity
    {
		
		/// <summary>
		/// Propriedade usa_id.
		/// </summary>
		[MSNotNullOrEmpty]
		[DataObjectField(true, true, false)]
		public virtual long usa_id { get; set; }

		/// <summary>
		/// Propriedade usu_id.
		/// </summary>
		[MSNotNullOrEmpty]
		public virtual Guid usu_id { get; set; }

		/// <summary>
		/// Propriedade usa_acao.
		/// </summary>
		[MSNotNullOrEmpty]
		public virtual short usa_acao { get; set; }

		/// <summary>
		/// Propriedade usa_status.
		/// </summary>
		[MSNotNullOrEmpty]
		public virtual short usa_status { get; set; }

		/// <summary>
		/// Propriedade usa_dataAcao.
		/// </summary>
		[MSNotNullOrEmpty]
		public virtual DateTime usa_dataAcao { get; set; }

		/// <summary>
		/// Propriedade usa_origemAcao.
		/// </summary>
		[MSNotNullOrEmpty]
		public virtual short usa_origemAcao { get; set; }

		/// <summary>
		/// Propriedade usa_dataProcessado.
		/// </summary>
		public virtual DateTime usa_dataProcessado { get; set; }

		/// <summary>
		/// Propriedade usa_dados.
		/// </summary>
		[MSNotNullOrEmpty]
		public virtual string usa_dados { get; set; }

    }
}