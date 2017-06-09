using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CoreLibrary.Business.Common;
using Autenticador.DAL;
using Autenticador.Entities;
using System.ComponentModel;

namespace Autenticador.BLL
{
    public class SYS_UnidadeAdministrativaContatoBO : BusinessBase<SYS_UnidadeAdministrativaContatoDAO, SYS_UnidadeAdministrativaContato>
    {
        /// <summary>
        /// Retorna um datatable contendo todos os contatos da unidade administrativa
        /// que não foram excluídos logicamente, filtrados por 
        /// id da entidade, id da unidade administrativa
        /// </summary>
        /// <param name="ent_id">Id da tabela SYS_Entidade do bd</param>
        /// <param name="uad_id">Id da tabela SYS_UnidadeAdministrativa do bd</param>
        /// <param name="paginado">Indica se o datatable será paginado ou não</param>
        /// <param name="currentPage">Página atual do gridview</param>
        /// <param name="pageSize">Total de registros por página</param> 
        /// <returns>DataTable com os contatos da unidade administrativa</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetSelect
        (
            Guid ent_id
            , Guid uad_id
            , bool paginado
            , int currentPage
            , int pageSize
        )
        {
            if (pageSize == 0)
                pageSize = 1;

            totalRecords = 0;
            SYS_UnidadeAdministrativaContatoDAO dal = new SYS_UnidadeAdministrativaContatoDAO();
            try
            {
                return dal.SelectBy_ent_id(ent_id, uad_id, paginado, currentPage / pageSize, pageSize, out totalRecords);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Inclui um novo contato para a unidade administrativa
        /// </summary>
        /// <param name="entity">Entidade SYS_UnidadeAdministrativaContato</param>        
        /// <returns>True = incluído/alterado | False = não incluído/alterado</returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public new static bool Save
        (
            Autenticador.Entities.SYS_UnidadeAdministrativaContato entity
            , CoreLibrary.Data.Common.TalkDBTransaction banco
        )
        {
            SYS_UnidadeAdministrativaContatoDAO dal = new SYS_UnidadeAdministrativaContatoDAO();
            dal._Banco = banco;

            try
            {
                if (entity.Validate())
                {
                    return dal.Salvar(entity);
                }
                else
                {
                    throw new CoreLibrary.Validation.Exceptions.ValidationException(entity.PropertiesErrorList[0].Message);
                }
            }
            catch
            {
                throw;
            }         
        }
    }
}
