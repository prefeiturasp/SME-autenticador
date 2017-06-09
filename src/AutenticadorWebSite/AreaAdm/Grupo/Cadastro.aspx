<%@ Page Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true" Inherits="AreaAdm_Grupo_Cadastro" Title="Untitled Page" Codebehind="Cadastro.aspx.cs" %>

<%@ PreviousPageType VirtualPath="~/AreaAdm/Grupo/Busca.aspx" %>
<%@ Register Src="../../WebControls/Combos/UCComboSistema.ascx" TagName="UCComboSistemas"
    TagPrefix="uc1" %>
<%@ Register Src="../../WebControls/Combos/UCComboVisao.ascx" TagName="UCComboVisao"
    TagPrefix="uc2" %>
<%@ Register Src="../../WebControls/Combos/UCComboGrupo.ascx" TagName="UCComboGrupo"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="_updGrupos" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Label ID="_lblMessage" runat="server" EnableViewState="False"></asp:Label>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
            <fieldset>
                <legend>Cadastro de grupos</legend>
                <uc1:UCComboSistemas ID="UCComboSistemas1" runat="server" />
                <asp:Label ID="lblNomeGrupo" runat="server" Text="Nome do grupo *" EnableViewState="False"
                    AssociatedControlID="_txtNome"></asp:Label>
                <asp:TextBox ID="_txtNome" runat="server" Columns="50" MaxLength="50" SkinID="text60C"></asp:TextBox>
                <asp:RequiredFieldValidator ID="_rfvNome" runat="server" ControlToValidate="_txtNome"
                    ErrorMessage="Nome do grupo é obrigatório." Display="Dynamic">*</asp:RequiredFieldValidator>
                <uc2:UCComboVisao ID="UCComboVisao1" runat="server" />
                <asp:CheckBox ID="_chkBloqueado" Text="Bloqueado" runat="server" />
                <uc3:UCComboGrupo ID="UCComboGrupo1" runat="server" />
                <uc3:UCComboGrupo ID="UCComboGrupo2" runat="server" />
                <div class="right">
                    <asp:Button ID="_btnSalvar" runat="server" Text="Salvar" Width="100px" OnClick="_btnSalvar_Click" />
                    <asp:Button ID="_btnCancelar" runat="server" Text="Cancelar" Width="100px" CausesValidation="False"
                        OnClick="_btnCancelar_Click" />
                </div>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
