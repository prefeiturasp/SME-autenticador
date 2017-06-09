using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CoreLibrary.Data.Common;
using Autenticador.DAL.Abstracts;
using Autenticador.Entities;

namespace Autenticador.DAL
{
    public class END_UnidadeFederativaDAO : Abstract_END_UnidadeFederativaDAO
    {
        /// <summary>
        /// Retorna um datatable contendo todos as unidades federativas
        /// que não foram excluídas logicamente, filtradas por 
        /// id, pais, nome, sigla, situacao e paginado.        
        /// </summary>
        /// <param name="unf_id">Id da tabela END_UnidadeFederativa do bd</param>
        /// <param name="pai_id">Campo pai_id END_UnidadeFederativa do bd</param>
        /// <param name="unf_nome">Campo unf_nome da tabela END_UnidadeFederativa do bd</param>
        /// <param name="unf_sigla">Campo unf_sigla da tabela END_UnidadeFederativa do bd</param>        
        /// <param name="unf_situacao">Campo unf_situcao da tabela END_UnidadeFederativa do bd</param>
        /// <param name="paginado">Indica se o datatable será paginado ou não</param> 
        /// <returns>DataTable com as unidades federativas</returns>
        public DataTable SelectBy_All
        (
            Guid unf_id
            , Guid pai_id
            , string unf_nome
            , string unf_sigla
            , byte unf_situacao
            , bool paginado
            , int currentPage
            , int pageSize
            , out int totalRecords
        )
        {
            //DataTable dt = new DataTable();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_END_UF_SelectBy_All", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@unf_id";
                Param.Size = 16;
                if (unf_id != Guid.Empty)
                    Param.Value = unf_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

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
                Param.ParameterName = "@unf_nome";
                Param.Size = 200;
                if (!string.IsNullOrEmpty(unf_nome))
                    Param.Value = unf_nome;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@unf_sigla";
                Param.Size = 200;
                if (!string.IsNullOrEmpty(unf_sigla))
                    Param.Value = unf_sigla;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Byte;
                Param.ParameterName = "@unf_situacao";
                Param.Size = 1;
                if (unf_situacao > 0)
                    Param.Value = unf_situacao;
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
                //    dt = qs.Return;

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

        /// <summary>
        /// Seleciona a unidade federativa filtrando pela sigla.    
        /// </summary>
        /// <param name="entity">Entidade da unidade federativa com o campo UNF_SIGLA preenchido.</param>        
        /// <returns>Caso encontre a unidade federativa retorna true e carrega a entidade, se não retorna false.</returns>
        public bool SelectBy_Sigla
        (
            END_UnidadeFederativa entity
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_END_UnidadeFederativa_SelectBy_Sigla", this._Banco);
            
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@unf_sigla";
                Param.Size = 2;
                Param.Value = entity.unf_sigla;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                if (qs.Return.Rows.Count == 1)
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

        public int Select_Integridade
        (
            Guid unf_id
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_END_UnidadeFederativa_Select_Integridade", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@unf_id";
                Param.Size = 16;
                if (unf_id != Guid.Empty)
                    Param.Value = unf_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                if (qs.Return.Rows.Count > 0)
                    return Convert.ToInt32(qs.Return.Rows[0]["unf_integridade"].ToString());

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
        /// Incrementa uma unidade no campo integridade da unidade federativa
        /// </summary>
        /// <param name="unf_id">Id da tabela END_UnidadeFederativa do bd</param>
        /// <returns>true = sucesso | false = fracasso</returns>    
        public bool Update_IncrementaIntegridade
        (
            Guid unf_id
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_END_UnidadeFederativa_INCREMENTA_INTEGRIDADE", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@unf_id";
                Param.Size = 16;
                if (unf_id != Guid.Empty)
                    Param.Value = unf_id;
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
        /// Decrementa uma unidade no campo integridade da unidade federativa
        /// </summary>
        /// <param name="unf_id">Id da tabela END_UnidadeFederativa do bd</param>
        /// <returns>true = sucesso | false = fracasso</returns>    
        public bool Update_DecrementaIntegridade
        (
            Guid unf_id
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_END_UnidadeFederativa_DECREMENTA_INTEGRIDADE", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@unf_id";
                Param.Size = 16;
                if (unf_id != Guid.Empty)
                    Param.Value = unf_id;
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
        protected override void ParamInserir(QuerySelectStoredProcedure qs, Autenticador.Entities.END_UnidadeFederativa entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@pai_id";
            Param.Size = 16;
            Param.Value = entity.pai_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@unf_nome";
            Param.Size = 100;
            Param.Value = entity.unf_nome;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@unf_sigla";
            Param.Size = 2;
            Param.Value = entity.unf_sigla;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@unf_situacao";
            Param.Size = 1;
            Param.Value = entity.unf_situacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@unf_integridade";
            Param.Size = 4;
            Param.Value = entity.unf_integridade;
            qs.Parameters.Add(Param);
        }

        /// <summary>
        /// Verifica se o registro é utilizado em outras tabelas.
        /// </summary>
        /// <param name="tep_id">ID do registro.</param>
        /// <returns></returns>
        public bool VerificaIntegridade(Guid unf_id)
        {
            QueryStoredProcedure qs = new QueryStoredProcedure("NEW_VerificarIntegridade", _Banco);

            try
            {
                #region Parâmetros

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@tabelasNaoVerificar";
                Param.Value = "END_UnidadeFederativa";
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@campo";
                Param.Size = 50;
                Param.Value = "unf_id";
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@valorCampo";
                Param.Size = 50;
                Param.Value = unf_id.ToString();
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                return qs.Return > 0;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        /// <summary>
        /// Retorna true/false
        /// para saber se a unidade federativa já está cadastrada
        /// filtradas por unf_id (diferente do parametro informado), pai_id, unf_nome, unf_situacao
        /// </summary>
        /// <param name="cid_id">Id da tabela END_UnidadeFederativa do bd</param>
        /// <param name="pai_id">Id da tabela END_Pais do bd</param>   
        /// <param name="cid_nome">Campo unf_nome da da tabela END_UnidadeFederativa do bd</param>
        /// <param name="ent_situacao">Campo unf_situacao da tabela END_UnidadeFederativa do bd</param>        
        /// <returns>DataTable com as entidades</returns>
        public bool SelectBy_unf_nome
        (
            Guid unf_id
            , Guid pai_id
            , string unf_nome
            , byte unf_situacao
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_END_UnidadeFederativa_SelectBy_unf_nome", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@unf_id";
                Param.Size = 16;
                if (unf_id != Guid.Empty)
                    Param.Value = unf_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

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
                Param.ParameterName = "@unf_nome";
                Param.Size = 200;
                if (!string.IsNullOrEmpty(unf_nome))
                    Param.Value = unf_nome;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Byte;
                Param.ParameterName = "@unf_situacao";
                Param.Size = 1;
                if (unf_situacao > 0)
                    Param.Value = unf_situacao;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                if (qs.Return.Rows.Count > 0)
                    return true;

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
    }
}
