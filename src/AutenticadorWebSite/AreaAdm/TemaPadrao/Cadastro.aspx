<%@ Page Title="" Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="AutenticadorWebSite.AreaAdm.TemaPadrao.Cadastro" %>

<%@ PreviousPageType VirtualPath="~/AreaAdm/TemaPadrao/Busca.aspx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblMensagem" runat="server" EnableViewState="false"></asp:Label>
    <asp:ValidationSummary ID="vsTema" runat="server" ValidationGroup="Tema" />
    <asp:Panel ID="pnlCadastroTema" runat="server" GroupingText="Cadastro de tema padrão">
        <asp:Label ID="lblNome" runat="server" Text="Nome" AssociatedControlID="txtNome"></asp:Label>
        <asp:TextBox ID="txtNome" runat="server" SkinID="text60C" MaxLength="100"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvNome" runat="server" ControlToValidate="txtNome" Display="Dynamic" ValidationGroup="Tema" ErrorMessage="Nome é obrigatório.">*</asp:RequiredFieldValidator>

        <asp:Label ID="lblDescricao" runat="server" Text="Descricao" AssociatedControlID="txtDescricao"></asp:Label>
        <asp:TextBox ID="txtDescricao" runat="server" SkinID="text60C" MaxLength="200"></asp:TextBox>

        <asp:Label ID="lblTipoMenu" runat="server" Text="Tipo de menu" AssociatedControlID="ddlTipoMenu"></asp:Label>
        <asp:DropDownList ID="ddlTipoMenu" runat="server" SkinID="text30C"></asp:DropDownList>
        <asp:CompareValidator ID="cpvTipoMenu" runat="server" ControlToValidate="ddlTipoMenu" Display="Dynamic" ValueToCompare="0" Operator="GreaterThan" ValidationGroup="Tema"
            ErrorMessage="Tipo de menu é obrigatório.">*</asp:CompareValidator>

        <asp:Label ID="lblTipoLogin" runat="server" Text="Tipo de login" AssociatedControlID="ddlTipoLogin"></asp:Label>
        <asp:DropDownList ID="ddlTipoLogin" runat="server" SkinID="text60C"></asp:DropDownList>
        <asp:CompareValidator ID="cpvTipoLogin" runat="server" ControlToValidate="ddlTipoLogin" Display="Dynamic" ValueToCompare="0" Operator="GreaterThan" ValidationGroup="Tema"
            ErrorMessage="Tipo de login é obrigatório.">*</asp:CompareValidator>

        <asp:CheckBox ID="chkExibeLinkLogin" runat="server" Text="Exibe link para site" />
        <asp:CheckBox ID="chkExibeLogoCliente" runat="server" Text="Exibe logo do cliente" />

        <div class="right">
            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" ValidationGroup="Tema" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
        </div>
    </asp:Panel>
</asp:Content>
