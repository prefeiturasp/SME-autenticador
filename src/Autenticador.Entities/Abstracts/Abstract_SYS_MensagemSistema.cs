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
	/// 
	/// </summary>
	[Serializable()]
    public abstract class Abstract_SYS_MensagemSistema : Abstract_Entity
    {

		/// <summary>
		/// 
		/// </summary>
		[MSNotNullOrEmpty()]
		[DataObjectField(true, true, false)]
		public virtual int mss_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[MSValidRange(100)]
		[MSNotNullOrEmpty()]
		public virtual string mss_chave { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[MSNotNullOrEmpty()]
		public virtual string mss_valor { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[MSValidRange(200)]
		public virtual string mss_descricao { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[MSNotNullOrEmpty()]
		public virtual byte mss_situacao { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[MSNotNullOrEmpty()]
		public virtual DateTime mss_dataCriacao { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[MSNotNullOrEmpty()]
		public virtual DateTime mss_dataAlteracao { get; set; }

    }
}