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
    public class SYS_Grupo : Abstract_SYS_Grupo
    {
        [MSValidRange(50, "Grupo pode conter até 50 caracteres.")]
        [MSNotNullOrEmpty("Grupo é obrigatório.")]
        public override string gru_nome { get; set; }
        [MSDefaultValue(1)]
        public override byte gru_situacao { get; set; }
        public override DateTime gru_dataCriacao { get; set; }
        public override DateTime gru_dataAlteracao { get; set; }
        [MSNotNullOrEmpty("Visão é obrigatório.")]
        public override int vis_id { get; set; }
        [MSNotNullOrEmpty("Sistema é obrigatório.")]
        public override int sis_id { get; set; }
        public override int gru_integridade { get; set; }
    }
}
