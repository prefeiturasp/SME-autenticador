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
    public abstract class Abstract_END_UnidadeFederativa : Abstract_Entity
    {
		[MSNotNullOrEmpty()]
		[DataObjectField(true, false, false)]
		public virtual Guid unf_id { get; set; }
		[MSNotNullOrEmpty()]
		public virtual Guid pai_id { get; set; }
		[MSValidRange(100)]
		[MSNotNullOrEmpty()]
		public virtual string unf_nome { get; set; }
		[MSValidRange(2)]
		[MSNotNullOrEmpty()]
		public virtual string unf_sigla { get; set; }
		[MSNotNullOrEmpty()]
		public virtual byte unf_situacao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual int unf_integridade { get; set; }

    }
}