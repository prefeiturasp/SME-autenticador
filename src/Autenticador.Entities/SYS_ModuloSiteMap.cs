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
    public class SYS_ModuloSiteMap : Abstract_SYS_ModuloSiteMap 
    {
        [DataObjectField(true, false, false)]
        public override int sis_id { get; set; }
        [DataObjectField(true, false, false)]
        public override int mod_id { get; set; }
        [DataObjectField(true, false, false)]
        public override int msm_id { get; set; }
        [MSValidRange(50, "Nome do site map pode conter até 50 caracteres.")]
        [MSNotNullOrEmpty("Nome do site map do módulo é obrigatório.")]
        public override string msm_nome { get; set; }
        [MSValidRange(1000, "Descrição pode conter até 1000 caracteres.")]
        public override string msm_descricao { get; set; }
        [MSValidRange(500, "URL pode conter até 500 caracteres.")]
        public override string msm_url { get; set; }
        [MSValidRange(500, "URL do help pode conter até 500 caracteres.")]
        public override string msm_urlHelp { get; set; }
    }
}
