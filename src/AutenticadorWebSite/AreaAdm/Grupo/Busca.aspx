<%@ Page Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true" Inherits="AreaAdm_Grupo_Busca" Title="Untitled Page" Codebehind="Busca.aspx.cs" %>

<%@ Register Src="../../WebControls/Combos/UCComboSistema.ascx" TagName="UCComboSistemas"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="_lblMessage" runat="server" EnableViewState="False"></asp:Label>
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Pesquisa" />
    <fieldset style="top: 0px; left: 0px">
        <legend>Consulta de grupos</legend>
        <div id="divPesquisa" runat="server">
            <uc1:UCComboSistemas ID="UCComboSistemas1" runat="server" />
        </div>
        <div class="right">
            <asp:Button ID="_btnPesquisa" runat="server" Text="Pesquisar" OnClick="_btnPesquisa_Click" ValidationGroup="Pesquisa" />
            <asp:Button ID="_btnNovo" runat="server" Text="Novo grupo" OnClick="_btnNovo_Click" />
            <asp:Button ID="btnLimpaCache" runat="server" Text="Limpar Cache" OnClick="_btnLimpaCache_Click" ValidationGroup="Pesquisa" Visible="false"/>
        </div>
    </fieldset>
    <fieldset id="fdsResultados" runat="server">
        <legend>Resultados</legend>
        <asp:GridView ID="_dgvGrupo" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            DataKeyNames="gru_id,sis_id,vis_id" DataSourceID="odsGrupo" OnRowDataBound="_dgvGrupo_RowDataBound"
            EmptyDataText="A pesquisa não encontrou resultados." 
            OnRowCommand="_dgvGrupo_RowCommand">
            <Columns>
                <asp:BoundField DataField="gru_id" HeaderText="gru_id" InsertVisible="False" ReadOnly="True"
                    SortExpression="gru_id" Visible="False" />
                <asp:TemplateField HeaderText="Nome do grupo">
                    <ItemTemplate>
                        <asp:LinkButton ID="_lkbAlterar" runat="server" CommandName="Edit" Text='<%# Bind("gru_nome") %>'
                            PostBackUrl="~/AreaAdm/Grupo/Cadastro.aspx"></asp:LinkButton>
                        <asp:Label ID="_lblAlterar" runat="server" Text='<%# Bind("gru_nome") %>' Visible="False"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="sis_nome" HeaderText="Sistema" />
                <asp:BoundField DataField="vis_nome" HeaderText="Visão" ItemStyle-HorizontalAlign="Center">
                    <HeaderStyle CssClass="center" />
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="gru_situacaoNome" HeaderText="Situação" ItemStyle-HorizontalAlign="Center">
                    <HeaderStyle CssClass="center" />
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderText="Usuários">
                    <ItemTemplate>
                        <asp:ImageButton ID="_btnUsuario" runat="server" CommandName="Edit" PostBackUrl="~/AreaAdm/Grupo/Associar.aspx"
                            SkinID="btDetalhar" />
                    </ItemTemplate>
                    <HeaderStyle CssClass="center" />
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>                    
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Permissões" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:ImageButton ID="_btnPermissao" runat="server" CommandName="Edit" SkinID="btPermissao"
                            PostBackUrl="~/AreaAdm/Permissao/Cadastro.aspx" />
                    </ItemTemplate>
                    <HeaderStyle CssClass="center" />
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Excluir" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:ImageButton ID="_btnExcluir" runat="server" CommandName="Deletar" SkinID="btExcluir" />
                    </ItemTemplate>
                    <HeaderStyle CssClass="center" />
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </fieldset>
    <asp:ObjectDataSource ID="odsGrupo" runat="server" DataObjectTypeName="Autenticador.Entities.SYS_Grupo"
        EnablePaging="True" MaximumRowsParameterName="pageSize" SelectCountMethod="GetTotalRecords"
        SelectMethod="GetSelect" StartRowIndexParameterName="currentPage" TypeName="Autenticador.BLL.SYS_GrupoBO"
        OnSelecting="odsGrupo_Selecting" DeleteMethod="Delete">
    </asp:ObjectDataSource>
</asp:Content>
