using CoreLibrary.Data.Common;
using Autenticador.DAL.Abstracts;
using System;
using System.Data;

namespace Autenticador.DAL
{
    public class SYS_TipoDocumentacaoDAO : Abstract_SYS_TipoDocumentacaoDAO
    {
        public DataTable SelectBy_All
       (
           Guid tdo_id
           , string tdo_nome
           , string tdo_sigla
           , byte tdo_situacao
           , bool paginado
           , int currentPage
           , int pageSize
           , out int totalRecords
       )
        {
            //DataTable dt = new DataTable();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_TipoDocumentacao_SelectBy_All", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@tdo_id";
                Param.Size = 16;
                if (tdo_id != Guid.Empty)
                    Param.Value = tdo_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@tdo_nome";
                Param.Size = 100;
                if (!string.IsNullOrEmpty(tdo_nome))
                    Param.Value = tdo_nome;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@tdo_sigla";
                Param.Size = 10;
                if (!string.IsNullOrEmpty(tdo_sigla))
                    Param.Value = tdo_sigla;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Byte;
                Param.ParameterName = "@tdo_situacao";
                Param.Size = 1;
                if (tdo_situacao > 0)
                    Param.Value = tdo_situacao;
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

        public bool SelectBy_Nome(string tdo_nome, Guid tdo_id)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_TipoDocumentacao_SelectBy_Nome", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@tdo_nome";
                Param.Size = 100;
                if (!string.IsNullOrEmpty(tdo_nome))
                    Param.Value = tdo_nome;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@tdo_id";
                Param.Size = 16;
                if (tdo_id != Guid.Empty)
                    Param.Value = tdo_id;
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

        public bool SelectBy_Classificacao(byte? tdo_classificacao, Guid tdo_id)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_TipoDocumentacao_SELECTBy_Classificacao", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@tdo_classificacao";
                Param.Size = 100;
                if (tdo_classificacao != null)
                    Param.Value = tdo_classificacao;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@tdo_id";
                Param.Size = 16;
                if (tdo_id != Guid.Empty)
                    Param.Value = tdo_id;
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

        public int Select_Integridade(Guid tdo_id)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_TipoDocumentacao_Select_Integridade", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@tdo_id";
                Param.Size = 16;
                if (tdo_id != Guid.Empty)
                    Param.Value = tdo_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

                qs.Execute();

                if (qs.Return.Rows.Count > 0)
                    return Convert.ToInt32(qs.Return.Rows[0]["tdo_integridade"].ToString());

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
        /// Incrementa uma unidade no campo integridade da tabela de documentacao
        /// </summary>
        /// <param name="ent_id">Id da tabela sys_tipodocumentacao do bd</param>
        /// <returns>true = sucesso | false = fracasso</returns>
        public bool Update_IncrementaIntegridade(Guid tdo_id)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_TipoDocumentacao_INCREMENTA_INTEGRIDADE", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@tdo_id";
                Param.Size = 16;
                if (tdo_id != Guid.Empty)
                    Param.Value = tdo_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

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
        /// Decrementa uma unidade no campo integridade da tabela de tipo documentacao
        /// </summary>
        /// <param name="ent_id">Id da tabela sys_tipoDocumentacao do bd</param>
        /// <returns>true = sucesso | false = fracasso</returns>
        public bool Update_DecrementaIntegridade(Guid tdo_id)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_TipoDocumentacao_DECREMENTA_INTEGRIDADE", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@tdo_id";
                Param.Size = 16;
                if (tdo_id != Guid.Empty)
                    Param.Value = tdo_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

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
        /// Parâmetros para efetuar a inclusão com data de criação e de alteração fixas
        /// </summary>
        protected override void ParamInserir(QuerySelectStoredProcedure qs, Autenticador.Entities.SYS_TipoDocumentacao entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@tdo_nome";
            Param.Size = 100;
            Param.Value = entity.tdo_nome;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@tdo_sigla";
            Param.Size = 10;
            if (!string.IsNullOrEmpty(entity.tdo_sigla))
                Param.Value = entity.tdo_sigla;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@tdo_validacao";
            Param.Size = 1;
            if (entity.tdo_validacao > 0)
                Param.Value = entity.tdo_validacao;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@tdo_situacao";
            Param.Size = 1;
            Param.Value = entity.tdo_situacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@tdo_dataCriacao";
            Param.Size = 16;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@tdo_dataAlteracao";
            Param.Size = 16;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@tdo_integridade";
            Param.Size = 4;
            Param.Value = entity.tdo_integridade;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@tdo_classificacao";
            Param.Size = 1;
            if (entity.tdo_classificacao > 0)
                Param.Value = entity.tdo_classificacao;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.String;
            Param.ParameterName = "@tdo_atributos";
            Param.Size = 1024;
            if (!string.IsNullOrEmpty(entity.tdo_atributos))
                Param.Value = entity.tdo_atributos;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);
        }

        protected override bool Inserir(Autenticador.Entities.SYS_TipoDocumentacao entity)
        {
            this.__STP_INSERT = "NEW_SYS_TipoDocumentacao_INSERT";
            return base.Inserir(entity);
        }

        /// <summary>
        /// Parâmetros para efetuar a alteração preservando a data de criação e a integridade
        /// </summary>
        protected override void ParamAlterar(QueryStoredProcedure qs, Autenticador.Entities.SYS_TipoDocumentacao entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@tdo_id";
            Param.Size = 16;
            Param.Value = entity.tdo_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@tdo_nome";
            Param.Size = 100;
            Param.Value = entity.tdo_nome;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@tdo_sigla";
            Param.Size = 10;
            if (!string.IsNullOrEmpty(entity.tdo_sigla))
                Param.Value = entity.tdo_sigla;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@tdo_validacao";
            Param.Size = 1;
            if (entity.tdo_validacao > 0)
                Param.Value = entity.tdo_validacao;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@tdo_situacao";
            Param.Size = 1;
            Param.Value = entity.tdo_situacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@tdo_dataAlteracao";
            Param.Size = 8;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@tdo_classificacao";
            Param.Size = 1;
            if (entity.tdo_classificacao > 0)
                Param.Value = entity.tdo_classificacao;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.String;
            Param.ParameterName = "@tdo_atributos";
            Param.Size = 1024;
            if (!string.IsNullOrEmpty(entity.tdo_atributos))
                Param.Value = entity.tdo_atributos;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);
        }

        protected override bool Alterar(Autenticador.Entities.SYS_TipoDocumentacao entity)
        {
            this.__STP_UPDATE = "NEW_SYS_TipoDocumentacao_UPDATE";
            return base.Alterar(entity);
        }

        protected override void ParamDeletar(QueryStoredProcedure qs, Autenticador.Entities.SYS_TipoDocumentacao entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@tdo_id";
            Param.Size = 16;
            Param.Value = entity.tdo_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@tdo_situacao";
            Param.Size = 1;
            Param.Value = 3;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@tdo_dataAlteracao";
            Param.Size = 8;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);
        }

        public override bool Delete(Autenticador.Entities.SYS_TipoDocumentacao entity)
        {
            this.__STP_DELETE = "NEW_SYS_TipoDocumentacao_Update_Situacao";
            return base.Delete(entity);
        }
    }
}