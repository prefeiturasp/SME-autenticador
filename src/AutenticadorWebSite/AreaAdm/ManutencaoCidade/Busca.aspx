<%@ Page Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true" Inherits="AreaAdm_ManutencaoCidade_Busca" Codebehind="Busca.aspx.cs" %>

<%@ Register Src="~/WebControls/Combos/UCComboPais.ascx" TagName="UCComboPais" TagPrefix="uc1" %>
<%@ Register Src="~/WebControls/Combos/UCComboUnidadeFederativa.ascx" TagName="UCComboUnidadeFederativa"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="_updCidades" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Label ID="_lblMessage" runat="server" EnableViewState="False"></asp:Label>
            <fieldset id="fdsAssociacaoCidades" runat="server" visible="false">
                <legend>Associação de cidades</legend>
                <asp:GridView ID="_grvAssociacaoCidades" runat="server" AutoGenerateColumns="False"
                    DataKeyNames="cid_id" OnRowCommand="_grvAssociacaoCidades_RowCommand" OnRowDataBound="_grvAssociacaoCidades_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="cid_nome" HeaderText="Cidade" />
                        <asp:BoundField DataField="unf_nome" HeaderText="Estado" />
                        <asp:BoundField DataField="pai_nome" HeaderText="País" />
                        <asp:TemplateField HeaderText="Remover" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="_btnRemoverCidade" SkinID="btExcluirSemMensagem" CommandName="RemoverCidade"
                                    runat="server" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <div class="right">
                    <asp:Button ID="_btnAssociarCidades" runat="server" Text="Associar cidades" OnClick="_btnAssociarCidades_Click"
                        CausesValidation="False" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CausesValidation="false" OnClick="btnCancelar_Click" />
                </div>
            </fieldset>
            <fieldset>
                <legend>Consulta de cidades</legend>
                <div id="_divPesquisa" runat="server">
                    <uc1:UCComboPais ID="UCComboPais" runat="server" />
                    <uc2:UCComboUnidadeFederativa ID="UCComboUnidadeFederativa" runat="server" />
                    <asp:Label ID="LabelCidade" runat="server" AssociatedControlID="_txtCidade" Text="Cidade"></asp:Label>
                    <asp:TextBox ID="_txtCidade" runat="server" MaxLength="200" SkinID="text30C"></asp:TextBox>
                </div>
                <div class="right">
                    <asp:Button ID="_btnPesquisar" runat="server" Text="Pesquisar" OnClick="_btnPesquisar_Click"
                        CausesValidation="False" />
                </div>
            </fieldset>
            <fieldset id="fdsResultados" runat="server">
                <legend>Resultados</legend>
                <asp:GridView ID="_grvCidade" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    OnRowCommand="_grvCidade_RowCommand" OnRowDataBound="_grvCidade_RowDataBound"
                    BorderStyle="None" DataKeyNames="pai_id,unf_id,cid_id" DataSourceID="odsCidade"
                    EmptyDataText="A pesquisa não encontrou resultados.">
                    <EmptyDataRowStyle Font-Bold="True" ForeColor="Red" />
                    <Columns>
                        <asp:TemplateField HeaderText="Cidade">
                            <ItemTemplate>
                                <asp:Label ID="_lbcid_nome" runat="server" Text='<%#Bind("cid_nome") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DDD">
                            <ItemTemplate>
                                <asp:Label ID="_lbcid_ddd" runat="server" Text='<%#Bind("cid_ddd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Estado">
                            <ItemTemplate>
                                <asp:Label ID="_lbunf_nome" runat="server" Text='<%#Bind("unf_nome") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="País">
                            <ItemTemplate>
                                <asp:Label ID="_lbpai_nome" runat="server" Text='<%#Bind("pai_nome") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Alterar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="_btnAlterar" SkinID="btEditar" CommandName="Edit" runat="server"
                                    PostBackUrl="~/AreaAdm/ManutencaoCidade/Cadastro.aspx" 
                                    CausesValidation="False" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Excluir" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="_btnExcluir" SkinID="btExcluir" CommandName="DeletarCidade"
                                    runat="server" CausesValidation="False" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Associar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="false">
                            <ItemTemplate>
                                <asp:ImageButton ID="_btnAssociar" SkinID="btNovo" CommandName="AssociarCidade" 
                                    runat="server" CausesValidation="False" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle Font-Bold="True" ForeColor="Red" />
                </asp:GridView>
            </fieldset>
            <asp:ObjectDataSource ID="odsCidade" runat="server" DataObjectTypeName="Autenticador.Entities.END_Cidade"
                SelectCountMethod="GetTotalRecords" SelectMethod="GetSelect" EnablePaging="True"
                MaximumRowsParameterName="pageSize" StartRowIndexParameterName="currentPage"
                TypeName="Autenticador.BLL.END_CidadeBO" OnSelecting="odsCidade_Selecting"
                DeleteMethod="Delete"></asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
