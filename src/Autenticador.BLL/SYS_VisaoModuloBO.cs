using Autenticador.DAL;
using Autenticador.Entities;
using CoreLibrary.Business.Common;
using Autenticador.DAL;

namespace Autenticador.BLL
{
    public class SYS_VisaoModuloBO : BusinessBase<SYS_VisaoModuloDAO, SYS_VisaoModulo>
    {

        public new static bool Save
        (
            SYS_VisaoModulo entity
            , CoreLibrary.Data.Common.TalkDBTransaction banco
        )
        {
            try
            {
                if (entity.Validate())
                {
                    SYS_VisaoModuloDAO visaoModuloDAO = new SYS_VisaoModuloDAO { _Banco = banco };
                    visaoModuloDAO.Salvar(entity);
                }
                else
                {
                    throw new CoreLibrary.Validation.Exceptions.ValidationException(entity.PropertiesErrorList[0].Message);
                }

                return true;
            }
            catch
            {
                throw;
            }
        }

        public new static bool Delete
        (
            SYS_VisaoModulo entity
            , CoreLibrary.Data.Common.TalkDBTransaction banco
        )
        {
            try
            {
                SYS_VisaoModuloDAO visaoModuloDAO = new SYS_VisaoModuloDAO { _Banco = banco };
                visaoModuloDAO.Delete(entity);

                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
