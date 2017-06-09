using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.OutputCache.V2;

namespace AutenticadorAPI.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [RoutePrefix("coreapi/v1/caches")]
    public class CacheController : BaseApiController
    {
        [HttpGet]
        [Route("")]
        public IEnumerable<string> Get()
        {
            var config = Configuration.CacheOutputConfiguration();
            var cache = config.GetCacheOutputProvider(Request);

            return cache.AllKeys;
        }

        [HttpPost]
        [Route("{controllerName}/{actionName?}")]
        public HttpResponseMessage Post(string controllerName, string actionName = "")
        {
            if (string.IsNullOrWhiteSpace(controllerName))
            {
                return BadRequestResponse();
            }
            else
            {
                var config = Configuration.CacheOutputConfiguration();
                var cache = config.GetCacheOutputProvider(Request);

                var controllerSearch = controllerName;

                controllerSearch = ((controllerSearch.EndsWith("controller", StringComparison.OrdinalIgnoreCase)) ? controllerSearch : (controllerSearch + "Controller")).ToLower();

                var actionSearch = actionName.ToLower();
                string searchTemplate = "{0}-{1}";
                var nameSpace = "AutenticadorAPI.Controllers".ToLower();

                var listTypes = Assembly
                    .GetExecutingAssembly()
                        .GetTypes()
                            .Where(t => string.Equals(t.Namespace, nameSpace, StringComparison.OrdinalIgnoreCase));

                var controller = listTypes.Where(p => p.FullName.ToLower().Contains(controllerSearch)).FirstOrDefault();

                if (controller != null)
                {
                    if (!string.IsNullOrWhiteSpace(actionSearch))
                    {
                        var listMethods = controller.GetMethods();
                        var action = listMethods.Where(p => string.Equals(p.Name, actionSearch, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

                        if (action == null)
                        {
                            return NotFoundResponse();
                        }
                    }

                    string search = string.Format(searchTemplate, controllerSearch, actionSearch).ToLower();

                    var keyss = cache.AllKeys.Where(p => p.ToLower().Contains(search));

                    foreach (var item in keyss)
                    {
                        cache.Remove(item);
                    }
                }
                else
                {
                    return NotFoundResponse();
                }
            }

            return OKResponse();
        }
    }
}