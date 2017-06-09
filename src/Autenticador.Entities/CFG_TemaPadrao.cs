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
    public class CFG_TemaPadrao : Abstract_CFG_TemaPadrao
    {
        #region Enumeradores

        /// <summary>
        /// Tipo de menu utilizado pelo tema.
        /// </summary>
        public enum eTipoMenu
        {
            [Description("Menu")]
            Menu = 1
            ,
            [Description("CoreUI Menu")]
            CoreUI_Menu = 2
        }

        /// <summary>
        /// Tipo de tratamento/corre��o javascript na tela de login.
        /// </summary>
        public enum eTipoLogin
        {
            [Description("Sem tratamento")]
            SemTratamento = 1
            ,
            [Description("Sobrescreve label de usu�rio e senha")]
            SobrescreveLabel = 2
            ,
            [Description("Corrige layout")]
            CorrigeLayout = 3
        }

        /// <summary>
        /// Situa��o do tema.
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
        [DataObjectField(true, true, false)]
        public override int tep_id { get; set; }

        /// <summary>
        /// Nome do tema padr�o.
        /// </summary>
        [MSValidRange(100, "Nome do tema padr�o deve possuir at� 100 caracteres.")]
        [MSNotNullOrEmpty("Nome do tema padr�o � obrigat�rio.")]
        public override string tep_nome { get; set; }

        /// <summary>
        /// Descrica��o do tema.
        /// </summary>
        [MSValidRange(200, "Descri��o do tema padr�o deve possuir at� 200 caracteres.")]
        public override string tep_descricao { get; set; }

        /// <summary>
        /// Tipo de menu utilizado pelo tema.
        /// </summary>
        [MSNotNullOrEmpty("Tipo de menu � obrigat�rio.")]
        public override short tep_tipoMenu { get; set; }

        /// <summary>
        /// Flag que indica se exibir� link para o site da empresa.
        /// </summary>
        [MSDefaultValue(false)]
        public override bool tep_exibeLinkLogin { get; set; }

        /// <summary>
        /// Tipo utilizado para indicar que tipo de tratamento javascript ser� realizado na tela de login (1 - sem tratamento, 2 - sobrescreve labels de usu�rio e senha, 3 - Redimensiona componentes da tela de login.
        /// </summary>
        [MSNotNullOrEmpty("Tipo de login � obrigat�rio.")]
        public override short tep_tipoLogin { get; set; }

        /// <summary>
        /// Indica se o logo ser� exibido na tela de login..
        /// </summary>
        [MSDefaultValue(false)]
        public override bool tep_exibeLogoCliente { get; set; }

        /// <summary>
        /// Situa��o do registro 1-Ativo, 3-Excluido.
        /// </summary>
        [MSDefaultValue(1)]
        public override short tep_situacao { get; set; }

        /// <summary>
        /// Data de cria��o do registro.
        /// </summary>
        public override DateTime tep_dataCriacao { get; set; }

        /// <summary>
        /// Data de altera��o do registro.
        /// </summary>
        public override DateTime tep_dataAlteracao { get; set; }

        #endregion Propriedades
    }
}