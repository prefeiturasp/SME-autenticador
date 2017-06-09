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
	/// Classe abstrata de LOG_UsuarioAD.
	/// </summary>
	public abstract class AbstractLOG_UsuarioADDAO : Abstract_DAL<LOG_UsuarioAD>
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
		protected override void ParamCarregar(QuerySelectStoredProcedure qs, LOG_UsuarioAD entity)
		{
			if (entity != null & qs != null)
            {
			Param = qs.NewParameter();
			Param.DbType = DbType.Int64;
			Param.ParameterName = "@usa_id";
			Param.Size = 4;
			Param.Value = entity.usa_id;
			qs.Parameters.Add(Param);


			}
		}
		
		/// <summary>
		/// Configura os parametros do metodo de Inserir.
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure.</param>
        /// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
		protected override void ParamInserir(QuerySelectStoredProcedure qs, LOG_UsuarioAD entity)
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
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@usa_acao";
			Param.Size = 1;
			Param.Value = entity.usa_acao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@usa_status";
			Param.Size = 1;
			Param.Value = entity.usa_status;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@usa_dataAcao";
			Param.Size = 16;
			Param.Value = entity.usa_dataAcao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@usa_origemAcao";
			Param.Size = 1;
			Param.Value = entity.usa_origemAcao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@usa_dataProcessado";
			Param.Size = 16;
				if(entity.usa_dataProcessado!= new DateTime())
				{
					Param.Value = entity.usa_dataProcessado;
				}
				else
				{
					Param.Value = DBNull.Value;
				}
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@usa_dados";
			Param.Size = 2147483647;
			Param.Value = entity.usa_dados;
			qs.Parameters.Add(Param);


			}
		}
		
		/// <summary>
		/// Configura os parametros do metodo de Alterar.
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure.</param>
        /// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
		protected override void ParamAlterar(QueryStoredProcedure qs, LOG_UsuarioAD entity)
		{
			if (entity != null & qs != null)
            {
			Param = qs.NewParameter();
			Param.DbType = DbType.Int64;
			Param.ParameterName = "@usa_id";
			Param.Size = 4;
			Param.Value = entity.usa_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@usu_id";
			Param.Size = 16;
			Param.Value = entity.usu_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@usa_acao";
			Param.Size = 1;
			Param.Value = entity.usa_acao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@usa_status";
			Param.Size = 1;
			Param.Value = entity.usa_status;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@usa_dataAcao";
			Param.Size = 16;
			Param.Value = entity.usa_dataAcao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@usa_origemAcao";
			Param.Size = 1;
			Param.Value = entity.usa_origemAcao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@usa_dataProcessado";
			Param.Size = 16;
				if(entity.usa_dataProcessado!= new DateTime())
				{
					Param.Value = entity.usa_dataProcessado;
				}
				else
				{
					Param.Value = DBNull.Value;
				}
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@usa_dados";
			Param.Size = 2147483647;
			Param.Value = entity.usa_dados;
			qs.Parameters.Add(Param);


			}
		}

		/// <summary>
		/// Configura os parametros do metodo de Deletar.
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure.</param>
        /// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
		protected override void ParamDeletar(QueryStoredProcedure qs, LOG_UsuarioAD entity)
		{
			if (entity != null & qs != null)
            {
			Param = qs.NewParameter();
			Param.DbType = DbType.Int64;
			Param.ParameterName = "@usa_id";
			Param.Size = 4;
			Param.Value = entity.usa_id;
			qs.Parameters.Add(Param);


			}
		}
		
		/// <summary>
		/// Recebe o valor do auto incremento e coloca na propriedade.
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure.</param>
		/// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
		/// <returns>TRUE - Se entity.ParametroId > 0</returns>
		protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, LOG_UsuarioAD entity)
		{
			if (entity != null & qs != null)
            {
			entity.usa_id = Convert.ToInt64(qs.Return.Rows[0][0]);
			return (entity.usa_id > 0);
			}

			return false;
		}		
	}
}