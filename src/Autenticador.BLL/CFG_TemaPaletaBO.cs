/*
	Classe gerada automaticamente pelo Code Creator
*/

namespace Autenticador.BLL
{
    using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Web;
using CoreLibrary.Business.Common;
using Autenticador.DAL;
using Autenticador.Entities;
using CoreLibrary.Data.Common;
using CoreLibrary.Validation.Exceptions;
	
	/// <summary>
	/// Description: CFG_TemaPaleta Business Object. 
	/// </summary>
	public class CFG_TemaPaletaBO : BusinessBase<CFG_TemaPaletaDAO, CFG_TemaPaleta>
    {
        #region M�todos de consulta

        /// <summary>
        /// Seleciona todos os temas de cores ativos no sistema.
        /// </summary>
        /// <returns></returns>
        public static List<CFG_TemaPaleta> SelecionaAtivos()
        {
            return new CFG_TemaPaletaDAO().SelecionaAtivos();
        }

        /// <summary>
        /// Seleciona todos os temas de cores ativos no sistema. (Paginado)
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static List<CFG_TemaPaleta> SelecionaAtivosPaginado(int currentPage, int pageSize)
        {
            pageSize = pageSize <= 0 ? 1 : pageSize;
            return new CFG_TemaPaletaDAO().SelecionaAtivosPaginado(currentPage / pageSize, pageSize, out totalRecords);
        }

        /// <summary>
        /// Seleciona temas de cores pelo tema padr�o.
        /// </summary>
        /// <param name="tep_id">ID do tema padr�o.</param>
        /// <returns></returns>
        public static List<CFG_TemaPaleta> SelecionaPorTemaPadrao(int tep_id)
        {
            return new CFG_TemaPaletaDAO().SelecionaPorTemaPadrao(tep_id);
        }

        #endregion M�todos de consulta

        #region M�todos de valida��o

        /// <summary>
        /// Verifica se j� existe um tema de cores com o mesmo nome e o mesmo tema padr�o.
        /// </summary>
        /// <param name="tep_id">ID do tema padr�o.</param>
        /// <param name="tpl_id">ID do tema de cores.</param>
        /// <param name="tpl_nome">Nome do tema.</param>
        /// <param name="banco"></param>
        /// <returns></returns>
        public static bool VerificaExistePorNomeTemaPadrao(int tep_id, int tpl_id, string tpl_nome, TalkDBTransaction banco = null)
        {
            TalkDBTransaction bancoCore = banco == null ? new CFG_TemaPadraoDAO()._Banco.CopyThisInstance() : banco;
            if (banco == null || (!bancoCore.ConnectionIsOpen))
            {
                bancoCore.Open(IsolationLevel.ReadCommitted);
            }

            try
            {
                return new CFG_TemaPaletaDAO { _Banco = bancoCore }.VerificaExistePorNomeTemaPadrao(tep_id, tpl_id, tpl_nome);
            }
            catch (Exception ex)
            {
                if (banco == null)
                {
                    bancoCore.Close(ex);
                }

                throw;
            }
            finally
            {
                if (banco == null && bancoCore.ConnectionIsOpen)
                {
                    bancoCore.Close();
                }
            }
        }

        /// <summary>
        /// Verifica se o registro � utilizado em outras tabelas.
        /// </summary>
        /// <param name="tep_id">ID do tema padr�o.</param>
        /// <param name="tpl_id">ID do tema de cores.</param>
        /// <param name="banco"></param>
        /// <returns></returns>
        private static bool VerificaIntegridade(int tep_id, int tpl_id, TalkDBTransaction banco = null)
        {
            TalkDBTransaction bancoCore = banco == null ? new CFG_TemaPaletaDAO()._Banco.CopyThisInstance() : banco;
            if (banco == null || (!bancoCore.ConnectionIsOpen))
            {
                bancoCore.Open(IsolationLevel.ReadCommitted);
            }

            try
            {
                return new CFG_TemaPaletaDAO { _Banco = bancoCore }.VerificaIntegridade(tep_id, tpl_id);
            }
            catch (Exception ex)
            {
                if (banco == null)
                {
                    bancoCore.Close(ex);
                }

                throw;
            }
            finally
            {
                if (banco == null && bancoCore.ConnectionIsOpen)
                {
                    bancoCore.Close();
                }
            }
        }

        #endregion M�todos de valida��o

        #region M�todos de inclus�o / altera��o

        /// <summary>
        /// Salva os dados de uma entidade de tema de cores no banco.
        /// </summary>
        /// <param name="entity">Entidade de tema de cores.</param>
        /// <returns></returns>
        public static new bool Save(CFG_TemaPaleta entity)
        {
            if (entity.Validate())
            {
                if (VerificaExistePorNomeTemaPadrao(entity.tep_id, entity.tpl_id, entity.tpl_nome))
                {
                    throw new DuplicateNameException("J� existe um tema de cores cadastrado com esse nome.");
                }

                return new CFG_TemaPaletaDAO().Salvar(entity);
            }

            throw new ValidationException(UtilBO.ErrosValidacao(entity));
        }

        #endregion M�todos de inclus�o / altera��o

        #region M�todos de exclus�o

        /// <summary>
        /// Delete logicamente um registro de tema de cores.
        /// </summary>
        /// <param name="entity">Entidade de tema de cores.</param>
        /// <returns></returns>
        public static new bool Delete(CFG_TemaPaleta entity)
        {
            TalkDBTransaction banco = new CFG_TemaPadraoDAO()._Banco.CopyThisInstance();
            banco.Open(IsolationLevel.ReadCommitted);

            try
            {
                if (VerificaIntegridade(entity.tep_id, entity.tpl_id, banco))
                {
                    throw new ValidationException("N�o � poss�vel excluir o tema de cores pois possui outros registros ligados a ele.");
                }

                return new CFG_TemaPaletaDAO { _Banco = banco }.Delete(entity);
            }
            catch (Exception ex)
            {
                banco.Close(ex);
                throw;
            }
            finally
            {
                if (banco.ConnectionIsOpen)
                {
                    banco.Close();
                }
            }
        }

        #endregion M�todos de exclus�o
    }
}