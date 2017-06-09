/*
	Classe gerada automaticamente pelo Code Creator
*/

namespace Autenticador.BLL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using CoreLibrary.Business.Common;
    using Autenticador.DAL;
    using Autenticador.Entities;
    using CoreLibrary.Data.Common;
    using CoreLibrary.Validation.Exceptions;
	
	/// <summary>
	/// Description: CFG_TemaPadrao Business Object. 
	/// </summary>
	public class CFG_TemaPadraoBO : BusinessBase<CFG_TemaPadraoDAO, CFG_TemaPadrao>
    {
        #region M�todos de consulta

        /// <summary>
        /// Carrega um tema padr�o pelo nome.
        /// </summary>
        /// <param name="tep_nome">Nome do tema padr�o.</param>
        /// <returns></returns>
        public static CFG_TemaPadrao CarregarPorNome(string tep_nome)
        {
            return new CFG_TemaPadraoDAO().CarregarPorNome(tep_nome);
        }

        /// <summary>
        /// Seleciona os temas ativos.
        /// </summary>
        /// <returns></returns>
        public static List<CFG_TemaPadrao> SelecionaAtivos()
        {
            return new CFG_TemaPadraoDAO().SelecionaAtivos();
        }

        /// <summary>
        /// Seleciona os temas ativos.
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static List<CFG_TemaPadrao> SelecionaAtivosPaginado(int currentPage, int pageSize)
        {
            pageSize = pageSize <= 0 ? 1 : pageSize;
            return new CFG_TemaPadraoDAO().SelecionaAtivosPaginado(currentPage / pageSize, pageSize, out totalRecords);
        }

        #endregion M�todos de consulta

        #region M�todos de valida��o

        /// <summary>
        /// Verifica se j� existe um tema com o mesmo nome.
        /// </summary>
        /// <param name="tep_id">ID do tema.</param>
        /// <param name="tep_nome">Nome do tema.</param>
        /// <returns></returns>
        public static bool VerificaExistePorNome(int tep_id, string tep_nome, TalkDBTransaction banco = null)
        {
            TalkDBTransaction bancoCore = banco == null ? new CFG_TemaPadraoDAO()._Banco.CopyThisInstance() : banco;
            if (banco == null || (!bancoCore.ConnectionIsOpen))
            {
                bancoCore.Open(IsolationLevel.ReadCommitted);
            }

            try
            {
                return new CFG_TemaPadraoDAO { _Banco = bancoCore }.VerificaExistePorNome(tep_id, tep_nome);
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
        /// <param name="tep_id">ID do registro.</param>
        /// <returns></returns>
        private static bool VerificaIntegridade(int tep_id, TalkDBTransaction banco = null)
        {
            TalkDBTransaction bancoCore = banco == null ? new CFG_TemaPadraoDAO()._Banco.CopyThisInstance() : banco;
            if (banco == null || (!bancoCore.ConnectionIsOpen))
            {
                bancoCore.Open(IsolationLevel.ReadCommitted);
            }

            try
            {
                return new CFG_TemaPadraoDAO { _Banco = bancoCore }.VerificaIntegridade(tep_id);
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
        /// M�todo para incluir/alterar um tema padr�o.
        /// </summary>
        /// <param name="entity">Entidade de tema padr�o.</param>
        /// <returns></returns>
        public static new bool Save(CFG_TemaPadrao entity)
        {
            TalkDBTransaction banco = new CFG_TemaPadraoDAO()._Banco.CopyThisInstance();
            banco.Open(IsolationLevel.ReadCommitted);

            try
            {
                if (entity.Validate())
                {
                    if (VerificaExistePorNome(entity.tep_id, entity.tep_nome))
                    {
                        throw new DuplicateNameException("J� existe um tema padr�o cadastrado com esse nome.");
                    }

                    return new CFG_TemaPadraoDAO().Salvar(entity);
                }

                throw new ValidationException(UtilBO.ErrosValidacao(entity));
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

        #endregion M�todos de inclus�o / altera��o

        #region M�todos de exclus�o

        /// <summary>
        /// Delete logicamente um registro de tema padr�o.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static new bool Delete(CFG_TemaPadrao entity)
        {
            TalkDBTransaction banco = new CFG_TemaPadraoDAO()._Banco.CopyThisInstance();
            banco.Open(IsolationLevel.ReadCommitted);

            try
            {
                if (VerificaIntegridade(entity.tep_id))
                {
                    throw new ValidationException("N�o � poss�vel excluir o tema padr�o pois possui outros registros ligados a ele.");
                }

                return new CFG_TemaPadraoDAO { _Banco = banco }.Delete(entity);
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