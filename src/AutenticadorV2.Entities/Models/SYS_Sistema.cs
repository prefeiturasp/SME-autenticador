using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class SYS_Sistema
    {
        public SYS_Sistema()
        {
            this.CFG_ServidorRelatorio = new List<CFG_ServidorRelatorio>();
            this.SYS_Grupo = new List<SYS_Grupo>();
            this.SYS_Modulo = new List<SYS_Modulo>();
            this.SYS_SistemaEntidade = new List<SYS_SistemaEntidade>();
        }

        public int sis_id { get; set; }
        public string sis_nome { get; set; }
        public string sis_descricao { get; set; }
        public string sis_caminho { get; set; }
        public string sis_urlImagem { get; set; }
        public string sis_urlLogoCabecalho { get; set; }
        public Nullable<byte> sis_tipoAutenticacao { get; set; }
        public string sis_urlIntegracao { get; set; }
        public byte sis_situacao { get; set; }
        public string sis_caminhoLogout { get; set; }
        public bool sis_ocultarLogo { get; set; }
        public virtual ICollection<CFG_ServidorRelatorio> CFG_ServidorRelatorio { get; set; }
        public virtual ICollection<SYS_Grupo> SYS_Grupo { get; set; }
        public virtual ICollection<SYS_Modulo> SYS_Modulo { get; set; }
        public virtual ICollection<SYS_SistemaEntidade> SYS_SistemaEntidade { get; set; }
    }
}
