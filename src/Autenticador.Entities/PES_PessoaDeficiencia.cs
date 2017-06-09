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
    public class PES_PessoaDeficiencia : Abstract_PES_PessoaDeficiencia
    {
        [MSNotNullOrEmpty("Tipo de deficiência é obrigatório.")]
        [DataObjectField(true, false, false)]
        public override Guid tde_id { get; set; }
    }
}
