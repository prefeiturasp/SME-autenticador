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
    public abstract class Abstract_SYS_Modulo : Abstract_Entity
    {
		[MSNotNullOrEmpty()]
		[DataObjectField(true, false, false)]
		public virtual int sis_id { get; set; }
		[MSNotNullOrEmpty()]
		[DataObjectField(true, false, false)]
		public virtual int mod_id { get; set; }
		[MSValidRange(50)]
		[MSNotNullOrEmpty()]
		public virtual string mod_nome { get; set; }
		public virtual string mod_descricao { get; set; }
		public virtual int mod_idPai { get; set; }
		[MSNotNullOrEmpty()]
		public virtual bool mod_auditoria { get; set; }
		[MSNotNullOrEmpty()]
		public virtual byte mod_situacao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual DateTime mod_dataCriacao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual DateTime mod_dataAlteracao { get; set; }

    }
}