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
    public abstract class Abstract_PES_TipoDeficiencia : Abstract_Entity
    {
		[MSNotNullOrEmpty()]
		[DataObjectField(true, false, false)]
		public virtual Guid tde_id { get; set; }
		[MSValidRange(100)]
		[MSNotNullOrEmpty()]
		public virtual string tde_nome { get; set; }
		[MSNotNullOrEmpty()]
		public virtual byte tde_situacao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual DateTime tde_dataCriacao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual DateTime tde_dataAlteracao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual int tde_integridade { get; set; }

    }
}