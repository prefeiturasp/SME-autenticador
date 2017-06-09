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
	/// Classe abstrata de SYS_LoginProviderDominioPermitido.
	/// </summary>
	public abstract class Abstract_SYS_LoginProviderDominioPermitidoDAO : Abstract_DAL<SYS_LoginProviderDominioPermitido>
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
		protected override void ParamCarregar(QuerySelectStoredProcedure qs, SYS_LoginProviderDominioPermitido entity)
		{
			if (entity != null & qs != null)
            {
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@ent_id";
			Param.Size = 16;
			Param.Value = entity.ent_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@LoginProvider";
			Param.Size = 128;
			Param.Value = entity.LoginProvider;
			qs.Parameters.Add(Param);


			}
		}
		
		/// <summary>
		/// Configura os parametros do metodo de Inserir.
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure.</param>
        /// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
		protected override void ParamInserir(QuerySelectStoredProcedure qs, SYS_LoginProviderDominioPermitido entity)
		{
			if (entity != null & qs != null)
            {
							Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@ent_id";
			Param.Size = 16;
			Param.Value = entity.ent_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@LoginProvider";
			Param.Size = 128;
			Param.Value = entity.LoginProvider;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@Dominios";
			Param.Size = 1024;
				if(!string.IsNullOrEmpty(entity.Dominios))
				{
					Param.Value = entity.Dominios;
				}
				else
				{
					Param.Value = DBNull.Value;
				}
			qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@Tenant";
            Param.Size = 1024;
            if (!string.IsNullOrEmpty(entity.Tenant))
            {
                Param.Value = entity.Tenant;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);


            }
		}
		
		/// <summary>
		/// Configura os parametros do metodo de Alterar.
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure.</param>
        /// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
		protected override void ParamAlterar(QueryStoredProcedure qs, SYS_LoginProviderDominioPermitido entity)
		{
			if (entity != null & qs != null)
            {
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@ent_id";
			Param.Size = 16;
			Param.Value = entity.ent_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@LoginProvider";
			Param.Size = 128;
			Param.Value = entity.LoginProvider;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@Dominios";
			Param.Size = 1024;
				if(!string.IsNullOrEmpty(entity.Dominios))
				{
					Param.Value = entity.Dominios;
				}
				else
				{
					Param.Value = DBNull.Value;
				}
			qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@Tenant";
            Param.Size = 1024;
            if (!string.IsNullOrEmpty(entity.Tenant))
            {
                Param.Value = entity.Tenant;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            }
		}

		/// <summary>
		/// Configura os parametros do metodo de Deletar.
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure.</param>
        /// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
		protected override void ParamDeletar(QueryStoredProcedure qs, SYS_LoginProviderDominioPermitido entity)
		{
			if (entity != null & qs != null)
            {
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@ent_id";
			Param.Size = 16;
			Param.Value = entity.ent_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@LoginProvider";
			Param.Size = 128;
			Param.Value = entity.LoginProvider;
			qs.Parameters.Add(Param);


			}
		}
		
		/// <summary>
		/// Recebe o valor do auto incremento e coloca na propriedade.
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure.</param>
		/// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
		/// <returns>TRUE - Se entity.ParametroId > 0</returns>
		protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, SYS_LoginProviderDominioPermitido entity)
		{
            
			if (entity != null & qs != null)
            {
                return true;
			}

			return false;
            
        }		
	}
}