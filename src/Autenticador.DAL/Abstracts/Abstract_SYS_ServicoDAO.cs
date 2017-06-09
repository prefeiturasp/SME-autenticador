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
	/// Classe abstrata de SYS_Servico.
	/// </summary>
	public abstract class Abstract_SYS_ServicoDAO : Abstract_DAL<SYS_Servico>
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
		protected override void ParamCarregar(QuerySelectStoredProcedure qs, SYS_Servico entity)
		{
			if (entity != null & qs != null)
            {
			Param = qs.NewParameter();
			Param.DbType = DbType.Int16;
			Param.ParameterName = "@ser_id";
			Param.Size = 2;
			Param.Value = entity.ser_id;
			qs.Parameters.Add(Param);


			}
		}
		
		/// <summary>
		/// Configura os parametros do metodo de Inserir.
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure.</param>
        /// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
		protected override void ParamInserir(QuerySelectStoredProcedure qs, SYS_Servico entity)
		{
			if (entity != null & qs != null)
            {
							Param = qs.NewParameter();
			Param.DbType = DbType.Int16;
			Param.ParameterName = "@ser_id";
			Param.Size = 2;
			Param.Value = entity.ser_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@ser_nome";
			Param.Size = 200;
			Param.Value = entity.ser_nome;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@ser_nomeProcedimento";
			Param.Size = 200;
			Param.Value = entity.ser_nomeProcedimento;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Boolean;
			Param.ParameterName = "@ser_ativo";
			Param.Size = 1;
			Param.Value = entity.ser_ativo;
			qs.Parameters.Add(Param);


			}
		}
		
		/// <summary>
		/// Configura os parametros do metodo de Alterar.
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure.</param>
        /// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
		protected override void ParamAlterar(QueryStoredProcedure qs, SYS_Servico entity)
		{
			if (entity != null & qs != null)
            {
			Param = qs.NewParameter();
			Param.DbType = DbType.Int16;
			Param.ParameterName = "@ser_id";
			Param.Size = 2;
			Param.Value = entity.ser_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@ser_nome";
			Param.Size = 200;
			Param.Value = entity.ser_nome;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@ser_nomeProcedimento";
			Param.Size = 200;
			Param.Value = entity.ser_nomeProcedimento;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Boolean;
			Param.ParameterName = "@ser_ativo";
			Param.Size = 1;
			Param.Value = entity.ser_ativo;
			qs.Parameters.Add(Param);


			}
		}

		/// <summary>
		/// Configura os parametros do metodo de Deletar.
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure.</param>
        /// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
		protected override void ParamDeletar(QueryStoredProcedure qs, SYS_Servico entity)
		{
			if (entity != null & qs != null)
            {
			Param = qs.NewParameter();
			Param.DbType = DbType.Int16;
			Param.ParameterName = "@ser_id";
			Param.Size = 2;
			Param.Value = entity.ser_id;
			qs.Parameters.Add(Param);


			}
		}
		
		/// <summary>
		/// Recebe o valor do auto incremento e coloca na propriedade.
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure.</param>
		/// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
		/// <returns>TRUE - Se entity.ParametroId > 0</returns>
		protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, SYS_Servico entity)
		{
			if (entity != null & qs != null)
            {

			}

			return false;
		}		
	}
}