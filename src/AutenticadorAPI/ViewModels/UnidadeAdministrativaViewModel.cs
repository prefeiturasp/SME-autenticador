using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutenticadorAPI.ViewModels
{
    public class UnidadeAdministrativaViewModel
    {
            
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
    }
}