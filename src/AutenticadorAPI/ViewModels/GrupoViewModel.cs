using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutenticadorAPI.ViewModels
{
    public class GrupoViewModel
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public int IdVisao { get; set; }

        public int IdSistema { get; set; }

        public byte Situacao { get; set; }
    }
}