using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutenticadorAPI.DTO.Entrada
{
    public class AssociarUsuarioGrupoEntradaDTO
    {
        /// <summary>
        /// Id do grupo e da unidade administrativa, caso necessário.
        /// </summary>
        public UsuarioGrupoDTO[] usergroup { get; set; }
    }

    public class UsuarioGrupoDTO
    {
        /// <summary>
        /// ID do grupo do usuário
        /// </summary>
        public Guid gru_id { get; set; }

        /// <summary>
        /// ID da unidade administrativa do grupo do usuário
        /// </summary>
        public Guid uad_id { get; set; }
    }
}
