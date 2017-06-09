using System;
using System.Data;
using CoreLibrary.Data.Common;
using Autenticador.DAL.Abstracts;


namespace Autenticador.DAL
{
    public class SYS_ModuloSiteMapDAO : Abstract_SYS_ModuloSiteMapDAO 
    {
        public Int32 Gerar_msm_id(int sis_id, int mod_id)
        {            
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_ModuloSiteMap_Select_Gerar_msm_id", _Banco);
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

                #endregion

                qs.Execute();
                
                return Convert.ToInt32(qs.Return.Rows[0][0]);
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

        public DataTable SelectBy_sis_id(int sis_id)
        {
            DataTable dt = new DataTable();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_ModuloSiteMap_SelectBy_sis_id", _Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@sis_id";
                Param.Size = 4;
                Param.Value = sis_id;
                qs.Parameters.Add(Param);

                #endregion

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

        public DataTable SelectBy_mod_idPai
        (
            int sis_id
            , int mod_idPai
            , Guid gru_id
            , int vis_id
        )
        {
            DataTable dt = new DataTable();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_ModuloSiteMap_SelectBy_mod_idPai", _Banco);
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
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@mod_idPai";
                Param.Size = 4;
                Param.Value = mod_idPai;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@gru_id";
                Param.Size = 16;
                Param.Value = gru_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@vis_id";
                Param.Size = 4;
                Param.Value = vis_id;
                qs.Parameters.Add(Param);

                #endregion

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
        /// Retorna um datatable contendo todos os sitemaps
        ///	sis_id e mod_id
        /// </summary>        
        /// <param name="sis_id">ID do sistema</param>
        /// <param name="mod_id">ID do módulo</param>
        /// <param name="paginado"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <returns>DataTable com os sitemaps</returns>
        public DataTable SelectBy_mod_id
        (
            int sis_id
            , int mod_id
            , bool paginado
            , int currentPage
            , int pageSize
            , out int totalRecords
        )
        {
            DataTable dt = new DataTable();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_ModuloSiteMap_SelectBy_mod_id", _Banco);
            try
            {
                #region PARAMETROS

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
                Param.ParameterName = "@mod_id";
                Param.Size = 8;
                if (mod_id > 0)
                    Param.Value = mod_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion

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
        /// Retorno booleano na qual verifica se existe a mesma URL no sistema
        /// </summary>
        /// <param name="sis_id">ID do sistema</param>        
        /// <param name="mod_id">ID do módulo</param>
        /// <param name="msm_id">ID do sitemap do módulo</param>        
        /// <param name="msm_url">URL do sitemap do módulo</param>
        /// <returns>True para registro existente/False para novo registro</returns>
        public bool SelectBy_Url
        (
            int sis_id
            , int mod_id
            , int msm_id            
            , string msm_url
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_ModuloSiteMap_SelectBy_URL", this._Banco);
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
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@mod_id";
                Param.Size = 4;
                Param.Value = mod_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Int32;
                Param.ParameterName = "@msm_id";
                Param.Size = 4;
                if (msm_id > 0)
                    Param.Value = msm_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@msm_url";
                Param.Size = 500;
                Param.Value = msm_url;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                return (qs.Return.Rows.Count > 0);

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
        /// Seleciona a URL do Help filtrando pela URL do sitemap.
        /// </summary>
        /// <param name="gru_id">ID do grupo</param>               
        /// <param name="msm_url">URL do sitemap</param>
        /// <returns>string da URL do Help</returns>
        public string SelectUrlHelpByUrl(Guid gru_id, string msm_url)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_ModuloSiteMap_SelectUrlHelpByUrl", this._Banco);
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

                #endregion

                qs.Execute();

                if (qs.Return.Rows.Count > 0)
                    return qs.Return.Rows[0]["msm_urlHelp"].ToString();
                else
                    return string.Empty;
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
        /// Retorna as urls do help, as urls do sitemap e o nome dos módulos
        /// cadastrados para o sistema informado.
        /// </summary>
        /// <param name="sis_id">ID do sistema</param>               
        /// <returns>string da URL do Help</returns>
        public DataTable Select_Modulos_Urls_Help(int sis_id)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_ModuloSiteMap_Select_Modulos_Urls_Help", this._Banco);

            #region PARAMETROS

            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@sis_id";
            Param.Size = 4;
            Param.Value = sis_id;
            qs.Parameters.Add(Param);
                
            #endregion

            qs.Execute();

            return qs.Return;
        }
    }
}
