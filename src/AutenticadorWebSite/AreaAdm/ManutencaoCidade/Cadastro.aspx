<%@ Page Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true" Inherits="AreaAdm_ManutencaoCidade_Cadastro" Codebehind="Cadastro.aspx.cs" %>

<%@ PreviousPageType VirtualPath="~/AreaAdm/ManutencaoCidade/Busca.aspx" %>

<%@ Register Src="~/WebControls/Combos/UCComboPais.ascx" TagName="UCComboPais" TagPrefix="uc1" %>
<%@ Register Src="~/WebControls/Combos/UCComboUnidadeFederativa.ascx" TagName="UCComboUnidadeFederativa" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="_updCidades" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="vlgPais" />
            <asp:Label ID="_lblMessage" runat="server" EnableViewState="False"></asp:Label>
            <fieldset>
                <legend>Cadastro de cidade</legend>
                <uc1:UCComboPais ID="UCComboPais" runat="server" />
                <uc2:UCComboUnidadeFederativa ID="UCComboUnidadeFederativa" runat="server" />
                <asp:Label ID="LabelCidade" runat="server" Text="Cidade *" AssociatedControlID="_txtCidade"></asp:Label>
                <asp:TextBox ID="_txtCidade" runat="server" MaxLength="200" SkinID="text30C"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCidade" runat="server" ControlToValidate="_txtCidade"
                    ErrorMessage="Cidade é obrigatório." ValidationGroup="vlgPais">*</asp:RequiredFieldValidator>
                <asp:Label ID="LabelDDD" runat="server" Text="DDD" AssociatedControlID="_txtDDD"></asp:Label>
                <asp:TextBox ID="_txtDDD" runat="server" MaxLength="3" CssClass="numeric" SkinID="Numerico"></asp:TextBox>
                <asp:RegularExpressionValidator ID="_revDDD" runat="server" ControlToValidate="_txtDDD"
                    ValidationGroup="vlgPais" Display="Dynamic" ErrorMessage="Quantidade inválida."
                    ValidationExpression="^([0-9]){1,10}$">*</asp:RegularExpressionValidator>
                <div class="right">
                    <asp:Button ID="_btnSalvar" runat="server" Text="Salvar" OnClick="_btnSalvar_Click" ValidationGroup="vlgPais" />
                    <asp:Button ID="_btnCancelar" runat="server" Text="Cancelar" CausesValidation="False" OnClick="_btnCancelar_Click" />
                </div>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>