using System;

namespace Autenticador.Web.WebProject.WebArea
{
    public class Area : CoreLibrary.Web.WebProject.Area
    {
        public override CoreLibrary.Web.WebProject.SubArea _SubArea
        {
            get
            {
                if (base._SubArea == null)
                    return base._SubArea = new WebSubArea.SubAreaHome();
                return base._SubArea;
            }
            set
            {
                base._SubArea = value;
            }
        }
        public override string _Diretorio
        {
            get { return _DiretorioVirtual; }
        }

        public override string _DiretorioImagens
        {
            get { return String.Concat(_DiretorioVirtual, "/"); }
        }

        public override string _DiretorioIncludes
        {
            get { return _DiretorioVirtual + "Includes/"; }
        }

        public override string _Nome
        {
            get { throw new NotImplementedException(); }
        }

        public override string _PaginaInicial
        {
            get { return _Diretorio + "Login.aspx"; }
        }
    }
}
