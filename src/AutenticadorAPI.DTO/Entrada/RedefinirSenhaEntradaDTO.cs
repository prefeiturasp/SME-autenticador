using System;

namespace AutenticadorAPI.DTO.Entrada
{
    /// <summary>
    /// Classe de entrada para redefinir senha do usuário.
    /// </summary>
    public class RedefinirSenhaEntradaDTO
    {
        /// <summary>
        /// ID da Entidade do usuário.
        /// </summary>
        public Guid ent_id { get; set; }

        /// <summary>
        /// Login do usuário.
        /// </summary>
        public string usu_login { get; set; }


        public string uap_username { get; set; }

        /// <summary>
        /// Senha atual criptografada do usuário.
        /// </summary>
        public string senhaAtual { get; set; }

        /// <summary>
        /// Senha nova criptografada do usuário.
        /// </summary>
        public string senhaNova { get; set; }
    }
}
