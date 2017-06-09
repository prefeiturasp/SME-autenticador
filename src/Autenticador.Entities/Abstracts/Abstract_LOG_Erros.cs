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
    public abstract class Abstract_LOG_Erros : Abstract_Entity
    {
        public virtual Guid err_id { get; set; }
        [MSNotNullOrEmpty()]
        public virtual DateTime err_dataHora { get; set; }
        [MSValidRange(15)]
        [MSNotNullOrEmpty()]
        public virtual string err_ip { get; set; }
        [MSValidRange(256)]
        [MSNotNullOrEmpty()]
        public virtual string err_machineName { get; set; }
        [MSValidRange(256)]
        public virtual string err_browser { get; set; }
        [MSValidRange(2000)]
        public virtual string err_caminhoArq { get; set; }
        [MSNotNullOrEmpty()]
        public virtual string err_descricao { get; set; }
        public virtual string err_erroBase { get; set; }
        [MSValidRange(1000)]
        public virtual string err_tipoErro { get; set; }
        public virtual int sis_id { get; set; }
        [MSValidRange(100)]
        public virtual string sis_decricao { get; set; }
        public virtual Guid usu_id { get; set; }
        [MSValidRange(100)]
        public virtual string usu_login { get; set; }
    }
}