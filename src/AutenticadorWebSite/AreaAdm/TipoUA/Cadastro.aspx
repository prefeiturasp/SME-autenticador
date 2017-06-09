<%@ Page Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true" Inherits="AreaAdm_TipoUA_Cadastro" Title="Untitled Page" Codebehind="Cadastro.aspx.cs" %>

<%@ PreviousPageType VirtualPath="~/AreaAdm/TipoUA/Busca.aspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="_lblMessage" runat="server" EnableViewState="False"></asp:Label>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    <fieldset>
        <legend>Cadastro de tipo de unidade administrativa</legend>
        <asp:Label ID="_lblTipoUA" runat="server" Text="Tipo de unidade administrativa *" AssociatedControlID="_txtTipoUA"></asp:Label>
        <asp:TextBox ID="_txtTipoUA" runat="server" SkinID="text60C" MaxLength="100"></asp:TextBox>
        <asp:RequiredFieldValidator ID="_rfvTipoUA" runat="server" ControlToValidate="_txtTipoUA" ErrorMessage="Tipo de unidade administrativa é obrigatório." Display="Dynamic">*</asp:RequiredFieldValidator>
        <asp:CheckBox ID="_ckbBloqueado" Text="Bloqueado" runat="server" />
        <div class="right">
            <asp:Button ID="_btnSalvar" runat="server" Text="Salvar" OnClick="_btnSalvar_Click" />
            <asp:Button ID="_btnCancelar" runat="server" Text="Cancelar" CausesValidation="false" OnClick="_btnCancelar_Click" />
        </div>
    </fieldset>
</asp:Content>
