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
	/// Classe abstrata de SYS_UsuarioSenhaHistorico.
	/// </summary>
	public abstract class Abstract_SYS_UsuarioSenhaHistoricoDAO : Abstract_DAL<SYS_UsuarioSenhaHistorico>
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
		protected override void ParamCarregar(QuerySelectStoredProcedure qs, SYS_UsuarioSenhaHistorico entity)
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
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@ush_id";
			Param.Size = 16;
			Param.Value = entity.ush_id;
			qs.Parameters.Add(Param);


			}
		}
		
		/// <summary>
		/// Configura os parametros do metodo de Inserir.
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure.</param>
        /// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
		protected override void ParamInserir(QuerySelectStoredProcedure qs, SYS_UsuarioSenhaHistorico entity)
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
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@ush_senha";
			Param.Size = 256;
			Param.Value = entity.ush_senha;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@ush_criptografia";
			Param.Size = 1;
				if(entity.ush_criptografia > 0 )
				{
					Param.Value = entity.ush_criptografia;
				}
				else
				{
					Param.Value = DBNull.Value;
				}
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@ush_id";
			Param.Size = 16;
			Param.Value = entity.ush_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@ush_data";
			Param.Size = 16;
			Param.Value = entity.ush_data;
			qs.Parameters.Add(Param);


			}
		}
		
		/// <summary>
		/// Configura os parametros do metodo de Alterar.
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure.</param>
        /// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
		protected override void ParamAlterar(QueryStoredProcedure qs, SYS_UsuarioSenhaHistorico entity)
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
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@ush_senha";
			Param.Size = 256;
			Param.Value = entity.ush_senha;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@ush_criptografia";
			Param.Size = 1;
				if(entity.ush_criptografia > 0 )
				{
					Param.Value = entity.ush_criptografia;
				}
				else
				{
					Param.Value = DBNull.Value;
				}
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@ush_id";
			Param.Size = 16;
			Param.Value = entity.ush_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@ush_data";
			Param.Size = 16;
			Param.Value = entity.ush_data;
			qs.Parameters.Add(Param);


			}
		}

		/// <summary>
		/// Configura os parametros do metodo de Deletar.
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure.</param>
        /// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
		protected override void ParamDeletar(QueryStoredProcedure qs, SYS_UsuarioSenhaHistorico entity)
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
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@ush_id";
			Param.Size = 16;
			Param.Value = entity.ush_id;
			qs.Parameters.Add(Param);


			}
		}
		
		/// <summary>
		/// Recebe o valor do auto incremento e coloca na propriedade.
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure.</param>
		/// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
		/// <returns>TRUE - Se entity.ParametroId > 0</returns>
		protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, SYS_UsuarioSenhaHistorico entity)
		{
			if (entity != null & qs != null)
            {
return true;
			}

			return false;
		}		
	}
}