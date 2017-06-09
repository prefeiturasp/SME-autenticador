<%@ Page Title="" Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true" CodeBehind="ConfigurarServico.aspx.cs" Inherits="AutenticadorWebSite.AreaAdm.Configuracao.Servico.ConfigurarServico" %>

<%@ Register Src="~/WebControls/Combos/UCComboServico.ascx" TagName="UCComboServico" TagPrefix="uc1" %>
<%@ Register Src="~/WebControls/FrequenciaServico/UCFrequenciaServico.ascx" TagName="UCFrequenciaServico" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblMessage" runat="server" EnableViewState="false"></asp:Label>
    <asp:ValidationSummary ID="vsServico" runat="server" ValidationGroup="Servico" />
    <asp:Panel ID="pnlConfigServico" runat="server" GroupingText="Configuração de serviços">
        <uc1:UCComboServico ID="UCComboServico" runat="server" />
        <div id="divServico" runat="server" visible="false">
            <asp:CheckBox ID="chkDesativar" runat="server" Text="Desativar serviço" />
            <div id="divCampos" class="divCampos" runat="server">
                <uc2:UCFrequenciaServico ID="UCFrequenciaServico" runat="server" ValidationGroupUCFrequenciaServico="Servico" /><br />
                <asp:Label ID="lblUltimaExecucao" runat="server"></asp:Label><br />
                <asp:Label ID="lblProximaExecucao" runat="server"></asp:Label>
            </div>
            <div class="right">
                <asp:Button ID="btnSalvar" runat="server" Text="Salvar configurações" OnClick="btnSalvar_Click" ValidationGroup="Servico" />
            </div>
        </div>
    </asp:Panel>
</asp:Content>
