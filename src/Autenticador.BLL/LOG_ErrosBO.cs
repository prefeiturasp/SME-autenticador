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
    public class LOG_ErrosBO : BusinessBase<LOG_ErrosDAO, LOG_Erros>
    {
        /// <summary>
        /// Consulta para retornar o tipo de erro e quantidade
        /// de vezes que ele ocorreu em um intervalo de datas.
        /// </summary>
        /// <param name="dataInicio">Data inicial do intervalo.</param>
        /// <param name="dataTermino">Data final do intervalo.</param>
        /// <returns>DataTable agrupando tipo de erro e quantidade de erros desse tipo.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static DataTable GetSelectBy_Data(DateTime dataInicio, DateTime dataTermino)
        {
            LOG_ErrosDAO dal = new LOG_ErrosDAO();
            try
            {
                return dal.SelectBy_Data(dataInicio, dataTermino);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Consulta para retornar o tipo de erro e quantidade
        /// de vezes que ele ocorreu em um intervalo de datas
        /// </summary>
        /// <param name="dataInicio">data do intervalo inicial</param>
        /// <param name="dataTermino">data do intervalo final</param>
        /// <param name="currentPage">pagina atual do grid</param>
        /// <param name="pageSize">otal de registros por página no grid</param>
        /// <returns>DataTable agrupando tipo de erro e quantidade de erros desse tipo</returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static DataTable GetSelectby_busca_QtdErros(DateTime dataInicio, DateTime dataTermino, int currentPage, int pageSize)
        {
            if (pageSize == 0)
                pageSize = 1;
            totalRecords = 0;
            LOG_ErrosDAO dal = new LOG_ErrosDAO();
            try
            {
                return dal.SelectBy_busca_QtdErros(dataInicio, dataTermino, currentPage / pageSize, pageSize, out totalRecords);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Consulta para retorna a busca de log de erros onde são filtrados 
        /// os log por data e numeros erros de cadas um dos dias.
        /// </summary>
        /// <param name="dataInicio">data do intervalo inicial</param>
        /// <param name="dataTermino">data do intervalo final</param>
        /// <param name="usu_id">Id do usuario do gestao core</param>
        /// <param name="currentPage">pagina atual do grid</param>
        /// <param name="pageSize">total de registros por página no grid</param>
        /// <returns>DataTable agrupando data e numero de erros</returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static DataTable GetSelect(int sis_id, DateTime dataInicio, DateTime dataTermino, string usu_login, int currentPage, int pageSize)
        {
            if (pageSize == 0)
                pageSize = 1;
            totalRecords = 0;
            LOG_ErrosDAO dal = new LOG_ErrosDAO();
            try
            {
                return dal.SelectBy_busca(sis_id, dataInicio, dataTermino, usu_login, currentPage / pageSize, pageSize, out totalRecords);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Consulta para retornar descrição(reduzida) do tipo de erro  
        /// em um determinado sistema e dia de ocorrência. 
        /// </summary>
        /// <param name="sis_id">Sistema ocorreu o erro</param>
        /// <param name="data">Data que ocorreu o erro</param>
        /// <param name="sis_tipoErro">Tipo de erro</param>
        /// <param name="paginado">Paginado</param>
        /// <param name="currentPage">Página atual</param>
        /// <param name="pageSize">Total de registros por página</param>
        /// <returns>Lista de obejtos LOG_Erros carregado com o resultado da busca</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static List<LOG_Erros> GetSelectby_Busca_TipoErros(int sis_id, DateTime data, string err_tipoErro, int currentPage, int pageSize)
        {
            if (pageSize == 0)
                pageSize = 1;
            totalRecords = 0;
            LOG_ErrosDAO dal = new LOG_ErrosDAO();
            try
            {
                return dal.Selectby_Busca_TipoErros(sis_id, data, err_tipoErro, currentPage / pageSize, pageSize, out totalRecords);
            }
            catch
            {
                throw;
            }
        }
                
        /// <summary>
        /// Consulta para retornar o log de erros de um determinado dia paginado.
        /// com a descrição do erro reduzida
        /// </summary>
        /// <param name="data">Data da ocorrencia desejada.</param>
        /// <param name="currentPage">Página atual</param>
        /// <param name="pageSize">Total de registros por página</param>
        /// <returns>Lista de obejtos LOG_Erros carregado com o resultado da busca</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static List<LOG_Erros> GetSelectBy_dia(int sis_id, DateTime data, string usu_login, bool paginado, int currentPage, int pageSize)
        {
            if (pageSize == 0)
                pageSize = 1;
            totalRecords = 0;
            LOG_ErrosDAO dal = new LOG_ErrosDAO();
            try
            {
                return dal.SelectBy_dia(sis_id, data, usu_login, paginado, currentPage / pageSize, pageSize, out totalRecords);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Consulta para retornar o log de erros de um determinado dia paginado.
        /// com a descrição do erro completa
        /// </summary>
        /// <param name="data">Data da ocorrencia desejada.</param>
        /// <param name="currentPage">Página atual</param>
        /// <param name="pageSize">Total de registros por página</param>
        /// <returns>Lista de obejtos LOG_Erros carregado com o resultado da busca</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static List<LOG_Erros> GetSelectBy_dia2(int sis_id, DateTime data, string usu_login, bool paginado, int currentPage, int pageSize)
        {
            if (pageSize == 0)
                pageSize = 1;
            totalRecords = 0;
            LOG_ErrosDAO dal = new LOG_ErrosDAO();
            try
            {
                return dal.SelectBy_dia2(sis_id, data, usu_login, paginado, currentPage / pageSize, pageSize, out totalRecords);
            }
            catch
            {
                throw;
            }
        }
    }
}
