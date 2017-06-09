<%@ Page Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true" Inherits='AreaAdm_Usuario_MeusDados' Title="Untitled Page" Codebehind="MeusDados.aspx.cs" %>

<%@ Register Src="~/WebControls/MeusDados/UCMeusDados.ascx" TagName="UCMeusDados" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:UCMeusDados ID="UCMeusDados1" runat="server" PageRedirectCancel="~/Index.aspx"></uc1:UCMeusDados>
</asp:Content>
