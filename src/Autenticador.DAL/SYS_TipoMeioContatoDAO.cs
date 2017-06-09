using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CoreLibrary.Data.Common;
using Autenticador.DAL.Abstracts;

namespace Autenticador.DAL
{
    public class SYS_TipoMeioContatoDAO : Abstract_SYS_TipoMeioContatoDAO
    {
        /// <summary>
        /// Retorna um datatable contendo todos os tipos de meio de contato
        /// que não foram excluídos logicamente. Filtrado por pes_id.
        /// </summary>
        /// <param name="pes_id">Id da pessoa.</param>
        /// <returns>DataTable com tipos de meio de contato.</returns>
        public DataTable SelecionaContatosDaPessoa
        (
            Guid pes_id
        )
        {
            DataTable dt = new DataTable();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_TipoMeioContato_SelecionaContatosDaPessoa", this._Banco);
            try
            {
                #region Parâmetros

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@pes_id";
                Param.Size = 16;
                if (pes_id != Guid.Empty)
                    Param.Value = pes_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

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
        /// Retorna um datatable contendo todos os tipos de meio de contato
        /// que não foram excluídos logicamente, filtrados por 
        /// id, nome, situacao e paginado.        
        /// </summary>
        /// <param name="tmc_id">Id da tabela SYS_TipoMeioContato do bd</param>
        /// <param name="tmc_nome">Campo tmc_nome da tabela SYS_TipoMeioContato do bd</param>        
        /// <param name="tmc_situacao">Campo tmc_situcao da tabela SYS_TipoMeioContato do bd</param>
        /// <returns>DataTable com tipos de meio de contato paginado</returns>
        public DataTable SelectBy_All
        (
            Guid tmc_id
            , string tmc_nome
            , byte tmc_situacao
            , bool paginado
            , int currentPage
            , int pageSize
            , out int totalRecords
        )
        {
            //DataTable dt = new DataTable();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_TipoMeioContato_SelectBy_All", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@tmc_id";
                Param.Size = 16;
                if (tmc_id != Guid.Empty)
                    Param.Value = tmc_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@tmc_nome";
                Param.Size = 200;
                if (!string.IsNullOrEmpty(tmc_nome))
                    Param.Value = tmc_nome;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Byte;
                Param.ParameterName = "@tmc_situacao";
                Param.Size = 1;
                if (tmc_situacao > 0)
                    Param.Value = tmc_situacao;
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

                return qs.Return;
                //if (qs.Return.Rows.Count > 0)
                //    dt = qs.Return;

                //return dt;
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
        /// Retorno booleano na qual verifica se existe nome de Tipo de Meio de Contato 
        /// já existente no banco com situação Ativo ou Bloqueado.             
        /// </summary>
        /// <param name="tmc_id">ID do tipo de meio de contato</param>
        /// <param name="tmc_nome">Nome do tipo de meio de contato</param>
        /// <returns>DataTable com os tipos de entidades</returns>
        public bool SelectBy_Nome(Guid tmc_id, string tmc_nome)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_TipoMeioContato_SelectBy_Nome", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@tmc_nome";
                Param.Size = 100;
                Param.Value = tmc_nome;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@tmc_id_alteracao";
                Param.Size = 16;
                if (tmc_id != Guid.Empty)
                    Param.Value = tmc_id;
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
        /// Método que retorna o tipo de meio de contato através do nome
        /// e carrega a entidade.
        /// </summary>
        /// <param name="entity"> Entidade SYS_TipoMeioContato.</param>
        /// <returns> True = Se existir o tipo de contato | False = Não existe.</returns>
        public bool SelectBy_Nome(Autenticador.Entities.SYS_TipoMeioContato entity)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_TipoMeioContato_SelectBy_tmc_nome", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@tmc_nome";
                Param.Size = 100;
                Param.Value = entity.tmc_nome;
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

        /// <summary>
        /// Parâmetros para efetuar a inclusão com data de criação e de alteração fixas
        /// </summary>
        protected override void ParamInserir(QuerySelectStoredProcedure qs, Autenticador.Entities.SYS_TipoMeioContato entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@tmc_nome";
            Param.Size = 100;
            Param.Value = entity.tmc_nome;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@tmc_validacao";
            Param.Size = 1;
            if (entity.tmc_validacao > 0)
                Param.Value = entity.tmc_validacao;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@tmc_situacao";
            Param.Size = 1;
            Param.Value = entity.tmc_situacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@tmc_dataCriacao";
            Param.Size = 16;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@tmc_dataAlteracao";
            Param.Size = 16;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@tmc_integridade";
            Param.Size = 4;
            Param.Value = entity.tmc_integridade;
            qs.Parameters.Add(Param);
        }

        protected override void ParamAlterar(QueryStoredProcedure qs, Autenticador.Entities.SYS_TipoMeioContato entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@tmc_id";
            Param.Size = 16;
            Param.Value = entity.tmc_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@tmc_nome";
            Param.Size = 100;
            Param.Value = entity.tmc_nome;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@tmc_validacao";
            Param.Size = 1;
            if (entity.tmc_validacao > 0)
                Param.Value = entity.tmc_validacao;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@tmc_situacao";
            Param.Size = 1;
            Param.Value = entity.tmc_situacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@tmc_dataAlteracao";
            Param.Size = 8;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);
        }
        /// <summary>
        /// Méotodo de update alterado para que não modificasse o valor do campo data da criação;
        /// </summary>
        /// <param name="entity">Entidade com dados preenchidos</param>
        /// <returns>True para alteração realizado com sucesso.</returns>
        protected override bool Alterar(Autenticador.Entities.SYS_TipoMeioContato entity)
        {
            this.__STP_UPDATE = "NEW_SYS_TipoMeioContato_UPDATE";
            return base.Alterar(entity);
        }

        protected override void ParamDeletar(QueryStoredProcedure qs, Autenticador.Entities.SYS_TipoMeioContato entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@tmc_id";
            Param.Size = 16;
            Param.Value = entity.tmc_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@tmc_situacao";
            Param.Size = 1;
            Param.Value = 3;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@tmc_dataAlteracao";
            Param.Size = 8;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);
        }

        /// <summary>
        /// Método alterado para que o delete não faça exclusão física e sim lógica.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override bool Delete(Autenticador.Entities.SYS_TipoMeioContato entity)
        {
            this.__STP_DELETE = "NEW_SYS_TipoMeioContato_UPDATE_Situacao";
            return base.Delete(entity);
        }

        /// <summary>
        /// Seleciona campo integridade do tipo de meio de contato
        /// </summary>
        /// <param name="tmc_id">Id da tabela SYS_TipoMeioContato do bd</param>
        /// <returns>true = sucesso | false = fracasso</returns>    
        public int Select_Integridade
        (
            Guid tmc_id
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_TipoMeioContato_Select_Integridade", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@tmc_id";
                Param.Size = 16;
                if (tmc_id != Guid.Empty)
                    Param.Value = tmc_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                if (qs.Return.Rows.Count > 0)
                    return Convert.ToInt32(qs.Return.Rows[0]["tmc_integridade"].ToString());

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
        /// Incrementa uma unidade no campo integridade do tipo de meio de contato
        /// </summary>
        /// <param name="tmc_id">Id da tabela SYS_TipoMeioContato do bd</param>
        /// <returns>true = sucesso | false = fracasso</returns>    
        public bool Update_IncrementaIntegridade
        (
            Guid tmc_id
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_TipoMeioContato_INCREMENTA_INTEGRIDADE", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@tmc_id";
                Param.Size = 16;
                if (tmc_id != Guid.Empty)
                    Param.Value = tmc_id;
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
        /// Decrementa uma unidade no campo integridade do tipo de meio de contato
        /// </summary>
        /// <param name="tmc_id">Id da tabela SYS_TipoMeioContato do bd</param>
        /// <returns>true = sucesso | false = fracasso</returns>    
        public bool Update_DecrementaIntegridade
        (
            Guid tmc_id
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_TipoMeioContato_DECREMENTA_INTEGRIDADE", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@tmc_id";
                Param.Size = 16;
                if (tmc_id != Guid.Empty)
                    Param.Value = tmc_id;
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
