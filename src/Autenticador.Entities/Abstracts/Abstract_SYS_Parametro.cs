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
    public abstract class Abstract_SYS_Parametro : Abstract_Entity
    {
		[MSNotNullOrEmpty()]
		[DataObjectField(true, false, false)]
		public virtual Guid par_id { get; set; }
		[MSValidRange(100)]
		[MSNotNullOrEmpty()]
		public virtual string par_chave { get; set; }
		[MSValidRange(1000)]
		[MSNotNullOrEmpty()]
		public virtual string par_valor { get; set; }
		[MSValidRange(200)]
		public virtual string par_descricao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual byte par_situacao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual DateTime par_vigenciaInicio { get; set; }
		public virtual DateTime par_vigenciaFim { get; set; }
		[MSNotNullOrEmpty()]
		public virtual DateTime par_dataCriacao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual DateTime par_dataAlteracao { get; set; }
		public virtual bool par_obrigatorio { get; set; }

    }
}