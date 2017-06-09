<%@ Page Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true" Inherits="AreaAdm_ManutencaoUnidadeFederativa_Cadastro" Codebehind="Cadastro.aspx.cs" %>

<%@ PreviousPageType VirtualPath="~/AreaAdm/ManutencaoUnidadeFederativa/Busca.aspx" %>

<%@ Register Src="~/WebControls/Combos/UCComboPais.ascx" TagName="UCComboPais" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="_updUnidadeFederativa" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="vlgUnidadeFederativa" />
            <asp:Label ID="_lblMessage" runat="server" EnableViewState="False"></asp:Label>
            <fieldset>
                <legend>Cadastro de unidade federativa</legend>
                <uc1:UCComboPais ID="UCComboPais" runat="server" />               
                <asp:Label ID="LabelUnidadeFederativa" runat="server" Text="Unidade federativa *" AssociatedControlID="_txtUnidadeFederativa"></asp:Label>
                <asp:TextBox ID="_txtUnidadeFederativa" runat="server" MaxLength="200" SkinID="text30C"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvUnidadeFederativa" runat="server" ControlToValidate="_txtUnidadeFederativa"
                    ErrorMessage="Unidade federativa é obrigatório." ValidationGroup="vlgUnidadeFederativa">*</asp:RequiredFieldValidator>
                <asp:Label ID="LabelSigla" runat="server" Text="Sigla *" AssociatedControlID="_txtSigla"></asp:Label>
                <asp:TextBox ID="_txtSigla" runat="server" MaxLength="2" SkinID="text10C"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvSigla" runat="server" ControlToValidate="_txtSigla"
                    ErrorMessage="Sigla é obrigatório." ValidationGroup="vlgUnidadeFederativa">*</asp:RequiredFieldValidator>
                <div class="right">
                    <asp:Button ID="_btnSalvar" runat="server" Text="Salvar" OnClick="_btnSalvar_Click" ValidationGroup="vlgUnidadeFederativa" />
                    <asp:Button ID="_btnCancelar" runat="server" Text="Cancelar" CausesValidation="False" OnClick="_btnCancelar_Click" />
                </div>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>