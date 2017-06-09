using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AutenticadorAPI.Controllers
{
    public class BaseApiController : ApiController
    {
        public Guid EntidadeID
        {
            get { return AppContext.GetIdEntidadeSistema(); }
        }

        protected HttpResponseMessage OKResponse<T>(T value)
        {
            return Request.CreateResponse(HttpStatusCode.OK, value);
        }

        protected HttpResponseMessage OKResponse()
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        protected HttpResponseMessage BadRequestResponse(string message = "")
        {
            return Request.CreateResponse(HttpStatusCode.BadRequest, message);
        }

        protected HttpResponseMessage NotFoundResponse(string message = "Recurso não encontrado!")
        {
            return Request.CreateResponse(HttpStatusCode.NotFound, message);
        }

        protected HttpResponseMessage InternalServerErrorResponse(string message = "Erro interno!")
        {
            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
        }
    }
}