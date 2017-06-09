/*
	Classe gerada automaticamente pelo Code Creator
*/
using CoreLibrary.Business.Common;
using Autenticador.Entities;
using Autenticador.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Autenticador.BLL
{

    /// <summary>
    /// Description: SYS_LoginProviderDominioPermitido Business Object. 
    /// </summary>
    public class SYS_LoginProviderDominioPermitidoBO : BusinessBase<SYS_LoginProviderDominioPermitidoDAO, SYS_LoginProviderDominioPermitido>
    {
        private SYS_LoginProviderDominioPermitidoDAO dao;
        private SYS_LoginProviderDominioPermitido entity;
        public SYS_LoginProviderDominioPermitidoBO()
        {
            dao = new SYS_LoginProviderDominioPermitidoDAO();
            entity = new SYS_LoginProviderDominioPermitido();
        }
        /// <summary>
        ///  Retorna todos os dominios da configura��o
        /// </summary>
        /// <param name="entId">ID da Entidade</param>
        /// <param name="providerName">Provedor do Login</param>
        /// <returns>Array de string com os dominios encontrados</returns>
        public string[] ObterDominiosPermitidos(Guid entId, string providerName)
        {
            /*
            SYS_LoginProviderDominioPermitidoDAO dao = new SYS_LoginProviderDominioPermitidoDAO();
            SYS_LoginProviderDominioPermitido entity = new SYS_LoginProviderDominioPermitido()
            {
                ent_id = entId,
                LoginProvider = providerName
            };
            */
            entity.ent_id = entId;
            entity.LoginProvider = providerName;

            bool retorno = dao.Carregar(entity);

            string[] dominios = null;

            if (!String.IsNullOrEmpty(entity.Dominios))
                dominios = entity.Dominios.Split(',').Select(a => a.Trim()).ToArray();

            return dominios;
        }
        /// <summary>
        ///  Deleta SYS_LoginProviderDominioPermitido
        /// </summary>
        /// <param name="entity">SYS_LoginProviderDominioPermitido</param>
        /// <returns>Bool</returns>
        public new bool Delete(SYS_LoginProviderDominioPermitido entity)
        {
            return dao.Delete(entity);
        }

        public bool Salvar(SYS_LoginProviderDominioPermitido entity)
        {
           return dao.Salvar(entity);
        }
    }
}