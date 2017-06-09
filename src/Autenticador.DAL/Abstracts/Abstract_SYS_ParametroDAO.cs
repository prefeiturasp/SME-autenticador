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
	/// Classe abstrata de SYS_Parametro
	/// </summary>
	public abstract class Abstract_SYS_ParametroDAO : Abstract_DAL<SYS_Parametro>
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
		protected override void ParamCarregar(QuerySelectStoredProcedure qs, SYS_Parametro entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@par_id";
			Param.Size = 16;
			Param.Value = entity.par_id;
			qs.Parameters.Add(Param);


		}
		
		/// <summary>
		/// Configura os parametros do metodo de Inserir
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamInserir(QuerySelectStoredProcedure qs, SYS_Parametro entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@par_id";
			Param.Size = 16;
			Param.Value = entity.par_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@par_chave";
			Param.Size = 100;
			Param.Value = entity.par_chave;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@par_valor";
			Param.Size = 1000;
			Param.Value = entity.par_valor;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@par_descricao";
			Param.Size = 200;
			if( !string.IsNullOrEmpty(entity.par_descricao) )
				Param.Value = entity.par_descricao;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@par_situacao";
			Param.Size = 1;
			Param.Value = entity.par_situacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@par_vigenciaInicio";
			Param.Size = 20;
			Param.Value = entity.par_vigenciaInicio;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
			Param.ParameterName = "@par_vigenciaFim";
			Param.Size = 20;
			if( entity.par_vigenciaFim!= new DateTime() )
				Param.Value = entity.par_vigenciaFim;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@par_dataCriacao";
			Param.Size = 16;
			Param.Value = entity.par_dataCriacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@par_dataAlteracao";
			Param.Size = 16;
			Param.Value = entity.par_dataAlteracao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Boolean;
			Param.ParameterName = "@par_obrigatorio";
			Param.Size = 1;
				Param.Value = entity.par_obrigatorio;
			qs.Parameters.Add(Param);


		}
		
		/// <summary>
		/// Configura os parametros do metodo de Alterar
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamAlterar(QueryStoredProcedure qs, SYS_Parametro entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@par_id";
			Param.Size = 16;
			Param.Value = entity.par_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@par_chave";
			Param.Size = 100;
			Param.Value = entity.par_chave;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@par_valor";
			Param.Size = 1000;
			Param.Value = entity.par_valor;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@par_descricao";
			Param.Size = 200;
			if( !string.IsNullOrEmpty(entity.par_descricao) )
				Param.Value = entity.par_descricao;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@par_situacao";
			Param.Size = 1;
			Param.Value = entity.par_situacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
			Param.ParameterName = "@par_vigenciaInicio";
			Param.Size = 20;
			Param.Value = entity.par_vigenciaInicio;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
			Param.ParameterName = "@par_vigenciaFim";
			Param.Size = 20;
			if( entity.par_vigenciaFim!= new DateTime() )
				Param.Value = entity.par_vigenciaFim;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@par_dataCriacao";
			Param.Size = 16;
			Param.Value = entity.par_dataCriacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@par_dataAlteracao";
			Param.Size = 16;
			Param.Value = entity.par_dataAlteracao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Boolean;
			Param.ParameterName = "@par_obrigatorio";
			Param.Size = 1;
				Param.Value = entity.par_obrigatorio;
			qs.Parameters.Add(Param);


		}

		/// <summary>
		/// Configura os parametros do metodo de Deletar
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamDeletar(QueryStoredProcedure qs, SYS_Parametro entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@par_id";
			Param.Size = 16;
			Param.Value = entity.par_id;
			qs.Parameters.Add(Param);


		}
		
		/// <summary>
		/// Recebe o valor do auto incremento e coloca na propriedade 
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, SYS_Parametro entity)
		{
            entity.par_id = new Guid(qs.Return.Rows[0][0].ToString());
            return (entity.par_id != Guid.Empty); 
		}		
	}
}

