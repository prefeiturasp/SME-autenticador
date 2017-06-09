using Newtonsoft.Json;
using System;

namespace AutenticadorV2.API.Model
{
    public class TipoUnidadeAdministrativa
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }
    }

    public class UnidadeAdministrativa : EntityBase
    {
        public string Codigo { get; set; }

        public string CodigoIntegracao { get; set; }

        public Guid Id { get; set; }

        public Guid IdEntidade { get; set; }

        public Guid IdUnidadeSuperior { get; set; }

        public string Nome { get; set; }

        public string Sigla { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public TipoUnidadeAdministrativa TipoUnidade { get; set; }
    }
}
