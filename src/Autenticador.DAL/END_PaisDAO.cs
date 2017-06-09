using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CoreLibrary.Data.Common;
using Autenticador.DAL.Abstracts;

namespace Autenticador.DAL
{
    public class END_PaisDAO : Abstract_END_PaisDAO
    {
        /// <summary>
        /// Retorna um datatable contendo todos os países
        /// que não foram excluídos logicamente, filtrados por 
        /// id, nome, sigla, situacao e paginado.        
        /// </summary>
        /// <param name="pai_id">Id da tabela END_Pais do bd</param>
        /// <param name="pai_nome">Campo pai_nome da tabela END_Pais do bd</param>
        /// <param name="pai_sigla">Campo pai_sigla da tabela END_Pais do bd</param>        
        /// <param name="pai_situacao">Campo pai_situcao da tabela END_Pais do bd</param>
        /// <param name="paginado">Indica se o datatable será paginado ou não</param> 
        /// <returns>DataTable com os países</returns>
        public DataTable SelectBy_All
        (
            Guid pai_id
            , string pai_nome
            , string pai_sigla
            , byte pai_situacao
            , bool paginado
            , int currentPage
            , int pageSize
            , out int totalRecords
        )
        {
            //DataTable dt = new DataTable();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_END_Pais_SelectBy_All", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@pai_id";
                Param.Size = 16;
                if (pai_id != Guid.Empty)
                    Param.Value = pai_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@pai_nome";
                Param.Size = 200;
                if (!string.IsNullOrEmpty(pai_nome))
                    Param.Value = pai_nome;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@pai_sigla";
                Param.Size = 200;
                if (!string.IsNullOrEmpty(pai_sigla))
                    Param.Value = pai_sigla;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Byte;
                Param.ParameterName = "@pai_situacao";
                Param.Size = 1;
                if (pai_situacao > 0)
                    Param.Value = pai_situacao;
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

                //if (qs.Return.Rows.Count > 0)
                    //dt = qs.Return;

                //return dt;
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

        public int Select_Integridade
       (
           Guid pai_id
       )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_END_Pais_Select_Integridade", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@pai_id";
                Param.Size = 16;
                if (pai_id != Guid.Empty)
                    Param.Value = pai_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                if (qs.Return.Rows.Count > 0)
                    return Convert.ToInt32(qs.Return.Rows[0]["pai_integridade"].ToString());

                return -1;
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
        /// Incrementa uma unidade no campo integridade do pais
        /// </summary>
        /// <param name="ent_id">Id da tabela end_pais do bd</param>
        /// <returns>true = sucesso | false = fracasso</returns>    
        public bool Update_IncrementaIntegridade
        (
            Guid pai_id
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_END_Pais_INCREMENTA_INTEGRIDADE", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@pai_id";
                Param.Size = 16;
                if (pai_id != Guid.Empty)
                    Param.Value = pai_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                return true;
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
        /// Decrementa uma unidade no campo integridade do pais
        /// </summary>
        /// <param name="ent_id">Id da tabela end_pais do bd</param>
        /// <returns>true = sucesso | false = fracasso</returns>    
        public bool Update_DecrementaIntegridade
        (
            Guid pai_id
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_END_Pais_DECREMENTA_INTEGRIDADE", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@pai_id";
                Param.Size = 16;
                if (pai_id != Guid.Empty)
                    Param.Value = pai_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                return true;
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
        /// Parâmetros para efetuar a inclusão sem o ID da PK gerado automaticamente
        /// </summary>
        protected override void ParamInserir(QuerySelectStoredProcedure qs, Autenticador.Entities.END_Pais entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@pai_nome";
            Param.Size = 100;
            Param.Value = entity.pai_nome;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@pai_sigla";
            Param.Size = 10;
            if (!string.IsNullOrEmpty(entity.pai_sigla))
                Param.Value = entity.pai_sigla;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@pai_ddi";
            Param.Size = 3;
            if (!string.IsNullOrEmpty(entity.pai_ddi))
                Param.Value = entity.pai_ddi;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@pai_naturalMasc";
            Param.Size = 100;
            if (!string.IsNullOrEmpty(entity.pai_naturalMasc))
                Param.Value = entity.pai_naturalMasc;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@pai_naturalFem";
            Param.Size = 100;
            if (!string.IsNullOrEmpty(entity.pai_naturalFem))
                Param.Value = entity.pai_naturalFem;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@pai_situacao";
            Param.Size = 1;
            Param.Value = entity.pai_situacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@pai_integridade";
            Param.Size = 4;
            Param.Value = entity.pai_integridade;
            qs.Parameters.Add(Param);
        }
    }    
}
