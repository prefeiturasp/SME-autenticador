using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autenticador.Entities.Abstracts;
using CoreLibrary.Validation;
using System.ComponentModel;

namespace Autenticador.Entities
{
    public enum SYS_TipoMeioContatoValidacao : byte
    {
        Email = 1
        ,
        WebSite = 2
        ,
        Telefone = 3
    }

    [Serializable]
    public class SYS_TipoMeioContato : Abstract_SYS_TipoMeioContato
    {
        [MSValidRange(100, "Tipo de meio de contato pode conter até 100 caracteres.")]
        [MSNotNullOrEmpty("Tipo de meio de contato é obrigatório.")]
        public override string tmc_nome { get; set; }
        [MSDefaultValue(1)]
        public override byte tmc_situacao { get; set; }
        public override DateTime tmc_dataCriacao { get; set; }
        public override DateTime tmc_dataAlteracao { get; set; }
        public override int tmc_integridade { get; set; }
    }
}
