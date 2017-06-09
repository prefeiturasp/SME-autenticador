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
    public abstract class Abstract_SYS_Grupo : Abstract_Entity
    {
		[MSNotNullOrEmpty()]
		[DataObjectField(true, false, false)]
		public virtual Guid gru_id { get; set; }
		[MSValidRange(50)]
		[MSNotNullOrEmpty()]
		public virtual string gru_nome { get; set; }
		[MSNotNullOrEmpty()]
		public virtual byte gru_situacao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual DateTime gru_dataCriacao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual DateTime gru_dataAlteracao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual int vis_id { get; set; }
		[MSNotNullOrEmpty()]
		public virtual int sis_id { get; set; }
		[MSNotNullOrEmpty()]
		public virtual int gru_integridade { get; set; }

    }
}