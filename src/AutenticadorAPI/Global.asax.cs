using AutenticadorV2.API.Mappers;
using AutenticadorAPI.Mappers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace AutenticadorAPI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // AutoMapper
            AutoMapper.Mapper.Initialize(x =>
            {
                x.AddProfile<EntityToViewModelProfile>();
                x.AddProfile<ViewModelToEntityProfile>();
                x.AddProfile<EntityToModelProfile>();
            });

            AppContext.Inicializar();
        }
    }
}