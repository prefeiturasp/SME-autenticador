/*
	Classe gerada automaticamente pelo Code Creator
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CoreLibrary.Data.Common;
using CoreLibrary.Data.Common.Abstracts;
using Autenticador.Entities;

namespace Autenticador.DAL.Abstracts
{

    /// <summary>
    /// Classe abstrata de LOG_Erros
    /// </summary>
    public abstract class Abstract_LOG_ErrosDAO : Abstract_DAL<LOG_Erros>
    {

        protected override string ConnectionStringName
        {
            get
            {
                return "LogDB";
            }
        }

        /// <summary>
        /// Configura os parametros do metodo de carregar
        /// </ssummary>
        /// <param name="qs">Objeto da Store Procedure</param>
        protected override void ParamCarregar(QuerySelectStoredProcedure qs, LOG_Erros entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@err_id";
            Param.Size = 16;
            Param.Value = entity.err_id;
            qs.Parameters.Add(Param);
        }

        /// <summary>
        /// Configura os parametros do metodo de Inserir
        /// </summary>
        /// <param name="qs">Objeto da Store Procedure</param>
        protected override void ParamInserir(QuerySelectStoredProcedure qs, LOG_Erros entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@err_dataHora";
            Param.Size = 16;
            Param.Value = entity.err_dataHora;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@err_ip";
            Param.Size = 15;
            Param.Value = entity.err_ip;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@err_machineName";
            Param.Size = 256;
            Param.Value = entity.err_machineName;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@err_browser";
            Param.Size = 256;
            if (!string.IsNullOrEmpty(entity.err_browser))
                Param.Value = entity.err_browser;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@err_caminhoArq";
            Param.Size = 2000;
            if (!string.IsNullOrEmpty(entity.err_caminhoArq))
                Param.Value = entity.err_caminhoArq;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.String;
            Param.ParameterName = "@err_descricao";
            Param.Value = entity.err_descricao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.String;
            Param.ParameterName = "@err_erroBase";
            if (!string.IsNullOrEmpty(entity.err_erroBase))
                Param.Value = entity.err_erroBase;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@err_tipoErro";
            Param.Size = 1000;
            if (!string.IsNullOrEmpty(entity.err_tipoErro))
                Param.Value = entity.err_tipoErro;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@sis_id";
            Param.Size = 4;
            if (entity.sis_id > 0)
                Param.Value = entity.sis_id;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@sis_decricao";
            Param.Size = 100;
            if (!string.IsNullOrEmpty(entity.sis_decricao))
                Param.Value = entity.sis_decricao;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@usu_id";
            Param.Size = 16;
            Param.Value = entity.usu_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@usu_login";
            Param.Size = 100;
            if (!string.IsNullOrEmpty(entity.usu_login))
                Param.Value = entity.usu_login;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);


        }

        /// <summary>
        /// Configura os parametros do metodo de Alterar
        /// </summary>
        /// <param name="qs">Objeto da Store Procedure</param>
        protected override void ParamAlterar(QueryStoredProcedure qs, LOG_Erros entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@err_id";
            Param.Size = 16;
            Param.Value = entity.err_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@err_dataHora";
            Param.Size = 16;
            Param.Value = entity.err_dataHora;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@err_ip";
            Param.Size = 15;
            Param.Value = entity.err_ip;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@err_machineName";
            Param.Size = 256;
            Param.Value = entity.err_machineName;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@err_browser";
            Param.Size = 256;
            if (!string.IsNullOrEmpty(entity.err_browser))
                Param.Value = entity.err_browser;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@err_caminhoArq";
            Param.Size = 2000;
            if (!string.IsNullOrEmpty(entity.err_caminhoArq))
                Param.Value = entity.err_caminhoArq;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.String;
            Param.ParameterName = "@err_descricao";
            Param.Value = entity.err_descricao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.String;
            Param.ParameterName = "@err_erroBase";
            if (!string.IsNullOrEmpty(entity.err_erroBase))
                Param.Value = entity.err_erroBase;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@err_tipoErro";
            Param.Size = 1000;
            if (!string.IsNullOrEmpty(entity.err_tipoErro))
                Param.Value = entity.err_tipoErro;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@sis_id";
            Param.Size = 4;
            if (entity.sis_id > 0)
                Param.Value = entity.sis_id;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@sis_decricao";
            Param.Size = 100;
            if (!string.IsNullOrEmpty(entity.sis_decricao))
                Param.Value = entity.sis_decricao;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@usu_id";
            Param.Size = 16;
            Param.Value = entity.usu_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@usu_login";
            Param.Size = 100;
            if (!string.IsNullOrEmpty(entity.usu_login))
                Param.Value = entity.usu_login;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);


        }

        /// <summary>
        /// Configura os parametros do metodo de Deletar
        /// </summary>
        /// <param name="qs">Objeto da Store Procedure</param>
        protected override void ParamDeletar(QueryStoredProcedure qs, LOG_Erros entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@err_id";
            Param.Size = 16;
            Param.Value = entity.err_id;
            qs.Parameters.Add(Param);
        }

        /// <summary>
        /// Recebe o valor do auto incremento e coloca na propriedade 
        /// </summary>
        /// <param name="qs">Objeto da Store Procedure</param>
        protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, LOG_Erros entity)
        {
            bool ret = qs.Return.Rows.Count > 0;
            if (ret)
                entity.err_id = new Guid(qs.Return.Rows[0][0].ToString());
            return ret;
        }
    }
}

