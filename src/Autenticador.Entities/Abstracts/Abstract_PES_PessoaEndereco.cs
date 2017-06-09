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
    public abstract class Abstract_PES_PessoaEndereco : Abstract_Entity
    {
		[MSNotNullOrEmpty()]
		[DataObjectField(true, false, false)]
		public virtual Guid pes_id { get; set; }
		[MSNotNullOrEmpty()]
		[DataObjectField(true, false, false)]
		public virtual Guid pse_id { get; set; }
		[MSNotNullOrEmpty()]
		public virtual Guid end_id { get; set; }
		[MSValidRange(20)]
		[MSNotNullOrEmpty()]
		public virtual string pse_numero { get; set; }
		[MSValidRange(100)]
		public virtual string pse_complemento { get; set; }
		[MSNotNullOrEmpty()]
		public virtual byte pse_situacao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual DateTime pse_dataCriacao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual DateTime pse_dataAlteracao { get; set; }
        public virtual bool pse_enderecoPrincipal { get; set; }
        public virtual decimal pse_latitude { get; set; }
        public virtual decimal pse_longitude { get; set; }

    }
}