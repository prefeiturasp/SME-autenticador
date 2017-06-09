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
	/// Classe abstrata de SYS_Modulo
	/// </summary>
	public abstract class Abstract_SYS_ModuloDAO : Abstract_DAL<SYS_Modulo>
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
		protected override void ParamCarregar(QuerySelectStoredProcedure qs, SYS_Modulo entity)
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


		}
		
		/// <summary>
		/// Configura os parametros do metodo de Inserir
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamInserir(QuerySelectStoredProcedure qs, SYS_Modulo entity)
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
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@mod_nome";
			Param.Size = 50;
			Param.Value = entity.mod_nome;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.String;
			Param.ParameterName = "@mod_descricao";		
			if( !string.IsNullOrEmpty(entity.mod_descricao) )
				Param.Value = entity.mod_descricao;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@mod_idPai";
			Param.Size = 4;
			if( entity.mod_idPai > 0  )
				Param.Value = entity.mod_idPai;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Boolean;
			Param.ParameterName = "@mod_auditoria";
			Param.Size = 1;
			Param.Value = entity.mod_auditoria;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@mod_situacao";
			Param.Size = 1;
			Param.Value = entity.mod_situacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@mod_dataCriacao";
			Param.Size = 16;
			Param.Value = entity.mod_dataCriacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@mod_dataAlteracao";
			Param.Size = 16;
			Param.Value = entity.mod_dataAlteracao;
			qs.Parameters.Add(Param);


		}
		
		/// <summary>
		/// Configura os parametros do metodo de Alterar
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamAlterar(QueryStoredProcedure qs, SYS_Modulo entity)
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
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@mod_nome";
			Param.Size = 50;
			Param.Value = entity.mod_nome;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.String;
			Param.ParameterName = "@mod_descricao";			
			if( !string.IsNullOrEmpty(entity.mod_descricao) )
				Param.Value = entity.mod_descricao;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@mod_idPai";
			Param.Size = 4;
			if( entity.mod_idPai > 0  )
				Param.Value = entity.mod_idPai;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Boolean;
			Param.ParameterName = "@mod_auditoria";
			Param.Size = 1;
			Param.Value = entity.mod_auditoria;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@mod_situacao";
			Param.Size = 1;
			Param.Value = entity.mod_situacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@mod_dataCriacao";
			Param.Size = 16;
			Param.Value = entity.mod_dataCriacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@mod_dataAlteracao";
			Param.Size = 16;
			Param.Value = entity.mod_dataAlteracao;
			qs.Parameters.Add(Param);


		}

		/// <summary>
		/// Configura os parametros do metodo de Deletar
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamDeletar(QueryStoredProcedure qs, SYS_Modulo entity)
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


		}
		
		/// <summary>
		/// Recebe o valor do auto incremento e coloca na propriedade 
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, SYS_Modulo entity)
		{
            entity.mod_id = Convert.ToInt32(qs.Return.Rows[0][0]);
            return (entity.mod_id > 0);
		}		
	}
}

