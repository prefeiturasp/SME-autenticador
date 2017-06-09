using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreLibrary.Business.Common;
using Autenticador.Entities;
using Autenticador.DAL;
using System.Net;
using System.Data;
using CoreLibrary.Security.Cryptography;
using System.ComponentModel;
using CoreLibrary.Validation.Exceptions;

namespace Autenticador.BLL
{
    public class SYS_IntegracaoExternaBO : BusinessBase<SYS_IntegracaoExternaDAO, SYS_IntegracaoExterna>
    {
        #region Enum

        public enum eSituacao 
        {
            Ativo = 1,
            Inativo = 2
        }

        public enum eTipoIntegracaoExterna
        {
            Live = 1
        }

        #endregion

        #region Methods

        #region Select

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ine_tipo"></param>
        /// <returns></returns>
        public static DataTable GetSelect
        (
            byte ine_tipo
        )
        {
            SYS_IntegracaoExternaDAO dao = new SYS_IntegracaoExternaDAO();
            return dao.SelectBy_Pesquisa(ine_tipo);
        }

        /// <summary>
        /// Retorna a entidade SYS_IntegracaoExterna filtrando pelo tipo de integração externa.
        /// </summary>
        /// <param name="ine_tipo">Tipo de integração externa</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static SYS_IntegracaoExterna GetSelectBy_ine_tipo(byte ine_tipo)
        {
            SYS_IntegracaoExternaDAO dao = new SYS_IntegracaoExternaDAO();
            return dao.SelectBy_ine_tipo(ine_tipo);
        }

        #endregion

        #region Save

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public new static bool Save(SYS_IntegracaoExterna entity)
        {
            SYS_IntegracaoExternaDAO dao = new SYS_IntegracaoExternaDAO();

            if (entity.ine_proxyAutenticacao)
            {
                if (string.IsNullOrEmpty(entity.ine_proxyAutenticacaoSenha))
                {
                    SYS_IntegracaoExterna NewEntity = new SYS_IntegracaoExterna() { ine_id = entity.ine_id };
                    GetEntity(NewEntity);
                    entity.ine_proxyAutenticacaoSenha = NewEntity.ine_proxyAutenticacaoSenha;
                }
                else
                {
                    SymmetricAlgorithm encript = new SymmetricAlgorithm(SymmetricAlgorithm.Tipo.TripleDES);
                    entity.ine_proxyAutenticacaoSenha = encript.Encrypt(entity.ine_proxyAutenticacaoSenha);
                }
            }

            if (entity.Validate())
                return dao.Salvar(entity);
            else
                throw new ValidationException(entity.PropertiesErrorList[0].Message);
        }

        #endregion

        /// <summary>
        /// Verifica configuraçãoes da Integração Externa, 
        /// e cria objeto WebProxy apartir das mesmas.
        /// <param name="entityIntegracaoExterna">Entidade SYS_IntegracaoExterna</param>
        /// <param name="proxy"></param>
        /// <returns></returns>
        public static bool GerarProxy(SYS_IntegracaoExterna entityIntegracaoExterna, out WebProxy proxy)
        {
            bool flagProxy = ((entityIntegracaoExterna.ine_proxy) && (!string.IsNullOrEmpty(entityIntegracaoExterna.ine_proxyIP)));

            // Verifica se configurado para Usar Proxy
            if (flagProxy)
            {
                int proxyPorta = (!string.IsNullOrEmpty(entityIntegracaoExterna.ine_proxyPorta) ? Convert.ToInt32(entityIntegracaoExterna.ine_proxyPorta) : 0);
                proxy = new WebProxy(entityIntegracaoExterna.ine_proxyIP, proxyPorta);

                // Verifica se configurado para Usar Autenticação,
                // caso esteja instancia o NetworkCredential do proxy
                if (entityIntegracaoExterna.ine_proxyAutenticacao)
                {
                    if ((!string.IsNullOrEmpty(entityIntegracaoExterna.ine_proxyAutenticacaoUsuario)) &&
                        (!string.IsNullOrEmpty(entityIntegracaoExterna.ine_proxyAutenticacaoSenha)))
                    {
                        SymmetricAlgorithm encript = new SymmetricAlgorithm(SymmetricAlgorithm.Tipo.TripleDES);
                        NetworkCredential credencial = new NetworkCredential(entityIntegracaoExterna.ine_proxyAutenticacaoUsuario, encript.Decrypt(entityIntegracaoExterna.ine_proxyAutenticacaoSenha));
                        proxy.Credentials = credencial;
                    }
                }
            }
            else
            {
                proxy = new WebProxy();
            }

            return flagProxy;
        }

        #endregion
    }
}
