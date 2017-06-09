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
    public abstract class Abstract_SYS_EntidadeContato : Abstract_Entity
    {
		[MSNotNullOrEmpty()]
		[DataObjectField(true, false, false)]
		public virtual Guid ent_id { get; set; }
		[MSNotNullOrEmpty()]
		[DataObjectField(true, false, false)]
		public virtual Guid enc_id { get; set; }
		[MSNotNullOrEmpty()]
		public virtual Guid tmc_id { get; set; }
		[MSValidRange(200)]
		[MSNotNullOrEmpty()]
		public virtual string enc_contato { get; set; }
		[MSNotNullOrEmpty()]
		public virtual byte enc_situacao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual DateTime enc_dataCriacao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual DateTime enc_dataAlteracao { get; set; }

    }
}