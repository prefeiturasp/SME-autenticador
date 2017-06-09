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
	/// Description: .
	/// </summary>
	[Serializable]
    public abstract class Abstract_SYS_LoginProviderDominioPermitido : Abstract_Entity
    {
		
		/// <summary>
		/// Propriedade ent_id.
		/// </summary>
		[MSNotNullOrEmpty]
		[DataObjectField(true, false, false)]
		public virtual Guid ent_id { get; set; }

		/// <summary>
		/// Propriedade LoginProvider.
		/// </summary>
		[MSValidRange(128)]
		[MSNotNullOrEmpty]
		[DataObjectField(true, false, false)]
		public virtual string LoginProvider { get; set; }

		/// <summary>
		/// Propriedade Dominios.
		/// </summary>
		[MSValidRange(1024)]
		public virtual string Dominios { get; set; }


        /// <summary>
		/// Propriedade Tenant.
		/// </summary>
		[MSValidRange(1024)]
        public virtual string Tenant { get; set; }

    }
}