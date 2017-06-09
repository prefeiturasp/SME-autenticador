using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Autenticador.Entities.V2
{
    public class EntidadeDTO
    {
        public Guid ent_id { get; internal set; }
        public string ent_codigo { get; internal set; }

        public string ent_nomeFantasia { get; internal set; }

        public string ent_razaoSocial { get; internal set; }

        public string ent_sigla { get; internal set; }

        public string ent_cnpj { get; internal set; }

        public string ent_inscricaoMunicipal { get; internal set; }

        public string ent_inscricaoEstadual { get; internal set; }

        public Guid ent_idSuperior { get; internal set; }

        public DateTime ent_dataCriacao { get; set; }

        public DateTime ent_dataAlteracao { get; set; }

        public TipoEntidadeDTO TipoEntidade { get; set; }
    }

    public class TipoEntidadeDTO
    {
        public Guid ten_id { get; internal set; }

        public string ten_nome { get; internal set; }
    }
}