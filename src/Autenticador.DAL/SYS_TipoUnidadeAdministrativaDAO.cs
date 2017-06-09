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
    public class SYS_TipoUnidadeAdministrativaDAO : Abstract_SYS_TipoUnidadeAdministrativaDAO
    {
        /// <summary>
        /// Retorna um datatable contendo todos os tipos de entidade
        /// que não foram excluídos logicamente, filtrados por 
        /// id, nome, situacao e paginado.        
        /// </summary>
        /// <param name="ten_id">Id da tabela SYS_TipoUnidadeAdministrativa do bd</param>
        /// <param name="ten_nome">Campo ten_nome da tabela SYS_TipoUnidadeAdministrativa do bd</param>        
        /// <param name="ten_situacao">Campo ten_situcao da tabela SYS_TipoUnidadeAdministrativa do bd</param>
        /// <returns>DataTable com tipos de entidade paginado</returns>
        public DataTable SelectBy_All
        (
            Guid tua_id
            , string tua_nome
            , byte tua_situacao
            , bool paginado
            , int currentPage
            , int pageSize
            , out int totalRecords
        )
        {
            //DataTable dt = new DataTable();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_TipoUnidadeAdministrativa_SelectBy_All", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@tua_id";
                Param.Size = 16;
                if (tua_id != Guid.Empty)
                    Param.Value = tua_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@tua_nome";
                Param.Size = 200;
                if (!string.IsNullOrEmpty(tua_nome))
                    Param.Value = tua_nome;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Byte;
                Param.ParameterName = "@tua_situacao";
                Param.Size = 1;
                if (tua_situacao > 0)
                    Param.Value = tua_situacao;
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
        /// Retorna o tipo de unidade administrativa apartir do nome
        /// </summary>
        /// <param name="entity">Entidade SYS_TipoUnidadeAdministrativa(contendo uad_nome)</param>
        /// <returns></returns>
        public bool SelectBy_Nome(SYS_TipoUnidadeAdministrativa entity)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_TipoUnidadeAdministrativa_SelectBy_Nome", _Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@tua_nome";
                Param.Size = 100;
                Param.Value = entity.tua_nome;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@tua_id_alteracao";
                Param.Size = 16;
                if (entity.tua_id != Guid.Empty)
                    Param.Value = entity.tua_id;
                else
                    Param.Value = DBNull.Value;
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
            finally
            {
                qs.Parameters.Clear();
            }
        }

        /// <summary>
        /// Retorno booleano na qual verifica se existe nome de Tipo de Unidade Administrativa 
        /// já existente no banco com situação Ativo ou Bloqueado.             
        /// </summary>
        /// <param name="tua_id">ID do tipo de unidade administrativa</param>
        /// <param name="tua_nome">Nome do tipo de unidade administrativa</param>
        /// <returns>DataTable com os tipos de entidades</returns>
        public bool SelectBy_Nome(Guid tua_id, string tua_nome)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_TipoUnidadeAdministrativa_SelectBy_Nome", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@tua_nome";
                Param.Size = 100;
                Param.Value = tua_nome;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@tua_id_alteracao";
                Param.Size = 16;
                if (tua_id != Guid.Empty)
                    Param.Value = tua_id;
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
        protected override void ParamInserir(QuerySelectStoredProcedure qs, Autenticador.Entities.SYS_TipoUnidadeAdministrativa entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@tua_nome";
            Param.Size = 100;
            Param.Value = entity.tua_nome;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@tua_situacao";
            Param.Size = 1;
            Param.Value = entity.tua_situacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@tua_dataCriacao";
            Param.Size = 16;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@tua_dataAlteracao";
            Param.Size = 16;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@tua_integridade";
            Param.Size = 4;
            Param.Value = entity.tua_integridade;
            qs.Parameters.Add(Param);
        }

        protected override void ParamAlterar(QueryStoredProcedure qs, Autenticador.Entities.SYS_TipoUnidadeAdministrativa entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@tua_id";
            Param.Size = 16;
            Param.Value = entity.tua_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@tua_nome";
            Param.Size = 100;
            Param.Value = entity.tua_nome;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@tua_situacao";
            Param.Size = 1;
            Param.Value = entity.tua_situacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@tua_dataAlteracao";
            Param.Size = 8;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);
        }

        /// <summary>
        /// Méotodo de update alterado para que não modificasse o valor do campo data da criação;
        /// </summary>
        /// <param name="entity">Entidade com dados preenchidos</param>
        /// <returns>True para alteração realizado com sucesso.</returns>
        protected override bool Alterar(Autenticador.Entities.SYS_TipoUnidadeAdministrativa entity)
        {
            this.__STP_UPDATE = "NEW_SYS_TipoUnidadeAdministrativa_UPDATE";
            return base.Alterar(entity);
        }

        protected override void ParamDeletar(QueryStoredProcedure qs, Autenticador.Entities.SYS_TipoUnidadeAdministrativa entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@tua_id";
            Param.Size = 16;
            Param.Value = entity.tua_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@tua_situacao";
            Param.Size = 1;
            Param.Value = 3;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@tua_dataAlteracao";
            Param.Size = 8;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);
        }

        /// <summary>
        /// Método alterado para que o delete não faça exclusão física e sim lógica.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override bool Delete(Autenticador.Entities.SYS_TipoUnidadeAdministrativa entity)
        {
            this.__STP_DELETE = "NEW_SYS_TipoUnidadeAdministrativa_UPDATE_Situacao";
            return base.Delete(entity);
        }

        /// <summary>
        /// Seleciona campo integridade do tipo de unidade administrativa
        /// </summary>
        /// <param name="tua_id">Id da tabela SYS_TipoUnidadeAdministrativa do bd</param>
        /// <returns>true = sucesso | false = fracasso</returns>    
        public int Select_Integridade
        (
            Guid tua_id
        )
        {            
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_TipoUnidadeAdministrativa_Select_Integridade", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@tua_id";
                Param.Size = 16;
                if (tua_id != Guid.Empty)
                    Param.Value = tua_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                if (qs.Return.Rows.Count > 0)
                    return Convert.ToInt32(qs.Return.Rows[0]["tua_integridade"].ToString());

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
        /// Incrementa uma unidade no campo integridade do tipo de unidade administrativa
        /// </summary>
        /// <param name="tua_id">Id da tabela SYS_TipoUnidadeAdministrativa do bd</param>
        /// <returns>true = sucesso | false = fracasso</returns>    
        public bool Update_IncrementaIntegridade
        (
            Guid tua_id
        )
        {            
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_TipoUnidadeAdministrativa_INCREMENTA_INTEGRIDADE", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@tua_id";
                Param.Size = 16;
                if (tua_id != Guid.Empty)
                    Param.Value = tua_id;
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
        /// Decrementa uma unidade no campo integridade do tipo de unidade administrativa
        /// </summary>
        /// <param name="ten_id">Id da tabela SYS_TipoEntidade do bd</param>
        /// <returns>true = sucesso | false = fracasso</returns>    
        public bool Update_DecrementaIntegridade
        (
            Guid tua_id
        )
        {            
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_TipoUnidadeAdministrativa_DECREMENTA_INTEGRIDADE", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@tua_id";
                Param.Size = 16;
                if (tua_id != Guid.Empty)
                    Param.Value = tua_id;
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
