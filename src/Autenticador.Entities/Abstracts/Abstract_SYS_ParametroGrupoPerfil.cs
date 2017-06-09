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
    public abstract class Abstract_SYS_ParametroGrupoPerfil : Abstract_Entity
    {
		[MSNotNullOrEmpty()]
		[DataObjectField(true, false, false)]
		public virtual Guid pgs_id { get; set; }
		[MSValidRange(100)]
		[MSNotNullOrEmpty()]
		public virtual string pgs_chave { get; set; }
		[MSNotNullOrEmpty()]
		public virtual Guid gru_id { get; set; }
		[MSNotNullOrEmpty()]
		public virtual byte pgs_situacao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual DateTime pgs_dataCriacao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual DateTime pgs_dataAlteracao { get; set; }

    }
}