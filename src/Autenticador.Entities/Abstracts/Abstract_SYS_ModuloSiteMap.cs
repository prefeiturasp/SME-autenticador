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
    public abstract class Abstract_SYS_ModuloSiteMap : Abstract_Entity
    {
		[MSNotNullOrEmpty()]
		[DataObjectField(true, false, false)]
		public virtual int sis_id { get; set; }
		[MSNotNullOrEmpty()]
		[DataObjectField(true, false, false)]
		public virtual int mod_id { get; set; }
		[MSNotNullOrEmpty()]
		[DataObjectField(true, false, false)]
		public virtual int msm_id { get; set; }
		[MSValidRange(50)]
		[MSNotNullOrEmpty()]
		public virtual string msm_nome { get; set; }
		[MSValidRange(1000)]
		public virtual string msm_descricao { get; set; }
		[MSValidRange(500)]
		public virtual string msm_url { get; set; }
        public virtual string msm_informacoes { get; set; }
        [MSValidRange(500)]
        public virtual string msm_urlHelp { get; set; }
    }
}