/*
	Classe gerada automaticamente pelo MSTech Code Creator
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
	/// Classe abstrata de SYS_TipoEnsino
	/// </summary>
	public abstract class Abstract_SYS_TipoEnsinoDAO : Abstract_DAL<SYS_TipoEnsino>
	{
	
        protected override string ConnectionStringName
        {
            get
            {
                return "Autenticador";
            }
        }
        	
		/// <summary>
		/// Configura os parametros do metodo de carregar
		/// </ssummary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamCarregar(QuerySelectStoredProcedure qs, SYS_TipoEnsino entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@tpe_id";
			Param.Size = 4;
			Param.Value = entity.tpe_id;
			qs.Parameters.Add(Param);


		}
		
		/// <summary>
		/// Configura os parametros do metodo de Inserir
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamInserir(QuerySelectStoredProcedure qs, SYS_TipoEnsino entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@tpe_id";
			Param.Size = 4;
			Param.Value = entity.tpe_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@tpe_nome";
			Param.Size = 50;
			if( !string.IsNullOrEmpty(entity.tpe_nome) )
				Param.Value = entity.tpe_nome;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);


		}
		
		/// <summary>
		/// Configura os parametros do metodo de Alterar
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamAlterar(QueryStoredProcedure qs, SYS_TipoEnsino entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@tpe_id";
			Param.Size = 4;
			Param.Value = entity.tpe_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@tpe_nome";
			Param.Size = 50;
			if( !string.IsNullOrEmpty(entity.tpe_nome) )
				Param.Value = entity.tpe_nome;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);


		}

		/// <summary>
		/// Configura os parametros do metodo de Deletar
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamDeletar(QueryStoredProcedure qs, SYS_TipoEnsino entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@tpe_id";
			Param.Size = 4;
			Param.Value = entity.tpe_id;
			qs.Parameters.Add(Param);


		}
		
		/// <summary>
		/// Recebe o valor do auto incremento e coloca na propriedade 
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, SYS_TipoEnsino entity)
		{
            entity.tpe_id = Convert.ToInt32(qs.Return.Rows[0][0]);
            return (entity.tpe_id > 0);
		}		
	}
}

