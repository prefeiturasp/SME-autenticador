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
    public class SYS_VisaoModuloMenu : Abstract_SYS_VisaoModuloMenu
    {
        [DataObjectField(true, false, false)]
        public override int vis_id { get; set; }
        [DataObjectField(true, false, false)]
        public override int sis_id { get; set; }
        [DataObjectField(true, false, false)]
        public override int mod_id { get; set; }
        [DataObjectField(true, false, false)]
        public override int msm_id { get; set; }
        [MSNotNullOrEmpty("Ordem é obrigatório.")]
        public override int vmm_ordem { get; set; }
    }
}
