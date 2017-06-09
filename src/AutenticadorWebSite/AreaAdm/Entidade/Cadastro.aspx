<%@ Page Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true" Inherits="AreaAdm_Entidade_Cadastro" Title="Untitled Page" CodeBehind="Cadastro.aspx.cs" %>

<%@ PreviousPageType VirtualPath="~/AreaAdm/Entidade/Busca.aspx" %>
<%@ Register Src="../../WebControls/Combos/UCComboTipoEntidade.ascx" TagName="UCComboTipoEntidade"
    TagPrefix="uc1" %>
<%@ Register Src="../../WebControls/Combos/UCComboEntidade.ascx" TagName="UCComboEntidade"
    TagPrefix="uc2" %>
<%@ Register Src="../../WebControls/Contato/UCGridContato.ascx" TagName="UCGridContato"
    TagPrefix="uc5" %>
<%@ Register Src="~/WebControls/Endereco/UCEnderecos.ascx" TagName="UCEnderecos" TagPrefix="uc7" %>

<%@ Register Src="~/WebControls/Combos/UCComboTemaPadrao.ascx" TagName="UCComboTemaPadrao" TagPrefix="uc4" %>

<%@ Register Src="~/WebControls/Combos/UCComboTemaPaleta.ascx" TagName="UCComboTemaPaleta" TagPrefix="uc6" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="_lblMessage" runat="server" EnableViewState="False"></asp:Label>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Endereco" />
    <fieldset>
        <legend>Cadastro de entidades</legend>
        <uc1:UCComboTipoEntidade ID="UCComboTipoEntidade1" runat="server" />
        <asp:Label ID="LabelRazaoSocial" runat="server" Text="Razão social *" AssociatedControlID="_txtRazaoSocial"></asp:Label>
        <asp:TextBox ID="_txtRazaoSocial" runat="server" MaxLength="200" SkinID="text60C"></asp:TextBox>
        <asp:RequiredFieldValidator ID="_rfvRazaoSocial" runat="server" ValidationGroup="Endereco"
            ControlToValidate="_txtRazaoSocial" ErrorMessage="Razão social é obrigatório."
            Display="Dynamic">*</asp:RequiredFieldValidator>
        <asp:Label ID="LabelNomeFantasia" runat="server" Text="Nome fantasia" AssociatedControlID="_txtNomeFantasia"></asp:Label>
        <asp:TextBox ID="_txtNomeFantasia" runat="server" MaxLength="200" SkinID="text60C"></asp:TextBox>
        <asp:Label ID="LabelSigla" runat="server" Text="Sigla" AssociatedControlID="_txtSigla"></asp:Label>
        <asp:TextBox ID="_txtSigla" runat="server" MaxLength="50" SkinID="text30C"></asp:TextBox>
        <asp:Label ID="LabelCodigo" runat="server" Text="Código" AssociatedControlID="_txtCodigo"></asp:Label>
        <asp:TextBox ID="_txtCodigo" runat="server" MaxLength="20" SkinID="text20C"></asp:TextBox>
        <asp:Label ID="LabelCNPJ" runat="server" Text="CNPJ" AssociatedControlID="_txtCNPJ"></asp:Label>
        <asp:TextBox ID="_txtCNPJ" runat="server" MaxLength="14" CssClass="numeric" SkinID="Numerico"></asp:TextBox>
        <asp:RegularExpressionValidator ID="_revCNPJ" runat="server" ValidationGroup="Endereco"
            ControlToValidate="_txtCNPJ" Display="Dynamic" ErrorMessage="CNPJ inválido."
            ValidationExpression="^([0-9]){14}$">*</asp:RegularExpressionValidator>
        <asp:Label ID="Label1" runat="server" Text="(somente números)"></asp:Label>
        <asp:Label ID="LabelIE" runat="server" Text="Inscrição estadual" AssociatedControlID="_txtIE"></asp:Label>
        <asp:TextBox ID="_txtIE" runat="server" MaxLength="20" SkinID="text20C"></asp:TextBox>
        <asp:Label ID="LabelIM" runat="server" Text="Inscrição municipal" AssociatedControlID="_txtIM"></asp:Label>
        <asp:TextBox ID="_txtIM" runat="server" MaxLength="20" SkinID="text20C"></asp:TextBox>
        <uc2:UCComboEntidade ID="UCComboEntidade1" runat="server" />
        <asp:CheckBox ID="_chkBloqueado" Text="Bloqueado" runat="server" />
    </fieldset>
    <fieldset>
        <legend>Cadastro de endereço</legend>
        <uc7:UCEnderecos ID="UCEnderecos1" runat="server" />
    </fieldset>
    <fieldset>
        <legend>Cadastro de contatos</legend>
        <uc5:UCGridContato ID="UCGridContato1" runat="server" />
    </fieldset>
    <fieldset>
        <legend>Configurações técnicas</legend>
        <asp:Label ID="lblUrlAcesso" runat="server" AssociatedControlID="txtUrlAcesso" Text="URL usada para acesso da entidade"></asp:Label>
        <asp:TextBox ID="txtUrlAcesso" runat="server" SkinID="text60C" MaxLength="200"></asp:TextBox>
        <asp:Label ID="lblLogoCliente" runat="server" Text="Logo do cliente" AssociatedControlID="fupLogoCliente"></asp:Label>
        <asp:FileUpload ID="fupLogoCliente" runat="server" Width="400" /><br />
        <asp:Image ID="imgLogoCliente" runat="server" Visible="false" />
        <asp:CheckBox ID="chkExibeLogoCliente" runat="server" Text="Exibir o logo do cliente nas telas" TextAlign="Right" /><br />
        <uc4:UCComboTemaPadrao ID="UCComboTemaPadrao" runat="server" MostrarMensagemSelecione="true" />
        <uc6:UCComboTemaPaleta ID="UCComboTemaPaleta" runat="server" MostrarMensagemSelecione="true" PermiteEditar="false" />
    </fieldset>
    <fieldset>
        <div class="right">
            <asp:Button ID="_btnSalvar" runat="server" Text="Salvar" OnClick="_btnSalvar_Click"
                ValidationGroup="Endereco" />
            <asp:Button ID="_btnCancelar" runat="server" Text="Cancelar" CausesValidation="False"
                OnClick="_btnCancelar_Click" />
            <input id="txtSelectedTab" type="hidden" class="txtSelectedTab" runat="server" />
        </div>
    </fieldset>
</asp:Content>
