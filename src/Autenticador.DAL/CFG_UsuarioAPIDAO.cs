/*
	Classe gerada automaticamente pelo Code Creator
*/

namespace Autenticador.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Autenticador.DAL.Abstracts;
    using Autenticador.Entities;
    using CoreLibrary.Data.Common;
    using System.Linq;
	
	/// <summary>
	/// Description: .
	/// </summary>
	public class CFG_UsuarioAPIDAO : Abstract_CFG_UsuarioAPIDAO
    {
        #region M�todos de atualiza��o

        /// <summary>
        /// Atualiza a senha do usu�rio.
        /// </summary>
        /// <param name="entity">Entidade do usu�rios com os campos de ID e a nova senha preenchidos.</param>
        /// <returns></returns>
        public bool AtualizaSenha(CFG_UsuarioAPI entity)
        {
            QueryStoredProcedure qs = new QueryStoredProcedure("NEW_CFG_UsuarioAPI_AtualizaSenha", _Banco);

            try
            {
                #region Par�metros

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@uap_id";
                Param.Size = 4;
                Param.Value = entity.uap_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@uap_password";
                Param.Size = 256;
                Param.Value = entity.uap_password;
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

        #endregion

        #region M�todos de consulta

        /// <summary>
        /// Seleciona uma lista de usu�rio API ativos.
        /// </summary>
        /// <returns></returns>
        public List<CFG_UsuarioAPI> SelecionaAtivos()
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_CFG_UsuarioAPI_SelecionaAtivos", _Banco);

            try
            {
                qs.Execute();
                return qs.Return.Rows.Count > 0 ?
                    qs.Return.Rows.Cast<DataRow>().Select(row => DataRowToEntity(row, new CFG_UsuarioAPI())).ToList() :
                    new List<CFG_UsuarioAPI>();
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        /// <summary>
        /// Seleciona um usu�rio da API pelo nome.
        /// </summary>
        /// <param name="uap_username">Nome do usu�rio da API.</param>
        /// <returns></returns>
        public CFG_UsuarioAPI SelecionaPorUsername(string uap_username)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_CFG_UsuarioAPI_SelecionaPorUsername", _Banco);

            try
            {
                #region Par�metro

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@uap_username";
                Param.Size = 100;
                Param.Value = uap_username;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                return qs.Return.Rows.Count > 0 ?
                        DataRowToEntity(qs.Return.Rows[0], new CFG_UsuarioAPI()) :
                        new CFG_UsuarioAPI();
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        #endregion

        #region M�todos de verifica��o

        /// <summary>
        /// Verifica se existe um usu�rio com o mesmo username.
        /// </summary>
        /// <param name="uap_username">Nome do usu�rio.</param>
        /// <param name="uap_id">ID do usu�rio.</param>
        /// <returns>True, se j� existir.</returns>
        public bool VerificaUsernameExistente(string uap_username, int uap_id)
        {
            QueryStoredProcedure qs = new QueryStoredProcedure("NEW_CFG_UsuarioAPI_VerificaUsernameExistente", _Banco);

            try
            {
                #region Par�metros

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@uap_username";
                Param.Size = 100;
                Param.Value = uap_username;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@uap_id";
                Param.Size = 4;
                if (uap_id > 0)
                    Param.Value = uap_id;
                else
                    Param.Value = DBNull.Value;
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

        #endregion

        #region M�todos sobrescritos

        protected override void ParamInserir(QuerySelectStoredProcedure qs, CFG_UsuarioAPI entity)
        {
            entity.uap_dataCriacao = entity.uap_dataAlteracao = DateTime.Now;
            base.ParamInserir(qs, entity);
        }

        protected override void ParamAlterar(QueryStoredProcedure qs, CFG_UsuarioAPI entity)
        {
            entity.uap_dataAlteracao = DateTime.Now;
            base.ParamAlterar(qs, entity);
            qs.Parameters.RemoveAt("@uap_password");
            qs.Parameters.RemoveAt("@uap_dataCriacao");
        }

        protected override bool Alterar(CFG_UsuarioAPI entity)
        {
            __STP_UPDATE = "NEW_CFG_UsuarioAPI_UPDATE";
            return base.Alterar(entity);
        }

        protected override void ParamDeletar(QueryStoredProcedure qs, CFG_UsuarioAPI entity)
        {
            base.ParamDeletar(qs, entity);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@uap_situacao";
            Param.Size = 1;
            Param.Value = 3;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@uap_dataAlteracao";
            Param.Size = 16;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);
        }

        public override bool Delete(CFG_UsuarioAPI entity)
        {
            __STP_DELETE = "NEW_CFG_UsuarioAPI_UpdateSituacao";
            return base.Delete(entity);
        }

        #endregion
    }
}