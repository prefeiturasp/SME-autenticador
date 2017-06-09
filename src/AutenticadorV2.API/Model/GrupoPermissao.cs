using System;
using System.Collections.Generic;

namespace AutenticadorV2.API.Model
{
    public class GrupoPermissao
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public IEnumerable<ModuloPermissao> Modulos { get; set; }
    }

    public class ModuloPermissao
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public int IdModuloPai { get; set; }

        public int FlagPermissao { get; set; }

        private bool Alterar { get; set; }

        private bool Consultar { get; set; }

        private bool Excluir { get; set; }

        private bool Incluir { get; set; }

        public string Url { get; set; }
    }

    public class ModuloSiteMap
    {
        private string msm_url { get; set; }
    }
}