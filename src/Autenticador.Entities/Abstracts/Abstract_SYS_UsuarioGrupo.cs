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
    public abstract class Abstract_SYS_UsuarioGrupo : Abstract_Entity
    {
		[MSNotNullOrEmpty()]
		[DataObjectField(true, false, false)]
		public virtual Guid usu_id { get; set; }
		[MSNotNullOrEmpty()]
		[DataObjectField(true, false, false)]
		public virtual Guid gru_id { get; set; }
		[MSNotNullOrEmpty()]
		public virtual byte usg_situacao { get; set; }

    }
}