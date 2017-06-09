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
    public abstract class Abstract_PES_TipoEscolaridade : Abstract_Entity
    {
		[MSNotNullOrEmpty()]
		[DataObjectField(true, false, false)]
		public virtual Guid tes_id { get; set; }
		[MSValidRange(100)]
		[MSNotNullOrEmpty()]
		public virtual string tes_nome { get; set; }
		[MSNotNullOrEmpty()]
		public virtual int tes_ordem { get; set; }
		[MSNotNullOrEmpty()]
		public virtual byte tes_situacao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual DateTime tes_dataCriacao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual DateTime tes_dataAlteracao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual int tes_integridade { get; set; }

    }
}