using System;

namespace Autenticador.Web.WebProject.WebArea.WebSubArea
{
    public class SubAreaHome : CoreLibrary.Web.WebProject.SubArea
    {
        public override string _Diretorio
        {
            get { return _DiretorioVirtual; }
        }

        public override string _DiretorioImagens
        {
            get { return String.Concat(_DiretorioVirtual, "/"); }
        }

        public override string _Nome
        {
            get { throw new NotImplementedException(); }
        }

        public override string _PaginaInicial
        {
            get { return _Diretorio + "Index.aspx"; }
        }
    }
}
