/*
	Classe gerada automaticamente pelo Code Creator
*/

namespace Autenticador.BLL
{
	using CoreLibrary.Business.Common;
	using Autenticador.Entities;
	using Autenticador.DAL;
    using CoreLibrary.Data.Common;
    using System.Collections.Generic;
    using System;
    using System.Data;
	
	/// <summary>
	/// Description: SYS_UsuarioSenhaHistorico Business Object. 
	/// </summary>
	public class SYS_UsuarioSenhaHistoricoBO : BusinessBase<SYS_UsuarioSenhaHistoricoDAO, SYS_UsuarioSenhaHistorico>
    {
        #region M�todos de consulta

        /// <summary>
        /// Seleciona as �ltimas senhas do usu�rio.
        /// </summary>
        /// <param name="usu_id">ID do usu�rio.</param>
        /// <param name="banco">Transa��o.</param>
        /// <returns></returns>
        public static List<SYS_UsuarioSenhaHistorico> SelecionaUltimasSenhas(Guid usu_id, TalkDBTransaction banco = null)
        {
            SYS_UsuarioSenhaHistoricoDAO dao = new SYS_UsuarioSenhaHistoricoDAO();

            if (banco == null)
            {
                dao._Banco.Open(IsolationLevel.ReadCommitted);
            }
            else
            {
                dao._Banco = banco;
            }

            try
            {
                return dao.SelecionaUltimasSenhas(usu_id, SYS_ParametroBO.ParametroValorInt32(SYS_ParametroBO.eChave.QUANTIDADE_ULTIMAS_SENHAS_VALIDACAO));
            }
            catch (Exception ex)
            {
                if (banco == null)
                {
                    dao._Banco.Close(ex);
                }
                throw;
            }
            finally
            {
                if (banco == null && dao._Banco.ConnectionIsOpen)
                {
                    dao._Banco.Close();
                }
            }
        }

        public static DataTable SelecionaUltimaSenha(Guid usu_id, TalkDBTransaction banco = null)
        {
            SYS_UsuarioSenhaHistoricoDAO dao = new SYS_UsuarioSenhaHistoricoDAO();

            if (banco == null)
            {
                dao._Banco.Open(IsolationLevel.ReadCommitted);
            }
            else
            {
                dao._Banco = banco;
            }
            DataTable usuarioSenhas = new DataTable();
            try
            {
                usuarioSenhas =  dao.SelecionaUltimaSenha(usu_id);
            }
            catch (Exception ex)
            {
                if (banco == null)
                {
                    dao._Banco.Close(ex);
                }
                throw;
            }
            finally
            {
                if (banco == null && dao._Banco.ConnectionIsOpen)
                {
                    dao._Banco.Close();
                }             
            }


            return usuarioSenhas;
        }

        #endregion M�todos de consulta

        #region M�todos de inclus�o/altera��o

        /// <summary>
        /// M�todo que salva a senha do usu�rio no hist�rico.
        /// </summary>
        /// <param name="entityUsuario">Entidade do usu�rio (senha criptografada).</param>
        /// <param name="banco">Transa��o.</param>
        /// <returns></returns>
        public static bool Salvar(SYS_Usuario entityUsuario, TalkDBTransaction banco)
        {
            SYS_UsuarioSenhaHistoricoDAO dao = new SYS_UsuarioSenhaHistoricoDAO { _Banco = banco };

            SYS_UsuarioSenhaHistorico entity = new SYS_UsuarioSenhaHistorico
            {
                usu_id = entityUsuario.usu_id
                ,
                ush_senha = entityUsuario.usu_senha
                ,
                ush_criptografia = entityUsuario.usu_criptografia
            };

            return dao.Salvar(entity);
        }

        #endregion M�todos de inclus�o/altera��o
    }
}