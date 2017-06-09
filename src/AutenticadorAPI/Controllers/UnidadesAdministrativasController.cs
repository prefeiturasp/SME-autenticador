using AutoMapper;
using AutenticadorV2.API.Model;
using AutenticadorV2.Entities.Models;
using AutenticadorAPI.Infra;
using AutenticadorAPI.Models;
using AutenticadorAPI.ViewModels;
using Autenticador.BLL;
using Autenticador.BLL.V2;
using Autenticador.Entities.V2;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.OutputCache.V2;

namespace AutenticadorAPI.Controllers
{
    /// <summary>
    /// Unidades Administrativas responsável por disponibilizar as informações das unidades
    /// administrativas. Elas pertencem a uma Entidade e podem ser configuradas para possuirem
    /// hierarquia entre elas.
    /// </summary>
    [RoutePrefix("coreapi/v1/unidadesadministrativas")]
    public class UnidadesAdministrativasController : BaseApiController
    {
        /// <summary>
        /// Retorna a Unidade Administrativa
        /// </summary>
        /// <param name="id">Id da Unidade Administrativa</param>
        /// <returns>Unidade Administrativa</returns>
        [HttpGet]
        [Route("{id:guid}")]
        [ResponseType(typeof(UnidadeAdministrativa))]
        [CacheOutput(ServerTimeSpan = OutputCacheConstants.MinutesTimeSpan5, ClientTimeSpan = OutputCacheConstants.MinutesTimeSpan1, MustRevalidate = true)]
        public HttpResponseMessage GetUnidadeAdministrativaPorId(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return BadRequestResponse("O Id da unidade administrativa é obrigatório!");
                }

                var idEntidade = EntidadeID;

                var unidadeCoreSSO = SYS_UnidadeAdministrativaBO.GetEntity(idEntidade, id);

                // Verifica se a consulta retornou algum registro
                if (unidadeCoreSSO.uad_id == Guid.Empty)
                {
                    return NotFoundResponse("Unidade administrativa não encontrada.");
                }

                var model = Mapper.Map<Autenticador.Entities.SYS_UnidadeAdministrativa, UnidadeAdministrativa>(unidadeCoreSSO);

                // Seleciona o tipo da unidade administrativa
                Autenticador.Entities.SYS_TipoUnidadeAdministrativa _tipoUA = new Autenticador.Entities.SYS_TipoUnidadeAdministrativa { tua_id = unidadeCoreSSO.tua_id };
                SYS_TipoUnidadeAdministrativaBO.GetEntity(_tipoUA);

                var tipoUA = Mapper.Map<Autenticador.Entities.SYS_TipoUnidadeAdministrativa, AutenticadorV2.API.Model.TipoUnidadeAdministrativa>(_tipoUA);

                model.TipoUnidade = tipoUA;

