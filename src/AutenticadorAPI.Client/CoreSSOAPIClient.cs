using Autenticador.BLL;
using AutenticadorAPI.DTO.Entrada;
using AutenticadorAPI.DTO.Saida;
using RestSharp;
using RestSharp.Authenticators;
using System;

namespace AutenticadorAPI.Client
{
    public static class AutenticadorAPIClient
    {
        /// <summary>
        /// O método redefine a senha de um usuário pela entidade e login. Todos os dados de senha devem ser passados com valores já criptografados.
        /// </summary>
        /// <param name="ent_id">ID da entidade do usuário.</param>
        /// <param name="usu_login">Login do usuário.</param>
        /// <param name="senhaAtual">Senha atual criptografada do usuário.</param>
        /// <param name="senhaNova">Nova senha criptografada do usuário.</param>
        /// <param name="uap_username">Nome do usuário da API configurado no CoreSSO.</param>
        /// <param name="uap_password">Senha criptogradada do usuário da API configurado no CoreSSO.</param>
        /// <returns></returns>
        public static RedefinirSenhaSaidaDTO RedefinirSenha(Guid ent_id, string usu_login, string senhaAtual, string senhaNova, string uap_username, string uap_password)
        {
            string urlApiCore = SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.URL_WEBAPI_CORESSO);

            RestClient client = new RestClient(urlApiCore);
            client.Authenticator = new HttpBasicAuthenticator(uap_username, uap_password);
            RestRequest request = new RestRequest("api/v1/users/password", Method.PUT);

            RedefinirSenhaEntradaDTO data = new RedefinirSenhaEntradaDTO
            {
                ent_id = ent_id
                ,
                usu_login = usu_login
                ,
                senhaAtual = senhaAtual
                ,
                senhaNova = senhaNova
                ,
                uap_username = uap_username
            };

            var json = SimpleJson.SerializeObject(data);
            request.AddParameter("text/json", json, ParameterType.RequestBody);

            var response = client.Execute(request);

            return (RedefinirSenhaSaidaDTO)SimpleJson.DeserializeObject(response.Content, typeof(RedefinirSenhaSaidaDTO));
        }
    }
}
