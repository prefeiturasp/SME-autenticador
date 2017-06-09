<%@ Page Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true" Inherits="AreaAdm_TipoDeficiencia_Cadastro" Title="Untitled Page" Codebehind="Cadastro.aspx.cs" %>

<%@ PreviousPageType VirtualPath="~/AreaAdm/TipoDeficiencia/Busca.aspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="_lblMessage" runat="server" EnableViewState="False"></asp:Label>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    <fieldset>
        <legend>Cadastro de tipo de deficiência</legend>
        <asp:Label ID="_lblTipoDeficiencia" runat="server" Text="Tipo de deficiência *" AssociatedControlID="_txtTipoDeficiencia"></asp:Label>
        <asp:TextBox ID="_txtTipoDeficiencia" runat="server" SkinID="text60C" MaxLength="100"></asp:TextBox>
        <asp:RequiredFieldValidator ID="_rfvNome" runat="server" ControlToValidate="_txtTipoDeficiencia"
            Display="Dynamic" ErrorMessage="Tipo de deficiência é obrigatório.">*</asp:RequiredFieldValidator>
        <asp:CheckBox ID="_ckbBloqueado" Text="Bloqueado" runat="server" Visible="false" />
        <div class="right">
            <asp:Button ID="_btnSalvar" runat="server" Text="Salvar" OnClick="_btnSalvar_Click" />
            <asp:Button ID="_btnCancelar" runat="server" Text="Cancelar" OnClick="_btnCancelar_Click"
                CausesValidation="false" />
        </div>
    </fieldset>
</asp:Content>