                return OKResponse(model);
            }
            catch (Exception ex)
            {
                Util.GravarErro(ex);
                return InternalServerErrorResponse();
            }
        }

        /// <summary>
        /// Lista de Unidades Administrativas paginada.
        /// Página inicial = 0.
        /// Items por página padrão = 20.
        /// Quantidade máxima de registros = 100.
        /// </summary>
        /// <param name="codigo">Código da Unidade Adminitrativa</param>
        /// <param name="nome">Nome da Unidade Adminitrativa</param>
        /// <param name="sigla">Sigla da Unidade Adminitrativa</param>
        /// <param name="tipoId">Id do Tipo da Unidade Administrativa</param>
        /// <param name="currentPage">Página atual, página inicial = 0</param>
        /// <param name="pageSize">Tamanho da página, valor padrão = 20 itens e valor máximo = 100 itens por página.</param>
        /// <returns>Lista de Unidades Administrativas</returns>
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(IEnumerable<AutenticadorV2.API.Model.UnidadeAdministrativa>))]
        [CacheOutput(ServerTimeSpan = OutputCacheConstants.MinutesTimeSpan120, ClientTimeSpan = OutputCacheConstants.MinutesTimeSpan15, MustRevalidate = true)]
        public HttpResponseMessage GetUnidadesAdministrativas(string codigo = null, string nome = null, string sigla = null, Guid? tipoId = null, int currentPage = 0, int pageSize = 20)
        {
            try
            {
                // Faz as validações referente a paginação
                if (pageSize > PageConfiguration.MaxPageSize)
                {
                    return BadRequestResponse("Limite de itens por página excedido!");
                }

                // Seleciona o Id da entidade
                var entidadeId = EntidadeID;

                AutenticadorContext context = new AutenticadorContext();
                var query = context.SYS_UnidadeAdministrativa
                                    .AsNoTracking()
                                    .Where(
                                        w => w.ent_id == entidadeId
                                          && w.uad_situacao != 3
                                          && w.uad_codigo == (codigo != null ? codigo : w.uad_codigo)
                                          && w.uad_nome.Contains(nome != null ? nome : w.uad_nome)
                                          && w.uad_sigla.Contains(sigla != null ? sigla : w.uad_sigla)
                                          && w.tua_id == (tipoId != null ? tipoId : w.tua_id)
                                    );

                var total = query.Count();

                var lista = query
                                .Include("SYS_TipoUnidadeAdministrativa")
                                .OrderBy(p => p.uad_nome)
                                .Skip(currentPage * pageSize)
                                .Take(pageSize).ToList();

                if (total == 0)
                {
                    return NotFoundResponse("Unidades Administrativas não encontradas.");
                }

                var model = Mapper.Map<IEnumerable<AutenticadorV2.Entities.Models.SYS_UnidadeAdministrativa>, IEnumerable<AutenticadorV2.API.Model.UnidadeAdministrativa>>(lista);

                var response = Request.CreateResponse(HttpStatusCode.OK, model);

                response.Headers.Add("X-RecordsCount", total.ToString());

                return response;
            }
            catch (Exception ex)
            {
                Util.GravarErro(ex);
                return InternalServerErrorResponse();
            }
        }

        /// <summary>
        /// Retorna a lista com as subunidades (filhas) administrativas
        /// </summary>
        /// <param name="unidadeId">Id da Unidade Administrativas</param>
        /// <returns>Lista de Unidades Administrativas</returns>
        [HttpGet]
        [Route("{unidadeId:guid}/subunidades")]
        [ResponseType(typeof(IEnumerable<UnidadeAdministrativa>))]
        [CacheOutput(ServerTimeSpan = OutputCacheConstants.MinutesTimeSpan30, ClientTimeSpan = OutputCacheConstants.MinutesTimeSpan15, MustRevalidate = true)]
        public HttpResponseMessage GetUnidadesAdministrativasFilhas(Guid unidadeId)
        {
            try
            {
                if (unidadeId == Guid.Empty)
                {
                    return BadRequestResponse("O Id da unidade administrativa é obrigatório!");
                }

                var entidadeId = EntidadeID;

                var unidadeCoreSSO = SYS_UnidadeAdministrativaBO.GetEntity(entidadeId, unidadeId);

                if (unidadeCoreSSO == null)
                {
                    return NotFoundResponse("Unidade administrativa não encontrada.");
                }

                if (unidadeCoreSSO.ent_id == Guid.Empty)
                {
                    return NotFoundResponse("Unidade administrativa não encontrada.");
                }

                // Seleciona a lista de unidades administrativas filhas (subunidades)
                UnidadeAdministrativaBO unidadeBO = new UnidadeAdministrativaBO();
                DataTable dt = unidadeBO.SelecionarUnidadesAdministrativasFilhasV2(entidadeId, unidadeId);

                // Verifica se a consulta retornou algum registro
                if (dt.Rows.Count == 0)
                {
                    return NotFoundResponse("Subunidade(s) administrativa(s) não encontrada(s).");
                }

                List<UnidadeAdministrativaDTO> unidades = (
                    from l1 in dt.AsEnumerable()
                    select (UnidadeAdministrativaDTO)UtilBO.DataRowToEntity(l1, new UnidadeAdministrativaDTO()
                    {
                        TipoUnidade = (
                            (TipoUnidadeDTO)UtilBO.DataRowToEntity(l1, new TipoUnidadeDTO()))
                    }
                    )).ToList();

                var model = Mapper.Map<List<UnidadeAdministrativaDTO>, List<AutenticadorV2.API.Model.UnidadeAdministrativa>>(unidades);

                return OKResponse(model);
            }
            catch (Exception ex)
            {
                Util.GravarErro(ex);
                return InternalServerErrorResponse();
            }
        }

        /// <summary>
        /// Lista de Tipo Unidades Administrativas.
        /// </summary>
        /// <returns>Lista de Unidades Administrativas</returns>
        [HttpGet]
        [Route("tipos")]
        [ResponseType(typeof(IEnumerable<AutenticadorV2.API.Model.UnidadeAdministrativa>))]
        [CacheOutput(ServerTimeSpan = OutputCacheConstants.MinutesTimeSpan120, ClientTimeSpan = OutputCacheConstants.MinutesTimeSpan15, MustRevalidate = true)]
        public HttpResponseMessage GetTipoUnidadesAdministrativas()
        {
            try
            {
                AutenticadorContext context = new AutenticadorContext();
                var query = context.SYS_TipoUnidadeAdministrativa
                                    .AsNoTracking()
                                    .Where(w => w.tua_situacao != 3)
                                    .Select(s => new TipoUnidadeAdministrativaViewModel
                                    {
                                        Id = s.tua_id,
                                        Nome = s.tua_nome
                                    })
                                    .ToList();

                var total = query.Count();

                if (total == 0)
                {
                    return NotFoundResponse("Tipo(s) de Unidade(s) Administrativa(s) não encontrada(s).");
                }

                var response = Request.CreateResponse(HttpStatusCode.OK, query);

                response.Headers.Add("X-RecordsCount", total.ToString());

                return response;
            }
            catch (Exception ex)
            {
                Util.GravarErro(ex);
                return InternalServerErrorResponse();
            }
        }
    }
}