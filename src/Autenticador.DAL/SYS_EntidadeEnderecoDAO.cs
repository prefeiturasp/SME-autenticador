using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autenticador.DAL.Abstracts;
using System.Data;
using CoreLibrary.Data.Common;

namespace Autenticador.DAL
{
    public class SYS_EntidadeEnderecoDAO : Abstract_SYS_EntidadeEnderecoDAO
    {
        /// <summary>
        /// Retorna os dados do endereço, cidade e estado dos endereços cadastrados para
        /// a entidade.
        /// </summary>
        /// <param name="ent_id">ID da entidade</param>
        /// <returns></returns>
        public DataTable SelectEnderecosBy_Entidade
        (
           Guid ent_id
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_EntidadeEndereco_SelectEnderecosBy_Entidade", this._Banco);
            
            #region PARAMETROS

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@ent_id";
            Param.Size = 16;
            Param.Value = ent_id;
            qs.Parameters.Add(Param);

            #endregion

            qs.Execute();

            return qs.Return;
        }


        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="ent_id">ID da entidade</param>
        /// <returns></returns>
        public DataTable CarregarEnderecosBy_Entidade(Guid ent_id)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_EntidadeEndereco_CarregaEnderecos_SelectBy_ent_id", this._Banco);

            #region PARAMETROS

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@ent_id";
            Param.Size = 16;
            Param.Value = ent_id;
            qs.Parameters.Add(Param);

            #endregion

            qs.Execute();

            return qs.Return;
        }


        public Guid SelectBy_ent_id_top_one
        (
           Guid ent_id
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_EntidadeEndereco_SelectBy_ent_id_top_one", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@ent_id";
                Param.Size = 16;
                if (ent_id != Guid.Empty)
                    Param.Value = ent_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                if (qs.Return.Rows.Count > 0)
                    return new Guid(qs.Return.Rows[0]["ene_id"].ToString());
                else
                    return Guid.Empty;
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
        protected override void ParamInserir(QuerySelectStoredProcedure qs, Autenticador.Entities.SYS_EntidadeEndereco entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@ent_id";
            Param.Size = 16;
            Param.Value = entity.ent_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@end_id";
            Param.Size = 16;
            Param.Value = entity.end_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@ene_numero";
            Param.Size = 20;
            Param.Value = entity.ene_numero;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@ene_complemento";
            Param.Size = 100;
            if (!string.IsNullOrEmpty(entity.ene_complemento))
                Param.Value = entity.ene_complemento;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@ene_situacao";
            Param.Size = 1;
            Param.Value = entity.ene_situacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@ene_dataCriacao";
            Param.Size = 16;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@ene_dataAlteracao";
            Param.Size = 16;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);


            //
            Param = qs.NewParameter();
            Param.DbType = DbType.Boolean;
            Param.ParameterName = "@ene_enderecoPrincipal";
            Param.Size = 1;
            Param.Value = entity.ene_enderecoPrincipal;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Decimal;
            Param.ParameterName = "@ene_latitude";
            Param.Size = 17;
            Param.Value = entity.ene_latitude;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Decimal;
            Param.ParameterName = "@ene_longitude";
            Param.Size = 17;
            Param.Value = entity.ene_longitude;
            qs.Parameters.Add(Param);

        }

        /// <summary>
        /// Parâmetros para efetuar a alteração preservando a data de criação
        /// </summary>
        protected override void ParamAlterar(QueryStoredProcedure qs, Autenticador.Entities.SYS_EntidadeEndereco entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@ent_id";
            Param.Size = 16;
            Param.Value = entity.ent_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@ene_id";
            Param.Size = 16;
            Param.Value = entity.ene_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@end_id";
            Param.Size = 16;
            Param.Value = entity.end_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@ene_numero";
            Param.Size = 20;
            Param.Value = entity.ene_numero;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@ene_complemento";
            Param.Size = 100;
            if (!string.IsNullOrEmpty(entity.ene_complemento))
                Param.Value = entity.ene_complemento;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@ene_situacao";
            Param.Size = 1;
            Param.Value = entity.ene_situacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@ene_dataAlteracao";
            Param.Size = 16;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);


            Param = qs.NewParameter();
            Param.DbType = DbType.Boolean;
            Param.ParameterName = "@ene_enderecoPrincipal";
            Param.Size = 1;
            Param.Value = entity.ene_enderecoPrincipal;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Decimal;
            Param.ParameterName = "@ene_latitude";
            Param.Size = 17;
            Param.Value = entity.ene_latitude;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Decimal;
            Param.ParameterName = "@ene_longitude";
            Param.Size = 17;
            Param.Value = entity.ene_longitude;
            qs.Parameters.Add(Param);
        }

        /// <summary>
        /// Método alterado para que o update não faça a alteração da data de criação
        /// </summary>
        /// <param name="entity"> Entidade SYS_EntidadeEndereco</param>
        /// <returns>true = sucesso | false = fracasso</returns>  
        protected override bool Alterar(Autenticador.Entities.SYS_EntidadeEndereco entity)
        {
            this.__STP_UPDATE = "NEW_SYS_EntidadeEndereco_UPDATE";
            return base.Alterar(entity);
        }

        /// <summary>
        /// Parâmetros para efetuar a exclusão lógica.
        /// </summary>
        protected override void ParamDeletar(QueryStoredProcedure qs, Autenticador.Entities.SYS_EntidadeEndereco entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@ent_id";
            Param.Size = 16;
            Param.Value = entity.ent_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@ene_id";
            Param.Size = 16;
            Param.Value = entity.ene_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@ene_situacao";
            Param.Size = 1;
            Param.Value = 3;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@ene_dataAlteracao";
            Param.Size = 8;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);
        }

        /// <summary>
        /// Método alterado para que o delete não faça exclusão física e sim lógica (update).
        /// </summary>
        /// <param name="entity"> Entidade SYS_EntidadeEndereco</param>
        /// <returns>true = sucesso | false = fracasso</returns>        
        public override bool Delete(Autenticador.Entities.SYS_EntidadeEndereco entity)
        {
            this.__STP_DELETE = "NEW_SYS_EntidadeEndereco_Update_Situacao";
            return base.Delete(entity);
        }  
    }
}
