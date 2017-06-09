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
	/// Classe abstrata de SYS_DiaNaoUtil
	/// </summary>
	public abstract class Abstract_SYS_DiaNaoUtilDAO : Abstract_DAL<SYS_DiaNaoUtil>
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
		protected override void ParamCarregar(QuerySelectStoredProcedure qs, SYS_DiaNaoUtil entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@dnu_id";
			Param.Size = 16;
			Param.Value = entity.dnu_id;
			qs.Parameters.Add(Param);


		}
		
		/// <summary>
		/// Configura os parametros do metodo de Inserir
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamInserir(QuerySelectStoredProcedure qs, SYS_DiaNaoUtil entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@dnu_id";
			Param.Size = 16;
			Param.Value = entity.dnu_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@dnu_nome";
			Param.Size = 100;
			Param.Value = entity.dnu_nome;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@dnu_abrangencia";
			Param.Size = 1;
			Param.Value = entity.dnu_abrangencia;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@dnu_descricao";
			Param.Size = 400;
			if( !string.IsNullOrEmpty(entity.dnu_descricao) )
				Param.Value = entity.dnu_descricao;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Date;
			Param.ParameterName = "@dnu_data";
			Param.Size = 20;
			Param.Value = entity.dnu_data;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Boolean;
			Param.ParameterName = "@dnu_recorrencia";
			Param.Size = 1;
				Param.Value = entity.dnu_recorrencia;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
			Param.ParameterName = "@dnu_vigenciaInicio";
			Param.Size = 20;
			Param.Value = entity.dnu_vigenciaInicio;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
			Param.ParameterName = "@dnu_vigenciaFim";
			Param.Size = 20;
			if( entity.dnu_vigenciaFim!= new DateTime() )
				Param.Value = entity.dnu_vigenciaFim;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@cid_id";
			Param.Size = 16;
				Param.Value = entity.cid_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@unf_id";
			Param.Size = 16;
				Param.Value = entity.unf_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@dnu_situacao";
			Param.Size = 1;
			Param.Value = entity.dnu_situacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@dnu_dataCriacao";
			Param.Size = 16;
			Param.Value = entity.dnu_dataCriacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@dnu_dataAlteracao";
			Param.Size = 16;
			Param.Value = entity.dnu_dataAlteracao;
			qs.Parameters.Add(Param);


		}
		
		/// <summary>
		/// Configura os parametros do metodo de Alterar
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamAlterar(QueryStoredProcedure qs, SYS_DiaNaoUtil entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@dnu_id";
			Param.Size = 16;
			Param.Value = entity.dnu_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@dnu_nome";
			Param.Size = 100;
			Param.Value = entity.dnu_nome;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@dnu_abrangencia";
			Param.Size = 1;
			Param.Value = entity.dnu_abrangencia;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@dnu_descricao";
			Param.Size = 400;
			if( !string.IsNullOrEmpty(entity.dnu_descricao) )
				Param.Value = entity.dnu_descricao;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
            Param.DbType = DbType.Date;
			Param.ParameterName = "@dnu_data";
			Param.Size = 20;
			Param.Value = entity.dnu_data;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Boolean;
			Param.ParameterName = "@dnu_recorrencia";
			Param.Size = 1;
				Param.Value = entity.dnu_recorrencia;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
			Param.ParameterName = "@dnu_vigenciaInicio";
			Param.Size = 20;
			Param.Value = entity.dnu_vigenciaInicio;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
			Param.ParameterName = "@dnu_vigenciaFim";
			Param.Size = 20;
			if( entity.dnu_vigenciaFim!= new DateTime() )
				Param.Value = entity.dnu_vigenciaFim;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@cid_id";
			Param.Size = 16;
				Param.Value = entity.cid_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@unf_id";
			Param.Size = 16;
				Param.Value = entity.unf_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@dnu_situacao";
			Param.Size = 1;
			Param.Value = entity.dnu_situacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@dnu_dataCriacao";
			Param.Size = 16;
			Param.Value = entity.dnu_dataCriacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@dnu_dataAlteracao";
			Param.Size = 16;
			Param.Value = entity.dnu_dataAlteracao;
			qs.Parameters.Add(Param);


		}

		/// <summary>
		/// Configura os parametros do metodo de Deletar
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamDeletar(QueryStoredProcedure qs, SYS_DiaNaoUtil entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@dnu_id";
			Param.Size = 16;
			Param.Value = entity.dnu_id;
			qs.Parameters.Add(Param);


		}
		
		/// <summary>
		/// Recebe o valor do auto incremento e coloca na propriedade 
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, SYS_DiaNaoUtil entity)
		{
            entity.dnu_id = new Guid(qs.Return.Rows[0][0].ToString());
            return (entity.dnu_id != Guid.Empty); 
		}		
	}
}

