using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Caching;
using CoreLibrary.Business.Common;
using Autenticador.DAL;
using Autenticador.Entities;
using System.ComponentModel;
using CoreLibrary.Data.Common;
using CoreLibrary.Validation.Exceptions;

namespace Autenticador.BLL
{
    public class SYS_ModuloSiteMapBO : BusinessBase<SYS_ModuloSiteMapDAO, SYS_ModuloSiteMap>
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetSelect_by_mod_id
        (
            int sis_id
            , int mod_id
            , bool paginado
            , int currentPage
            , int pageSize

        )
        {
            if (pageSize == 0)
                pageSize = 1;

            totalRecords = 0;
            SYS_ModuloSiteMapDAO dao = new SYS_ModuloSiteMapDAO();
            try
            {
                return dao.SelectBy_mod_id(sis_id, mod_id, paginado, currentPage / pageSize, pageSize, out totalRecords);
            }
            catch
            {
                throw;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetSelect_by_mod_idPai
        (
            int sis_id
            , int mod_idPai
            , Guid gru_id
            , int vis_id
        )
        {
            SYS_ModuloSiteMapDAO dao = new SYS_ModuloSiteMapDAO();
            try
            {
                return dao.SelectBy_mod_idPai(sis_id, mod_idPai, gru_id, vis_id);
            }
            catch
            {
                throw;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetSelect_by_sis_id
        (
            int sis_id            
        )
        {
            SYS_ModuloSiteMapDAO dao = new SYS_ModuloSiteMapDAO();
            try
            {
                return dao.SelectBy_sis_id(sis_id);
            }
            catch
            {
                throw;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static int Gerar_msm_id(int sis_id, int mod_id)
        {
            SYS_ModuloSiteMapDAO dal = new SYS_ModuloSiteMapDAO();
            try
            {
                return dal.Gerar_msm_id(sis_id, mod_id);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Retorno booleano na qual verifica se existe a mesma URL no sistema
        /// </summary>
        /// <param name="entity">Entidade SYS_ModuloSiteMap</param>        
        /// <returns>True para registro existente/False para novo registro</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static bool VerificaURLExistente
        (
            SYS_ModuloSiteMap entity
        )
        {
            int msm_id = entity.IsNew ? 0 : entity.msm_id;

            SYS_ModuloSiteMapDAO dao = new SYS_ModuloSiteMapDAO();
            return dao.SelectBy_Url(entity.sis_id, entity.mod_id, msm_id, entity.msm_url);
        }

        /// <summary>
        /// Seleciona a URL do Help filtrando pela URL do sitemap.
        /// </summary>
        /// <param name="gru_id">ID do grupo</param>               
        /// <param name="msm_url">URL do sitemap</param>
        /// <returns>string da URL do Help</returns>
        public static string SelecionaUrlHelpByUrl(Guid gru_id, string msm_url)
        {
            SYS_ModuloSiteMapDAO dao = new SYS_ModuloSiteMapDAO();
            return dao.SelectUrlHelpByUrl(gru_id, msm_url);
        }

        /// <summary>
        /// Retorna a chave para o cahce dos módulos e urls do sistema.
        /// "Modulos_Urls_UrlHelp_[sis_id]"
        /// </summary>
        /// <param name="sis_id">ID do sistema</param>
        /// <returns></returns>
        private static string ChaveCacheModulosUrls(int sis_id)
        {
            return "Modulos_Urls_UrlHelp_" + sis_id;
        }

        /// <summary>
        /// Seleciona a URL do Help filtrando pela URL do sitemap,
        /// busca os resultados uma única vez e guarda em cache (2 horas).
        /// </summary>
        /// <param name="msm_url">URL do sitemap</param>
        /// <param name="sis_id">ID do sistema para carregar os módulos</param>
        /// <returns>string da URL do Help</returns>
        public static string SelecionaUrlHelpByUrl_Cache(string msm_url, int sis_id)
        {
            DataTable dt = RetornaTabelaModulosUrls_Cache(sis_id);

            // Busca o resultado da tabela de urls.
            var resultado = from DataRow dr in dt.Rows
                    where dr["msm_url"].ToString() == msm_url
                    select dr["msm_urlHelp"].ToString();

            return resultado.FirstOrDefault();
        }

        /// <summary>
        /// Retorna a tabela com todos os módulos, urls desses módulos e urls do help cadastrados
        /// para o sistema.
        /// </summary>
        /// <param name="sis_id">ID do sistema</param>
        /// <returns></returns>
        private static DataTable RetornaTabelaModulosUrls_Cache(int sis_id)
        {
            string chave = ChaveCacheModulosUrls(sis_id);
            object cache = HttpContext.Current != null
                               ? HttpContext.Current.Cache[chave]
                               : null;

            DataTable dt;

            if ((cache == null) || (((DataTable)cache).Rows.Count == 0))
            {
                // Carregar dados do banco de dados se não estão no cache.
                dt = new SYS_ModuloSiteMapDAO().Select_Modulos_Urls_Help(sis_id);

                // Guarda resultado da consulta em cache.
                HttpContext.Current.Cache.Insert(chave, dt, null, DateTime.Now.AddMonths(1),
                                                 Cache.NoSlidingExpiration);
            }
            else
            {
                dt = (DataTable) cache;
            }
            return dt;
        }

        public new static bool Save
        (
            SYS_ModuloSiteMap entity
            , TalkDBTransaction banco
        )
        {
            if (entity.Validate())
            {
                if (VerificaURLExistente(entity))
                    throw new ValidationException("Já existe um SiteMap cadastrado com a URL '" + entity.msm_url + "' no sistema.");

                SYS_ModuloSiteMapDAO dao = new SYS_ModuloSiteMapDAO {_Banco = banco};
                dao.Salvar(entity);
            }
            else
            {
                throw new ValidationException(entity.PropertiesErrorList[0].Message);
            }

            return true;
        }

        public new static bool Delete
        (
            SYS_ModuloSiteMap entity
            ,CoreLibrary.Data.Common.TalkDBTransaction banco
        )
        {
            try
            {
                SYS_ModuloSiteMapDAO moduloSiteMapDAO = new SYS_ModuloSiteMapDAO { _Banco = banco };
                moduloSiteMapDAO.Delete(entity);

                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
