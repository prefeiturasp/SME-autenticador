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
    public abstract class Abstract_SYS_DiaNaoUtil : Abstract_Entity
    {
        [MSNotNullOrEmpty()]
        [DataObjectField(true, false, false)]
        public virtual Guid dnu_id { get; set; }
        [MSValidRange(100)]
        [MSNotNullOrEmpty()]
        public virtual string dnu_nome { get; set; }
        [MSNotNullOrEmpty()]
        public virtual byte dnu_abrangencia { get; set; }
        [MSValidRange(400)]
        public virtual string dnu_descricao { get; set; }
        [MSNotNullOrEmpty()]
        public virtual DateTime dnu_data { get; set; }
        public virtual bool dnu_recorrencia { get; set; }
        [MSNotNullOrEmpty()]
        public virtual DateTime dnu_vigenciaInicio { get; set; }
        public virtual DateTime dnu_vigenciaFim { get; set; }
        public virtual Guid cid_id { get; set; }
        public virtual Guid unf_id { get; set; }
        [MSNotNullOrEmpty()]
        public virtual byte dnu_situacao { get; set; }
        [MSNotNullOrEmpty()]
        public virtual DateTime dnu_dataCriacao { get; set; }
        [MSNotNullOrEmpty()]
        public virtual DateTime dnu_dataAlteracao { get; set; }

    }
}