/*
	Classe gerada automaticamente pelo Code Creator
*/

using System;
using System.ComponentModel;
using Autenticador.Entities.Abstracts;
using CoreLibrary.Validation;

namespace Autenticador.Entities
{
	
	/// <summary>
	/// 
	/// </summary>
    [Serializable]
	public class SYS_UsuarioFalhaAutenticacao : Abstract_SYS_UsuarioFalhaAutenticacao
	{
        /// <summary>
        /// ID do usu�rio que tentou a autentica��o.
        /// </summary>
        [MSNotNullOrEmpty("Usu�rio � obrigat�rio.")]
        [DataObjectField(true, false, false)]
        public override Guid usu_id { get; set; }

        /// <summary>
        /// Quantidade de falhas de autentica��o do usu�rio.
        /// </summary>
        [MSNotNullOrEmpty("Quantidade de falhas � obrigat�rio.")]
        public override int ufl_qtdeFalhas { get; set; }

        /// <summary>
        /// Data e hora da �ltima tentativa de login do usu�rio com falha.
        /// </summary>
        [MSNotNullOrEmpty("Data da �ltima tentativa � obrigat�rio.")]
        public override DateTime ufl_dataUltimaTentativa { get; set; }
	}
}