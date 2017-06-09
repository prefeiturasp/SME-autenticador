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
	public class SYS_Modulo : Abstract_SYS_Modulo
    {
        [DataObjectField(true, false, false)]
        public override int mod_id { get; set; }
        [MSValidRange(50, "Nome do m�dulo pode conter at� 50 caracteres.")]
        [MSNotNullOrEmpty("Nome do m�dulo � obrigat�rio.")]
        public override string mod_nome { get; set; }                
        [MSDefaultValue(1)]
        public override byte mod_situacao { get; set; }
        public override DateTime mod_dataCriacao { get; set; }
        public override DateTime mod_dataAlteracao { get; set; }

        #region CONSTANTES

        public const string SessionName = "SYS_Modulo";

        #endregion
    }
}