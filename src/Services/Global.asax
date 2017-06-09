<%@ Application Language="C#" Inherits="MSTech.CoreSSO.Web.WebProject.ApplicationWEB" %>
<%@ Import Namespace="MSTech.CoreSSO.BLL" %>

<script RunAt="server">

    protected override void Application_Start(object sender, EventArgs e)
    {
        base.Application_Start(sender, e);
        LOG_SistemaBO.SalvarLog = new LOG_SistemaBO.DelSalvarLog(_GravaLogSistema);
    }

    protected void Application_Error(object sender, EventArgs e)
    {
        _GravaErro(Server.GetLastError());
    }

</script>