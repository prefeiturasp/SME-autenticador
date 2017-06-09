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
    public abstract class Abstract_END_Pais : Abstract_Entity
    {
		[MSNotNullOrEmpty()]
		[DataObjectField(true, false, false)]
		public virtual Guid pai_id { get; set; }
		[MSValidRange(100)]
		[MSNotNullOrEmpty()]
		public virtual string pai_nome { get; set; }
		[MSValidRange(10)]
		public virtual string pai_sigla { get; set; }
		[MSValidRange(3)]
		public virtual string pai_ddi { get; set; }
		[MSValidRange(100)]
		public virtual string pai_naturalMasc { get; set; }
		[MSValidRange(100)]
		public virtual string pai_naturalFem { get; set; }
		[MSNotNullOrEmpty()]
		public virtual byte pai_situacao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual int pai_integridade { get; set; }

    }
}