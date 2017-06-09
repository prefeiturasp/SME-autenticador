using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class SYS_IntegracaoExterna
    {
        public System.Guid ine_id { get; set; }
        public string ine_descricao { get; set; }
        public string ine_urlInterna { get; set; }
        public string ine_urlExterna { get; set; }
        public string ine_dominio { get; set; }
        public byte ine_tipo { get; set; }
        public string ine_chave { get; set; }
        public string ine_tokenInterno { get; set; }
        public string ine_tokenExterno { get; set; }
        public bool ine_proxy { get; set; }
        public string ine_proxyIP { get; set; }
        public string ine_proxyPorta { get; set; }
        public bool ine_proxyAutenticacao { get; set; }
        public string ine_proxyAutenticacaoUsuario { get; set; }
        public string ine_proxyAutenticacaoSenha { get; set; }
        public byte ine_situacao { get; set; }
    }
}
