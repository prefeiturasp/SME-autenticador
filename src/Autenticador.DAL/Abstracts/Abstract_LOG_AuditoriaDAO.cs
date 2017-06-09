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
    /// Classe abstrata de LOG_Auditoria
    /// </summary>
    public abstract class Abstract_LOG_AuditoriaDAL : Abstract_DAL<LOG_Auditoria>
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
        protected override void ParamCarregar(QuerySelectStoredProcedure qs, LOG_Auditoria entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@log_id";
            Param.Size = 16;
            Param.Value = entity.log_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@aud_id";
            Param.Size = 4;
            Param.Value = entity.aud_id;
            qs.Parameters.Add(Param);
        }

        /// <summary>
        /// Configura os parametros do metodo de Inserir
        /// </summary>
        /// <param name="qs">Objeto da Store Procedure</param>
        protected override void ParamInserir(QuerySelectStoredProcedure qs, LOG_Auditoria entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@log_id";
            Param.Size = 16;
            Param.Value = entity.log_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@aud_dataHora";
            Param.Size = 16;
            Param.Value = entity.aud_dataHora;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@aud_entidade";
            Param.Size = 100;
            Param.Value = entity.aud_entidade;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@aud_operacao";
            Param.Size = 50;
            Param.Value = entity.aud_operacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.String;
            Param.ParameterName = "@aud_entidadeOriginal";
            if (!string.IsNullOrEmpty(entity.aud_entidadeOriginal))
                Param.Value = entity.aud_entidadeOriginal;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.String;
            Param.ParameterName = "@aud_entidadeNova";
            if (!string.IsNullOrEmpty(entity.aud_entidadeNova))
                Param.Value = entity.aud_entidadeNova;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);


        }

        /// <summary>
        /// Configura os parametros do metodo de Alterar
        /// </summary>
        /// <param name="qs">Objeto da Store Procedure</param>
        protected override void ParamAlterar(QueryStoredProcedure qs, LOG_Auditoria entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@log_id";
            Param.Size = 16;
            Param.Value = entity.log_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@aud_id";
            Param.Size = 4;
            Param.Value = entity.aud_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@aud_dataHora";
            Param.Size = 16;
            Param.Value = entity.aud_dataHora;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@aud_entidade";
            Param.Size = 100;
            Param.Value = entity.aud_entidade;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@aud_operacao";
            Param.Size = 50;
            Param.Value = entity.aud_operacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.String;
            Param.ParameterName = "@aud_entidadeOriginal";
            if (!string.IsNullOrEmpty(entity.aud_entidadeOriginal))
                Param.Value = entity.aud_entidadeOriginal;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.String;
            Param.ParameterName = "@aud_entidadeNova";
            if (!string.IsNullOrEmpty(entity.aud_entidadeNova))
                Param.Value = entity.aud_entidadeNova;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);


        }

        /// <summary>
        /// Configura os parametros do metodo de Deletar
        /// </summary>
        /// <param name="qs">Objeto da Store Procedure</param>
        protected override void ParamDeletar(QueryStoredProcedure qs, LOG_Auditoria entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@log_id";
            Param.Size = 16;
            Param.Value = entity.log_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@aud_id";
            Param.Size = 4;
            Param.Value = entity.aud_id;
            qs.Parameters.Add(Param);
        }

        /// <summary>
        /// Recebe o valor do auto incremento e coloca na propriedade 
        /// </summary>
        /// <param name="qs">Objeto da Store Procedure</param>
        protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, LOG_Auditoria entity)
        {
            entity.aud_id = Convert.ToInt32(qs.Return.Rows[0][0]);
            return (entity.aud_id > 0);
        }
    }
}

