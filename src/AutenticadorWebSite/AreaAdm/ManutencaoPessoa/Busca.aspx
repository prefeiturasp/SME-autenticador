<%@ Page Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true" Inherits="AreaAdm_ManutencaoPessoa_Busca" CodeBehind="Busca.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="_updPessoas" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Label ID="_lblMessage" runat="server" EnableViewState="False"></asp:Label>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="pessoa" />
            <fieldset id="fdsAssociarPessoas" runat="server" visible="false">
                <legend>Associação de pessoas</legend>
                <asp:GridView ID="_grvAssociarPessoas" runat="server" AutoGenerateColumns="False"
                    DataKeyNames="pes_id" OnRowCommand="_grvAssociarPessoas_RowCommand" OnRowDataBound="_grvAssociarPessoas_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="pes_nome" HeaderText="Nome" />
                        <asp:BoundField DataField="pes_dataNascimento" HeaderText="Data nasc.">
                            <HeaderStyle CssClass="center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tipo_documentacao_cpf" HeaderText="tipo_documentacao_cpf" />
                        <asp:BoundField DataField="tipo_documentacao_rg" HeaderText="tipo_documentacao_rg" />
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
                    <asp:Button ID="_btnAssociar" runat="server" Text="Associar pessoas" OnClick="_btnAssociar_Click"
                        CausesValidation="False" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click"
                        CausesValidation="False" />

                </div>
            </fieldset>
            <fieldset>
                <legend>Consulta de pessoas</legend>
                <div id="_divPesquisa" runat="server">
                    <asp:Label ID="_lblNome" runat="server" Text="Nome" AssociatedControlID="_txtNome"></asp:Label>
                    <asp:TextBox ID="_txtNome" runat="server" MaxLength="200" SkinID="text60C"></asp:TextBox>
                    <asp:Label ID="_lblDataNasc" runat="server" Text="Data de nascimento" AssociatedControlID="_txtDataNasc"></asp:Label>
                    <asp:TextBox ID="_txtDataNasc" runat="server" MaxLength="10" CssClass="mskData"></asp:TextBox>
                    <asp:CustomValidator ID="cvDataNascimento" runat="server" ControlToValidate="_txtDataNasc"
                        ValidationGroup="pessoa" Display="Dynamic" ErrorMessage="Data de nascimento inválida."
                        OnServerValidate="ValidarData_ServerValidate">* </asp:CustomValidator>
                    <asp:Label ID="_lblCPF" runat="server" Text="Label" AssociatedControlID="_txtCPF"></asp:Label>
                    <asp:TextBox ID="_txtCPF" runat="server" MaxLength="50" SkinID="text15C"></asp:TextBox>
                    <asp:Label ID="_lblRG" runat="server" Text="Label" AssociatedControlID="_txtRG"></asp:Label>
                    <asp:TextBox ID="_txtRG" runat="server" MaxLength="50" SkinID="text15C"></asp:TextBox>
                </div>
                <div class="right">
                    <asp:Button ID="_btnPesquisar" runat="server" Text="Pesquisar" OnClick="_btnPesquisar_Click" ValidationGroup="pessoa" />
                    <asp:Button ID="_btnNovo" runat="server" Text="Nova pessoa" OnClick="_btnNovo_Click"
                        CausesValidation="False" />
                </div>
            </fieldset>
            <fieldset id="fdsResultado" runat="server">
                <legend>Resultados</legend>
                <asp:GridView ID="_grvPessoa" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    DataKeyNames="pes_id" DataSourceID="odsPessoas" OnRowCommand="_grvPessoa_RowCommand"
                    OnRowDataBound="_grvPessoa_RowDataBound" EmptyDataText="A pesquisa não encontrou resultados.">
                    <Columns>
                        <asp:TemplateField HeaderText="Nome">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("pes_nome") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="_lblNome" runat="server" Text='<%# Bind("pes_nome") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Data nasc.">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("pes_dataNascimento") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="_lblData" runat="server" Text='<%# Bind("pes_dataNascimento") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="tipo_documentacao_cpf">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("tipo_documentacao_cpf") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="_lblCPF" runat="server" Text='<%# Bind("tipo_documentacao_cpf") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="tipo_documentacao_rg">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("tipo_documentacao_rg") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="_lblRG" runat="server" Text='<%# Bind("tipo_documentacao_rg") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Alterar">
                            <ItemTemplate>
                                <asp:ImageButton ID="_btnAlterar" runat="server" CommandName="Edit" PostBackUrl="~/AreaAdm/ManutencaoPessoa/Cadastro.aspx"
                                    SkinID="btEditar" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Exluir">
                            <ItemTemplate>
                                <asp:ImageButton ID="_btnExcluir" runat="server" CommandName="Deletar" SkinID="btExcluir" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Associar" Visible="false">
                            <ItemTemplate>
                                <asp:ImageButton ID="_btnAssociar" runat="server" CommandName="Associar" SkinID="btNovo" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </fieldset>
            <asp:ObjectDataSource ID="odsPessoas" runat="server" DataObjectTypeName="Autenticador.Entities.PES_Pessoa"
                EnablePaging="True" MaximumRowsParameterName="pageSize" OnSelecting="odsPessoas_Selecting"
                SelectCountMethod="GetTotalRecords" SelectMethod="GetSelect" StartRowIndexParameterName="currentPage"
                TypeName="Autenticador.BLL.PES_PessoaBO" DeleteMethod="Delete"></asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        //ADICIONADO POR CONTA DO UPDATEPANEL. PRECISA SER ADICIONADO NA PÁGINA POR CONTA DE POSTBACK.
        Sys.Application.add_load(function () {

            //mascara de data 'dd/mm/yyyy'
            $(".mskData").setMask({ mask: '39/19/2999', selectCharsOnFocus: false, autoTab: false });
            $(".mskData").datepicker({ dateFormat: 'dd/mm/yy' });
        });
    </script>
</asp:Content>

