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
    public abstract class Abstract_SYS_UnidadeAdministrativaContato : Abstract_Entity
    {
		[MSNotNullOrEmpty()]
		[DataObjectField(true, false, false)]
		public virtual Guid ent_id { get; set; }
		[MSNotNullOrEmpty()]
		[DataObjectField(true, false, false)]
		public virtual Guid uad_id { get; set; }
		[MSNotNullOrEmpty()]
		[DataObjectField(true, false, false)]
		public virtual Guid uac_id { get; set; }
		[MSNotNullOrEmpty()]
		public virtual Guid tmc_id { get; set; }
		[MSValidRange(200)]
		[MSNotNullOrEmpty()]
		public virtual string uac_contato { get; set; }
		[MSNotNullOrEmpty()]
		public virtual byte uac_situacao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual DateTime uac_dataCriacao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual DateTime uac_dataAlteracao { get; set; }

    }
}