/*
	Classe gerada automaticamente pelo Code Creator
*/

using System.Data;
using CoreLibrary.Data.Common;
using CoreLibrary.Data.Common.Abstracts;
using Autenticador.Entities;

namespace Autenticador.DAL.Abstracts
{
	
	/// <summary>
	/// Classe abstrata de SYS_UsuarioFalhaAutenticacao
	/// </summary>
	public abstract class Abstract_SYS_UsuarioFalhaAutenticacaoDAO : Abstract_DAL<SYS_UsuarioFalhaAutenticacao>
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
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamCarregar(QuerySelectStoredProcedure qs, SYS_UsuarioFalhaAutenticacao entity)
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
		protected override void ParamInserir(QuerySelectStoredProcedure qs, SYS_UsuarioFalhaAutenticacao entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@usu_id";
			Param.Size = 16;
			Param.Value = entity.usu_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@ufl_qtdeFalhas";
			Param.Size = 4;
			Param.Value = entity.ufl_qtdeFalhas;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@ufl_dataUltimaTentativa";
			Param.Size = 16;
			Param.Value = entity.ufl_dataUltimaTentativa;
			qs.Parameters.Add(Param);


		}
		
		/// <summary>
		/// Configura os parametros do metodo de Alterar
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamAlterar(QueryStoredProcedure qs, SYS_UsuarioFalhaAutenticacao entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@usu_id";
			Param.Size = 16;
			Param.Value = entity.usu_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Int32;
			Param.ParameterName = "@ufl_qtdeFalhas";
			Param.Size = 4;
			Param.Value = entity.ufl_qtdeFalhas;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@ufl_dataUltimaTentativa";
			Param.Size = 16;
			Param.Value = entity.ufl_dataUltimaTentativa;
			qs.Parameters.Add(Param);


		}

		/// <summary>
		/// Configura os parametros do metodo de Deletar
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamDeletar(QueryStoredProcedure qs, SYS_UsuarioFalhaAutenticacao entity)
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
		protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, SYS_UsuarioFalhaAutenticacao entity)
		{
		    return true;
		}		
	}
}

