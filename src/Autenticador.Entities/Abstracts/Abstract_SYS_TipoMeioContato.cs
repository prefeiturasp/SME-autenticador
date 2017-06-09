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
    public abstract class Abstract_SYS_TipoMeioContato : Abstract_Entity
    {
		[MSNotNullOrEmpty()]
		[DataObjectField(true, false, false)]
		public virtual Guid tmc_id { get; set; }
		[MSValidRange(100)]
		[MSNotNullOrEmpty()]
		public virtual string tmc_nome { get; set; }
		public virtual byte tmc_validacao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual byte tmc_situacao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual DateTime tmc_dataCriacao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual DateTime tmc_dataAlteracao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual int tmc_integridade { get; set; }

    }
}