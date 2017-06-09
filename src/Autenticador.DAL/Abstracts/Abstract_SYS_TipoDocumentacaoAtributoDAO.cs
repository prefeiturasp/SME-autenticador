/*
    Classe gerada automaticamente pelo Code Creator
*/

namespace Autenticador.DAL.Abstracts
{
    using Autenticador.Entities;
    using CoreLibrary.Data.Common;
    using CoreLibrary.Data.Common.Abstracts;
    using System;
    using System.Data;

    /// <summary>
    /// Classe abstrata de SYS_TipoDocumentacaoAtributo.
    /// </summary>
    public abstract class Abstract_SYS_TipoDocumentacaoAtributoDAO : Abstract_DAL<SYS_TipoDocumentacaoAtributo>
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
        protected override void ParamCarregar(QuerySelectStoredProcedure qs, SYS_TipoDocumentacaoAtributo entity)
        {
            if (entity != null & qs != null)
            {
                Param = qs.NewParameter();
                Param.DbType = DbType.Byte;
                Param.ParameterName = "@tda_id";
                Param.Size = 1;
                Param.Value = entity.tda_id;
                qs.Parameters.Add(Param);
            }
        }

        /// <summary>
        /// Configura os parametros do metodo de Inserir.
        /// </summary>
        /// <param name="qs">Objeto da Store Procedure.</param>
        /// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
        protected override void ParamInserir(QuerySelectStoredProcedure qs, SYS_TipoDocumentacaoAtributo entity)
        {
            if (entity != null & qs != null)
            {
                Param = qs.NewParameter();
                Param.DbType = DbType.Byte;
                Param.ParameterName = "@tda_id";
                Param.Size = 1;
                Param.Value = entity.tda_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.String;
                Param.ParameterName = "@tda_descricao";
                Param.Size = 64;
                if (!string.IsNullOrEmpty(entity.tda_descricao))
                    Param.Value = entity.tda_descricao;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.String;
                Param.ParameterName = "@tda_nomeObjeto";
                Param.Size = 256;
                if (!string.IsNullOrEmpty(entity.tda_nomeObjeto))
                    Param.Value = entity.tda_nomeObjeto;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Boolean;
                Param.ParameterName = "@tda_default";
                Param.Size = 1;
                Param.Value = entity.tda_default;
                qs.Parameters.Add(Param);
            }
        }

        /// <summary>
        /// Configura os parametros do metodo de Alterar.
        /// </summary>
        /// <param name="qs">Objeto da Store Procedure.</param>
        /// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
        protected override void ParamAlterar(QueryStoredProcedure qs, SYS_TipoDocumentacaoAtributo entity)
        {
            if (entity != null & qs != null)
            {
                Param = qs.NewParameter();
                Param.DbType = DbType.Byte;
                Param.ParameterName = "@tda_id";
                Param.Size = 1;
                Param.Value = entity.tda_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.String;
                Param.ParameterName = "@tda_descricao";
                Param.Size = 64;
                if (!string.IsNullOrEmpty(entity.tda_descricao))
                {
                    Param.Value = entity.tda_descricao;
                }
                else
                {
                    Param.Value = DBNull.Value;
                }
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.String;
                Param.ParameterName = "@tda_nomeObjeto";
                Param.Size = 256;
                if (!string.IsNullOrEmpty(entity.tda_nomeObjeto))
                {
                    Param.Value = entity.tda_nomeObjeto;
                }
                else
                {
                    Param.Value = DBNull.Value;
                }
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Boolean;
                Param.ParameterName = "@tda_default";
                Param.Size = 1;
                Param.Value = entity.tda_default;
                qs.Parameters.Add(Param);
            }
        }

        /// <summary>
        /// Configura os parametros do metodo de Deletar.
        /// </summary>
        /// <param name="qs">Objeto da Store Procedure.</param>
        /// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
        protected override void ParamDeletar(QueryStoredProcedure qs, SYS_TipoDocumentacaoAtributo entity)
        {
            if (entity != null & qs != null)
            {
                Param = qs.NewParameter();
                Param.DbType = DbType.Byte;
                Param.ParameterName = "@tda_id";
                Param.Size = 1;
                Param.Value = entity.tda_id;
                qs.Parameters.Add(Param);
            }
        }

        /// <summary>
        /// Recebe o valor do auto incremento e coloca na propriedade.
        /// </summary>
        /// <param name="qs">Objeto da Store Procedure.</param>
        /// <param name="entity">Entidade com os dados para preenchimento dos parametros.</param>
        /// <returns>TRUE - Se entity.ParametroId > 0</returns>
        protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, SYS_TipoDocumentacaoAtributo entity)
        {
            return true;
        }
    }
}