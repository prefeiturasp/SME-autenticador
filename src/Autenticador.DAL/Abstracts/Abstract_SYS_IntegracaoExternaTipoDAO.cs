/*
	Classe gerada automaticamente pelo Code Creator
*/

using System;
using System.Data;
using CoreLibrary.Data.Common;
using CoreLibrary.Data.Common.Abstracts;
using Autenticador.Entities;
	
namespace Autenticador.DAL.Abstracts
{

	
	/// <summary>
	/// Classe abstrata de SYS_IntegracaoExternaTipo.
	/// </summary>
	public abstract class Abstract_SYS_IntegracaoExternaTipoDAO : Abstract_DAL<SYS_IntegracaoExternaTipo>
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
		protected override void ParamCarregar(QuerySelectStoredProcedure qs, SYS_IntegracaoExternaTipo entity)
		{
			if (entity != null & qs != null)
            {
			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@iet_id";
			Param.Size = 1;
			Param.Value = entity.iet_id;
			qs.Parameters.Add(Param);


			}
		}
		
		/// <summary>
		/// Configura os parametros do metodo de Inserir.
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure.</param>
        /// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
		protected override void ParamInserir(QuerySelectStoredProcedure qs, SYS_IntegracaoExternaTipo entity)
		{
            if (entity != null & qs != null)
            {
                Param = qs.NewParameter();
                Param.DbType = DbType.Byte;
                Param.ParameterName = "@iet_id";
                Param.Size = 1;
                Param.Value = entity.iet_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@iet_descricao";
                Param.Size = 128;
                Param.Value = entity.iet_descricao;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@iet_qtdeDiasAutenticacao";
                Param.Size = 4;
                Param.Value = entity.iet_qtdeDiasAutenticacao;
                qs.Parameters.Add(Param);


            }
		}
		
		/// <summary>
		/// Configura os parametros do metodo de Alterar.
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure.</param>
        /// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
		protected override void ParamAlterar(QueryStoredProcedure qs, SYS_IntegracaoExternaTipo entity)
		{
            if (entity != null & qs != null)
            {
                Param = qs.NewParameter();
                Param.DbType = DbType.Byte;
                Param.ParameterName = "@iet_id";
                Param.Size = 1;
                Param.Value = entity.iet_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@iet_descricao";
                Param.Size = 128;
                Param.Value = entity.iet_descricao;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@iet_qtdeDiasAutenticacao";
                Param.Size = 4;
                Param.Value = entity.iet_qtdeDiasAutenticacao;
                qs.Parameters.Add(Param);


            }
		}

		/// <summary>
		/// Configura os parametros do metodo de Deletar.
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure.</param>
        /// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
		protected override void ParamDeletar(QueryStoredProcedure qs, SYS_IntegracaoExternaTipo entity)
		{
			if (entity != null & qs != null)
            {
			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@iet_id";
			Param.Size = 1;
			Param.Value = entity.iet_id;
			qs.Parameters.Add(Param);


			}
		}
		
		/// <summary>
		/// Recebe o valor do auto incremento e coloca na propriedade.
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure.</param>
		/// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
		/// <returns>TRUE - Se entity.ParametroId > 0</returns>
		protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, SYS_IntegracaoExternaTipo entity)
		{
            entity.iet_id = Convert.ToInt16(qs.Return.Rows[0][0]);
			return (entity.iet_id > 0);

			/*if (entity != null & qs != null)
            {
				return true;
			}
			return false;*/
		}		
	}
}