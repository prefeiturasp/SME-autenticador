/*
	Classe gerada automaticamente pelo Code Creator
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autenticador.Entities.Abstracts;
using CoreLibrary.Validation;
using System.ComponentModel;

namespace Autenticador.Entities
{
	
	/// <summary>
	/// 
	/// </summary>
    [Serializable]
	public class SYS_MensagemSistema : Abstract_SYS_MensagemSistema
	{
        /// <summary>
        /// ID do par�metro de mensagem.
        /// </summary>
        [DataObjectField(true, true, false)]
        public override int mss_id { get; set; }

        /// <summary>
        /// Chave do par�metro de mensagem.
        /// </summary>
        [MSValidRange(100, "Chave pode conter at� 100 caracteres.")]
        [MSNotNullOrEmpty("Chave � obrigat�rio.")]
        public override string mss_chave { get; set; }

        /// <summary>
        /// Valor da mensagem.
        /// </summary>
        [MSNotNullOrEmpty("Valor � obrigat�rio.")]
        public override string mss_valor { get; set; }

        /// <summary>
        /// Descri��o da utiliza��o da mensagem.
        /// </summary>
        [MSValidRange(200, "Descri��o pode conter at� 200 caracteres.")]
        public override string mss_descricao { get; set; }

        /// <summary>
        /// Situa��o do registro. 1-Ativo,3-Excluido,4-Inativo
        /// </summary>
        [MSDefaultValue(1)]
        public override byte mss_situacao { get; set; }

        /// <summary>
        /// Data de cria��o do registro.
        /// </summary>
        public override DateTime mss_dataCriacao { get; set; }

        /// <summary>
        /// Data de altera��o do registro.
        /// </summary>
        public override DateTime mss_dataAlteracao { get; set; }

	}
}