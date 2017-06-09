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
    public class END_PaisBO : BusinessBase<END_PaisDAO, END_Pais>
    {
        ///<summary>
        /// Retorna um datatable contendo todos os países
        /// que não foram excluídos logicamente, filtrados por 
        /// id, nome, sigla, situacao e paginado.        
        /// </summary>
        /// <param name="pai_id">Id da tabela END_Pais do bd</param>
        /// <param name="pai_nome">Campo pai_nome da tabela END_Pais do bd</param>
        /// <param name="pai_sigla">Campo pai_sigla da tabela END_Pais do bd</param>        
        /// <param name="pai_situacao">Campo pai_situcao da tabela END_Pais do bd</param>
        /// <param name="paginado">Indica se o datatable será paginado ou não</param> 
        /// <param name="currentPage">Página atual do gridview</param>
        /// <param name="pageSize">Total de registros por página</param>
        /// <returns>DataTable com os países</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetSelect
        (
            Guid pai_id
            , string pai_nome
            , string pai_sigla
            , byte pai_situacao
            , bool paginado
            , int currentPage
            , int pageSize
        )
        {
            totalRecords = 0;

            if (pageSize == 0)
                pageSize = 1;

            END_PaisDAO dal = new END_PaisDAO();
            try
            {
                return dal.SelectBy_All(pai_id, pai_nome, pai_sigla, pai_situacao, paginado, currentPage / pageSize, pageSize, out totalRecords);
            }
            catch
            {
                throw;
            }
        }
    }
}
