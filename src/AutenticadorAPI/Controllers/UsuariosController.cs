using AutoMapper;
using AutenticadorV2.API.Model;
using AutenticadorAPI.Infra;
using AutenticadorAPI.Models;
using Autenticador.BLL.V2;
using Autenticador.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.OutputCache.V2;
using AutenticadorV2.API.Model;

namespace AutenticadorAPI.Controllers
{
    /// <summary>
    /// Usuários do sistema.
    /// </summary>
    [RoutePrefix("coreapi/v1/usuarios")]
    [CacheOutput(ServerTimeSpan = OutputCacheConstants.MinutesTimeSpan15, ClientTimeSpan = OutputCacheConstants.MinutesTimeSpan5, MustRevalidate = true)]
    public class UsuariosController : BaseApiController
    {
        /// <summary>
        /// Retorna Usuario por Id do Usuário.
        /// </summary>
        /// <param name="id">Id do Usuário</param>
        /// <returns>Dados do usuário</returns>
        [HttpGet]
        [Route("{id:guid}")]
        [ResponseType(typeof(AutenticadorV2.API.Model.Usuario))]
        public HttpResponseMessage GetUsuarioPorId(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return BadRequestResponse("O Id do usuário é obrigatório!");
                }

                var idEntidade = EntidadeID;

                var usuarioCore = UsuarioBO.GetEntity(idEntidade, id);

                // Verifica se a pesquisa retornou algum registro
                if (usuarioCore == null)
                {
                    return NotFoundResponse("Usuário não encontrado!");
                }

                var model = Mapper.Map<Autenticador.Entities.SYS_Usuario, AutenticadorV2.API.Model.Usuario>(usuarioCore);

                if (usuarioCore.pes_id != null)
                {
                    if (usuarioCore.pes_id != Guid.Empty)
                    {
                        var pessoa = Mapper.Map<Autenticador.Entities.PES_Pessoa, AutenticadorV2.API.Model.Pessoa>(Autenticador.BLL.PES_PessoaBO.GetEntity(new Autenticador.Entities.PES_Pessoa { pes_id = usuarioCore.pes_id }));
                        model.Pessoa = pessoa;
                    }
                }

                return OKResponse(model);
            }
            catch (Exception ex)
            {
                Util.GravarErro(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Erro interno.");
            }
        }

