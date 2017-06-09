/*
	Classe gerada automaticamente pelo Code Creator
*/

namespace Autenticador.Entities.Abstracts
{
	using System;
    using System.ComponentModel;
	using CoreLibrary.Data.Common.Abstracts;
	using CoreLibrary.Validation;
	
	/// <summary>
	/// Description: .
	/// </summary>
	[Serializable]
    public abstract class Abstract_CFG_TemaPaleta : Abstract_Entity
    {
		
		/// <summary>
		/// ID do tema padrão.
		/// </summary>
		[MSNotNullOrEmpty]
		[DataObjectField(true, false, false)]
		public virtual int tep_id { get; set; }

		/// <summary>
		/// ID do tema de paleta de cores.
		/// </summary>
		[MSNotNullOrEmpty]
		[DataObjectField(true, false, false)]
		public virtual int tpl_id { get; set; }

		/// <summary>
		/// Nome do tema de paleta de cores.
		/// </summary>
		[MSValidRange(100)]
		[MSNotNullOrEmpty]
		public virtual string tpl_nome { get; set; }

		/// <summary>
		/// Caminho dos arquivos CSS.
		/// </summary>
		[MSNotNullOrEmpty]
		public virtual string tpl_caminhoCSS { get; set; }

		/// <summary>
		/// Imagem de exemplo do tema de paleta de cores.
		/// </summary>
		[MSValidRange(2000)]
		public virtual string tpl_imagemExemploTema { get; set; }

		/// <summary>
		/// Situacaoção do registro 1 - Ativo, 3 - Excluído.
		/// </summary>
		[MSNotNullOrEmpty]
		public virtual byte tpl_situacao { get; set; }

		/// <summary>
		/// Data de criação do registro.
		/// </summary>
		[MSNotNullOrEmpty]
		public virtual DateTime tpl_dataCriacao { get; set; }

		/// <summary>
		/// Data de alteração do registro.
		/// </summary>
		[MSNotNullOrEmpty]
		public virtual DateTime tpl_dataAlteracao { get; set; }

    }
}