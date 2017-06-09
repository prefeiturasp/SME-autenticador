using AutenticadorAPI.Models;
using Autenticador.BLL;
using Autenticador.Entities;
using AutenticadorAPI.DTO.Entrada;
using AutenticadorAPI.DTO.Saida;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace AutenticadorAPI.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class UsuarioController : ApiController
    {
        /// <summary>
        /// O método utilizado via web api na tela de meus dados de outros sistemas para redefinir a senha do usuário.
        /// </summary>
        /// <param name="data">DTO com informações do usuário e de alteração de senha.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/v1/users/password")]
        public HttpResponseMessage UpdatePassword([FromBody] RedefinirSenhaEntradaDTO data)
        {
            RedefinirSenhaSaidaDTO retorno = new RedefinirSenhaSaidaDTO();

            try
            {
                SYS_Usuario entity = new SYS_Usuario
                {
                    ent_id = data.ent_id
                    ,
                    usu_login = data.usu_login
                };
                SYS_UsuarioBO.GetSelectBy_ent_id_usu_login(entity);

                // Verifica se o id do usuário enviado existe na base de dados.
                if (entity.IsNew)
                {
                    retorno.Message = "Usuário não encontrado.";
                    retorno.statusCode = (int)HttpStatusCode.NotFound;
                    return Request.CreateResponse<RedefinirSenhaSaidaDTO>(HttpStatusCode.NotFound, retorno);
                }

                // Configura criptografia da senha
                eCriptografa criptografia = (eCriptografa)Enum.Parse(typeof(eCriptografa), Convert.ToString(entity.usu_criptografia), true);
                if (!Enum.IsDefined(typeof(eCriptografa), criptografia))
                    criptografia = eCriptografa.TripleDES;

                if (!UtilBO.EqualsSenha(entity.usu_senha, data.senhaAtual, criptografia))
                {
                    retorno.Message = "Senha atual inválida.";
                    retorno.statusCode = (int)HttpStatusCode.Unauthorized;
                    return Request.CreateResponse<RedefinirSenhaSaidaDTO>(HttpStatusCode.Unauthorized, retorno);
                }

                if (data.senhaAtual.Equals(data.senhaNova))
                {
                    retorno.Message = "Senha nova deve ser diferente da atual.";
                    retorno.statusCode = (int)HttpStatusCode.BadRequest;
                    return Request.CreateResponse<RedefinirSenhaSaidaDTO>(HttpStatusCode.BadRequest, retorno);
                }

                entity.usu_senha = data.senhaNova;

                SYS_UsuarioBO.AlterarSenhaUsuario(entity, false);

                retorno.Message = "Senha alterada com sucesso.";
                retorno.statusCode = (int)HttpStatusCode.OK;

                CFG_UsuarioAPI entityUsuarioAPI = CFG_UsuarioAPIBO.SelecionaPorUsername(data.uap_username);

                LOG_UsuarioAPIBO.Save
                (
                    new LOG_UsuarioAPI
                    {
                        usu_id = entity.usu_id
                        ,
                        uap_id = entityUsuarioAPI.uap_id
                        ,
                        lua_dataHora = DateTime.Now
                        ,
                        lua_acao = (byte)LOG_UsuarioAPIBO.eAcao.AlteracaoSenha
                    }
                );

                return Request.CreateResponse<RedefinirSenhaSaidaDTO>(HttpStatusCode.OK, retorno);
            }
            catch (Exception ex)
            {
                retorno.Message = ex.Message;
                retorno.statusCode = (int)HttpStatusCode.InternalServerError;
                Util.GravarErro(ex);
                return Request.CreateResponse<RedefinirSenhaSaidaDTO>(HttpStatusCode.InternalServerError, retorno);
            }
        }

        /// <summary>
        /// Método utilizado via Web API para criação de usuários
        ///
        /// OBSERVACAO: Este metodo faz uma busca por nome, data de nascimento e CPF
        /// para tentar vincular uma pessoa já existente com estes dados ao usuario
        /// que esta sendo criado, sendo que apenas nome e data de nascimento são requeridos.
        /// </summary>
        /// <param name="data">Parametros de entrada: Id Entidade, Id Grupo,  ID Usuario, Nome,
        /// CPF, Data de nascimento, E-mail, Senha
        /// </param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/v1/users")]
        public HttpResponseMessage Create([FromBody] UsuarioEntradaDTO data)
        {
            try
            {
                //Recupera usuário da API
                CFG_UsuarioAPI entityUsuarioAPI = CFG_UsuarioAPIBO.SelecionaPorUsername(Util.ReturnCredentialUserName(Request));

                //Cria o usuário
                Usuario.Create(data, entityUsuarioAPI);

                return Request.CreateResponse(HttpStatusCode.Created, "Usuário criado com sucesso.");
            }
            catch (ValidationException ex)
            {
                Util.GravarErro(ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                Util.GravarErro(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Método utilizado via Web API para criação de usuários
        ///
        /// OBSERVAÇÂO: este metodo SEMPRE cria uma nova pessoa ao usuario.
        /// </summary>
        /// <param name="data">Parametros de entrada: Id Entidade, Id Grupo,  ID Usuario, Nome,
        /// CPF, Data de nascimento, E-mail, Senha
        /// </param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/v2/users")]
        public HttpResponseMessage CreateV2([FromBody] UsuarioEntradaDTO data)
        {
            try
            {
                //Recupera usuário da API
                CFG_UsuarioAPI entityUsuarioAPI = CFG_UsuarioAPIBO.SelecionaPorUsername(Util.ReturnCredentialUserName(Request));

                //Cria o usuário
                Usuario.CreateV2(data, entityUsuarioAPI);

                return Request.CreateResponse(HttpStatusCode.Created, "Usuário criado com sucesso.");
            }
            catch (ValidationException ex)
            {
                Util.GravarErro(ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                Util.GravarErro(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Método utilizado via Web API para alteração de usuários
        /// </summary>
        /// <param name="data">Parametros de entrada: Id Entidade, Id Grupo, ID Usuario, Nome,
        /// CPF, Data de nascimento, E-mail, Senha</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/v1/users")]
        public HttpResponseMessage Update([FromBody] UsuarioEntradaDTO data)
        {
            try
            {
                //Recupera usuário da API
                CFG_UsuarioAPI entityUsuarioAPI = CFG_UsuarioAPIBO.SelecionaPorUsername(Util.ReturnCredentialUserName(Request));

                //Atualiza o usuário
                Usuario.Update(data, entityUsuarioAPI);

                return Request.CreateResponse(HttpStatusCode.OK, "Usuário alterado com sucesso.");
            }
            catch (ValidationException ex)
            {
                Util.GravarErro(ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                Util.GravarErro(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Método utilizado via Web API para alteração de login de um usuário
        /// </summary>
        /// <param name="data">Parametros de entrada: Id Entidade, login antigo, login novo
        /// </param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/v1/users/login")]
        public HttpResponseMessage UpdateLogin([FromBody] AlterarLoginEntradaDTO data)
        {
            try
            {
                //Recupera usuário da API
                CFG_UsuarioAPI entityUsuarioAPI = CFG_UsuarioAPIBO.SelecionaPorUsername(Util.ReturnCredentialUserName(Request));

                //Atualiza login do usuário
                Usuario.UpdateLogin(data, entityUsuarioAPI);

                return Request.CreateResponse(HttpStatusCode.OK, "Login alterado com sucesso.");
            }
            catch (ValidationException ex)
            {
                Util.GravarErro(ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                Util.GravarErro(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Método utilizado via Web API para exclusão de usuários
        /// </summary>
        /// <param name="data">Parametros de entrada: Id Entidade, login, senha</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/v1/users")]
        public HttpResponseMessage Delete([FromBody] DeletarUsuarioDTO data)
        {
            try
            {
                //Recupera usuário da API
                CFG_UsuarioAPI entityUsuarioAPI = CFG_UsuarioAPIBO.SelecionaPorUsername(Util.ReturnCredentialUserName(Request));

                return Usuario.Delete(data, entityUsuarioAPI, Request);
            }
            catch (Exception ex)
            {
                Util.GravarErro(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Método utilizado via Web API para associação de usuários à grupos
        /// </summary>
        /// <param name="data">Parâmetros de entrada: Id Usuario, Id Grupo, Id UA</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/v1/users/{id}/usergroup")]
        public HttpResponseMessage AssociateUserGroup([FromUri] Guid id, [FromBody] AssociarUsuarioGrupoEntradaDTO data)
        {
            try
            {
                //Recupera usuário da API
                CFG_UsuarioAPI entityUsuarioAPI = CFG_UsuarioAPIBO.SelecionaPorUsername(Util.ReturnCredentialUserName(Request));

                //Atualiza o usuário
                Usuario.AssociateUserGroup(id, data, entityUsuarioAPI);

                return Request.CreateResponse(HttpStatusCode.OK, "Grupo de usuário associado com sucesso.");
            }
            catch (ValidationException ex)
            {
                Util.GravarErro(ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                Util.GravarErro(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Método utilizado via Web API para selecionar as entidades vinculadas ao login.
        /// </summary>
        /// <param name="data">Parametros de entrada: Login.</param>
        /// <returns>Entidades vinculadas ao login.</returns>
        [HttpGet]
        [Route("api/v1/users/SelectEntityByLogin")]
        public HttpResponseMessage SelectEntityByLogin([FromUri] UsuarioEntradaDTO data)
        {
            try
            {
                if (data == null || data.usu_login == null)
                {
                    throw new ValidationException("É necessário informar o(s) parâmetro(s).");
                }

                List<SYS_UsuarioGrupoUA> listaUsuarios = SYS_UsuarioGrupoUABO.SelecionaPorLogin(data.usu_login);

                var usuarios = (from usu in listaUsuarios
                                group usu by usu.ent_id into grupo
                                select new
                                {
                                    pes_id = grupo.First().pes_id
                                    ,
                                    ent_razaoSocial = grupo.First().ent_razaoSocial
                                    ,
                                    ent_id = grupo.Key
                                    ,
                                    list_uad_id = grupo.GroupBy(p => p.uad_id).Where(p => p.Key != Guid.Empty).Select(p => new { uad_id = p.Key }).ToList()
                                }).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, usuarios);
            }
            catch (ValidationException ex)
            {
                Util.GravarErro(ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                Util.GravarErro(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Método utilizado via Web API para indicar se um usuário pode ser autenticado.
        /// </summary>
        /// <param name="data">Parametros de entrada: Login, senha e entidade.</param>
        /// <returns>Indica se o usuário pode ser autenticado no sistema.</returns>
        [HttpPut]
        [Route("api/v1/users/UserAuthorized")]
        public HttpResponseMessage UserAuthorized([FromBody] UsuarioEntradaDTO data)
        {
            try
            {
                if (data == null || data.usu_login == null)
                {
                    throw new ValidationException("É necessário informar o(s) parâmetro(s).");
                }

                SYS_Usuario usuario = new SYS_Usuario
                {
                    usu_login = data.usu_login,
                    usu_senha = data.senha,
                    ent_id = data.ent_id
                };
                LoginStatus status = SYS_UsuarioBO.LoginWEB(usuario, false);

                bool retorno = false;
                if (status == LoginStatus.Sucesso)
                {
                    retorno = true;
                }

                return Request.CreateResponse(HttpStatusCode.OK, retorno);
            }
            catch (ValidationException ex)
            {
                Util.GravarErro(ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                Util.GravarErro(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Método utilizado via Web API para selecionar os usuários vinculados à unidade.
        /// </summary>
        /// <param name="data">Parametros de entrada: Id da entidade, Id da unidade administrativa, Código da unidade administrativa, flag para indicar se deve trazer a foto e data base para seleção dos registros.</param>
        /// <returns>Entidades vinculadas ao login.</returns>
        [HttpGet]
        [Route("api/v1/users/SelectUsersByUad")]
        public HttpResponseMessage SelectUsersByUad([FromUri] UsuarioEntradaDTO data)
        {
            try
            {
                if (data == null)
                {
                    throw new ValidationException("É necessário informar o(s) parâmetro(s).");
                }

                if (data.uad_id == Guid.Empty && string.IsNullOrEmpty(data.uad_codigo))
                {
                    throw new ValidationException("Unidade administrativa é obrigatório.");
                }

                byte[] arquivo = null;
                DataTable dtUsuarios = string.IsNullOrEmpty(data.dataBase.ToString()) ?
                    SYS_UsuarioBO.SelecionaPorUnidadeAdministrativa(data.ent_id, data.uad_id, data.uad_codigo, data.trazerFoto, data.usu_id) :
                    SYS_UsuarioBO.SelecionaPorUnidadeAdministrativa(data.ent_id, data.uad_id, data.uad_codigo, data.trazerFoto, data.usu_id, data.dataBase);
                var usuarios = (from dr in dtUsuarios.Rows.Cast<DataRow>()
                                select new
                                {
                                    usu_id = new Guid(Convert.ToString(dr["usu_id"])),
                                    usu_login = Convert.ToString(dr["usu_login"]),
                                    usu_email = Convert.ToString(dr["usu_email"]),
                                    usu_senha = Convert.ToString(dr["usu_senha"]),
                                    usu_criptografia = Convert.ToByte(dr["usu_criptografia"]),
                                    usu_situacao = Convert.ToByte(dr["usu_situacao"]),
                                    pes_id = string.IsNullOrEmpty(Convert.ToString(dr["pes_id"])) ? Guid.Empty : new Guid(Convert.ToString(dr["pes_id"])),
                                    pes_nome = Convert.ToString(dr["pes_nome"]),
                                    pes_sexo = string.IsNullOrEmpty(dr["pes_sexo"].ToString()) ? 0 : Convert.ToByte(dr["pes_sexo"]),
                                    foto = string.IsNullOrEmpty(Convert.ToString(dr["foto"])) ? arquivo : (byte[])dr["foto"],
                                    usu_dataCriacao = Convert.ToDateTime(dr["usu_dataCriacao"]),
                                    usu_dataAlteracao = Convert.ToDateTime(dr["usu_dataAlteracao"]),
                                    pes_dataNascimento = string.IsNullOrEmpty(dr["pes_dataNascimento"].ToString())
                                         ? new DateTime()
                                         : Convert.ToDateTime(dr["pes_dataNascimento"])
                                }).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, usuarios);
            }
            catch (ValidationException ex)
            {
                Util.GravarErro(ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                Util.GravarErro(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Método utilizado via Web API para selecionar os usuários vinculados à entidade.
        /// </summary>
        /// <param name="data">Parametros de entrada: Id da entidade, Id da pessoa (opcional), flag para indicar se deve trazer a foto e data base para seleção dos registros.</param>
        /// <returns>Usuários vinculados à entidade.</returns>
        [HttpGet]
        [Route("api/v1/users/SelectUsersByEnt")]
        public HttpResponseMessage SelectUsersByEnt([FromUri] UsuarioEntradaDTO data)
        {
            try
            {
                if (data == null)
                {
                    throw new ValidationException("É necessário informar o(s) parâmetro(s).");
                }

                if (data.ent_id == Guid.Empty)
                {
                    throw new ValidationException("Entidade é obrigatório.");
                }

                byte[] arquivo = null;
                DataTable dtUsuarios = string.IsNullOrEmpty(data.dataBase.ToString()) ?
                    SYS_UsuarioBO.SelecionaPorEntidade(data.ent_id, data.pes_id, data.trazerFoto) :
                    SYS_UsuarioBO.SelecionaPorEntidade(data.ent_id, data.pes_id, data.trazerFoto, data.dataBase);
                var usuarios = (from dr in dtUsuarios.Rows.Cast<DataRow>()
                                select new
                                {
                                    usu_id = new Guid(Convert.ToString(dr["usu_id"])),
                                    usu_login = Convert.ToString(dr["usu_login"]),
                                    usu_email = Convert.ToString(dr["usu_email"]),
                                    usu_senha = Convert.ToString(dr["usu_senha"]),
                                    usu_criptografia = Convert.ToByte(dr["usu_criptografia"]),
                                    usu_situacao = Convert.ToByte(dr["usu_situacao"]),
                                    pes_id = string.IsNullOrEmpty(Convert.ToString(dr["pes_id"])) ? Guid.Empty : new Guid(Convert.ToString(dr["pes_id"])),
                                    pes_nome = Convert.ToString(dr["pes_nome"]),
                                    pes_sexo = string.IsNullOrEmpty(dr["pes_sexo"].ToString()) ? 0 : Convert.ToByte(dr["pes_sexo"]),
                                    foto = string.IsNullOrEmpty(Convert.ToString(dr["foto"])) ? arquivo : (byte[])dr["foto"],
                                    usu_dataCriacao = Convert.ToDateTime(dr["usu_dataCriacao"]),
                                    usu_dataAlteracao = Convert.ToDateTime(dr["usu_dataAlteracao"]),
                                    pes_dataNascimento = string.IsNullOrEmpty(dr["pes_dataNascimento"].ToString())
                                         ? new DateTime()
                                         : Convert.ToDateTime(dr["pes_dataNascimento"])
                                }).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, usuarios);
            }
            catch (ValidationException ex)
            {
                Util.GravarErro(ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                Util.GravarErro(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}