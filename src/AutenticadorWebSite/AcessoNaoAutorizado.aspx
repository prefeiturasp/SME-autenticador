<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageMeusDados.master" AutoEventWireup="true" CodeBehind="AcessoNaoAutorizado.aspx.cs" Inherits="AutenticadorWebSite.AcessoNaoAutorizado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="height: 65px;"></div>
    <fieldset style="text-align:center;">
        <legend>Acesso não autorizado</legend>
        <asp:Label ID="lblMensagem" runat="server" EnableViewState="False" Font-Bold="true" Text="Acesso não autorizado ao sistema. Deseja visualizar os sistemas ao qual você possui acesso?"></asp:Label>
        <br /><br />
        <span style="width:100px!important; text-align:left!important;">
            <asp:Button ID="btnSim" runat="server" Text="Sim" onclick="btnSim_Click" />
        </span>
        <span style="width:100px!important; text-align:right!important;">
            <asp:Button ID="btnNao" runat="server" Text="Não" onclick="btnNao_Click" />
        </span>
    </fieldset>
</asp:Content>
