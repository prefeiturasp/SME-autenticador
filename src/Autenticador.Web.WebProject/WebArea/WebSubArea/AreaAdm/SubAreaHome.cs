using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Autenticador.Web.WebProject.WebArea.WebSubArea.AreaAdm
{
    public class SubAreaHome : Autenticador.Web.WebProject.WebArea.WebSubArea.SubAreaHome
    {
        public override string _Diretorio
        {
            get { return base._Diretorio + "AreaAdm/"; }
        }
    }
}
