﻿<%@ Page Language="C#" MasterPageFile="~/MasterPageLogin.master" AutoEventWireup="true" Inherits="Manutencao" Title="Untitled Page" Codebehind="Manutencao.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="text-align:center;">        
        <br /><br /><br />
        <br /><br /><br />
        <br /><br /><br />
        <br /><br /><br />
        <br /><br /><br />
        <br /><br /><br />
        <br /><br /><br />
        <asp:Image ID="imgErro" runat="server" ImageAlign="Middle" Width="35" Height="35" />
        &nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblMensagem" Font-Size="Large" runat="server" Text="Ocorreu um erro inesperado. Por favor, tente novamente. Se o problema persistir, avise a equipe de suporte e apoio da ferramenta."></asp:Label>
        <br />
        <asp:Label ID="lblDataErro" runat="server" Font-Size="Large"></asp:Label>
        <br /><br /><br />
        <a href="Sistema.aspx" title="Clique aqui para voltar para a página inicial do sistema.">Voltar para página inicial</a>
        <br /><br />
        <a href="Logout.ashx" title="Clique aqui para voltar para a página de login do sistema.">Voltar para página de login</a>
        <br /><br /><br />
        <br /><br /><br />
        <br /><br /><br />
    </fieldset>
</asp:Content>
