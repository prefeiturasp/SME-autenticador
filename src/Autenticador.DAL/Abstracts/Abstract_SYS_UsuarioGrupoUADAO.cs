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
	/// Classe abstrata de SYS_UsuarioGrupoUA
	/// </summary>
	public abstract class Abstract_SYS_UsuarioGrupoUADAO : Abstract_DAL<SYS_UsuarioGrupoUA>
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
		protected override void ParamCarregar(QuerySelectStoredProcedure qs, SYS_UsuarioGrupoUA entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@usu_id";
			Param.Size = 16;
			Param.Value = entity.usu_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@gru_id";
			Param.Size = 16;
			Param.Value = entity.gru_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@ugu_id";
			Param.Size = 16;
			Param.Value = entity.ugu_id;
			qs.Parameters.Add(Param);


		}
		
		/// <summary>
		/// Configura os parametros do metodo de Inserir
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamInserir(QuerySelectStoredProcedure qs, SYS_UsuarioGrupoUA entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@usu_id";
			Param.Size = 16;
			Param.Value = entity.usu_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@gru_id";
			Param.Size = 16;
			Param.Value = entity.gru_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@ugu_id";
			Param.Size = 16;
			Param.Value = entity.ugu_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@ent_id";
			Param.Size = 16;
			Param.Value = entity.ent_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@uad_id";
			Param.Size = 16;
			Param.Value = entity.uad_id;
			qs.Parameters.Add(Param);


		}
		
		/// <summary>
		/// Configura os parametros do metodo de Alterar
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamAlterar(QueryStoredProcedure qs, SYS_UsuarioGrupoUA entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@usu_id";
			Param.Size = 16;
			Param.Value = entity.usu_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@gru_id";
			Param.Size = 16;
			Param.Value = entity.gru_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@ugu_id";
			Param.Size = 16;
			Param.Value = entity.ugu_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@ent_id";
			Param.Size = 16;
			Param.Value = entity.ent_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@uad_id";
			Param.Size = 16;
			Param.Value = entity.uad_id;
			qs.Parameters.Add(Param);


		}

		/// <summary>
		/// Configura os parametros do metodo de Deletar
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamDeletar(QueryStoredProcedure qs, SYS_UsuarioGrupoUA entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@usu_id";
			Param.Size = 16;
			Param.Value = entity.usu_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@gru_id";
			Param.Size = 16;
			Param.Value = entity.gru_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@ugu_id";
			Param.Size = 16;
			Param.Value = entity.ugu_id;
			qs.Parameters.Add(Param);


		}
		
		/// <summary>
		/// Recebe o valor do auto incremento e coloca na propriedade 
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, SYS_UsuarioGrupoUA entity)
		{
            entity.ugu_id = new Guid(qs.Return.Rows[0][0].ToString());
            return (entity.ugu_id != Guid.Empty);
		}		
	}
}

