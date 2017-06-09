<%@ Application Inherits="Autenticador.Web.WebProject.ApplicationWEB" Language="C#" %>
<%@ Import Namespace="Autenticador.BLL" %>

<script RunAt="server">

    protected override void Application_Start(object sender, EventArgs e)
    {
        base.Application_Start(sender, e);
        LOG_SistemaBO.SalvarLog = new LOG_SistemaBO.DelSalvarLog(_GravaLogSistema);
        HttpContext.Current.Application["eConfig"] = "CoreSSO";

    }

    protected void Application_Error(object sender, EventArgs e)
    {
        _GravaErro(Server.GetLastError());
    }
</script>