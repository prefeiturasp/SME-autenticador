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
    public class SYS_UsuarioGrupoUA : Abstract_SYS_UsuarioGrupoUA
    {
        [DataObjectField(true, false, false)]
        public override Guid usu_id { get; set; }
        [DataObjectField(true, false, false)]
        public override Guid gru_id { get; set; }
        [DataObjectField(true, false, false)]
        public override Guid ugu_id { get; set; }
        [MSNotNullOrEmpty("Entidade é obrigatório.")]
        public override Guid ent_id { get; set; }
        public override Guid uad_id { get; set; }

        /// <summary>
        /// Propriedade auxiliar para carregar o id da pessoa.
        /// </summary>
        public Guid pes_id { get; set; }

        /// <summary>
        /// Propriedade auxiliar para carregar o nome da entidade.
        /// </summary>
        public string ent_razaoSocial { get; set; }
    }
}
