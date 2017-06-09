<%@ Page Title="" Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="AutenticadorWebSite.AreaAdm.TemaPadrao.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblMensagem" runat="server" EnableViewState="false"></asp:Label>
    <asp:Panel ID="pnlBuscaTema" runat="server" GroupingText="Listagem de temas padrões">
        <div>
            <asp:Button ID="btnNovoTema" runat="server" Text="Novo tema padrão" OnClick="btnNovoTema_Click" />
        </div>
        <asp:GridView ID="grvTema" runat="server" AutoGenerateColumns="false" AllowPaging="true" DataKeyNames="tep_id"
            EmptyDataText="Não existem temas padrões cadastrados." OnRowCommand="grvTema_RowCommand" OnRowDataBound="grvTema_RowDataBound"
            DataSourceID="odsTema">
            <Columns>
                <asp:TemplateField HeaderText="Nome">
                    <ItemTemplate>
                        <asp:LinkButton ID="lkbAlterar" runat="server" CommandName="Edit" Text='<%# Bind("tep_nome") %>' PostBackUrl="~/AreaAdm/TemaPadrao/Cadastro.aspx"></asp:LinkButton>
                        <asp:Label ID="lblAlterar" runat="server" Text='<%# Bind("tep_nome") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="30%" />
                    <ItemStyle HorizontalAlign="Left" Width="30%" />
                </asp:TemplateField>
                <asp:BoundField DataField="tep_descricao" HeaderText="Descrição" SortExpression="tep_descricao">
                    <ItemStyle HorizontalAlign="Left" />
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Excluir">
                    <ItemTemplate>
                        <asp:ImageButton ID="btnExcluir" SkinID="btExcluir" runat="server" CommandName="Deletar" />
                    </ItemTemplate>
                    <HeaderStyle CssClass="center" Width="10%" />
                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <asp:ObjectDataSource ID="odsTema" runat="server" DataObjectTypeName="Autenticador.Entities.CFG_TemaPadrao" 
        DeleteMethod="Delete" SelectMethod="SelecionaAtivosPaginado" TypeName="Autenticador.BLL.CFG_TemaPadraoBO"
        EnablePaging="True" MaximumRowsParameterName="pageSize" StartRowIndexParameterName="currentPage" 
        SelectCountMethod="GetTotalRecords" OnSelecting="odsTema_Selecting">
        <SelectParameters>
            <asp:Parameter Name="currentPage" Type="Int32" />
            <asp:Parameter Name="pageSize" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
