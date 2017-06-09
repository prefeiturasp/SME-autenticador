using Autenticador.DAL.Abstracts;
using Autenticador.Entities;
using CoreLibrary.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.XPath;

namespace Autenticador.DAL
{
    public class SYS_ModuloDAO : Abstract_SYS_ModuloDAO
    {
        /// <summary>
        /// Retorna uma lista de objetos SYS_Modulo filtrado por grupo
        /// </summary>
        /// <param name="gru_id">ID do grupo</param>
        /// <returns>Lista de SYS_Modulo</returns>
        public IList<SYS_Modulo> SelectBy_gru_id(Guid gru_id)
        {
            IList<SYS_Modulo> lt = new List<SYS_Modulo>();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Modulo_SelectBy_gru_id", _Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@gru_id";
                Param.Size = 16;
                Param.Value = gru_id;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                foreach (DataRow dr in qs.Return.Rows)
                {
                    SYS_Modulo entity = new SYS_Modulo();
                    lt.Add(DataRowToEntity(dr, entity));
                }
                return lt;
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
        /// Retorna uma lista de objetos com módulos filhos do sistema de determinado módulo.
        /// </summary>
        /// <param name="sis_id">ID do sistema</param>
        /// <param name="sis_id">ID do módulo pai, sendo 0, traz os pais</param>
        /// <param name="mod_id"></param>
        /// <returns>DataTable de SYS_Modulo</returns>
        public DataTable SelectBy_mod_id_Filhos(int sis_id, int mod_id)
        {
            DataTable dt = new DataTable();
            QuerySelectStoredProcedure qu = new QuerySelectStoredProcedure("NEW_SYS_Modulo_SelectBy_mod_id_Filhos", _Banco);
            
            try
            {   
                #region PARAMETROS

                Param = qu.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@sis_id";
                Param.Size = 8;
                if (sis_id > 0)
                    Param.Value = sis_id;
                else
                    Param.Value = DBNull.Value;
                qu.Parameters.Add(Param);

                Param = qu.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@mod_id";
                Param.Size = 8;
                if (mod_id > 0)
                    Param.Value = mod_id;
                else
                    Param.Value = DBNull.Value;
                qu.Parameters.Add(Param);

                #endregion

                qu.Execute();
                if (qu.Return.Rows.Count > 0)
                    dt = qu.Return;

                return dt;
            }
            catch
            {
                throw;
            }
            finally
            {
                qu.Parameters.Clear();
            }
        }

        public DataTable SelectBy_mod_id_Filhos(int mod_id)
        {
            DataTable dt = new DataTable();
            QuerySelectStoredProcedure qu = new QuerySelectStoredProcedure("NEW_SYS_Modulo_SelectBy_mod_idFilhos", _Banco);

            try
            {
                #region PARAMETROS

                Param = qu.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@mod_id";
                Param.Size = 8;
                if (mod_id > 0)
                    Param.Value = mod_id;
                else
                    Param.Value = DBNull.Value;
                qu.Parameters.Add(Param);

                #endregion

                qu.Execute();
                if (qu.Return.Rows.Count > 0)
                    dt = qu.Return;

                return dt;
            }
            catch
            {
                throw;
            }
            finally
            {
                qu.Parameters.Clear();
            }
        }

        public XPathDocument SelectBy_MenuXML(Guid gru_id, int sis_id, int vis_id)
        {
            QuerySelectXMLStoredProcedure qs = new QuerySelectXMLStoredProcedure("NEW_SYS_Modulo_SelectBy_MenuXML", _Banco);
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
                Param.Size = 4;
                Param.Value = sis_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@vis_id";
                Param.Size = 4;
                Param.Value = vis_id;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                return qs.Return;
            }
            catch
            {
                throw;
            }
        }

        
        /// <summary>
        /// Carrega o mapa do site à partir do id de um módulo.
        /// </summary>
        /// <param name="gru_id">Grupo do usuário logado</param>
        /// <param name="sis_id">Sistema do usuário logado</param>
        /// <param name="vis_id">Visão do usuário logado</param>
        /// <param name="mod_id">ID do módulo que deseja iniciar o mapa do site</param>
        /// <returns>XML contendo todos os módulos do sistema à partir do módulo selecionado.</returns>
        public XPathDocument SelectBy_SiteMapXML(Guid gru_id, int sis_id, int vis_id, int mod_id)
        {
            QuerySelectXMLStoredProcedure qs = new QuerySelectXMLStoredProcedure("NEW_SYS_Modulo_SelectBy_SiteMapXML", _Banco);
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
                Param.Size = 4;
                Param.Value = sis_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@vis_id";
                Param.Size = 4;
                Param.Value = vis_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@mod_id";
                Param.Size = 4;
                if (mod_id > 0)
                    Param.Value = mod_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                return qs.Return;
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// Carrega o mapa do site à partir do sistema ou de um módulo.
        /// </summary>
        /// <param name="gru_id">Grupo do usuário logado</param>
        /// <param name="sis_id">Sistema do usuário logado</param>
        /// <param name="vis_id">Visão do usuário logado</param>
        /// <param name="mod_id">ID do módulo que deseja iniciar o mapa do site</param>
        /// <returns>XML contendo todos os módulos do sistema à partir do módulo selecionado.</returns>
        public XPathDocument SelectBy_SiteMapXML2(Guid gru_id, int sis_id, int vis_id, int mod_id)
        {
            QuerySelectXMLStoredProcedure qs = new QuerySelectXMLStoredProcedure("NEW_SYS_Modulo_SelectBy_SiteMapXML2", _Banco);
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
                Param.Size = 4;
                Param.Value = sis_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@vis_id";
                Param.Size = 4;
                Param.Value = vis_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@mod_id";
                Param.Size = 4;
                //Se for zero carrega todos os menus incluido o sistema
                if (mod_id > -1)
                    Param.Value = mod_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                return qs.Return;
            }
            catch
            {
                throw;
            }
        }



