<%@ Page Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true" Inherits="AreaAdm_ManutencaoEndereco_Associar" Codebehind="Associar.aspx.cs" %>

<%@ Register Src="../../WebControls/Combos/UCComboZona.ascx" TagName="UCComboZona"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="_updEnderecos" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Endereco" />
            <asp:Label ID="_lblMessage" runat="server" EnableViewState="False"></asp:Label>
            <fieldset style="top: 0px; left: 0px">
                <legend>Associação de endereços</legend>
                <asp:GridView ID="_grvAssociarEnderecos" runat="server" AutoGenerateColumns="False"
                    DataKeyNames="end_id,cid_id,end_zona" OnRowCommand="_grvAssociarEnderecos_RowCommand"
                    OnRowDataBound="_grvAssociarEnderecos_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="CEP">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("end_cep") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="_btnSelecionar" runat="server" CommandName="Selecionar" Text='<%# Bind("end_cep") %>'
                                    CausesValidation="False"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Endereço">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("end_logradouro") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="_lblLogradouro" runat="server" Text='<%# Bind("end_logradouro") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Distrito">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("end_bairro") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="_lblDistrito" runat="server" Text='<%# Bind("end_distrito") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bairro">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("end_bairro") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="_lblBairro" runat="server" Text='<%# Bind("end_bairro") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cidade">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("cidadeuf") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="_lblCidade" runat="server" Text='<%# Bind("cidadeuf") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </fieldset>
            <fieldset>
                <legend>Cadastro de endereço</legend>
                <asp:Label ID="LabelCEP" runat="server" AssociatedControlID="txtCEP" Text="CEP *"></asp:Label>
                <asp:TextBox ID="txtCEP" runat="server" CssClass="numeric tbCep_incremental" MaxLength="8"
                    SkinID="Numerico"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCEP" runat="server" ControlToValidate="txtCEP"
                    Display="Dynamic" ErrorMessage="CEP é obrigatório." ValidationGroup="Endereco">*</asp:RequiredFieldValidator>
                <asp:Label ID="LabelSomenteNumeros" runat="server" Text="(somente números)"></asp:Label>
                <asp:Label ID="LabelLogradouro" runat="server" AssociatedControlID="txtLogradouro"
                    Text="Endereço *"></asp:Label>
                <asp:TextBox ID="txtLogradouro" runat="server" CssClass="text60C tbLogradouro_incremental"
                    MaxLength="200"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvLogradouro" runat="server" ControlToValidate="txtLogradouro"
                    Display="Dynamic" ErrorMessage="Endereço é obrigatório." ValidationGroup="Endereco">*</asp:RequiredFieldValidator>
                <asp:ImageButton ID="btnIncluirEndereco" runat="server" CssClass="tbNovoEndereco_incremental"
                    OnClick="btnIncluirEndereco_Click" SkinID="btNovo" Visible="False" />
                <asp:Label ID="LabelNumero" runat="server" AssociatedControlID="txtNumero" Text="Número *"></asp:Label>
                <asp:TextBox ID="txtNumero" runat="server" MaxLength="10" SkinID="text10C"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvNumero" runat="server" ControlToValidate="txtNumero"
                    Display="Dynamic" ErrorMessage="Número é obrigatório." ValidationGroup="Endereco">*</asp:RequiredFieldValidator>
                <asp:Label ID="LabelComplemento" runat="server" AssociatedControlID="txtComplemento"
                    Text="Complemento"></asp:Label>
                <asp:TextBox ID="txtComplemento" runat="server" MaxLength="100" SkinID="text30C"></asp:TextBox>
                <asp:Label ID="LabelDistrito" runat="server" AssociatedControlID="txtDistrito" Text="Distrito"></asp:Label>
                <asp:TextBox ID="txtDistrito" runat="server" CssClass="text30C tbDistrito_incremental"
                    MaxLength="100"></asp:TextBox>
                <uc1:UCComboZona ID="UCComboZona1" runat="server" />
                <asp:Label ID="LabelBairro" runat="server" AssociatedControlID="txtBairro" Text="Bairro *"></asp:Label>
                <asp:TextBox ID="txtBairro" runat="server" CssClass="text30C tbBairro_incremental"
                    MaxLength="100"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvBairro" runat="server" ControlToValidate="txtBairro"
                    Display="Dynamic" ErrorMessage="Bairro é obrigatório." ValidationGroup="Endereco">*</asp:RequiredFieldValidator>
                <asp:Label ID="LabelCidade" runat="server" AssociatedControlID="txtCidade" Text="Cidade *"
                    ValidationGroup="Endereco"></asp:Label>
                <input id="_txtCid_id" runat="server" type="hidden" class="tbCid_id_incremental" />
                <input id="_txtEnd_id" runat="server" type="hidden" class="tbEnd_id_incremental" />
                <asp:TextBox ID="txtCidade" runat="server" CssClass="text30C tbCidade_incremental"
                    MaxLength="200"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCidade" runat="server" ControlToValidate="txtCidade"
                    Display="Dynamic" ErrorMessage="Cidade é obrigatório." ValidationGroup="Endereco">*</asp:RequiredFieldValidator>
                <br />
            </fieldset>
            <div class="right">
                <asp:Button ID="_btnSalvar" runat="server" Text="Confirmar associação" OnClick="_btnSalvar_Click"
                    ValidationGroup="Endereco" />
                <asp:Button ID="_btnCancelar" runat="server" Text="Cancelar" CausesValidation="False"
                    OnClick="_btnCancelar_Click" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
