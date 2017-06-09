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
	/// Classe abstrata de SYS_Usuario
	/// </summary>
	public abstract class Abstract_SYS_UsuarioDAO : Abstract_DAL<SYS_Usuario>
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
		protected override void ParamCarregar(QuerySelectStoredProcedure qs, SYS_Usuario entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@usu_id";
			Param.Size = 16;
			Param.Value = entity.usu_id;
			qs.Parameters.Add(Param);


		}
		
		/// <summary>
		/// Configura os parametros do metodo de Inserir
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamInserir(QuerySelectStoredProcedure qs, SYS_Usuario entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@usu_id";
			Param.Size = 16;
			Param.Value = entity.usu_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@usu_login";
			Param.Size = 500;
			if( !string.IsNullOrEmpty(entity.usu_login) )
				Param.Value = entity.usu_login;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@usu_dominio";
			Param.Size = 100;
			if( !string.IsNullOrEmpty(entity.usu_dominio) )
				Param.Value = entity.usu_dominio;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@usu_email";
			Param.Size = 500;
			if( !string.IsNullOrEmpty(entity.usu_email) )
				Param.Value = entity.usu_email;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@usu_senha";
			Param.Size = 256;
			if( !string.IsNullOrEmpty(entity.usu_senha) )
				Param.Value = entity.usu_senha;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
			Param.ParameterName = "@usu_criptografia";
			Param.Size = 1;
			if( entity.usu_criptografia > 0  )
				Param.Value = entity.usu_criptografia;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@usu_situacao";
			Param.Size = 1;
			Param.Value = entity.usu_situacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@usu_dataCriacao";
			Param.Size = 16;
			Param.Value = entity.usu_dataCriacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@usu_dataAlteracao";
			Param.Size = 16;
			Param.Value = entity.usu_dataAlteracao;
			qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@usu_dataAlteracaoSenha";
            Param.Size = 16;
            Param.Value = entity.usu_dataAlteracao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@pes_id";
			Param.Size = 16;
				Param.Value = entity.pes_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@usu_integridade";
			Param.Size = 4;
			Param.Value = entity.usu_integridade;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@ent_id";
			Param.Size = 16;
			Param.Value = entity.ent_id;
			qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@usu_integracaoAD";
            Param.Size = 1;
            Param.Value = entity.usu_integracaoAD;
            qs.Parameters.Add(Param);

           
            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@usu_integracaoExterna";
            Param.Size = 1;
            if (entity.usu_integracaoExterna > 0)
            {
                Param.Value = entity.usu_integracaoExterna;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);
        }
		
		/// <summary>
		/// Configura os parametros do metodo de Alterar
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamAlterar(QueryStoredProcedure qs, SYS_Usuario entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@usu_id";
			Param.Size = 16;
			Param.Value = entity.usu_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@usu_login";
			Param.Size = 500;
			if( !string.IsNullOrEmpty(entity.usu_login) )
				Param.Value = entity.usu_login;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@usu_dominio";
			Param.Size = 100;
			if( !string.IsNullOrEmpty(entity.usu_dominio) )
				Param.Value = entity.usu_dominio;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@usu_email";
			Param.Size = 500;
			if( !string.IsNullOrEmpty(entity.usu_email) )
				Param.Value = entity.usu_email;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@usu_senha";
			Param.Size = 256;
			if( !string.IsNullOrEmpty(entity.usu_senha) )
				Param.Value = entity.usu_senha;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
			Param.ParameterName = "@usu_criptografia";
			Param.Size = 1;
			if( entity.usu_criptografia > 0  )
				Param.Value = entity.usu_criptografia;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@usu_situacao";
			Param.Size = 1;
			Param.Value = entity.usu_situacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@usu_dataCriacao";
			Param.Size = 16;
			Param.Value = entity.usu_dataCriacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@usu_dataAlteracao";
			Param.Size = 16;
			Param.Value = entity.usu_dataAlteracao;
			qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@usu_dataAlteracaoSenha";
            Param.Size = 16;
            Param.Value = entity.usu_dataAlteracao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@pes_id";
			Param.Size = 16;
				Param.Value = entity.pes_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@usu_integridade";
			Param.Size = 4;
			Param.Value = entity.usu_integridade;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@ent_id";
			Param.Size = 16;
			Param.Value = entity.ent_id;
			qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@usu_integracaoAD";
            Param.Size = 1;
            Param.Value = entity.usu_integracaoAD;
            qs.Parameters.Add(Param);

            
            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@usu_integracaoExterna";
            Param.Size = 1;
            if (entity.usu_integracaoExterna > 0)
            {
                Param.Value = entity.usu_integracaoExterna;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);
        }

		/// <summary>
		/// Configura os parametros do metodo de Deletar
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamDeletar(QueryStoredProcedure qs, SYS_Usuario entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@usu_id";
			Param.Size = 16;
			Param.Value = entity.usu_id;
			qs.Parameters.Add(Param);


		}
		
		/// <summary>
		/// Recebe o valor do auto incremento e coloca na propriedade 
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, SYS_Usuario entity)
		{
            entity.usu_id = new Guid(qs.Return.Rows[0][0].ToString());
            return (entity.usu_id != Guid.Empty);
		}		
	}
}

