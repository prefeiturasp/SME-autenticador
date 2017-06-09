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
    public class SYS_UnidadeAdministrativa : Abstract_SYS_UnidadeAdministrativa
    {
        [MSNotNullOrEmpty("Entidade é obrigatório.")]
        [DataObjectField(true, false, false)]
        public override Guid ent_id { get; set; }
        [MSNotNullOrEmpty("Tipo de unidade administrativa é obrigatório.")]
        public override Guid tua_id { get; set; }
        [MSValidRange(20, "Código pode conter até 20 caracteres.")]
        public override string uad_codigo { get; set; }
        [MSValidRange(200, "Nome pode conter até 200 caracteres.")]
        [MSNotNullOrEmpty("Nome é obrigatório.")]
        public override string uad_nome { get; set; }
        [MSValidRange(50, "Sigla pode conter até 50 caracteres.")]
        public override string uad_sigla { get; set; }
        [MSDefaultValue(1)]
        public override byte uad_situacao { get; set; }
        public override DateTime uad_dataCriacao { get; set; }
        public override DateTime uad_dataAlteracao { get; set; }
        public String tua_nome { get; set; }
        public String ent_razaoSocial { get; set; }
        public String uad_nomeSup { get; set; }
        public override int uad_integridade { get; set; }
        [MSValidRange(30, "Codigo Inep pode conter até 30 caracteres.")]
        public override string uad_codigoInep { get; set; }
    }
}