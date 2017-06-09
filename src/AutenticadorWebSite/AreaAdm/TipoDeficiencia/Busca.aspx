<%@ Page Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true" Inherits="AreaAdm_TipoDeficiencia_Busca" Title="Untitled Page" Codebehind="Busca.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="_lblMessage" runat="server" EnableViewState="False"></asp:Label>
    <fieldset>
        <legend>Listagem de tipos de deficiência</legend>
        <div>
            <asp:Button ID="btnNovoTipoDeficiencia" runat="server" Text="Novo tipo de deficiência"
                OnClick="btnNovoTipoDeficiencia_Click" />
        </div>
        <asp:GridView ID="_dgvTipoDeficiencia" runat="server" AutoGenerateColumns="False"
            EmptyDataText="Não existem tipos de deficiência cadastrados." DataKeyNames="tde_id"
            DataSourceID="_odsTipoDeficiencia" AllowPaging="True" OnRowCommand="_dgvTipoDeficiencia_RowCommand"
            OnRowDataBound="_dgvTipoDeficiencia_RowDataBound">
            <Columns>
                <asp:BoundField DataField="tde_id" HeaderText="tde_id" InsertVisible="False" ReadOnly="True"
                    SortExpression="tde_id" Visible="False">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Tipo de deficiência">
                    <ItemTemplate>
                        <asp:LinkButton ID="_btnAlterar" runat="server" CommandName="Edit" Text='<%# Bind("tde_nome") %>'
                            PostBackUrl="~/AreaAdm/TipoDeficiencia/Cadastro.aspx"></asp:LinkButton>
                        <asp:Label ID="_lblAlterar" runat="server" Text='<%# Bind("tde_nome") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:BoundField DataField="tde_situacao" HeaderText="Bloqueado" SortExpression="tde_situacao">
                    <HeaderStyle CssClass="center" />
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderText="Excluir">
                    <ItemTemplate>
                        <asp:ImageButton ID="_btnExcluir" runat="server" CommandName="Deletar" SkinID="btExcluir">
                        </asp:ImageButton>
                    </ItemTemplate>
                    <HeaderStyle CssClass="center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
    </fieldset>
    <asp:ObjectDataSource ID="_odsTipoDeficiencia" runat="server" OnSelecting="_odsTipoDeficiencia_Selecting"
        DataObjectTypeName="Autenticador.Entities.PES_TipoDeficiencia" SelectMethod="GetSelect"
        TypeName="Autenticador.BLL.PES_TipoDeficienciaBO" EnablePaging="True" MaximumRowsParameterName="pageSize"
        SelectCountMethod="GetTOtalRecords" StartRowIndexParameterName="currentPage"
        DeleteMethod="Delete">
        <SelectParameters>
            <asp:Parameter Name="tde_id" DbType="Int32" Size="4" />
            <asp:Parameter Name="tde_nome" DbType="AnsiString" Size="100" />
            <asp:Parameter Name="tde_situacao" DbType="Byte" Size="1" />
            <asp:Parameter Name="paginado" DbType="Boolean" DefaultValue="true" />
        </SelectParameters>
    </asp:ObjectDataSource>
    </div>
</asp:Content>
