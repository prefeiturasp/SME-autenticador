using Newtonsoft.Json;
using System;

namespace AutenticadorV2.API.Model
{
    public class Pessoa : EntityBase
    {
        public DateTime DataNascimento { get; set; }
        public int EstadoCivil { get; set; }
        public Guid Id { get; set; }

        public string Nome { get; set; }
        public int Sexo { get; set; }
    }

    public class Usuario : EntityBase
    {
        public int CodigoIntegracaoAD { get; set; }
        public string Dominio { get; set; }
        public string Email { get; set; }
        public Guid Id { get; set; }

        public string Login { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Pessoa Pessoa { get; set; }
    }
}
