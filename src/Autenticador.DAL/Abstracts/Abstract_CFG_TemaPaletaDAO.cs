/*
	Classe gerada automaticamente pelo Code Creator
*/

namespace Autenticador.DAL.Abstracts
{
    using System;
    using System.Data;
    using Autenticador.Entities;
    using CoreLibrary.Data.Common;
    using CoreLibrary.Data.Common.Abstracts;
	
	/// <summary>
	/// Classe abstrata de CFG_TemaPaleta.
	/// </summary>
	public abstract class Abstract_CFG_TemaPaletaDAO : Abstract_DAL<CFG_TemaPaleta>
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
		protected override void ParamCarregar(QuerySelectStoredProcedure qs, CFG_TemaPaleta entity)
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
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@tpl_id";
			Param.Size = 4;
			Param.Value = entity.tpl_id;
			qs.Parameters.Add(Param);


			}
		}
		
		/// <summary>
		/// Configura os parametros do metodo de Inserir.
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure.</param>
        /// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
		protected override void ParamInserir(QuerySelectStoredProcedure qs, CFG_TemaPaleta entity)
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
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@tpl_id";
			Param.Size = 4;
			Param.Value = entity.tpl_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@tpl_nome";
			Param.Size = 100;
			Param.Value = entity.tpl_nome;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@tpl_caminhoCSS";
			Param.Size = 2000;
			Param.Value = entity.tpl_caminhoCSS;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@tpl_imagemExemploTema";
			Param.Size = 2000;
				if(!string.IsNullOrEmpty(entity.tpl_imagemExemploTema))
				{
					Param.Value = entity.tpl_imagemExemploTema;
				}
				else
				{
					Param.Value = DBNull.Value;
				}
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@tpl_situacao";
			Param.Size = 1;
			Param.Value = entity.tpl_situacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@tpl_dataCriacao";
			Param.Size = 16;
			Param.Value = entity.tpl_dataCriacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@tpl_dataAlteracao";
			Param.Size = 16;
			Param.Value = entity.tpl_dataAlteracao;
			qs.Parameters.Add(Param);


			}
		}
		
		/// <summary>
		/// Configura os parametros do metodo de Alterar.
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure.</param>
        /// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
		protected override void ParamAlterar(QueryStoredProcedure qs, CFG_TemaPaleta entity)
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
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@tpl_id";
			Param.Size = 4;
			Param.Value = entity.tpl_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@tpl_nome";
			Param.Size = 100;
			Param.Value = entity.tpl_nome;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@tpl_caminhoCSS";
			Param.Size = 2000;
			Param.Value = entity.tpl_caminhoCSS;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@tpl_imagemExemploTema";
			Param.Size = 2000;
				if(!string.IsNullOrEmpty(entity.tpl_imagemExemploTema))
				{
					Param.Value = entity.tpl_imagemExemploTema;
				}
				else
				{
					Param.Value = DBNull.Value;
				}
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@tpl_situacao";
			Param.Size = 1;
			Param.Value = entity.tpl_situacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@tpl_dataCriacao";
			Param.Size = 16;
			Param.Value = entity.tpl_dataCriacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@tpl_dataAlteracao";
			Param.Size = 16;
			Param.Value = entity.tpl_dataAlteracao;
			qs.Parameters.Add(Param);


			}
		}

		/// <summary>
		/// Configura os parametros do metodo de Deletar.
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure.</param>
        /// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
		protected override void ParamDeletar(QueryStoredProcedure qs, CFG_TemaPaleta entity)
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
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@tpl_id";
			Param.Size = 4;
			Param.Value = entity.tpl_id;
			qs.Parameters.Add(Param);


			}
		}
		
		/// <summary>
		/// Recebe o valor do auto incremento e coloca na propriedade.
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure.</param>
		/// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
		/// <returns>TRUE - Se entity.ParametroId > 0</returns>
		protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, CFG_TemaPaleta entity)
		{
			if (entity != null & qs != null)
            {
return true;
			}

			return false;
		}		
	}
}