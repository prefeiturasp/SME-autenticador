<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCPluginNotificacao.ascx.cs" Inherits="Autenticador.WebControls.PluginNotificacao.UCPluginNotificacao" %>

<script type="text/javascript"> 
    config = JSON.parse('<%= GetConfig %>');

    new plgnotify(
        {
            url: config.urlNotificationAPI,
            userId: config.userId,                    
            tokenType: 'Bearer ',
            token: config.idsToken,
            ws: {
                url: config.urlNotificationSignalR
            }
        }
    );

</script>