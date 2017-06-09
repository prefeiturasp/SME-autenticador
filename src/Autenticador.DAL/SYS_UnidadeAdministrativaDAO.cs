using Autenticador.DAL.Abstracts;
using Autenticador.Entities;
using CoreLibrary.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml.XPath;

namespace Autenticador.DAL
{
    public class SYS_UnidadeAdministrativaDAO : Abstract_SYS_UnidadeAdministrativaDAO
    {
        /// <summary>
        /// Retorna uma lista contendo a unidade administrativa superior
        /// que não foram excluídas logicamente,
        /// filtrando por: entidade e unidade adminsitrativa.
        /// </summary>
        /// <param name="ent_id"></param>
        /// <param name="uad_idSupeior"></param>
        /// <returns></returns>
        public IList<SYS_UnidadeAdministrativa> ConsultarUASuperior(Guid ent_id, Guid uad_idSupeior)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_UnidadeAdministrativaSuperior_SelectBy_UASuperior", _Banco);
            List<SYS_UnidadeAdministrativa> Lista = new List<SYS_UnidadeAdministrativa>();

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
                Param.ParameterName = "@uad_idSupeior";
                Param.Size = 16;
                Param.Value = uad_idSupeior;
                qs.Parameters.Add(Param);

                #endregion Parâmetros

                qs.Execute();

                Lista.AddRange(from DataRow row in qs.Return.Rows
                               select DataRowToEntity(row, new SYS_UnidadeAdministrativa()));

                return Lista;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        /// <summary>
        /// Método alterado para que o delete não faça exclusão física e sim lógica (update).
        /// </summary>
        /// <param name="entity">Entity Unidade Administrativa</param>
        /// <returns>true = sucesso | false = fracasso</returns>
        public override bool Delete(SYS_UnidadeAdministrativa entity)
        {
            __STP_DELETE = "NEW_SYS_UnidadeAdministrativa_Update_Situacao";
            return base.Delete(entity);
        }

