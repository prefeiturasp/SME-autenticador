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
    public abstract class Abstract_END_Endereco : Abstract_Entity
    {
		[MSNotNullOrEmpty()]
		[DataObjectField(true, false, false)]
		public virtual Guid end_id { get; set; }
		[MSValidRange(8)]
		[MSNotNullOrEmpty()]
		public virtual string end_cep { get; set; }
		[MSValidRange(200)]
		[MSNotNullOrEmpty()]
		public virtual string end_logradouro { get; set; }
		[MSValidRange(100)]
		public virtual string end_bairro { get; set; }
		[MSValidRange(100)]
		public virtual string end_distrito { get; set; }
		public virtual byte end_zona { get; set; }
		[MSNotNullOrEmpty()]
		public virtual Guid cid_id { get; set; }
		[MSNotNullOrEmpty()]
		public virtual byte end_situacao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual DateTime end_dataCriacao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual DateTime end_dataAlteracao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual int end_integridade { get; set; }

    }
}