<%@ Page Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true" Inherits="AreaAdm_Entidade_Busca" Title="Untitled Page" Codebehind="Busca.aspx.cs" %>

<%@ Register Src="../../WebControls/Combos/UCComboTipoEntidade.ascx" TagName="UCComboTipoEntidade"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="_lblMessage" runat="server" EnableViewState="False"></asp:Label>
    <fieldset>
        <legend>Consulta de entidades</legend>
        <div id="_divPesquisa" runat="server">
            <uc1:UCComboTipoEntidade ID="UCComboTipoEntidade1" runat="server" />
            <asp:Label ID="LabelRazaoSocial" runat="server" Text="Razão social" AssociatedControlID="_txtRazaoSocial"></asp:Label>
            <asp:TextBox ID="_txtRazaoSocial" runat="server" MaxLength="200" SkinID="text60C"></asp:TextBox>
            <asp:Label ID="LabelNomeFantasia" runat="server" Text="Nome fantasia" AssociatedControlID="_txtNomeFantasia"></asp:Label>
            <asp:TextBox ID="_txtNomeFantasia" runat="server" MaxLength="200" SkinID="text60C"></asp:TextBox>
            <asp:Label ID="LabelCNPJ" runat="server" Text="CNPJ" MaxLength="14" SkinID="text20C"
                AssociatedControlID="_txtCNPJ"></asp:Label>
            <asp:TextBox ID="_txtCNPJ" runat="server" MaxLength="14" SkinID="text20C"></asp:TextBox>
            <asp:Label ID="LabelCodigo" runat="server" Text="Código" AssociatedControlID="_txtCodigo"></asp:Label>
            <asp:TextBox ID="_txtCodigo" runat="server" MaxLength="20" SkinID="text20C"></asp:TextBox>
        </div>
        <div class="right">
            <asp:Button ID="_btnPesquisar" runat="server" Text="Pesquisar" OnClick="_btnPesquisar_Click"
                CausesValidation="False" />
            <asp:Button ID="_btnNovo" runat="server" Text="Nova entidade" OnClick="_btnNovo_Click"
                CausesValidation="False" />
        </div>
    </fieldset>
    <fieldset id="fdsResultado" runat="server">
        <legend>Resultados</legend>
        <asp:GridView ID="_grvEntidades" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            DataKeyNames="ent_id" DataSourceID="odsEntidade" EmptyDataText="A pesquisa não encontrou resultados."
            OnRowCommand="_grvEntidades_RowCommand" OnRowDataBound="_grvEntidades_RowDataBound">
            <EmptyDataRowStyle Font-Bold="True" ForeColor="Red" />
            <Columns>
                <asp:TemplateField HeaderText="Razão Social">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ent_razaoSocial") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="_btnAlterar" runat="server" CommandName="Edit" PostBackUrl="~/AreaAdm/Entidade/Cadastro.aspx"
                            Text='<%# Bind("ent_razaoSocial") %>' CausesValidation="False"></asp:LinkButton>
                        <asp:Label ID="_lblAlterar" runat="server" Text='<%# Bind("ent_razaoSocial") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ent_nomeFantasia" HeaderText="Nome fantasia">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="ent_codigo" HeaderText="Código" />
                <asp:BoundField DataField="ent_CNPJ" HeaderText="CNPJ">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="ten_nome" HeaderText="Tipo de entidade">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="ent_situacao" HeaderText="Bloqueado">
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
    <asp:ObjectDataSource ID="odsEntidade" runat="server" DataObjectTypeName="Autenticador.Entities.SYS_Entidade"
        EnablePaging="True" MaximumRowsParameterName="pageSize" SelectCountMethod="GetTotalRecords"
        SelectMethod="GetSelect" StartRowIndexParameterName="currentPage" TypeName="Autenticador.BLL.SYS_EntidadeBO"
        OnSelecting="odsEntidade_Selecting" DeleteMethod="Delete"></asp:ObjectDataSource>
</asp:Content>
