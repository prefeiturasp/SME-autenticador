using Autenticador.Entities.Abstracts;
using CoreLibrary.Validation;
using System;

namespace Autenticador.Entities
{
    [Serializable]
    public class SYS_Sistema : Abstract_SYS_Sistema
    {
        [MSValidRange(100, "Sistema pode conter até 100 caracteres.")]
        [MSNotNullOrEmpty("Sistema é obrigatório.")]
        public override string sis_nome { get; set; }

        [MSValidRange(2000, "Caminho pode conter até 2000 caracteres.")]
        public override string sis_caminho { get; set; }

        [MSDefaultValue(1)]
        public override byte sis_situacao { get; set; }

        [MSNotNullOrEmpty("Tipo de autenticação é obrigatório.")]
        public override byte sis_tipoAutenticacao { get; set; }

        [MSValidRange(2000, "URL da imagem pode conter até 2000 caracteres.")]
        public override string sis_urlImagem { get; set; }

        [MSValidRange(2000, "URL do logotipo do cabeçalho pode conter até 2000 caracteres.")]
        public override string sis_urlLogoCabecalho { get; set; }

        [MSValidRange(200, "URL integração pode conter até 200 caracteres.")]
        public override string sis_urlIntegracao { get; set; }
    }
}