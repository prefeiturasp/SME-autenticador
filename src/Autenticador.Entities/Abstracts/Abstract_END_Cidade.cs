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
    public abstract class Abstract_END_Cidade : Abstract_Entity
    {
		[MSNotNullOrEmpty()]
		[DataObjectField(true, false, false)]
		public virtual Guid cid_id { get; set; }
		[MSNotNullOrEmpty()]
		public virtual Guid pai_id { get; set; }
		public virtual Guid unf_id { get; set; }
		[MSValidRange(200)]
		[MSNotNullOrEmpty()]
		public virtual string cid_nome { get; set; }
		[MSValidRange(3)]
		public virtual string cid_ddd { get; set; }
		[MSNotNullOrEmpty()]
		public virtual byte cid_situacao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual int cid_integridade { get; set; }

    }
}