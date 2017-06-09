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
    public class PES_CertidaoCivil : Abstract_PES_CertidaoCivil
	{        
        [DataObjectField(true, false, false)]
        public override Guid ctc_id { get; set; }
        [MSNotNullOrEmpty("Tipo de certid�o � obrigat�rio.")]
        public override byte ctc_tipo { get; set; }
        [MSValidRange(50, "N�mero do termo pode conter at� 50 caracteres.")]
        public override string ctc_numeroTermo { get; set; }
        [MSValidRange(20, "Folha pode conter at� 20 caracteres.")]
        public override string ctc_folha { get; set; }
        [MSValidRange(20, "Livro pode conter at� 20 caracteres.")]
        public override string ctc_livro { get; set; }
        [MSValidRange(200, "Nome do cart�rio pode conter at� 200 caracteres.")]
        public override string ctc_nomeCartorio { get; set; }
        [MSValidRange(100, "Distrito pode conter at� 100 caracteres.")]
        public override string ctc_distritoCartorio { get; set; }
        [MSDefaultValue(1)]
        public override byte ctc_situacao { get; set; }
        public override DateTime ctc_dataCriacao { get; set; }
        public override DateTime ctc_dataAlteracao { get; set; }
        [MSValidRange(32, "Matr�cula pode conter at� 32 caracteres.")]
        public override string ctc_matricula { get; set; }
	}
}