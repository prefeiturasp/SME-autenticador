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
    public class END_Cidade : Abstract_END_Cidade
    {
        [MSNotNullOrEmpty("País é obrigatório.")]
        public override Guid pai_id { get; set; }
        [MSValidRange(200, "Cidade pode conter até 200 caracteres.")]
        [MSNotNullOrEmpty("Cidade é obrigatório.")]
        public override string cid_nome { get; set; }
        [MSValidRange(3, "DDD pode conter até 3 caracteres.")]
        public override string cid_ddd { get; set; }
        [MSDefaultValue(1)]
        public override byte cid_situacao { get; set; }
        public override int cid_integridade { get; set; }

        //utilizado na busca incremental de cidades
        public virtual string cid_unf_pai_nome { get; set; }        
    }
}
