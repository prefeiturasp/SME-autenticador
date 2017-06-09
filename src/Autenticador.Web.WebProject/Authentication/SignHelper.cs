using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Autenticador.BLL;
using Autenticador.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Configuration;


namespace Autenticador.Web.WebProject.Authentication
{


    public static class SignHelper
    {


        public static void AutenticarUsuarioDaRespostaDoSaml(string nameIdValue)
        {
            var dadosUsuario = nameIdValue.Split('\\');

            Guid entidadeId;
            string login;

            if (dadosUsuario.Length == 2)
            {
                entidadeId = Guid.Parse(dadosUsuario[0]);
                login = dadosUsuario[1];

                var usuarioCore = new SYS_Usuario { ent_id = entidadeId, usu_login = login };

                if (SYS_UsuarioBO.GetSelectBy_ent_id_usu_login(usuarioCore))
                {
                    AutenticarUsuario(usuarioCore);
                }
                else
                {
                    throw new Exception("Usuário(Saml) não encontrado");
                }
            }
            else
            {
                throw new Exception("Nome de Usuário inválido! Usuario(saml): " + nameIdValue);
            }
        }

        public static void AutenticarUsuario(SYS_Usuario usuarioCore, SYS_Grupo entityGrupo = null)
        {
            var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;

            var claims = GetClaims(usuarioCore, entityGrupo);

            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            authenticationManager.SignIn(new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = false,
                ExpiresUtc = ConfigCustomAutenticadorCookieAuthentication.ExpiresUtc
            }, identity);
        }

        public static SYS_Usuario ObterUsuarioDoClaimsIdentity()
        {
            SYS_Usuario usuarioCore = null;
            var identity = HttpContext.Current.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var usuarioIdClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                usuarioCore = new SYS_Usuario
                {
                    usu_id = Guid.Parse(usuarioIdClaim.Value)
                };

                SYS_UsuarioBO.GetEntity(usuarioCore);
            }
            return usuarioCore;
        }

        public static SYS_Grupo ObterGrupoDoUsuarioDoClaimsIdentity()
        {
            SYS_Grupo grupo = null;
            var identity = HttpContext.Current.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var grupoIdClaim = identity.FindFirst(AutenticadorCustomClaimTypes.GrupoId);

                if (grupoIdClaim != null && !string.IsNullOrWhiteSpace(grupoIdClaim.Value))
                {
                    grupo = new SYS_Grupo
                    {
                        gru_id = Guid.Parse(grupoIdClaim.Value)
                    };

                    grupo = SYS_GrupoBO.GetEntity(grupo);
                }
            }

            return grupo;
        }

        public static string FormatarUserNameDoCookie(SYS_Usuario usuarioCore)
        {
            return FormatarUserNameDoCookie(usuarioCore.ent_id.ToString(), usuarioCore.usu_login);
        }

        public static string FormatarUserNameDoCookie(string entId, string login)
        {
            return string.Concat(entId, "\\", login);
        }

        public static string FormatarUserNameDoCookie(string entId, string login, string grupoId)
        {
            string userName = FormatarUserNameDoCookie(entId, login);

            if (!string.IsNullOrWhiteSpace(grupoId))
            {
                userName = string.Concat(userName, "\\", grupoId);
            }

            return userName;
        }

        private static IList<Claim> GetClaims(SYS_Usuario usuarioCore, SYS_Grupo entityGrupo = null)
        {
            string roles = "";
            string grupoId = null;

            if (entityGrupo != null)
            {
                SYS_Sistema entitySistema = new SYS_Sistema
                {
                    sis_id = entityGrupo.sis_id
                };
                grupoId = entityGrupo.gru_id.ToString();

                SYS_SistemaBO.GetEntity(entitySistema);

                if (entitySistema.sis_tipoAutenticacao == 1)
                {
                    SYS_Visao entityVisao = new SYS_Visao
                    {
                        vis_id = entityGrupo.vis_id
                    };
                    SYS_VisaoBO.GetEntity(entityVisao);

                    roles = entityVisao.vis_nome;
                }
            }

            string name = FormatarUserNameDoCookie(usuarioCore.ent_id.ToString(), usuarioCore.usu_login, grupoId);

            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, usuarioCore.usu_id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, name));
            claims.Add(new Claim(ClaimTypes.Email, usuarioCore.usu_email ?? string.Empty));
            claims.Add(new Claim(ClaimTypes.Role, roles));
            claims.Add(new Claim(AutenticadorCustomClaimTypes.EntidadeId, usuarioCore.ent_id.ToString()));
            claims.Add(new Claim(AutenticadorCustomClaimTypes.GrupoId, grupoId ?? string.Empty));

            return claims;
        }

        public static void SignOut()
        {
            try
            {
                HttpContext.Current.GetOwinContext().Authentication
                    .SignOut(DefaultAuthenticationTypes.ApplicationCookie,
                             DefaultAuthenticationTypes.ExternalCookie);
            }
            catch (ThreadAbortException)
            {
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }

        public static void RedirectToLoginPage(string extraQueryString = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ConfigCustomAutenticadorCookieAuthentication.RedirectUrlLoginExternal))
                {
                    HttpContext.Current.Response.Redirect("~" + ConfigCustomAutenticadorCookieAuthentication.LoginPath + (!string.IsNullOrWhiteSpace(extraQueryString) ? "?" + extraQueryString : ""));
                }
                else
                {
                    HttpContext.Current.Response.Redirect(ConfigCustomAutenticadorCookieAuthentication.RedirectUrlLoginExternal);
                }
            }
            catch (ThreadAbortException)
            {
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }



    }
}