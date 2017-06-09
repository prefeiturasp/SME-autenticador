<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/MasterPageMeusDados.master" Inherits="MeusDados" Codebehind="MeusDados.aspx.cs" %>

<%@ Register Src="~/WebControls/MeusDados/UCMeusDados.ascx" TagName="UCMeusDados" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:UCMeusDados ID="UCMeusDados1" runat="server"></uc1:UCMeusDados>
</asp:Content>
    
    