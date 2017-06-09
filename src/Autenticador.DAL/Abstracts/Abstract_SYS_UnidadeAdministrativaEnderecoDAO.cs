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
	/// Classe abstrata de SYS_UnidadeAdministrativaEndereco
	/// </summary>
	public abstract class Abstract_SYS_UnidadeAdministrativaEnderecoDAO : Abstract_DAL<SYS_UnidadeAdministrativaEndereco>
	{

        /// <summary>
        /// ConnectionString.
        /// </summary>
        protected override string ConnectionStringName
        {
            get
            {
                return "AutenticadorDB";
            }
        }

        /// <summary>
        /// Configura os parametros do metodo de carregar.
        /// </summary>
        /// <param name="qs">Objeto da Store Procedure.</param>
        /// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
        protected override void ParamCarregar(QuerySelectStoredProcedure qs, SYS_UnidadeAdministrativaEndereco entity)
        {
            if (entity != null & qs != null)
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
                Param.ParameterName = "@uae_id";
                Param.Size = 16;
                Param.Value = entity.uae_id;
                qs.Parameters.Add(Param);


            }
        }

        /// <summary>
        /// Configura os parametros do metodo de Inserir.
        /// </summary>
        /// <param name="qs">Objeto da Store Procedure.</param>
        /// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
        protected override void ParamInserir(QuerySelectStoredProcedure qs, SYS_UnidadeAdministrativaEndereco entity)
        {
            if (entity != null & qs != null)
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
                Param.ParameterName = "@uae_id";
                Param.Size = 16;
                Param.Value = entity.uae_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@end_id";
                Param.Size = 16;
                Param.Value = entity.end_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@uae_numero";
                Param.Size = 20;
                Param.Value = entity.uae_numero;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@uae_complemento";
                Param.Size = 100;
                if (!string.IsNullOrEmpty(entity.uae_complemento))
                {
                    Param.Value = entity.uae_complemento;
                }
                else
                {
                    Param.Value = DBNull.Value;
                }
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Byte;
                Param.ParameterName = "@uae_situacao";
                Param.Size = 1;
                Param.Value = entity.uae_situacao;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.DateTime;
                Param.ParameterName = "@uae_dataCriacao";
                Param.Size = 16;
                Param.Value = entity.uae_dataCriacao;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.DateTime;
                Param.ParameterName = "@uae_dataAlteracao";
                Param.Size = 16;
                Param.Value = entity.uae_dataAlteracao;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Boolean;
                Param.ParameterName = "@uae_enderecoPrincipal";
                Param.Size = 1;
                Param.Value = entity.uae_enderecoPrincipal;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Decimal;
                Param.ParameterName = "@uae_latitude";
                Param.Size = 17;
                Param.Value = entity.uae_latitude;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Decimal;
                Param.ParameterName = "@uae_longitude";
                Param.Size = 17;
                Param.Value = entity.uae_longitude;
                qs.Parameters.Add(Param);


            }
        }

        /// <summary>
        /// Configura os parametros do metodo de Alterar.
        /// </summary>
        /// <param name="qs">Objeto da Store Procedure.</param>
        /// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
        protected override void ParamAlterar(QueryStoredProcedure qs, SYS_UnidadeAdministrativaEndereco entity)
        {
            if (entity != null & qs != null)
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
                Param.ParameterName = "@uae_id";
                Param.Size = 16;
                Param.Value = entity.uae_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@end_id";
                Param.Size = 16;
                Param.Value = entity.end_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@uae_numero";
                Param.Size = 20;
                Param.Value = entity.uae_numero;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@uae_complemento";
                Param.Size = 100;
                if (!string.IsNullOrEmpty(entity.uae_complemento))
                {
                    Param.Value = entity.uae_complemento;
                }
                else
                {
                    Param.Value = DBNull.Value;
                }
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Byte;
                Param.ParameterName = "@uae_situacao";
                Param.Size = 1;
                Param.Value = entity.uae_situacao;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.DateTime;
                Param.ParameterName = "@uae_dataCriacao";
                Param.Size = 16;
                Param.Value = entity.uae_dataCriacao;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.DateTime;
                Param.ParameterName = "@uae_dataAlteracao";
                Param.Size = 16;
                Param.Value = entity.uae_dataAlteracao;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Boolean;
                Param.ParameterName = "@uae_enderecoPrincipal";
                Param.Size = 1;
                Param.Value = entity.uae_enderecoPrincipal;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Decimal;
                Param.ParameterName = "@uae_latitude";
                Param.Size = 17;
                Param.Value = entity.uae_latitude;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Decimal;
                Param.ParameterName = "@uae_longitude";
                Param.Size = 17;
                Param.Value = entity.uae_longitude;
                qs.Parameters.Add(Param);


            }
        }

        /// <summary>
        /// Configura os parametros do metodo de Deletar.
        /// </summary>
        /// <param name="qs">Objeto da Store Procedure.</param>
        /// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
        protected override void ParamDeletar(QueryStoredProcedure qs, SYS_UnidadeAdministrativaEndereco entity)
        {
            if (entity != null & qs != null)
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
                Param.ParameterName = "@uae_id";
                Param.Size = 16;
                Param.Value = entity.uae_id;
                qs.Parameters.Add(Param);


            }
        }

        /// <summary>
        /// Recebe o valor do auto incremento e coloca na propriedade.
        /// </summary>
        /// <param name="qs">Objeto da Store Procedure.</param>
        /// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
        /// <returns>TRUE - Se entity.ParametroId > 0</returns>
        protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, SYS_UnidadeAdministrativaEndereco entity)
        {
            if (entity != null & qs != null)
            {
                entity.uae_id = new Guid(qs.Return.Rows[0][0].ToString());
                return (entity.uae_id != Guid.Empty);
            }

            return false;
        }
    }
}

