using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autenticador.DAL.Abstracts;
using System.Data;
using CoreLibrary.Data.Common;
using Autenticador.Entities;

namespace Autenticador.DAL
{
    public class SYS_UnidadeAdministrativaEnderecoDAO : Abstract_SYS_UnidadeAdministrativaEnderecoDAO
    {
        public Guid SelectBy_ent_id_uad_id_top_one
        (
           Guid ent_id
           , Guid uad_id
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_UnidadeAdministrativaEndereco_SelectBy_ent_id_uad_it_top_one", this._Banco);
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

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@uad_id";
                Param.Size = 16;
                if (uad_id != Guid.Empty)
                    Param.Value = uad_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                if (qs.Return.Rows.Count > 0)
                    return new Guid(qs.Return.Rows[0]["uae_id"].ToString());
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
        /// Seleciona o endereço de uma unidade administrativa.
        /// </summary>
        /// <param name="ent_id">ID da entidade da unidade administrativa.</param>
        /// <param name="uad_id">ID da unidade administrativa.</param>
        /// <returns></returns>
        public DataTable SelecionaEndereco(Guid ent_id, Guid uad_id)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_UnidadeAdministrativaEndereco_SelecionaEndereco", _Banco);

            try
            {
                #region Parâmetros

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@ent_id";
                Param.Size = 16;
                Param.Value = ent_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@uad_id";
                Param.Size = 16;
                Param.Value = uad_id;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                return qs.Return;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        /// <summary>
        /// Carrega o endereço de uma unidade administrativa.
        /// </summary>
        /// <param name="ent_id">ID da entidade da unidade administrativa.</param>
        /// <param name="uad_id">ID da unidade administrativa.</param>
        /// <returns>DT</returns>
        public DataTable CarregaEnderecos(Guid ent_id, Guid uad_id)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_UnidadeAdministrativaEndereco_CarregaEndereco", _Banco);

            try
            {
                #region Parâmetros

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@ent_id";
                Param.Size = 16;
                Param.Value = ent_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@uad_id";
                Param.Size = 16;
                Param.Value = uad_id;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                return qs.Return;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }


        public Guid SelectUAE(Guid ent_id, Guid uad_id, Guid uae_id)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_UnidadeAdministrativaEndereco_SelectBy_ent_id_uad_id_uae_id", _Banco);

            try
            {
                #region Parâmetros

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@ent_id";
                Param.Size = 16;
                Param.Value = ent_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@uad_id";
                Param.Size = 16;
                Param.Value = uad_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@uae_id";
                Param.Size = 16;
                Param.Value = uae_id;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                if (qs.Return.Rows.Count > 0)
                    return new Guid(qs.Return.Rows[0]["uae_id"].ToString());
                else
                    return Guid.Empty;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }


        /// <summary>
        /// Parâmetros para efetuar a inclusão com data de criação e de alteração fixas
        /// </summary>
        protected override void ParamInserir(QuerySelectStoredProcedure qs, Autenticador.Entities.SYS_UnidadeAdministrativaEndereco entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@ent_id";
            Param.Size = 16;
            Param.Value = entity.ent_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@uad_id";
            Param.Size = 16;
            Param.Value = entity.uad_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@uae_id";
            Param.Size = 16;
            Param.Value = entity.uae_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@end_id";
            Param.Size = 16;
            Param.Value = entity.end_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@uae_numero";
            Param.Size = 20;
            Param.Value = entity.uae_numero;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@uae_complemento";
            Param.Size = 100;
            if (!string.IsNullOrEmpty(entity.uae_complemento))
                Param.Value = entity.uae_complemento;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@uae_situacao";
            Param.Size = 1;
            Param.Value = entity.uae_situacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@uae_dataCriacao";
            Param.Size = 16;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@uae_dataAlteracao";
            Param.Size = 16;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);
            /*
                        Param = qs.NewParameter();
                        Param.DbType = DbType.Decimal;
                        Param.ParameterName = "@uae_latitude";
                        Param.Size = 100;
                        Param.Value = entity.uae_latitude;
                        qs.Parameters.Add(Param);

                        Param = qs.NewParameter();
                        Param.DbType = DbType.Decimal;
                        Param.ParameterName = "@uae_longitude";
                        Param.Size = 100;
                        Param.Value = entity.uae_longitude;
                        qs.Parameters.Add(Param);
            */
            Param = qs.NewParameter();
            Param.DbType = DbType.Decimal;
            Param.ParameterName = "@uae_latitude";
            Param.Size = 100;
            if (entity.uae_latitude == 0)
                Param.Value = DBNull.Value;
            else
                Param.Value = entity.uae_latitude;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Decimal;
            Param.ParameterName = "@uae_longitude";
            Param.Size = 100;
            if (entity.uae_longitude == 0)
                Param.Value = DBNull.Value;
            else
                Param.Value = entity.uae_longitude;

            qs.Parameters.Add(Param);
            Param = qs.NewParameter();
            Param.DbType = DbType.Boolean;
            Param.ParameterName = "@uae_enderecoPrincipal";
            Param.Size = 100;
            Param.Value = entity.uae_enderecoPrincipal;
            qs.Parameters.Add(Param);
        }

        /// <summary>
        /// Parâmetros para efetuar a alteração preservando a data de criação
        /// </summary>
        protected override void ParamAlterar(QueryStoredProcedure qs, Autenticador.Entities.SYS_UnidadeAdministrativaEndereco entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@ent_id";
            Param.Size = 16;
            Param.Value = entity.ent_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@uad_id";
            Param.Size = 16;
            Param.Value = entity.uad_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@uae_id";
            Param.Size = 16;
            Param.Value = entity.uae_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@end_id";
            Param.Size = 16;
            Param.Value = entity.end_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@uae_numero";
            Param.Size = 20;
            Param.Value = entity.uae_numero;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@uae_complemento";
            Param.Size = 100;
            if (!string.IsNullOrEmpty(entity.uae_complemento))
                Param.Value = entity.uae_complemento;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@uae_situacao";
            Param.Size = 1;
            Param.Value = entity.uae_situacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@uae_dataAlteracao";
            Param.Size = 8;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Decimal;
            Param.ParameterName = "@uae_latitude";
            Param.Size = 100;
            if (entity.uae_latitude == 0)
                Param.Value = DBNull.Value;
            else
                Param.Value = entity.uae_latitude;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Decimal;
            Param.ParameterName = "@uae_longitude";
            Param.Size = 100;
            if (entity.uae_longitude == 0)
                Param.Value = DBNull.Value;
            else
                Param.Value = entity.uae_longitude;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Boolean;
            Param.ParameterName = "@uae_enderecoPrincipal";
            Param.Size = 100;
            Param.Value = entity.uae_enderecoPrincipal;
            qs.Parameters.Add(Param);
        }

        /// <summary>
        /// Método alterado para que o update não faça a alteração da data de criação
        /// </summary>
        /// <param name="entity"> Entidade SYS_UnidadeAdministrativa</param>
        /// <returns>true = sucesso | false = fracasso</returns>  
        protected override bool Alterar(Autenticador.Entities.SYS_UnidadeAdministrativaEndereco entity)
        {
            this.__STP_UPDATE = "NEW_SYS_UnidadeAdministrativaEndereco_UPDATE";
            return base.Alterar(entity);
        }

        /// <summary>
        /// Parâmetros para efetuar a exclusão lógica.
        /// </summary>
        protected override void ParamDeletar(QueryStoredProcedure qs, Autenticador.Entities.SYS_UnidadeAdministrativaEndereco entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@ent_id";
            Param.Size = 16;
            Param.Value = entity.ent_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@uad_id";
            Param.Size = 16;
            Param.Value = entity.uad_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@uae_id";
            Param.Size = 16;
            Param.Value = entity.uae_id;
            qs.Parameters.Add(Param);
            
            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@uae_situacao";
            Param.Size = 1;
            Param.Value = 3;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@uae_dataAlteracao";
            Param.Size = 8;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);
            
        }

        /// <summary>
        /// Método alterado para que o delete não faça exclusão física e sim lógica (update).
        /// </summary>
        /// <param name="entity"> Entidade SYS_UnidadeAdministrativaEndereco</param>
        /// <returns>true = sucesso | false = fracasso</returns>        
        public override bool Delete(Autenticador.Entities.SYS_UnidadeAdministrativaEndereco entity)
        {
            this.__STP_DELETE = "NEW_SYS_UnidadeAdministrativaEndereco_Update_Situacao";
            return base.Delete(entity);
        }
    }
}
