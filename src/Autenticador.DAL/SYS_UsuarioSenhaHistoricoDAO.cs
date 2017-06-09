/*
	Classe gerada automaticamente pelo Code Creator
*/

namespace Autenticador.DAL
{
    using Autenticador.DAL.Abstracts;
    using Autenticador.Entities;
    using CoreLibrary.Data.Common;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

	/// <summary>
	/// Description: .
	/// </summary>
	public class SYS_UsuarioSenhaHistoricoDAO : Abstract_SYS_UsuarioSenhaHistoricoDAO
    {
        #region M�todos

        /// <summary>
        /// Seleciona as �ltimas senhas do usu�rio.
        /// </summary>
        /// <param name="usu_id">ID do usu�rio.</param>
        /// <param name="qtdeSenhas">Quantidade de senhas que devem ser retornadas.</param>
        /// <returns></returns>
        public List<SYS_UsuarioSenhaHistorico> SelecionaUltimasSenhas(Guid usu_id, int qtdeSenhas)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_UsuarioSenhaHistorico_SelecionaUltimasSenhas", _Banco);

            try
            {
                #region Par�metros

                Param = qs.NewParameter();
                Param.ParameterName = "@usu_id";
                Param.DbType = DbType.Guid;
                Param.Size = 16;
                Param.Value = usu_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.ParameterName = "@qtdeSenhas";
                Param.DbType = DbType.Int32;
                Param.Size = 4;
                Param.Value = qtdeSenhas;
                qs.Parameters.Add(Param);

                #endregion Par�metros

                qs.Execute();

                return qs.Return.Rows.Cast<DataRow>().Select(p => DataRowToEntity(p, new SYS_UsuarioSenhaHistorico())).ToList();
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }


        /// <summary>
        /// Seleciona as �ltimas senhas do usu�rio.
        /// </summary>
        /// <param name="usu_id">ID do usu�rio.</param>
        /// <param name="qtdeSenhas">Quantidade de senhas que devem ser retornadas.</param>
        /// <returns></returns>
        public DataTable SelecionaUltimaSenha(Guid usu_id)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_UsuarioSenhaHistorico_SelecionaUltimaSenhas", _Banco);

            try
            {
                #region Par�metros

                Param = qs.NewParameter();
                Param.ParameterName = "@usu_id";
                Param.DbType = DbType.Guid;
                Param.Size = 16;
                Param.Value = usu_id;
                qs.Parameters.Add(Param);

                #endregion Par�metros

                qs.Execute();

                return qs.Return;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }


        #endregion M�todos

        #region Sobrescritos

        protected override void ParamInserir(QuerySelectStoredProcedure qs, SYS_UsuarioSenhaHistorico entity)
        {
            entity.ush_data = DateTime.Now;
            base.ParamInserir(qs, entity);
            qs.Parameters.RemoveAt("@ush_id");
        }

        protected override bool Inserir(SYS_UsuarioSenhaHistorico entity)
        {
            __STP_INSERT = "NEW_SYS_UsuarioSenhaHistorico_INSERT";
            return base.Inserir(entity);
        }
        #endregion Sobrescritos
    }
}