using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutenticadorAPI.DTO.Entrada
{
    public class DeletarUsuarioDTO
    {
        /// <summary>
        /// ID da Entidade do usuário.
        /// </summary>
        public Guid ent_id { get; set; }

        /// <summary>
        /// Login do usuário.
        /// </summary>
        public string usu_login { get; set; }

        /// <summary>
        /// Senha do Usuário
        /// </summary>
        public string senha { get; set; }
    }
}
