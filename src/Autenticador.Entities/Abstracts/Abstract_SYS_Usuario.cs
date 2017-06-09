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
    public abstract class Abstract_SYS_Usuario : Abstract_Entity
    {
		[MSNotNullOrEmpty()]
		[DataObjectField(true, false, false)]
		public virtual Guid usu_id { get; set; }
		[MSValidRange(500)]
		public virtual string usu_login { get; set; }
		[MSValidRange(100)]
		public virtual string usu_dominio { get; set; }
		[MSValidRange(500)]
		public virtual string usu_email { get; set; }
		[MSValidRange(256)]
		public virtual string usu_senha { get; set; }
		public virtual byte usu_criptografia { get; set; }
		[MSNotNullOrEmpty()]
		public virtual byte usu_situacao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual DateTime usu_dataCriacao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual DateTime usu_dataAlteracao { get; set; }
        [MSNotNullOrEmpty()]
        public virtual DateTime usu_dataAlteracaoSenha { get; set; }
        public virtual Guid pes_id { get; set; }
		[MSNotNullOrEmpty()]
		public virtual int usu_integridade { get; set; }
		[MSNotNullOrEmpty()]
		public virtual Guid ent_id { get; set; }

        /// <summary>
		/// 0 - Usu�rio n�o integrado; 1 - Usu�rio integrado com AD; 2 - Usu�rio integrado com AD/Replica��o de senha.
		/// </summary>
        [MSNotNullOrEmpty()]
        public virtual byte usu_integracaoAD { get; set; }

        /// <summary>
        /// Se campo for <> que Null, ser� o c�digo do tipo integra��o.
        /// </summary>
        public virtual short usu_integracaoExterna { get; set; }
    }
}