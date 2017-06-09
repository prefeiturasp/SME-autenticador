using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.MetadirectoryServices;
using Autenticador.BLL;
using Autenticador.Entities;

namespace Autenticador.FIM.PasswordManagement
{
    public class PasswordManagement : IMAPasswordManagement
    {

        public void BeginConnectionToServer(string connectTo, string user, string password)
        {
        }

        public void ChangePassword(CSEntry csentry, string OldPassword, string NewPassword)
        {
        }

        public void EndConnectionToServer()
        {
        }

        public ConnectionSecurityLevel GetConnectionSecurityLevel()
        {
            return ConnectionSecurityLevel.NotSecure;
        }

        public void RequireChangePasswordOnNextLogin(CSEntry csentry, bool fRequireChangePasswordOnNextLogin)
        {
        }

        public void SetPassword(CSEntry csentry, string NewPassword)
        {
            List<SYS_Usuario> ltUsuario = SYS_UsuarioBO.SelecionaPorLogin(csentry.DN.ToString());

            foreach (SYS_Usuario usuario in ltUsuario)
            {
                try
                {
                    // Configura criptografia da senha
                    eCriptografa criptografia = (eCriptografa)Enum.Parse(typeof(eCriptografa), Convert.ToString(usuario.usu_criptografia), true);
                    if (!Enum.IsDefined(typeof(eCriptografa), criptografia))
                        criptografia = eCriptografa.TripleDES;
                    string novaSenhaCript = UtilBO.CriptografarSenha(NewPassword, criptografia);

                    if (!novaSenhaCript.Equals(usuario.usu_senha))
                    {
                        LOG_UsuarioAD logUsuario = new LOG_UsuarioAD
                        {
                            usu_id = usuario.usu_id
                            ,
                            usa_acao = (short)LOG_UsuarioAD.eAcao.AlterarSenha
                            ,
                            usa_status = (short)LOG_UsuarioAD.eStatus.Pendente
                            ,
                            usa_dataAcao = DateTime.Now
                            ,
                            usa_origemAcao = (short)LOG_UsuarioAD.eOrigem.AD
                            ,
                            usa_dados = LOG_UsuarioADBO.GetDadosUsuarioAD(usuario, NewPassword)
                        };

                        LOG_UsuarioADBO.Save(logUsuario);
                    }
                }
                catch (Exception ex)
                {
                    UtilBO.GravarErro(ex);
                }
            }
        }
    }
}
