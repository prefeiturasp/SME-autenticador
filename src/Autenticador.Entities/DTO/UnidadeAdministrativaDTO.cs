using System;

namespace Autenticador.Entities.V2
{
    public class TipoUnidadeDTO
    {
        public Guid tua_id { get; internal set; }

        public string tua_nome { get; internal set; }
    }

    public class UnidadeAdministrativaDTO
    {
        public Guid ent_id { get; internal set; }

        public string ent_razaoSocial { get; internal set; }

        public TipoUnidadeDTO TipoUnidade { get; set; }

        public string tua_nome { get; internal set; }

        public string uad_codigo { get; internal set; }

        public string uad_codigoInep { get; internal set; }

        public string uad_codigoIntegracao { get; internal set; }

        public DateTime uad_dataAlteracao { get; internal set; }

        public DateTime uad_dataCriacao { get; internal set; }

        public Guid uad_id { get; internal set; }

        public int uad_integridade { get; internal set; }

        public string uad_nome { get; internal set; }

        public string uad_nomeSup { get; internal set; }

        public string uad_sigla { get; internal set; }

        public byte uad_situacao { get; internal set; }
    }
}
