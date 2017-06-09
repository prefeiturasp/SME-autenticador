using AutenticadorV2.API.Model;
using AutenticadorAPI.Infra;
using AutenticadorAPI.Models;
using Autenticador.BLL;
using Autenticador.BLL.V2;
using Autenticador.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.OutputCache.V2;

namespace AutenticadorAPI.Controllers
{
    /// <summary>
    /// Sistema é responsável por disponbilizar informações dos sistemas.
    /// </summary>
    [RoutePrefix("coreapi/v1/sistemas")]
    public class SistemasController : BaseApiController
    {
        /// <summary>
        /// Retorna a lista com os grupos de usuário do sistema
        /// </summary>
        /// <param name="sistemaId">Id do Sistema</param>
        /// <returns>Lista com os grupos de usuários</returns>
        [HttpGet]
        [Route("{sistemaId:int}/grupos")]
        [ResponseType(typeof(IEnumerable<Grupo>))]
        [CacheOutput(ServerTimeSpan = OutputCacheConstants.MinutesTimeSpan240, ClientTimeSpan = OutputCacheConstants.MinutesTimeSpan15, MustRevalidate = true)]
        public HttpResponseMessage GetGruposDeUsuarioDoSistema(int sistemaId)
        {
            try
            {
                if (sistemaId <= 0)
                {
                    return BadRequestResponse("Id do sistema é obrigatório");
                }

                var dados = GrupoBO.SelecionarGruposPorIdSistema(sistemaId);

                // Verifica se a consulta retornou algum registro
                if (dados.Count == 0)
                {
                    return NotFoundResponse("Grupo(s) não encontrado(s).");
                }

                var model = AutoMapper.Mapper.Map<List<SYS_Grupo>, List<Grupo>>(dados);

                return OKResponse(model);
            }
            catch (Exception ex)
            {
                Util.GravarErro(ex);
                return InternalServerErrorResponse();
            }
        }

        /// <summary>
        /// Retorna XML formatado com o Menu do sistema
        /// </summary>
        /// <param name="sistemaId">Id do Sistema</param>
        /// <param name="grupoId">Id do Grupo</param>
        /// <returns>
        /// <![CDATA[ <menus> <menu id="Configuração" url="~/Configuracao/Index.aspx" ordem="3">
        /// <item id = "Administração" url="~/Configuracao/Administracao/Index.aspx" ordem="1" />
        /// <item id = "Dados gerais" url="~/Configuracao/DadosGerais.aspx" ordem="2"> <subitem id =
        /// "Tipos" url="~/Configuracao/Tipos/Busca.aspx" ordem="1" /> <subitem id = "Tipo A"
        /// url="~/TipoA.aspx" ordem="2" /> <subitem id = "Tipo B" url="~/TipoB.aspx" ordem="5" />
        /// </item> </menu> </menus> ]]>
        /// </returns>
        [HttpGet]
        [Route("{sistemaId:int}/menu/{grupoId:guid}")]
        [CacheOutput(ServerTimeSpan = OutputCacheConstants.MinutesTimeSpan240, ClientTimeSpan = OutputCacheConstants.MinutesTimeSpan15, MustRevalidate = true)]
        public HttpResponseMessage GetMenuDoGrupoDeUsuarioNoSistema(int sistemaId, Guid grupoId)
        {
            try
            {
                if (sistemaId <= 0)
                {
                    return BadRequestResponse("O Id do sistema é obrigatório!");
                }

                if (grupoId == Guid.Empty)
                {
                    return BadRequestResponse("O Id do grupo é obrigatório!");
                }

                SYS_Grupo grupo = GrupoBO.SelecionarGrupoPorId(grupoId).FirstOrDefault();

                if (grupo == null)
                {
                    return NotFoundResponse("Grupo não encontrado!");
                }

                var menuxml = SYS_ModuloBO.CarregarMenuXML(grupoId
                            , sistemaId
                            , grupo.vis_id);

                return new HttpResponseMessage
                {
                    RequestMessage = Request,
                    Content = new StringContent(menuxml, Encoding.UTF8, "application/xml")
                };
            }
            catch (Exception ex)
            {
                Util.GravarErro(ex);
                return InternalServerErrorResponse();
            }
        }
    }
}