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
	/// Classe abstrata de CFG_ServidorRelatorio
	/// </summary>
	public abstract class Abstract_CFG_ServidorRelatorioDAO : Abstract_DAL<CFG_ServidorRelatorio>
	{
	
        protected override string ConnectionStringName
        {
            get
            {
                return "AutenticadorDB";
            }
        }

        /// <summary>
        /// Configura os parametros do metodo de carregar
        /// </ssummary>
        /// <param name="qs">Objeto da Store Procedure</param>
        protected override void ParamCarregar(QuerySelectStoredProcedure qs, CFG_ServidorRelatorio entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@sis_id";
            Param.Size = 4;
            Param.Value = entity.sis_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@ent_id";
            Param.Size = 16;
            Param.Value = entity.ent_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@srr_id";
            Param.Size = 4;
            Param.Value = entity.srr_id;
            qs.Parameters.Add(Param);


        }

        /// <summary>
        /// Configura os parametros do metodo de Inserir
        /// </summary>
        /// <param name="qs">Objeto da Store Procedure</param>
        protected override void ParamInserir(QuerySelectStoredProcedure qs, CFG_ServidorRelatorio entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@sis_id";
            Param.Size = 4;
            Param.Value = entity.sis_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@ent_id";
            Param.Size = 16;
            Param.Value = entity.ent_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@srr_id";
            Param.Size = 4;
            Param.Value = entity.srr_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@srr_nome";
            Param.Size = 100;
            Param.Value = entity.srr_nome;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@srr_descricao";
            Param.Size = 1000;
            if (!string.IsNullOrEmpty(entity.srr_descricao))
                Param.Value = entity.srr_descricao;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Boolean;
            Param.ParameterName = "@srr_remoteServer";
            Param.Size = 1;
            Param.Value = entity.srr_remoteServer;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@srr_usuario";
            Param.Size = 512;
            if (!string.IsNullOrEmpty(entity.srr_usuario))
                Param.Value = entity.srr_usuario;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@srr_senha";
            Param.Size = 512;
            if (!string.IsNullOrEmpty(entity.srr_senha))
                Param.Value = entity.srr_senha;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@srr_dominio";
            Param.Size = 512;
            if (!string.IsNullOrEmpty(entity.srr_dominio))
                Param.Value = entity.srr_dominio;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@srr_diretorioRelatorios";
            Param.Size = 1000;
            if (!string.IsNullOrEmpty(entity.srr_diretorioRelatorios))
                Param.Value = entity.srr_diretorioRelatorios;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@srr_pastaRelatorios";
            Param.Size = 1000;
            Param.Value = entity.srr_pastaRelatorios;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@srr_situacao";
            Param.Size = 1;
            Param.Value = entity.srr_situacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@srr_dataCriacao";
            Param.Size = 16;
            Param.Value = entity.srr_dataCriacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@srr_dataAlteracao";
            Param.Size = 16;
            Param.Value = entity.srr_dataAlteracao;
            qs.Parameters.Add(Param);


        }

        /// <summary>
        /// Configura os parametros do metodo de Alterar
        /// </summary>
        /// <param name="qs">Objeto da Store Procedure</param>
        protected override void ParamAlterar(QueryStoredProcedure qs, CFG_ServidorRelatorio entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@sis_id";
            Param.Size = 4;
            Param.Value = entity.sis_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@ent_id";
            Param.Size = 16;
            Param.Value = entity.ent_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@srr_id";
            Param.Size = 4;
            Param.Value = entity.srr_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@srr_nome";
            Param.Size = 100;
            Param.Value = entity.srr_nome;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@srr_descricao";
            Param.Size = 1000;
            if (!string.IsNullOrEmpty(entity.srr_descricao))
                Param.Value = entity.srr_descricao;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Boolean;
            Param.ParameterName = "@srr_remoteServer";
            Param.Size = 1;
            Param.Value = entity.srr_remoteServer;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@srr_usuario";
            Param.Size = 512;
            if (!string.IsNullOrEmpty(entity.srr_usuario))
                Param.Value = entity.srr_usuario;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@srr_senha";
            Param.Size = 512;
            if (!string.IsNullOrEmpty(entity.srr_senha))
                Param.Value = entity.srr_senha;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@srr_dominio";
            Param.Size = 512;
            if (!string.IsNullOrEmpty(entity.srr_dominio))
                Param.Value = entity.srr_dominio;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@srr_diretorioRelatorios";
            Param.Size = 1000;
            if (!string.IsNullOrEmpty(entity.srr_diretorioRelatorios))
                Param.Value = entity.srr_diretorioRelatorios;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@srr_pastaRelatorios";
            Param.Size = 1000;
            Param.Value = entity.srr_pastaRelatorios;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@srr_situacao";
            Param.Size = 1;
            Param.Value = entity.srr_situacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@srr_dataCriacao";
            Param.Size = 16;
            Param.Value = entity.srr_dataCriacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@srr_dataAlteracao";
            Param.Size = 16;
            Param.Value = entity.srr_dataAlteracao;
            qs.Parameters.Add(Param);


        }

        /// <summary>
        /// Configura os parametros do metodo de Deletar
        /// </summary>
        /// <param name="qs">Objeto da Store Procedure</param>
        protected override void ParamDeletar(QueryStoredProcedure qs, CFG_ServidorRelatorio entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@sis_id";
            Param.Size = 4;
            Param.Value = entity.sis_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@ent_id";
            Param.Size = 16;
            Param.Value = entity.ent_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@srr_id";
            Param.Size = 4;
            Param.Value = entity.srr_id;
            qs.Parameters.Add(Param);


        }
		
		/// <summary>
		/// Recebe o valor do auto incremento e coloca na propriedade 
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, CFG_ServidorRelatorio entity)
		{
            entity.srr_id = Convert.ToInt32(qs.Return.Rows[0][0]);
            return entity.srr_id > 0;
		}		
	}
}

