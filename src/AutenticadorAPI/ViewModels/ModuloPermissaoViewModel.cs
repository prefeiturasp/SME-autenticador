namespace AutenticadorAPI.ViewModels
{
    public class ModuloPermissaoViewModel
    {
        public int mod_id { get; set; }

        public string mod_nome { get; set; }

        public int? mod_idPai { get; set; }

        public int FlagPermissao { get; set; }

        private bool grp_alterar { get; set; }

        private bool grp_consultar { get; set; }

        private bool grp_excluir { get; set; }

        private bool grp_inserir { get; set; }

        public string Url { get; set; }
    }
}