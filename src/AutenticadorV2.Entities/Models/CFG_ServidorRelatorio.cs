using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class CFG_ServidorRelatorio
    {
        public CFG_ServidorRelatorio()
        {
            this.CFG_Relatorio = new List<CFG_Relatorio>();
        }

        public int sis_id { get; set; }
        public System.Guid ent_id { get; set; }
        public int srr_id { get; set; }
        public string srr_nome { get; set; }
        public string srr_descricao { get; set; }
        public bool srr_remoteServer { get; set; }
        public string srr_usuario { get; set; }
        public string srr_senha { get; set; }
        public string srr_dominio { get; set; }
        public string srr_diretorioRelatorios { get; set; }
        public string srr_pastaRelatorios { get; set; }
        public byte srr_situacao { get; set; }
        public System.DateTime srr_dataCriacao { get; set; }
        public System.DateTime srr_dataAlteracao { get; set; }
        public virtual SYS_Sistema SYS_Sistema { get; set; }
        public virtual ICollection<CFG_Relatorio> CFG_Relatorio { get; set; }
    }
}
