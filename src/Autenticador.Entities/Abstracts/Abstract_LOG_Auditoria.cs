/*
	Classe gerada automaticamente pelo Code Creator
*/

using CoreLibrary.Data.Common.Abstracts;
using CoreLibrary.Validation;
using System;

namespace Autenticador.Entities.Abstracts
{
    /// <summary>
    /// </summary>
    [Serializable()]
    public abstract class Abstract_LOG_Auditoria : Abstract_Entity
    {
        public virtual Guid log_id { get; set; }
        public virtual int aud_id { get; set; }

        [MSNotNullOrEmpty()]
        public virtual DateTime aud_dataHora { get; set; }

        [MSValidRange(100)]
        [MSNotNullOrEmpty()]
        public virtual string aud_entidade { get; set; }

        [MSValidRange(50)]
        [MSNotNullOrEmpty()]
        public virtual string aud_operacao { get; set; }

        public virtual string aud_entidadeOriginal { get; set; }
        public virtual string aud_entidadeNova { get; set; }
    }
}