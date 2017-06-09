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
    public class SYS_Entidade : Abstract_SYS_Entidade
    {
        [MSNotNullOrEmpty("Tipo de entidade é obrigatório.")]
        public override Guid ten_id { get; set; }
        [MSValidRange(20, "Código pode conter até 20 caracteres.")]
        public override string ent_codigo { get; set; }
        [MSValidRange(200, "Nome fantasia pode conter até 200 caracteres.")]
        public override string ent_nomeFantasia { get; set; }
        [MSValidRange(200, "Razão social pode conter até 200 caracteres.")]
        [MSNotNullOrEmpty("Razão social é obrigatório.")]
        public override string ent_razaoSocial { get; set; }
        [MSValidRange(50, "Sigla pode conter até 50 caracteres.")]
        public override string ent_sigla { get; set; }
        [MSValidRange(14, "CNPJ pode conter até 14 caracteres.")]
        public override string ent_cnpj { get; set; }
        [MSValidRange(20, "Inscrição municipal pode conter até 20 caracteres.")]
        public override string ent_inscricaoMunicipal { get; set; }
        [MSValidRange(20, "Inscrição estadual pode conter até 20 caracteres.")]
        public override string ent_inscricaoEstadual { get; set; }
        [MSDefaultValue(1)]
        public override byte ent_situacao { get; set; }
        public override DateTime ent_dataCriacao { get; set; }
        public override DateTime ent_dataAlteracao { get; set; }
        public override int ent_integridade { get; set; }

        /// <summary>
        /// Url de acesso da entidade.
        /// </summary>
        [MSValidRange(200, "Url de acesso da entidade pode conter até 200 caracteres.")]
        public override string ent_urlAcesso { get; set; }

        /// <summary>
        /// Flag que indica se o logo da entidade serpa exibida.
        /// </summary>
        [MSDefaultValue(false)]
        public override bool ent_exibeLogoCliente { get; set; }

        /// <summary>
        /// Logo da entidade.
        /// </summary>
        [MSValidRange(2000, "Caminho da imagem do logo do cliente da entidade pode conter até 200 caracteres.")]
        public override string ent_logoCliente { get; set; }
    }
}
