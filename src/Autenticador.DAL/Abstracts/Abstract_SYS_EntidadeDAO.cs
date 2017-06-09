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
	/// Classe abstrata de SYS_Entidade
	/// </summary>
	public abstract class Abstract_SYS_EntidadeDAO : Abstract_DAL<SYS_Entidade>
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
		protected override void ParamCarregar(QuerySelectStoredProcedure qs, SYS_Entidade entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@ent_id";
			Param.Size = 16;
			Param.Value = entity.ent_id;
			qs.Parameters.Add(Param);


		}
		
		/// <summary>
		/// Configura os parametros do metodo de Inserir
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamInserir(QuerySelectStoredProcedure qs, SYS_Entidade entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@ent_id";
			Param.Size = 16;
			Param.Value = entity.ent_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@ten_id";
			Param.Size = 16;
			Param.Value = entity.ten_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@ent_codigo";
			Param.Size = 20;
			if( !string.IsNullOrEmpty(entity.ent_codigo) )
				Param.Value = entity.ent_codigo;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@ent_nomeFantasia";
			Param.Size = 200;
			if( !string.IsNullOrEmpty(entity.ent_nomeFantasia) )
				Param.Value = entity.ent_nomeFantasia;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@ent_razaoSocial";
			Param.Size = 200;
			if( !string.IsNullOrEmpty(entity.ent_razaoSocial) )
				Param.Value = entity.ent_razaoSocial;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@ent_sigla";
			Param.Size = 50;
			if( !string.IsNullOrEmpty(entity.ent_sigla) )
				Param.Value = entity.ent_sigla;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@ent_cnpj";
			Param.Size = 14;
			if( !string.IsNullOrEmpty(entity.ent_cnpj) )
				Param.Value = entity.ent_cnpj;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@ent_inscricaoMunicipal";
			Param.Size = 20;
			if( !string.IsNullOrEmpty(entity.ent_inscricaoMunicipal) )
				Param.Value = entity.ent_inscricaoMunicipal;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@ent_inscricaoEstadual";
			Param.Size = 20;
			if( !string.IsNullOrEmpty(entity.ent_inscricaoEstadual) )
				Param.Value = entity.ent_inscricaoEstadual;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@ent_idSuperior";
			Param.Size = 16;
				Param.Value = entity.ent_idSuperior;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@ent_situacao";
			Param.Size = 1;
			Param.Value = entity.ent_situacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@ent_dataCriacao";
			Param.Size = 16;
			Param.Value = entity.ent_dataCriacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@ent_dataAlteracao";
			Param.Size = 16;
			Param.Value = entity.ent_dataAlteracao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@ent_integridade";
			Param.Size = 4;
			Param.Value = entity.ent_integridade;
			qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@ent_urlAcesso";
            Param.Size = 200;
            if (!string.IsNullOrEmpty(entity.ent_urlAcesso))
            {
                Param.Value = entity.ent_urlAcesso;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Boolean;
            Param.ParameterName = "@ent_exibeLogoCliente";
            Param.Size = 1;
            Param.Value = entity.ent_exibeLogoCliente;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@ent_logoCliente";
            Param.Size = 2000;
            if (!string.IsNullOrEmpty(entity.ent_logoCliente))
            {
                Param.Value = entity.ent_logoCliente;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@tep_id";
            Param.Size = 4;
            if (entity.tep_id > 0)
            {
                Param.Value = entity.tep_id;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@tpl_id";
            Param.Size = 4;
            if (entity.tpl_id > 0)
            {
                Param.Value = entity.tpl_id;
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
		protected override void ParamAlterar(QueryStoredProcedure qs, SYS_Entidade entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@ent_id";
			Param.Size = 16;
			Param.Value = entity.ent_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@ten_id";
			Param.Size = 16;
			Param.Value = entity.ten_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@ent_codigo";
			Param.Size = 20;
			if( !string.IsNullOrEmpty(entity.ent_codigo) )
				Param.Value = entity.ent_codigo;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@ent_nomeFantasia";
			Param.Size = 200;
			if( !string.IsNullOrEmpty(entity.ent_nomeFantasia) )
				Param.Value = entity.ent_nomeFantasia;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@ent_razaoSocial";
			Param.Size = 200;
			if( !string.IsNullOrEmpty(entity.ent_razaoSocial) )
				Param.Value = entity.ent_razaoSocial;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@ent_sigla";
			Param.Size = 50;
			if( !string.IsNullOrEmpty(entity.ent_sigla) )
				Param.Value = entity.ent_sigla;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@ent_cnpj";
			Param.Size = 14;
			if( !string.IsNullOrEmpty(entity.ent_cnpj) )
				Param.Value = entity.ent_cnpj;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@ent_inscricaoMunicipal";
			Param.Size = 20;
			if( !string.IsNullOrEmpty(entity.ent_inscricaoMunicipal) )
				Param.Value = entity.ent_inscricaoMunicipal;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@ent_inscricaoEstadual";
			Param.Size = 20;
			if( !string.IsNullOrEmpty(entity.ent_inscricaoEstadual) )
				Param.Value = entity.ent_inscricaoEstadual;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@ent_idSuperior";
			Param.Size = 16;
				Param.Value = entity.ent_idSuperior;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@ent_situacao";
			Param.Size = 1;
			Param.Value = entity.ent_situacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@ent_dataCriacao";
			Param.Size = 16;
			Param.Value = entity.ent_dataCriacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@ent_dataAlteracao";
			Param.Size = 16;
			Param.Value = entity.ent_dataAlteracao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@ent_integridade";
			Param.Size = 4;
			Param.Value = entity.ent_integridade;
			qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@ent_urlAcesso";
            Param.Size = 200;
            if (!string.IsNullOrEmpty(entity.ent_urlAcesso))
            {
                Param.Value = entity.ent_urlAcesso;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Boolean;
            Param.ParameterName = "@ent_exibeLogoCliente";
            Param.Size = 1;
            Param.Value = entity.ent_exibeLogoCliente;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@ent_logoCliente";
            Param.Size = 2000;
            if (!string.IsNullOrEmpty(entity.ent_logoCliente))
            {
                Param.Value = entity.ent_logoCliente;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@tep_id";
            Param.Size = 4;
            if (entity.tep_id > 0)
            {
                Param.Value = entity.tep_id;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@tpl_id";
            Param.Size = 4;
            if (entity.tpl_id > 0)
            {
                Param.Value = entity.tpl_id;
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
		protected override void ParamDeletar(QueryStoredProcedure qs, SYS_Entidade entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@ent_id";
			Param.Size = 16;
			Param.Value = entity.ent_id;
			qs.Parameters.Add(Param);


		}
		
		/// <summary>
		/// Recebe o valor do auto incremento e coloca na propriedade 
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, SYS_Entidade entity)
		{
            entity.ent_id = new Guid(qs.Return.Rows[0][0].ToString());
            return (entity.ent_id != Guid.Empty); 
		}		
	}
}

