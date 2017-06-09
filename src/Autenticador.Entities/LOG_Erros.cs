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
    public class LOG_Erros : Abstract_LOG_Erros
    {
        [MSNotNullOrEmpty("Data/hora é obrigatório.")]
        public override DateTime err_dataHora { get; set; }
        [MSValidRange(15, "IP pode conter até 15 caracteres.")]
        [MSNotNullOrEmpty("IP é obrigatório.")]
        public override string err_ip { get; set; }
        [MSValidRange(256, "Nome da máquina pode conter até 256 caracteres.")]
        [MSNotNullOrEmpty("Nome da máquina é obrigatório.")]
        public override string err_machineName { get; set; }
        [MSValidRange(256, "Navegador pode conter até 256 caracteres.")]
        public override string err_browser { get; set; }
        [MSValidRange(2000, "Caminho do arquivo pode conter até 2000 caracteres.")]
        public override string err_caminhoArq { get; set; }
        [MSNotNullOrEmpty("Descrição é obrigatório.")]
        public override string err_descricao { get; set; }
        [MSValidRange(1000, "TIpo de erro pode conter até 1000 caracteres.")]
        public override string err_tipoErro { get; set; }
        [MSValidRange(100, "Descrição pode conter até 100 caracteres.")]
        public override string sis_decricao { get; set; }
        [MSValidRange(100, "Login pode conter até 100 caracteres.")]
        public override string usu_login { get; set; }
    }
}
