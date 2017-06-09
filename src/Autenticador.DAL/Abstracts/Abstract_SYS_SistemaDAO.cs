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
	/// Classe abstrata de SYS_Sistema
	/// </summary>
	public abstract class Abstract_SYS_SistemaDAO : Abstract_DAL<SYS_Sistema>
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
		protected override void ParamCarregar(QuerySelectStoredProcedure qs, SYS_Sistema entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@sis_id";
			Param.Size = 4;
			Param.Value = entity.sis_id;
			qs.Parameters.Add(Param);


		}
		
		/// <summary>
		/// Configura os parametros do metodo de Inserir
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamInserir(QuerySelectStoredProcedure qs, SYS_Sistema entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@sis_nome";
			Param.Size = 100;
			Param.Value = entity.sis_nome;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
            Param.DbType = DbType.String;
			Param.ParameterName = "@sis_descricao";
			Param.Size = 2147483647;
			if( !string.IsNullOrEmpty(entity.sis_descricao) )
				Param.Value = entity.sis_descricao;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@sis_caminho";
			Param.Size = 2000;
			if( !string.IsNullOrEmpty(entity.sis_caminho) )
				Param.Value = entity.sis_caminho;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@sis_caminhoLogout";
			Param.Size = 2000;
			if( !string.IsNullOrEmpty(entity.sis_caminhoLogout) )
				Param.Value = entity.sis_caminhoLogout;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@sis_urlImagem";
			Param.Size = 2000;
			if( !string.IsNullOrEmpty(entity.sis_urlImagem) )
				Param.Value = entity.sis_urlImagem;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@sis_urlLogoCabecalho";
			Param.Size = 2000;
			if( !string.IsNullOrEmpty(entity.sis_urlLogoCabecalho) )
				Param.Value = entity.sis_urlLogoCabecalho;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@sis_tipoAutenticacao";
			Param.Size = 1;
			if( entity.sis_tipoAutenticacao > 0  )
				Param.Value = entity.sis_tipoAutenticacao;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@sis_urlIntegracao";
			Param.Size = 200;
			if( !string.IsNullOrEmpty(entity.sis_urlIntegracao) )
				Param.Value = entity.sis_urlIntegracao;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@sis_situacao";
			Param.Size = 1;
			Param.Value = entity.sis_situacao;
			qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Boolean;
            Param.ParameterName = "@sis_ocultarLogo";
            Param.Size = 1;
            Param.Value = entity.sis_ocultarLogo;
            qs.Parameters.Add(Param);


		}
		
		/// <summary>
		/// Configura os parametros do metodo de Alterar
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamAlterar(QueryStoredProcedure qs, SYS_Sistema entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@sis_id";
			Param.Size = 4;
			Param.Value = entity.sis_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@sis_nome";
			Param.Size = 100;
			Param.Value = entity.sis_nome;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
            Param.DbType = DbType.String;
			Param.ParameterName = "@sis_descricao";
			Param.Size = 2147483647;
			if( !string.IsNullOrEmpty(entity.sis_descricao) )
				Param.Value = entity.sis_descricao;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@sis_caminho";
			Param.Size = 2000;
			if( !string.IsNullOrEmpty(entity.sis_caminho) )
				Param.Value = entity.sis_caminho;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@sis_caminhoLogout";
			Param.Size = 2000;
			if( !string.IsNullOrEmpty(entity.sis_caminhoLogout) )
				Param.Value = entity.sis_caminhoLogout;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@sis_urlImagem";
			Param.Size = 2000;
			if( !string.IsNullOrEmpty(entity.sis_urlImagem) )
				Param.Value = entity.sis_urlImagem;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@sis_urlLogoCabecalho";
			Param.Size = 2000;
			if( !string.IsNullOrEmpty(entity.sis_urlLogoCabecalho) )
				Param.Value = entity.sis_urlLogoCabecalho;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@sis_tipoAutenticacao";
			Param.Size = 1;
			if( entity.sis_tipoAutenticacao > 0  )
				Param.Value = entity.sis_tipoAutenticacao;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@sis_urlIntegracao";
			Param.Size = 200;
			if( !string.IsNullOrEmpty(entity.sis_urlIntegracao) )
				Param.Value = entity.sis_urlIntegracao;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@sis_situacao";
			Param.Size = 1;
			Param.Value = entity.sis_situacao;
			qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Boolean;
            Param.ParameterName = "@sis_ocultarLogo";
            Param.Size = 1;
            Param.Value = entity.sis_ocultarLogo;
            qs.Parameters.Add(Param);

		}

		/// <summary>
		/// Configura os parametros do metodo de Deletar
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamDeletar(QueryStoredProcedure qs, SYS_Sistema entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@sis_id";
			Param.Size = 4;
			Param.Value = entity.sis_id;
			qs.Parameters.Add(Param);


		}
		
		/// <summary>
		/// Recebe o valor do auto incremento e coloca na propriedade 
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, SYS_Sistema entity)
		{
			entity.sis_id = Convert.ToInt32(qs.Return.Rows[0][0]);
			return (entity.sis_id > 0);
		}		
	}
}

