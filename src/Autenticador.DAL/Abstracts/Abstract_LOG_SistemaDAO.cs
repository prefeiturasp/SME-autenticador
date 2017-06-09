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
	/// Classe abstrata de LOG_Sistema
	/// </summary>
	public abstract class Abstract_LOG_SistemaDAO : Abstract_DAL<LOG_Sistema>
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
		protected override void ParamCarregar(QuerySelectStoredProcedure qs, LOG_Sistema entity)
		{
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@log_id";
            Param.Size = 16;
            Param.Value = entity.log_id;
            qs.Parameters.Add(Param);
		}
		
		/// <summary>
		/// Configura os parametros do metodo de Inserir
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamInserir(QuerySelectStoredProcedure qs, LOG_Sistema entity)
		{
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@log_id";
            Param.Size = 16;
            Param.Value = entity.log_id;
            qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@log_dataHora";
			Param.Size = 16;
			Param.Value = entity.log_dataHora;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@log_ip";
			Param.Size = 15;
			Param.Value = entity.log_ip;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@log_machineName";
			Param.Size = 256;
			Param.Value = entity.log_machineName;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@log_acao";
			Param.Size = 50;
			Param.Value = entity.log_acao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.String;
			Param.ParameterName = "@log_descricao";
			Param.Value = entity.log_descricao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@sis_id";
			Param.Size = 4;
			if( entity.sis_id > 0  )
				Param.Value = entity.sis_id;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@sis_nome";
			Param.Size = 50;
            if (!string.IsNullOrEmpty(entity.sis_nome))
                Param.Value = entity.sis_nome;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@mod_id";
			Param.Size = 4;
			if( entity.mod_id > 0  )
				Param.Value = entity.mod_id;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@mod_nome";
			Param.Size = 50;
			if( !string.IsNullOrEmpty(entity.mod_nome) )
				Param.Value = entity.mod_nome;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@usu_id";
			Param.Size = 16;
			if( entity.usu_id != Guid.Empty  )
				Param.Value = entity.usu_id;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@usu_login";
			Param.Size = 100;
			if( !string.IsNullOrEmpty(entity.usu_login) )
				Param.Value = entity.usu_login;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@gru_id";
			Param.Size = 16;
			if( entity.gru_id != Guid.Empty  )
				Param.Value = entity.gru_id;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@gru_nome";
			Param.Size = 50;
			if( !string.IsNullOrEmpty(entity.gru_nome) )
				Param.Value = entity.gru_nome;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.String;
			Param.ParameterName = "@log_grupoUA";
			if( !string.IsNullOrEmpty(entity.log_grupoUA) )
				Param.Value = entity.log_grupoUA;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);
		}
		
		/// <summary>
		/// Configura os parametros do metodo de Alterar
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamAlterar(QueryStoredProcedure qs, LOG_Sistema entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@log_id";
			Param.Size = 16;
			Param.Value = entity.log_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@log_dataHora";
			Param.Size = 16;
			Param.Value = entity.log_dataHora;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@log_ip";
			Param.Size = 15;
			Param.Value = entity.log_ip;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@log_machineName";
			Param.Size = 256;
			Param.Value = entity.log_machineName;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@log_acao";
			Param.Size = 50;
			Param.Value = entity.log_acao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.String;
			Param.ParameterName = "@log_descricao";
			Param.Value = entity.log_descricao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@sis_id";
			Param.Size = 4;
			if( entity.sis_id > 0  )
				Param.Value = entity.sis_id;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@sis_nome";
			Param.Size = 50;
            if (!string.IsNullOrEmpty(entity.sis_nome))
                Param.Value = entity.sis_nome;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@mod_id";
			Param.Size = 4;
			if( entity.mod_id > 0  )
				Param.Value = entity.mod_id;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@mod_nome";
			Param.Size = 50;
			if( !string.IsNullOrEmpty(entity.mod_nome) )
				Param.Value = entity.mod_nome;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@usu_id";
			Param.Size = 16;
			if( entity.usu_id != Guid.Empty  )
				Param.Value = entity.usu_id;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@usu_login";
			Param.Size = 100;
			if( !string.IsNullOrEmpty(entity.usu_login) )
				Param.Value = entity.usu_login;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@gru_id";
			Param.Size = 16;
			if( entity.gru_id != Guid.Empty  )
				Param.Value = entity.gru_id;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@gru_nome";
			Param.Size = 50;
			if( !string.IsNullOrEmpty(entity.gru_nome) )
				Param.Value = entity.gru_nome;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.String;
			Param.ParameterName = "@log_grupoUA";
			if( !string.IsNullOrEmpty(entity.log_grupoUA) )
				Param.Value = entity.log_grupoUA;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);
		}

		/// <summary>
		/// Configura os parametros do metodo de Deletar
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamDeletar(QueryStoredProcedure qs, LOG_Sistema entity)
		{
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@log_id";
            Param.Size = 16;
            Param.Value = entity.log_id;
            qs.Parameters.Add(Param);
		}
		
		/// <summary>
		/// Recebe o valor do auto incremento e coloca na propriedade 
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, LOG_Sistema entity)
		{
            return (Convert.ToInt32(qs.Return.Rows[0][0]) > 0);
		}		
	}
}

