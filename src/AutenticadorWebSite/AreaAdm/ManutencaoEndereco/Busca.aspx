<%@ Page Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true" Inherits="AreaAdm_ManutencaoEndereco_Busca" Codebehind="Busca.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="_updEnderecos" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Label ID="_lblMessage" runat="server" EnableViewState="False"></asp:Label>
            <fieldset id="fdsAssociarEnderecos" runat="server" visible="false">
                <legend>Associação de endereços</legend>
                <asp:GridView ID="_grvAssociarEnderecos" runat="server" AutoGenerateColumns="False"
                    DataKeyNames="end_id" OnRowCommand="_grvAssociarEnderecos_RowCommand" OnRowDataBound="_grvAssociarEnderecos_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="end_cep" HeaderText="CEP" />
                        <asp:BoundField DataField="end_logradouro" HeaderText="Endereço" />
                        <asp:BoundField DataField="end_distrito" HeaderText="Distrito" />
                        <asp:BoundField DataField="end_bairro" HeaderText="Bairro" />
                        <asp:BoundField DataField="cidadeuf" HeaderText="Cidade" />
                        <asp:TemplateField HeaderText="Remover">
                            <ItemTemplate>
                                <asp:ImageButton ID="_btnRemover" runat="server" CommandName="Remover" SkinID="btExcluirSemMensagem" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <div class="right">
                    <asp:Button ID="_btnAssociarEnderecos" runat="server" Text="Associar endereços" OnClick="_btnAssociarEnderecos_Click"
                        CausesValidation="False" />
                </div>
            </fieldset>
            <fieldset>
                <legend>Consulta de endereços</legend>
                <div id="_divPesquisa" runat="server">
                    <asp:Label ID="LabelCEP" runat="server" Text="CEP" AssociatedControlID="_txtCEP"></asp:Label>
                    <asp:TextBox ID="_txtCEP" runat="server" MaxLength="8" CssClass="numeric" SkinID="Numerico"></asp:TextBox>
                    <asp:Label ID="LabelSomenteNumeros" runat="server" Text="(somente números)"></asp:Label>
                    <asp:Label ID="LabelLogradouro" runat="server" Text="Endereço" AssociatedControlID="_txtLogradouro"></asp:Label>
                    <asp:TextBox ID="_txtLogradouro" runat="server" MaxLength="200" CssClass="text60C"></asp:TextBox>
                    <asp:Label ID="LabelBairro" runat="server" Text="Bairro" AssociatedControlID="_txtBairro"></asp:Label>
                    <asp:TextBox ID="_txtBairro" runat="server" MaxLength="100" CssClass="text30C"></asp:TextBox>
                    <asp:Label ID="LabelCidade" runat="server" ValidationGroup="Endereco" Text="Cidade"
                        AssociatedControlID="_txtCidade"></asp:Label>
                    <input id="_txtCid_id" runat="server" type="hidden" class="tbCid_id_incremental" />
                    <asp:TextBox ID="_txtCidade" runat="server" MaxLength="200" CssClass="text30C tbCidade_incremental"></asp:TextBox>
                </div>
                <div class="right">
                    <asp:Button ID="_btnPesquisar" runat="server" Text="Pesquisar" OnClick="_btnPesquisar_Click"
                        CausesValidation="False" />
                    <asp:Button ID="_btnNovo" runat="server" Text="Novo endereço" OnClick="_btnNovo_Click"
                        CausesValidation="False" />
                </div>
            </fieldset>
            <fieldset id="fdsResultado" runat="server">
                <legend>Resultados</legend>
                <asp:GridView ID="_grvEndereco" runat="server" AutoGenerateColumns="False" DataKeyNames="end_id,cid_id,end_zona"
                    DataSourceID="odsEndereco" OnRowCommand="_grvEndereco_RowCommand" OnRowDataBound="_grvEndereco_RowDataBound"
                    EmptyDataText="A pesquisa não encontrou resultados." AllowPaging="True">
                    <Columns>
                        <asp:BoundField DataField="end_cep" HeaderText="CEP" />
                        <asp:TemplateField HeaderText="Endereço">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("end_logradouro") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="_lblLogradouro" runat="server" Text='<%# Bind("end_logradouro") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Distrito">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("end_bairro") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="_lblDistrito" runat="server" Text='<%# Bind("end_distrito") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bairro">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("end_bairro") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="_lblBairro" runat="server" Text='<%# Bind("end_bairro") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cidade">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("cidadeuf") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="_lblCidade" runat="server" Text='<%# Bind("cidadeuf") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Alterar">
                            <ItemTemplate>
                                <asp:ImageButton ID="_btnAlterar" runat="server" CommandName="Edit" PostBackUrl="~/AreaAdm/ManutencaoEndereco/Cadastro.aspx"
                                    SkinID="btEditar" CausesValidation="False" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Excluir">
                            <ItemTemplate>
                                <asp:ImageButton ID="_btnExcluir" runat="server" CommandName="Deletar" SkinID="btExcluir"
                                    CausesValidation="False" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Associar" Visible="false">
                            <ItemTemplate>
                                <asp:ImageButton ID="_btnAssociar" runat="server" CommandName="Associar" SkinID="btNovo"
                                    CausesValidation="False" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </fieldset>
            <asp:ObjectDataSource ID="odsEndereco" runat="server" DataObjectTypeName="Autenticador.Entities.END_Endereco"
                EnablePaging="True" MaximumRowsParameterName="pageSize" SelectCountMethod="GetTotalRecords"
                SelectMethod="GetSelect" StartRowIndexParameterName="currentPage" TypeName="Autenticador.BLL.END_EnderecoBO"
                OnSelecting="odsEndereco_Selecting" DeleteMethod="Delete" OnDeleted="odsEndereco_Deleted">
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
