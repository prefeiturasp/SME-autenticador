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
    public abstract class Abstract_PES_CertidaoCivil : Abstract_Entity
    {
		[MSNotNullOrEmpty()]
		[DataObjectField(true, false, false)]
		public virtual Guid pes_id { get; set; }
		[MSNotNullOrEmpty()]
		[DataObjectField(true, false, false)]
		public virtual Guid ctc_id { get; set; }
		[MSNotNullOrEmpty()]
		public virtual byte ctc_tipo { get; set; }
		[MSValidRange(50)]
		public virtual string ctc_numeroTermo { get; set; }
		[MSValidRange(20)]
		public virtual string ctc_folha { get; set; }
		[MSValidRange(20)]
		public virtual string ctc_livro { get; set; }
		public virtual DateTime ctc_dataEmissao { get; set; }
		[MSValidRange(200)]
		public virtual string ctc_nomeCartorio { get; set; }
        public virtual Guid cid_idCartorio { get; set; }
		public virtual Guid unf_idCartorio { get; set; }
        [MSValidRange(100)]
        public virtual string ctc_distritoCartorio { get; set; }
		[MSNotNullOrEmpty()]
		public virtual byte ctc_situacao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual DateTime ctc_dataCriacao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual DateTime ctc_dataAlteracao { get; set; }
        [MSValidRange(32)]
        public virtual string ctc_matricula { get; set; }

        /// <summary>
        /// Indica se a pessoa possui irm�o g�meo.
        /// </summary>
        [MSNotNullOrEmpty]
        public virtual bool ctc_gemeo { get; set; }

        /// <summary>
        /// Indica se a certid�o � do modelo novo.
        /// </summary>
        [MSNotNullOrEmpty]
        public virtual bool ctc_modeloNovo { get; set; }

    }
}