        /// <summary>
        /// Retorna Usuario por Login do Usuário.
        /// </summary>
        /// <param name="login">Login do Usuário.</param>
        /// <returns>Dados do usuário</returns>
        [HttpGet]
        [Route("{*login}")]
        [ResponseType(typeof(AutenticadorV2.API.Model.Usuario))]
        public HttpResponseMessage GetUsuarioPorLogin(string login)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(login))
                {
                    return BadRequestResponse("O login do usuário é obrigatório!");
                }

                var idEntidade = EntidadeID;

                var usuarioCore = UsuarioBO.GetEntity(idEntidade, login);

                // Verifica se a pesquisa retornou algum registro
                if (usuarioCore == null)
                {
                    return NotFoundResponse("Usuário não encontrado!");
                }

                var model = Mapper.Map<SYS_Usuario, AutenticadorV2.API.Model.Usuario>(usuarioCore);

                if (usuarioCore.pes_id != null)
                {
                    if (usuarioCore.pes_id != Guid.Empty)
                    {
                        var pessoa = Mapper.Map<PES_Pessoa, Pessoa>(Autenticador.BLL.PES_PessoaBO.GetEntity(new PES_Pessoa { pes_id = usuarioCore.pes_id }));
                        model.Pessoa = pessoa;
                    }
                }

                return OKResponse(model);
            }
            catch (Exception ex)
            {
                Util.GravarErro(ex);
                return InternalServerErrorResponse();
            }
        }

        /// <summary>
        /// Retorna lista de grupo que o usuário pertence
        /// </summary>
        /// <param name="usuarioId">Id do usuário</param>
        /// <returns>Lista de grupos</returns>
        [HttpGet]
        [Route("{usuarioId:guid}/grupos")]
        [ResponseType(typeof(AutenticadorV2.API.Model.Grupo))]
        public HttpResponseMessage GetGruposUsuario(Guid usuarioId)
        {
            try
            {
                AutenticadorV2.Entities.Models.AutenticadorContext context = new AutenticadorV2.Entities.Models.AutenticadorContext();
                var query = context.SYS_UsuarioGrupo
                                    .AsNoTracking()
                                    .Where(
                                        s => s.SYS_Usuario.usu_id == usuarioId
                                    )
                                    .Select(s => s.SYS_Grupo)
                                    .ToList();

                var lista = query
                                .OrderBy(p => p.gru_id)
                                .Select(s => new AutenticadorAPI.ViewModels.GrupoViewModel
                                {
                                    Id = s.gru_id,
                                    Nome = s.gru_nome,
                                    IdSistema = s.sis_id,
                                    IdVisao = s.vis_id,
                                    Situacao = s.gru_situacao
                                })
                                .ToList();

                if (lista.Count == 0)
                {
                    return NotFoundResponse("Grupos não encontrados.");
                }

                var response = Request.CreateResponse(HttpStatusCode.OK, lista);

                response.Headers.Add("X-RecordsCount", lista.Count.ToString());

                return response;
            }
            catch (Exception ex)
            {
                Util.GravarErro(ex);
                return InternalServerErrorResponse();
            }
        }

        /// <summary>
        /// Retorna lista de unidades administrativas, filtradas por grupo e usuario
        /// </summary>
        /// <param name="usuarioId">Id do usuário</param>
        /// <param name="grupoId">Id do grupo</param>
        /// <returns>Lista de unidades administrativas</returns>
        [HttpGet]
        [Route("{usuarioId:guid}/grupos/{grupoId:guid}/unidadesadministrativas")]
        [ResponseType(typeof(AutenticadorV2.API.Model.UnidadeAdministrativa))]
        [CacheOutput(ServerTimeSpan = OutputCacheConstants.MinutesTimeSpan5, ClientTimeSpan = OutputCacheConstants.MinutesTimeSpan5, MustRevalidate = true)]
        public HttpResponseMessage GetUnidadesAdministrativasGrupoUsuario(Guid usuarioId, Guid grupoId)
        {
            try
            {
                AutenticadorV2.Entities.Models.AutenticadorContext context = new AutenticadorV2.Entities.Models.AutenticadorContext();

                var entities = context.SYS_UsuarioGrupoUA
                                    .AsNoTracking()
                                    .Where(
                                        s => s.usu_id == usuarioId
                                        && s.gru_id == grupoId
                                        && s.SYS_UnidadeAdministrativa.uad_situacao != 3
                                        && s.SYS_UnidadeAdministrativa.SYS_TipoUnidadeAdministrativa.tua_situacao != 3
                                    )
                                    .Select(s => new AutenticadorV2.API.Model.UnidadeAdministrativa
                                    {
                                        Id = s.SYS_UnidadeAdministrativa.uad_id,
                                        Nome = s.SYS_UnidadeAdministrativa.uad_nome,
                                        Sigla = s.SYS_UnidadeAdministrativa.uad_sigla,
                                        IdEntidade = s.SYS_UnidadeAdministrativa.ent_id,
                                        IdUnidadeSuperior = s.SYS_UnidadeAdministrativa.uad_idSuperior ?? Guid.Empty,
                                        TipoUnidade = new TipoUnidadeAdministrativa
                                        {
                                            Id = s.SYS_UnidadeAdministrativa.SYS_TipoUnidadeAdministrativa.tua_id,
                                            Nome = s.SYS_UnidadeAdministrativa.SYS_TipoUnidadeAdministrativa.tua_nome
                                        },
                                        DataCriacao = s.SYS_UnidadeAdministrativa.uad_dataCriacao,
                                        DataAlteracao = s.SYS_UnidadeAdministrativa.uad_dataAlteracao
                                    })
                                    .OrderBy(s => s.Id)
                                    .ToList();

                if (entities.Count == 0)
                {
                    return NotFoundResponse("Unidades Administrativas não encontradas.");
                }

                var response = Request.CreateResponse(HttpStatusCode.OK, entities);

                response.Headers.Add("X-RecordsCount", entities.Count.ToString());

                return response;
            }
            catch (Exception ex)
            {
                Util.GravarErro(ex);
                return InternalServerErrorResponse();
            }
        }

        /// <summary>
        /// Retorna a lista de permissões do usuário
        /// </summary>
        /// <param name="usuarioId">Id do Grupo</param>
        /// <returns>Lista de permissões</returns>
        [HttpGet]
        [Route("{usuarioId:guid}/aplicativos")]
        [ResponseType(typeof(IEnumerable<AutenticadorV2.API.Model.GrupoPermissao>))]
        public HttpResponseMessage GetAplicativosDoUsuario(Guid usuarioId)
        {
            try
            {
                if (usuarioId == Guid.Empty)
                    return BadRequestResponse("Id do usuário é obrigatório!");

                AutenticadorV2.Entities.Models.AutenticadorContext context = new AutenticadorV2.Entities.Models.AutenticadorContext();
                var query = context.SYS_UsuarioGrupo
                                    .AsNoTracking()
                                    .Include
                                        (i => i.SYS_Grupo)
                                    .Include
                                        (i2 => i2.SYS_Grupo.SYS_Sistema)
                                    .Where
                                        (w => w.usu_id == usuarioId
                                           && w.SYS_Grupo.gru_situacao != 3
                                           && w.SYS_Grupo.SYS_Sistema.sis_situacao == 1
                                           && w.SYS_Grupo.SYS_Sistema.sis_ocultarLogo == false)
                                    .GroupBy(g => new AutenticadorAPI.ViewModels.SistemaViewModel
                                    {
                                        Id = g.SYS_Grupo.SYS_Sistema.sis_id,
                                        Nome = g.SYS_Grupo.SYS_Sistema.sis_nome,
                                        Caminho = g.SYS_Grupo.SYS_Sistema.sis_caminho,
                                        CaminhoLogout = g.SYS_Grupo.SYS_Sistema.sis_caminhoLogout,
                                        Descricao = g.SYS_Grupo.SYS_Sistema.sis_descricao.Substring(0, 5120).ToString(),
                                        OcultarLogo = g.SYS_Grupo.SYS_Sistema.sis_ocultarLogo,
                                        Situacao = g.SYS_Grupo.SYS_Sistema.sis_situacao,
                                        TipoAutenticacao = g.SYS_Grupo.SYS_Sistema.sis_tipoAutenticacao,
                                        UrlImagem = g.SYS_Grupo.SYS_Sistema.sis_urlImagem,
                                        UrlIntegracao = g.SYS_Grupo.SYS_Sistema.sis_urlIntegracao,
                                        UrlLogoCabecalho = g.SYS_Grupo.SYS_Sistema.sis_urlLogoCabecalho
                                    })
                                    .Select(s => s.Key)
                                    .ToList();

                //var lista = query
                //.OrderBy(o => o.Nome)
                //.Select(s => new AutenticadorAPI.ViewModels.SistemaViewModel
                //{
                //    Id = s.Id,
                //    Nome = s.Nome,
                //    Caminho = s.Caminho,
                //    CaminhoLogout = s.CaminhoLogout,
                //    Descricao = s.Descricao,
                //    OcultarLogo = s.OcultarLogo,
                //    Situacao = s.Situacao,
                //    TipoAutenticacao = s.TipoAutenticacao,
                //    UrlImagem = s.UrlImagem,
                //    UrlIntegracao = s.UrlIntegracao,
                //    UrlLogoCabecalho = s.UrlLogoCabecalho
                //})
                //.ToList();
                var lista = query
                            .OrderBy(o => o.Nome)
                            .ToList();

                int count = query.Count();

                if (count == 0)
                    return NotFoundResponse("Aplicativos não encontrados");

                return OKResponse(lista);
            }
            catch (Exception ex)
            {
                Util.GravarErro(ex);
                return InternalServerErrorResponse();
            }
        }
    }
}