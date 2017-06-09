/*
	Classe gerada automaticamente pelo Code Creator
*/

namespace Autenticador.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Autenticador.DAL.Abstracts;
    using Autenticador.Entities;
    using CoreLibrary.Data.Common;
	
	/// <summary>
	/// Description: .
	/// </summary>
	public class CFG_TemaPadraoDAO : Abstract_CFG_TemaPadraoDAO
    {
        #region Métodos de consulta

        /// <summary>
        /// Carrega um tema padrão pelo nome.
        /// </summary>
        /// <param name="tep_nome">Nome do tema padrão.</param>
        /// <returns></returns>
        public CFG_TemaPadrao CarregarPorNome(string tep_nome)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_CFG_TemaPadrao_CarregarPorNome", _Banco);

            try
            {
                #region Parâmetro

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@tep_nome";
                Param.Size = 100;
                Param.Value = tep_nome;
                qs.Parameters.Add(Param);

                #endregion Parâmetro

                qs.Execute();

                return qs.Return.Rows.Count > 0 ?
                    DataRowToEntity(qs.Return.Rows[0], new CFG_TemaPadrao()) :
                    new CFG_TemaPadrao();
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        /// <summary>
        /// Seleciona os temas ativos.
        /// </summary>
        /// <returns></returns>
        public List<CFG_TemaPadrao> SelecionaAtivos()
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_CFG_TemaPadrao_SelecionaAtivos", _Banco);

            try
            {
                qs.Execute();

                return qs.Return.Rows.Cast<DataRow>().Select(p => DataRowToEntity(p, new CFG_TemaPadrao())).ToList();
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        /// <summary>
        /// Seleciona os temas ativos.
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public List<CFG_TemaPadrao> SelecionaAtivosPaginado(int currentPage, int pageSize, out int totalRecords)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_CFG_TemaPadrao_SelecionaAtivos", _Banco);

            try
            {
                totalRecords = qs.Execute(currentPage, pageSize);

                return qs.Return.Rows.Cast<DataRow>().Select(p => DataRowToEntity(p, new CFG_TemaPadrao())).ToList();
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        #endregion Métodos de consulta

        #region Métodos de validação

        /// <summary>
        /// Verifica se já existe um tema com o mesmo nome.
        /// </summary>
        /// <param name="tep_id">ID do tema.</param>
        /// <param name="tep_nome">Nome do tema.</param>
        /// <returns></returns>
        public bool VerificaExistePorNome(int tep_id, string tep_nome)
        {
            QueryStoredProcedure qs = new QueryStoredProcedure("NEW_CFG_TemaPadrao_VerificaExistePorNome", _Banco);

            #region Parâmetros

            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@tep_id";
            Param.Size = 4;
            if (tep_id > 0)
            {
                Param.Value = tep_id;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@tep_nome";
            Param.Size = 100;
            Param.Value = tep_nome;
            qs.Parameters.Add(Param);

            #endregion Parâmetros

            qs.Execute();

            return qs.Return > 0;
        }

        /// <summary>
        /// Verifica se o registro é utilizado em outras tabelas.
        /// </summary>
        /// <param name="tep_id">ID do registro.</param>
        /// <returns></returns>
        public bool VerificaIntegridade(int tep_id)
        {
            QueryStoredProcedure qs = new QueryStoredProcedure("NEW_VerificarIntegridade", _Banco);

            try
            {
                #region Parâmetros

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@tabelasNaoVerificar";
                Param.Value = "CFG_TemaPadrao";
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@campo";
                Param.Size = 50;
                Param.Value = "tep_id";
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@valorCampo";
                Param.Size = 50;
                Param.Value = tep_id.ToString();
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

        #endregion Métodos de validação

        #region Métodos sobrescritos

        protected override void ParamInserir(QuerySelectStoredProcedure qs, CFG_TemaPadrao entity)
        {
            entity.tep_dataCriacao = entity.tep_dataAlteracao = DateTime.Now;
            base.ParamInserir(qs, entity);
        }

        protected override void ParamAlterar(QueryStoredProcedure qs, CFG_TemaPadrao entity)
        {
            entity.tep_dataAlteracao = DateTime.Now;
            base.ParamAlterar(qs, entity);
            qs.Parameters.RemoveAt("@tep_dataCriacao");
        }

        protected override bool Alterar(CFG_TemaPadrao entity)
        {
            __STP_UPDATE = "NEW_CFG_TemaPadrao_UPDATE";
            return base.Alterar(entity);
        }

        protected override void ParamDeletar(QueryStoredProcedure qs, CFG_TemaPadrao entity)
        {
            base.ParamDeletar(qs, entity);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@tep_situacao";
            Param.Size = 1;
            Param.Value = (byte)CFG_TemaPadrao.eSituacao.Excluido;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@tep_dataAlteracao";
            Param.Size = 16;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);
        }

        public override bool Delete(CFG_TemaPadrao entity)
        {
            __STP_DELETE = "NEW_CFG_TemaPadrao_UpdateSituacao";
            return base.Delete(entity);
        }

        #endregion Métodos sobrescritos
    }
}