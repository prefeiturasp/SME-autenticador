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
	/// Classe abstrata de SYS_UnidadeAdministrativa
	/// </summary>
	public abstract class Abstract_SYS_UnidadeAdministrativaDAO : Abstract_DAL<SYS_UnidadeAdministrativa>
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
		protected override void ParamCarregar(QuerySelectStoredProcedure qs, SYS_UnidadeAdministrativa entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@ent_id";
			Param.Size = 16;
			Param.Value = entity.ent_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@uad_id";
			Param.Size = 16;
			Param.Value = entity.uad_id;
			qs.Parameters.Add(Param);


		}
		
		/// <summary>
		/// Configura os parametros do metodo de Inserir
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamInserir(QuerySelectStoredProcedure qs, SYS_UnidadeAdministrativa entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@ent_id";
			Param.Size = 16;
			Param.Value = entity.ent_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@uad_id";
			Param.Size = 16;
			Param.Value = entity.uad_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@tua_id";
			Param.Size = 16;
			Param.Value = entity.tua_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@uad_codigo";
			Param.Size = 20;
			if( !string.IsNullOrEmpty(entity.uad_codigo) )
				Param.Value = entity.uad_codigo;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@uad_nome";
			Param.Size = 200;
			Param.Value = entity.uad_nome;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@uad_sigla";
			Param.Size = 50;
			if( !string.IsNullOrEmpty(entity.uad_sigla) )
				Param.Value = entity.uad_sigla;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@uad_idSuperior";
			Param.Size = 16;
				Param.Value = entity.uad_idSuperior;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@uad_situacao";
			Param.Size = 1;
			Param.Value = entity.uad_situacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@uad_dataCriacao";
			Param.Size = 16;
			Param.Value = entity.uad_dataCriacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@uad_dataAlteracao";
			Param.Size = 16;
			Param.Value = entity.uad_dataAlteracao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@uad_integridade";
			Param.Size = 4;
			Param.Value = entity.uad_integridade;
			qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@uad_codigoIntegracao";
            Param.Size = 50;
            if (!string.IsNullOrEmpty(entity.uad_codigoIntegracao))
                Param.Value = entity.uad_codigoIntegracao;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

		}
		
		/// <summary>
		/// Configura os parametros do metodo de Alterar
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamAlterar(QueryStoredProcedure qs, SYS_UnidadeAdministrativa entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@ent_id";
			Param.Size = 16;
			Param.Value = entity.ent_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@uad_id";
			Param.Size = 16;
			Param.Value = entity.uad_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@tua_id";
			Param.Size = 16;
			Param.Value = entity.tua_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@uad_codigo";
			Param.Size = 20;
			if( !string.IsNullOrEmpty(entity.uad_codigo) )
				Param.Value = entity.uad_codigo;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@uad_nome";
			Param.Size = 200;
			Param.Value = entity.uad_nome;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@uad_sigla";
			Param.Size = 50;
			if( !string.IsNullOrEmpty(entity.uad_sigla) )
				Param.Value = entity.uad_sigla;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@uad_idSuperior";
			Param.Size = 16;
				Param.Value = entity.uad_idSuperior;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@uad_situacao";
			Param.Size = 1;
			Param.Value = entity.uad_situacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@uad_dataCriacao";
			Param.Size = 16;
			Param.Value = entity.uad_dataCriacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@uad_dataAlteracao";
			Param.Size = 16;
			Param.Value = entity.uad_dataAlteracao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@uad_integridade";
			Param.Size = 4;
			Param.Value = entity.uad_integridade;
			qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@uad_codigoIntegracao";
            Param.Size = 50;
            if (!string.IsNullOrEmpty(entity.uad_codigoIntegracao))
                Param.Value = entity.uad_codigoIntegracao;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

		}

		/// <summary>
		/// Configura os parametros do metodo de Deletar
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamDeletar(QueryStoredProcedure qs, SYS_UnidadeAdministrativa entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@ent_id";
			Param.Size = 16;
			Param.Value = entity.ent_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@uad_id";
			Param.Size = 16;
			Param.Value = entity.uad_id;
			qs.Parameters.Add(Param);


		}
		
		/// <summary>
		/// Recebe o valor do auto incremento e coloca na propriedade 
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, SYS_UnidadeAdministrativa entity)
		{
            entity.uad_id = new Guid(qs.Return.Rows[0][0].ToString());
            return (entity.uad_id != Guid.Empty);
		}		
	}
}

