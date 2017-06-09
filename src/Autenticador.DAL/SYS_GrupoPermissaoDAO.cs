using Autenticador.DAL.Abstracts;
using Autenticador.Entities;
using CoreLibrary.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;

namespace Autenticador.DAL
{
    public class SYS_GrupoPermissaoDAO : Abstract_SYS_GrupoPermissaoDAO
    {
        /// <summary>
        /// Seleciona as visões que possuem permissão módulo do sistema
        /// </summary>
        /// <param name="sis_id">ID do sistema</param>
        /// <param name="mod_id">ID do módulo</param>
        public DataTable GetSelect_Visoes(int sis_id, int mod_id)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_GrupoPermissao_Select_Visoes", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@sis_id";
                Param.Size = 8;
                Param.Value = sis_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@mod_id";
                Param.Size = 8;
                Param.Value = mod_id;
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

                qs.Execute();

                return qs.Return;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Copia permissões de um grupo.
        /// </summary>
        /// <param name="gru_id">Id do grupo novo</param>
        /// <param name="sis_id">Id do sistema</param>
        /// <param name="gru_idOld">Id do grupo que será copiado</param>
        public void CopiaPermissao
        (
            Guid gru_id
            , int sis_id
            , Guid gru_idOld
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_GrupoPermissao_CopiaPermissao", _Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@gru_id";
                Param.Size = 16;
                Param.Value = gru_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@sis_id";
                Param.Size = 8;
                Param.Value = sis_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@gru_idOld";
                Param.Size = 16;
                Param.Value = gru_idOld;
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

                qs.Execute();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Copia usuários de um grupo.
        /// </summary>
        /// <param name="gru_id">Id do grupo novo</param>
        /// <param name="gru_idOld">Id do grupo que será copiado</param>
        public void CopiaUsuario
        (
            Guid gru_id
            , Guid gru_idOld
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_GrupoPermissao_CopiaUsuarios", _Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@gru_id";
                Param.Size = 16;
                Param.Value = gru_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@gru_idOld";
                Param.Size = 16;
                Param.Value = gru_idOld;
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

                qs.Execute();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Insere permissão para determinada visão no grupo
        /// </summary>
        /// <param name="sis_id">ID do sistema</param>
        /// <param name="vis_id">ID da visão </param>
        /// <param name="mod_id">ID do módulo</param>
        public void InsertPermissao_Visoes(int sis_id, int vis_id, int mod_id)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_GrupoPermissao_Insert_Visoes", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@sis_id";
                Param.Size = 8;
                Param.Value = sis_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@vis_id";
                Param.Size = 8;
                Param.Value = vis_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@mod_id";
                Param.Size = 8;
                Param.Value = mod_id;
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

                qs.Execute();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Delete permissão para determinada visão no grupo
        /// </summary>
        /// <param name="sis_id">ID do sistema</param>
        /// <param name="vis_id">ID da visão </param>
        /// <param name="mod_id">ID do módulo</param>
        public void DeletePermissao_Visoes(int sis_id, int vis_id, int mod_id)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_GrupoPermissao_Delete_Visoes", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@sis_id";
                Param.Size = 8;
                Param.Value = sis_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@vis_id";
                Param.Size = 8;
                Param.Value = vis_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@mod_id";
                Param.Size = 8;
                Param.Value = mod_id;
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

                qs.Execute();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Carrega a entidade SYS_GrupoPermissao atraves do grupo
        /// e da url do módulo se houver. caso contrario retorna um
        /// objeto vázio.
        /// </summary>
        /// <param name="gru_id">Grupo do usuário</param>
        /// <param name="msm_url">Url do moódulo</param>
        /// <returns>Autenticador.Entities.SYS_GrupoPermissao</returns>
        public SYS_GrupoPermissao CarregarBy_url(Guid gru_id, string msm_url)
        {
            SYS_GrupoPermissao entity = new SYS_GrupoPermissao();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_GrupoPermissao_LoadBy_url", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@gru_id";
                Param.Size = 16;
                Param.Value = gru_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@msm_url";
                Param.Size = 500;
                Param.Value = msm_url;
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

                qs.Execute();
                if (qs.Return.Rows.Count > 0)
                    entity = this.DataRowToEntity(qs.Return.Rows[0], entity, false);
                return entity;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna um datatable com o registro da SYS_GrupoPermissao e SYS_Modulo, buscando a permissão
        /// do grupo informado no módulo que está ligado à url informada do sistema.
        /// </summary>
        /// <param name="gru_id">Grupo do usuário</param>
        /// <param name="msm_url">Url do moódulo</param>
        /// <param name="sis_id">ID do sistema</param>
        /// <returns>DataTable com as propriedades das entidades: SYS_GrupoPermissao, SYS_Modulo, e o campo msm_url</returns>
        public DataTable CarregarGrupos_Urls_PorSistema(Guid gru_id, string msm_url, int sis_id)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_GrupoPermissao_Load_PermissaoModulo_PorUrl", this._Banco);

            #region PARAMETROS

            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@sis_id";
            Param.Size = 4;
            Param.Value = sis_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@gru_id";
            Param.Size = 16;
            Param.Value = gru_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@msm_url";
            Param.Size = 500;
            Param.Value = msm_url;
            qs.Parameters.Add(Param);

            #endregion PARAMETROS

            qs.Execute();

            return qs.Return;
        }

        public DataTable SelectBy_mod_id(int mod_id, Guid gru_id)
        {
            DataTable dt = new DataTable();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_GrupoPermissao_SelectBy_mod_id", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@mod_id";
                Param.Size = 4;
                Param.Value = mod_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@gru_id";
                Param.Size = 16;
                Param.Value = gru_id;
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

        public bool _AlterarByXML(XmlNode xml)
        {
            QueryStoredProcedure qs = new QueryStoredProcedure("NEW_SYS_GrupoPermissao_UPDATEBY_XML", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Xml;
                Param.ParameterName = "@permissoesXml";
                XmlNode node = xml.SelectSingleNode("/ArrayOfPermissoes/Permissoes");
                if (node != null)
                    Param.Value = xml.OuterXml;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

                qs.Execute();

                if (qs.Return == 0)
                {
                    return false;
                }
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
        /// Recebe o valor do auto incremento e coloca na propriedade
        /// </summary>
        /// <param name="qs">Objeto da Store Procedure</param>
        protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, SYS_GrupoPermissao entity)
        {
            return true;
        }

        public DataTable SelecionarPermissoesGrupoPorIdGrupo(Guid grupoId)
        {
            List<SYS_GrupoPermissao> lt = new List<SYS_GrupoPermissao>();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("MS_SelecionarGruposPermissaoPorIdGrupo", this._Banco);
            try
            {
                #region Parâmetros

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@grupoId";
                Param.Size = 16;
                Param.Value = grupoId;
                qs.Parameters.Add(Param);

                #endregion Parâmetros

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
    }
}