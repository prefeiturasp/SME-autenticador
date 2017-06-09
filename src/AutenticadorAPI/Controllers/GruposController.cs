using AutoMapper;
using AutenticadorV2.API.Model;
using AutenticadorAPI.Infra;
using AutenticadorAPI.Models;
using Autenticador.BLL;
using Autenticador.BLL.V2;
using Autenticador.Entities;
using Autenticador.Entities.V2;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.OutputCache.V2;
using AutenticadorV2.API.Model;

namespace AutenticadorAPI.Controllers
{
    /// <summary>
    /// Grupos de permissões dos usuários
    /// </summary>
    [RoutePrefix("coreapi/v1/grupos")]
    public class GruposController : BaseApiController
    {
        [Flags]
        private enum Permission
        {
            None = 0,
            Create = 8,
            Read = 4,
            Update = 2,
            Delete = 1,
            All = 15
        }

        /// <summary>
        /// Retorna os dados do grupo de usuário
        /// </summary>
        /// <param name="id">Id do Grupo de Usuário</param>
        /// <returns>retorna dos dados do grupo.</returns>
        [HttpGet]
        [Route("{id:guid}")]
        [ResponseType(typeof(Grupo))]
        [CacheOutput(ServerTimeSpan = OutputCacheConstants.MinutesTimeSpan60, ClientTimeSpan = OutputCacheConstants.MinutesTimeSpan15, MustRevalidate = true)]
        public HttpResponseMessage GetGrupoPorId(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return BadRequestResponse("Id do grupo é obrigatório!");
                }
                SYS_Grupo grupoCore = GrupoBO.SelecionarGrupoPorId(id).FirstOrDefault();

                if (grupoCore == null)
                {
                    return NotFoundResponse("Grupo não encontrado!");
                }

                if (grupoCore.gru_id == Guid.Empty)
                {
                    return NotFoundResponse("Grupo não encontrado!");
                }
                var model = Mapper.Map<SYS_Grupo, Grupo>(grupoCore);

