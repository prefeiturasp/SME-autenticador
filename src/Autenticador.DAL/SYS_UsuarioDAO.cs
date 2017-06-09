using Autenticador.DAL.Abstracts;
using Autenticador.DAL.Interfaces;
using Autenticador.Entities;
using CoreLibrary.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Autenticador.DAL
{
    public class SYS_UsuarioDAO : Abstract_SYS_UsuarioDAO, INewDALUsuario
    {
        /// <summary>
        /// Retorna um DataTable contendo todos os usuários que não
        /// foram excluídos logicamente, filtrando por:
        /// entidade, grupo e unidade adminsitrativa
        /// </summary>
        /// <param name="ent_id">Id da entidade</param>
        /// <param name="gru_id">Id do grupo</param>
        /// <param name="uad_id">Id da unidade administrativa</param>
        /// <param name="uad_idSuperior">Id da unidade administrativa superior</param>
        /// <param name="usu_idPermissao">Id do usuário utilizado para verificar permissão</param>
        /// <param name="gru_idPermissao">Id do grupo utilizado para verificar permissão</param>
        /// <param name="paginado">Indica se será paginado</param>
        /// <param name="currentPage">Página atual</param>
        /// <param name="pageSize">Quantidade de registros por página</param>
        /// <param name="totalRecords">Total de registros retornado</param>
        /// <returns>DataTable de usuários</returns>
        public DataTable SelectBy_GrupoUA
            (
                  Guid ent_id
                , Guid gru_id
                , Guid uad_id
                , Guid uad_idSuperior
                , Guid usu_idPermissao
                , Guid gru_idPermissao
                , bool paginado
                , int currentPage
                , int pageSize
                , out int totalRecords
            )
        {
            totalRecords = 0;
            DataTable dt = new DataTable();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Usuario_SelectBy_GrupoUA", _Banco);
            try
            {
                #region Parâmetros

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@ent_id";
                Param.Size = 16;
                Param.Value = ent_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@usu_idPermissao";
                Param.Size = 16;
                Param.Value = usu_idPermissao;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@gru_idPermissao";
                Param.Size = 16;
                Param.Value = gru_idPermissao;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@gru_id";
                Param.Size = 16;
                Param.Value = gru_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@uad_id";
                Param.Size = 16;
                if (uad_id != Guid.Empty)
                    Param.Value = uad_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@uad_idSuperior";
                Param.Size = 16;
                if (uad_idSuperior != Guid.Empty)
                    Param.Value = uad_idSuperior;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion Parâmetros

                if (paginado)
                {
                    if (pageSize == 0) pageSize = 1;
                    totalRecords = qs.Execute(currentPage / pageSize, pageSize);
                }
                else
                {
                    qs.Execute();
                    totalRecords = qs.Return.Rows.Count;
                }

                if (qs.Return.Rows.Count > 0)
                    dt = qs.Return;

                return dt;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        public DataTable SelectBy_Pesquisa(
            Guid ent_id
            , string usu_login
            , string usu_email
            , byte usu_situacao
            , string pes_nome
            , int currentPage
            , int pageSize
            , out int totalRecords)
        {
            DataTable dt = new DataTable();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Usuario_SelectBy_Pesquisa", _Banco);
            try
            {
                #region PARAMETROS

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
                Param.ParameterName = "@usu_login";
                Param.Size = 500;
                if (!String.IsNullOrEmpty(usu_login))
                    Param.Value = usu_login;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@usu_email";
                Param.Size = 500;
                if (!String.IsNullOrEmpty(usu_email))
                    Param.Value = usu_email;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Byte;
                Param.ParameterName = "@usu_bloqueado";
                Param.Size = 1;
                if (usu_situacao > 0)
                    Param.Value = usu_situacao;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@pes_nome";
                Param.Size = 200;
                if (!String.IsNullOrEmpty(pes_nome))
                    Param.Value = pes_nome;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

                totalRecords = qs.Execute(currentPage, pageSize);

                if (qs.Return.Rows.Count > 0)
                    dt = qs.Return;

                return dt;
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
        /// Expira a senha dos usuários que não alteraram no prazo de dias do parâmetro
        /// </summary>
        public void ExpirarSenhas()
        {
            QueryStoredProcedure qs = new QueryStoredProcedure("NEW_SYS_Usuario_ExpirarSenhas", _Banco);

            try
            {
                qs.Execute();
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        public DataTable SelectBy_ent_id_usu_email
        (
            Guid ent_id
            , string usu_email
        )
        {
            DataTable dt = new DataTable();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Usuario_SelecionaPorEmailEntidade", _Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@ent_id";
                Param.Size = 16;
                Param.Value = ent_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@usu_email";
                Param.Size = 500;
                Param.Value = usu_email;
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

                qs.Execute();

                if (qs.Return.Rows.Count > 0)
                    dt = qs.Return;

                return dt;
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

        public Guid SelectBy_pes_id
        (
            Guid pes_id
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Usuario_SelectBy_pes_id", _Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@pes_id";
                Param.Size = 16;
                if (pes_id != Guid.Empty)
                    Param.Value = pes_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

                qs.Execute();

                if (qs.Return.Rows.Count > 0)
                    return new Guid(qs.Return.Rows[0]["usu_id"].ToString());
                else
                    return Guid.Empty;
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
        /// Retorna true/false
        /// para saber se o login, email ou pessoa já está cadastrada
        /// filtradas por usuario (diferente do parametro informado), login, email, pessoa e situacao.
        /// </summary>
        /// <param name="ent_id"></param>
        /// <param name="usu_id">Id da tabela SYS_Usuario do bd</param>
        /// <param name="usu_dominio"></param>
        /// <param name="usu_login">Campo usu_login da tabela SYS_Usuario do bd</param>
        /// <param name="usu_email">Campo usu_email da tabela SYS_Usuario do bd</param>
        /// <param name="pes_id">Campo pes_id da tabela SYS_Usuario do bd</param>
        /// <param name="usu_situacao">Campo usu_situacao da tabela SYS_Usuario do bd</param>
        /// <returns>Retorna true = login/email/pessoa já cadastrado | false para login/email/pessoa ainda não cadastrado</returns>
        public bool SelectBy__ent_id_usu_login_usu_email_pes_id
        (
            Guid ent_id
            , Guid usu_id
            , string usu_dominio
            , string usu_login
            , string usu_email
            , Guid pes_id
            , byte usu_situacao
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Usuario_SelectBy_ent_id_usu_login_usu_email_pes_id", _Banco);
            try
            {
                #region PARAMETROS

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
                Param.ParameterName = "@usu_id";
                Param.Size = 16;
                if (usu_id != Guid.Empty)
                    Param.Value = usu_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@usu_login";
                Param.Size = 500;
                if (!string.IsNullOrEmpty(usu_login))
                    Param.Value = usu_login;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@usu_dominio";
                Param.Size = 100;
                if (!string.IsNullOrEmpty(usu_dominio))
                    Param.Value = usu_dominio;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@usu_email";
                Param.Size = 500;
                if (!string.IsNullOrEmpty(usu_email))
                    Param.Value = usu_email;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@pes_id";
                Param.Size = 16;
                if (pes_id != Guid.Empty)
                    Param.Value = pes_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Byte;
                Param.ParameterName = "@usu_situacao";
                Param.Size = 1;
                if (usu_situacao > 0)
                    Param.Value = usu_situacao;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

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
        /// Retorna uma lista de objetos não paginados contendo
        /// os usuários filtrado por grupo.
        /// A lista é ordenada pelo nome do usuário.
        /// </summary>
        /// <param name="gru_id">ID do grupo</param>
        /// <param name="paginado"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <returns>Lista de usuários do grupo</returns>
        public DataTable SelectBy_gru_id
        (
            Guid gru_id
            , bool paginado
            , int currentPage
            , int pageSize
            , out int totalRecords
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Usuario_SelectBy_gru_id", _Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@gru_id";
                Param.Size = 16;
                Param.Value = gru_id;
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

                if (paginado)
                    totalRecords = qs.Execute(currentPage, pageSize);
                else
                {
                    qs.Execute();
                    totalRecords = qs.Return.Rows.Count;
                }

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

        public virtual bool CarregarBy_ent_id_usu_login(SYS_Usuario entity)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Usuario_LOADBy_ent_id_usu_login", _Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@ent_id";
                Param.Size = 16;
                if (entity.ent_id != Guid.Empty)
                    Param.Value = entity.ent_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@usu_login";
                Param.Size = 500;
                if (!String.IsNullOrEmpty(entity.usu_login))
                    Param.Value = entity.usu_login;
                else
                    throw new ArgumentNullException("entity", "entity.usu_login vázio ou nulo");
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

                qs.Execute();

                if (qs.Return.Rows.Count > 0)
                {
                    entity = DataRowToEntity(qs.Return.Rows[0], entity, false);
                    return true;
                }
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


        public virtual bool CarregarBy_ent_id_usu_dominio_usu_login(SYS_Usuario entity)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Usuario_LOADBy_ent_id_usu_dominio_usu_login", _Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@ent_id";
                Param.Size = 16;
                if (entity.ent_id != Guid.Empty)
                    Param.Value = entity.ent_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@usu_dominio";
                Param.Size = 100;
                if (!String.IsNullOrEmpty(entity.usu_dominio))
                    Param.Value = entity.usu_dominio;
                else
                    throw new ArgumentNullException("entity", "entity.usu_dominio vázio ou nulo");
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@usu_login";
                Param.Size = 500;
                if (!String.IsNullOrEmpty(entity.usu_login))
                    Param.Value = entity.usu_login;
                else
                    throw new ArgumentNullException("entity", "entity.usu_login vázio ou nulo");
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

                qs.Execute();

                if (qs.Return.Rows.Count > 0)
                {
                    entity = DataRowToEntity(qs.Return.Rows[0], entity, false);
                    return true;
                }
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
        /// Seleciona usuário por documento e data de nascimento.
        /// </summary>
        /// <param name="ent_id">Entidade do usuário</param>
        /// <param name="psd_numero">Número do documento</param>
        /// <param name="tdo_id">ID do tipo de documento</param>
        /// <param name="pes_dataNascimento">Data de nascimento</param>
        /// <returns></returns>
        public DataTable SelecionaPorDocumentoDataNascimento(Guid ent_id, string psd_numero, Guid tdo_id, DateTime pes_dataNascimento)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Usuario_SelecionaPorDocumentoDataNascimento", _Banco);

            try
            {
                #region Parâmetros

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@ent_id";
                Param.Size = 16;
                Param.Value = ent_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@psd_numero";
                Param.Size = 50;
                Param.Value = psd_numero;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@tdo_id";
                Param.Size = 16;
                Param.Value = tdo_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.DateTime;
                Param.ParameterName = "@pes_dataNascimento";
                Param.Size = 16;
                Param.Value = pes_dataNascimento;
                qs.Parameters.Add(Param);

                #endregion Parâmetros

                qs.Execute();

                return qs.Return;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        /// <summary>
        /// Seleciona usuários por login.
        /// </summary>
        /// <param name="usu_login">Login do usuário.</param>
        /// <returns></returns>
        public List<SYS_Usuario> SelecionaPorLogin(string usu_login)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Usuario_SelecionaPorLogin", _Banco);

            try
            {
                #region Parâmetro

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@usu_login";
                Param.Size = 500;
                Param.Value = usu_login;
                qs.Parameters.Add(Param);

                #endregion Parâmetro

                qs.Execute();

                return qs.Return.Rows.Count > 0 ?
                    qs.Return.Rows.Cast<DataRow>().Select(p => DataRowToEntity(p, new SYS_Usuario())).ToList() :
                    new List<SYS_Usuario>();
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        /// <summary>
        /// Seleciona usuário por unidade administrativa.
        /// </summary>
        /// <param name="ent_id">Id da entidade do usuário.</param>
        /// <param name="uad_id">Id da unidade administrativa.</param>
        /// <param name="uad_codigo">Código da unidade administrativa.</param>
        /// <param name="trazerFoto">Indica se deve trazer a foto.</param>
        /// <param name="usu_id">ID do usuário.</param>
        /// <param name="dataAlteracao">Data base (data de alteração) para retorno dos registros.</param>
        /// <returns>DataTable com os usuários selecionados.</returns>
        public DataTable SelecionaPorUnidadeAdministrativa(Guid ent_id, Guid uad_id, string uad_codigo, bool trazerFoto, Guid usu_id, DateTime dataAlteracao)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Usuario_SelecionaPorUnidadeAdministrativa", _Banco);

            #region Parâmetro

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@ent_id";
            Param.Size = 16;
            Param.Value = ent_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@uad_id";
            Param.Size = 16;
            if (uad_id != Guid.Empty)
            {
                Param.Value = uad_id;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@uad_codigo";
            Param.Size = 20;
            if (!string.IsNullOrEmpty(uad_codigo))
            {
                Param.Value = uad_codigo;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Boolean;
            Param.ParameterName = "@trazerFoto";
            Param.Size = 1;
            Param.Value = trazerFoto;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@usu_id";
            Param.Size = 16;
            if (usu_id != Guid.Empty)
            {
                Param.Value = usu_id;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@dataAlteracao";
            Param.Size = 16;
            if (dataAlteracao != new DateTime())
            {
                Param.Value = dataAlteracao;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            #endregion Parâmetro

            qs.Execute();
            qs.Parameters.Clear();

            return qs.Return;
        }

        /// <summary>
        /// Seleciona usuário por entidade. Filtro opcional por pessoa.
        /// </summary>
        /// <param name="ent_id">ID da entidade do usuário.</param>
        /// <param name="pes_id">ID da pessoa do usuário. (opcional)</param>
        /// <param name="trazerFoto">Indica se deve trazer a foto.</param>
        /// <param name="dataAlteracao">Data base (data de alteração) para retorno dos registros.</param>
        /// <returns>Datatable com os usuários selecionados.</returns>
        public DataTable SelecionaPorEntidade(Guid ent_id, Guid pes_id, bool trazerFoto, DateTime dataAlteracao)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Usuario_SelecionaPorEntidade", _Banco);

            try
            {
                #region Parâmetros

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@ent_id";
                Param.Size = 16;
                Param.Value = ent_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@pes_id";
                Param.Size = 16;
                if (pes_id != Guid.Empty)
                {
                    Param.Value = pes_id;
                }
                else
                {
                    Param.Value = DBNull.Value;
                }
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Boolean;
                Param.ParameterName = "@trazerFoto";
                Param.Size = 1;
                Param.Value = trazerFoto;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.DateTime;
                Param.ParameterName = "@dataAlteracao";
                Param.Size = 16;
                if (dataAlteracao != new DateTime())
                {
                    Param.Value = dataAlteracao;
                }
                else
                {
                    Param.Value = DBNull.Value;
                }
                qs.Parameters.Add(Param);

                #endregion Parâmetros

                qs.Execute();

                return qs.Return;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        /// <summary>
        /// Altera a senha do usuário
        /// filtradas por id do usuario
        /// </summary>
        /// <param name="usu_id">Id da tabela SYS_Usuario do bd</param>
        /// <param name="usu_senha"></param>
        /// <returns>Retorna true/false</returns>
        public bool Update_Senha
        (
            Guid usu_id
            , string usu_senha
            , byte usu_criptografia
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Usuario_Update_Senha", _Banco);
            try
            {
                #region PARAMETROS

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
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@usu_senha";
                Param.Size = 256;
                Param.Value = usu_senha;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Byte;
                Param.ParameterName = "@usu_criptografia";
                Param.Size = 1;
                Param.Value = usu_criptografia;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.DateTime;
                Param.ParameterName = "@usu_dataAlteracao";
                Param.Size = 8;
                Param.Value = DateTime.Now;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.DateTime;
                Param.ParameterName = "@usu_dataAlteracaoSenha";
                Param.Size = 8;
                Param.Value = DateTime.Now;
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

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
        /// Parâmetros para efetuar a inclusão com data de criação e de alteração fixas
        /// </summary>
        protected override void ParamInserir(QuerySelectStoredProcedure qs, SYS_Usuario entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@usu_login";
            Param.Size = 500;
            if (!string.IsNullOrEmpty(entity.usu_login))
                Param.Value = entity.usu_login;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@usu_dominio";
            Param.Size = 100;
            if (!string.IsNullOrEmpty(entity.usu_dominio))
                Param.Value = entity.usu_dominio;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@usu_email";
            Param.Size = 500;
            if (!string.IsNullOrEmpty(entity.usu_email))
                Param.Value = entity.usu_email;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@usu_senha";
            Param.Size = 256;
            if (!string.IsNullOrEmpty(entity.usu_senha))
                Param.Value = entity.usu_senha;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@usu_criptografia";
            Param.Size = 1;
            if (entity.usu_criptografia > 0)
                Param.Value = entity.usu_criptografia;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@usu_situacao";
            Param.Size = 1;
            Param.Value = entity.usu_situacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@usu_dataCriacao";
            Param.Size = 16;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@usu_dataAlteracao";
            Param.Size = 16;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@usu_dataAlteracaoSenha";
            Param.Size = 16;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@pes_id";
            Param.Size = 16;
            if (entity.pes_id != Guid.Empty)
                Param.Value = entity.pes_id;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@usu_integridade";
            Param.Size = 4;
            Param.Value = entity.usu_integridade;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@ent_id";
            Param.Size = 16;
            Param.Value = entity.ent_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@usu_integracaoAD";
            Param.Size = 1;
            Param.Value = entity.usu_integracaoAD;
            qs.Parameters.Add(Param);
            
            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@usu_integracaoExterna";
            Param.Size = 1;
            if (entity.usu_integracaoExterna > 0)
            {
                Param.Value = entity.usu_integracaoExterna;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);
        }

        protected override bool Inserir(SYS_Usuario entity)
        {
            __STP_INSERT = "NEW_SYS_Usuario_INSERT";
            return base.Inserir(entity);
        }

        /// <summary>
        /// Parâmetros para efetuar a alteração preservando a senha, a data de criação e a integridade
        /// </summary>
        protected override void ParamAlterar(QueryStoredProcedure qs, SYS_Usuario entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@usu_id";
            Param.Size = 16;
            Param.Value = entity.usu_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@usu_login";
            Param.Size = 500;
            Param.Value = entity.usu_login;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@usu_email";
            Param.Size = 500;
            Param.Value = entity.usu_email;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@usu_dominio";
            Param.Size = 100;
            if (!string.IsNullOrEmpty(entity.usu_dominio))
                Param.Value = entity.usu_dominio;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@usu_criptografia";
            Param.Size = 1;
            if (entity.usu_criptografia > 0)
                Param.Value = entity.usu_criptografia;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@usu_situacao";
            Param.Size = 1;
            Param.Value = entity.usu_situacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@usu_dataAlteracao";
            Param.Size = 8;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@pes_id";
            Param.Size = 16;
            if (entity.pes_id != Guid.Empty)
                Param.Value = entity.pes_id;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@ent_id";
            Param.Size = 16;
            Param.Value = entity.ent_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@usu_integracaoAD";
            Param.Size = 1;
            Param.Value = entity.usu_integracaoAD;
            qs.Parameters.Add(Param);


            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@usu_integracaoExterna";
            Param.Size = 1;
            if (entity.usu_integracaoExterna > 0)
            {
                Param.Value = entity.usu_integracaoExterna;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);
        }

        protected override bool Alterar(SYS_Usuario entity)
        {
            __STP_UPDATE = "NEW_SYS_Usuario_UPDATE";
            return base.Alterar(entity);
        }

        /// <summary>
        /// Parâmetros para efetuar a exclusão lógica.
        /// </summary>
        protected override void ParamDeletar(QueryStoredProcedure qs, SYS_Usuario entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@usu_id";
            Param.Size = 16;
            Param.Value = entity.usu_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@usu_situacao";
            Param.Size = 1;
            Param.Value = 3;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@usu_dataAlteracao";
            Param.Size = 8;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);
        }

        /// <summary>
        /// Método alterado para que o delete não faça exclusão física e sim lógica (update).
        /// </summary>
        /// <param name="entity"> Entidade SYS_Entidade</param>
        /// <returns>true = sucesso | false = fracasso</returns>
        public override bool Delete(SYS_Usuario entity)
        {
            __STP_DELETE = "NEW_SYS_Usuario_Update_Situacao";
            return base.Delete(entity);
        }

        public long Select_Integridade
        (
            Guid usu_id
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Usuario_Select_Integridade", _Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@usu_id";
                Param.Size = 16;
                if (usu_id != Guid.Empty)
                    Param.Value = usu_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

                qs.Execute();

                if (qs.Return.Rows.Count > 0)
                    return Convert.ToInt64(qs.Return.Rows[0]["usu_integridade"].ToString());

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
        /// Incrementa uma unidade no campo integridade do usuario
        /// </summary>
        /// <param name="usu_id">Id do usuário</param>
        /// <returns>true = sucesso | false = fracasso</returns>
        public bool Update_IncrementaIntegridade
        (
            Guid usu_id
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Usuario_INCREMENTA_INTEGRIDADE", _Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@usu_id";
                Param.Size = 16;
                if (usu_id != Guid.Empty)
                    Param.Value = usu_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

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
        /// Decrementa uma unidade no campo integridade do usuario
        /// </summary>
        /// <param name="usu_id">Id do usuário</param>
        /// <returns>true = sucesso | false = fracasso</returns>
        public bool Update_DecrementaIntegridade
        (
            Guid usu_id
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Usuario_DECREMENTA_INTEGRIDADE", _Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@usu_id";
                Param.Size = 16;
                if (usu_id != Guid.Empty)
                    Param.Value = usu_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

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
        /// Atualiza o email de um usuário.
        /// </summary>
        /// <param name="usu_id">ID do usuário.</param>
        /// <param name="usu_email">Novo email do usuário.</param>
        /// <returns></returns>
        public bool AtualizaEmail(Guid usu_id, string usu_email)
        {
            QueryStoredProcedure qs = new QueryStoredProcedure("NEW_SYS_Usuario_AtualizaEmail", _Banco);

            try
            {
                #region Parâmetros

                Param = qs.NewParameter();
                Param.ParameterName = "@usu_id";
                Param.DbType = DbType.Guid;
                Param.Size = 16;
                Param.Value = usu_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.ParameterName = "@usu_email";
                Param.DbType = DbType.AnsiString;
                Param.Size = 500;
                Param.Value = usu_email;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.ParameterName = "@usu_dataAlteracao";
                Param.DbType = DbType.DateTime;
                Param.Size = 16;
                Param.Value = DateTime.Now;
                qs.Parameters.Add(Param);

                #endregion Parâmetros

                qs.Execute();

                return qs.Return > 0;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        /// <summary>
        /// Atualiza a situação do usuário. (não altera suas ligações com os grupos)
        /// </summary>
        /// <param name="usu_id">ID do usuário.</param>
        /// <param name="usu_situacao">Situação do usuário.</param>
        /// <returns></returns>
        public bool AtualizarSituacao(Guid usu_id, byte usu_situacao)
        {
            QueryStoredProcedure qs = new QueryStoredProcedure("NEW_SYS_Usuario_AtualizaSituacao", _Banco);

            #region Parâmetros

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@usu_id";
            Param.Size = 16;
            Param.Value = usu_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@usu_situacao";
            Param.Size = 1;
            Param.Value = usu_situacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@usu_dataAlteracao";
            Param.Size = 8;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);

            #endregion Parâmetros

            qs.Execute();

            return qs.Return > 0;
        }

        public List<SYS_Usuario> SelecionarUsuariosDaUnidadeAdministrativa(Guid idEntidade, Guid idUnidade)
        {
            DataTable dt = new DataTable();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("MS_SelecionarUsuariosDaUnidadeAdministrativa", this._Banco);
            try
            {
                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@ent_id";
                Param.Size = 16;
                if (idEntidade != Guid.Empty)
                    Param.Value = idEntidade;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@uad_id";
                Param.Size = 16;
                if (idUnidade != Guid.Empty)
                    Param.Value = idUnidade;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                qs.Execute();
                dt = qs.Return;

                var lista = new List<SYS_Usuario>();

                foreach (DataRow dr in qs.Return.Rows)
                {
                    SYS_Usuario entity = new SYS_Usuario();
                    lista.Add(this.DataRowToEntity(dr, entity));
                }

                return lista;
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

        public DataTable SelecionarPorIdGrupo(Guid idGrupo)
        {
            DataTable dt = new DataTable();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("MS_SelecionarUsuariosPorIdGrupo", _Banco);
            try
            {
                #region Parâmetros

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@idGrupo";
                Param.Size = 16;
                Param.Value = idGrupo;
                qs.Parameters.Add(Param);

                #endregion Parâmetros

                qs.Execute();

                if (qs.Return.Rows.Count > 0)
                    dt = qs.Return;

                return dt;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        public DataTable SelecionarPorIdUsuario(Guid idUsuario)
        {
            DataTable dt = new DataTable();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("MS_SelecionarUsuarioPorIdUsuario", _Banco);
            try
            {
                #region Parâmetros

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@idUsuario";
                Param.Size = 16;
                Param.Value = idUsuario;
                qs.Parameters.Add(Param);

                #endregion Parâmetros

                if (qs.Return.Rows.Count > 0)
                    dt = qs.Return;

                return dt;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }


        public SYS_Usuario Select_Usuario_By_LoginProvider_ProviderKey(string providerName, string providerKey)
        {

            List<SYS_Usuario> lt = new List<SYS_Usuario>();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("MS_Seleciona_Usuario_By_LoginProvider_ProviderKey", _Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.String;
                Param.ParameterName = "@LoginProvider";
                Param.Size = 128;
                Param.Value = providerName;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.String;
                Param.ParameterName = "@ProviderKey";
                Param.Size = 128;
                Param.Value = providerKey;
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

                qs.Execute();


                foreach (DataRow dr in qs.Return.Rows)
                {
                    SYS_Usuario entity = new SYS_Usuario();
                    lt.Add(this.DataRowToEntity(dr, entity));
                }

                return lt.FirstOrDefault();
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
        /// Seleciona os usuários ativos e a senha padrão, para comparação com a senha atual.
        /// </summary>
        /// <returns></returns>
        public DataTable SelecionaUsuariosSenhaPadrao()
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Usuario_SelecionaUsuariosSenhaPadrao", _Banco);
            //Sem limite de timeout
            qs.TimeOut = 0;
            try
            {
                qs.Execute();
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
        ///  Atualiza a situação dos usuários para senha expirada.
        /// </summary>
        /// <param name="usuIds">String concatenada com os ids dos usuários</param>
        public void ExpiraUsuariosSenhaPadrao(string usuIds)
        {
            QueryStoredProcedure qs = new QueryStoredProcedure("NEW_SYS_Usuario_ExpiraUsuariosSenhaPadrao", _Banco);
            try
            {
                #region Parametros

                Param = qs.NewParameter();
                Param.DbType = DbType.String;
                Param.ParameterName = "@usuIds";
                Param.Value = usuIds;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();
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
    }
}