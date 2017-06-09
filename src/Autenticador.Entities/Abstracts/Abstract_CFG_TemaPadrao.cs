/*
	Classe gerada automaticamente pelo Code Creator
*/

namespace Autenticador.Entities.Abstracts
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.ComponentModel;
	using CoreLibrary.Data.Common.Abstracts;
	using CoreLibrary.Validation;
	
	/// <summary>
	/// Description: .
	/// </summary>
	[Serializable]
    public abstract class Abstract_CFG_TemaPadrao : Abstract_Entity
    {
		
		/// <summary>
		/// ID do tema padrão.
		/// </summary>
		[MSNotNullOrEmpty]
		[DataObjectField(true, true, false)]
		public virtual int tep_id { get; set; }

		/// <summary>
		/// Nome do tema padrão.
		/// </summary>
		[MSValidRange(100)]
		[MSNotNullOrEmpty]
		public virtual string tep_nome { get; set; }

        /// <summary>
        /// Descricação do tema.
        /// </summary>
        [MSValidRange(200)]
        public virtual string tep_descricao { get; set; }

		/// <summary>
		/// Tipo de menu utilizado pelo tema.
		/// </summary>
		[MSNotNullOrEmpty]
		public virtual short tep_tipoMenu { get; set; }

		/// <summary>
		/// Flag que indica se exibirá link para o site da empresa.
		/// </summary>
		[MSNotNullOrEmpty]
		public virtual bool tep_exibeLinkLogin { get; set; }

		/// <summary>
		/// Tipo utilizado para indicar que tipo de tratamento javascript será realizado na tela de login (1 - sem tratamento, 2 - sobrescreve labels de usuário e senha, 3 - Redimensiona componentes da tela de login.
		/// </summary>
		[MSNotNullOrEmpty]
		public virtual short tep_tipoLogin { get; set; }

        /// <summary>
        /// Indica se o logo será exibido na tela de login..
        /// </summary>
        [MSNotNullOrEmpty]
        public virtual bool tep_exibeLogoCliente { get; set; }

		/// <summary>
		/// Situação do registro 1-Ativo, 3-Excluido.
		/// </summary>
		[MSNotNullOrEmpty]
		public virtual short tep_situacao { get; set; }

		/// <summary>
		/// Data de criação do registro.
		/// </summary>
		[MSNotNullOrEmpty]
		public virtual DateTime tep_dataCriacao { get; set; }

		/// <summary>
		/// Data de alteração do registro.
		/// </summary>
		[MSNotNullOrEmpty]
		public virtual DateTime tep_dataAlteracao { get; set; }

    }
}