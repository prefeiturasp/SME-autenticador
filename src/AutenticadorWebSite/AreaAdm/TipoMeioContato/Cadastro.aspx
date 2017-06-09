<%@ Page Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true" Inherits="AreaAdm_TipoMeioContato_Cadastro" Title="Untitled Page" Codebehind="Cadastro.aspx.cs" %>

<%@ PreviousPageType VirtualPath="~/AreaAdm/TipoMeioContato/Busca.aspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="_lblMessage" runat="server" EnableViewState="False"></asp:Label>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    <fieldset>
        <legend>Cadastro de tipo de meio de contato</legend>
        <asp:Label ID="lblTipoMeioContato" runat="server" Text="Tipo de meio de contato *"
            AssociatedControlID="_txtTipoMeioContato"></asp:Label>
        <asp:TextBox ID="_txtTipoMeioContato" runat="server" SkinID="text60C" MaxLength="100"></asp:TextBox>
        <asp:RequiredFieldValidator ID="_rfvTipoMeioContato" runat="server" Display="Dynamic"
            ControlToValidate="_txtTipoMeioContato" ErrorMessage="Tipo de meio de contato é obrigatório.">*</asp:RequiredFieldValidator>
        <asp:Label ID="lblValidacao" runat="server" Text="Validação" AssociatedControlID="_ddlValidacao"></asp:Label>
        <asp:DropDownList ID="_ddlValidacao" runat="server" SkinID="text30C">
            <asp:ListItem Value="0">-- Selecione uma validação --</asp:ListItem>
            <asp:ListItem Value="1">E-mail</asp:ListItem>
            <asp:ListItem Value="2">Web site</asp:ListItem>
            <asp:ListItem Value="3">Telefone</asp:ListItem>
        </asp:DropDownList>
        <asp:CheckBox ID="_ckbBloqueado" Text="Bloqueado" runat="server" />
        <div class="right">
            <asp:Button ID="_btnSalvar" runat="server" Text="Salvar" OnClick="_btnSalvar_Click" />
            <asp:Button ID="_btnCancelar" runat="server" Text="Cancelar" OnClick="_btnCancelar_Click"
                CausesValidation="false" /></div>
    </fieldset>
</asp:Content>
