using System;
using Autenticador.Entities.Abstracts;
using CoreLibrary.Validation;

namespace Autenticador.Entities
{
    [Serializable]
    public class SYS_Parametro : Abstract_SYS_Parametro
    {
        [MSValidRange(100, "Chave pode conter até 100 caracteres.")]
        [MSNotNullOrEmpty("Chave é obrigatório.")]
        public override string par_chave { get; set; }
        [MSValidRange(1000, "Valor pode conter até 1000 caracteres.")]
        public override string par_valor { get; set; }
        [MSValidRange(200, "Descrição pode conter até 200 caracteres.")]
        public override string par_descricao { get; set; }
        [MSDefaultValue(1)]
        public override byte par_situacao { get; set; }
        [MSNotNullOrEmpty("Vigência inicial é obrigatório.")]
        public override DateTime par_vigenciaInicio { get; set; }
        public override DateTime par_dataCriacao { get; set; }
        public override DateTime par_dataAlteracao { get; set; }
    }
}
