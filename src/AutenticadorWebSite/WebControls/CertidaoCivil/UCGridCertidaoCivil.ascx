<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="WebControls_CertidaoCivil_UCGridCertidaoCivil" Codebehind="UCGridCertidaoCivil.ascx.cs" %>
<asp:UpdatePanel ID="updGridCertidaoCivil" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:UpdateProgress ID="upgConteudo" runat="server" DisplayAfter="10" AssociatedUpdatePanelID="updGridCertidaoCivil">
            <ProgressTemplate>
                <div class="loader">
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:Label ID="_lblMessage" runat="server"></asp:Label>
        <asp:Repeater ID="rptCertidaoCivil" runat="server" OnItemDataBound="rptCertidaoCivil_ItemDataBound">
            <HeaderTemplate>
                <br />
            </HeaderTemplate>
            <ItemTemplate>
                <asp:Panel ID="pnlContato" runat="server" GroupingText='<%#Bind("ctc_tipoDescricao") %>'>
                    <input id="ctc_id" value='<%# Bind("ctc_id") %>' runat="server" type="hidden" />
                    <asp:ImageButton ID="btnLimparVertidao" runat="server" Visible="true" SkinID="btLimpar"
                        ToolTip="Limpar certidão" OnClick="btnLimparCertidao_Click" CausesValidation="false"
                        TabIndex="10" />
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblMatricula" runat="server" Text="Matrícula" AssociatedControlID="txtMatricula"></asp:Label>
                                <asp:TextBox ID="txtMatricula" runat="server" MaxLength="32" Text='<%#Bind("ctc_matricula") %>'></asp:TextBox>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkGemeo" runat="server" Text="Possui irmão gêmeo" Checked='<%#Bind("ctc_gemeo")%>'/>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkModeloNovo" runat="server" Text="Certidão modelo novo" Checked='<%#Bind("ctc_modeloNovo") %>'/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label9" runat="server" Text="Número do termo" AssociatedControlID="tbNumTerm"></asp:Label>
                                <asp:TextBox ID="tbNumTerm" runat="server" MaxLength="50" Text='<%#Bind("ctc_numeroTermo") %>'></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label10" runat="server" Text="Folha" AssociatedControlID="tbFolha"></asp:Label>
                                <asp:TextBox ID="tbFolha" runat="server" MaxLength="20" Text='<%#Bind("ctc_folha") %>'></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label11" runat="server" Text="Livro" AssociatedControlID="tbLivro"></asp:Label>
                                <asp:TextBox ID="tbLivro" runat="server" MaxLength="20" Text='<%#Bind("ctc_livro") %>'></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label12" runat="server" Text="Data emissão" AssociatedControlID="tbDtEmissao"></asp:Label>
                                <asp:TextBox ID="tbDtEmissao" runat="server" Text='<%#Bind("ctc_dataEmissao") %>'
                                    SkinID="DataSemCalendario"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label13" runat="server" Text="Nome do Cartório" AssociatedControlID="tbNomeCart"></asp:Label>
                                <asp:TextBox ID="tbNomeCart" runat="server" MaxLength="200" Text='<%#Bind("ctc_nomeCartorio") %>'></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label14" runat="server" Text="Distrito" AssociatedControlID="tbDistritoCart"></asp:Label>
                                <asp:TextBox ID="tbDistritoCart" runat="server" MaxLength="100" Text='<%#Bind("ctc_distritoCartorio") %>'></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label15" runat="server" Text="Cidade" AssociatedControlID="tbCidadeCart"></asp:Label>
                                <asp:TextBox ID="tbCidadeCart" runat="server" CssClass="text15C tbCidadeCertidao_incremental"
                                    Text='<%#Bind("cid_nomeCartorio") %>'></asp:TextBox>
                                <input id="tbCid_idCertidao" value='<%# Bind("cid_idCartorio") %>' runat="server"
                                    type="hidden" class="tbCid_idCertidao_incremental" />
                            </td>
                            <td>
                                <asp:Label ID="Label16" runat="server" Text="UF" AssociatedControlID="ddlUF"></asp:Label>
                                <asp:DropDownList ID="ddlUF" runat="server" DataSourceID="odsUF" DataTextField="unf_nome"
                                    DataValueField="unf_id" AppendDataBoundItems="True" SkinID="text20C">
                                    <asp:ListItem Value="">-- Selcione uma opção --</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ItemTemplate>
        </asp:Repeater>
        <asp:ObjectDataSource ID="odsUF" runat="server" DataObjectTypeName="Autenticador.Entities.END_UnidadeFederativa"
            DeleteMethod="Delete" OldValuesParameterFormatString="original_{0}" SelectMethod="GetSelect"
            TypeName="Autenticador.BLL.END_UnidadeFederativaBO" UpdateMethod="Save">
            <DeleteParameters>
                <asp:Parameter Name="entity" Type="Object" />
                <asp:Parameter Name="banco" Type="Object" />
            </DeleteParameters>
        </asp:ObjectDataSource>
    </ContentTemplate>
</asp:UpdatePanel>
