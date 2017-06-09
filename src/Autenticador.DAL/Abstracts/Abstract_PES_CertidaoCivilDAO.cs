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
	/// Classe abstrata de PES_CertidaoCivil
	/// </summary>
	public abstract class Abstract_PES_CertidaoCivilDAO : Abstract_DAL<PES_CertidaoCivil>
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
		protected override void ParamCarregar(QuerySelectStoredProcedure qs, PES_CertidaoCivil entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@pes_id";
			Param.Size = 16;
			Param.Value = entity.pes_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@ctc_id";
			Param.Size = 16;
			Param.Value = entity.ctc_id;
			qs.Parameters.Add(Param);


		}
		
		/// <summary>
		/// Configura os parametros do metodo de Inserir
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamInserir(QuerySelectStoredProcedure qs, PES_CertidaoCivil entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@pes_id";
			Param.Size = 16;
			Param.Value = entity.pes_id;
			qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@ctc_id";
            Param.Size = 16;
            Param.Value = entity.ctc_id;
            qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@ctc_tipo";
			Param.Size = 1;
			Param.Value = entity.ctc_tipo;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@ctc_numeroTermo";
			Param.Size = 50;
			if( !string.IsNullOrEmpty(entity.ctc_numeroTermo) )
				Param.Value = entity.ctc_numeroTermo;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@ctc_folha";
			Param.Size = 20;
			if( !string.IsNullOrEmpty(entity.ctc_folha) )
				Param.Value = entity.ctc_folha;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@ctc_livro";
			Param.Size = 20;
			if( !string.IsNullOrEmpty(entity.ctc_livro) )
				Param.Value = entity.ctc_livro;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@ctc_dataEmissao";
			Param.Size = 20;
			if( entity.ctc_dataEmissao!= new DateTime() )
				Param.Value = entity.ctc_dataEmissao;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@ctc_nomeCartorio";
			Param.Size = 200;
			if( !string.IsNullOrEmpty(entity.ctc_nomeCartorio) )
				Param.Value = entity.ctc_nomeCartorio;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@cid_idCartorio";
            Param.Size = 16;
            if (entity.cid_idCartorio != Guid.Empty)
                Param.Value = entity.cid_idCartorio;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@unf_idCartorio";
            Param.Size = 16;
            if (entity.unf_idCartorio != Guid.Empty)
                Param.Value = entity.unf_idCartorio;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@ctc_distritoCartorio";
            Param.Size = 100;
            if (!string.IsNullOrEmpty(entity.ctc_distritoCartorio))
                Param.Value = entity.ctc_distritoCartorio;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@ctc_situacao";
			Param.Size = 1;
			Param.Value = entity.ctc_situacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@ctc_dataCriacao";
			Param.Size = 16;
			Param.Value = entity.ctc_dataCriacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@ctc_dataAlteracao";
			Param.Size = 16;
			Param.Value = entity.ctc_dataAlteracao;
			qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@ctc_matricula";
            Param.Size = 32;
            if (!string.IsNullOrEmpty(entity.ctc_matricula))
                Param.Value = entity.ctc_matricula;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Boolean;
            Param.ParameterName = "@ctc_gemeo";
            Param.Size = 1;
            Param.Value = entity.ctc_gemeo;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Boolean;
            Param.ParameterName = "@ctc_modeloNovo";
            Param.Size = 1;
            Param.Value = entity.ctc_modeloNovo;
            qs.Parameters.Add(Param);

        }
		
		/// <summary>
		/// Configura os parametros do metodo de Alterar
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamAlterar(QueryStoredProcedure qs, PES_CertidaoCivil entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@pes_id";
			Param.Size = 16;
			Param.Value = entity.pes_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@ctc_id";
			Param.Size = 16;
			Param.Value = entity.ctc_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@ctc_tipo";
			Param.Size = 1;
			Param.Value = entity.ctc_tipo;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@ctc_numeroTermo";
			Param.Size = 50;
			if( !string.IsNullOrEmpty(entity.ctc_numeroTermo) )
				Param.Value = entity.ctc_numeroTermo;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@ctc_folha";
			Param.Size = 20;
			if( !string.IsNullOrEmpty(entity.ctc_folha) )
				Param.Value = entity.ctc_folha;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@ctc_livro";
			Param.Size = 20;
			if( !string.IsNullOrEmpty(entity.ctc_livro) )
				Param.Value = entity.ctc_livro;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
			Param.ParameterName = "@ctc_dataEmissao";
			Param.Size = 20;
			if( entity.ctc_dataEmissao!= new DateTime() )
				Param.Value = entity.ctc_dataEmissao;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@ctc_nomeCartorio";
			Param.Size = 200;
			if( !string.IsNullOrEmpty(entity.ctc_nomeCartorio) )
				Param.Value = entity.ctc_nomeCartorio;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@cid_idCartorio";
            Param.Size = 16;
            if (entity.cid_idCartorio != Guid.Empty)
                Param.Value = entity.cid_idCartorio;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@unf_idCartorio";
            Param.Size = 16;
            if (entity.unf_idCartorio != Guid.Empty)
                Param.Value = entity.unf_idCartorio;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@ctc_distritoCartorio";
            Param.Size = 100;
            if (!string.IsNullOrEmpty(entity.ctc_distritoCartorio))
                Param.Value = entity.ctc_distritoCartorio;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@ctc_situacao";
			Param.Size = 1;
			Param.Value = entity.ctc_situacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@ctc_dataCriacao";
			Param.Size = 16;
			Param.Value = entity.ctc_dataCriacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@ctc_dataAlteracao";
			Param.Size = 16;
			Param.Value = entity.ctc_dataAlteracao;
			qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@ctc_matricula";
            Param.Size = 32;
            if (!string.IsNullOrEmpty(entity.ctc_matricula))
                Param.Value = entity.ctc_matricula;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Boolean;
            Param.ParameterName = "@ctc_gemeo";
            Param.Size = 1;
            Param.Value = entity.ctc_gemeo;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Boolean;
            Param.ParameterName = "@ctc_modeloNovo";
            Param.Size = 1;
            Param.Value = entity.ctc_modeloNovo;
            qs.Parameters.Add(Param);

        }

		/// <summary>
		/// Configura os parametros do metodo de Deletar
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamDeletar(QueryStoredProcedure qs, PES_CertidaoCivil entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@pes_id";
			Param.Size = 16;
			Param.Value = entity.pes_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@ctc_id";
			Param.Size = 16;
			Param.Value = entity.ctc_id;
			qs.Parameters.Add(Param);


		}
		
		/// <summary>
		/// Recebe o valor do auto incremento e coloca na propriedade 
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, PES_CertidaoCivil entity)
		{
            entity.ctc_id = new Guid(qs.Return.Rows[0][0].ToString());
            return (entity.ctc_id != Guid.Empty); 
		}		
	}
}

