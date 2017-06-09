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
    public abstract class Abstract_SYS_Visao : Abstract_Entity
    {
		[MSNotNullOrEmpty()]
		[DataObjectField(true, true, false)]
		public virtual int vis_id { get; set; }
		[MSValidRange(50)]
		[MSNotNullOrEmpty()]
		public virtual string vis_nome { get; set; }

    }
}