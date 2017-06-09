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
    public abstract class Abstract_SYS_TipoUnidadeAdministrativa : Abstract_Entity
    {
		[MSNotNullOrEmpty()]
		[DataObjectField(true, false, false)]
		public virtual Guid tua_id { get; set; }
		[MSValidRange(100)]
		[MSNotNullOrEmpty()]
		public virtual string tua_nome { get; set; }
		[MSNotNullOrEmpty()]
		public virtual byte tua_situacao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual DateTime tua_dataCriacao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual DateTime tua_dataAlteracao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual int tua_integridade { get; set; }

    }
}