        public DataTable GetSelect_by_Sis_id
        (
              int sis_id
            , bool paginado
            , int currentPage
            , int pageSize
            , out int totalRecords
        )
        {
            DataTable dt = new DataTable();
            QuerySelectStoredProcedure qu = new QuerySelectStoredProcedure("NEW_SYS_Modulo_SelectBy_Sis_id", _Banco);
            try
            {
                #region PARAMETROS


                Param = qu.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@sis_id";
                Param.Size = 16;
                if (sis_id > 0)
                    Param.Value = sis_id;
                else
                    Param.Value = DBNull.Value;
                qu.Parameters.Add(Param);


                #endregion

                if (paginado)
                    totalRecords = qu.Execute(currentPage, pageSize);
                else
                {
                    qu.Execute();
                    totalRecords = qu.Return.Rows.Count;
                }

                if (qu.Return.Rows.Count > 0)
                    dt = qu.Return;

                return dt;
            }
            catch
            {
                throw;
            }
            finally
            {
                qu.Parameters.Clear();
            }
        }

        /// <summary>
        /// Atualiza o campo de auditoria nos modulos
        /// </summary>
        /// <param name="modulo">Entity do modulo que será alterado</param>
        /// <returns>True: ocorreu alguma alteração / False: não ocorreu nenhuma alteração </returns>
        public bool UpdateAuditoria
        (
            SYS_Modulo modulo
            
        )
        {
            int totalRecords;

            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Modulo_UPDATE_Auditoria", _Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@sis_id";
                Param.Size = 4;
                if (modulo.sis_id > 0)
                    Param.Value = modulo.sis_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@mod_id";
                Param.Size = 4;
                if (modulo.mod_id > 0)
                    Param.Value = modulo.mod_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Boolean;
                Param.ParameterName = "@mod_auditoria";
                Param.Size = 1;
                Param.Value = modulo.mod_auditoria;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.DateTime;
                Param.ParameterName = "@mod_dataAlteracao";
                Param.Size = 8;
                Param.Value = DateTime.Now;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();
                totalRecords = qs.Return.Rows.Count;

                if (totalRecords > 0)
                    return true;
                else
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
        /// Parâmetros para efetuar a inclusão com data de criação e de alteração fixas
        /// </summary>
        protected override void ParamInserir(QuerySelectStoredProcedure qs, SYS_Modulo entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@sis_id";
            Param.Size = 4;
            Param.Value = entity.sis_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@mod_id";
            Param.Size = 4;
            Param.Value = entity.mod_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@mod_nome";
            Param.Size = 50;
            Param.Value = entity.mod_nome;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.String;
            Param.ParameterName = "@mod_descricao";
            if (!string.IsNullOrEmpty(entity.mod_descricao))
                Param.Value = entity.mod_descricao;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@mod_idPai";
            Param.Size = 4;
            if (entity.mod_idPai > 0)
                Param.Value = entity.mod_idPai;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Boolean;
            Param.ParameterName = "@mod_auditoria";
            Param.Size = 1;
            Param.Value = entity.mod_auditoria;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@mod_situacao";
            Param.Size = 1;
            Param.Value = entity.mod_situacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@mod_dataCriacao";
            Param.Size = 16;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@mod_dataAlteracao";
            Param.Size = 16;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);
        }

