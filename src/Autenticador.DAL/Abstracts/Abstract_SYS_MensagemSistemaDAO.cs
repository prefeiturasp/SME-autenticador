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
	/// Classe abstrata de SYS_MensagemSistema
	/// </summary>
	public abstract class Abstract_SYS_MensagemSistemaDAO : Abstract_DAL<SYS_MensagemSistema>
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
		protected override void ParamCarregar(QuerySelectStoredProcedure qs, SYS_MensagemSistema entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@mss_id";
			Param.Size = 4;
			Param.Value = entity.mss_id;
			qs.Parameters.Add(Param);


		}
		
		/// <summary>
		/// Configura os parametros do metodo de Inserir
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamInserir(QuerySelectStoredProcedure qs, SYS_MensagemSistema entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@mss_chave";
			Param.Size = 100;
			Param.Value = entity.mss_chave;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@mss_valor";
			Param.Size = 2147483647;
			Param.Value = entity.mss_valor;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@mss_descricao";
			Param.Size = 200;
			if( !string.IsNullOrEmpty(entity.mss_descricao) )
				Param.Value = entity.mss_descricao;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@mss_situacao";
			Param.Size = 1;
			Param.Value = entity.mss_situacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@mss_dataCriacao";
			Param.Size = 16;
			Param.Value = entity.mss_dataCriacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@mss_dataAlteracao";
			Param.Size = 16;
			Param.Value = entity.mss_dataAlteracao;
			qs.Parameters.Add(Param);


		}
		
		/// <summary>
		/// Configura os parametros do metodo de Alterar
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamAlterar(QueryStoredProcedure qs, SYS_MensagemSistema entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@mss_id";
			Param.Size = 4;
			Param.Value = entity.mss_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@mss_chave";
			Param.Size = 100;
			Param.Value = entity.mss_chave;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@mss_valor";
			Param.Size = 2147483647;
			Param.Value = entity.mss_valor;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@mss_descricao";
			Param.Size = 200;
			if( !string.IsNullOrEmpty(entity.mss_descricao) )
				Param.Value = entity.mss_descricao;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@mss_situacao";
			Param.Size = 1;
			Param.Value = entity.mss_situacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@mss_dataCriacao";
			Param.Size = 16;
			Param.Value = entity.mss_dataCriacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@mss_dataAlteracao";
			Param.Size = 16;
			Param.Value = entity.mss_dataAlteracao;
			qs.Parameters.Add(Param);


		}

		/// <summary>
		/// Configura os parametros do metodo de Deletar
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamDeletar(QueryStoredProcedure qs, SYS_MensagemSistema entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@mss_id";
			Param.Size = 4;
			Param.Value = entity.mss_id;
			qs.Parameters.Add(Param);


		}
		
		/// <summary>
		/// Recebe o valor do auto incremento e coloca na propriedade 
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, SYS_MensagemSistema entity)
		{
			entity.mss_id = Convert.ToInt32(qs.Return.Rows[0][0]);
			return (entity.mss_id > 0);
		}		
	}
}

