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
    public abstract class Abstract_LOG_Sistema : Abstract_Entity
    {
        [MSNotNullOrEmpty()]
        public virtual Guid log_id { get; set; }
		[MSNotNullOrEmpty()]
		public virtual DateTime log_dataHora { get; set; }
		[MSValidRange(15)]
		[MSNotNullOrEmpty()]
		public virtual string log_ip { get; set; }
		[MSValidRange(256)]
		[MSNotNullOrEmpty()]
		public virtual string log_machineName { get; set; }
		[MSValidRange(50)]
		[MSNotNullOrEmpty()]
		public virtual string log_acao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual string log_descricao { get; set; }
		public virtual int sis_id { get; set; }
		[MSValidRange(50)]
        public virtual string sis_nome { get; set; }
		public virtual int mod_id { get; set; }
		[MSValidRange(50)]
		public virtual string mod_nome { get; set; }
		public virtual Guid usu_id { get; set; }
		[MSValidRange(100)]
		public virtual string usu_login { get; set; }
		public virtual Guid gru_id { get; set; }
		[MSValidRange(50)]
		public virtual string gru_nome { get; set; }
		public virtual string log_grupoUA { get; set; }

    }
}