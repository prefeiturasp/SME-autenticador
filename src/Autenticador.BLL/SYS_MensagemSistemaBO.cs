/*
	Classe gerada automaticamente pelo Code Creator
*/

using System.Web.Caching;
using CoreLibrary.Business.Common;
using Autenticador.Entities;
using Autenticador.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Autenticador.BLL
{
    #region Enumerador

    /// <summary>
    /// Chaves da mensagem do sistema.
    /// </summary>
    public enum SYS_MensagemSistemaChave
    {
        Login = 1
        , MeusDadosMensagemEmail = 2
        , MeusDadosMensagemEmailInvalido = 3
        , MeusDadosMensagemComplexidadeNovaSenha = 4
        , MeusDadosMensagemSenhaAtualIncorreta = 5
        , MeusDadosMensagemConfirmarSenhaNaoIdentico = 6
        , MeusDadosMensagemValidacaoComplexidadeSenhaFormato = 7
        , MeusDadosMensagemValidacaoComplexidadeSenhaTamanho = 8
        , MeusDadosMensagemValidacaoHistoricoSenha = 9
        , MeusDadosMensagemValidacaoEmailExistente = 10
        , EsqueciSenhaMensagemInformacaoEmail = 11
        , EsqueciSenhaCorpoEmailRecuperacaoSenha = 12
        , EsqueciSenhaAssuntoEmailRecuperacaoSenha = 13
        , MeusDadosMensagemConfirmarcaoAlteracaoSenhaEmail = 14
        , MeusDadosMensagemConfirmarcaoAlteracaoSenha = 15
        , MeusDadosMensagemSenhaAtualSenhaNovaDiferenca = 16
        , LoginMensagemSenhaAlteradaSucesso = 17
        , LoginMensagemSenhaEmailEnviadaSucesso = 18
        , AlteracaoUsuarioCorpoEmailNovaSenha = 19
        , CriacaoUsuarioCorpoEmailNovaSenha = 20
        , AlteracaoUsuarioAssuntoEmailNovaSenha = 21
        , CriacaoUsuarioAssuntoEmailNovaSenha = 22
        , LoginMensagemUsuarioNaoEncontrado = 23
    }

    /// <summary>
    /// Situa��o do registro.
    /// </summary>
    public enum SYS_MensagemSistemaSituacao : byte
    {
        Ativo = 1
        , Excluido = 3
        , Interno = 4
    }

    #endregion

	/// <summary>
	/// SYS_MensagemSistema Business Object 
	/// </summary>
	public class SYS_MensagemSistemaBO : BusinessBase<SYS_MensagemSistemaDAO,SYS_MensagemSistema>
	{
        #region Propriedades

        /// <summary>
        /// Retorna as mensagens do sistema.
        /// </summary>
        private static IDictionary<SYS_MensagemSistemaChave, string[]> Parametros
        {
            get
            {
                IDictionary<SYS_MensagemSistemaChave, string[]> parametros;

                SelecionaParametrosAtivos(out parametros);

                return parametros;
            }
        }

        #endregion
        /// <summary>
        /// Retorna as mensagens ativas do sistema.
        /// </summary>
        private static void SelecionaParametrosAtivos(out IDictionary<SYS_MensagemSistemaChave, string[]> dictionary)
        {
            IList<SYS_MensagemSistema> lt = GetSelect();

            dictionary = (from SYS_MensagemSistema ent in lt
                          where Enum.IsDefined(typeof(SYS_MensagemSistemaChave), ent.mss_chave)
                                && ent.mss_situacao == (byte)SYS_MensagemSistemaSituacao.Ativo
                          group ent by ent.mss_chave into t
                          select new
                          {
                              chave = t.Key
                              ,
                              valor = t.Select(p => p.mss_valor).ToArray()
                          }).ToDictionary(
                                p => (SYS_MensagemSistemaChave)Enum.Parse(typeof(SYS_MensagemSistemaChave), p.chave)
                                , p => p.valor);
        }

        /// <summary>
        /// Seleciona o valor da mensagem filtrado pela chave.
        /// </summary>
        /// <param name="mss_chave">Enum que representa a chave a ser pesquisada.</param>        
        /// <returns>O valor do par�metro (mss_valor).</returns>
        public static string RetornaValor
        (
            SYS_MensagemSistemaChave mss_chave
        )
        {
            IDictionary<SYS_MensagemSistemaChave, string[]> parametros = Parametros;
            string valor = "";
            if (parametros.ContainsKey(mss_chave))
                valor = parametros[mss_chave].FirstOrDefault();

            return valor;
        }
	}
}