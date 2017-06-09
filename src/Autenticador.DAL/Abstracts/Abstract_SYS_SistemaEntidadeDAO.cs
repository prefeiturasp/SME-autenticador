/*
	Classe gerada automaticamente pelo Code Creator
*/

using System;
using System.Data;
using CoreLibrary.Data.Common;
using CoreLibrary.Data.Common.Abstracts;
using Autenticador.Entities;

namespace Autenticador.DAL.Abstracts
{

    /// <summary>
    /// Classe abstrata de SYS_SistemaEntidade
    /// </summary>
    public abstract class Abstract_SYS_SistemaEntidadeDAO : Abstract_DAL<SYS_SistemaEntidade>
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
        protected override void ParamCarregar(QuerySelectStoredProcedure qs, SYS_SistemaEntidade entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@sis_id";
            Param.Size = 4;
            Param.Value = entity.sis_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@ent_id";
            Param.Size = 16;
            Param.Value = entity.ent_id;
            qs.Parameters.Add(Param);


        }

        /// <summary>
        /// Configura os parametros do metodo de Inserir
        /// </summary>
        /// <param name="qs">Objeto da Store Procedure</param>
        protected override void ParamInserir(QuerySelectStoredProcedure qs, SYS_SistemaEntidade entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@sis_id";
            Param.Size = 4;
            Param.Value = entity.sis_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@ent_id";
            Param.Size = 16;
            Param.Value = entity.ent_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@sen_chaveK1";
            Param.Size = 100;
            if (!string.IsNullOrEmpty(entity.sen_chaveK1))
                Param.Value = entity.sen_chaveK1;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@sen_urlAcesso";
            Param.Size = 200;
            if (!string.IsNullOrEmpty(entity.sen_urlAcesso))
                Param.Value = entity.sen_urlAcesso;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);
            
            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@sen_logoCliente";
            Param.Size = 2000;
            if (!string.IsNullOrEmpty(entity.sen_logoCliente))
                Param.Value = entity.sen_logoCliente;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@sen_urlCliente";
            Param.Size = 1000;
            if (!string.IsNullOrEmpty(entity.sen_urlCliente))
                Param.Value = entity.sen_urlCliente;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@sen_situacao";
            Param.Size = 1;
            Param.Value = entity.sen_situacao;
            qs.Parameters.Add(Param);

        }

        /// <summary>
        /// Configura os parametros do metodo de Alterar
        /// </summary>
        /// <param name="qs">Objeto da Store Procedure</param>
        protected override void ParamAlterar(QueryStoredProcedure qs, SYS_SistemaEntidade entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@sis_id";
            Param.Size = 4;
            Param.Value = entity.sis_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@ent_id";
            Param.Size = 16;
            Param.Value = entity.ent_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@sen_chaveK1";
            Param.Size = 100;
            if (!string.IsNullOrEmpty(entity.sen_chaveK1))
                Param.Value = entity.sen_chaveK1;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@sen_urlAcesso";
            Param.Size = 200;
            if (!string.IsNullOrEmpty(entity.sen_urlAcesso))
                Param.Value = entity.sen_urlAcesso;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);
          
            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@sen_logoCliente";
            Param.Size = 2000;
            if (!string.IsNullOrEmpty(entity.sen_logoCliente))
                Param.Value = entity.sen_logoCliente;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@sen_urlCliente";
            Param.Size = 1000;
            if (!string.IsNullOrEmpty(entity.sen_urlCliente))
                Param.Value = entity.sen_urlCliente;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@sen_situacao";
            Param.Size = 1;
            Param.Value = entity.sen_situacao;
            qs.Parameters.Add(Param);

        }

        /// <summary>
        /// Configura os parametros do metodo de Deletar
        /// </summary>
        /// <param name="qs">Objeto da Store Procedure</param>
        protected override void ParamDeletar(QueryStoredProcedure qs, SYS_SistemaEntidade entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@sis_id";
            Param.Size = 4;
            Param.Value = entity.sis_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@ent_id";
            Param.Size = 16;
            Param.Value = entity.ent_id;
            qs.Parameters.Add(Param);


        }

        /// <summary>
        /// Recebe o valor do auto incremento e coloca na propriedade 
        /// </summary>
        /// <param name="qs">Objeto da Store Procedure</param>
        protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, SYS_SistemaEntidade entity)
        {
            entity.ent_id = new Guid(qs.Return.Rows[0][0].ToString());
            return (entity.ent_id != Guid.Empty);
        }
    }
}

