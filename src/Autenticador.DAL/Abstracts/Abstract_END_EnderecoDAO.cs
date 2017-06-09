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
	/// Classe abstrata de END_Endereco
	/// </summary>
	public abstract class Abstract_END_EnderecoDAO : Abstract_DAL<END_Endereco>
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
		protected override void ParamCarregar(QuerySelectStoredProcedure qs, END_Endereco entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@end_id";
			Param.Size = 16;
			Param.Value = entity.end_id;
			qs.Parameters.Add(Param);


		}
		
		/// <summary>
		/// Configura os parametros do metodo de Inserir
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamInserir(QuerySelectStoredProcedure qs, END_Endereco entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@end_id";
			Param.Size = 16;
			Param.Value = entity.end_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@end_cep";
			Param.Size = 8;
			Param.Value = entity.end_cep;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@end_logradouro";
			Param.Size = 200;
			Param.Value = entity.end_logradouro;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@end_bairro";
			Param.Size = 100;
			if( !string.IsNullOrEmpty(entity.end_bairro) )
				Param.Value = entity.end_bairro;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@end_distrito";
			Param.Size = 100;
			if( !string.IsNullOrEmpty(entity.end_distrito) )
				Param.Value = entity.end_distrito;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@end_zona";
			Param.Size = 1;
			if( entity.end_zona > 0  )
				Param.Value = entity.end_zona;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@cid_id";
			Param.Size = 16;
			Param.Value = entity.cid_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@end_situacao";
			Param.Size = 1;
			Param.Value = entity.end_situacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@end_dataCriacao";
			Param.Size = 16;
			Param.Value = entity.end_dataCriacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@end_dataAlteracao";
			Param.Size = 16;
			Param.Value = entity.end_dataAlteracao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@end_integridade";
			Param.Size = 4;
			Param.Value = entity.end_integridade;
			qs.Parameters.Add(Param);


		}
		
		/// <summary>
		/// Configura os parametros do metodo de Alterar
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamAlterar(QueryStoredProcedure qs, END_Endereco entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@end_id";
			Param.Size = 16;
			Param.Value = entity.end_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@end_cep";
			Param.Size = 8;
			Param.Value = entity.end_cep;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@end_logradouro";
			Param.Size = 200;
			Param.Value = entity.end_logradouro;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@end_bairro";
			Param.Size = 100;
			if( !string.IsNullOrEmpty(entity.end_bairro) )
				Param.Value = entity.end_bairro;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@end_distrito";
			Param.Size = 100;
			if( !string.IsNullOrEmpty(entity.end_distrito) )
				Param.Value = entity.end_distrito;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@end_zona";
			Param.Size = 1;
			if( entity.end_zona > 0  )
				Param.Value = entity.end_zona;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@cid_id";
			Param.Size = 16;
			Param.Value = entity.cid_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@end_situacao";
			Param.Size = 1;
			Param.Value = entity.end_situacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@end_dataCriacao";
			Param.Size = 16;
			Param.Value = entity.end_dataCriacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@end_dataAlteracao";
			Param.Size = 16;
			Param.Value = entity.end_dataAlteracao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@end_integridade";
			Param.Size = 4;
			Param.Value = entity.end_integridade;
			qs.Parameters.Add(Param);


		}

		/// <summary>
		/// Configura os parametros do metodo de Deletar
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamDeletar(QueryStoredProcedure qs, END_Endereco entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@end_id";
			Param.Size = 16;
			Param.Value = entity.end_id;
			qs.Parameters.Add(Param);


		}
		
		/// <summary>
		/// Recebe o valor do auto incremento e coloca na propriedade 
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, END_Endereco entity)
		{
            entity.end_id = new Guid(qs.Return.Rows[0][0].ToString());
            return (entity.end_id != Guid.Empty); 
		}		
	}
}

