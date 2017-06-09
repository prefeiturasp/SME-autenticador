/*
	Classe gerada automaticamente pelo MSTech Code Creator
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
	/// Classe abstrata de SYS_Professor
	/// </summary>
	public abstract class Abstract_SYS_ProfessorDAO : Abstract_DAL<SYS_Professor>
	{
	
        protected override string ConnectionStringName
        {
            get
            {
                return "Autenticador";
            }
        }
        	
		/// <summary>
		/// Configura os parametros do metodo de carregar
		/// </ssummary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamCarregar(QuerySelectStoredProcedure qs, SYS_Professor entity)
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
		protected override void ParamInserir(QuerySelectStoredProcedure qs, SYS_Professor entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@usu_id";
			Param.Size = 16;
			Param.Value = entity.usu_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Boolean;
			Param.ParameterName = "@prof_dinamizador";
			Param.Size = 1;
			Param.Value = entity.prof_dinamizador;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@prof_rspv";
			Param.Size = 100;
			if( !string.IsNullOrEmpty(entity.prof_rspv) )
				Param.Value = entity.prof_rspv;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@prof_distrito";
			Param.Size = 30;
			if( !string.IsNullOrEmpty(entity.prof_distrito) )
				Param.Value = entity.prof_distrito;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@prof_DI";
			Param.Size = 1;
			if( !string.IsNullOrEmpty(entity.prof_DI) )
				Param.Value = entity.prof_DI;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@prof_RS";
			Param.Size = 4;
			if( entity.prof_RS > 0  )
				Param.Value = entity.prof_RS;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@prof_cargoC";
			Param.Size = 4;
			if( entity.prof_cargoC > 0  )
				Param.Value = entity.prof_cargoC;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@prof_UAC";
			Param.Size = 4;
			if( entity.prof_UAC > 0  )
				Param.Value = entity.prof_UAC;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@prof_codcargo";
			Param.Size = 4;
			if( entity.prof_codcargo > 0  )
				Param.Value = entity.prof_codcargo;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@prof_UA";
			Param.Size = 4;
			if( entity.prof_UA > 0  )
				Param.Value = entity.prof_UA;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@prof_Quadro";
			Param.Size = 3;
			if( !string.IsNullOrEmpty(entity.prof_Quadro) )
				Param.Value = entity.prof_Quadro;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);


		}
		
		/// <summary>
		/// Configura os parametros do metodo de Alterar
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamAlterar(QueryStoredProcedure qs, SYS_Professor entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@usu_id";
			Param.Size = 16;
			Param.Value = entity.usu_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Boolean;
			Param.ParameterName = "@prof_dinamizador";
			Param.Size = 1;
			Param.Value = entity.prof_dinamizador;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@prof_rspv";
			Param.Size = 100;
			if( !string.IsNullOrEmpty(entity.prof_rspv) )
				Param.Value = entity.prof_rspv;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@prof_distrito";
			Param.Size = 30;
			if( !string.IsNullOrEmpty(entity.prof_distrito) )
				Param.Value = entity.prof_distrito;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@prof_DI";
			Param.Size = 1;
			if( !string.IsNullOrEmpty(entity.prof_DI) )
				Param.Value = entity.prof_DI;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@prof_RS";
			Param.Size = 4;
			if( entity.prof_RS > 0  )
				Param.Value = entity.prof_RS;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@prof_cargoC";
			Param.Size = 4;
			if( entity.prof_cargoC > 0  )
				Param.Value = entity.prof_cargoC;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@prof_UAC";
			Param.Size = 4;
			if( entity.prof_UAC > 0  )
				Param.Value = entity.prof_UAC;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@prof_codcargo";
			Param.Size = 4;
			if( entity.prof_codcargo > 0  )
				Param.Value = entity.prof_codcargo;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@prof_UA";
			Param.Size = 4;
			if( entity.prof_UA > 0  )
				Param.Value = entity.prof_UA;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@prof_Quadro";
			Param.Size = 3;
			if( !string.IsNullOrEmpty(entity.prof_Quadro) )
				Param.Value = entity.prof_Quadro;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);


		}

		/// <summary>
		/// Configura os parametros do metodo de Deletar
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamDeletar(QueryStoredProcedure qs, SYS_Professor entity)
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
		protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, SYS_Professor entity)
		{
            return (entity.usu_id != Guid.Empty);
		}		
	}
}

