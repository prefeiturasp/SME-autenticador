using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CoreLibrary.Data.Common;
using Autenticador.DAL.Abstracts;

namespace Autenticador.DAL
{
    public class PES_PessoaContatoDAO : Abstract_PES_PessoaContatoDAO
    {
        /// <summary>
        /// Retorna um datatable contendo todos os contatos da pessoa
        /// que não foram excluídos logicamente, filtrados por 
        /// id da pessoa
        /// </summary>
        /// <param name="pes_id">Id da tabela PES_Pessoa do bd</param>
        /// <param name="paginado">Indica se o datatable será paginado ou não</param>
        /// <param name="currentPage">Página atual do grid</param>
        /// <param name="pageSize">Total de registros por página do grid</param>
        /// <param name="totalRecord">Total de registros retornado na busca</param>
        /// <returns>DataTable com os contatos da pessoa</returns>
        public DataTable SelectBy_pes_id
        (
            Guid pes_id
            , bool paginado
            , int currentPage
            , int pageSize
            , out int totalRecords
        )
        {
            DataTable dt = new DataTable();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_PES_PessoaContato_SelectBy_pes_id", this._Banco);
            try
            {
                #region PARAMETROS

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

                if (paginado)
                    totalRecords = qs.Execute(currentPage, pageSize);
                else
                {
                    qs.Execute();
                    totalRecords = qs.Return.Rows.Count;
                }

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
        /// Retorna um datatable de contatos da pessoa de acordo com o tipo_meio_contato e o pes_id
        /// </summary>
        public DataTable SelectBy_pes_id_tmc_id
        (
            Guid pes_id
            , Guid tmc_id
        )
        {
            DataTable dt = new DataTable();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_PES_PessoaContato_SelectBy_pes_id_tmc_id", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@pes_id";
                Param.Size = 16;
                if (pes_id != Guid.Empty)
                    Param.Value = pes_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@tmc_id";
                Param.Size = 16;
                Param.Value = tmc_id;
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
        /// Parâmetros para efetuar a inclusão com data de criação e de alteração fixas
        /// </summary>
        protected override void ParamInserir(QuerySelectStoredProcedure qs, Autenticador.Entities.PES_PessoaContato entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@pes_id";
            Param.Size = 16;
            Param.Value = entity.pes_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@tmc_id";
            Param.Size = 16;
            Param.Value = entity.tmc_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psc_contato";
            Param.Size = 200;
            Param.Value = entity.psc_contato;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@psc_situacao";
            Param.Size = 1;
            Param.Value = entity.psc_situacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@psc_dataCriacao";
            Param.Size = 16;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@psc_dataAlteracao";
            Param.Size = 16;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);
        }

        /// <summary>
        /// Parâmetros para efetuar a alteração preservando a data de criação
        /// </summary>
        protected override void ParamAlterar(QueryStoredProcedure qs, Autenticador.Entities.PES_PessoaContato entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@pes_id";
            Param.Size = 16;
            Param.Value = entity.pes_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@psc_id";
            Param.Size = 16;
            Param.Value = entity.psc_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@tmc_id";
            Param.Size = 16;
            Param.Value = entity.tmc_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psc_contato";
            Param.Size = 200;
            Param.Value = entity.psc_contato;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@psc_situacao";
            Param.Size = 1;
            Param.Value = entity.psc_situacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@psc_dataAlteracao";
            Param.Size = 16;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);
        }

        /// <summary>
        /// Método alterado para que o update não faça a alteração da data de criação
        /// </summary>
        /// <param name="entity"> Entidade PES_PessoaContato</param>
        /// <returns>true = sucesso | false = fracasso</returns>  
        protected override bool Alterar(Autenticador.Entities.PES_PessoaContato entity)
        {
            this.__STP_UPDATE = "NEW_PES_PessoaContato_UPDATE";
            return base.Alterar(entity);
        }

        /// <summary>
        /// Parâmetros para efetuar a exclusão lógica.
        /// </summary>
        protected override void ParamDeletar(QueryStoredProcedure qs, Autenticador.Entities.PES_PessoaContato entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@pes_id";
            Param.Size = 16;
            Param.Value = entity.pes_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@psc_id";
            Param.Size = 16;
            Param.Value = entity.psc_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@psc_situacao";
            Param.Size = 1;
            Param.Value = 3;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@psc_dataAlteracao";
            Param.Size = 8;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);
        }

        /// <summary>
        /// Método alterado para que o delete não faça exclusão física e sim lógica (update).
        /// </summary>
        /// <param name="entity"> Entidade PES_PessoaContato</param>
        /// <returns>true = sucesso | false = fracasso</returns>        
        public override bool Delete(Autenticador.Entities.PES_PessoaContato entity)
        {
            this.__STP_DELETE = "NEW_PES_PessoaContato_Update_Situacao";
            return base.Delete(entity);
        }
    }
}
