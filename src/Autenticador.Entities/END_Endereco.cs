using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autenticador.Entities.Abstracts;
using CoreLibrary.Validation;
using System.ComponentModel;

namespace Autenticador.Entities
{
    [Serializable]
    public class END_Endereco : Abstract_END_Endereco
    {
        [MSValidRange(8, 8, "CEP deve conter 8 caracteres.")]
        [MSNotNullOrEmpty("CEP é obrigatório.")]
        public override string end_cep { get; set; }
        [MSValidRange(200, "Endereço pode conter até 200 caracteres.")]
        [MSNotNullOrEmpty("Endereço é obrigatório.")]
        public override string end_logradouro { get; set; }
        [MSValidRange(100, "Bairro pode conter até 100 caracteres.")]
        [MSNotNullOrEmpty("Bairro é obrigatório.")]
        public override string end_bairro { get; set; }
        [MSValidRange(100, "Distrito pode conter até 100 caracteres.")]
        public override string end_distrito { get; set; }
        [MSNotNullOrEmpty("Cidade é obrigatório.")]
        public override Guid cid_id { get; set; }
        [MSDefaultValue(1)]
        public override byte end_situacao { get; set; }
        public override DateTime end_dataCriacao { get; set; }
        public override DateTime end_dataAlteracao { get; set; }
        public override int end_integridade { get; set; }

        //utilizado na busca incremental de endereços
        public virtual string end_endereco { get; set; }
        public virtual string cid_nome { get; set; }
    }
}
