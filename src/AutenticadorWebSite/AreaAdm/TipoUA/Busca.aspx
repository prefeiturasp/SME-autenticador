<%@ Page Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true" Inherits="AreaAdm_TipoUA_Busca" Title="Untitled Page" Codebehind="Busca.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="_lblMessage" runat="server" EnableViewState="False"></asp:Label>
    <fieldset>
        <legend>Listagem de tipos de unidade administrativa</legend>
        <div>
            <asp:Button ID="_btnNovo" runat="server" Text="Novo tipo de unidade administrativa"
                OnClick="_btnNovo_Click" />
        </div>
        <div id="_divConsulta" runat="server">
            <asp:GridView ID="_dgvTipoUA" runat="server" AutoGenerateColumns="False" DataSourceID="_odsTipoUA"
                DataKeyNames="tua_id" AllowPaging="True" EmptyDataText="Não existem tipos de unidade administrativa cadastrados."
                OnRowCommand="_dgvTipoUA_RowCommand" OnRowDataBound="_dgvTipoUA_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="tua_id" HeaderText="tua_id" SortExpression="tua_id" InsertVisible="False"
                        Visible="False" />
                    <asp:TemplateField HeaderText="Tipo de unidade administrativa">
                        <ItemTemplate>
                            <asp:LinkButton ID="_btnAlterar" CommandName="Edit" runat="server" Text='<%# Bind("tua_nome") %>'
                                PostBackUrl="~/AreaAdm/TipoUA/Cadastro.aspx"></asp:LinkButton>
                            <asp:Label ID="_lblAlterar" runat="server" Text='<%# Bind("tua_nome") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="tua_situacao" HeaderText="Bloqueado" SortExpression="tua_situacao"
                        HeaderStyle-CssClass="center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                    <asp:TemplateField HeaderText="Excluir" HeaderStyle-CssClass="center" ItemStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="70px">
                        <ItemTemplate>
                            <asp:ImageButton ID="_btnExcluir" SkinID="btExcluir" CommandName="Deletar" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </fieldset>
    <asp:ObjectDataSource ID="_odsTipoUA" runat="server" DataObjectTypeName="Autenticador.Entities.SYS_TipoUnidadeAdministrativa"
        DeleteMethod="Delete" SelectMethod="GetSelect" TypeName="Autenticador.BLL.SYS_TipoUnidadeAdministrativaBO"
        EnablePaging="True" MaximumRowsParameterName="pageSize" SelectCountMethod="GetTotalRecords"
        StartRowIndexParameterName="currentPage" OnSelecting="_odsTipoUA_Selecting">
        <SelectParameters>
            <asp:Parameter Name="tua_id" DbType="Int32" Size="4" />
            <asp:Parameter Name="tua_nome" DbType="AnsiString" Size="100" />
            <asp:Parameter Name="tua_situacao" DbType="Byte" Size="1" />
            <asp:Parameter Name="paginado" DbType="Boolean" DefaultValue="true" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
