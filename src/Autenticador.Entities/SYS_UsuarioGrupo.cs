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
    public class SYS_UsuarioGrupo : Abstract_SYS_UsuarioGrupo
    {
        [MSDefaultValue(1)]
        public override byte usg_situacao { get; set; }
    }
}
