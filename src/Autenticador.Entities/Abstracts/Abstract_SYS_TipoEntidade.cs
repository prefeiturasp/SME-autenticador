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
    public abstract class Abstract_SYS_TipoEntidade : Abstract_Entity
    {
		[MSNotNullOrEmpty()]
		[DataObjectField(true, false, false)]
		public virtual Guid ten_id { get; set; }
		[MSValidRange(100)]
		[MSNotNullOrEmpty()]
		public virtual string ten_nome { get; set; }
		[MSNotNullOrEmpty()]
		public virtual byte ten_situacao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual DateTime ten_dataCriacao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual DateTime ten_dataAlteracao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual int ten_integridade { get; set; }

    }
}