        /// <summary>
        /// Retorna as Unidades Administrativas SUPERIORES de acordo com os filtros informados.
        /// Os parâmetros ent_id, usu_id e gru_id são obrigatórios e filtra o resultado
        /// pela permissão do usuário na unidade administrativa e na unidade administrativa superior.
        /// </summary>
        /// <param name="tua_id"></param>
        /// <param name="ent_id"></param>
        /// <param name="gru_id"></param>
        /// <param name="usu_id"></param>
        /// <returns></returns>
        public DataTable GetSelectBy_UadSuperior_PermissaoUsuario
        (
            Guid tua_id
            , Guid ent_id
            , Guid gru_id
            , Guid usu_id
            , Guid uad_idSuperior
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_UnidadeAdministrativa_SelectBy_UadSuperior_PermissaoUsuario", _Banco);

            try
            {
                DataTable dt = new DataTable();

                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@tua_id";
                Param.Size = 16;
                if (tua_id != Guid.Empty)
                    Param.Value = tua_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@ent_id";
                Param.Size = 16;
                Param.Value = ent_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@gru_id";
                Param.Size = 16;
                Param.Value = gru_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@usu_id";
                Param.Size = 16;
                Param.Value = usu_id;
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

                #endregion PARAMETROS

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

        public List<SYS_UnidadeAdministrativa> SelecionarPorIdGrupo(Guid idEntidade, Guid idGrupo)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("MS_UnidadeAdministrativaSelecionarPorIdGrupo", _Banco);
            List<SYS_UnidadeAdministrativa> Lista = new List<SYS_UnidadeAdministrativa>();

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
                               select DataRowToEntity(row, new SYS_UnidadeAdministrativa()));

                return Lista;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        public IList<SYS_UnidadeAdministrativa> SelecionarUnidadesAdministrativasFilhas(Guid idEntidade, Guid idUnidade)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("MS_SelecionarUnidadesAdministrativasFilhas", _Banco);
            List<SYS_UnidadeAdministrativa> Lista = new List<SYS_UnidadeAdministrativa>();

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
                Param.ParameterName = "@uad_idSuperior";
                Param.Size = 16;
                if (idUnidade != Guid.Empty)
                    Param.Value = idUnidade;
                else
                    Param.Value = DBNull.Value;

                qs.Parameters.Add(Param);

                qs.Execute();

                Lista.AddRange(from DataRow row in qs.Return.Rows
                               select DataRowToEntity(row, new SYS_UnidadeAdministrativa()));

                return Lista;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        public DataTable SelecionarUnidadesAdministrativasFilhasV2(Guid entidadeId, Guid unidadeId)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("MS_SelecionarUnidadesAdministrativasFilhas", _Banco);
            List<SYS_UnidadeAdministrativa> Lista = new List<SYS_UnidadeAdministrativa>();
            DataTable dt = new DataTable();

            try
            {
                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@ent_id";
                Param.Size = 16;
                if (entidadeId != Guid.Empty)
                    Param.Value = entidadeId;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@uad_idSuperior";
                Param.Size = 16;
                if (unidadeId != Guid.Empty)
                    Param.Value = unidadeId;
                else
                    Param.Value = DBNull.Value;

                qs.Parameters.Add(Param);

                qs.Execute();

                if (qs.Return.Rows.Count > 0)
                    dt = qs.Return;

                return dt;

                //Lista.AddRange(from DataRow row in qs.Return.Rows
                //               select DataRowToEntity(row, new SYS_UnidadeAdministrativa()));

                //return Lista;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        /// <summary>
        /// Seleciona campo integridade da unidade administrativa
        /// </summary>
        /// <param name="ent_id">Id da tabela SYS_Entidade do bd</param>
        /// <param name="uad_id">Id da tabela SYS_UnidadeAdministrativa do bd</param>
        /// <returns>true = sucesso | false = fracasso</returns>
        public int Select_Integridade
        (
            Guid ent_id
            , Guid uad_id
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_UnidadeAdministrativa_Select_Integridade", _Banco);
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
                Param.ParameterName = "@uad_id";
                Param.Size = 16;
                if (uad_id != Guid.Empty)
                    Param.Value = uad_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

                qs.Execute();

                if (qs.Return.Rows.Count > 0)
                    return Convert.ToInt32(qs.Return.Rows[0]["uad_integridade"].ToString());

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
        /// Retorna a unidade administrativa apartir da entidade e código
        /// </summary>
        /// <param name="entity">Entidade SYS_UnidadeAdministrativa(contendo ent_id e uad_codigo)</param>
        /// <returns></returns>
        public bool SelectBy_Codigo(SYS_UnidadeAdministrativa entity)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_UnidadeAdministrativa_SelectBy_Codigo", _Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@ent_id";
                Param.Size = 16;
                Param.Value = entity.ent_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@uad_codigo";
                Param.Size = 20;
                Param.Value = entity.uad_codigo;
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

                qs.Execute();

                if (qs.Return.Rows.Count == 1)
                {
                    entity = DataRowToEntity(qs.Return.Rows[0], entity, false);
                    return true;
                }
                return false;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        public XPathDocument SelectBy_ent_id_HierarquiaXML(Guid ent_id)
        {
            QuerySelectXMLStoredProcedure qs = new QuerySelectXMLStoredProcedure("NEW_SYS_UnidadeAdministrativa_SelectBy_ent_id_HierarquiaXML", _Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@ent_id";
                Param.Size = 16;
                Param.Value = ent_id;
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

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
        /// Retorna a unidade administrativa apartir da entidade e nome
        /// </summary>
        /// <param name="entity">Entidade SYS_UnidadeAdministrativa(contendo ent_id e uad_nome)</param>
        /// <returns></returns>
        public bool SelectBy_Nome(SYS_UnidadeAdministrativa entity)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_UnidadeAdministrativa_SelectBy_uad_Nome", _Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@ent_id";
                Param.Size = 16;
                Param.Value = entity.ent_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@uad_nome";
                Param.Value = entity.uad_nome;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@uad_id";
                Param.Size = 16;
                if (entity.uad_id != Guid.Empty)
                    Param.Value = entity.uad_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Byte;
                Param.ParameterName = "@uad_situacao";
                Param.Size = 1;
                if (entity.uad_situacao > 0)
                    Param.Value = entity.uad_situacao;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

                qs.Execute();

                if (qs.Return.Rows.Count == 1)
                {
                    entity = DataRowToEntity(qs.Return.Rows[0], entity, false);
                    return true;
                }
                return false;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        /// <summary>
        /// Retorna um datatable paginado referente a busca de unidade administrativa.
        /// Caso seja preenchido os parametros gru_id e usu_id retornará todas as
        /// unidade administrativas na qual o usuário tem acesso.
        /// </summary>
        /// <param name="usu_id">ID do usuário logado</param>
        /// <param name="gru_id">ID do grupo que o usuário logou no sistema</param>
        /// <param name="tua_id">ID do tipo de unidade administrativa</param>
        /// <param name="ent_id">ID da entidade</param>
        /// <param name="uad_id"></param>
        /// <param name="uad_nome">Nome a unidade administrativa ou parte dele</param>
        /// <param name="uad_codigo">Código da unidade administrativa ou parte dele</param>
        /// <param name="paginado"></param>
        /// <param name="currentPage">Página atual do grid</param>
        /// <param name="pageSize">Total de registros por página do grid</param>
        /// <param name="totalRecords">Total de registros retornado na busca</param>
        /// <param name="uad_situacao"></param>
        /// <returns>DataTable com as Unidades Administrativas</returns>
        public DataTable SelectBy_Pesquisa
        (
            Guid gru_id
            , Guid usu_id
            , Guid tua_id
            , Guid ent_id
            , Guid uad_id
            , string uad_nome
            , string uad_codigo
            , byte uad_situacao
            , bool paginado
            , int currentPage
            , int pageSize
            //, string uad_codigoInep
            , out int totalRecords
        )
        {
            //DataTable dt = new DataTable();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_UnidadeAdministrativa_SelectBy_Pesquisa", _Banco);
            try
            {
                #region PARAMETROS

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
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@tua_id";
                Param.Size = 16;
                if (tua_id != Guid.Empty)
                    Param.Value = tua_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

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
                Param.ParameterName = "@uad_id";
                Param.Size = 16;
                if (uad_id != Guid.Empty)
                    Param.Value = uad_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@uad_nome";
                Param.Size = 200;
                if (!String.IsNullOrEmpty(uad_nome))
                    Param.Value = uad_nome;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@uad_codigo";
                Param.Size = 20;
                if (!String.IsNullOrEmpty(uad_codigo))
                    Param.Value = uad_codigo;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Byte;
                Param.ParameterName = "@uad_situacao";
                Param.Size = 1;
                if (uad_situacao > 0)
                    Param.Value = uad_situacao;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

                if (paginado)
                    totalRecords = qs.Execute(currentPage / pageSize, pageSize);
                else
                {
                    qs.Execute();
                    totalRecords = qs.Return.Rows.Count;
                }

                //if (qs.Return.Rows.Count > 0)
                //    dt = qs.Return;

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
        /// Retorna as unidades administrativas, não considerando as permissões do usuário.
        /// </summary>
        /// <param name="tua_id"></param>
        /// <param name="ent_id"></param>
        /// <returns></returns>
        public DataTable SelectBy_Pesquisa_PermissaoTotal
        (
            Guid tua_id
            , Guid ent_id
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_UnidadeAdministrativa_SelectBy_Pesquisa_PermissaoTotal", _Banco);

            try
            {
                DataTable dt = new DataTable();

                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@tua_id";
                Param.Size = 16;
                if (tua_id != Guid.Empty)
                    Param.Value = tua_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@ent_id";
                Param.Size = 16;
                Param.Value = ent_id;
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

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

        /// <summary>
        /// Método que retorna datatable de unidade administrativa
        /// filtrando pela permissão do usuário.
        /// </summary>
        /// <param name="gru_id">Id do grupo do usuário.</param>
        /// <param name="usu_id">Id do usuário.</param>
        /// <param name="tua_id">Id do tipo de uni. admin.</param>
        /// <param name="ent_id">Id da entidade.</param>
        /// <param name="uad_id">Id da unidade admin.</param>
        /// <param name="uad_nome">Nome da unidade admin.</param>
        /// <param name="uad_codigo">Código da unidade. admin.</param>
        public DataTable SelectBy_Pesquisa_PermissaoUsuario
        (
            Guid gru_id
            , Guid usu_id
            , Guid tua_id
            , Guid ent_id
            , Guid uad_id
            , string uad_nome
            , string uad_codigo
            , byte uad_situacao
            , bool paginado
            , int currentPage
            , int pageSize
            , out int totalRecords
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_UnidadeAdministrativa_SelectBy_Pesquisa_PermissaoUsuario", _Banco);
            try
            {
                #region PARAMETROS

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
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@tua_id";
                Param.Size = 16;
                if (tua_id != Guid.Empty)
                    Param.Value = tua_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

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
                Param.ParameterName = "@uad_id";
                Param.Size = 16;
                if (uad_id != Guid.Empty)
                    Param.Value = uad_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@uad_nome";
                Param.Size = 200;
                if (!String.IsNullOrEmpty(uad_nome))
                    Param.Value = uad_nome;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@uad_codigo";
                Param.Size = 20;
                if (!String.IsNullOrEmpty(uad_codigo))
                    Param.Value = uad_codigo;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Byte;
                Param.ParameterName = "@uad_situacao";
                Param.Size = 1;
                if (uad_situacao > 0)
                    Param.Value = uad_situacao;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

                if (paginado)
                    totalRecords = qs.Execute(currentPage / pageSize, pageSize);
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

        /// <summary>
        /// Retorna as Unidades Administrativas de acordo com os filtros informados.
        /// Os parâmetros ent_id, usu_id e gru_id são obrigatórios e filtra o resultado
        /// pela permissão do usuário na entidade.
        /// </summary>
        /// <param name="tua_id"></param>
        /// <param name="ent_id"></param>
        /// <param name="gru_id"></param>
        /// <param name="usu_id"></param>
        /// <param name="uad_situacao"></param>
        /// <param name="uad_id"></param>
        /// <param name="paginado"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable SelectBy_Pesquisa_PermissaoUsuario
        (
            Guid tua_id
            , Guid ent_id
            , Guid gru_id
            , Guid usu_id
            , Int16 uad_situacao
            , Guid uad_id
            , bool paginado
            , int currentPage
            , int pageSize
            , out int totalRecords
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_UnidadeAdministrativa_SelectBy_Pesquisa_Permissao", _Banco);

            try
            {
                DataTable dt = new DataTable();

                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@tua_id";
                Param.Size = 16;
                if (tua_id != Guid.Empty)
                    Param.Value = tua_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@ent_id";
                Param.Size = 16;
                Param.Value = ent_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@gru_id";
                Param.Size = 16;
                Param.Value = gru_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@usu_id";
                Param.Size = 16;
                Param.Value = usu_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Byte;
                Param.ParameterName = "@uad_situacao";
                Param.Size = 1;
                if (uad_situacao > 0)
                    Param.Value = uad_situacao;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@uad_id";
                Param.Size = 16;
                if (ent_id == Guid.Empty)
                    Param.Value = DBNull.Value;
                else
                    Param.Value = uad_id;
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

                if (paginado)
                    totalRecords = qs.Execute(currentPage / pageSize, pageSize);
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

        /// <summary>
        /// Retorna as Unidades Administrativas de acordo com os filtros informados.
        /// Os parâmetros ent_id, usu_id e gru_id são obrigatórios e filtra o resultado
        /// pela permissão do usuário na unidade administrativa e na unidade administrativa superior.
        /// </summary>
        /// <param name="tua_id"></param>
        /// <param name="ent_id"></param>
        /// <param name="gru_id"></param>
        /// <param name="usu_id"></param>
        /// <returns></returns>
        public DataTable SelectBy_Pesquisa_PermissaoUsuarioUASuperior
        (
            Guid tua_id
            , Guid ent_id
            , Guid gru_id
            , Guid usu_id
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_UnidadeAdministrativa_SelectBy_Pesquisa_PermissaoUASuperior", _Banco);

            try
            {
                DataTable dt = new DataTable();

                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@tua_id";
                Param.Size = 16;
                if (tua_id != Guid.Empty)
                    Param.Value = tua_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@ent_id";
                Param.Size = 16;
                Param.Value = ent_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@gru_id";
                Param.Size = 16;
                Param.Value = gru_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@usu_id";
                Param.Size = 16;
                Param.Value = usu_id;
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

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

        /// <summary>
        /// Método que retorna datatable de UA filtrando por Grupo e Usuario
        /// </summary>
        public DataTable SelectBy_Pesquisa_UsuarioGrupoUA
        (
            Guid gru_id
            , Guid usu_id
            , Guid tua_id
            , Guid ent_id
            , Guid uad_id
            , string uad_nome
            , string uad_codigo
            , byte uad_situacao
            , bool paginado
            , int currentPage
            , int pageSize
            , out int totalRecords
        )
        {
            //DataTable dt = new DataTable();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_UnidadeAdministrativa_SelectBy_Pesquisa_UsuarioGrupoUA", _Banco);
            try
            {
                #region PARAMETROS

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
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@tua_id";
                Param.Size = 16;
                if (tua_id != Guid.Empty)
                    Param.Value = tua_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

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
                Param.ParameterName = "@uad_id";
                Param.Size = 16;
                if (uad_id != Guid.Empty)
                    Param.Value = uad_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@uad_nome";
                Param.Size = 200;
                if (!String.IsNullOrEmpty(uad_nome))
                    Param.Value = uad_nome;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@uad_codigo";
                Param.Size = 20;
                if (!String.IsNullOrEmpty(uad_codigo))
                    Param.Value = uad_codigo;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Byte;
                Param.ParameterName = "@uad_situacao";
                Param.Size = 1;
                if (uad_situacao > 0)
                    Param.Value = uad_situacao;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

                if (paginado)
                    totalRecords = qs.Execute(currentPage / pageSize, pageSize);
                else
                {
                    qs.Execute();
                    totalRecords = qs.Return.Rows.Count;
                }

                //if (qs.Return.Rows.Count > 0)
                //    dt = qs.Return;

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
        /// Retorna as Unidades Administrativas de acordo com os filtros informados.
        /// </summary>
        public DataTable SelectBy_PesquisaTodos
        (
            Guid tua_id
            , Guid ent_id
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_UnidadeAdministrativa_SelectBy_Pesquisa_Todos", _Banco);

            try
            {
                DataTable dt = new DataTable();

                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@tua_id";
                Param.Size = 16;
                if (tua_id != Guid.Empty)
                    Param.Value = tua_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@ent_id";
                Param.Size = 16;
                Param.Value = ent_id;
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

                qs.Execute();

                if (qs.Return.Rows.Count > 0)
                    dt = qs.Return;

                return dt;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        /// <summary>
        /// Retorna as Unidades Administrativas que pertencem ao tipo de UA passado por parâmetro.
        /// Filtra por Entidade também.
        /// </summary>
        /// <param name="tua_id"></param>
        /// <param name="ent_id"></param>
        /// <returns></returns>
        public DataTable SelectBy_TipoUA_Entidade(Guid tua_id, Guid ent_id)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_UnidadeAdministrativa_SelectBy_tua_id", _Banco);
            try
            {
                DataTable dt = new DataTable();

                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@tua_id";
                Param.Size = 16;
                if (tua_id != Guid.Empty)
                    Param.Value = tua_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@ent_id";
                Param.Size = 16;
                if (ent_id == Guid.Empty)
                    Param.Value = DBNull.Value;
                else
                    Param.Value = ent_id;
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

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

        /// <summary>
        /// Retorna as Unidades Administrativas que pertencem ao tipo de UA passado por parâmetro.
        /// </summary>
        /// <param name="tua_id"></param>
        /// <returns></returns>
        public DataTable SelectBy_tua_id(Guid tua_id)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_UnidadeAdministrativa_SelectBy_tua_id", _Banco);
            try
            {
                DataTable dt = new DataTable();

                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@tua_id";
                Param.Size = 16;
                if (tua_id != Guid.Empty)
                    Param.Value = tua_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                // Passa sempre vazio - não filtra por entidade.
                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@ent_id";
                Param.Size = 16;
                Param.Value = DBNull.Value;
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

        /// <summary>
        /// Retorna true/false
        /// para saber se nome da unidade administrativa já está cadastrada na entidade informada
        /// filtradas por entidade, unidade administrativa (diferente do parametro informado), nome, situacao.
        /// </summary>
        /// <param name="uad_id">Id da tabela SYS_UnidadeAdministrativa do bd</param>
        /// <param name="ent_id">Id da tabela SYS_Entidade do bd</param>
        /// <param name="uad_nome">Campo nome da tabela SYS_UnidadeAdministrativa do bd</param>
        /// <param name="uad_situacao">Campo uad_situcao da tabela SYS_UnidadeAdministrativa do bd</param>
        /// <returns>Retorna true = nome ja cadastrado | false para nome ainda não cadastrado</returns>
        public bool SelectBy_uad_Nome
        (
            Guid ent_id
            , Guid uad_id
            , string uad_nome
            , byte uad_situacao
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_UnidadeAdministrativa_SelectBy_uad_Nome", _Banco);
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
                Param.ParameterName = "@uad_id";
                Param.Size = 16;
                if (uad_id != Guid.Empty)
                    Param.Value = uad_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@uad_nome";
                Param.Size = 200;
                if (!string.IsNullOrEmpty(uad_nome))
                    Param.Value = uad_nome;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Byte;
                Param.ParameterName = "@uad_situacao";
                Param.Size = 1;
                if (uad_situacao > 0)
                    Param.Value = uad_situacao;
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
        /// Retorna um DataTable contendo todas as unidades administrativas
        /// que não foram excluídas logicamente,
        /// filtrando por: entidade e unidade adminsitrativa.
        /// </summary>
        /// <param name="ent_id"></param>
        /// <param name="uad_idSupeior"></param>
        /// <returns></returns>
        public DataTable SelectBy_UASuperior
           (
                Guid ent_id
               , Guid uad_idSupeior
           )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_UnidadeAdministrativa_SelectBy_UASuperior", _Banco);
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
                Param.ParameterName = "@uad_idSupeior";
                Param.Size = 16;
                if (uad_idSupeior != Guid.Empty)
                    Param.Value = uad_idSupeior;
                else
                    Param.Value = DBNull.Value;
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
        /// Decrementa uma unidade no campo integridade da unidade administrativa
        /// </summary>
        /// <param name="ent_id">Id da tabela SYS_Entidade do bd</param>
        /// <param name="uad_id">Id da tabela SYS_UnidadeAdministrativa do bd</param>
        /// <returns>true = sucesso | false = fracasso</returns>
        public bool Update_DecrementaIntegridade
        (
            Guid ent_id
            , Guid uad_id
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_UnidadeAdministrativa_DECREMENTA_INTEGRIDADE", _Banco);
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
                Param.ParameterName = "@uad_id";
                Param.Size = 16;
                if (uad_id != Guid.Empty)
                    Param.Value = uad_id;
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
        /// Incrementa uma unidade no campo integridade da unidade administrativa
        /// </summary>
        /// <param name="ent_id">Id da tabela SYS_Entidade do bd</param>
        /// <param name="uad_id">Id da tabela SYS_UnidadeAdministrativa do bd</param>
        /// <returns>true = sucesso | false = fracasso</returns>
        public bool Update_IncrementaIntegridade
        (
            Guid ent_id
            , Guid uad_id
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_UnidadeAdministrativa_INCREMENTA_INTEGRIDADE", _Banco);
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
                Param.ParameterName = "@uad_id";
                Param.Size = 16;
                if (uad_id != Guid.Empty)
                    Param.Value = uad_id;
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
        /// Método alterado para que o update não faça a alteração da data de criação e a integridade
        /// </summary>
        /// <param name="entity"> Entidade SYS_UnidadeAdministrativa</param>
        /// <returns>true = sucesso | false = fracasso</returns>
        protected override bool Alterar(SYS_UnidadeAdministrativa entity)
        {
            __STP_UPDATE = "NEW_SYS_UnidadeAdministrativa_UPDATE";
            return base.Alterar(entity);
        }

        /// <summary>
        /// Parâmetros para efetuar a alteração preservando a data de criação e a integridade
        /// </summary>
        protected override void ParamAlterar(QueryStoredProcedure qs, SYS_UnidadeAdministrativa entity)
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
            Param.ParameterName = "@tua_id";
            Param.Size = 16;
            Param.Value = entity.tua_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@uad_codigo";
            Param.Size = 20;
            if (!string.IsNullOrEmpty(entity.uad_codigo))
                Param.Value = entity.uad_codigo;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@uad_nome";
            Param.Size = 200;
            Param.Value = entity.uad_nome;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@uad_sigla";
            Param.Size = 50;
            if (!string.IsNullOrEmpty(entity.uad_sigla))
                Param.Value = entity.uad_sigla;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@uad_idSuperior";
            Param.Size = 16;
            if (entity.uad_idSuperior != Guid.Empty)
                Param.Value = entity.uad_idSuperior;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@uad_situacao";
            Param.Size = 1;
            Param.Value = entity.uad_situacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@uad_dataAlteracao";
            Param.Size = 8;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@uad_codigoIntegracao";
            Param.Size = 50;
            Param.Value = entity.uad_codigoIntegracao ?? "";
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@uad_codigoInep";
            Param.Size = 20;
            if (!string.IsNullOrEmpty(entity.uad_codigoInep))
                Param.Value = entity.uad_codigoInep;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);
        }

        /// <summary>
        /// Parâmetros para efetuar a exclusão lógica.
        /// </summary>
        protected override void ParamDeletar(QueryStoredProcedure qs, SYS_UnidadeAdministrativa entity)
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
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@uad_situacao";
            Param.Size = 1;
            Param.Value = 3;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@uad_dataAlteracao";
            Param.Size = 8;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);
        }

        /// <summary>
        /// Parâmetros para efetuar a inclusão com data de criação e de alteração fixas
        /// </summary>
        protected override void ParamInserir(QuerySelectStoredProcedure qs, SYS_UnidadeAdministrativa entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@ent_id";
            Param.Size = 16;
            Param.Value = entity.ent_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@tua_id";
            Param.Size = 16;
            Param.Value = entity.tua_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@uad_codigo";
            Param.Size = 20;
            if (!string.IsNullOrEmpty(entity.uad_codigo))
                Param.Value = entity.uad_codigo;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@uad_nome";
            Param.Size = 200;
            Param.Value = entity.uad_nome;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@uad_sigla";
            Param.Size = 50;
            if (!string.IsNullOrEmpty(entity.uad_sigla))
                Param.Value = entity.uad_sigla;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@uad_idSuperior";
            Param.Size = 16;
            if (entity.uad_idSuperior != Guid.Empty)
                Param.Value = entity.uad_idSuperior;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@uad_situacao";
            Param.Size = 1;
            Param.Value = entity.uad_situacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@uad_dataCriacao";
            Param.Size = 16;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@uad_dataAlteracao";
            Param.Size = 16;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@uad_integridade";
            Param.Size = 4;
            Param.Value = entity.uad_integridade;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@uad_codigoIntegracao";
            Param.Size = 50;
            if (!string.IsNullOrEmpty(entity.uad_codigoIntegracao))
                Param.Value = entity.uad_codigoIntegracao;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@uad_codigoInep";
            Param.Size = 20;
            if (!string.IsNullOrEmpty(entity.uad_codigoInep))
                Param.Value = entity.uad_codigoInep;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);
        }
    }
}
