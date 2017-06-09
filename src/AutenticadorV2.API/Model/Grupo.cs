using System;

namespace AutenticadorV2.API.Model
{
    public class Grupo : EntityBase
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public int IdVisao { get; set; }

        public int IdSistema { get; set; }
    }

    public class GrupoPermissoes
    {
        public int FlagPermissao { get; set; }

        public Modulo Modulo { get; set; }
    }

    public class Modulo
    {
        public int IdModulo { get; set; }

        public string Nome { get; set; }

        public int IdModuloPai { get; set; }
    }
}