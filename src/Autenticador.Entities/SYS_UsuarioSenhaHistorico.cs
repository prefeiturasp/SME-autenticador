/*
	Classe gerada automaticamente pelo Code Creator
*/

namespace Autenticador.Entities
{
    using Autenticador.Entities.Abstracts;
    using CoreLibrary.Validation;
    using System;
    using System.ComponentModel;
		
	/// <summary>
	/// Description: .
	/// </summary>
    [Serializable]
	public class SYS_UsuarioSenhaHistorico : Abstract_SYS_UsuarioSenhaHistorico
	{
        /// <summary>
        /// ID do usu�rio.
        /// </summary>
        [MSNotNullOrEmpty("Usu�rio � obrigat�rio.")]
        [DataObjectField(true, false, false)]
        public override Guid usu_id { get; set; }

        /// <summary>
        /// Senha do usu�rio.
        /// </summary>
        [MSNotNullOrEmpty("Senha � obrigat�rio")]
        public override string ush_senha { get; set; }

        /// <summary>
        /// Tipo de criptografia da senha.
        /// </summary>
        [MSDefaultValue(1)]
        public override short ush_criptografia { get; set; }

        /// <summary>
        /// ID do hist�rico de senha.
        /// </summary>
        [DataObjectField(true, false, false)]
        public override Guid ush_id { get; set; }

        /// <summary>
        /// Data do hist�rico de senha.
        /// </summary>
        public override DateTime ush_data { get; set; }
	}
}