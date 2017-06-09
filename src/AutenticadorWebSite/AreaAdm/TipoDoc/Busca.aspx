<%@ Page Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true" Inherits="AreaAdm_TipoDoc_Busca" Title="Untitled Page" Codebehind="Busca.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="_lblMessage" runat="server" EnableViewState="False"></asp:Label>
    <fieldset>
        <legend>Listagem de tipos de documentação</legend>
        <div>
            <asp:Button ID="_btnNovaDocumentacao" runat="server" Text="Novo tipo de documentação"
                OnClick="_btnNovaDocumentacao_Click" />
        </div>
        <asp:GridView ID="_dgvDocumentacao" runat="server" AutoGenerateColumns="False" DataSourceID="_odsTipoDocumentacao"
            AllowPaging="True" DataKeyNames="tdo_id" EmptyDataText="Não existem tipos de documentação cadastrados."
            OnRowCommand="_dgvDocumentacao_RowCommand" OnRowDataBound="_dgvDocumentacao_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="Documento">
                    <ItemTemplate>
                        <asp:LinkButton ID="_btnAlterar" runat="server" CommandName="Edit" Text='<%# Bind("tdo_nome") %>'
                            PostBackUrl="Cadastro.aspx"></asp:LinkButton>
                        <asp:Label ID="_lblAlterar" runat="server" Text='<%# Bind("tdo_nome") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                <asp:BoundField DataField="tdo_sigla" HeaderText="Sigla" SortExpression="tdo_sigla">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>


                <asp:BoundField DataField="tdo_classificacaoNome" HeaderText="Classificação" SortExpression="tdo_classificacaoNome">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                
                                
                <asp:BoundField DataField="tdo_validacao" HeaderText="Validação" SortExpression="tdo_validacao">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                
                <asp:BoundField DataField="tdo_situacao" HeaderText="Bloqueado" SortExpression="tdo_situacao">
                    <HeaderStyle CssClass="center" />
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                
                <asp:TemplateField HeaderText="Excluir">
                    <ItemTemplate>
                        <asp:ImageButton ID="_btnExcluir" SkinID="btExcluir" runat="server" CommandName="Deletar" />
                    </ItemTemplate>
                    <HeaderStyle CssClass="center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </fieldset>
    <asp:ObjectDataSource ID="_odsTipoDocumentacao" runat="server" DataObjectTypeName="Autenticador.Entities.SYS_TipoDocumentacao"
        OnSelecting="_odsTipoDocumentacao_Selecting" EnablePaging="True" MaximumRowsParameterName="pageSize"
        SelectMethod="GetSelect" StartRowIndexParameterName="currentPage" TypeName="Autenticador.BLL.SYS_TipoDocumentacaoBO"
        DeleteMethod="Delete" SelectCountMethod="GetTotalRecords">
        <SelectParameters>
            <asp:Parameter Name="tdo_id" DbType="Int32" Size="4" />
            <asp:Parameter Name="tdo_nome" DbType="AnsiString" Size="100" />
            <asp:Parameter Name="tdo_sigla" DbType="AnsiString" Size="10" />
            <asp:Parameter Name="tdo_situacao" DbType="Byte" Size="1" />
            <asp:Parameter Name="paginado" DbType="Boolean" DefaultValue="true" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
