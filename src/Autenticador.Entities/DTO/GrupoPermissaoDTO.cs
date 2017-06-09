using System;
using System.Collections.Generic;

namespace Autenticador.Entities.V2
{
    public class GrupoPermissaoDTO
    {
        public Guid gru_id { get; set; }

        public string gru_nome { get; set; }

        public IEnumerable<ModuloPermisaoDTO> Modulos { get; set; }
    }

    public class ModuloPermisaoDTO
    {
        public int mod_id { get; set; }

        public string mod_nome { get; set; }

        public string mod_idPai { get; set; }

        public int FlagPermissao { get; set; }

        private bool grp_alterar { get; set; }

        private bool grp_consultar { get; set; }

        private bool grp_excluir { get; set; }

        private bool grp_inserir { get; set; }

        public string Url { get; set; }
    }

    public class ModuloSiteMapDTO
    {
        public string msm_url { get; set; }
    }
}