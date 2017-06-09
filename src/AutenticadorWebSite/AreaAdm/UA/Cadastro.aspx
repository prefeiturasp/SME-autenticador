<%@ Page Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true"
    Inherits="AreaAdm_UA_Cadastro" Title="Untitled Page" CodeBehind="Cadastro.aspx.cs" %>

<%@ PreviousPageType VirtualPath="~/AreaAdm/UA/Busca.aspx" %>
<%@ Register Src="~/WebControls/Combos/UCComboEntidade.ascx" TagName="UCComboEntidade"
    TagPrefix="uc1" %>
<%@ Register Src="~/WebControls/Combos/UCComboTipoUnidadeAdministrativa.ascx" TagName="UCComboTipoUnidadeAdministrativa"
    TagPrefix="uc2" %>
<%@ Register Src="~/WebControls/Contato/UCGridContato.ascx" TagName="UCGridContato"
    TagPrefix="uc8" %>
<%@ Register Src="~/WebControls/Endereco/UCEnderecos.ascx" TagName="UCEnderecos"
    TagPrefix="uc4" %>
<%@ Register Src="~/WebControls/Busca/UCUASuperior.ascx" TagName="UCUASuperior" TagPrefix="uc5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="_updCadastroUA" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:Label ID="_lblMessage" runat="server" EnableViewState="False"></asp:Label>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
            <fieldset>
                <legend>Cadastro de unidades administrativas</legend></td> </tr>
                <uc1:UCComboEntidade ID="UCComboEntidade1" runat="server" />
                <uc2:UCComboTipoUnidadeAdministrativa ID="UCComboTipoUnidadeAdministrativa1" runat="server" ValidationGroup="ValidationSummary1" />
                <asp:Label ID="LabelNome" runat="server" Text="Nome *" AssociatedControlID="_txtNome"></asp:Label>
                <asp:TextBox ID="_txtNome" runat="server" MaxLength="200" Width="480" ValidationGroup="ValidationSummary1"></asp:TextBox>
                <asp:RequiredFieldValidator ID="_rfvNome" runat="server" ControlToValidate="_txtNome"
                    Display="Dynamic" ErrorMessage="Nome é obrigatório.">*</asp:RequiredFieldValidator>
                <asp:Label ID="LabelSigla" runat="server" Text="Sigla" AssociatedControlID="_txtSigla"></asp:Label>
                <asp:TextBox ID="_txtSigla" runat="server" MaxLength="50" SkinID="text30C"></asp:TextBox>
                <asp:Label ID="LabelCodigo" runat="server" Text="Código" AssociatedControlID="_txtCodigo"></asp:Label>
                <asp:TextBox ID="_txtCodigo" runat="server" MaxLength="20" SkinID="text20C"></asp:TextBox>
                <asp:Label ID="LabelCodigoInep" runat="server" Text="Código do Inep" AssociatedControlID="_txtCodigoInep"></asp:Label>
                <asp:TextBox ID="_txtCodigoInep" runat="server" MaxLength="30" SkinID="text30C"></asp:TextBox>

                <asp:Panel ID="pnUASuperior" runat="server">
                    <asp:Label ID="LabelUA" runat="server" AssociatedControlID="_txtUad_nome" Text="Unidade administrativa superior"></asp:Label>
                    <asp:TextBox ID="_txtUad_nome" runat="server" Enabled="False" MaxLength="200" SkinID="text60C"></asp:TextBox>
                    <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="false" SkinID="btPesquisar" ToolTip="Pesquisar" OnClick="btnProcurarUASuperior_Click" />
                    <asp:ImageButton ID="btnLimpar" runat="server" CausesValidation="false" ToolTip="Limpar"
                        SkinID="btLimpar" OnClick="btnLimpar_OnClick" Style="vertical-align: middle; height: 25px; width: 25px" /><br />
                </asp:Panel>
                <asp:Label ID="Label1" runat="server" Text="Código integração" AssociatedControlID="_txtCodigoIntegracao"></asp:Label>
                <asp:TextBox ID="_txtCodigoIntegracao" runat="server" MaxLength="50" SkinID="text30C"></asp:TextBox>
                <asp:CheckBox ID="_chkBloqueado" Text="Bloqueado" runat="server" />
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>

    <fieldset>
        <legend>Cadastro de endereço</legend>
        <uc4:UCEnderecos ID="UCEnderecos1" runat="server" />
    </fieldset>
    <fieldset>
        <legend>Cadastro de contatos</legend>
        <uc8:UCGridContato ID="UCGridContato1" runat="server" />
    </fieldset>
    <fieldset>
        <div class="right">
            <asp:Button ID="_btnSalvar" runat="server" CausesValidation="true" ValidationGroup="ValidationSummary1" Text="Salvar" OnClick="_btnSalvar_Click" />
            <asp:Button ID="_btnCancelar" runat="server" Text="Cancelar" CausesValidation="False"
                OnClick="_btnCancelar_Click" />
            <input id="txtSelectedTab" type="hidden" class="txtSelectedTab" runat="server" />
        </div>
    </fieldset>
    <div id="ModalBuscaUASuperior">
        <uc5:UCUASuperior ID="UCUASuperior" runat="server" />
    </div>
</asp:Content>
