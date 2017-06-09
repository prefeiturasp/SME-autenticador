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
    public abstract class Abstract_SYS_Entidade : Abstract_Entity
    {
		[MSNotNullOrEmpty()]
		[DataObjectField(true, false, false)]
		public virtual Guid ent_id { get; set; }
		[MSNotNullOrEmpty()]
		public virtual Guid ten_id { get; set; }
		[MSValidRange(20)]
		public virtual string ent_codigo { get; set; }
		[MSValidRange(200)]
		public virtual string ent_nomeFantasia { get; set; }
		[MSValidRange(200)]
		public virtual string ent_razaoSocial { get; set; }
		[MSValidRange(50)]
		public virtual string ent_sigla { get; set; }
		[MSValidRange(14)]
		public virtual string ent_cnpj { get; set; }
		[MSValidRange(20)]
		public virtual string ent_inscricaoMunicipal { get; set; }
		[MSValidRange(20)]
		public virtual string ent_inscricaoEstadual { get; set; }
		public virtual Guid ent_idSuperior { get; set; }
		[MSNotNullOrEmpty()]
		public virtual byte ent_situacao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual DateTime ent_dataCriacao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual DateTime ent_dataAlteracao { get; set; }
		[MSNotNullOrEmpty()]
		public virtual int ent_integridade { get; set; }


        /// <summary>
        /// Url de acesso da entidade.
        /// </summary>
        [MSValidRange(200)]
        public virtual string ent_urlAcesso { get; set; }

        /// <summary>
        /// Flag que indica se o logo da entidade serpa exibida.
        /// </summary>
        [MSNotNullOrEmpty]
        public virtual bool ent_exibeLogoCliente { get; set; }

        /// <summary>
        /// Logo da entidade.
        /// </summary>
        [MSValidRange(2000)]
        public virtual string ent_logoCliente { get; set; }

        /// <summary>
        /// Tema padr�o da entidade.
        /// </summary>
        public virtual int tep_id { get; set; }

        /// <summary>
        /// ID do tema de cores.
        /// </summary>
        public virtual int tpl_id { get; set; }
    }
}