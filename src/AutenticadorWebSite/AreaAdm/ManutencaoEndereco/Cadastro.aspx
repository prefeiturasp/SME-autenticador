<%@ Page Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true" Inherits="AreaAdm_ManutencaoEndereco_Cadastro" Codebehind="Cadastro.aspx.cs" %>

<%@ PreviousPageType VirtualPath="~/AreaAdm/ManutencaoEndereco/Busca.aspx" %>
<%@ Register src="../../WebControls/Combos/UCComboZona.ascx" tagname="UCComboZona" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="_updEnderecos" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Label ID="_lblMessage" runat="server" EnableViewState="False"></asp:Label>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Endereco" />
            <fieldset>
                <legend>Cadastro de endereços</legend>
                <asp:Label ID="LabelCEP" runat="server" AssociatedControlID="txtCEP" 
                    Text="CEP *"></asp:Label>
                <asp:TextBox ID="txtCEP" runat="server" 
                    CssClass="numeric tbCep_incremental" MaxLength="8" SkinID="Numerico"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCEP" runat="server" 
                    ControlToValidate="txtCEP" Display="Dynamic" ErrorMessage="CEP é obrigatório." 
                    ValidationGroup="Endereco">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revCEP" runat="server" 
                    ControlToValidate="txtCEP" Display="Dynamic" ErrorMessage="CEP inválido." 
                    ValidationExpression="^([0-9]){8}$" ValidationGroup="Endereco">*</asp:RegularExpressionValidator>
                <asp:Label ID="LabelSomenteNumeros" runat="server" Text="(somente números)"></asp:Label>
                <asp:Label ID="LabelLogradouro" runat="server" 
                    AssociatedControlID="txtLogradouro" Text="Endereço *"></asp:Label>
                <asp:TextBox ID="txtLogradouro" runat="server" 
                    CssClass="text60C tbLogradouro_incremental" MaxLength="200"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvLogradouro" runat="server" 
                    ControlToValidate="txtLogradouro" Display="Dynamic" 
                    ErrorMessage="Endereço é obrigatório." ValidationGroup="Endereco">*</asp:RequiredFieldValidator>
                <asp:ImageButton ID="btnIncluirEndereco" runat="server" 
                    CssClass="tbNovoEndereco_incremental" OnClick="btnIncluirEndereco_Click" 
                    SkinID="btNovo" Visible="False" />
                <asp:Label ID="LabelNumero" runat="server" AssociatedControlID="txtNumero" 
                    Text="Número *"></asp:Label>
                <asp:TextBox ID="txtNumero" runat="server" MaxLength="10" SkinID="text10C"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvNumero" runat="server" 
                    ControlToValidate="txtNumero" Display="Dynamic" 
                    ErrorMessage="Número é obrigatório." ValidationGroup="Endereco">*</asp:RequiredFieldValidator>
                <asp:Label ID="LabelComplemento" runat="server" 
                    AssociatedControlID="txtComplemento" Text="Complemento"></asp:Label>
                <asp:TextBox ID="txtComplemento" runat="server" MaxLength="100" 
                    SkinID="text30C"></asp:TextBox>
                <asp:Label ID="LabelDistrito" runat="server" AssociatedControlID="txtDistrito" 
                    Text="Distrito"></asp:Label>
                <asp:TextBox ID="txtDistrito" runat="server" 
                    CssClass="text30C tbDistrito_incremental" MaxLength="100"></asp:TextBox>
                <uc1:UCComboZona ID="UCComboZona1" runat="server" />
                <asp:Label ID="LabelBairro" runat="server" AssociatedControlID="txtBairro" 
                    Text="Bairro *"></asp:Label>
                <asp:TextBox ID="txtBairro" runat="server" 
                    CssClass="text30C tbBairro_incremental" MaxLength="100"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvBairro" runat="server" 
                    ControlToValidate="txtBairro" Display="Dynamic" 
                    ErrorMessage="Bairro é obrigatório." ValidationGroup="Endereco">*</asp:RequiredFieldValidator>
                <asp:Label ID="LabelCidade" runat="server" AssociatedControlID="txtCidade" 
                    Text="Cidade *" ValidationGroup="Endereco"></asp:Label>
                <input id="_txtCid_id" runat="server" type="hidden" class="tbCid_id_incremental" />
                <input id="_txtEnd_id" runat="server" type="hidden" class="tbEnd_id_incremental" />
                <asp:TextBox ID="txtCidade" runat="server" 
                    CssClass="text30C tbCidade_incremental" MaxLength="200"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCidade" runat="server" 
                    ControlToValidate="txtCidade" Display="Dynamic" 
                    ErrorMessage="Cidade é obrigatório." ValidationGroup="Endereco">*</asp:RequiredFieldValidator>
                <div class="right">
                    <asp:Button ID="_btnSalvar" runat="server" Text="Salvar" OnClick="_btnSalvar_Click"
                        ValidationGroup="Endereco" />
                    <asp:Button ID="_btnCancelar" runat="server" Text="Cancelar" CausesValidation="False"
                        OnClick="_btnCancelar_Click" />
                </div>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
