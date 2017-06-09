using AutoMapper;
using Autenticador.BLL;
using Autenticador.BLL.V2;
using Autenticador.Entities;
using Autenticador.Entities.V2;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.OutputCache.V2;
using AutenticadorAPI.Infra;
using AutenticadorV2.Entities.Models;
using AutenticadorAPI.Models;
using System.Collections.Generic;

namespace AutenticadorAPI.Controllers
{
    /// <summary>
    /// Entidade reponsável por disponibilizar as informações das entidades. Elas podem ser
    /// configuradas para possuirem hierarquia e também podem ter unidades administrativas ligadas a elas.
    /// </summary>
    [RoutePrefix("coreapi/v1/entidades")]
    public class EntidadesController : BaseApiController
    {
        /// <summary>
        /// Entidade
        /// </summary>
        /// <param name="id">Id da entidade</param>
        /// <returns>Entidade</returns>
        [HttpGet]
        [Route("{id:guid}")]
        [ResponseType(typeof(AutenticadorV2.API.Model.Entidade))]
        [CacheOutput(ServerTimeSpan = OutputCacheConstants.MinutesTimeSpan15, ClientTimeSpan = OutputCacheConstants.MinutesTimeSpan1, MustRevalidate = true)]
        public HttpResponseMessage GetEntidade(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return BadRequestResponse("Id da entidade é obrigatório!");
                }

                var entidadeCoreSSO = SYS_EntidadeBO.GetEntity(id);

                if (entidadeCoreSSO == null)
                {
                    return NotFoundResponse("Entidade não encontrada.");
                }

                if (entidadeCoreSSO.ent_id == Guid.Empty)
                {
                    return NotFoundResponse("Entidade não encontrada.");
                }

                var model = Mapper.Map<SYS_Entidade, AutenticadorV2.API.Model.Entidade>(entidadeCoreSSO);

                var tipoEntidade = Mapper.Map<SYS_TipoEntidade, AutenticadorV2.API.Model.TipoEntidade>(SYS_TipoEntidadeBO.GetEntity(new SYS_TipoEntidade { ten_id = entidadeCoreSSO.ten_id }));

                model.TipoEntidade = tipoEntidade;

                return OKResponse(model);
            }
            catch (Exception ex)
            {
                Util.GravarErro(ex);
                return InternalServerErrorResponse();
            }
        }

        /// <summary>
        /// Lista de Entidades paginada.
        /// Página inicial = 0.
        /// Items por página padrão = 20.
        /// Quantidade máxima de registros = 100.
        /// </summary>
        /// <param name="currentPage">Página atual, página inicial = 0</param>
        /// <param name="pageSize">Tamanho da página, valor padrão = 20 itens e valor máximo = 100 itens por página.</param>
        /// <returns>Lista de Entidades</returns>
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(IEnumerable<AutenticadorV2.API.Model.Entidade>))]
        [CacheOutput(ServerTimeSpan = OutputCacheConstants.MinutesTimeSpan240, ClientTimeSpan = OutputCacheConstants.MinutesTimeSpan120, MustRevalidate = true)]
        public HttpResponseMessage GetEntidades(int currentPage = 0, int pageSize = 20)
        {
            try
            {
                if (pageSize > PageConfiguration.MaxPageSize)
                {
                    return BadRequestResponse("Limite de itens por página excedido!");
                }

                AutenticadorContext context = new AutenticadorContext();

                var query = context.SYS_Entidade.AsNoTracking();

                var total = query.Count();

                var lista = query
                                .Include("SYS_TipoEntidade")
                                .OrderBy(p => p.ent_razaoSocial)
                                .Skip(currentPage * pageSize)
                                .Take(pageSize).ToList();

                if (lista.Count == 0)
                {
                    return NotFoundResponse("Entidades não encontradas.");
                }

                var model = Mapper.Map<IEnumerable<AutenticadorV2.Entities.Models.Entidade>, IEnumerable<AutenticadorV2.API.Model.Entidade>>(lista);

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
        /// Retorna a lista com as subentidades (filhas)
        /// </summary>
        /// <param name="entidadeId">Id da Entidade</param>
        /// <returns>Lista de Entidades</returns>
        [HttpGet]
        [Route("{entidadeId:guid}/subentidades")]
        [ResponseType(typeof(IEnumerable<AutenticadorV2.API.Model.Entidade>))]
        [CacheOutput(ServerTimeSpan = OutputCacheConstants.MinutesTimeSpan30, ClientTimeSpan = OutputCacheConstants.MinutesTimeSpan15, MustRevalidate = true)]
        public HttpResponseMessage GetEntidadesFilhas(Guid entidadeId)
        {
            try
            {
                if (entidadeId == Guid.Empty)
                {
                    return BadRequestResponse("O Id da entidade é obrigatório!");
                }

                var entidadeCoreSSO = SYS_EntidadeBO.GetEntity(entidadeId);

                if (entidadeCoreSSO == null)
                {
                    return NotFoundResponse("Entidade não encontrada.");
                }

                if (entidadeCoreSSO.ent_id == Guid.Empty)
                {
                    return NotFoundResponse("Entidade não encontrada.");
                }

                // Seleciona a lista de entidades filhas
                EntidadeBO entidadeBO = new EntidadeBO();
                DataTable dt = entidadeBO.SelecionarEntidadesFilhas(entidadeId);

                // Verifica se a consulta retornou algum registro
                if (dt.Rows.Count == 0)
                {
                    return NotFoundResponse("Subentidade(s) não encontrada(s).");
                }

                List<EntidadeDTO> entidades = (
                    from l1 in dt.AsEnumerable()
                    select (EntidadeDTO)UtilBO.DataRowToEntity(l1, new EntidadeDTO()
                    {
                        TipoEntidade = (
                            (TipoEntidadeDTO)UtilBO.DataRowToEntity(l1, new TipoEntidadeDTO()))
                    }
                    )).ToList();

                var model = Mapper.Map<List<EntidadeDTO>, List<AutenticadorV2.API.Model.Entidade>>(entidades);

                return OKResponse(model);
            }
            catch (Exception ex)
            {
                Util.GravarErro(ex);
                return InternalServerErrorResponse();
            }
        }
    }
}