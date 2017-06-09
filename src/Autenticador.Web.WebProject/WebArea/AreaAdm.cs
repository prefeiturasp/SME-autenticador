using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Autenticador.Web.WebProject.WebArea
{
    public class AreaAdm : Area
    {
        public override CoreLibrary.Web.WebProject.SubArea _SubArea
        {
            get
            {
                if (base._SubArea == null)
                    return base._SubArea = new Autenticador.Web.WebProject.WebArea.WebSubArea.AreaAdm.SubAreaHome();
                return base._SubArea;
            }
            set
            {
                base._SubArea = value;
            }
        }

        public override string _Diretorio
        {
            get
            {
                return base._Diretorio + "AreaAdm/";
            }
        }
    }
}
