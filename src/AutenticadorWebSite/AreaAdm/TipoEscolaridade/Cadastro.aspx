<%@ Page Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true" Inherits="AreaAdm_TipoEscolaridade_Cadastro" Codebehind="Cadastro.aspx.cs" %>
<%@ PreviousPageType VirtualPath="~/AreaAdm/TipoEscolaridade/Busca.aspx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="_lblMessage" runat="server" EnableViewState="False"></asp:Label>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    <fieldset>
        <legend>Cadastro de tipo de escolaridade</legend>
        <asp:Label ID="_lblTipoEscolaridade" runat="server" Text="Tipo de escolaridade *" AssociatedControlID="_txtTipoEscolaridade"></asp:Label>
        <asp:TextBox ID="_txtTipoEscolaridade" runat="server" SkinID="text60C" MaxLength="100"></asp:TextBox>
        <asp:RequiredFieldValidator ID="_rfvTipoEscolaridade" runat="server" ControlToValidate="_txtTipoEscolaridade" ErrorMessage="Tipo de escolaridade é obrigatório." Display="Dynamic">*</asp:RequiredFieldValidator>        
        <asp:CheckBox ID="_ckbBloqueado" runat="server" text= "Bloqueado"/>
        <div class="right">
            <asp:Button ID="_btnSalvar" runat="server" Text="Salvar" OnClick="_btnSalvar_Click" />
            <asp:Button ID="_btnCancelar" runat="server" Text="Cancelar" OnClick="_btnCancelar_Click" CausesValidation="false" />
        </div>
    </fieldset>
</asp:Content>