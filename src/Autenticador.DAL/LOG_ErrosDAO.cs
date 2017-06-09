/*
	Classe gerada automaticamente pelo Code Creator
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CoreLibrary.Data.Common;
using Autenticador.Entities;
using Autenticador.DAL.Abstracts;

namespace Autenticador.DAL
{
    public class LOG_ErrosDAO : Abstract_LOG_ErrosDAO
    {
        /// <summary>
        /// Consulta para retornar o tipo de erro e quantidade
        /// de vezes que ele ocorreu em um intervalo de datas.
        /// </summary>
        /// <param name="dataInicio">Data inicial do intervalo.</param>
        /// <param name="dataTermino">Data final do intervalo.</param>
        /// <returns>DataTable agrupando tipo de erro e quantidade de erros desse tipo.</returns>
        public DataTable SelectBy_Data(DateTime dataInicio, DateTime dataTermino)
        {
            DataTable dt = new DataTable();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_LOG_Erros_Selectby_Busca_QtdErros", this._Banco);

            try
            {
                #region Parâmetros

                Param = qs.NewParameter();
                Param.DbType = DbType.DateTime;
                Param.ParameterName = "@DataInicio";
                Param.Size = 8;
                Param.Value = dataInicio;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.DateTime;
                Param.ParameterName = "@DataTermino";
                Param.Size = 8;
                Param.Value = dataTermino;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                return qs.Return;
            }
            catch
            {
                throw;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        /// <summary>
        /// Consulta pra retornar o tipo de erro e quantidade de
        /// vezes que este ocorreu em um intervalo de datas.
        /// </summary>
        /// <param name="dataInicio">data do intervalo inicial</param>
        /// <param name="dataTermino">data do intervalo final</param>
        /// <param name="currentPage">pagina atual do grid</param>
        /// <param name="pageSize">total de registros por página no grid</param>
        /// <param name="totalRecords">DataTable agrupando tipo de erro e quantidade de erros desse tipo</param>
        /// <returns></returns>
        public DataTable SelectBy_busca_QtdErros(DateTime dataInicio, DateTime dataTermino, int currentPage, int pageSize, out int totalRecords)
        {
            DataTable dt = new DataTable();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_LOG_Erros_Selectby_Busca_QtdErros", this._Banco);

            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.DateTime;
                Param.ParameterName = "@DataInicio";
                Param.Size = 8;
                Param.Value = dataInicio;//.ToString("yyyy-MM-dd");
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.DateTime;
                Param.ParameterName = "@DataTermino";
                Param.Size = 8;
                Param.Value = dataTermino;
                qs.Parameters.Add(Param);

                #endregion

                totalRecords = qs.Execute(currentPage, pageSize);
                               
                if (qs.Return.Rows.Count > 0)
                    dt = qs.Return;                   
                
                return dt;
            }
            catch
            {
                throw;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        /// <summary>
        /// Consulta para retorna a busca de log de erros onde são filtrados 
        /// os log por data e numeros erros de cadas um dos dias paginado.
        /// </summary>
        /// <param name="dataInicio">data do intervalo inicial</param>
        /// <param name="dataTermino">data do intervalo final</param>
        /// <param name="usu_id">Id do usuario do gestao core</param>
        /// <param name="currentPage">pagina atual do grid</param>
        /// <param name="pageSize">total de registros por página no grid</param>
        /// <param name="totalRecords">total de registros da consulta</param>
        /// <returns>DataTable agrupando data e numero de erros</returns>
        public DataTable SelectBy_busca(int sistema, DateTime dataInicio, DateTime dataTermino, string usu_login, int currentPage, int pageSize, out int totalRecords)
        {
            DataTable dt = new DataTable();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_LOG_Erros_SelectBY_Busca", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@sis_id";
                Param.Size = 4;
                if (sistema > 0)
                    Param.Value = sistema;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);


                Param = qs.NewParameter();
                Param.DbType = DbType.DateTime;
                Param.ParameterName = "@DataInicio";
                Param.Size = 8;
                if (dataInicio != new DateTime())
                    Param.Value = dataInicio;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.DateTime;
                Param.ParameterName = "@DataTermino";
                Param.Size = 8;
                if (dataTermino != new DateTime())
                    Param.Value = dataTermino;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@usu_login";
                Param.Size = 100;
                if (!String.IsNullOrEmpty(usu_login))
                    Param.Value = usu_login;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion


                totalRecords = qs.Execute(currentPage, pageSize);
                
                if (qs.Return.Rows.Count > 0)
                    dt = qs.Return;

                return dt;
            }
            catch
            {
                throw;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        /// <summary>
        ///  Consulta para retornar descrição(reduzida) do tipo de erro  
        /// em um determinado sistema e dia de ocorrência. 
        /// </summary>
        /// <param name="sis_id">Sistemas que ocorreu o erro</param>
        /// <param name="data">Data que ocorreu o erro</param>
        /// <param name="err_tipoErro">Tipo de erro</param>
        /// <param name="paginado">Paginado</param>
        /// <param name="currentPage">Página atual</param>
        /// <param name="pageSize">Total de registros por página</param>
        /// <param name="totalRecords">Total de registros por página</param>
        /// <returns>Lista de obejtos LOG_Erros carregado com o resultado da busca</returns>
        public List<LOG_Erros> Selectby_Busca_TipoErros(int sis_id, DateTime data, string err_tipoErro, int currentPage, int pageSize, out int totalRecords)
        {
            List<LOG_Erros> lt = new List<LOG_Erros>();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_LOG_Erros_Selectby_Busca_TipoErros", this._Banco);
            try
            {
                #region PARAMETROS
                
                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@sis_id";
                Param.Size = 4;
                if (sis_id > 0)
                    Param.Value = sis_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.DateTime;
                Param.ParameterName = "@Data";
                Param.Size = 8;
                Param.Value = data;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.String;
                Param.ParameterName = "@err_tipoErro";
                Param.Size = 1000;
                Param.Value = err_tipoErro;
                qs.Parameters.Add(Param);

                #endregion

                totalRecords = qs.Execute(currentPage, pageSize); 
              
                foreach (DataRow dr in qs.Return.Rows)
                {
                    LOG_Erros entity = new LOG_Erros();
                    lt.Add(this.DataRowToEntity(dr, entity));
                }
                return lt;
            }
            
            catch
            {
                throw;
            }

            finally
            {
                qs.Parameters.Clear();
            }
        }
                      
        /// <summary>
        /// Consulta para retornar o log de erros de um determinado dia paginado.
        /// com a descrição do erro reduzida
        /// </summary>
        /// <param name="data">Data da ocorrencia desejada.</param>
        /// <param name="currentPage">Página atual</param>
        /// <param name="pageSize">Total de registros por página</param>
        /// <param name="totalRecords">total de registros da consulta</param>
        /// <returns>Lista de obejtos LOG_Erros carregado com o resultado da busca</returns>
        public List<LOG_Erros> SelectBy_dia(int sis_id, DateTime data, string usu_login, bool paginado, int currentPage, int pageSize, out int totalRecords)
        {
            List<LOG_Erros> lt = new List<LOG_Erros>();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_LOG_Erros_SelectBY_Dia", this._Banco);
            try
            {
                #region PARAMETROS
                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@sis_id";
                Param.Size = 4;
                if (sis_id > 0)
                    Param.Value = sis_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.DateTime;
                Param.ParameterName = "@Data";
                Param.Size = 8;
                Param.Value = data;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@usu_login";
                Param.Size = 100;
                if (!String.IsNullOrEmpty(usu_login))
                    Param.Value = usu_login;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion

                if (paginado)
                    totalRecords = qs.Execute(currentPage, pageSize);
                else
                {
                    qs.Execute();
                    totalRecords = qs.Return.Rows.Count;
                }

                foreach (DataRow dr in qs.Return.Rows)
                {
                    LOG_Erros entity = new LOG_Erros();
                    lt.Add(this.DataRowToEntity(dr, entity));
                }
                return lt;
            }
            catch
            {
                throw;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        /// <summary>
        /// Consulta para retornar o log de erros de um determinado dia paginado.
        /// com a descrição do erro completa
        /// </summary>
        /// <param name="data">Data da ocorrencia desejada.</param>
        /// <param name="currentPage">Página atual</param>
        /// <param name="pageSize">Total de registros por página</param>
        /// <param name="totalRecords">total de registros da consulta</param>
        /// <returns>Lista de obejtos LOG_Erros carregado com o resultado da busca</returns>
        public List<LOG_Erros> SelectBy_dia2(int sis_id, DateTime data, string usu_login, bool paginado, int currentPage, int pageSize, out int totalRecords)
        {
            List<LOG_Erros> lt = new List<LOG_Erros>();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_LOG_Erros_SelectBY_Dia2", this._Banco);
            try
            {
                #region PARAMETROS
                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@sis_id";
                Param.Size = 4;
                if (sis_id > 0)
                    Param.Value = sis_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.DateTime;
                Param.ParameterName = "@Data";
                Param.Size = 8;
                Param.Value = data;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@usu_login";
                Param.Size = 100;
                if (!String.IsNullOrEmpty(usu_login))
                    Param.Value = usu_login;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion

                if (paginado)
                    totalRecords = qs.Execute(currentPage, pageSize);
                else
                {
                    qs.Execute();
                    totalRecords = qs.Return.Rows.Count;
                }

                foreach (DataRow dr in qs.Return.Rows)
                {
                    LOG_Erros entity = new LOG_Erros();
                    lt.Add(this.DataRowToEntity(dr, entity));
                }
                return lt;
            }
            catch
            {
                throw;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }
    }
}
