using Autenticador.DAL.Abstracts;
using Autenticador.Entities;
using CoreLibrary.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace Autenticador.DAL
{
    public class SYS_GrupoDAO : Abstract_SYS_GrupoDAO
    {
        /// <summary>
        /// Método alterado para que o delete não faça exclusão física e sim lógica.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override bool Delete(Autenticador.Entities.SYS_Grupo entity)
        {
            this.__STP_DELETE = "NEW_SYS_Grupo_UPDATEBy_Situacao";
            return base.Delete(entity);
        }

        public long Select_Integridade
                (
                    Guid gru_id
                )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Grupo_Select_Integridade", this._Banco);
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

                #endregion PARAMETROS

                qs.Execute();

                if (qs.Return.Rows.Count > 0)
                    return Convert.ToInt64(qs.Return.Rows[0]["gru_integridade"].ToString());

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

        public List<SYS_Grupo> SelectBy_All_In_Usuario()
        {
            List<SYS_Grupo> lt = new List<SYS_Grupo>();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Grupo_SELECTBy_ALL_In_Usuario", this._Banco);
            try
            {
                qs.Execute();

                foreach (DataRow dr in qs.Return.Rows)
                {
                    SYS_Grupo entity = new SYS_Grupo();
                    lt.Add(this.DataRowToEntity(dr, entity));
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
        /// Retorna true/false Utilizado para saber se o grupo já está cadastrado no sistema
        /// filtradas por grupo (diferente do parametro informado), sistema, nome e situacao.
        /// </summary>
        /// <param name="gru_id">Id da tabela SYS_Grupo do bd</param>
        /// <param name="isi_id">Id da tabela SYS_Sistema do bd</param>
        /// <param name="gru_nome">Campo gru_nome da tabela SYS_Grupo do bd</param>
        /// <param name="gru_situacao">Campo gru_situcao da tabela SYS_Grupo do bd</param>
        /// <returns>Retorna true = grupo já cadastrado | false para grupo ainda não cadastrado</returns>
        public bool SelectBy_gru_nome
        (
            Guid gru_id
            , int sis_id
            , string gru_nome
            , byte gru_situacao
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Grupo_SelectBy_gru_nome", this._Banco);
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
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@sis_id";
                Param.Size = 4;
                if (sis_id > 0)
                    Param.Value = sis_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@gru_nome";
                Param.Size = 50;
                if (!string.IsNullOrEmpty(gru_nome))
                    Param.Value = gru_nome;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Byte;
                Param.ParameterName = "@gru_situacao";
                Param.Size = 1;
                if (gru_situacao > 0)
                    Param.Value = gru_situacao;
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

        public DataTable SelectBy_MeusDados(Guid usu_id, bool paginado, int currentPage, int pageSize, out int totalRecords)
        {
            DataTable dt = new DataTable();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Grupo_Select_gru_sis_id", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@usu_id";
                Param.Size = 4;
                if (!usu_id.Equals(Guid.Empty))
                    Param.Value = usu_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

                if (paginado)
                    totalRecords = qs.Execute(currentPage, pageSize);
                else
                {
                    qs.Execute();
                    totalRecords = qs.Return.Rows.Count;
                }

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
        /// Carrega os grupos do usuário para um determinado sistema ordenado pelo nome do grupo.
        /// </summary>
        /// <param name="usu_id">ID do usuário logado</param>
        /// <param name="sis_id">ID do sistema na qual o usuário pretende entrar</param>
        /// <returns>Lista de grupos</returns>
        public IList<SYS_Grupo> SelectBy_Sis_id_And_Usu_id(Guid usu_id, int sis_id)
        {
            List<SYS_Grupo> lt = new List<SYS_Grupo>();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Grupo_SelectBy_sis_id_usu_id", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@sis_id";
                Param.Size = 4;
                Param.Value = sis_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@usu_id";
                Param.Size = 16;
                Param.Value = usu_id;
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

                qs.Execute();

                foreach (DataRow dr in qs.Return.Rows)
                {
                    SYS_Grupo entity = new SYS_Grupo();
                    lt.Add(this.DataRowToEntity(dr, entity));
                }
                return lt;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna um datatable contendo todos os grupos que não foram excluído lógicamente
        /// filtrados por sistema e paginado. Se sis_id = -1 retorna todos os grupos independete de sistema.
        /// </summary>
        /// <param name="sis_id">Id da tabela SYS_Sistema do bd</param>
        /// <returns>DataTable com grupo de usuário paginado</returns>
        public DataTable SelectBy_sis_id(int sis_id, bool paginado, int currentPage, int pageSize, out int totalRecords)
        {
            //DataTable dt = new DataTable();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Grupo_SelectBy_sis_id", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@sis_id";
                Param.Size = 4;
                if (sis_id > 0)
                    Param.Value = sis_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

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

       

        public DataTable SelectBy_sis_id_vis_id(int sis_id, int vis_id, bool paginado, int currentPage, int pageSize, out int totalRecords)
        {
            DataTable dt = new DataTable();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Grupo_SelectBy_sis_id_vis_id", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@sis_id";
                Param.Size = 4;
                if (sis_id > 0)
                    Param.Value = sis_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@vis_id";
                Param.Size = 4;
                if (vis_id > 0)
                    Param.Value = vis_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

                if (paginado)
                    totalRecords = qs.Execute(currentPage, pageSize);
                else
                {
                    qs.Execute();
                    totalRecords = qs.Return.Rows.Count;
                }

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
        /// Retorna DataTable com os grupos de usuário ativos filtrados por sistema.
        /// </summary>
        /// <param name="sis_id">Id do sistema</param>
        /// <returns>DataTable com os grupos de usuário</returns>
        public DataTable SelectBy_Sistema(int sis_id)
        {
            DataTable dt = new DataTable();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Grupo_SelectBy_Sistema", _Banco);
            try
            {
                #region Parâmetros

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@sis_id";
                Param.Size = 4;
                Param.Value = sis_id;
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

        /// <summary>
        /// Retorna uma lista de objetos não paginados contendo os grupos filtrado por usuário. A
        /// lista é ordenada pelo nome do grupo.
        /// </summary>
        /// <param name="usu_id">ID do usuário</param>
        /// <returns>Lista de grupos do usuário</returns>
        public DataTable SelectBy_usu_id(Guid usu_id)
        {
            DataTable dt = new DataTable();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Grupo_SELECTBy_usu_id", this._Banco);
            try
            {
                #region PARAMETROS

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
        /// Decrementa uma unidade no campo integridade do grupo
        /// </summary>
        /// <param name="ent_id">Id da tabela sys_grupo do bd</param>
        /// <returns>true = sucesso | false = fracasso</returns>
        public bool Update_DecrementaIntegridade
        (
            Guid gru_id
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Grupo_DECREMENTA_INTEGRIDADE", this._Banco);
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
        /// Incrementa uma unidade no campo integridade do grupo
        /// </summary>
        /// <param name="ent_id">Id da tabela sys_grupo do bd</param>
        /// <returns>true = sucesso | false = fracasso</returns>
        public bool Update_IncrementaIntegridade
        (
            Guid gru_id
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Grupo_INCREMENTA_INTEGRIDADE", this._Banco);
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
        /// Méotodo de update alterado para que não modificasse o valor do campo data da criação;
        /// </summary>
        /// <param name="entity">Entidade com dados preenchidos</param>
        /// <returns>True para alteração realizado com sucesso.</returns>
        protected override bool Alterar(Autenticador.Entities.SYS_Grupo entity)
        {
            this.__STP_UPDATE = "NEW_SYS_Grupo_UPDATE";
            return base.Alterar(entity);
        }

        /// <summary>
        /// Parâmetros para efetuar a alteração preservando a data de criação e a integridade
        /// </summary>
        protected override void ParamAlterar(QueryStoredProcedure qs, Autenticador.Entities.SYS_Grupo entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@gru_id";
            Param.Size = 16;
            Param.Value = entity.gru_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@gru_nome";
            Param.Size = 50;
            Param.Value = entity.gru_nome;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.Size = 1;
            Param.ParameterName = "@gru_situacao";
            Param.Value = entity.gru_situacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@gru_dataAlteracao";
            Param.Size = 8;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@vis_id";
            Param.Size = 4;
            Param.Value = entity.vis_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@sis_id";
            Param.Size = 4;
            Param.Value = entity.sis_id;
            qs.Parameters.Add(Param);
        }

        protected override void ParamDeletar(QueryStoredProcedure qs, Autenticador.Entities.SYS_Grupo entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@gru_id";
            Param.Size = 16;
            Param.Value = entity.gru_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.Size = 1;
            Param.ParameterName = "@gru_situacao";
            Param.Value = 3;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@gru_dataAlteracao";
            Param.Size = 8;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);
        }

        protected override void ParamInserir(QuerySelectStoredProcedure qs, Autenticador.Entities.SYS_Grupo entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@gru_nome";
            Param.Size = 50;
            Param.Value = entity.gru_nome;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.Size = 1;
            Param.ParameterName = "@gru_situacao";
            Param.Value = entity.gru_situacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@gru_dataCriacao";
            Param.Size = 8;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@gru_dataAlteracao";
            Param.Size = 8;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@vis_id";
            Param.Size = 4;
            Param.Value = entity.vis_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@sis_id";
            Param.Size = 4;
            Param.Value = entity.sis_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@gru_integridade";
            Param.Size = 4;
            Param.Value = entity.gru_integridade;
            qs.Parameters.Add(Param);
        }









        public List<SYS_Grupo> SelecionarGrupoPorId(Guid idGrupo)
        {
            List<SYS_Grupo> lt = new List<SYS_Grupo>();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("MS_SelecionarGrupoPorId", this._Banco);
            try
            {
                #region Parâmetros

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@idGrupo";
                Param.Size = 16;
                Param.Value = idGrupo;
                qs.Parameters.Add(Param);

                #endregion 

                qs.Execute();

                foreach (DataRow dr in qs.Return.Rows)
                    {
                    SYS_Grupo entity = new SYS_Grupo();
                    lt.Add(this.DataRowToEntity(dr, entity));
                    }
                return lt;

            }
            catch
            {
                throw;
            }
        }

        
        public List<SYS_Grupo> SelecionarGruposPorIdSistema(int idSistema)
        {
            List<SYS_Grupo> lt = new List<SYS_Grupo>();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("MS_SelecionarGruposPorIdSistema", this._Banco);
            try
            {
                #region Parâmetros

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@idSistema";
                Param.Size = 4;
                Param.Value = idSistema;
                qs.Parameters.Add(Param);

                #endregion 

                qs.Execute();

                foreach (DataRow dr in qs.Return.Rows)
                {
                    SYS_Grupo entity = new SYS_Grupo();
                    lt.Add(this.DataRowToEntity(dr, entity));
                }
                return lt;
            }
            catch
            {
                throw;
            }
        }

    }

}