                return OKResponse(model);
            }
            catch (Exception ex)
            {
                Util.GravarErro(ex);
                return InternalServerErrorResponse();
            }
        }

        /// <summary>
        /// Retorna a lista com as entidades vinculadas ao grupo de usuário
        /// </summary>
        /// <param name="grupoId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{grupoId:guid}/entidades")]
        [ResponseType(typeof(IEnumerable<Entidade>))]
        [CacheOutput(ServerTimeSpan = OutputCacheConstants.MinutesTimeSpan60, ClientTimeSpan = OutputCacheConstants.MinutesTimeSpan15, MustRevalidate = true)]
        public HttpResponseMessage GetEntidadesDoGrupoDeUsuario(Guid grupoId)
        {
            try
            {
                if (grupoId == Guid.Empty)
                {
                    return BadRequestResponse("Id do grupo é obrigatório!");
                }

                var idEntidade = EntidadeID;

                var listaEntidadesCore = EntidadeBO.SelecionarPorIdGrupo(idEntidade, grupoId);

                if (listaEntidadesCore == null)
                {
                    return NotFoundResponse("Entidade(s) relacionada(s) ao grupo não encontrada.");
                }

                if (listaEntidadesCore.Count == 0)
                {
                    return NotFoundResponse("Entidade(s) relacionada(s) ao grupo não encontrada.");
                }

                var model = Mapper.Map<List<SYS_Entidade>, List<Entidade>>(listaEntidadesCore);

                return OKResponse(model);
            }
            catch (Exception ex)
            {
                Util.GravarErro(ex);
                return InternalServerErrorResponse();
            }
        }

        /// <summary>
        /// Retorna a lista das unidades administrativas vinculadas ao grupo de usuário.
        /// </summary>
        /// <param name="grupoId">Id do Grupo de Usuário</param>
        /// <returns>Lista das unidades administrativas</returns>
        [HttpGet]
        [Route("{grupoId:guid}/unidadesadministrativas")]
        [ResponseType(typeof(IEnumerable<UnidadeAdministrativa>))]
        [CacheOutput(ServerTimeSpan = OutputCacheConstants.MinutesTimeSpan120, ClientTimeSpan = OutputCacheConstants.MinutesTimeSpan15, MustRevalidate = true)]
        public HttpResponseMessage GetUnidadesAdministrativasDoGrupoDeUsuario(Guid grupoId)
        {
            try
            {
                if (grupoId == Guid.Empty)
                {
                    return BadRequestResponse("Id do grupo é obrigatório!");
                }

                var idEntidade = EntidadeID;

                var listaUnidadeCore = UnidadeAdministrativaBO.SelecionarPorIdGrupo(idEntidade, grupoId);

                if (listaUnidadeCore == null)
                {
                    return NotFoundResponse("Unidades(s) Administrativa(s) relacionada(s) ao grupo não encontrada.");
                }

                if (listaUnidadeCore.Count == 0)
                {
                    return NotFoundResponse("Unidades(s) Administrativa(s) relacionada(s) ao grupo não encontrada.");
                }

                var model = Mapper.Map<List<SYS_UnidadeAdministrativa>, List<UnidadeAdministrativa>>(listaUnidadeCore);

                return OKResponse(model);
            }
            catch (Exception ex)
            {
                Util.GravarErro(ex);
                return InternalServerErrorResponse();
            }
        }

        /// <summary>
        /// Retorna a lista de usuários do grupo
        /// </summary>
        /// <param name="grupoId">Id do Grupo de Usuário</param>
        /// <returns>Lista de usuário</returns>
        [HttpGet]
        [Route("{grupoId:guid}/usuarios")]
        [ResponseType(typeof(IEnumerable<AutenticadorV2.API.Model.Usuario>))]
        [CacheOutput(ServerTimeSpan = OutputCacheConstants.MinutesTimeSpan30, ClientTimeSpan = OutputCacheConstants.MinutesTimeSpan15, MustRevalidate = true)]
        public HttpResponseMessage GetUsuariosDoGrupo(Guid grupoId)
        {
            try
            {
                if (grupoId == Guid.Empty)
                {
                    return BadRequestResponse("Id do grupo é obrigatório!");
                }

                var listaUsuariosCore = UsuarioBO.SelecionarPorIdGrupo(grupoId);

                if (listaUsuariosCore == null)
                {
                    return NotFoundResponse("Usuário(s) do grupo não encontrado!");
                }

                if (listaUsuariosCore.Count == 0)
                {
                    return NotFoundResponse("Usuário(s) do grupo não encontrado!");
                }

                var model = Mapper.Map<List<SYS_Usuario>, List<AutenticadorV2.API.Model.Usuario>>(listaUsuariosCore);

                return OKResponse(model);
            }
            catch (Exception ex)
            {
                Util.GravarErro(ex);
                return InternalServerErrorResponse();
            }
        }

        /// <summary>
        /// Retorna a lista de permissões do grupo
        /// </summary>
        /// <param name="grupoId">Id do Grupo</param>
        /// <returns>Lista de permissões</returns>
        [HttpGet]
        [Route("{grupoId:guid}/permissoes")]
        [ResponseType(typeof(IEnumerable<AutenticadorV2.API.Model.GrupoPermissao>))]
        public HttpResponseMessage GetPermissoesDoGrupo(Guid grupoId)
        {
            try
            {
                if (grupoId == Guid.Empty)
                    return BadRequestResponse("Id do grupo é obrigatório!");

                DataTable dt = GrupoBO.SelecionarPermissoesGrupoPorIdGrupo(grupoId);

                List<GrupoPermissaoDTO> grupoPermissao = (
                     from l1 in dt.AsEnumerable()
                     group l1 by l1["gru_id"] into grupo
                     select (GrupoPermissaoDTO)UtilBO.DataRowToEntity(grupo.First(), new GrupoPermissaoDTO()
                     {
                         Modulos = (
                             from l2 in grupo
                             group l2 by l2["mod_id"] into modulo
                             select (ModuloPermisaoDTO)UtilBO.DataRowToEntity(modulo.First(), new ModuloPermisaoDTO()
                             {
                                 FlagPermissao = RetornaFlagPermissao(
                                         Convert.ToBoolean(modulo.First()["grp_alterar"]),
                                         Convert.ToBoolean(modulo.First()["grp_consultar"]),
                                         Convert.ToBoolean(modulo.First()["grp_excluir"]),
                                         Convert.ToBoolean(modulo.First()["grp_inserir"])),

                                 Url = modulo.First()["msm_url"].ToString()
                             })
                            )
                     })
                       ).ToList();

                var model = Mapper.Map<List<GrupoPermissaoDTO>, List<GrupoPermissao>>(grupoPermissao);

                return OKResponse(model);
            }
            catch (Exception ex)
            {
                Util.GravarErro(ex);
                return InternalServerErrorResponse();
            }
        }

        private int RetornaFlagPermissao(
            bool Alterar,
            bool Consultar,
            bool Excluir,
            bool Inserir)
        {
            var permissaoRetorno = Permission.None;

            if (Excluir == true)
                permissaoRetorno = permissaoRetorno | Permission.Delete;

            if (Alterar == true)
                permissaoRetorno = permissaoRetorno | Permission.Update;

            if (Consultar == true)
                permissaoRetorno = permissaoRetorno | Permission.Read;

            if (Inserir == true)
                permissaoRetorno = permissaoRetorno | Permission.Create;

            return (int)permissaoRetorno;
        }
    }
}