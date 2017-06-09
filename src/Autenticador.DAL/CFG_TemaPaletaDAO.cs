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
	public class CFG_TemaPaletaDAO : Abstract_CFG_TemaPaletaDAO
    {
        #region Métodos de consulta

        /// <summary>
        /// Seleciona todos os temas de cores ativos no sistema.
        /// </summary>
        /// <returns></returns>
        public List<CFG_TemaPaleta> SelecionaAtivos()
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_CFG_TemaPaleta_SelecionaAtivos", _Banco);

            qs.Execute();

            return qs.Return.Rows.Count > 0 ?
                qs.Return.Rows.Cast<DataRow>().Select(p => DataRowToEntity(p, new CFG_TemaPaleta())).ToList() :
                new List<CFG_TemaPaleta>();
        }

        /// <summary>
        /// Seleciona todos os temas de cores ativos no sistema. (Paginado)
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public List<CFG_TemaPaleta> SelecionaAtivosPaginado(int currentPage, int pageSize, out int totalRecords)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_CFG_TemaPaleta_SelecionaAtivos", _Banco);

            totalRecords = qs.Execute(currentPage, pageSize);

            return qs.Return.Rows.Count > 0 ?
                qs.Return.Rows.Cast<DataRow>().Select(p => DataRowToEntity(p, new CFG_TemaPaleta())).ToList() :
                new List<CFG_TemaPaleta>();
        }

        /// <summary>
        /// Seleciona temas de cores pelo tema padrão.
        /// </summary>
        /// <param name="tep_id">ID do tema padrão.</param>
        /// <returns></returns>
        public List<CFG_TemaPaleta> SelecionaPorTemaPadrao(int tep_id)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_CFG_TemaPaleta_SelecionaPorTemaPadrao", _Banco);

            try
            {
                #region Parâmetro

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@tep_id";
                Param.Size = 4;
                Param.Value = tep_id;
                qs.Parameters.Add(Param);

                #endregion Parâmetro

                qs.Execute();

                return qs.Return.Rows.Count > 0 ?
                    qs.Return.Rows.Cast<DataRow>().Select(p => DataRowToEntity(p, new CFG_TemaPaleta())).ToList() :
                    new List<CFG_TemaPaleta>();
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        #endregion Métodos de consulta

        #region Métodos de validação

        /// <summary>
        /// Verifica se já existe um tema de cores com o mesmo nome e o mesmo tema padrão.
        /// </summary>
        /// <param name="tep_id">ID do tema padrão.</param>
        /// <param name="tpl_id">ID do tema de cores.</param>
        /// <param name="tpl_nome">Nome do tema.</param>
        /// <returns></returns>
        public bool VerificaExistePorNomeTemaPadrao(int tep_id, int tpl_id, string tpl_nome)
        {
            QueryStoredProcedure qs = new QueryStoredProcedure("NEW_CFG_TemaPaleta_VerificaExistePorNomeTemaPadrao", _Banco);

            try
            {
                #region Parâmetros

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@tep_id";
                Param.Size = 4;
                Param.Value = tep_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@tpl_id";
                Param.Size = 4;
                if (tpl_id > 0)
                    Param.Value = tpl_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@tpl_nome";
                Param.Size = 100;
                Param.Value = tpl_nome;
                qs.Parameters.Add(Param);

                #endregion Parâmetros

                qs.Execute();

                return qs.Return > 0;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        /// <summary>
        /// Verifica se o registro é utilizado em outras tabelas.
        /// </summary>
        /// <param name="tep_id">ID do registro.</param>
        /// <returns></returns>
        public bool VerificaIntegridade(int tep_id, int tpl_id)
        {
            QueryStoredProcedure qs = new QueryStoredProcedure("NEW_VerificarIntegridadeChaveDupla", _Banco);

            try
            {
                #region Parâmetros

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@tabelasNaoVerificar";
                Param.Value = "CFG_TemaPaleta";
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@campo1";
                Param.Size = 50;
                Param.Value = "tep_id";
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@valorCampo1";
                Param.Size = 50;
                Param.Value = tep_id.ToString();
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@campo2";
                Param.Size = 50;
                Param.Value = "tpl_id";
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@valorCampo2";
                Param.Size = 50;
                Param.Value = tpl_id.ToString();
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

        protected override void ParamInserir(QuerySelectStoredProcedure qs, CFG_TemaPaleta entity)
        {
            entity.tpl_dataCriacao = entity.tpl_dataAlteracao = DateTime.Now;
            base.ParamInserir(qs, entity);
        }

        protected override void ParamAlterar(QueryStoredProcedure qs, CFG_TemaPaleta entity)
        {
            entity.tpl_dataAlteracao = DateTime.Now;
            base.ParamAlterar(qs, entity);
            qs.Parameters.RemoveAt("@tpl_dataCriacao");
        }

        protected override bool Alterar(CFG_TemaPaleta entity)
        {
            __STP_UPDATE = "NEW_CFG_TemaPaleta_UPDATE";
            return base.Alterar(entity);
        }

        protected override void ParamDeletar(QueryStoredProcedure qs, CFG_TemaPaleta entity)
        {
            base.ParamDeletar(qs, entity);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@tpl_situacao";
            Param.Size = 1;
            Param.Value = (byte)CFG_TemaPaleta.eSituacao.Excluido;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@tpl_dataAlteracao";
            Param.Size = 16;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);
        }

        public override bool Delete(CFG_TemaPaleta entity)
        {
            __STP_DELETE = "NEW_CFG_TemaPaleta_UpdateSituacao";
            return base.Delete(entity);
        }

        protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, CFG_TemaPaleta entity)
        {
            if (qs != null && entity != null)
            {
                entity.tpl_id = Convert.ToInt32(qs.Return.Rows[0][0]);
                return entity.tpl_id > 0;
            }

            return false;
        }

        #endregion Métodos sobrescritos
    }
}