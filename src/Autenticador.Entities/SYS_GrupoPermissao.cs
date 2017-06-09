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
    public class SYS_GrupoPermissao : Abstract_SYS_GrupoPermissao 
    {
        [DataObjectField(true, false, false)]
        public override Guid gru_id { get; set; }
        [DataObjectField(true, false, false)]
        public override int sis_id { get; set; }
        [DataObjectField(true, false, false)]
        public override int mod_id { get; set; }
        public override bool grp_consultar { get; set; }
        public override bool grp_inserir { get; set; }
        public override bool grp_alterar { get; set; }
        public override bool grp_excluir { get; set; }
    }
}
