using CoreLibrary.Business.Common;
using Autenticador.DAL;
using Autenticador.Entities;
using System.ComponentModel;

namespace Autenticador.BLL
{
    public class SYS_VisaoModuloMenuBO : BusinessBase<SYS_VisaoModuloMenuDAO, SYS_VisaoModuloMenu>
    {
        /// <summary>
        /// Retorna a próxima ordem para inserção
        /// </summary>
        /// <param name="sis_id">Id do sistema</param>
        /// <param name="vis_id">Id do módulo</param>
        /// <param name="mod_idPai">Id do sistema</param>        
        /// <param name="banco"></param>
        /// <returns>Int com a ordem para o novo registro</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static int Gerar_vmm_ordem
        (
            int sis_id
            , int mod_idPai
            , int vis_id
             ,CoreLibrary.Data.Common.TalkDBTransaction banco
        )
        {
            SYS_VisaoModuloMenuDAO dao = new SYS_VisaoModuloMenuDAO { _Banco = banco };

            try
            {
                return dao.Gerar_vmm_ordem(sis_id, mod_idPai, vis_id);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna o sitemap setado como "PRINCIPAL", ou seja, inserido na SYS_VisaoModuloMenu
        /// </summary>
        /// <param name="sis_id">Id do sistema</param>
        /// <param name="mod_id">Id do módulo</param>
        /// <returns>Int com o id do SiteMap do menu</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static int GetSelect_SiteMapMenu
        (
            int sis_id
            , int mod_id 
        )
        {
            SYS_VisaoModuloMenuDAO dao = new SYS_VisaoModuloMenuDAO();

            try
            {
                return dao.GetSelect_SiteMapMenu(sis_id, mod_id);
            }
            catch
            {
                throw;
            }
        }


        public new static bool Delete
        (
            SYS_VisaoModuloMenu entity
            ,CoreLibrary.Data.Common.TalkDBTransaction banco
        )
        {
            try
            {
                SYS_VisaoModuloMenuDAO visaoModuloMenuDAO = new SYS_VisaoModuloMenuDAO {_Banco = banco};
                visaoModuloMenuDAO.Delete(entity);

                return true;
            }
            catch
            {
                throw;
            }
        }

        public new static bool Save
        (
            SYS_VisaoModuloMenu entity
            ,CoreLibrary.Data.Common.TalkDBTransaction banco
        )
        {
            try
            {
                if (entity.Validate())
                {
                    SYS_VisaoModuloMenuDAO visaoModuloMenuDAO = new SYS_VisaoModuloMenuDAO { _Banco = banco };
                    visaoModuloMenuDAO.Salvar(entity);
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
    }
}
