using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreLibrary.Business.Common;
using Autenticador.DAL;
using Autenticador.Entities;
using System.ComponentModel;
using System.Data;
using CoreLibrary.Validation.Exceptions;
using CoreLibrary.Data.Common;

namespace Autenticador.BLL
{
    public class CFG_ConfiguracaoBO : BusinessBase<CFG_ConfiguracaoDAO, CFG_Configuracao>
    {
        #region Enumerador

        public enum eSituacao
        {
            Ativo = 1
                ,
            Interno = 4
        }

        #endregion

        /// <summary>
        /// Retorna as configurações ativas.
        /// </summary>
        /// <returns>List de entidades CFG_Configuracao</returns>
        public static List<CFG_Configuracao> Consultar()
        {
            CFG_ConfiguracaoDAO dao = new CFG_ConfiguracaoDAO();
            return dao.Select(false, 0, 0, out totalRecords);
        }

        /// <summary>
        /// Retorna as configurações ativas.
        /// </summary>
        /// <param name="paginado">Indica se será paginado</param>
        /// <param name="currentPage">Página atual</param>
        /// <param name="pageSize">Quantidade de registros por página</param>
        /// <returns>List de entidades CFG_Configuracao</returns>
        public static List<CFG_Configuracao> Consultar
            (
                 bool paginado
                , int currentPage
                , int pageSize
            )
        {
            CFG_ConfiguracaoDAO dao = new CFG_ConfiguracaoDAO();
            return dao.Select(paginado, currentPage, pageSize, out totalRecords);
        }

        /// <summary>
        /// Seleciona valor de configuração do sistema pela chave.
        /// </summary>
        /// <param name="cfg_chave">Chave da busca.</param>
        /// <param name="banco">Transação.</param>
        /// <returns></returns>
        public static string SelecionaValorPorChave(string cfg_chave, TalkDBTransaction banco = null)
        {
            return banco == null ?
                new CFG_ConfiguracaoDAO().SelecionaValorPorChave(cfg_chave) :
                new CFG_ConfiguracaoDAO { _Banco = banco }.SelecionaValorPorChave(cfg_chave);
        }

        /// <summary>
        /// Verifica se existe uma configuração que possua a mesma chave.
        /// </summary>
        /// <param name="entity">Entidade CFG_Configuracao</param>
        /// <returns></returns>
        public static bool VerificarChaveExistente(CFG_Configuracao entity)
        {
            CFG_ConfiguracaoDAO dao = new CFG_ConfiguracaoDAO();
            return dao.SelectBy_cfg_chave(entity);
        }

        /// <summary>
        ///  Salva (inclusão ou alteração) uma configuração.
        /// </summary>
        /// <param name="entity">Entidade CFG_Configuracao</param>
        /// <returns></returns>
        public static bool Salvar(CFG_Configuracao entity)
        {
            CFG_ConfiguracaoDAO dao = new CFG_ConfiguracaoDAO();

            // Verifica chave da configuração
            if (VerificarChaveExistente(entity))
                throw new DuplicateNameException("Já existe uma configuração cadastrada com esta chave.");

            if (entity.Validate())
                return dao.Salvar(entity);
            else
                throw new ValidationException(UtilBO.ErrosValidacao(entity));
        }

        /// <summary>
        /// Deleta uma configuração.
        /// </summary>
        /// <param name="entityConfiguracao">Entidade CFG_Configuracao</param>
        /// <returns></returns>
        public static bool Deletar(CFG_Configuracao entity)
        {
            CFG_ConfiguracaoDAO dao = new CFG_ConfiguracaoDAO();

            // Verifica situação da configuração
            if (entity.cfg_situacao == Convert.ToByte(eSituacao.Interno))
                throw new ValidationException("A configuração possui situação obrigatória, não pode ser excluída.");

            return dao.Delete(entity);
        }
    }
}
