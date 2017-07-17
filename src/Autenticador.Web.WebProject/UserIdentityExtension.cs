using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Linq;

namespace Autenticador.Web.WebProjects
{
    public static class UserIdentityExtension
    {
        public static string GetEntityId(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst(ClaimTypes.PrimarySid);
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetUsuLogin(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst(ClaimTypes.Name);
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetGrupoId(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst(ClaimTypes.GroupSid);
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static void AddGrupoId(this IIdentity identity, HttpRequest request, string grupoId)
        {
            var identityUser = (ClaimsIdentity)identity;
            var claimGrupo = new Claim(ClaimTypes.GroupSid, grupoId);
            identityUser.AddClaim(claimGrupo);
            request.GetOwinContext().Authentication.SignIn(identityUser);
        }

        public static string GetSistemaLogged(this IIdentity identity, HttpRequest request)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("SistemasLogged");
            string ret = string.Empty;
            var value = (claim != null) ? claim.Value.Split(',') : null;
            if (value != null)
            {
                ret = value.First();

                var identityUser = (ClaimsIdentity)identity;
                value = value.Skip(1).ToArray();
                identityUser.RemoveClaim(claim);
                if (value.Count() > 0)
                {
                    var newValue = string.Join(",", value);
                    var claimSistemasLogged = new Claim("SistemasLogged", newValue);
                    identityUser.AddClaim(claimSistemasLogged);
                }
                request.GetOwinContext().Authentication.SignIn(identityUser);
            }
            return ret;
        }

        public static void AddSistemasLogged(this IIdentity identity, HttpRequest request, string sisId)
        {
            var identityUser = (ClaimsIdentity)identity;
            var claim = ((ClaimsIdentity)identity).FindFirst("SistemasLogged");
            string value = claim == null ? string.Empty : claim.Value;
            if (claim != null)
                identityUser.RemoveClaim(claim);
            value = string.IsNullOrEmpty(value) ? sisId : value + "," + sisId;
            var claimSistemasLogged = new Claim("SistemasLogged", value);
            identityUser.AddClaim(claimSistemasLogged);
            Microsoft.Owin.Security.AuthenticationProperties a = new Microsoft.Owin.Security.AuthenticationProperties();
            a.IsPersistent = true;
            request.GetOwinContext().Authentication.SignIn(a, identityUser);
        }
    }

}
