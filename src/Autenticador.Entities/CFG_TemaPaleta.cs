/*
	Classe gerada automaticamente pelo Code Creator
*/

namespace Autenticador.Entities
{
    using System;
    using System.ComponentModel;
    using Autenticador.Entities.Abstracts;
    using CoreLibrary.Validation;
		
	/// <summary>
	/// Description: .
	/// </summary>
    [Serializable]
	public class CFG_TemaPaleta : Abstract_CFG_TemaPaleta
    {
        #region Enumeradores

        /// <summary>
        /// Enumerador da situa��o do registro.
        /// </summary>
        public enum eSituacao
        {
            Ativo = 1
            ,
            Excluido = 3
        }

        #endregion Enumeradores

        #region Propriedades

        /// <summary>
        /// ID do tema padr�o.
        /// </summary>
        [MSNotNullOrEmpty("Tema padr�o � obrigat�rio.")]
        [DataObjectField(true, false, false)]
        public override int tep_id { get; set; }

        /// <summary>
        /// ID do tema de paleta de cores.
        /// </summary>
        [DataObjectField(true, false, false)]
        public override int tpl_id { get; set; }

        /// <summary>
        /// Nome do tema de paleta de cores.
        /// </summary>
        [MSValidRange(100, "Nome do tema de paleta de cores deve possuir at� 100 caracteres.")]
        [MSNotNullOrEmpty("Nome do tema de paleta de cores � obrigat�rio.")]
        public override string tpl_nome { get; set; }

        /// <summary>
        /// Caminho dos arquivos CSS.
        /// </summary>
        [MSNotNullOrEmpty("Caminho dos arquivos CSS")]
        public override string tpl_caminhoCSS { get; set; }

        /// <summary>
        /// Imagem de exemplo do tema de paleta de cores.
        /// </summary>
        [MSValidRange(2000, "Imagem de exemplo do tema de paleta de cores deve possuir at� 2000 caracteres.")]
        public override string tpl_imagemExemploTema { get; set; }

        /// <summary>
        /// Situacao��o do registro 1 - Ativo, 3 - Exclu�do.
        /// </summary>
        [MSDefaultValue(1)]
        public override byte tpl_situacao { get; set; }

        /// <summary>
        /// Data de cria��o do registro.
        /// </summary>
        public override DateTime tpl_dataCriacao { get; set; }

        /// <summary>
        /// Data de altera��o do registro.
        /// </summary>
        public override DateTime tpl_dataAlteracao { get; set; }

        /// <summary>
        /// Nome do tema padr�o (chave estrangeira)
        /// </summary>
        public string tep_nome { get; set; }

        #endregion Propriedades
    }
}