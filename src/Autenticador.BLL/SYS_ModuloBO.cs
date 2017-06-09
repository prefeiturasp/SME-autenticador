using CoreLibrary.Business.Common;
using Autenticador.DAL;
using Autenticador.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Caching;
using System.Xml.XPath;

namespace Autenticador.BLL
{
    public class SYS_ModuloBO : BusinessBase<SYS_ModuloDAO, SYS_Modulo>
    {
        /// <summary>
        /// Retorna a chave para o cahce de menu.
        /// "Menu_[gru_id]_[sis_id]_[vis_id]"
        /// </summary>
        /// <param name="gru_id">ID do grupo</param>
        /// <param name="sis_id">ID do sistema</param>
        /// <param name="vis_id">ID da visão</param>
        /// <returns></returns>
        private static string ChaveCacheMenu(Guid gru_id, int sis_id, int vis_id)
        {
            return "Menu_" + gru_id + "_" + sis_id + "_" + vis_id;
        }

        /// <summary>
        /// Retorna a string com o xml do menu, de acordo com as permissões do grupo do usuário, com 
        /// a visão e o sistema.
        /// Armazena em cache o valor da consulta (duração de 1 hora).
        /// </summary>
        /// <param name="gru_id"></param>
        /// <param name="sis_id"></param>
        /// <param name="vis_id"></param>
        /// <returns></returns>
        public static string CarregarMenuXML(Guid gru_id, int sis_id, int vis_id, int cacheMinutos = 60)
        {

            string xmlRetorno;

            if (cacheMinutos > 0 && HttpContext.Current != null)
            {
                string chave = ChaveCacheMenu(gru_id, sis_id, vis_id);
                object cache = HttpContext.Current != null
                    ? HttpContext.Current.Cache[chave]
                    : null;

                if ((cache == null) || (string.IsNullOrEmpty(cache.ToString())))
                {
                    SYS_ModuloDAO dal = new SYS_ModuloDAO();
                    XPathDocument xDoc = dal.SelectBy_MenuXML(gru_id, sis_id, vis_id);
                    XPathNavigator xpNav = xDoc.CreateNavigator();

                    xmlRetorno = xpNav.InnerXml;

                    // Se estiver vazio, é inválido.
                    if (string.IsNullOrEmpty(xmlRetorno))
                        xmlRetorno = "<menus/>";

                    // Guarda resultado da consulta em cache.
                    HttpContext.Current.Cache.Insert(chave, xmlRetorno, null, DateTime.Now.AddMinutes(cacheMinutos),
                                                     Cache.NoSlidingExpiration);
                }
                else
                {
                    xmlRetorno = cache.ToString();
                }
            }
            else
            {
                SYS_ModuloDAO dal = new SYS_ModuloDAO();
                XPathDocument xDoc = dal.SelectBy_MenuXML(gru_id, sis_id, vis_id);
                XPathNavigator xpNav = xDoc.CreateNavigator();

                xmlRetorno = xpNav.InnerXml;

                // Se estiver vazio, é inválido.
                if (string.IsNullOrEmpty(xmlRetorno))
                    xmlRetorno = "<menus/>";
            }

            return xmlRetorno;
        }
        /// <summary>
        /// Carrega o mapa do site em XML à partir de um módulo selecionado pelo usuário.
        /// Usado nos link do breadcrumb para mostrar o mapa do site.
        /// </summary>
        /// <param name="gru_id">Grupo do usuário logado</param>
        /// <param name="sis_id">Sistema do usuário logado</param>
        /// <param name="vis_id">Visão do usuário logado</param>
        /// <param name="mod_id">ID do módulo que deseja iniciar o mapa do site</param>
        /// <returns>XML contendo todos os módulos do sistema à partir do módulo selecionado.</returns>
        public static string CarregarSiteMapXML(Guid gru_id, int sis_id, int vis_id, int mod_id)
        {
            SYS_ModuloDAO dal = new SYS_ModuloDAO();
            try
            {
                XPathDocument xDoc = dal.SelectBy_SiteMapXML(gru_id, sis_id, vis_id, mod_id);
                XPathNavigator xpNav = xDoc.CreateNavigator();
                return xpNav.InnerXml;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Carrega o mapa do site em XML à partir do sistema ou de um módulo selecionado pelo usuário.
        /// Usado nos link do breadcrumb para mostrar o mapa do site.
        /// </summary>
        /// <param name="gru_id">Grupo do usuário logado</param>
        /// <param name="sis_id">Sistema do usuário logado</param>
        /// <param name="vis_id">Visão do usuário logado</param>
        /// <param name="mod_id">ID do módulo que deseja iniciar o mapa do site</param>
        /// <returns>XML contendo todos os módulos do sistema à partir do módulo selecionado.</returns>
        public static string CarregarSiteMapXML2(Guid gru_id, int sis_id, int vis_id, int mod_id)
        {
            SYS_ModuloDAO dal = new SYS_ModuloDAO();
            try
            {
                XPathDocument xDoc = dal.SelectBy_SiteMapXML2(gru_id, sis_id, vis_id, mod_id);
                XPathNavigator xpNav = xDoc.CreateNavigator();
                return xpNav.InnerXml;
            }
            catch
            {
                throw;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetSelect_by_Sis_id
        (
              int sis_id
            , bool paginado
            , int currentPage
            , int pageSize

        )
        {
            if (pageSize == 0)
                pageSize = 1;

            totalRecords = 0;
            SYS_ModuloDAO dal = new SYS_ModuloDAO();
            try
            {
                return dal.GetSelect_by_Sis_id(sis_id, paginado, currentPage / pageSize, pageSize, out totalRecords);
            }
            catch
            {
                throw;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable SelectBy_mod_id_Filhos
        (
              int sis_id
              , int mod_id
        )
        {
            SYS_ModuloDAO dal = new SYS_ModuloDAO();
            try
            {
                return dal.SelectBy_mod_id_Filhos(sis_id, mod_id);
            }
            catch
            {
                throw;
            }
        }

        public bool SalvarAuditoria(List<SYS_Modulo> lstModulos)
        {
            SYS_ModuloDAO modDal = new SYS_ModuloDAO();
            modDal._Banco.Open(IsolationLevel.ReadCommitted);
            try
            {
                foreach (SYS_Modulo mod in lstModulos)
                {
                    modDal.UpdateAuditoria(mod);
                }
                return true;

            }
            catch (Exception err)
            {
                modDal._Banco.Close(err);
                throw;
            }
            finally
            {
                modDal._Banco.Close();
            }

        }

        /// <summary>
        /// Inclui ou Altera um módulo     
        /// </summary>            
        /// <param name="modulo"></param>
        /// <param name="SiteMapMenu">SiteMap selecionado como menu do módulo</param>                
        /// <param name="SiteMapMenuAntigo"></param>
        /// <param name="dtVisoes">Datatable de Visões</param>        
        /// <param name="dtSiteMap"></param>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static bool Save
        (
            SYS_Modulo modulo
            , int SiteMapMenu
            , int SiteMapMenuAntigo
            , DataTable dtVisoes
            , DataTable dtSiteMap
        )
        {
            SYS_ModuloDAO moduloDal = new SYS_ModuloDAO();
            moduloDal._Banco.Open(IsolationLevel.ReadCommitted);
            try
            {
                if (modulo.Validate())
                {
                    moduloDal.Salvar(modulo);

                    //Inserir SiteMap.
                    for (int i = 0; i < dtSiteMap.Rows.Count; i++)
                    {
                        if (dtSiteMap.Rows[i].RowState == DataRowState.Added || dtSiteMap.Rows[i].RowState == DataRowState.Modified)
                        {
                            int msm_id = Convert.ToInt32(dtSiteMap.Rows[i]["msm_id"].ToString());

                            SYS_ModuloSiteMap entity = new SYS_ModuloSiteMap
                            {
                                sis_id = modulo.sis_id
                                ,
                                mod_id = modulo.mod_id
                                ,
                                msm_id = msm_id
                                ,
                                msm_nome = dtSiteMap.Rows[i]["msm_nome"].ToString()
                                ,
                                msm_url = dtSiteMap.Rows[i]["msm_url"].ToString()
                                ,
                                msm_urlHelp = dtSiteMap.Rows[i]["msm_urlHelp"].ToString()
                                ,
                                msm_descricao = dtSiteMap.Rows[i]["msm_descricao"].ToString()
                                ,
                                msm_informacoes = dtSiteMap.Rows[i]["msm_informacoes"].ToString()
                                ,
                                IsNew = dtSiteMap.Rows[i].RowState == DataRowState.Added
                            };
                            SYS_ModuloSiteMapBO.Save(entity, moduloDal._Banco);

                            if (SiteMapMenu == msm_id)
                                SiteMapMenu = entity.msm_id;

                            if (SiteMapMenuAntigo == msm_id)
                                SiteMapMenuAntigo = entity.msm_id;
                        }
                    }

                    for (int i = 0; i < dtVisoes.Rows.Count; i++)
                    {
                        if (dtVisoes.Rows[i].RowState == DataRowState.Added)
                        {
                            int vis_id = Convert.ToInt32(dtVisoes.Rows[i]["vis_id"].ToString());
                            int vmm_ordem = SYS_VisaoModuloMenuBO.Gerar_vmm_ordem(modulo.sis_id, modulo.mod_idPai, vis_id, moduloDal._Banco);

                            //Inserir visão do módulo
                            SYS_VisaoModulo visaoModulo = new SYS_VisaoModulo
                            {
                                sis_id = modulo.sis_id
                                ,
                                mod_id = modulo.mod_id
                                ,
                                vis_id = vis_id
                                ,
                                IsNew = true
                            };
                            SYS_VisaoModuloBO.Save(visaoModulo, moduloDal._Banco);

                            if (SiteMapMenu > 0)
                            {
                                //Insere menu para a visão nova.
                                SYS_VisaoModuloMenu visaoModuloMenu = new SYS_VisaoModuloMenu
                                {
                                    sis_id = modulo.sis_id
                                    ,
                                    mod_id = modulo.mod_id
                                    ,
                                    msm_id = SiteMapMenu
                                    ,
                                    vis_id = vis_id
                                    ,
                                    vmm_ordem = vmm_ordem
                                };

                                SYS_VisaoModuloMenuBO.Save(visaoModuloMenu, moduloDal._Banco);
                            }

                            //Inserir permissões para os grupos das visões selecionadas.
                            SYS_GrupoPermissaoBO.InsertPermissao_Visoes(modulo.sis_id, vis_id, modulo.mod_id, moduloDal._Banco);
                        }
                        else if (dtVisoes.Rows[i].RowState == DataRowState.Deleted)
                        {
                            int vis_id = Convert.ToInt32(dtVisoes.Rows[i]["vis_id", DataRowVersion.Original].ToString());

                            if (SiteMapMenuAntigo > 0)
                            {
                                //Deleta menu da visão.
                                SYS_VisaoModuloMenu visaoModuloMenu = new SYS_VisaoModuloMenu
                                {
                                    sis_id = modulo.sis_id
                                    ,
                                    mod_id = modulo.mod_id
                                    ,
                                    msm_id = SiteMapMenuAntigo
                                    ,
                                    vis_id = vis_id
                                };
                                SYS_VisaoModuloMenuBO.Delete(visaoModuloMenu, moduloDal._Banco);
                            }

                            //Deletar visão do módulo
                            SYS_VisaoModulo visaoModulo = new SYS_VisaoModulo
                            {
                                sis_id = modulo.sis_id
                                ,
                                mod_id = modulo.mod_id
                                ,
                                vis_id = vis_id
                            };
                            SYS_VisaoModuloBO.Delete(visaoModulo, moduloDal._Banco);

                            //Deletar permissões para os grupos das visões selecionadas.
                            SYS_GrupoPermissaoBO.DeletePermissao_Visoes(modulo.sis_id, vis_id, modulo.mod_id, moduloDal._Banco);
                        }
                        else
                        {
                            //Caso tenha alterado o SiteMap do menu e nada feito na visão.
                            if (SiteMapMenu != SiteMapMenuAntigo)
                            {
                                int vis_id = Convert.ToInt32(dtVisoes.Rows[i]["vis_id"].ToString());
                                int vmm_ordem = SYS_VisaoModuloMenuBO.Gerar_vmm_ordem(modulo.sis_id, modulo.mod_idPai, vis_id, moduloDal._Banco);

                                if (SiteMapMenuAntigo > 0)
                                {
                                    //Deleta o anterior pra cada visão selecionada.
                                    SYS_VisaoModuloMenu visaoModuloMenu = new SYS_VisaoModuloMenu
                                    {
                                        sis_id = modulo.sis_id
                                        ,
                                        mod_id = modulo.mod_id
                                        ,
                                        msm_id = SiteMapMenuAntigo
                                        ,
                                        vis_id = vis_id
                                    };
                                    SYS_VisaoModuloMenu visaoModuloMenuAntigo = SYS_VisaoModuloMenuBO.GetEntity(visaoModuloMenu);
                                    SYS_VisaoModuloMenuBO.Delete(visaoModuloMenu, moduloDal._Banco);

                                    vmm_ordem = visaoModuloMenuAntigo.vmm_ordem;
                                }

                                //Inclui o novo pra cada visão selecionada.
                                if (SiteMapMenu > 0)
                                {
                                    SYS_VisaoModuloMenu visaoModuloMenuNovo = new SYS_VisaoModuloMenu
                                    {
                                        sis_id = modulo.sis_id
                                        ,
                                        mod_id = modulo.mod_id
                                        ,
                                        msm_id = SiteMapMenu
                                        ,
                                        vis_id = vis_id
                                        ,
                                        vmm_ordem = vmm_ordem
                                    };
                                    SYS_VisaoModuloMenuBO.Save(visaoModuloMenuNovo, moduloDal._Banco);
                                }
                            }
                        }
                    }
                }
                else
                {
                    throw new CoreLibrary.Validation.Exceptions.ValidationException(modulo.PropertiesErrorList[0].Message);
                }

                //Deletar SiteMap.
                for (int i = 0; i < dtSiteMap.Rows.Count; i++)
                {
                    if (dtSiteMap.Rows[i].RowState == DataRowState.Deleted)
                    {
                        int msm_id = Convert.ToInt32(dtSiteMap.Rows[i]["msm_id", DataRowVersion.Original].ToString());

                        //Deleta menu da visão.
                        SYS_VisaoModuloMenu visaoModuloMenu = new SYS_VisaoModuloMenu
                        {
                            sis_id = modulo.sis_id
                            ,
                            mod_id = modulo.mod_id
                            ,
                            msm_id = msm_id
                        };
                        SYS_VisaoModuloMenuBO.Delete(visaoModuloMenu, moduloDal._Banco);

                        SYS_ModuloSiteMap entity = new SYS_ModuloSiteMap
                        {
                            sis_id = modulo.sis_id
                            ,
                            mod_id = modulo.mod_id
                            ,
                            msm_id = msm_id
                        };
                        SYS_ModuloSiteMapBO.Delete(entity, moduloDal._Banco);
                    }
                }

                return true;
            }
            catch (Exception err)
            {
                moduloDal._Banco.Close(err);
                throw;
            }
            finally
            {
                moduloDal._Banco.Close();
            }
        }


        public bool ValidarPermissaoModuloPai(int sis_id, int vis_id, int mod_id)
        {
            bool ret = true;

            SYS_ModuloDAO dao = new SYS_ModuloDAO();

            DataTable dt = dao.SelecionaModulos_By_sis_id_vis_id_mod_id(sis_id, vis_id, mod_id);

            if (dt.Rows.Count <= 0)
                ret = false;

            return ret;
        }




        /// <summary>
        /// Seleciona os módulos (e módulos filhos) de um determinado grupo do usuário
        /// </summary>
        /// <param name="sistemaId">Id do sistema</param>
        /// <param name="grupoId">Id do grupo</param>
        /// <returns>DataTable com os módulos e submódulos</returns>
        public static DataTable SelecionarModulosPorIdGrupoUsuario(int sistemaId, Guid grupoId)
        {
            SYS_ModuloDAO dao = new SYS_ModuloDAO();
            try
            {
                return dao.SelecionarModulosPorIdGrupoUsuario(sistemaId, grupoId);
            }
            catch
            {
                throw;
            }
        }


    }
}
