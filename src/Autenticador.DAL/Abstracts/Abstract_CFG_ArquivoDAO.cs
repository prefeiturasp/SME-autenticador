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
    /// Classe abstrata de CFG_Arquivo
    /// </summary>
    public abstract class Abstract_CFG_ArquivoDAO : Abstract_DAL<CFG_Arquivo>
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
        protected override void ParamCarregar(QuerySelectStoredProcedure qs, CFG_Arquivo entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Int64;
            Param.ParameterName = "@arq_id";
            Param.Size = 8;
            Param.Value = entity.arq_id;
            qs.Parameters.Add(Param);


        }

        /// <summary>
        /// Configura os parametros do metodo de Inserir
        /// </summary>
        /// <param name="qs">Objeto da Store Procedure</param>
        protected override void ParamInserir(QuerySelectStoredProcedure qs, CFG_Arquivo entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@arq_nome";
            Param.Size = 256;
            Param.Value = entity.arq_nome;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Int64;
            Param.ParameterName = "@arq_tamanhoKB";
            Param.Size = 8;
            Param.Value = entity.arq_tamanhoKB;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@arq_typeMime";
            Param.Size = 200;
            Param.Value = entity.arq_typeMime;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Binary;
            Param.ParameterName = "@arq_data";
            Param.Size = 2147483647;
            Param.Value = entity.arq_data;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@arq_situacao";
            Param.Size = 1;
            Param.Value = entity.arq_situacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@arq_dataCriacao";
            Param.Size = 16;
            Param.Value = entity.arq_dataCriacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@arq_dataAlteracao";
            Param.Size = 16;
            Param.Value = entity.arq_dataAlteracao;
            qs.Parameters.Add(Param);


        }

        /// <summary>
        /// Configura os parametros do metodo de Alterar
        /// </summary>
        /// <param name="qs">Objeto da Store Procedure</param>
        protected override void ParamAlterar(QueryStoredProcedure qs, CFG_Arquivo entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Int64;
            Param.ParameterName = "@arq_id";
            Param.Size = 8;
            Param.Value = entity.arq_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@arq_nome";
            Param.Size = 256;
            Param.Value = entity.arq_nome;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Int64;
            Param.ParameterName = "@arq_tamanhoKB";
            Param.Size = 8;
            Param.Value = entity.arq_tamanhoKB;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@arq_typeMime";
            Param.Size = 200;
            Param.Value = entity.arq_typeMime;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@arq_data";
            Param.Size = 2147483647;
            Param.Value = entity.arq_data;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@arq_situacao";
            Param.Size = 1;
            Param.Value = entity.arq_situacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@arq_dataCriacao";
            Param.Size = 16;
            Param.Value = entity.arq_dataCriacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@arq_dataAlteracao";
            Param.Size = 16;
            Param.Value = entity.arq_dataAlteracao;
            qs.Parameters.Add(Param);


        }

        /// <summary>
        /// Configura os parametros do metodo de Deletar
        /// </summary>
        /// <param name="qs">Objeto da Store Procedure</param>
        protected override void ParamDeletar(QueryStoredProcedure qs, CFG_Arquivo entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Int64;
            Param.ParameterName = "@arq_id";
            Param.Size = 8;
            Param.Value = entity.arq_id;
            qs.Parameters.Add(Param);


        }

        /// <summary>
        /// Recebe o valor do auto incremento e coloca na propriedade 
        /// </summary>
        /// <param name="qs">Objeto da Store Procedure</param>
        protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, CFG_Arquivo entity)
        {
            entity.arq_id = Convert.ToInt32(qs.Return.Rows[0][0]);
            return (entity.arq_id > 0);
        }
    }
}

