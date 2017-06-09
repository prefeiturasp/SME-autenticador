using System;
using System.Data;
using Autenticador.DAL.Abstracts;
using CoreLibrary.Data.Common;

namespace Autenticador.DAL
{
    public class PES_TipoDeficienciaDAO : Abstract_PES_TipoDeficienciaDAO
    {
        public DataTable SelectBy_All
        (
             Guid tde_id
             , string tde_nome
             , byte tde_situacao
             , bool paginado
             , int currentPage
             , int pageSize
             , out int totalRecords
        )
        {
            //DataTable dt = new DataTable();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_PES_TipoDeficiencia_SelectBy_All", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@tde_id";
                Param.Size = 16;
                if (tde_id != Guid.Empty)
                    Param.Value = tde_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@tde_nome";
                Param.Size = 100;
                if (!string.IsNullOrEmpty(tde_nome))
                    Param.Value = tde_nome;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Byte;
                Param.ParameterName = "@tde_situacao";
                Param.Size = 1;
                if (tde_situacao > 0)
                    Param.Value = tde_situacao;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

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

        public bool SelectBy_Nome(string tde_nome, Guid tde_id)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_PES_TipoDeficiencia_SelectBy_Nome", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@tde_nome";
                Param.Size = 100;
                if (!string.IsNullOrEmpty(tde_nome))
                    Param.Value = tde_nome;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@tde_id";
                Param.Size = 16;
                if (tde_id != Guid.Empty)
                    Param.Value = tde_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

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
        protected override void ParamInserir(QuerySelectStoredProcedure qs, Autenticador.Entities.PES_TipoDeficiencia entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@tde_nome";
            Param.Size = 100;
            Param.Value = entity.tde_nome;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@tde_situacao";
            Param.Size = 1;
            Param.Value = entity.tde_situacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@tde_dataCriacao";
            Param.Size = 16;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@tde_dataAlteracao";
            Param.Size = 16;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@tde_integridade";
            Param.Size = 4;
            Param.Value = entity.tde_integridade;
            qs.Parameters.Add(Param);
        }

        /// <summary>
        /// Parâmetros para efetuar a alteração preservando a data de criação e a integridade
        /// </summary>
        protected override void ParamAlterar(QueryStoredProcedure qs, Autenticador.Entities.PES_TipoDeficiencia entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@tde_id";
            Param.Size = 16;
            Param.Value = entity.tde_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@tde_nome";
            Param.Size = 100;
            Param.Value = entity.tde_nome;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@tde_situacao";
            Param.Size = 1;
            Param.Value = entity.tde_situacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@tde_dataAlteracao";
            Param.Size = 8;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);
        }

        protected override bool Alterar(Autenticador.Entities.PES_TipoDeficiencia entity)
        {
            this.__STP_UPDATE = "NEW_PES_TipoDeficiencia_UPDATE";
            return base.Alterar(entity);
        }

        protected override void ParamDeletar(QueryStoredProcedure qs, Autenticador.Entities.PES_TipoDeficiencia entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@tde_id";
            Param.Size = 16;
            Param.Value = entity.tde_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@tde_situacao";
            Param.Size = 1;
            Param.Value = 3;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@tde_dataAlteracao";
            Param.Size = 8;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);
        }

        public override bool Delete(Autenticador.Entities.PES_TipoDeficiencia entity)
        {
            this.__STP_DELETE = "NEW_PES_TipoDeficiencia_Update_Situacao";
            return base.Delete(entity);
        }

        public int Select_Integridade(Guid tde_id)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_PES_TipoDeficiencia_Select_Integridade", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@tde_id";
                Param.Size = 16;
                if (tde_id != Guid.Empty)
                    Param.Value = tde_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

                qs.Execute();

                return Convert.ToInt32(qs.Return.Rows[0][0]);
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

        public void Update_IncrementaIntegridade(Guid tde_id)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_PES_TipoDeficiencia_INCREMENTA_INTEGRIDADE", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@tde_id";
                Param.Size = 16;
                if (tde_id != Guid.Empty)
                    Param.Value = tde_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

                qs.Execute();
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

        public void Update_DecrementaIntegridade(Guid tde_id)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_PES_TipoDeficiencia_DECREMENTA_INTEGRIDADE", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@tde_id";
                Param.Size = 16;
                if (tde_id != Guid.Empty)
                    Param.Value = tde_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

                qs.Execute();
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