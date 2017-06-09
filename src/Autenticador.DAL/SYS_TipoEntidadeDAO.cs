using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CoreLibrary.Data.Common;
using Autenticador.DAL.Abstracts;

namespace Autenticador.DAL
{
    public class SYS_TipoEntidadeDAO : Abstract_SYS_TipoEntidadeDAO
    {
        /// <summary>
        /// Retorna um datatable contendo todos os tipos de entidade
        /// que não foram excluídos logicamente, filtrados por 
        /// id, nome, situacao.        
        /// </summary>
        /// <param name="ten_id">Id da tabela SYS_TipoEntidade do bd</param>
        /// <param name="ten_nome">Campo ten_nome da tabela SYS_TipoEntidade do bd</param>        
        /// <param name="ten_situacao">Campo ten_situcao da tabela SYS_TipoEntidade do bd</param>
        /// <param name="paginado">Indica se o datatable será paginado ou não</param> 
        /// <returns>DataTable com tipos de entidade paginado</returns>
        public DataTable SelectBy_All
        (
            Guid ten_id
            , string ten_nome            
            , byte ten_situacao
            , bool paginado
            , int currentPage
            , int pageSize
            , out int totalRecords
        )
        {
            //DataTable dt = new DataTable();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_TipoEntidade_SelectBy_All", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@ten_id";
                Param.Size = 16;
                if (ten_id != Guid.Empty)
                    Param.Value = ten_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@ten_nome";
                Param.Size = 100;
                if (!string.IsNullOrEmpty(ten_nome))
                    Param.Value = ten_nome;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Byte;
                Param.ParameterName = "@ten_situacao";
                Param.Size = 1;
                if (ten_situacao > 0)
                    Param.Value = ten_situacao;
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

        /// <summary>
        /// Retorno booleano na qual verifica se existe nome de Tipo de Entidade 
        /// já existente no banco com situação Ativo ou Bloqueado.             
        /// </summary>
        /// <param name="ten_id">ID do tipo de entidade</param>
        /// <param name="ten_nome">Nome do tipo de entidade</param>
        /// <returns>DataTable com os tipos de entidades</returns>
        public bool SelectBy_Nome(Guid ten_id, string ten_nome)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_TipoEntidade_SelectBy_Nome", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@ten_nome";
                Param.Size = 100;
                Param.Value = ten_nome;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@ten_id_alteracao";
                Param.Size = 16;
                if (ten_id != Guid.Empty)
                    Param.Value = ten_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();
                return (qs.Return.Rows.Count > 0);

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
        /// Parâmetros para efetuar a inclusão com data de criação e de alteração fixas
        /// </summary>
        protected override void ParamInserir(QuerySelectStoredProcedure qs, Autenticador.Entities.SYS_TipoEntidade entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@ten_nome";
            Param.Size = 100;
            Param.Value = entity.ten_nome;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@ten_situacao";
            Param.Size = 1;
            Param.Value = entity.ten_situacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@ten_dataCriacao";
            Param.Size = 16;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@ten_dataAlteracao";
            Param.Size = 16;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@ten_integridade";
            Param.Size = 4;
            Param.Value = entity.ten_integridade;
            qs.Parameters.Add(Param);
        }

        /// <summary>
        /// Parâmetros para efetuar a alteração preservando a data de criação e a integridade
        /// </summary>
        protected override void ParamAlterar(QueryStoredProcedure qs, Autenticador.Entities.SYS_TipoEntidade entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@ten_id";
            Param.Size = 16;
            Param.Value = entity.ten_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@ten_nome";
            Param.Size = 100;
            Param.Value = entity.ten_nome;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@ten_situacao";
            Param.Size = 1;
            Param.Value = entity.ten_situacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@ten_dataAlteracao";
            Param.Size = 8;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);
        }
        /// <summary>
        /// Méotodo de update alterado para que não modificasse o valor do campo data da criação;
        /// </summary>
        /// <param name="entity">Entidade com dados preenchidos</param>
        /// <returns>True para alteração realizado com sucesso.</returns>
        protected override bool Alterar(Autenticador.Entities.SYS_TipoEntidade entity)
        {
            this.__STP_UPDATE = "NEW_SYS_TipoEntidade_Update";
            return base.Alterar(entity);
        }

        protected override void ParamDeletar(QueryStoredProcedure qs, Autenticador.Entities.SYS_TipoEntidade entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@ten_id";
            Param.Size = 16;
            Param.Value = entity.ten_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@ten_situacao";
            Param.Size = 1;
            Param.Value = 3;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@ten_dataAlteracao";
            Param.Size = 8;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);
        }
        /// <summary>
        /// Método alterado para que o delete não faça exclusão física e sim lógica.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override bool Delete(Autenticador.Entities.SYS_TipoEntidade entity)
        {
            this.__STP_DELETE = "NEW_SYS_TipoEntidade_Update_Situacao";
            return base.Delete(entity);
        }

        /// <summary>
        /// Seleciona campo integridade do tipo de entidade
        /// </summary>
        /// <param name="ten_id">Id da tabela SYS_TipoEntidade do bd</param>
        /// <returns>true = sucesso | false = fracasso</returns>    
        public int Select_Integridade
        (
            Guid ten_id
        )
        {            
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_TipoEntidade_Select_Integridade", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@ten_id";
                Param.Size = 16;
                if (ten_id != Guid.Empty)
                    Param.Value = ten_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                if (qs.Return.Rows.Count > 0)
                    return Convert.ToInt32(qs.Return.Rows[0]["ten_integridade"].ToString());

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
        /// Incrementa uma unidade no campo integridade do tipo de entidade
        /// </summary>
        /// <param name="ten_id">Id da tabela SYS_TipoEntidade do bd</param>
        /// <returns>true = sucesso | false = fracasso</returns>    
        public bool Update_IncrementaIntegridade
        (
            Guid ten_id
        )
        {            
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_TipoEntidade_INCREMENTA_INTEGRIDADE", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@ten_id";
                Param.Size = 16;
                if (ten_id != Guid.Empty)
                    Param.Value = ten_id;
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
        /// Decrementa uma unidade no campo integridade do tipo de entidade
        /// </summary>
        /// <param name="ten_id">Id da tabela SYS_TipoEntidade do bd</param>
        /// <returns>true = sucesso | false = fracasso</returns>    
        public bool Update_DecrementaIntegridade
        (
            Guid ten_id
        )
        {            
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_TipoEntidade_DECREMENTA_INTEGRIDADE", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@ten_id";
                Param.Size = 16;
                if (ten_id != Guid.Empty)
                    Param.Value = ten_id;
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
    }
}
