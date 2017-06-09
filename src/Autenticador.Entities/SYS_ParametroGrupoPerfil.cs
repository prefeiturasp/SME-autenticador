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
	public class SYS_ParametroGrupoPerfil : Abstract_SYS_ParametroGrupoPerfil
	{
        [MSValidRange(100, "Chave pode conter at� 100 caracteres.")]
        [MSNotNullOrEmpty("Chave � obrigat�rio.")]
        public override string pgs_chave { get; set; }
        public override Guid gru_id { get; set; }
        [MSDefaultValue(1)]
        public override byte pgs_situacao { get; set; }
        public override DateTime pgs_dataCriacao { get; set; }
        public override DateTime pgs_dataAlteracao { get; set; }
	}
}