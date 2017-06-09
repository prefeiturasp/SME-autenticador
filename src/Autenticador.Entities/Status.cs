using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreLibrary.Validation;

namespace Autenticador.Entities
{
    [Serializable]
    public class Status
    {
        [MSNotNullOrEmpty("Id do sistema é obrigatório.")]
        public int SistemaID { get; set; }
        [MSNotNullOrEmpty("Nome do sistema é obrigatório.")]
        public string SistemaNome { get; set; }
        [MSNotNullOrEmpty("Versao do sistema é obrigatório.")]
        public string SistemaVersao { get; set; }
        public string EmailSuporte { get; set; }
        public string EmailHost { get; set; }
        public string EmailTo { get; set; }
        [MSNotNullOrEmpty("Id do Usuario é obrigatório.")]
        public Guid UsuarioID { get; set; }
        [MSDefaultValue(false)]
        public bool UsuarioIsAuthorized { get; set; }

        //public virtual bool Validate()
        //{
        //}
    }
}
