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
	public class CFG_UsuarioAPI : Abstract_CFG_UsuarioAPI
	{
        /// <summary>
        /// Propriedade uap_id.
        /// </summary>
        [DataObjectField(true, true, false)]
        public override int uap_id { get; set; }

        /// <summary>
        /// Propriedade uap_username.
        /// </summary>
        [MSValidRange(100, "Nome do usu�rio API deve possuir at� 100 caracteres.")]
        [MSNotNullOrEmpty("Nome do usu�rio API � obrigat�rio.")]
        public override string uap_username { get; set; }

        /// <summary>
        /// Propriedade uap_password.
        /// </summary>
        public override string uap_password { get; set; }

        /// <summary>
        /// Propriedade uap_situacao.
        /// </summary>
        [MSDefaultValue(1)]
        public override byte uap_situacao { get; set; }

        /// <summary>
        /// Propriedade uap_dataCriacao.
        /// </summary>
        public override DateTime uap_dataCriacao { get; set; }

        /// <summary>
        /// Propriedade uap_dataAlteracao.
        /// </summary>
        public override DateTime uap_dataAlteracao { get; set; }

        #region Enumerador

        public enum eSituacao
        {
            Ativo = 1
            ,
            Inativo = 2
            ,
            Excluido = 3
        }

        #endregion
    }
}