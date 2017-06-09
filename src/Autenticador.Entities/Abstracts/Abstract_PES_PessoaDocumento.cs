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
    public abstract class Abstract_PES_PessoaDocumento : Abstract_Entity
    {
		[MSNotNullOrEmpty()]
		[DataObjectField(true, false, false)]
		public virtual Guid pes_id { get; set; }
		[MSNotNullOrEmpty()]
		[DataObjectField(true, false, false)]
		public virtual Guid tdo_id { get; set; }
		[MSValidRange(50)]
		[MSNotNullOrEmpty()]
		public virtual string psd_numero { get; set; }
		public virtual DateTime psd_dataEmissao { get; set; }
		[MSValidRange(200)]
		public virtual string psd_orgaoEmissao { get; set; }
		public virtual Guid unf_idEmissao { get; set; }
		[MSValidRange(1000)]
		public virtual string psd_infoComplementares { get; set; }
		[MSNotNullOrEmpty()]
		public virtual byte psd_situacao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual DateTime psd_dataCriacao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual DateTime psd_dataAlteracao { get; set; }

        //NOVOS CAMPOS
        /// <summary>
		/// Propriedade psd_categoria.
		/// </summary>
		[MSValidRange(64)]
        public virtual string psd_categoria { get; set; }

        /// <summary>
        /// Propriedade psd_classificacao.
        /// </summary>
        [MSValidRange(64)]
        public virtual string psd_classificacao { get; set; }

        /// <summary>
        /// Propriedade psd_csm.
        /// </summary>
        [MSValidRange(32)]
        public virtual string psd_csm { get; set; }

        /// <summary>
        /// Propriedade psd_dataEntrada.
        /// </summary>
        public virtual DateTime psd_dataEntrada { get; set; }

        /// <summary>
        /// Propriedade psd_dataValidade.
        /// </summary>
        public virtual DateTime psd_dataValidade { get; set; }

        /// <summary>
        /// Propriedade pai_idOrigem.
        /// </summary>
        public virtual Guid pai_idOrigem { get; set; }

        /// <summary>
        /// Propriedade psd_serie.
        /// </summary>
        [MSValidRange(32)]
        public virtual string psd_serie { get; set; }

        /// <summary>
        /// Propriedade psd_tipoGuarda.
        /// </summary>
        [MSValidRange(128)]
        public virtual string psd_tipoGuarda { get; set; }

        /// <summary>
        /// Propriedade psd_via.
        /// </summary>
        [MSValidRange(16)]
        public virtual string psd_via { get; set; }

        /// <summary>
        /// Propriedade psd_secao.
        /// </summary>
        [MSValidRange(32)]
        public virtual string psd_secao { get; set; }

        /// <summary>
        /// Propriedade psd_zona.
        /// </summary>
        [MSValidRange(16)]
        public virtual string psd_zona { get; set; }

        /// <summary>
        /// Propriedade psd_regiaoMilitar.
        /// </summary>
        [MSValidRange(16)]
        public virtual string psd_regiaoMilitar { get; set; }

        /// <summary>
        /// Propriedade psd_numeroRA.
        /// </summary>
        [MSValidRange(64)]
        public virtual string psd_numeroRA { get; set; }

        /// <summary>
        /// Propriedade psd_dataExpedicao.
        /// </summary>
        public virtual DateTime psd_dataExpedicao { get; set; }

    }
}