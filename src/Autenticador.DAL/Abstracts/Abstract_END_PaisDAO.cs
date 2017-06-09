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
	/// Classe abstrata de END_Pais
	/// </summary>
	public abstract class Abstract_END_PaisDAO : Abstract_DAL<END_Pais>
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
		protected override void ParamCarregar(QuerySelectStoredProcedure qs, END_Pais entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@pai_id";
			Param.Size = 16;
			Param.Value = entity.pai_id;
			qs.Parameters.Add(Param);


		}
		
		/// <summary>
		/// Configura os parametros do metodo de Inserir
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamInserir(QuerySelectStoredProcedure qs, END_Pais entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@pai_id";
			Param.Size = 16;
			Param.Value = entity.pai_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@pai_nome";
			Param.Size = 100;
			Param.Value = entity.pai_nome;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@pai_sigla";
			Param.Size = 10;
			if( !string.IsNullOrEmpty(entity.pai_sigla) )
				Param.Value = entity.pai_sigla;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@pai_ddi";
			Param.Size = 3;
			if( !string.IsNullOrEmpty(entity.pai_ddi) )
				Param.Value = entity.pai_ddi;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@pai_naturalMasc";
			Param.Size = 100;
			if( !string.IsNullOrEmpty(entity.pai_naturalMasc) )
				Param.Value = entity.pai_naturalMasc;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@pai_naturalFem";
			Param.Size = 100;
			if( !string.IsNullOrEmpty(entity.pai_naturalFem) )
				Param.Value = entity.pai_naturalFem;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@pai_situacao";
			Param.Size = 1;
			Param.Value = entity.pai_situacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@pai_integridade";
			Param.Size = 4;
			Param.Value = entity.pai_integridade;
			qs.Parameters.Add(Param);


		}
		
		/// <summary>
		/// Configura os parametros do metodo de Alterar
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamAlterar(QueryStoredProcedure qs, END_Pais entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@pai_id";
			Param.Size = 16;
			Param.Value = entity.pai_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@pai_nome";
			Param.Size = 100;
			Param.Value = entity.pai_nome;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@pai_sigla";
			Param.Size = 10;
			if( !string.IsNullOrEmpty(entity.pai_sigla) )
				Param.Value = entity.pai_sigla;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@pai_ddi";
			Param.Size = 3;
			if( !string.IsNullOrEmpty(entity.pai_ddi) )
				Param.Value = entity.pai_ddi;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@pai_naturalMasc";
			Param.Size = 100;
			if( !string.IsNullOrEmpty(entity.pai_naturalMasc) )
				Param.Value = entity.pai_naturalMasc;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@pai_naturalFem";
			Param.Size = 100;
			if( !string.IsNullOrEmpty(entity.pai_naturalFem) )
				Param.Value = entity.pai_naturalFem;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@pai_situacao";
			Param.Size = 1;
			Param.Value = entity.pai_situacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@pai_integridade";
			Param.Size = 4;
			Param.Value = entity.pai_integridade;
			qs.Parameters.Add(Param);


		}

		/// <summary>
		/// Configura os parametros do metodo de Deletar
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamDeletar(QueryStoredProcedure qs, END_Pais entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@pai_id";
			Param.Size = 16;
			Param.Value = entity.pai_id;
			qs.Parameters.Add(Param);


		}
		
		/// <summary>
		/// Recebe o valor do auto incremento e coloca na propriedade 
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, END_Pais entity)
		{
            entity.pai_id = new Guid(qs.Return.Rows[0][0].ToString());
            return (entity.pai_id != Guid.Empty); 
		}		
	}
}

