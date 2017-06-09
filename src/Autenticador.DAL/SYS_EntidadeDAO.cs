using Autenticador.DAL.Abstracts;
using Autenticador.Entities;
using CoreLibrary.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Autenticador.DAL
{
    public class SYS_EntidadeDAO : Abstract_SYS_EntidadeDAO
    {
        /// <summary>
        /// Retorna um datatable contendo todas as entidades que não foram excluídas logicamente,
        /// filtradas por entidade (diferente do parametro informado), tipo de entidade, razão
        /// social, nome fantasia, cnpj, situacao
        /// </summary>
        /// <param name="ent_id">
        /// Id da tabela SYS_Entidade do bd
        /// </param>
        /// <param name="ten_id">
        /// Id da tabela SYS_TipoEntidade do bd
        /// </param>
        /// <param name="ent_razaoSocial">
        /// Campo ent_razaoSocial da tabela SYS_Entidade do bd
        /// </param>
        /// <param name="ent_nomeFantasia">
        /// Campo ent_nomeFantasia da tabela SYS_Entidade do bd
        /// </param>
        /// <param name="ent_CNPJ">
        /// Campo ent_CNPJ da tabela SYS_Entidade do bd
        /// </param>
        /// <param name="ent_situacao">
        /// Campo ent_situcao da tabela SYS_Entidade do bd
        /// </param>
        /// <param name="paginado">
        /// Indica se o datatable será paginado ou não
        /// </param>
        /// <param name="currentPage">
        /// Página atual do grid
        /// </param>
        /// <param name="pageSize">
        /// Total de registros por página do grid
        /// </param>
        /// <param name="totalRecord">
        /// Total de registros retornado na busca
        /// </param>
        /// <returns>
        /// DataTable com as entidades
        /// </returns>
        public DataTable SelectBy_All
        (
            Guid ent_id
            , Guid ten_id
            , string ent_razaoSocial
            , string ent_nomeFantasia
            , string ent_cnpj
            , string ent_codigo
            , byte ent_situacao
            , bool paginado
            , int currentPage
            , int pageSize
            , out int totalRecords
        )
        {
            //DataTable dt = new DataTable();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Entidade_SelectBy_All", this._Banco);
            try
            {
                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@ent_id";
                Param.Size = 16;
                if (ent_id != Guid.Empty)
                    Param.Value = ent_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@ten_id";
                Param.Size = 16;
                if (ten_id != Guid.Empty)
                    Param.Value = ten_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@ent_razaoSocial";
                Param.Size = 200;
                if (!string.IsNullOrEmpty(ent_razaoSocial))
                    Param.Value = ent_razaoSocial;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@ent_nomeFantasia";
                Param.Size = 200;
                if (!string.IsNullOrEmpty(ent_nomeFantasia))
                    Param.Value = ent_nomeFantasia;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@ent_cnpj";
                Param.Size = 14;
                if (!string.IsNullOrEmpty(ent_cnpj))
                    Param.Value = ent_cnpj;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@ent_codigo";
                Param.Size = 14;
                if (!string.IsNullOrEmpty(ent_codigo))
                    Param.Value = ent_codigo;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Byte;
                Param.ParameterName = "@ent_situacao";
                Param.Size = 1;
                if (ent_situacao > 0)
                    Param.Value = ent_situacao;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                if (paginado)
                    totalRecords = qs.Execute(currentPage, pageSize);
                else
                {
                    qs.Execute();
                    totalRecords = qs.Return.Rows.Count;
                }

                //if (qs.Return.Rows.Count > 0)
                //dt = qs.Return;

                //return dt;
                return qs.Return;
            }
            catch
            {
                throw;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        /// <summary>
        /// Método na qual retorna datatable com regsitros de entidade filtrado por usuario e grupo usuario
        /// </summary>
        public DataTable SelectBy_UsuarioGrupoUA
        (
            Guid ent_id
            , Guid gru_id
            , Guid usu_id
            , byte ent_situacao
            , bool paginado
            , int currentPage
            , int pageSize
            , out int totalRecords
        )
        {
            //DataTable dt = new DataTable();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Entidade_SelectBy_UsuarioGrupoUA", this._Banco);
            try
            {
                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@ent_id";
                Param.Size = 16;
                if (ent_id != Guid.Empty)
                    Param.Value = ent_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@gru_id";
                Param.Size = 16;
                if (gru_id != Guid.Empty)
                    Param.Value = gru_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@usu_id";
                Param.Size = 16;
                if (usu_id != Guid.Empty)
                    Param.Value = usu_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Byte;
                Param.ParameterName = "@ent_situacao";
                Param.Size = 1;
                if (ent_situacao > 0)
                    Param.Value = ent_situacao;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                if (paginado)
                    totalRecords = qs.Execute(currentPage, pageSize);
                else
                {
                    qs.Execute();
                    totalRecords = qs.Return.Rows.Count;
                }

                //if (qs.Return.Rows.Count > 0)
                //dt = qs.Return;

                //return dt;
                return qs.Return;
            }
            catch
            {
                throw;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        /// <summary>
        /// Retorna um datatable contendo todas as entidades que não foram excluídas logicamente e
        /// que estão ligadas a pelo menos um sistema, filtradas por entidade (diferente do parametro
        /// informado), tipo de entidade, razão social, nome fantasia, cnpj, situacao
        /// </summary>
        /// <param name="ent_id">
        /// Id da tabela SYS_Entidade do bd
        /// </param>
        /// <param name="ten_id">
        /// Id da tabela SYS_TipoEntidade do bd
        /// </param>
        /// <param name="ent_razaoSocial">
        /// Campo ent_razaoSocial da tabela SYS_Entidade do bd
        /// </param>
        /// <param name="ent_nomeFantasia">
        /// Campo ent_nomeFantasia da tabela SYS_Entidade do bd
        /// </param>
        /// <param name="ent_CNPJ">
        /// Campo ent_CNPJ da tabela SYS_Entidade do bd
        /// </param>
        /// <param name="ent_situacao">
        /// Campo ent_situcao da tabela SYS_Entidade do bd
        /// </param>
        /// <param name="paginado">
        /// Indica se o datatable será paginado ou não
        /// </param>
        /// <param name="currentPage">
        /// Página atual do grid
        /// </param>
        /// <param name="pageSize">
        /// Total de registros por página do grid
        /// </param>
        /// <param name="totalRecord">
        /// Total de registros retornado na busca
        /// </param>
        /// <returns>
        /// DataTable com as entidades
        /// </returns>
        public DataTable SelectBy_SistemaEntidade
        (
            Guid ent_id
            , Guid ten_id
            , string ent_razaoSocial
            , string ent_nomeFantasia
            , string ent_cnpj
            , byte ent_situacao
            , bool paginado
            , int currentPage
            , int pageSize
            , out int totalRecords
        )
        {
            //DataTable dt = new DataTable();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Entidade_SelectBy_SistemaEntidade", this._Banco);
            try
            {
                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@ent_id";
                Param.Size = 16;
                if (ent_id != Guid.Empty)
                    Param.Value = ent_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@ten_id";
                Param.Size = 16;
                if (ten_id != Guid.Empty)
                    Param.Value = ten_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@ent_razaoSocial";
                Param.Size = 200;
                if (!string.IsNullOrEmpty(ent_razaoSocial))
                    Param.Value = ent_razaoSocial;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@ent_nomeFantasia";
                Param.Size = 200;
                if (!string.IsNullOrEmpty(ent_nomeFantasia))
                    Param.Value = ent_nomeFantasia;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@ent_cnpj";
                Param.Size = 14;
                if (!string.IsNullOrEmpty(ent_cnpj))
                    Param.Value = ent_cnpj;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Byte;
                Param.ParameterName = "@ent_situacao";
                Param.Size = 1;
                if (ent_situacao > 0)
                    Param.Value = ent_situacao;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                if (paginado)
                    totalRecords = qs.Execute(currentPage, pageSize);
                else
                {
                    qs.Execute();
                    totalRecords = qs.Return.Rows.Count;
                }

                //if (qs.Return.Rows.Count > 0)
                //dt = qs.Return;

                //return dt;
                return qs.Return;
            }
            catch
            {
                throw;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        /// <summary>
        /// Retorna true/false para saber se a razão social ou CNPJ já está cadastrada filtradas por
        /// entidade (diferente do parametro informado), razão social, cnpj, situacao.
        /// </summary>
        /// <param name="ent_id">
        /// Id da tabela SYS_Entidade do bd
        /// </param>
        /// <param name="ent_razaoSocial">
        /// Campo ent_razaoSocial da tabela SYS_Entidade do bd
        /// </param>
        /// <param name="ent_CNPJ">
        /// Campo ent_CNPJ da tabela SYS_Entidade do bd
        /// </param>
        /// <param name="ent_situacao">
        /// Campo ent_situcao da tabela SYS_Entidade do bd
        /// </param>
        /// <returns>
        /// Retorna true = razão/cnpj já cadastrado | false para razão/cnpj ainda não cadastrado
        /// </returns>
        public bool SelectBy_ent_razaoSocial_ent_CNPJ
        (
            Guid ent_id
            , string ent_razaoSocial
            , string ent_cnpj
            , byte ent_situacao
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Entidade_SelectBy_ent_razaoSocial_ent_CNPJ", this._Banco);
            try
            {
                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@ent_id";
                Param.Size = 16;
                if (ent_id != Guid.Empty)
                    Param.Value = ent_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@ent_razaoSocial";
                Param.Size = 200;
                if (!string.IsNullOrEmpty(ent_razaoSocial))
                    Param.Value = ent_razaoSocial;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@ent_cnpj";
                Param.Size = 14;
                if (!string.IsNullOrEmpty(ent_cnpj))
                    Param.Value = ent_cnpj;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Byte;
                Param.ParameterName = "@ent_situacao";
                Param.Size = 1;
                if (ent_situacao > 0)
                    Param.Value = ent_situacao;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                qs.Execute();

                if (qs.Return.Rows.Count > 0)
                    return true;

                return false;
            }
            catch
            {
                throw;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        /// <summary>
        /// Método alterado para que o delete não faça exclusão física e sim lógica (update).
        /// </summary>
        /// <param name="entity">
        /// Entidade SYS_Entidade
        /// </param>
        /// <returns>
        /// true = sucesso | false = fracasso
        /// </returns>
        public override bool Delete(Autenticador.Entities.SYS_Entidade entity)
        {
            this.__STP_DELETE = "NEW_SYS_Entidade_Update_Situacao";
            return base.Delete(entity);
        }

        /// <summary>
        /// Seleciona campo integridade da entidade
        /// </summary>
        /// <param name="ent_id">
        /// Id da tabela SYS_Entidade do bd
        /// </param>
        /// <returns>
        /// true = sucesso | false = fracasso
        /// </returns>
        public int Select_Integridade
        (
            Guid ent_id
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Entidade_Select_Integridade", this._Banco);
            try
            {
                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@ent_id";
                Param.Size = 16;
                if (ent_id != Guid.Empty)
                    Param.Value = ent_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                qs.Execute();

                if (qs.Return.Rows.Count > 0)
                    return Convert.ToInt32(qs.Return.Rows[0]["ent_integridade"].ToString());

                return -1;
            }
            catch
            {
                throw;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        /// <summary>
        /// Incrementa uma unidade no campo integridade da entidade
        /// </summary>
        /// <param name="ent_id">
        /// Id da tabela SYS_Entidade do bd
        /// </param>
        /// <returns>
        /// true = sucesso | false = fracasso
        /// </returns>
        public bool Update_IncrementaIntegridade
        (
            Guid ent_id
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Entidade_INCREMENTA_INTEGRIDADE", this._Banco);
            try
            {
                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@ent_id";
                Param.Size = 16;
                if (ent_id != Guid.Empty)
                    Param.Value = ent_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                qs.Execute();

                return true;
            }
            catch
            {
                throw;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        /// <summary>
        /// Decrementa uma unidade no campo integridade da entidade
        /// </summary>
        /// <param name="ent_id">
        /// Id da tabela SYS_Entidade do bd
        /// </param>
        /// <returns>
        /// true = sucesso | false = fracasso
        /// </returns>
        public bool Update_DecrementaIntegridade
        (
            Guid ent_id
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Entidade_DECREMENTA_INTEGRIDADE", this._Banco);
            try
            {
                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@ent_id";
                Param.Size = 16;
                if (ent_id != Guid.Empty)
                    Param.Value = ent_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                qs.Execute();

                return true;
            }
            catch
            {
                throw;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        public List<SYS_Entidade> SelecionarPorIdGrupo(Guid idEntidade, Guid idGrupo)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("MS_EntidadeSelecionarPorIdGrupo", _Banco);
            List<SYS_Entidade> Lista = new List<SYS_Entidade>();

            try
            {
                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@idEntidade";
                Param.Size = 16;
                if (idEntidade != Guid.Empty)
                    Param.Value = idEntidade;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@idGrupo";
                Param.Size = 16;
                if (idGrupo != Guid.Empty)
                    Param.Value = idGrupo;
                else
                    Param.Value = DBNull.Value;

                qs.Parameters.Add(Param);

                qs.Execute();

                Lista.AddRange(from DataRow row in qs.Return.Rows
                               select DataRowToEntity(row, new SYS_Entidade()));

                return Lista;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        public List<SYS_Entidade> SelecionarTodasLigadasPorSistemas()
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("MS_EntidadeSelecionarTodasLigadasPorSistema", _Banco);
            List<SYS_Entidade> Lista = new List<SYS_Entidade>();

            try
            {
                qs.Execute();

                Lista.AddRange(from DataRow row in qs.Return.Rows
                               select DataRowToEntity(row, new SYS_Entidade()));

                return Lista;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        /// <summary>
        /// Selecionar as entidades filhas (subentidades)
        /// </summary>
        /// <param name="entidadeId">
        /// Id da entidade
        /// </param>
        /// <returns>
        /// Lista de entidades
        /// </returns>
        public DataTable SelecionarEntidadesFilhas(Guid entidadeId)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("MS_EntidadeSelecionarEntidadesFilhas", _Banco);
            List<SYS_Entidade> Lista = new List<SYS_Entidade>();

            try
            {
                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@entidadeId";
                Param.Size = 16;
                if (entidadeId != Guid.Empty)
                    Param.Value = entidadeId;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                qs.Execute();

                return qs.Return;

                //Lista.AddRange(from DataRow row in qs.Return.Rows
                //               select DataRowToEntity(row, new SYS_Entidade()));
                //return Lista;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        /// <summary>
        /// Parâmetros para efetuar a inclusão com data de criação e de alteração fixas
        /// </summary>
        protected override void ParamInserir(QuerySelectStoredProcedure qs, Autenticador.Entities.SYS_Entidade entity)
        {
            entity.ent_dataCriacao = entity.ent_dataAlteracao = DateTime.Now;
            base.ParamInserir(qs, entity);
            qs.Parameters.RemoveAt("@ent_id");

            if (entity.ent_idSuperior == Guid.Empty)
            {
                qs.Parameters["@ent_idSuperior"].Value = DBNull.Value;
            }
        }

        protected override bool Inserir(Entities.SYS_Entidade entity)
        {
            __STP_INSERT = "NEW_SYS_Entidade_INSERT";
            return base.Inserir(entity);
        }

        /// <summary>
        /// Parâmetros para efetuar a alteração preservando a data de criação e a integridade
        /// </summary>
        protected override void ParamAlterar(QueryStoredProcedure qs, Autenticador.Entities.SYS_Entidade entity)
        {
            entity.ent_dataAlteracao = DateTime.Now;
            base.ParamAlterar(qs, entity);
            qs.Parameters.RemoveAt("@ent_dataCriacao");
            qs.Parameters.RemoveAt("@ent_integridade");

            if (entity.ent_idSuperior == Guid.Empty)
            {
                qs.Parameters["@ent_idSuperior"].Value = DBNull.Value;
            }
        }

        /// <summary>
        /// Método alterado para que o update não faça a alteração da data de criação e da integridade
        /// </summary>
        /// <param name="entity">
        /// Entidade SYS_Entidade
        /// </param>
        /// <returns>
        /// true = sucesso | false = fracasso
        /// </returns>
        protected override bool Alterar(Autenticador.Entities.SYS_Entidade entity)
        {
            this.__STP_UPDATE = "NEW_SYS_Entidade_UPDATE";
            return base.Alterar(entity);
        }

        /// <summary>
        /// Parâmetros para efetuar a exclusão lógica.
        /// </summary>
        protected override void ParamDeletar(QueryStoredProcedure qs, Autenticador.Entities.SYS_Entidade entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@ent_id";
            Param.Size = 16;
            Param.Value = entity.ent_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@ent_situacao";
            Param.Size = 1;
            Param.Value = 3;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@ent_dataAlteracao";
            Param.Size = 8;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);
        }
    }
}