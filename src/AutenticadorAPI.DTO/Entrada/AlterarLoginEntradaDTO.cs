using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutenticadorAPI.DTO.Entrada
{
   public class AlterarLoginEntradaDTO
    {
        /// <summary>
        /// ID da Entidade do usuário.
        /// </summary>
        public Guid ent_id { get; set; }

        /// <summary>
        /// Login do usuário antigo.
        /// </summary>
        public string usu_login_antigo { get; set; }

        /// <summary>
        /// Login do usuário novo.
        /// </summary>
        public string usu_login_novo { get; set; }
    }
}
