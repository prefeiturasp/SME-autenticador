<%@ Control Language="C#" AutoEventWireup="true" Inherits="WebControls_Endereco_UCEnderecosAntigo"
    CodeBehind="UCEnderecosAntigo.ascx.cs" %>
<%@ Register Src="../Combos/UCComboZona.ascx" TagName="UCComboZona" TagPrefix="uc2" %>
<asp:UpdatePanel ID="updCadastroEndereco" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Label ID="lblMessage" runat="server" EnableViewState="False"></asp:Label>
        <asp:Repeater ID="rptEndereco" runat="server" OnItemDataBound="rptEndereco_ItemDataBound">
            <HeaderTemplate>
                <br />
            </HeaderTemplate>
            <ItemTemplate>
                <asp:Panel ID="pnlEndereco" runat="server" CssClass="tbEndereco">
                    <asp:Label ID="lblMessage" runat="server" EnableViewState="False"></asp:Label>
                    <asp:Label ID="LabelCEP" runat="server" Text='<%#_VS_Obrigatorio ? "CEP (somente números)  *" : "CEP (somente números)" %>'
                        AssociatedControlID="txtCEP"> </asp:Label>
                    <asp:TextBox ID="txtCEP" runat="server" MaxLength="8" Width="160" SkinID="CepIncremental"
                        AutoPostBack="True" Text='<%#Bind("end_cep") %>' OnTextChanged="txtCEP_TextChanged"> </asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvCEP" runat="server" ValidationGroup='<%#_ValidationGroup %>'
                        ControlToValidate="txtCEP" Display="Dynamic" Visible='<%#_VS_Obrigatorio %>'
                        ErrorMessage="CEP é obrigatório.">* </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revCEP" runat="server" ValidationGroup='<%#_ValidationGroup %>'
                        ControlToValidate="txtCEP" Display="Dynamic" ErrorMessage="CEP inválido." ValidationExpression="^([0-9]){8}$">* </asp:RegularExpressionValidator>
                    <asp:ImageButton ID="btnLimparEndereco" runat="server" Visible="False" SkinID="btLimpar"
                        ToolTip="Limpar campos do endereço" CssClass="tbNovoEndereco_incremental" OnClick="btnLimparEndereco_Click"
                        CausesValidation="false" TabIndex="10" />
                    <asp:Label ID="LabelLogradouro" runat="server" Text='<%#_VS_Obrigatorio ? "Endereço *" : "Endereço" %>'
                        AssociatedControlID="txtLogradouro"></asp:Label>
                    <asp:TextBox ID="txtLogradouro" runat="server" MaxLength="200" ToolTip="Digite para buscar o endereço"
                        Text='<%#Bind("end_logradouro") %>' Width="510" CssClass="tbLogradouro_incremental"> </asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvLogradouro" runat="server" ValidationGroup='<%#_ValidationGroup %>'
                        Visible='<%#_VS_Obrigatorio %>' ControlToValidate="txtLogradouro" ErrorMessage="Endereço é obrigatório."
                        Display="Dynamic">* </asp:RequiredFieldValidator>
                    <table>
                        <tr id="trNumeroCompl" runat="server">
                            <td>
                                <asp:Label ID="LabelNumero" runat="server" Text='<%#_VS_Obrigatorio ? "Número *" : "Número" %>'
                                    AssociatedControlID="txtNumero"> </asp:Label>
                                <asp:TextBox ID="txtNumero" runat="server" Text='<%#Bind("numero") %>' MaxLength="20"
                                    Width="240px"> </asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvNumero" runat="server" ValidationGroup='<%#_ValidationGroup %>'
                                    Visible='<%#_VS_Obrigatorio %>' ControlToValidate="txtNumero" ErrorMessage="Número é obrigatório."
                                    Display="Dynamic">* </asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="LabelComplemento" runat="server" Text="Complemento" AssociatedControlID="txtComplemento"> </asp:Label>
                                <asp:TextBox ID="txtComplemento" runat="server" MaxLength="100" Text='<%#Bind("complemento") %>'
                                    Width="240px"> </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LabelDistrito" runat="server" Text="Distrito" AssociatedControlID="txtDistrito"></asp:Label>
                                <asp:TextBox ID="txtDistrito" runat="server" MaxLength="100" Text='<%#Bind("end_distrito") %>'
                                    CssClass="text30C tbDistrito_incremental"> </asp:TextBox>
                            </td>
                            <td>
                                <uc2:UCComboZona ID="UCComboZona1" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LabelBairro" runat="server" Text='<%#_VS_Obrigatorio ? "Bairro *" : "Bairro" %>'
                                    AssociatedControlID="txtBairro"></asp:Label>
                                <asp:TextBox ID="txtBairro" runat="server" MaxLength="100" Text='<%#Bind("end_bairro") %>'
                                    Width="240"> </asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvBairro" runat="server" ControlToValidate="txtBairro"
                                    ValidationGroup='<%#_ValidationGroup %>' Visible='<%#_VS_Obrigatorio %>' Display="Dynamic"
                                    ErrorMessage="Bairro é obrigatório.">* </asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <input id="txtCid_id" runat="server" type="hidden" class="tbCid_id_incremental" value='<%#Bind("cid_id") %>' />
                                <asp:Label ID="LabelCidade" runat="server" ValidationGroup='<%#_ValidationGroup %>'
                                    Text='<%#_VS_Obrigatorio ? "Cidade *" : "Cidade" %>' AssociatedControlID="txtCidade"> </asp:Label>
                                <asp:TextBox ID="txtCidade" runat="server" MaxLength="200" Text='<%#Bind("cid_nome") %>'
                                    CssClass="text30C tbCidade_incremental"> </asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvCidade" runat="server" ControlToValidate="txtCidade"
                                    ValidationGroup='<%#_ValidationGroup %>' Visible='<%#_VS_Obrigatorio %>' Display="Dynamic"
                                    ErrorMessage="Cidade é obrigatório.">* </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                    <asp:Button ID="btnExcluir" runat="server" Text="Excluir" CausesValidation="false"
                        Visible="false" OnClick="btnExcluir_Click" />
                    <asp:Label ID="lblBanco" runat="server" Visible="false" Text='<%#Bind("banco") %>'></asp:Label>
                    <asp:Label ID="lblID" runat="server" Visible="false" Text='<%#Bind("id") %>'></asp:Label>
                    <input id="txtEnd_id" runat="server" type="hidden" value='<%#Bind("end_id") %>' class="tbEnd_id_incremental" />
                </asp:Panel>
            </ItemTemplate>
        </asp:Repeater>
        <div class="botoes">
            <asp:Button ID="btnNovo" runat="server" Text="Novo endereço" OnClick="btnNovo_Click"
                CausesValidation="false" Visible="false" />
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
