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
	public class PES_TipoEscolaridade : Abstract_PES_TipoEscolaridade
	{
        [MSValidRange(100, "Tipo de escolaridade pode conter at� 100 caracteres.")]
        [MSNotNullOrEmpty("Tipo de escolaridade � obrigat�rio.")]
        public override string tes_nome { get; set; }
        [MSNotNullOrEmpty("Ordem � obrigat�rio.")]
        public override int tes_ordem { get; set; }
        [MSDefaultValue(1)]
        public override byte tes_situacao { get; set; }
        public override DateTime tes_dataCriacao { get; set; }
        public override DateTime tes_dataAlteracao { get; set; }
        public override int tes_integridade { get; set; }
	}
}