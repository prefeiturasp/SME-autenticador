using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autenticador.Entities.Abstracts;
using CoreLibrary.Validation;
using System.ComponentModel;

namespace Autenticador.Entities
{
    [Serializable]
    public class SYS_DiaNaoUtil : Abstract_SYS_DiaNaoUtil
    {
        [MSValidRange(100, "Nome pode conter até 100 caracteres.")]
        [MSNotNullOrEmpty("Nome é obrigatório.")]
        public override string dnu_nome { get; set; }
        [MSValidRange(400, "Descrição pode conter até 400 caracteres.")]
        public override string dnu_descricao { get; set; }
        [MSNotNullOrEmpty("Data é obrigatório.")]
        public override DateTime dnu_data { get; set; }
        [MSNotNullOrEmpty("Data de início de vigência é obrigatório.")]
        public override DateTime dnu_vigenciaInicio { get; set; }
        [MSDefaultValue(1)]
        public override byte dnu_situacao { get; set; }
        public override DateTime dnu_dataCriacao { get; set; }
        public override DateTime dnu_dataAlteracao { get; set; }
    }
}
