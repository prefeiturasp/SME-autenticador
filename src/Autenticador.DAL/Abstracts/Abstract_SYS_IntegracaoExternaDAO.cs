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
    /// Classe abstrata de SYS_IntegracaoExterna
    /// </summary>
    public abstract class Abstract_SYS_IntegracaoExternaDAO : Abstract_DAL<SYS_IntegracaoExterna>
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
        protected override void ParamCarregar(QuerySelectStoredProcedure qs, SYS_IntegracaoExterna entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@ine_id";
            Param.Size = 16;
            Param.Value = entity.ine_id;
            qs.Parameters.Add(Param);


        }

        /// <summary>
        /// Configura os parametros do metodo de Inserir
        /// </summary>
        /// <param name="qs">Objeto da Store Procedure</param>
        protected override void ParamInserir(QuerySelectStoredProcedure qs, SYS_IntegracaoExterna entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@ine_id";
            Param.Size = 16;
            Param.Value = entity.ine_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@ine_descricao";
            Param.Size = 200;
            if (!string.IsNullOrEmpty(entity.ine_descricao))
                Param.Value = entity.ine_descricao;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@ine_urlInterna";
            Param.Size = 200;
            Param.Value = entity.ine_urlInterna;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@ine_urlExterna";
            Param.Size = 200;
            Param.Value = entity.ine_urlExterna;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@ine_dominio";
            Param.Size = 100;
            Param.Value = entity.ine_dominio;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@ine_tipo";
            Param.Size = 1;
            //Param.Value = entity.ine_tipo;
            if (entity.ine_tipo > 0)
            {
                Param.Value = entity.ine_tipo;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@ine_chave";
            Param.Size = 10;
            Param.Value = entity.ine_chave;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@ine_tokenInterno";
            Param.Size = 50;
            if (!string.IsNullOrEmpty(entity.ine_tokenInterno))
                Param.Value = entity.ine_tokenInterno;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@ine_tokenExterno";
            Param.Size = 50;
            if (!string.IsNullOrEmpty(entity.ine_tokenExterno))
                Param.Value = entity.ine_tokenExterno;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Boolean;
            Param.ParameterName = "@ine_proxy";
            Param.Size = 1;
            Param.Value = entity.ine_proxy;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@ine_proxyIP";
            Param.Size = 15;
            if (!string.IsNullOrEmpty(entity.ine_proxyIP))
                Param.Value = entity.ine_proxyIP;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@ine_proxyPorta";
            Param.Size = 10;
            if (!string.IsNullOrEmpty(entity.ine_proxyPorta))
                Param.Value = entity.ine_proxyPorta;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Boolean;
            Param.ParameterName = "@ine_proxyAutenticacao";
            Param.Size = 1;
            Param.Value = entity.ine_proxyAutenticacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@ine_proxyAutenticacaoUsuario";
            Param.Size = 100;
            if (!string.IsNullOrEmpty(entity.ine_proxyAutenticacaoUsuario))
                Param.Value = entity.ine_proxyAutenticacaoUsuario;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@ine_proxyAutenticacaoSenha";
            Param.Size = 100;
            if (!string.IsNullOrEmpty(entity.ine_proxyAutenticacaoSenha))
                Param.Value = entity.ine_proxyAutenticacaoSenha;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@ine_situacao";
            Param.Size = 1;
            Param.Value = entity.ine_situacao;
            qs.Parameters.Add(Param);

           
            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@iet_id";
            Param.Size = 1;
            if (entity.iet_id > 0)
            {
                Param.Value = entity.iet_id;
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
        protected override void ParamAlterar(QueryStoredProcedure qs, SYS_IntegracaoExterna entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@ine_id";
            Param.Size = 16;
            Param.Value = entity.ine_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@ine_descricao";
            Param.Size = 200;
            if (!string.IsNullOrEmpty(entity.ine_descricao))
                Param.Value = entity.ine_descricao;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@ine_urlInterna";
            Param.Size = 200;
            Param.Value = entity.ine_urlInterna;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@ine_urlExterna";
            Param.Size = 200;
            Param.Value = entity.ine_urlExterna;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@ine_dominio";
            Param.Size = 100;
            Param.Value = entity.ine_dominio;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@ine_tipo";
            Param.Size = 1;
            // Param.Value = entity.ine_tipo;
            if (entity.ine_tipo > 0)
            {
                Param.Value = entity.ine_tipo;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@ine_chave";
            Param.Size = 10;
            Param.Value = entity.ine_chave;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@ine_tokenInterno";
            Param.Size = 50;
            if (!string.IsNullOrEmpty(entity.ine_tokenInterno))
                Param.Value = entity.ine_tokenInterno;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@ine_tokenExterno";
            Param.Size = 50;
            if (!string.IsNullOrEmpty(entity.ine_tokenExterno))
                Param.Value = entity.ine_tokenExterno;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Boolean;
            Param.ParameterName = "@ine_proxy";
            Param.Size = 1;
            Param.Value = entity.ine_proxy;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@ine_proxyIP";
            Param.Size = 15;
            if (!string.IsNullOrEmpty(entity.ine_proxyIP))
                Param.Value = entity.ine_proxyIP;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@ine_proxyPorta";
            Param.Size = 10;
            if (!string.IsNullOrEmpty(entity.ine_proxyPorta))
                Param.Value = entity.ine_proxyPorta;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Boolean;
            Param.ParameterName = "@ine_proxyAutenticacao";
            Param.Size = 1;
            Param.Value = entity.ine_proxyAutenticacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@ine_proxyAutenticacaoUsuario";
            Param.Size = 100;
            if (!string.IsNullOrEmpty(entity.ine_proxyAutenticacaoUsuario))
                Param.Value = entity.ine_proxyAutenticacaoUsuario;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@ine_proxyAutenticacaoSenha";
            Param.Size = 100;
            if (!string.IsNullOrEmpty(entity.ine_proxyAutenticacaoSenha))
                Param.Value = entity.ine_proxyAutenticacaoSenha;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@ine_situacao";
            Param.Size = 1;
            Param.Value = entity.ine_situacao;
            qs.Parameters.Add(Param);

            
            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@iet_id";
            Param.Size = 1;
            if (entity.iet_id > 0)
            {
                Param.Value = entity.iet_id;
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
        protected override void ParamDeletar(QueryStoredProcedure qs, SYS_IntegracaoExterna entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@ine_id";
            Param.Size = 16;
            Param.Value = entity.ine_id;
            qs.Parameters.Add(Param);


        }

        /// <summary>
        /// Recebe o valor do auto incremento e coloca na propriedade 
        /// </summary>
        /// <param name="qs">Objeto da Store Procedure</param>
        protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, SYS_IntegracaoExterna entity)
        {
            entity.ine_id = new Guid(qs.Return.Rows[0][0].ToString());
            return (entity.ine_id != Guid.Empty);
        }
    }
}

