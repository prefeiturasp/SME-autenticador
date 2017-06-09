/*
	Classe gerada automaticamente pelo Code Creator
*/

namespace Autenticador.BLL
{
	using CoreLibrary.Business.Common;
	using Autenticador.Entities;
	using Autenticador.DAL;
    using System.Collections.Generic;
    using CoreLibrary.Data.Common;
    using System.Data;
    using System;
    using CoreLibrary.Validation.Exceptions;

    /// <summary>
	/// Description: CFG_UsuarioAPI Business Object. 
	/// </summary>
	public class CFG_UsuarioAPIBO : BusinessBase<CFG_UsuarioAPIDAO, CFG_UsuarioAPI>
    {
        #region M�todos de consulta

        /// <summary>
        /// Seleciona uma lista de usu�rio API ativos. 
        /// </summary>
        /// <param name="banco"></param>
        /// <returns></returns>
        public static List<CFG_UsuarioAPI> SelecionaAtivos(TalkDBTransaction banco = null)
        {
            return banco == null ?
                new CFG_UsuarioAPIDAO().SelecionaAtivos() :
                new CFG_UsuarioAPIDAO { _Banco = banco }.SelecionaAtivos();
        }

        /// <summary>
        /// Seleciona um usu�rio da API pelo nome.
        /// </summary>
        /// <param name="uap_username">Nome do usu�rio da API.</param>
        /// <returns></returns>
        public static CFG_UsuarioAPI SelecionaPorUsername(string uap_username)
        {
            return new CFG_UsuarioAPIDAO().SelecionaPorUsername(uap_username);
        }

        #endregion

        #region M�todos de verifica��o

        /// <summary>
        /// Verifica se existe um usu�rio com o mesmo username.
        /// </summary>
        /// <param name="entity">Entidade do usu�rio</param>
        /// <param name="banco">Transa��o</param>
        /// <returns>True, se j� existir.</returns>
        public static bool VerificaUsernameExistente(CFG_UsuarioAPI entity, TalkDBTransaction banco = null)
        {
            return banco == null ?
                new CFG_UsuarioAPIDAO().VerificaUsernameExistente(entity.uap_username, entity.uap_id) :
                new CFG_UsuarioAPIDAO { _Banco = banco }.VerificaUsernameExistente(entity.uap_username, entity.uap_id);
        }

        #endregion

        #region M�todos de inser��o/altera��o

        /// <summary>
        /// Atualiza a senha do usu�rio.
        /// </summary>
        /// <param name="entity">Entidade do usu�rios com os campos de ID e a nova senha preenchidos.</param>
        /// <param name="banco"></param>
        /// <returns></returns>
        public static bool AtualizaSenha(CFG_UsuarioAPI entity, TalkDBTransaction banco = null)
        {
            CFG_UsuarioAPIDAO dao = banco == null ?
                new CFG_UsuarioAPIDAO() :
                new CFG_UsuarioAPIDAO { _Banco = banco };
            return dao.AtualizaSenha(entity);
        }

        /// <summary>
        /// O m�todo realiza valida��es e criptografa a senha antes de salvar o usu�rio API.
        /// </summary>
        /// <param name="entity">Entidade do usu�rio</param>
        /// <returns></returns>
        public static new bool Save(CFG_UsuarioAPI entity)
        {
            CFG_UsuarioAPIDAO dao = new CFG_UsuarioAPIDAO();
            dao._Banco.Open(IsolationLevel.ReadCommitted);

            try
            {
                bool retorno = false;

                entity.uap_password = string.IsNullOrEmpty(entity.uap_password) ? string.Empty : UtilBO.CriptografarSenha(entity.uap_password, eCriptografa.TripleDES);

                if (entity.IsNew && string.IsNullOrEmpty(entity.uap_password))
                    throw new ValidationException("Senha do usu�rio API � obrigat�rio.");

                if (!string.IsNullOrEmpty(entity.uap_password) && entity.uap_password.Length > 256)
                    throw new ValidationException("Senha do usu�rio API deve possuir at� 256 caracteres.");

                if (!entity.Validate())
                    throw new ValidationException(UtilBO.ErrosValidacao(entity));

                if (VerificaUsernameExistente(entity, dao._Banco))
                    throw new DuplicateNameException("J� existe um usu�rio API com o  mesmo nome de usu�rio.");

                retorno = dao.Salvar(entity);

                if (!string.IsNullOrEmpty(entity.uap_password) && !entity.IsNew)
                {
                    retorno &= AtualizaSenha(entity, dao._Banco);
                }

                return retorno;
            }
            catch (Exception ex)
            {
                dao._Banco.Close(ex);
                throw ex;
            }
            finally
            {
                if (dao._Banco.ConnectionIsOpen)
                    dao._Banco.Close();
            }
        }

        #endregion
    }
}