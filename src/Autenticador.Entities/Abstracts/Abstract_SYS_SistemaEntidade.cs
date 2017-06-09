/*
	Classe gerada automaticamente pelo Code Creator
*/

using System;
using System.ComponentModel;
using CoreLibrary.Data.Common.Abstracts;
using CoreLibrary.Validation;

namespace Autenticador.Entities.Abstracts
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable()]
    public abstract class Abstract_SYS_SistemaEntidade : Abstract_Entity
    {
        [MSNotNullOrEmpty()]
        [DataObjectField(true, false, false)]
        public virtual int sis_id { get; set; }
        [MSNotNullOrEmpty()]
        [DataObjectField(true, false, false)]
        public virtual Guid ent_id { get; set; }
        [MSValidRange(100)]
        public virtual string sen_chaveK1 { get; set; }
        [MSValidRange(200)]
        public virtual string sen_urlAcesso { get; set; }
        [MSValidRange(2000)]
        public virtual string sen_logoCliente { get; set; }
        [MSValidRange(1000)]
        public virtual string sen_urlCliente { get; set; }
        [MSNotNullOrEmpty()]
        public virtual byte sen_situacao { get; set; }
    }
}