/*
	Classe gerada automaticamente pelo Code Creator
*/

namespace Autenticador.BLL
{
	using CoreLibrary.Business.Common;
	using Autenticador.Entities;
	using Autenticador.DAL;
    using CoreLibrary.Validation.Exceptions;
	
	/// <summary>
	/// Description: LOG_UsuarioAPI Business Object. 
	/// </summary>
	public class LOG_UsuarioAPIBO : BusinessBase<LOG_UsuarioAPIDAO, LOG_UsuarioAPI>
    {
        #region Enumeradores

        /// <summary>
        /// Enumerador de a��es realizadas pela API.
        /// </summary>
        public enum eAcao
        {
            AlteracaoSenha = 1,
            AlteracaoLogin = 2,
            CriacaoUsuario = 3,
            AlteracaoUsuario = 4,
            DelecaoUsuario = 5,
            AssociacaoUsuarioGrupo = 6 
        }

        #endregion

        #region Salvar/Alterar

        /// <summary>
        /// Salva o log da API.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static new bool Save(LOG_UsuarioAPI entity)
        {
            if (entity.Validate())
                return new LOG_UsuarioAPIDAO().Salvar(entity);

            throw new ValidationException(UtilBO.ErrosValidacao(entity)); 
        }

        #endregion
    }
}