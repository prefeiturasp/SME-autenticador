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
	/// Classe abstrata de SYS_ModuloSiteMap
	/// </summary>
	public abstract class Abstract_SYS_ModuloSiteMapDAO : Abstract_DAL<SYS_ModuloSiteMap>
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
		protected override void ParamCarregar(QuerySelectStoredProcedure qs, SYS_ModuloSiteMap entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@sis_id";
			Param.Size = 4;
			Param.Value = entity.sis_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@mod_id";
			Param.Size = 4;
			Param.Value = entity.mod_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@msm_id";
			Param.Size = 4;
			Param.Value = entity.msm_id;
			qs.Parameters.Add(Param);


		}
		
		/// <summary>
		/// Configura os parametros do metodo de Inserir
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamInserir(QuerySelectStoredProcedure qs, SYS_ModuloSiteMap entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@sis_id";
			Param.Size = 4;
			Param.Value = entity.sis_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@mod_id";
			Param.Size = 4;
			Param.Value = entity.mod_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@msm_id";
			Param.Size = 4;
			Param.Value = entity.msm_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@msm_nome";
			Param.Size = 50;
			Param.Value = entity.msm_nome;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@msm_descricao";
			Param.Size = 1000;
			if( !string.IsNullOrEmpty(entity.msm_descricao) )
				Param.Value = entity.msm_descricao;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@msm_url";
			Param.Size = 500;
			if( !string.IsNullOrEmpty(entity.msm_url) )
				Param.Value = entity.msm_url;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.String;
			Param.ParameterName = "@msm_informacoes";			
			if( !string.IsNullOrEmpty(entity.msm_informacoes) )
				Param.Value = entity.msm_informacoes;
			else
				Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@msm_urlHelp";
            Param.Size = 500;
            if (!string.IsNullOrEmpty(entity.msm_urlHelp))
                Param.Value = entity.msm_url;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

		}
		
		/// <summary>
		/// Configura os parametros do metodo de Alterar
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamAlterar(QueryStoredProcedure qs, SYS_ModuloSiteMap entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@sis_id";
			Param.Size = 4;
			Param.Value = entity.sis_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@mod_id";
			Param.Size = 4;
			Param.Value = entity.mod_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@msm_id";
			Param.Size = 4;
			Param.Value = entity.msm_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@msm_nome";
			Param.Size = 50;
			Param.Value = entity.msm_nome;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@msm_descricao";
			Param.Size = 1000;
			if( !string.IsNullOrEmpty(entity.msm_descricao) )
				Param.Value = entity.msm_descricao;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@msm_url";
			Param.Size = 500;
			if( !string.IsNullOrEmpty(entity.msm_url) )
				Param.Value = entity.msm_url;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.String;
			Param.ParameterName = "@msm_informacoes";			
			if( !string.IsNullOrEmpty(entity.msm_informacoes) )
				Param.Value = entity.msm_informacoes;
			else
				Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@msm_urlHelp";
            Param.Size = 500;
            if (!string.IsNullOrEmpty(entity.msm_urlHelp))
                Param.Value = entity.msm_urlHelp;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

		}

		/// <summary>
		/// Configura os parametros do metodo de Deletar
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamDeletar(QueryStoredProcedure qs, SYS_ModuloSiteMap entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@sis_id";
			Param.Size = 4;
			Param.Value = entity.sis_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@mod_id";
			Param.Size = 4;
			Param.Value = entity.mod_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@msm_id";
			Param.Size = 4;
			Param.Value = entity.msm_id;
			qs.Parameters.Add(Param);


		}
		
		/// <summary>
		/// Recebe o valor do auto incremento e coloca na propriedade 
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, SYS_ModuloSiteMap entity)
		{
            entity.msm_id = Convert.ToInt32(qs.Return.Rows[0][0]);
            return (entity.msm_id > 0);
		}		
	}
}

