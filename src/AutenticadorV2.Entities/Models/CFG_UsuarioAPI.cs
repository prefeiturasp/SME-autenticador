using System;
using System.Collections.Generic;

namespace AutenticadorV2.Entities.Models
{
    public partial class CFG_UsuarioAPI
    {
        public CFG_UsuarioAPI()
        {
            this.LOG_UsuarioAPI = new List<LOG_UsuarioAPI>();
        }

        public int uap_id { get; set; }
        public string uap_username { get; set; }
        public string uap_password { get; set; }
        public byte uap_situacao { get; set; }
        public System.DateTime uap_dataCriacao { get; set; }
        public System.DateTime uap_dataAlteracao { get; set; }
        public virtual ICollection<LOG_UsuarioAPI> LOG_UsuarioAPI { get; set; }
    }
}
