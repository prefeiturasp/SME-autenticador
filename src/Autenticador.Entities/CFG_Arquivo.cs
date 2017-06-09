/*
	Classe gerada automaticamente pelo Code Creator
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
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
	public class CFG_Arquivo : Abstract_CFG_Arquivo
	{
        /// <summary>
        /// ID do arquivo
        /// </summary>
        [DataObjectField(true, true, false)]
        public override Int64 arq_id { get; set; }

        /// <summary>
        /// Nome do arquivo
        /// </summary>
        [MSValidRange(256, "Nome do arquivo pode conter at� 256 caracteres.")]
        [MSNotNullOrEmpty("Nome do arquivo � obrigat�rio.")]
        public override string arq_nome { get; set; }

        /// <summary>
        /// Tamanho do arquivo em KB.
        /// </summary>
        [MSNotNullOrEmpty("Tamanho do arquivo � obrigat�rio.")]
        public override Int64 arq_tamanhoKB { get; set; }

        /// <summary>
        /// Tipo de arquivo.
        /// </summary>
        [MSValidRange(200, "Tipo de arquivo pode conter at� 256 caracteres.")]
        [MSNotNullOrEmpty("Tipo de arquivo � obrigat�rio.")]
        public override string arq_typeMime { get; set; }

        /// <summary>
        /// Conte�do bin�rio do arquivo
        /// </summary>
        [MSNotNullOrEmpty("Arquivo � obrigat�rio")]
        public override byte[] arq_data { get; set; }

        /// <summary>
        /// 1-Ativo, 3-Excluido, 4-Tempor�rio
        /// </summary>
        [MSDefaultValue(1)]
        public override byte arq_situacao { get; set; }

        /// <summary>
        /// Data de cria��o do registro
        /// </summary>
        public override DateTime arq_dataCriacao { get; set; }

        /// <summary>
        /// Data de altera��o do registro
        /// </summary>
        public override DateTime arq_dataAlteracao { get; set; }
	}
}