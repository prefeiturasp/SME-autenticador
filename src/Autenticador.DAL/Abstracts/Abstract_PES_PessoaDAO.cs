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
	/// Classe abstrata de PES_Pessoa
	/// </summary>
	public abstract class Abstract_PES_PessoaDAO : Abstract_DAL<PES_Pessoa>
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
		protected override void ParamCarregar(QuerySelectStoredProcedure qs, PES_Pessoa entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@pes_id";
			Param.Size = 16;
			Param.Value = entity.pes_id;
			qs.Parameters.Add(Param);


		}
		
		/// <summary>
		/// Configura os parametros do metodo de Inserir
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamInserir(QuerySelectStoredProcedure qs, PES_Pessoa entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@pes_id";
			Param.Size = 16;
			Param.Value = entity.pes_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@pes_nome";
			Param.Size = 200;
			Param.Value = entity.pes_nome;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@pes_nome_abreviado";
			Param.Size = 50;
			if( !string.IsNullOrEmpty(entity.pes_nome_abreviado) )
				Param.Value = entity.pes_nome_abreviado;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@pai_idNacionalidade";
			Param.Size = 16;
				Param.Value = entity.pai_idNacionalidade;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Boolean;
			Param.ParameterName = "@pes_naturalizado";
			Param.Size = 1;
				Param.Value = entity.pes_naturalizado;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@cid_idNaturalidade";
			Param.Size = 16;
				Param.Value = entity.cid_idNaturalidade;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Object;
			Param.ParameterName = "@pes_dataNascimento";
			Param.Size = 20;
			if( entity.pes_dataNascimento!= new DateTime() )
				Param.Value = entity.pes_dataNascimento;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@pes_estadoCivil";
			Param.Size = 1;
			if( entity.pes_estadoCivil > 0  )
				Param.Value = entity.pes_estadoCivil;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@pes_racaCor";
			Param.Size = 1;
			if( entity.pes_racaCor > 0  )
				Param.Value = entity.pes_racaCor;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@pes_sexo";
			Param.Size = 1;
			if( entity.pes_sexo > 0  )
				Param.Value = entity.pes_sexo;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@pes_idFiliacaoPai";
			Param.Size = 16;
				Param.Value = entity.pes_idFiliacaoPai;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@pes_idFiliacaoMae";
			Param.Size = 16;
				Param.Value = entity.pes_idFiliacaoMae;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@tes_id";
			Param.Size = 16;
				Param.Value = entity.tes_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@pes_situacao";
			Param.Size = 1;
			Param.Value = entity.pes_situacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@pes_dataCriacao";
			Param.Size = 16;
			Param.Value = entity.pes_dataCriacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@pes_dataAlteracao";
			Param.Size = 16;
			Param.Value = entity.pes_dataAlteracao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@pes_integridade";
			Param.Size = 4;
			Param.Value = entity.pes_integridade;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int64;
			Param.ParameterName = "@arq_idFoto";
			Param.Size = 8;
			if( entity.arq_idFoto > 0  )
				Param.Value = entity.arq_idFoto;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@pes_nomeSocial";
            Param.Size = 200;
            if (!string.IsNullOrEmpty(entity.pes_nomeSocial))
            {
                Param.Value = entity.pes_nomeSocial;
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
		protected override void ParamAlterar(QueryStoredProcedure qs, PES_Pessoa entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@pes_id";
			Param.Size = 16;
			Param.Value = entity.pes_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@pes_nome";
			Param.Size = 200;
			Param.Value = entity.pes_nome;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@pes_nome_abreviado";
			Param.Size = 50;
			if( !string.IsNullOrEmpty(entity.pes_nome_abreviado) )
				Param.Value = entity.pes_nome_abreviado;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@pai_idNacionalidade";
			Param.Size = 16;
				Param.Value = entity.pai_idNacionalidade;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Boolean;
			Param.ParameterName = "@pes_naturalizado";
			Param.Size = 1;
				Param.Value = entity.pes_naturalizado;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@cid_idNaturalidade";
			Param.Size = 16;
				Param.Value = entity.cid_idNaturalidade;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Object;
			Param.ParameterName = "@pes_dataNascimento";
			Param.Size = 20;
			if( entity.pes_dataNascimento!= new DateTime() )
				Param.Value = entity.pes_dataNascimento;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@pes_estadoCivil";
			Param.Size = 1;
			if( entity.pes_estadoCivil > 0  )
				Param.Value = entity.pes_estadoCivil;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@pes_racaCor";
			Param.Size = 1;
			if( entity.pes_racaCor > 0  )
				Param.Value = entity.pes_racaCor;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@pes_sexo";
			Param.Size = 1;
			if( entity.pes_sexo > 0  )
				Param.Value = entity.pes_sexo;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@pes_idFiliacaoPai";
			Param.Size = 16;
				Param.Value = entity.pes_idFiliacaoPai;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@pes_idFiliacaoMae";
			Param.Size = 16;
				Param.Value = entity.pes_idFiliacaoMae;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@tes_id";
			Param.Size = 16;
				Param.Value = entity.tes_id;
			qs.Parameters.Add(Param);
            
			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@pes_situacao";
			Param.Size = 1;
			Param.Value = entity.pes_situacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@pes_dataCriacao";
			Param.Size = 16;
			Param.Value = entity.pes_dataCriacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@pes_dataAlteracao";
			Param.Size = 16;
			Param.Value = entity.pes_dataAlteracao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@pes_integridade";
			Param.Size = 4;
			Param.Value = entity.pes_integridade;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int64;
			Param.ParameterName = "@arq_idFoto";
			Param.Size = 8;
			if( entity.arq_idFoto > 0  )
				Param.Value = entity.arq_idFoto;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@pes_nomeSocial";
            Param.Size = 200;
            if (!string.IsNullOrEmpty(entity.pes_nomeSocial))
            {
                Param.Value = entity.pes_nomeSocial;
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
		protected override void ParamDeletar(QueryStoredProcedure qs, PES_Pessoa entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@pes_id";
			Param.Size = 16;
			Param.Value = entity.pes_id;
			qs.Parameters.Add(Param);


		}
		
		/// <summary>
		/// Recebe o valor do auto incremento e coloca na propriedade 
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, PES_Pessoa entity)
		{
            entity.pes_id = new Guid(qs.Return.Rows[0][0].ToString());
            return (entity.pes_id != Guid.Empty); 
		}		
	}
}

