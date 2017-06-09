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
	/// Classe abstrata de CFG_TemaPadrao.
	/// </summary>
	public abstract class Abstract_CFG_TemaPadraoDAO : Abstract_DAL<CFG_TemaPadrao>
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
		protected override void ParamCarregar(QuerySelectStoredProcedure qs, CFG_TemaPadrao entity)
		{
			if (entity != null & qs != null)
            {
			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@tep_id";
			Param.Size = 4;
			Param.Value = entity.tep_id;
			qs.Parameters.Add(Param);


			}
		}
		
		/// <summary>
		/// Configura os parametros do metodo de Inserir.
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure.</param>
        /// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
		protected override void ParamInserir(QuerySelectStoredProcedure qs, CFG_TemaPadrao entity)
		{
			if (entity != null & qs != null)
            {
							Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@tep_nome";
			Param.Size = 100;
			Param.Value = entity.tep_nome;
			qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@tep_descricao";
            Param.Size = 200;
            if (!string.IsNullOrEmpty(entity.tep_descricao))
            {
                Param.Value = entity.tep_descricao;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@tep_tipoMenu";
			Param.Size = 1;
			Param.Value = entity.tep_tipoMenu;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Boolean;
			Param.ParameterName = "@tep_exibeLinkLogin";
			Param.Size = 1;
			Param.Value = entity.tep_exibeLinkLogin;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@tep_tipoLogin";
			Param.Size = 1;
			Param.Value = entity.tep_tipoLogin;
			qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Boolean;
            Param.ParameterName = "@tep_exibeLogoCliente";
            Param.Size = 1;
            Param.Value = entity.tep_exibeLogoCliente;
            qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@tep_situacao";
			Param.Size = 1;
			Param.Value = entity.tep_situacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@tep_dataCriacao";
			Param.Size = 16;
			Param.Value = entity.tep_dataCriacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@tep_dataAlteracao";
			Param.Size = 16;
			Param.Value = entity.tep_dataAlteracao;
			qs.Parameters.Add(Param);


			}
		}
		
		/// <summary>
		/// Configura os parametros do metodo de Alterar.
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure.</param>
        /// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
		protected override void ParamAlterar(QueryStoredProcedure qs, CFG_TemaPadrao entity)
		{
			if (entity != null & qs != null)
            {
			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@tep_id";
			Param.Size = 4;
			Param.Value = entity.tep_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@tep_nome";
			Param.Size = 100;
			Param.Value = entity.tep_nome;
			qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@tep_descricao";
            Param.Size = 200;
            if (!string.IsNullOrEmpty(entity.tep_descricao))
            {
                Param.Value = entity.tep_descricao;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);        

			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@tep_tipoMenu";
			Param.Size = 1;
			Param.Value = entity.tep_tipoMenu;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Boolean;
			Param.ParameterName = "@tep_exibeLinkLogin";
			Param.Size = 1;
			Param.Value = entity.tep_exibeLinkLogin;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@tep_tipoLogin";
			Param.Size = 1;
			Param.Value = entity.tep_tipoLogin;
			qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Boolean;
            Param.ParameterName = "@tep_exibeLogoCliente";
            Param.Size = 1;
            Param.Value = entity.tep_exibeLogoCliente;
            qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@tep_situacao";
			Param.Size = 1;
			Param.Value = entity.tep_situacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@tep_dataCriacao";
			Param.Size = 16;
			Param.Value = entity.tep_dataCriacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@tep_dataAlteracao";
			Param.Size = 16;
			Param.Value = entity.tep_dataAlteracao;
			qs.Parameters.Add(Param);


			}
		}

		/// <summary>
		/// Configura os parametros do metodo de Deletar.
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure.</param>
        /// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
		protected override void ParamDeletar(QueryStoredProcedure qs, CFG_TemaPadrao entity)
		{
			if (entity != null & qs != null)
            {
			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@tep_id";
			Param.Size = 4;
			Param.Value = entity.tep_id;
			qs.Parameters.Add(Param);


			}
		}
		
		/// <summary>
		/// Recebe o valor do auto incremento e coloca na propriedade.
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure.</param>
		/// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
		/// <returns>TRUE - Se entity.ParametroId > 0</returns>
		protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, CFG_TemaPadrao entity)
		{
			if (entity != null & qs != null)
            {
			entity.tep_id = Convert.ToInt32(qs.Return.Rows[0][0]);
			return (entity.tep_id > 0);
			}

			return false;
		}		
	}
}