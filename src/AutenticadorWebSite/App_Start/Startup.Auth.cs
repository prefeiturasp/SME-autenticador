using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Google;
using Autenticador.BLL;
using Autenticador.Entities;
using Autenticador.Web.WebProject.Authentication;
using System.Collections.Generic;
using Owin;
using System;
using System.Configuration;
using System.Globalization;
using Quartz;

namespace AutenticadorWebSite
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new AutenticadorCookieAuthenticationOptions());

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

        }
    }
}