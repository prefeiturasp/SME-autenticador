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
    public abstract class Abstract_CFG_Arquivo : Abstract_Entity
    {

		/// <summary>
		/// 
		/// </summary>
		[MSNotNullOrEmpty()]
		[DataObjectField(true, true, false)]
		public virtual Int64 arq_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[MSValidRange(256)]
		[MSNotNullOrEmpty()]
		public virtual string arq_nome { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[MSNotNullOrEmpty()]
		public virtual Int64 arq_tamanhoKB { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[MSValidRange(200)]
		[MSNotNullOrEmpty()]
		public virtual string arq_typeMime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[MSNotNullOrEmpty()]
		public virtual byte[] arq_data { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[MSNotNullOrEmpty()]
		public virtual byte arq_situacao { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[MSNotNullOrEmpty()]
		public virtual DateTime arq_dataCriacao { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[MSNotNullOrEmpty()]
		public virtual DateTime arq_dataAlteracao { get; set; }

    }
}