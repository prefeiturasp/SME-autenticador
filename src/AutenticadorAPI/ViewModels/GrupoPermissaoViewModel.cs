using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutenticadorAPI.ViewModels
{
    public class GrupoPermissaoViewModel
    {

        public Guid gru_id { get; set; }

        public string gru_nome { get; set; }

        public IEnumerable<ModuloPermissaoViewModel> Modulos { get; set; }


    }
}