<%@ Page Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true" Inherits="AreaAdm_TipoEntidade_Busca" Title="Untitled Page" Codebehind="Busca.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="_lblMessage" runat="server" EnableViewState="False"></asp:Label>
    <fieldset>
        <legend>Listagem de tipos de entidade</legend>
        <div>
            <asp:Button ID="_btnNovo" runat="server" Text="Novo tipo de entidade" OnClick="_btnNovo_Click" /></div>
        <asp:GridView ID="_dgvTipoEntidade" runat="server" AutoGenerateColumns="False" DataKeyNames="ten_id"
            EmptyDataText="Não existem tipos de entidade cadastrados." DataSourceID="_odsTipoEntidade"
            AllowPaging="True" OnRowCommand="_dgvTipoEntidade_RowCommand" OnRowDataBound="_dgvTipoEntidade_RowDataBound">
            <Columns>
                <asp:BoundField DataField="ten_id" HeaderText="ten_id" InsertVisible="false" ReadOnly="True"
                    SortExpression="ten_id" Visible="false" />
                <asp:TemplateField HeaderText="Tipo de entidade">
                    <ItemTemplate>
                        <asp:LinkButton ID="_btnAlterar" runat="server" CommandName="Edit" Text='<%# Bind("ten_nome") %>'
                            PostBackUrl="~/AreaAdm/TipoEntidade/Cadastro.aspx"></asp:LinkButton>
                        <asp:Label ID="_lblAlterar" runat="server" Text='<%# Bind("ten_nome") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ten_situacao" HeaderText="Bloqueado" SortExpression="ten_situacao"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="center"></asp:BoundField>
                <asp:TemplateField HeaderText="Excluir" HeaderStyle-CssClass="center" HeaderStyle-Width="70px"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:ImageButton ID="_btnExcluir" SkinID="btExcluir" runat="server" CommandName="Deletar" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </fieldset>
    <asp:ObjectDataSource ID="_odsTipoEntidade" runat="server" DataObjectTypeName="Autenticador.Entities.SYS_TipoEntidade"
        DeleteMethod="Delete" EnablePaging="True" StartRowIndexParameterName="currentPage"
        MaximumRowsParameterName="pageSize" SelectCountMethod="GetTotalRecords" SelectMethod="GetSelect"
        TypeName="Autenticador.BLL.SYS_TipoEntidadeBO" OnSelecting="_odsTipoEntidade_Selecting">
        <SelectParameters>
            <asp:Parameter Name="ten_id" DbType="Int32" Size="4" />
            <asp:Parameter Name="ten_nome" DbType="AnsiString" Size="100" />
            <asp:Parameter Name="ten_situacao" DbType="Byte" Size="1" />
            <asp:Parameter Name="paginado" DbType="Boolean" DefaultValue="true" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
