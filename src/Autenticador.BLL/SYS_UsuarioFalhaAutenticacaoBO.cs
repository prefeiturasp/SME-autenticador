/*
	Classe gerada automaticamente pelo Code Creator
*/

using System;
using CoreLibrary.Business.Common;
using Autenticador.Entities;
using Autenticador.DAL;

namespace Autenticador.BLL
{
	
	/// <summary>
	/// SYS_UsuarioFalhaAutenticacao Business Object 
	/// </summary>
	public class SYS_UsuarioFalhaAutenticacaoBO : BusinessBase<SYS_UsuarioFalhaAutenticacaoDAO,SYS_UsuarioFalhaAutenticacao>
	{
        /// <summary>
        /// Zera a quantidade de falhas que o usu�rio teve, para n�o exibir mais o captcha.
        /// Utilizada ap�s o usu�rio efetuar um login com sucesso.
        /// </summary>
        /// <param name="usu_id">ID do usu�rio que efetuou login com sucesso</param>
        /// <returns></returns>
        public static bool ZeraFalhaAutenticacaoUsuario(Guid usu_id)
        {
            SYS_UsuarioFalhaAutenticacaoDAO dao = new SYS_UsuarioFalhaAutenticacaoDAO();

            return dao.ZeraFalhaAutenticacaoUsuario(usu_id);
        }

        /// <summary>
        /// Insere um registro de falha de autentica��o para o usu�rio, ou incrementa 1
        /// no contador de erros caso o usu�rio j� tenha errado, no intervalo de minutos do par�metro.
        /// Caso o �ltimo erro do usu�rio tenha sido depois desse intervalo, reinicia o contador pra 1.
        /// Retorna o registro do usu�rio com a quantidade erros efetuada.
        /// </summary>
        /// <param name="usu_id">ID do usu�rio que efetuou o login com falha</param>
        /// <returns></returns>
		public static SYS_UsuarioFalhaAutenticacao InsereFalhaAutenticacaoUsuario(Guid usu_id)
		{
		    SYS_UsuarioFalhaAutenticacaoDAO dao = new SYS_UsuarioFalhaAutenticacaoDAO();

		    return dao.InsereFalhaAutenticacaoUsuario(usu_id, SYS_ParametroBO.Parametro_IntervaloMinutosFalhaAutenticacao());
		}
	}
}