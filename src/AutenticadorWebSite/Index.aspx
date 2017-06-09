<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Index" Codebehind="Index.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMasterLevel0" Runat="Server">
   <asp:Label ID="_lblMessage" runat="server" EnableViewState="False"></asp:Label>
   <div ID="_lblSiteMap" runat="server"></div>
</asp:Content>

