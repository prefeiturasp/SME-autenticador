/*
	Classe gerada automaticamente pelo Code Creator
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autenticador.Entities.Abstracts;
using CoreLibrary.Validation;

namespace Autenticador.Entities
{
	
	/// <summary>
	/// 
	/// </summary>
    [Serializable]
	public class LOG_Sistema : Abstract_LOG_Sistema
    {
        [MSNotNullOrEmpty("Log_id � obrigat�rio.")]
        public override Guid log_id { get; set; }
        [MSNotNullOrEmpty("Data/hora � obrigat�rio.")]
        public override DateTime log_dataHora { get; set; }
        [MSValidRange(15, "IP pode conter at� 15 caracteres.")]
        [MSNotNullOrEmpty("IP � obrigat�rio.")]
        public override string log_ip { get; set; }
        [MSValidRange(256, "Nome da m�quina pode conter at� 256 caracteres.")]
        [MSNotNullOrEmpty("Nome da m�qui�na � obrigat�rio.")]
        public override string log_machineName { get; set; }
        [MSValidRange(50, "A��o pode conter at� 50 caracteres.")]
        [MSNotNullOrEmpty("A��o � obrigat�rio.")]
        public override string log_acao { get; set; }
        [MSNotNullOrEmpty("Descri��o � obrigat�rio.")]
        public override string log_descricao { get; set; }
        [MSValidRange(50, "Sistema pode conter at� 50 caracteres.")]
        public override string sis_nome { get; set; }
        [MSValidRange(50, "M�dulo pode conter at� 50 caracteres.")]
        public override string mod_nome { get; set; }
        [MSValidRange(100, "Login pode conter at� 100 caracteres.")]
        public override string usu_login { get; set; }
        [MSValidRange(50, "Grupo pode conter at� 50 caracteres.")]
        public override string gru_nome { get; set; }

        #region CONSTANTES

        public const string SessionName = "LOG_Sistema";

        #endregion
	}
}