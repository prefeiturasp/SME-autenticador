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
    public abstract class Abstract_CFG_Configuracao : Abstract_Entity
    {
		[MSNotNullOrEmpty()]
		[DataObjectField(true, false, false)]
		public virtual Guid cfg_id { get; set; }
		[MSValidRange(100)]
		[MSNotNullOrEmpty()]
		public virtual string cfg_chave { get; set; }
		[MSValidRange(300)]
		[MSNotNullOrEmpty()]
		public virtual string cfg_valor { get; set; }
		[MSValidRange(200)]
		public virtual string cfg_descricao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual byte cfg_situacao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual DateTime cfg_dataCriacao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual DateTime cfg_dataAlteracao { get; set; }

    }
}