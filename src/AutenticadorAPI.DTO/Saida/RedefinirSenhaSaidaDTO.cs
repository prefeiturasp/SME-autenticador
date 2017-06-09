using System;
using System.Net;

namespace AutenticadorAPI.DTO.Saida
{
    /// <summary>
    /// Classe de retorno ao redefinir senha do usuário.
    /// </summary>
    public class RedefinirSenhaSaidaDTO
    {
        /// <summary>
        /// Valor inteiro do código de resposta da requisição.
        /// </summary>
        public int statusCode { get; set; }

        /// <summary>
        /// Mensagem de retorno ao tentar redefinir a senha.
        /// </summary>
        public string Message { get; set; }
    }
}
