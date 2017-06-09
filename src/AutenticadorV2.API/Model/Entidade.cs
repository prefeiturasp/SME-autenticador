using Newtonsoft.Json;
using System;

namespace AutenticadorV2.API.Model
{
    public class Entidade : EntityBase
    {
        public string CNPJ { get; internal set; }

        public string Codigo { get; internal set; }

        public Guid Id { get; internal set; }

        public Guid IdEntidadeSuperior { get; internal set; }

        public string InscricaoEstadual { get; internal set; }

        public string InscricaoMunicipal { get; internal set; }

        public string NomeFantasia { get; internal set; }

        public string RazaoSocial { get; internal set; }

        public string Sigla { get; internal set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public TipoEntidade TipoEntidade { get; set; }
    }

    public class TipoEntidade
    {
        public Guid Id { get; internal set; }

        public string Nome { get; internal set; }
    }
}
