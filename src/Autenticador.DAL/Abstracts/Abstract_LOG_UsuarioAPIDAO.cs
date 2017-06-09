/*
	Classe gerada automaticamente pelo Code Creator
*/

namespace Autenticador.DAL.Abstracts
{
	using System;
	using System.Data;
	using CoreLibrary.Data.Common;
	using CoreLibrary.Data.Common.Abstracts;
	using Autenticador.Entities;
	
	/// <summary>
	/// Classe abstrata de LOG_UsuarioAPI.
	/// </summary>
	public abstract class Abstract_LOG_UsuarioAPIDAO : Abstract_DAL<LOG_UsuarioAPI>
	{
        /// <summary>
		/// ConnectionString.
		/// </summary>
        protected override string ConnectionStringName
        {
            get
            {
                return "AutenticadorDB";
            }
        }
        	
		/// <summary>
		/// Configura os parametros do metodo de carregar.
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure.</param>
        /// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
		protected override void ParamCarregar(QuerySelectStoredProcedure qs, LOG_UsuarioAPI entity)
		{
			if (entity != null & qs != null)
            {
			Param = qs.NewParameter();
			Param.DbType = DbType.Int64;
			Param.ParameterName = "@lua_id";
			Param.Size = 8;
			Param.Value = entity.lua_id;
			qs.Parameters.Add(Param);


			}
		}
		
		/// <summary>
		/// Configura os parametros do metodo de Inserir.
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure.</param>
        /// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
		protected override void ParamInserir(QuerySelectStoredProcedure qs, LOG_UsuarioAPI entity)
		{
			if (entity != null & qs != null)
            {
							Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@usu_id";
			Param.Size = 16;
			Param.Value = entity.usu_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@uap_id";
			Param.Size = 4;
			Param.Value = entity.uap_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@lua_acao";
			Param.Size = 1;
			Param.Value = entity.lua_acao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@lua_dataHora";
			Param.Size = 16;
			Param.Value = entity.lua_dataHora;
			qs.Parameters.Add(Param);


			}
		}
		
		/// <summary>
		/// Configura os parametros do metodo de Alterar.
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure.</param>
        /// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
		protected override void ParamAlterar(QueryStoredProcedure qs, LOG_UsuarioAPI entity)
		{
			if (entity != null & qs != null)
            {
			Param = qs.NewParameter();
			Param.DbType = DbType.Int64;
			Param.ParameterName = "@lua_id";
			Param.Size = 8;
			Param.Value = entity.lua_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@usu_id";
			Param.Size = 16;
			Param.Value = entity.usu_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@uap_id";
			Param.Size = 4;
			Param.Value = entity.uap_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@lua_acao";
			Param.Size = 1;
			Param.Value = entity.lua_acao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@lua_dataHora";
			Param.Size = 16;
			Param.Value = entity.lua_dataHora;
			qs.Parameters.Add(Param);


			}
		}

		/// <summary>
		/// Configura os parametros do metodo de Deletar.
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure.</param>
        /// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
		protected override void ParamDeletar(QueryStoredProcedure qs, LOG_UsuarioAPI entity)
		{
			if (entity != null & qs != null)
            {
			Param = qs.NewParameter();
			Param.DbType = DbType.Int64;
			Param.ParameterName = "@lua_id";
			Param.Size = 8;
			Param.Value = entity.lua_id;
			qs.Parameters.Add(Param);


			}
		}
		
		/// <summary>
		/// Recebe o valor do auto incremento e coloca na propriedade.
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure.</param>
		/// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
		/// <returns>TRUE - Se entity.ParametroId > 0</returns>
		protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, LOG_UsuarioAPI entity)
		{
			if (entity != null & qs != null)
            {
			entity.lua_id = Convert.ToInt32(qs.Return.Rows[0][0]);
			return (entity.lua_id > 0);
			}

			return false;
		}		
	}
}