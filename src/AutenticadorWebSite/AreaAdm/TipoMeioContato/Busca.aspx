<%@ Page Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true" Inherits="AreaAdm_TipoMeioContato_Busca" Title="Untitled Page" Codebehind="Busca.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="_lblMessage" runat="server" EnableViewState="False"></asp:Label>
    <fieldset>
        <legend>Listagem de tipos de meio de contato</legend>
        <div>
            <asp:Button ID="_btnNovo" runat="server" Text="Novo tipo de meio de contato" OnClick="_btnNovo_Click" />
        </div>
        <div id="_divConsulta" runat="server">
            <asp:GridView ID="_dgvTipoMeioContato" runat="server" AutoGenerateColumns="False"
                DataSourceID="_odsTipoMeioContato" AllowPaging="True" DataKeyNames="tmc_id" EmptyDataText="Não existem tipos de meio de contato cadastrados."
                OnRowCommand="_dgvTipoMeioContato_RowCommand" OnRowDataBound="_dgvTipoMeioContato_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="tmc_id" HeaderText="tmc_id" InsertVisible="False" SortExpression="tmc_id"
                        Visible="False" />
                    <asp:TemplateField HeaderText="Tipo de meio de contato">
                        <ItemTemplate>
                            <asp:LinkButton ID="_lkbAlterar" CommandName="Edit" runat="server" Text='<%# Bind("tmc_nome") %>'
                                PostBackUrl="~/AreaAdm/TipoMeioContato/Cadastro.aspx"></asp:LinkButton>
                            <asp:Label ID="_lblAlterar" runat="server" Text='<%# Bind("tmc_nome") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="tmc_validacao" HeaderText="Validação" SortExpression="tmc_validacao">
                    </asp:BoundField>
                    <asp:BoundField DataField="tmc_situacao" HeaderText="Bloqueado" ItemStyle-HorizontalAlign="Center"
                        SortExpression="tmc_situacao">
                        <HeaderStyle CssClass="center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Excluir" HeaderStyle-CssClass="center" ItemStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="70px">
                        <ItemTemplate>
                            <asp:ImageButton ID="_btnExcluir" SkinID="btExcluir" CommandName="Deletar" runat="server" />
                        </ItemTemplate>
                        <HeaderStyle CssClass="center" Width="70px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </fieldset>
    <asp:ObjectDataSource ID="_odsTipoMeioContato" runat="server" DataObjectTypeName="Autenticador.Entities.SYS_TipoMeioContato"
        DeleteMethod="Delete" SelectMethod="GetSelect" TypeName="Autenticador.BLL.SYS_TipoMeioContatoBO"
        EnablePaging="True" MaximumRowsParameterName="pageSize" StartRowIndexParameterName="currentPage"
        SelectCountMethod="GetTotalRecords" OnSelecting="_odsTipoMeioContato_Selecting"
        InsertMethod="Save">
        <SelectParameters>
            <asp:Parameter Name="tmc_id" DbType="Int32" Size="4" />
            <asp:Parameter Name="tmc_nome" DbType="AnsiString" Size="100" />
            <asp:Parameter Name="tmc_situacao" DbType="Byte" Size="1" />
            <asp:Parameter Name="paginado" DbType="Boolean" DefaultValue="true" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
