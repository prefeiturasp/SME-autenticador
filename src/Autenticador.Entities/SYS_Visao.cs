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
    public class SYS_Visao : Abstract_SYS_Visao
    {
        [DataObjectField(true, true, false)]
        public override int vis_id { get; set; }
        [MSValidRange(50, "Nome da visão pode conter até 50 caracteres.")]
        [MSNotNullOrEmpty("Nome da visão é obrigatório.")]
        public override string vis_nome { get; set; }
    }
}