        /// <summary>
        /// Parâmetros para efetuar a alteração preservando a data de criação
        /// </summary>      
        protected override void ParamAlterar(QueryStoredProcedure qs, SYS_Modulo entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@sis_id";
            Param.Size = 4;
            Param.Value = entity.sis_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@mod_id";
            Param.Size = 4;
            Param.Value = entity.mod_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@mod_nome";
            Param.Size = 50;
            Param.Value = entity.mod_nome;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.String;
            Param.ParameterName = "@mod_descricao";
            if (!string.IsNullOrEmpty(entity.mod_descricao))
                Param.Value = entity.mod_descricao;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@mod_idPai";
            Param.Size = 4;
            if (entity.mod_idPai > 0)
                Param.Value = entity.mod_idPai;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Boolean;
            Param.ParameterName = "@mod_auditoria";
            Param.Size = 1;
            Param.Value = entity.mod_auditoria;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@mod_situacao";
            Param.Size = 1;
            Param.Value = entity.mod_situacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@mod_dataAlteracao";
            Param.Size = 16;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);
        }

        /// <summary>
        /// Método alterado para que o update não faça a alteração da data de criação e da integridade
        /// </summary>
        /// <param name="entity"> Entidade SYS_Modulo</param>
        /// <returns>true = sucesso | false = fracasso</returns>  
        protected override bool Alterar(SYS_Modulo entity)
        {
            __STP_UPDATE = "NEW_SYS_Modulo_UPDATE";
            return base.Alterar(entity);
        }

        /// <summary>
        /// Método alterado para que o delete não faça exclusão física e sim lógica (update).
        /// </summary>
        /// <param name="entity"> Entidade SYS_Modulo</param>
        /// <returns>true = sucesso | false = fracasso</returns>    
        public override bool Delete(SYS_Modulo entity)
        {
            __STP_DELETE = "NEW_SYS_Modulo_Update_mod_situacao";
            return base.Delete(entity);
        }

        /// <summary>
        /// Seleciona os módulos (e módulos filhos) de um determinado grupo do usuário
        /// </summary>
        /// <param name="sistemaId">Id do sistema</param>
        /// <param name="grupoId">Id do grupo</param>
        /// <returns>DataTable com os módulos e submódulos</returns>
        public DataTable SelecionarModulosPorIdGrupoUsuario(int sistemaId, Guid grupoId)
        {
            DataTable dt = new DataTable();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("MS_Modulos_SelecionarModulosPorIdGrupo", _Banco);

            try
            {
                #region Parâmetros

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@sistemaId";
                Param.Size = 8;
                if (sistemaId > 0)
                    Param.Value = sistemaId;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@grupoId";
                Param.Size = 16;
                Param.Value = grupoId;
                qs.Parameters.Add(Param);

                #endregion Parâmetros

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

        public DataTable SelecionaModulos_By_sis_id_vis_id_mod_id(Int32 sis_id, Int32 vis_id, Int32 mod_id)
        {
            DataTable dt = new DataTable();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Select_Modulos_By_Sistema_Visao_Modulo", _Banco);

            try
            {
                #region Parâmetros

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@sis_id";
                Param.Size = 8;
                if (sis_id > 0)
                    Param.Value = sis_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@vis_id";
                Param.Size = 4;
                Param.Value = vis_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@mod_id";
                Param.Size = 4;
                Param.Value = mod_id;
                qs.Parameters.Add(Param);

                #endregion Parâmetros

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
    }
}
