using System;
using System.Data;
using System.ComponentModel;
using CoreLibrary.Business.Common;
using Autenticador.DAL;
using Autenticador.Entities;
using CoreLibrary.Validation.Exceptions;

namespace Autenticador.BLL
{
    public class SYS_GrupoPermissaoBO : BusinessBase<SYS_GrupoPermissaoDAO, SYS_GrupoPermissao>
    {

        #region METODOS

        /// <summary>
        /// Seleciona as visões que possuem permissão módulo do sistema
        /// </summary>
        /// <param name="sis_id">ID do sistema</param>
        /// <param name="mod_id">ID do módulo</param>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetSelect_Visoes(int sis_id, int mod_id)
        {
            SYS_GrupoPermissaoDAO dao = new SYS_GrupoPermissaoDAO();

            try
            {
                return dao.GetSelect_Visoes(sis_id, mod_id);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Insere permissão para determinada visão no grupo
        /// </summary>
        /// <param name="sis_id">ID do sistema</param>
        /// <param name="vis_id">ID da visão </param>
        /// <param name="mod_id">ID do módulo</param>
        /// <param name="banco"></param>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static void InsertPermissao_Visoes
        (
            int sis_id
            , int vis_id
            , int mod_id
            ,CoreLibrary.Data.Common.TalkDBTransaction banco
        )
        {
            SYS_GrupoPermissaoDAO dao = new SYS_GrupoPermissaoDAO { _Banco = banco };

            try
            {
                dao.InsertPermissao_Visoes(sis_id, vis_id, mod_id);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Delete permissão para determinada visão no grupo
        /// </summary>
        /// <param name="sis_id">ID do sistema</param>
        /// <param name="vis_id">ID da visão </param>
        /// <param name="mod_id">ID do módulo</param>
        /// <param name="banco"></param>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static void DeletePermissao_Visoes
        (
            int sis_id
            , int vis_id
            , int mod_id
            ,CoreLibrary.Data.Common.TalkDBTransaction banco
        )
        {
            SYS_GrupoPermissaoDAO dal = new SYS_GrupoPermissaoDAO { _Banco = banco };
            try
            {
                dal.DeletePermissao_Visoes(sis_id, vis_id, mod_id);
            }
            catch
            {
                throw;
            }
        }

        public new static bool Save(
            SYS_GrupoPermissao entityGrupoPermissao
            ,CoreLibrary.Data.Common.TalkDBTransaction banco
        )
        {
            SYS_GrupoPermissaoDAO dal = new SYS_GrupoPermissaoDAO { _Banco = banco };
            try
            {
                if (entityGrupoPermissao.Validate())
                {
                    dal.Salvar(entityGrupoPermissao);
                }
                else
                {
                    throw new ValidationException(entityGrupoPermissao.PropertiesErrorList[0].Message);
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }




        





        #endregion
    }
}
