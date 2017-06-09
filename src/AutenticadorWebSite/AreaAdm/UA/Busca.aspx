<%@ Page Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true" Inherits="AreaAdm_UA_Busca" Title="Untitled Page" Codebehind="Busca.aspx.cs" %>

<%@ Register Src="~/WebControls/Combos/UCComboEntidade.ascx" TagName="UCComboEntidade"
    TagPrefix="uc1" %>
<%@ Register Src="~/WebControls/Combos/UCComboTipoUnidadeAdministrativa.ascx" TagName="UCComboTipoUnidadeAdministrativa"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="_lblMessage" runat="server" EnableViewState="False"></asp:Label>
    <fieldset>
        <legend>Consulta de unidades administrativas</legend>
        <div id="_divPesquisa" runat="server">
            <uc1:UCComboEntidade ID="UCComboEntidade1" runat="server" />
            <uc2:UCComboTipoUnidadeAdministrativa ID="UCComboTipoUnidadeAdministrativa1" runat="server" />
            <asp:Label ID="LabelNome" runat="server" Text="Nome" AssociatedControlID="_txtNome"></asp:Label>
            <asp:TextBox ID="_txtNome" runat="server" SkinID="text60C"></asp:TextBox>
            <asp:Label ID="LabelCodigo" runat="server" Text="Código" AssociatedControlID="_txtCodigo"></asp:Label>
            <asp:TextBox ID="_txtCodigo" runat="server" MaxLength="20" SkinID="text20C"></asp:TextBox>
        </div>
        <div class="right">
            <asp:Button ID="_btnPesquisar" runat="server" Text="Pesquisar" OnClick="_btnPesquisar_Click" />
            <asp:Button ID="_btnNovo" runat="server" Text="Nova unidade administrativa" OnClick="_btnNovo_Click"
                CausesValidation="False" />
        </div>
    </fieldset>
    <fieldset id="fdsResultados" runat="server">
        <legend>Resultados</legend>
        <asp:GridView ID="_grvUA" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            DataSourceID="odsUA" DataKeyNames="ent_id,uad_id" EmptyDataText="A pesquisa não encontrou resultados."
            OnRowCommand="_grvUA_RowCommand" OnRowDataBound="_grvUA_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="Nome">
                    <ItemTemplate>
                        <asp:LinkButton ID="_btnAlterar" runat="server" Text='<%# bind("uad_nome") %>' CommandName="Edit"
                            PostBackUrl="~/AreaAdm/UA/Cadastro.aspx"></asp:LinkButton>
                        <asp:Label ID="_lblAlterar" runat="server" Text='<%# bind("uad_nome") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("uad_nome") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="uad_codigo" HeaderText="Código" />
                <asp:BoundField DataField="tua_nome" HeaderText="Tipo de UA" />
                <asp:BoundField DataField="ent_razaosocial" HeaderText="Entidade" />
                <asp:BoundField DataField="uad_bloqueado" HeaderText="Bloqueado">
                    <HeaderStyle CssClass="center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Excluir">
                    <ItemTemplate>
                        <asp:ImageButton ID="_btnExcluir" runat="server" SkinID="btExcluir" CommandName="Deletar" />
                    </ItemTemplate>
                    <HeaderStyle CssClass="center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </fieldset>
    <asp:ObjectDataSource ID="odsUA" runat="server" DataObjectTypeName="Autenticador.Entities.SYS_UnidadeAdministrativa"
        EnablePaging="True" MaximumRowsParameterName="pageSize" SelectCountMethod="GetTotalRecords"
        SelectMethod="GetSelect" TypeName="Autenticador.BLL.SYS_UnidadeAdministrativaBO"
        StartRowIndexParameterName="currentPage" OnSelecting="odsUA_Selecting" DeleteMethod="Delete">
    </asp:ObjectDataSource>
</asp:Content>
