using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autenticador.DAL.Abstracts;
using System.Data;
using CoreLibrary.Data.Common;

namespace Autenticador.DAL
{
    public class PES_PessoaEnderecoDAO : Abstract_PES_PessoaEnderecoDAO
    {
        /// <summary>
        /// Retorna um datatable contendo todos os endereços da pessoa
        /// que não foram excluídos logicamente, filtrados por 
        /// id da pessoa
        /// </summary>
        /// <param name="pes_id">Id da tabela SYS_Entidade do bd</param>        
        /// <param name="paginado">Indica se o datatable será paginado ou não</param>
        /// <param name="currentPage">Página atual do grid</param>
        /// <param name="pageSize">Total de registros por página do grid</param>
        /// <param name="totalRecord">Total de registros retornado na busca</param>
        /// <returns>DataTable com os endereços da pessoa</returns>
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
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_PES_PessoaEndereco_SelectBy_pes_id", this._Banco);
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
        /// Carrega a Pes_PessoaEndereco
        /// </summary>
        /// <param name="pes_id">Id da pessoa a carregar os endereços</param>
        /// <returns>DataTable</returns>
        public DataTable CarregaEnderecos_Pessoa(Guid pes_id)
        {
            DataTable dt = new DataTable();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_PES_PessoaEndereco_CarregaEnderecos_SelectBy_pes_id", this._Banco);
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


                qs.Execute();


            //    if (qs.Return.Rows.Count > 0)
                    return qs.Return;

              //  return dt;
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
        protected override void ParamInserir(QuerySelectStoredProcedure qs, Autenticador.Entities.PES_PessoaEndereco entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@pes_id";
            Param.Size = 16;
            Param.Value = entity.pes_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@end_id";
            Param.Size = 16;
            Param.Value = entity.end_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@pse_numero";
            Param.Size = 20;
            Param.Value = entity.pse_numero;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@pse_complemento";
            Param.Size = 100;
            if (!string.IsNullOrEmpty(entity.pse_complemento))
                Param.Value = entity.pse_complemento;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@pse_situacao";
            Param.Size = 1;
            Param.Value = entity.pse_situacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@pse_dataCriacao";
            Param.Size = 16;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@pse_dataAlteracao";
            Param.Size = 16;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);

            
            Param = qs.NewParameter();
            Param.DbType = DbType.Boolean;
            Param.ParameterName = "@pse_enderecoPrincipal";
            Param.Size = 1;
            Param.Value = entity.pse_enderecoPrincipal;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Decimal;
            Param.ParameterName = "@pse_latitude";
            Param.Size = 17;
            Param.Value = entity.pse_latitude;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Decimal;
            Param.ParameterName = "@pse_longitude";
            Param.Size = 17;
            Param.Value = entity.pse_longitude;
            qs.Parameters.Add(Param);
        }

        /// <summary>
        /// Parâmetros para efetuar a alteração preservando a data de criação
        /// </summary>
        protected override void ParamAlterar(QueryStoredProcedure qs, Autenticador.Entities.PES_PessoaEndereco entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@pes_id";
            Param.Size = 16;
            Param.Value = entity.pes_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@pse_id";
            Param.Size = 16;
            Param.Value = entity.pse_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@end_id";
            Param.Size = 16;
            Param.Value = entity.end_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@pse_numero";
            Param.Size = 20;
            Param.Value = entity.pse_numero;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@pse_complemento";
            Param.Size = 100;
            if (!string.IsNullOrEmpty(entity.pse_complemento))
                Param.Value = entity.pse_complemento;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@pse_situacao";
            Param.Size = 1;
            Param.Value = entity.pse_situacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@pse_dataAlteracao";
            Param.Size = 16;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);


           
            Param = qs.NewParameter();
            Param.DbType = DbType.Boolean;
            Param.ParameterName = "@pse_enderecoPrincipal";
            Param.Size = 1;
            Param.Value = entity.pse_enderecoPrincipal;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Decimal;
            Param.ParameterName = "@pse_latitude";
            Param.Size = 17;
            Param.Value = entity.pse_latitude;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Decimal;
            Param.ParameterName = "@pse_longitude";
            Param.Size = 17;
            Param.Value = entity.pse_longitude;
            qs.Parameters.Add(Param);
        }

        /// <summary>
        /// Método alterado para que o update não faça a alteração da data de criação
        /// </summary>
        /// <param name="entity"> Entidade PES_PessoaEndereco</param>
        /// <returns>true = sucesso | false = fracasso</returns>  
        protected override bool Alterar(Autenticador.Entities.PES_PessoaEndereco entity)
        {
            this.__STP_UPDATE = "NEW_PES_PessoaEndereco_UPDATE";
            return base.Alterar(entity);
        }

        /// <summary>
        /// Parâmetros para efetuar a exclusão lógica.
        /// </summary>
        protected override void ParamDeletar(QueryStoredProcedure qs, Autenticador.Entities.PES_PessoaEndereco entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@pes_id";
            Param.Size = 16;
            Param.Value = entity.pes_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@pse_id";
            Param.Size = 16;
            Param.Value = entity.pse_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@pse_situacao";
            Param.Size = 1;
            Param.Value = 3;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@pse_dataAlteracao";
            Param.Size = 8;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);
        }

        /// <summary>
        /// Método alterado para que o delete não faça exclusão física e sim lógica (update).
        /// </summary>
        /// <param name="entity"> Entidade PES_PessoaEndereco</param>
        /// <returns>true = sucesso | false = fracasso</returns>        
        public override bool Delete(Autenticador.Entities.PES_PessoaEndereco entity)
        {
            this.__STP_DELETE = "NEW_PES_PessoaEndereco_Update_Situacao";
            return base.Delete(entity);
        }
    }
}
