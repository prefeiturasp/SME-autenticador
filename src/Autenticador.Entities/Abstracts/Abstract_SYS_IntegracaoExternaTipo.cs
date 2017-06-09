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
    public abstract class Abstract_SYS_IntegracaoExternaTipo : Abstract_Entity
    {
		
		/// <summary>
		/// Propriedade iet_id.
		/// </summary>
		[MSNotNullOrEmpty]
		[DataObjectField(true, false, false)]
		public virtual short iet_id { get; set; }

		/// <summary>
		/// Propriedade iet_descricao.
		/// </summary>
		[MSValidRange(128)]
		[MSNotNullOrEmpty]
		public virtual string iet_descricao { get; set; }

        /// <summary>
        /// N�mero de dias para que seja efetuada a nova autentica��o do usu�rio.
        /// </summary>
        [MSNotNullOrEmpty]
        public virtual int iet_qtdeDiasAutenticacao { get; set; }

    }
}