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
	public class LOG_UsuarioAPI : Abstract_LOG_UsuarioAPI
	{
        /// <summary>
        /// Propriedade lua_id.
        /// </summary>
        [DataObjectField(true, true, false)]
        public override long lua_id { get; set; }

        /// <summary>
        /// Propriedade usu_id.
        /// </summary>
        [MSNotNullOrEmpty("Usu�rio � obrigat�rio.")]
        public override Guid usu_id { get; set; }

        /// <summary>
        /// Propriedade uap_id.
        /// </summary>
        [MSNotNullOrEmpty("Usu�rio da API � obrigat�rio.")]
        public override int uap_id { get; set; }

        /// <summary>
        /// Propriedade lua_acao.
        /// </summary>
        [MSNotNullOrEmpty("A��o � obrigat�rio.")]
        public override byte lua_acao { get; set; }

        /// <summary>
        /// Propriedade lua_dataHora.
        /// </summary>
        [MSNotNullOrEmpty("Data/hora � obrigat�rio.")]
        public override DateTime lua_dataHora { get; set; }
	}
}