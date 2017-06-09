using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutenticadorAPI.ViewModels
{
    public class SistemaViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Caminho { get; set; }
        public string UrlImagem { get; set; }
        public string UrlLogoCabecalho { get; set; }
        public byte? TipoAutenticacao { get; set; }
        public string UrlIntegracao { get; set; }
        public byte Situacao { get; set; }
        public string CaminhoLogout { get; set; }
        public bool OcultarLogo { get; set; }
    }
}