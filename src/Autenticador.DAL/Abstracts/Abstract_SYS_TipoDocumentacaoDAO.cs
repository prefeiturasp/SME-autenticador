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
    /// Classe abstrata de SYS_TipoDocumentacao.
    /// </summary>
    public abstract class Abstract_SYS_TipoDocumentacaoDAO : Abstract_DAL<SYS_TipoDocumentacao>
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
        /// <param name="qs">
        /// Objeto da Store Procedure.
        /// </param>
        /// <param name="entity">
        /// Entidade com os dados para preenchimento dos parametros.
        /// </param>
        protected override void ParamCarregar(QuerySelectStoredProcedure qs, SYS_TipoDocumentacao entity)
        {
            if (entity != null & qs != null)
            {
                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@tdo_id";
                Param.Size = 16;
                Param.Value = entity.tdo_id;
                qs.Parameters.Add(Param);
            }
        }

        /// <summary>
        /// Configura os parametros do metodo de Inserir.
        /// </summary>
        /// <param name="qs">
        /// Objeto da Store Procedure.
        /// </param>
        /// <param name="entity">
        /// Entidade com os dados para preenchimento dos parametros.
        /// </param>
        protected override void ParamInserir(QuerySelectStoredProcedure qs, SYS_TipoDocumentacao entity)
        {
            if (entity != null & qs != null)
            {
                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@tdo_id";
                Param.Size = 16;
                Param.Value = entity.tdo_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.String;
                Param.ParameterName = "@tdo_nome";
                Param.Size = 100;
                Param.Value = entity.tdo_nome;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.String;
                Param.ParameterName = "@tdo_sigla";
                Param.Size = 10;
                if (!string.IsNullOrEmpty(entity.tdo_sigla))
                {
                    Param.Value = entity.tdo_sigla;
                }
                else
                {
                    Param.Value = DBNull.Value;
                }
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@tdo_validacao";
                Param.Size = 1;
                if (entity.tdo_validacao > 0)
                {
                    Param.Value = entity.tdo_validacao;
                }
                else
                {
                    Param.Value = DBNull.Value;
                }
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Byte;
                Param.ParameterName = "@tdo_situacao";
                Param.Size = 1;
                Param.Value = entity.tdo_situacao;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.DateTime;
                Param.ParameterName = "@tdo_dataCriacao";
                Param.Size = 16;
                Param.Value = entity.tdo_dataCriacao;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.DateTime;
                Param.ParameterName = "@tdo_dataAlteracao";
                Param.Size = 16;
                Param.Value = entity.tdo_dataAlteracao;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@tdo_integridade";
                Param.Size = 4;
                Param.Value = entity.tdo_integridade;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@tdo_classificacao";
                Param.Size = 1;
                if (entity.tdo_classificacao > 0)
                    Param.Value = entity.tdo_classificacao;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.String;
                Param.ParameterName = "@tdo_atributos";
                Param.Size = 1024;
                if (!string.IsNullOrEmpty(entity.tdo_atributos))
                    Param.Value = entity.tdo_atributos;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);
            }
        }

        /// <summary>
        /// Configura os parametros do metodo de Alterar.
        /// </summary>
        /// <param name="qs">
        /// Objeto da Store Procedure.
        /// </param>
        /// <param name="entity">
        /// Entidade com os dados para preenchimento dos parametros.
        /// </param>
        protected override void ParamAlterar(QueryStoredProcedure qs, SYS_TipoDocumentacao entity)
        {
            if (entity != null & qs != null)
            {
                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@tdo_id";
                Param.Size = 16;
                Param.Value = entity.tdo_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.String;
                Param.ParameterName = "@tdo_nome";
                Param.Size = 100;
                Param.Value = entity.tdo_nome;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.String;
                Param.ParameterName = "@tdo_sigla";
                Param.Size = 10;
                if (!string.IsNullOrEmpty(entity.tdo_sigla))
                {
                    Param.Value = entity.tdo_sigla;
                }
                else
                {
                    Param.Value = DBNull.Value;
                }
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@tdo_validacao";
                Param.Size = 1;
                if (entity.tdo_validacao > 0)
                {
                    Param.Value = entity.tdo_validacao;
                }
                else
                {
                    Param.Value = DBNull.Value;
                }
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Byte;
                Param.ParameterName = "@tdo_situacao";
                Param.Size = 1;
                Param.Value = entity.tdo_situacao;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.DateTime;
                Param.ParameterName = "@tdo_dataCriacao";
                Param.Size = 16;
                Param.Value = entity.tdo_dataCriacao;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.DateTime;
                Param.ParameterName = "@tdo_dataAlteracao";
                Param.Size = 16;
                Param.Value = entity.tdo_dataAlteracao;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@tdo_integridade";
                Param.Size = 4;
                Param.Value = entity.tdo_integridade;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@tdo_classificacao";
                Param.Size = 1;
                if (entity.tdo_classificacao > 0)
                    Param.Value = entity.tdo_classificacao;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.String;
                Param.ParameterName = "@tdo_atributos";
                Param.Size = 1024;
                if (!string.IsNullOrEmpty(entity.tdo_atributos))
                    Param.Value = entity.tdo_atributos;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);
            }
        }

        /// <summary>
        /// Configura os parametros do metodo de Deletar.
        /// </summary>
        /// <param name="qs">
        /// Objeto da Store Procedure.
        /// </param>
        /// <param name="entity">
        /// Entidade com os dados para preenchimento dos parametros.
        /// </param>
        protected override void ParamDeletar(QueryStoredProcedure qs, SYS_TipoDocumentacao entity)
        {
            if (entity != null & qs != null)
            {
                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@tdo_id";
                Param.Size = 16;
                Param.Value = entity.tdo_id;
                qs.Parameters.Add(Param);
            }
        }

        /// <summary>
        /// Recebe o valor do auto incremento e coloca na propriedade
        /// </summary>
        /// <param name="qs">
        /// Objeto da Store Procedure
        /// </param>
        protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, SYS_TipoDocumentacao entity)
        {
            entity.tdo_id = new Guid(qs.Return.Rows[0][0].ToString());
            return (entity.tdo_id != Guid.Empty);
        }
    }
}