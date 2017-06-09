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
    public abstract class Abstract_SYS_EntidadeEndereco : Abstract_Entity
    {
		[MSNotNullOrEmpty()]
		[DataObjectField(true, false, false)]
		public virtual Guid ent_id { get; set; }
		[MSNotNullOrEmpty()]
		[DataObjectField(true, false, false)]
		public virtual Guid ene_id { get; set; }
		[MSNotNullOrEmpty()]
		public virtual Guid end_id { get; set; }
		[MSValidRange(20)]
		[MSNotNullOrEmpty()]
		public virtual string ene_numero { get; set; }
		[MSValidRange(100)]
		public virtual string ene_complemento { get; set; }
		[MSNotNullOrEmpty()]
		public virtual byte ene_situacao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual DateTime ene_dataCriacao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual DateTime ene_dataAlteracao { get; set; }
       
        public virtual bool ene_enderecoPrincipal { get; set; }
        public virtual decimal ene_latitude { get; set; }
        public virtual decimal ene_longitude { get; set; }

    }
}