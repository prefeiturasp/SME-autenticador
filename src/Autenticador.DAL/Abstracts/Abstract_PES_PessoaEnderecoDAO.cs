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
	/// Classe abstrata de PES_PessoaEndereco
	/// </summary>
	public abstract class Abstract_PES_PessoaEnderecoDAO : Abstract_DAL<PES_PessoaEndereco>
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
		protected override void ParamCarregar(QuerySelectStoredProcedure qs, PES_PessoaEndereco entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@pes_id";
			Param.Size = 16;
			Param.Value = entity.pes_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@pse_id";
			Param.Size = 16;
			Param.Value = entity.pse_id;
			qs.Parameters.Add(Param);


		}
		
		/// <summary>
		/// Configura os parametros do metodo de Inserir
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamInserir(QuerySelectStoredProcedure qs, PES_PessoaEndereco entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@pes_id";
			Param.Size = 16;
			Param.Value = entity.pes_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@pse_id";
			Param.Size = 16;
			Param.Value = entity.pse_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@end_id";
			Param.Size = 16;
			Param.Value = entity.end_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@pse_numero";
			Param.Size = 20;
			Param.Value = entity.pse_numero;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@pse_complemento";
			Param.Size = 100;
			if( !string.IsNullOrEmpty(entity.pse_complemento) )
				Param.Value = entity.pse_complemento;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@pse_situacao";
			Param.Size = 1;
			Param.Value = entity.pse_situacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@pse_dataCriacao";
			Param.Size = 16;
			Param.Value = entity.pse_dataCriacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@pse_dataAlteracao";
			Param.Size = 16;
			Param.Value = entity.pse_dataAlteracao;
			qs.Parameters.Add(Param);

            
            Param = qs.NewParameter();
            Param.DbType = DbType.Boolean;
            Param.ParameterName = "@pse_enderecoPrincipal";
            Param.Size = 1;
            Param.Value = entity.pse_enderecoPrincipal;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Decimal;
            Param.ParameterName = "@pse_latitude";
            Param.Size = 17;
            Param.Value = entity.pse_latitude;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Decimal;
            Param.ParameterName = "@pse_longitude";
            Param.Size = 17;
            Param.Value = entity.pse_longitude;
            qs.Parameters.Add(Param);


        }
		
		/// <summary>
		/// Configura os parametros do metodo de Alterar
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamAlterar(QueryStoredProcedure qs, PES_PessoaEndereco entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@pes_id";
			Param.Size = 16;
			Param.Value = entity.pes_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@pse_id";
			Param.Size = 16;
			Param.Value = entity.pse_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@end_id";
			Param.Size = 16;
			Param.Value = entity.end_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@pse_numero";
			Param.Size = 20;
			Param.Value = entity.pse_numero;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@pse_complemento";
			Param.Size = 100;
			if( !string.IsNullOrEmpty(entity.pse_complemento) )
				Param.Value = entity.pse_complemento;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@pse_situacao";
			Param.Size = 1;
			Param.Value = entity.pse_situacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@pse_dataCriacao";
			Param.Size = 16;
			Param.Value = entity.pse_dataCriacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@pse_dataAlteracao";
			Param.Size = 16;
			Param.Value = entity.pse_dataAlteracao;
			qs.Parameters.Add(Param);


            
            Param = qs.NewParameter();
            Param.DbType = DbType.Boolean;
            Param.ParameterName = "@pse_enderecoPrincipal";
            Param.Size = 1;
            Param.Value = entity.pse_enderecoPrincipal;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Decimal;
            Param.ParameterName = "@pse_latitude";
            Param.Size = 17;
            Param.Value = entity.pse_latitude;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Decimal;
            Param.ParameterName = "@pse_longitude";
            Param.Size = 17;
            Param.Value = entity.pse_longitude;
            qs.Parameters.Add(Param);


        }

		/// <summary>
		/// Configura os parametros do metodo de Deletar
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamDeletar(QueryStoredProcedure qs, PES_PessoaEndereco entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@pes_id";
			Param.Size = 16;
			Param.Value = entity.pes_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@pse_id";
			Param.Size = 16;
			Param.Value = entity.pse_id;
			qs.Parameters.Add(Param);


		}
		
		/// <summary>
		/// Recebe o valor do auto incremento e coloca na propriedade 
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, PES_PessoaEndereco entity)
		{
            entity.pse_id = new Guid(qs.Return.Rows[0][0].ToString());
            return (entity.pse_id != Guid.Empty); 
		}		
	}
}

