<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="WebControls_Pessoa_UCCadastroPessoa" Codebehind="UCCadastroPessoa.ascx.cs" %>
<%@ Register Src="../Combos/UCComboEstadoCivil.ascx" TagName="UCComboEstadoCivil"
    TagPrefix="uc1" %>
<%@ Register Src="../Combos/UCComboSexo.ascx" TagName="UCComboSexo"
     TagPrefix="uc2" %>
<%@ Register Src="../Combos/UCComboPais.ascx" TagName="UCComboPais"
     TagPrefix="uc3" %>
<%@ Register Src="../Combos/UCComboRacaCor.ascx" TagName="UCComboRacaCor"
     TagPrefix="uc4" %>
<%@ Register Src="../Combos/UCComboTipoEscolaridade.ascx" TagName="UCComboTipoEscolaridade"
    TagPrefix="uc5" %>
<%@ Register Src="../Combos/UCComboTipoDeficiencia.ascx" TagName="UCComboTipoDeficiencia"
    TagPrefix="uc6" %>
    
<asp:UpdatePanel ID="_updCadastroPessoa" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Label ID="LabelNome" runat="server" Text="Nome *" AssociatedControlID="txtNome"></asp:Label>
        <asp:TextBox ID="txtNome" runat="server" MaxLength="200" SkinID="text60C"></asp:TextBox>
        <asp:RequiredFieldValidator ID="_rfvNome" runat="server" ControlToValidate="txtNome"
            ValidationGroup="Pessoa" ErrorMessage="Nome é obrigatório." Display="Dynamic">*</asp:RequiredFieldValidator>
        <asp:Label ID="LabelNomeAbreviado" runat="server" Text="Nome abreviado" AssociatedControlID="txtNomeAbreviado"></asp:Label>
        <asp:TextBox ID="txtNomeAbreviado" runat="server" MaxLength="50" SkinID="text30C"></asp:TextBox>
        <asp:Label ID="LabelNomeSocial" runat="server" Text="Nome social" AssociatedControlID="txtNomeSocial"></asp:Label>
        <asp:TextBox ID="txtNomeSocial" runat="server" MaxLength="200" SkinID="text30C"></asp:TextBox>
        <uc3:UCComboPais ID="UCComboPais1" runat="server" />
        <asp:CheckBox ID="chkNaturalizado" runat="server" Text="Naturalizado" />
        <asp:Label ID="LabelNaturalidade" runat="server" Text="Naturalidade" AssociatedControlID="txtNaturalidade"></asp:Label>
        <input id="_txtCid_id" runat="server" type="hidden" class="tbCid_idNaturalidade_incremental" />
        <asp:TextBox ID="txtNaturalidade" runat="server" MaxLength="200"
            CssClass="text30C tbNaturalidade_incremental"></asp:TextBox>
        <asp:Label ID="LabelDataNasc" runat="server" Text="Data de nascimento" AssociatedControlID="txtDataNasc"></asp:Label>
        <asp:TextBox ID="txtDataNasc" runat="server" MaxLength="10" SkinID="Data"></asp:TextBox>
        <asp:CustomValidator ID="cvDataNascimento" runat="server" ControlToValidate="txtDataNasc"
            ValidationGroup="Pessoa" Display="Dynamic" ErrorMessage="Data de nascimento inválida."
            OnServerValidate="ValidarData_ServerValidate">* </asp:CustomValidator>
        <uc1:UCComboEstadoCivil ID="UCComboEstadoCivil1" runat="server" />
        <uc4:UCComboRacaCor ID="UCComboRacaCor1" runat="server" />
        <uc2:UCComboSexo ID="UCComboSexo1" runat="server" />
        <asp:Label ID="LabelPai" runat="server" Text="Pai" AssociatedControlID="txtPai"></asp:Label>
        <input id="_txtPes_idFiliacaoPai" runat="server" type="hidden" />
        <asp:TextBox ID="txtPai" runat="server" MaxLength="200" SkinID="text60C" Enabled="false"></asp:TextBox>
        <asp:ImageButton ID="_btnPai" runat="server" CausesValidation="False" SkinID="btPesquisar"
            OnClick="_btnPai_Click" />
        <asp:Label ID="LabelMae" runat="server" Text="Mãe" AssociatedControlID="txtMae"></asp:Label>
        <input id="_txtPes_idFiliacaoMae" runat="server" type="hidden" />
        <asp:TextBox ID="txtMae" runat="server" MaxLength="200" SkinID="text60C" Enabled="false"></asp:TextBox>
        <asp:ImageButton ID="_btnMae" runat="server" CausesValidation="False" SkinID="btPesquisar"
            OnClick="_btnMae_Click" />
        <uc5:UCComboTipoEscolaridade ID="UCComboTipoEscolaridade1" runat="server" />
        <uc6:UCComboTipoDeficiencia ID="UCComboTipoDeficiencia1" runat="server" />
        <asp:Label ID="LabelFoto" runat="server" Text="Foto" AssociatedControlID="iptFoto"></asp:Label>
        <input type="file" id="iptFoto" name="Foto" runat="server" visible="true" />
        <br />
        <asp:Image ID="imgFoto" runat="server" Visible="false" />
        <asp:CheckBox ID="chbExcluirImagem" runat="server" Visible="false" Text="Excluir foto" />
    </ContentTemplate>
</asp:UpdatePanel>
