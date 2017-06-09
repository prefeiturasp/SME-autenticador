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
	/// Classe abstrata de SYS_EntidadeEndereco
	/// </summary>
	public abstract class Abstract_SYS_EntidadeEnderecoDAO : Abstract_DAL<SYS_EntidadeEndereco>
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
		protected override void ParamCarregar(QuerySelectStoredProcedure qs, SYS_EntidadeEndereco entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@ent_id";
			Param.Size = 16;
			Param.Value = entity.ent_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@ene_id";
			Param.Size = 16;
			Param.Value = entity.ene_id;
			qs.Parameters.Add(Param);


		}
		
		/// <summary>
		/// Configura os parametros do metodo de Inserir
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamInserir(QuerySelectStoredProcedure qs, SYS_EntidadeEndereco entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@ent_id";
			Param.Size = 16;
			Param.Value = entity.ent_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@ene_id";
			Param.Size = 16;
			Param.Value = entity.ene_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@end_id";
			Param.Size = 16;
			Param.Value = entity.end_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@ene_numero";
			Param.Size = 20;
			Param.Value = entity.ene_numero;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@ene_complemento";
			Param.Size = 100;
			if( !string.IsNullOrEmpty(entity.ene_complemento) )
				Param.Value = entity.ene_complemento;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@ene_situacao";
			Param.Size = 1;
			Param.Value = entity.ene_situacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@ene_dataCriacao";
			Param.Size = 16;
			Param.Value = entity.ene_dataCriacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@ene_dataAlteracao";
			Param.Size = 16;
			Param.Value = entity.ene_dataAlteracao;
			qs.Parameters.Add(Param);


            Param = qs.NewParameter();
            Param.DbType = DbType.Boolean;
            Param.ParameterName = "@ene_enderecoPrincipal";
            Param.Size = 1;
            Param.Value = entity.ene_enderecoPrincipal;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Decimal;
            Param.ParameterName = "@ene_latitude";
            Param.Size = 17;
            Param.Value = entity.ene_latitude;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Decimal;
            Param.ParameterName = "@ene_longitude";
            Param.Size = 17;
            Param.Value = entity.ene_longitude;
            qs.Parameters.Add(Param);


        }
		
		/// <summary>
		/// Configura os parametros do metodo de Alterar
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamAlterar(QueryStoredProcedure qs, SYS_EntidadeEndereco entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@ent_id";
			Param.Size = 16;
			Param.Value = entity.ent_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@ene_id";
			Param.Size = 16;
			Param.Value = entity.ene_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@end_id";
			Param.Size = 16;
			Param.Value = entity.end_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@ene_numero";
			Param.Size = 20;
			Param.Value = entity.ene_numero;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@ene_complemento";
			Param.Size = 100;
			if( !string.IsNullOrEmpty(entity.ene_complemento) )
				Param.Value = entity.ene_complemento;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@ene_situacao";
			Param.Size = 1;
			Param.Value = entity.ene_situacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@ene_dataCriacao";
			Param.Size = 16;
			Param.Value = entity.ene_dataCriacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@ene_dataAlteracao";
			Param.Size = 16;
			Param.Value = entity.ene_dataAlteracao;
			qs.Parameters.Add(Param);



            Param = qs.NewParameter();
            Param.DbType = DbType.Boolean;
            Param.ParameterName = "@ene_enderecoPrincipal";
            Param.Size = 1;
            Param.Value = entity.ene_enderecoPrincipal;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Decimal;
            Param.ParameterName = "@ene_latitude";
            Param.Size = 17;
            Param.Value = entity.ene_latitude;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Decimal;
            Param.ParameterName = "@ene_longitude";
            Param.Size = 17;
            Param.Value = entity.ene_longitude;
            qs.Parameters.Add(Param);
        }

		/// <summary>
		/// Configura os parametros do metodo de Deletar
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamDeletar(QueryStoredProcedure qs, SYS_EntidadeEndereco entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@ent_id";
			Param.Size = 16;
			Param.Value = entity.ent_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@ene_id";
			Param.Size = 16;
			Param.Value = entity.ene_id;
			qs.Parameters.Add(Param);


		}
		
		/// <summary>
		/// Recebe o valor do auto incremento e coloca na propriedade 
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, SYS_EntidadeEndereco entity)
		{
            entity.ene_id = new Guid(qs.Return.Rows[0][0].ToString());
            return (entity.ene_id != Guid.Empty); 
		}		
	}
}

