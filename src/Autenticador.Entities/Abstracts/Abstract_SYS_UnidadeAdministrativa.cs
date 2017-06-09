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
    public abstract class Abstract_SYS_UnidadeAdministrativa : Abstract_Entity
    {
		[MSNotNullOrEmpty()]
		[DataObjectField(true, false, false)]
		public virtual Guid ent_id { get; set; }
		[MSNotNullOrEmpty()]
		[DataObjectField(true, false, false)]
		public virtual Guid uad_id { get; set; }
		[MSNotNullOrEmpty()]
		public virtual Guid tua_id { get; set; }
		[MSValidRange(20)]
		public virtual string uad_codigo { get; set; }
		[MSValidRange(200)]
		[MSNotNullOrEmpty()]
		public virtual string uad_nome { get; set; }
		[MSValidRange(50)]
		public virtual string uad_sigla { get; set; }
		public virtual Guid uad_idSuperior { get; set; }
		[MSNotNullOrEmpty()]
		public virtual byte uad_situacao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual DateTime uad_dataCriacao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual DateTime uad_dataAlteracao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual int uad_integridade { get; set; }

        public virtual string uad_codigoIntegracao { get; set; }

        public virtual string uad_codigoInep { get; set; }
    }
}