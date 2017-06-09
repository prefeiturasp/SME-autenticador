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
	public class LOG_Auditoria : Abstract_LOG_Auditoria
	{
        [MSNotNullOrEmpty("Data/hora � obrigat�rio.")]
        public override DateTime aud_dataHora { get; set; }
        [MSValidRange(100, "Entidade pode conter at� 100 caracteres.")]
        [MSNotNullOrEmpty("Entidade � obrigat�rio.")]
        public override string aud_entidade { get; set; }
        [MSValidRange(50, "Opera��o pode conter at� 50 caracteres.")]
        [MSNotNullOrEmpty("Opera��o � obrigat�rio.")]
        public override string aud_operacao { get; set; }
	}
}