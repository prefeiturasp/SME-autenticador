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
    public abstract class Abstract_SYS_UnidadeAdministrativaEndereco : Abstract_Entity
    {

        /// <summary>
        /// Propriedade ent_id.
        /// </summary>
        [MSNotNullOrEmpty]
        [DataObjectField(true, false, false)]
        public virtual Guid ent_id { get; set; }

        /// <summary>
        /// Propriedade uad_id.
        /// </summary>
        [MSNotNullOrEmpty]
        [DataObjectField(true, false, false)]
        public virtual Guid uad_id { get; set; }

        /// <summary>
        /// Propriedade uae_id.
        /// </summary>
        [MSNotNullOrEmpty]
        [DataObjectField(true, false, false)]
        public virtual Guid uae_id { get; set; }

        /// <summary>
        /// Propriedade end_id.
        /// </summary>
        [MSNotNullOrEmpty]
        public virtual Guid end_id { get; set; }

        /// <summary>
        /// Propriedade uae_numero.
        /// </summary>
        [MSValidRange(20)]
        [MSNotNullOrEmpty]
        public virtual string uae_numero { get; set; }

        /// <summary>
        /// Propriedade uae_complemento.
        /// </summary>
        [MSValidRange(100)]
        public virtual string uae_complemento { get; set; }

        /// <summary>
        /// Propriedade uae_situacao.
        /// </summary>
        [MSNotNullOrEmpty]
        public virtual byte uae_situacao { get; set; }

        /// <summary>
        /// Propriedade uae_dataCriacao.
        /// </summary>
        [MSNotNullOrEmpty]
        public virtual DateTime uae_dataCriacao { get; set; }

        /// <summary>
        /// Propriedade uae_dataAlteracao.
        /// </summary>
        [MSNotNullOrEmpty]
        public virtual DateTime uae_dataAlteracao { get; set; }

        /// <summary>
        /// Indica se o endere�o � o principal na unidade.
        /// </summary>
        public virtual bool uae_enderecoPrincipal { get; set; }

        /// <summary>
        /// Latitude do endere�o.
        /// </summary>
        public virtual decimal uae_latitude { get; set; }

        /// <summary>
        /// Longitude do endere�o.
        /// </summary>
        public virtual decimal uae_longitude { get; set; }

    }
}