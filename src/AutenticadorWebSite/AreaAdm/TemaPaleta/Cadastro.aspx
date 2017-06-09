<%@ Page Title="" Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="AutenticadorWebSite.AreaAdm.TemaPaleta.Cadastro" %>

<%@ PreviousPageType VirtualPath="~/AreaAdm/TemaPaleta/Busca.aspx" %>

<%@ Register Src="~/WebControls/Combos/UCComboTemaPadrao.ascx" TagName="UCComboTemaPadrao" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblMensagem" runat="server" EnableViewState="false"></asp:Label>
    <asp:ValidationSummary ID="vsTema" runat="server" ValidationGroup="Tema" />
    <asp:Panel ID="pnlTema" runat="server" GroupingText="Cadastro de tema de cores">

        <uc1:UCComboTemaPadrao ID="UCComboTemaPadrao" runat="server" MostrarMensagemSelecione="true" Obrigatorio="true" ValidationGroup="Tema" />

        <asp:Label ID="lblNome" runat="server" Text="Nome" AssociatedControlID="txtNome"></asp:Label>
        <asp:TextBox ID="txtNome" runat="server" MaxLength="100" SkinID="text60C"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvNome" runat="server" ControlToValidate="txtNome" Display="Dynamic"
            ValidationGroup="Tema" ErrorMessage="Nome é obrigatório.">*</asp:RequiredFieldValidator>
        <asp:CustomValidator ID="cvPastaTema" runat="server" Display="Dynamic" ValidationGroup="Tema" ControlToValidate="txtNome"
            ErrorMessage="Caminho para os arquivos CSS do tema não foi encontrado. Verifique se o mesmo já foi criado." OnServerValidate="cvPastaTema_ServerValidate">*</asp:CustomValidator>

        <div class="right">
            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" ValidationGroup="Tema" OnClick="btnSalvar_Click" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CausesValidation="false" OnClick="btnCancelar_Click" />
        </div>
    </asp:Panel>

</asp:Content>
