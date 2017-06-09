using System;
using System.Collections.Generic;
using System.Data;
using CoreLibrary.Data.Common;
using Autenticador.Entities;
using Autenticador.DAL.Abstracts;

namespace Autenticador.DAL
{
    public class SYS_SistemaDAO : Abstract_SYS_SistemaDAO
    {
        public IList<SYS_Sistema> SelectBy_usu_id(Guid usu_id)
        {
            List<SYS_Sistema> lt = new List<SYS_Sistema>();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Sistema_SELECTBY_usu_id", _Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@usu_id";
                Param.Size = 16;
                Param.Value = usu_id;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                foreach (DataRow dr in qs.Return.Rows)
                {
                    SYS_Sistema entity = new SYS_Sistema();
                    lt.Add(DataRowToEntity(dr, entity));
                }
                return lt;
            }
            catch 
            {
                throw;
            }        
        }

        public DataTable SelectBy_ModuloVinculado
        (
            bool paginado
            , int currentPage
            , int pageSize
            , out int totalRecords
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Sistema_SELECTBY_ModuloVinculado", _Banco);
            try
            {
                if (paginado)
                    totalRecords = qs.Execute(currentPage, pageSize);
                else
                {
                    qs.Execute();
                    totalRecords = qs.Return.Rows.Count;
                }
                
                return qs.Return;
            }
            catch
            {
                throw;
            }
        }
        
        public DataTable SelectBy_Sistema_Situacao
        (
              int sis_id
            , string sis_nome
            , byte sis_situacao
            , bool paginado
            , int currentPage
            , int pageSize
            , out int totalRecords
        )
        {
            //DataTable dt = new DataTable();
            QuerySelectStoredProcedure qss = new QuerySelectStoredProcedure("NEW_SYS_Sistema_SELECTBY_Sistema_Situacao", _Banco);
            try
            {
                #region PARAMETROS

                Param = qss.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@sis_id";
                Param.Size = 4;
                if (sis_id > 0)
                    Param.Value = sis_id;
                else
                    Param.Value = DBNull.Value;
                qss.Parameters.Add(Param);

                Param = qss.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@sis_nome";
                Param.Size = 100;
                if (!string.IsNullOrEmpty(sis_nome))
                    Param.Value = sis_nome;
                else
                    Param.Value = DBNull.Value;
                qss.Parameters.Add(Param);

                Param = qss.NewParameter();
                Param.DbType = DbType.Byte;
                Param.ParameterName = "@sis_situacao";
                Param.Size = 1;
                if (sis_situacao > 0)
                    Param.Value = sis_situacao;
                else
                    Param.Value = DBNull.Value;
                qss.Parameters.Add(Param);

                #endregion

                if (paginado)
                    totalRecords = qss.Execute(currentPage, pageSize);
                else
                {
                    qss.Execute();
                    totalRecords = qss.Return.Rows.Count;
                }

                //if (qss.Return.Rows.Count > 0)
                    //dt = qss.Return;

                //return dt;
                return qss.Return;
            }
            catch
            {
                throw;
            }
            finally
            {
                qss.Parameters.Clear();
            }
        }

        /// <summary>
        /// Carrega os dados do sistema através do caminho de logout
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool SelectBy_sis_caminhoLogout(SYS_Sistema entity)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Sistema_SELECTBY_sis_caminhoLogout", _Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@sis_caminhoLogout";
                Param.Size = 2000;
                if (!string.IsNullOrEmpty(entity.sis_caminhoLogout))
                    Param.Value = entity.sis_caminhoLogout;
                else
                    throw new ArgumentNullException("entity", "O parâmetro entity.sis_caminhoLogout não pode ser vazio ou nulo.");
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                if (qs.Return.Rows.Count > 0)
                {
                    entity = DataRowToEntity(qs.Return.Rows[0], entity, false);
                    return true;
                }
                return false;
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
        /// Carrega sistema apartir do caminho de login
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool SelectBy_sis_caminho(SYS_Sistema entity)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Sistema_SELECTBY_sis_caminho", _Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@sis_caminho";
                Param.Size = 2000;
                if (!string.IsNullOrEmpty(entity.sis_caminho))
                    Param.Value = entity.sis_caminho;
                else
                    throw new ArgumentNullException("entity", "O parâmetro entity.sis_caminho não pode ser vázio ou nulo.");                    

                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                if (qs.Return.Rows.Count > 0)
                {
                    entity = DataRowToEntity(qs.Return.Rows[0], entity, false);
                    return true;
                }
                return false;
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
        /// Atualiza o campo de auditoria nos modulos
        /// </summary>        
        /// <returns>True: ocorreu alguma alteração / False: não ocorreu nenhuma alteração </returns>
        public bool UpdateUrlInte
        (
            SYS_Sistema sistema
        )
        {
            int totalRecords;

            QuerySelectStoredProcedure ques = new QuerySelectStoredProcedure("NEW_SYS_Sistema_UPDATE_UrlIntegracao", _Banco);
            try
            {
                #region PARAMETROS

                Param = ques.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@sis_id";
                Param.Size = 4;
                if (sistema.sis_id > 0)
                    Param.Value = sistema.sis_id;
                else
                    Param.Value = DBNull.Value;
                ques.Parameters.Add(Param);

                Param = ques.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@sis_caminho";
                Param.Size = 100;
                if (!string.IsNullOrEmpty(sistema.sis_caminho))
                    Param.Value = sistema.sis_caminho;
                else
                    Param.Value = DBNull.Value;
                ques.Parameters.Add(Param);

                Param = ques.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@sis_urlIntegracao";
                Param.Size = 200;
                if (!string.IsNullOrEmpty(sistema.sis_urlIntegracao))
                    Param.Value = sistema.sis_urlIntegracao;
                else
                    Param.Value = DBNull.Value;
                ques.Parameters.Add(Param);
               
                #endregion

                ques.Execute();
                totalRecords = ques.Return.Rows.Count;

                if (totalRecords > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                throw;
            }
            finally
            {
                ques.Parameters.Clear();
            }
        }
    }
}
