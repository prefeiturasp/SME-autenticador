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
	/// Classe abstrata de SYS_Aluno
	/// </summary>
	public abstract class Abstract_SYS_AlunoDAO : Abstract_DAL<SYS_Aluno>
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
		protected override void ParamCarregar(QuerySelectStoredProcedure qs, SYS_Aluno entity)
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
		protected override void ParamInserir(QuerySelectStoredProcedure qs, SYS_Aluno entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@usu_id";
			Param.Size = 16;
			Param.Value = entity.usu_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int64;
			Param.ParameterName = "@alu_rm";
			Param.Size = 8;
			if( entity.alu_rm > 0  )
				Param.Value = entity.alu_rm;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@alu_ufra";
			Param.Size = 5;
			if( !string.IsNullOrEmpty(entity.alu_ufra) )
				Param.Value = entity.alu_ufra;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Boolean;
			Param.ParameterName = "@alu_comunitario";
			Param.Size = 1;
				Param.Value = entity.alu_comunitario;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@tpe_id";
			Param.Size = 4;
			if( entity.tpe_id > 0  )
				Param.Value = entity.tpe_id;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@alu_serie";
			Param.Size = 4;
			if( entity.alu_serie > 0  )
				Param.Value = entity.alu_serie;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@alu_turma";
			Param.Size = 4;
			if( !string.IsNullOrEmpty(entity.alu_turma) )
				Param.Value = entity.alu_turma;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@alu_sala";
			Param.Size = 4;
			if( !string.IsNullOrEmpty(entity.alu_sala) )
				Param.Value = entity.alu_sala;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@alu_anoLetivo";
			Param.Size = 4;
			if( entity.alu_anoLetivo > 0  )
				Param.Value = entity.alu_anoLetivo;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@alu_numclasse";
			Param.Size = 10;
			if( !string.IsNullOrEmpty(entity.alu_numclasse) )
				Param.Value = entity.alu_numclasse;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@alu_HrIni";
			Param.Size = 4;
			if( !string.IsNullOrEmpty(entity.alu_HrIni) )
				Param.Value = entity.alu_HrIni;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@alu_HrFim";
			Param.Size = 4;
			if( !string.IsNullOrEmpty(entity.alu_HrFim) )
				Param.Value = entity.alu_HrFim;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@alu_ra_original";
			Param.Size = 100;
			if( !string.IsNullOrEmpty(entity.alu_ra_original) )
				Param.Value = entity.alu_ra_original;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@alteracao_wsIntegracaoBlueControl";
			Param.Size = 4;
			if( entity.alteracao_wsIntegracaoBlueControl > 0  )
				Param.Value = entity.alteracao_wsIntegracaoBlueControl;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);


		}
		
		/// <summary>
		/// Configura os parametros do metodo de Alterar
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamAlterar(QueryStoredProcedure qs, SYS_Aluno entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@usu_id";
			Param.Size = 16;
			Param.Value = entity.usu_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int64;
			Param.ParameterName = "@alu_rm";
			Param.Size = 8;
			if( entity.alu_rm > 0  )
				Param.Value = entity.alu_rm;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@alu_ufra";
			Param.Size = 5;
			if( !string.IsNullOrEmpty(entity.alu_ufra) )
				Param.Value = entity.alu_ufra;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Boolean;
			Param.ParameterName = "@alu_comunitario";
			Param.Size = 1;
				Param.Value = entity.alu_comunitario;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@tpe_id";
			Param.Size = 4;
			if( entity.tpe_id > 0  )
				Param.Value = entity.tpe_id;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@alu_serie";
			Param.Size = 4;
			if( entity.alu_serie > 0  )
				Param.Value = entity.alu_serie;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@alu_turma";
			Param.Size = 4;
			if( !string.IsNullOrEmpty(entity.alu_turma) )
				Param.Value = entity.alu_turma;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@alu_sala";
			Param.Size = 4;
			if( !string.IsNullOrEmpty(entity.alu_sala) )
				Param.Value = entity.alu_sala;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@alu_anoLetivo";
			Param.Size = 4;
			if( entity.alu_anoLetivo > 0  )
				Param.Value = entity.alu_anoLetivo;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@alu_numclasse";
			Param.Size = 10;
			if( !string.IsNullOrEmpty(entity.alu_numclasse) )
				Param.Value = entity.alu_numclasse;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@alu_HrIni";
			Param.Size = 4;
			if( !string.IsNullOrEmpty(entity.alu_HrIni) )
				Param.Value = entity.alu_HrIni;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@alu_HrFim";
			Param.Size = 4;
			if( !string.IsNullOrEmpty(entity.alu_HrFim) )
				Param.Value = entity.alu_HrFim;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@alu_ra_original";
			Param.Size = 100;
			if( !string.IsNullOrEmpty(entity.alu_ra_original) )
				Param.Value = entity.alu_ra_original;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@alteracao_wsIntegracaoBlueControl";
			Param.Size = 4;
			if( entity.alteracao_wsIntegracaoBlueControl > 0  )
				Param.Value = entity.alteracao_wsIntegracaoBlueControl;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);


		}

		/// <summary>
		/// Configura os parametros do metodo de Deletar
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamDeletar(QueryStoredProcedure qs, SYS_Aluno entity)
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
		protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, SYS_Aluno entity)
		{
            return (entity.usu_id != Guid.Empty);
		}		
	}
}

