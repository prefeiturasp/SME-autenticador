<%@ Page Language="C#" MasterPageFile="~/Busca/MasterPageBusca.master" AutoEventWireup="true" Inherits="Busca_UA" Codebehind="UA.aspx.cs" %>

<%@ Register Src="../WebControls/Combos/UCComboEntidade.ascx" TagName="UCComboEntidade"
    TagPrefix="uc1" %>
<%@ Register Src="../WebControls/Combos/UCComboTipoUnidadeAdministrativa.ascx" TagName="UCComboTipoUnidadeAdministrativa"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="_lblMessage" runat="server" EnableViewState="False"></asp:Label>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Entidade" />
    <fieldset>
        <uc1:UCComboEntidade ID="UCComboEntidade1" runat="server" />
        <uc2:UCComboTipoUnidadeAdministrativa ID="UCComboTipoUnidadeAdministrativa1" runat="server" />
        <asp:Label ID="Label3" runat="server" Text="Nome" EnableViewState="False" AssociatedControlID="_txtNome"></asp:Label><asp:TextBox ID="_txtNome" runat="server" MaxLength="200" SkinID="text60C"></asp:TextBox><asp:Label ID="Label4" runat="server" Text="Código" EnableViewState="False" AssociatedControlID="_txtCodigo"></asp:Label><asp:TextBox ID="_txtCodigo" runat="server" MaxLength="20" SkinID="text20C"></asp:TextBox><div class="right">
        <asp:Button ID="_btnPesquisar" runat="server" OnClick="_btnPesquisar_Click" Text="Pesquisar" ValidationGroup="Entidade" />
        </div>
    </fieldset>
    <fieldset id="fdsResultados" runat="server">
        <legend>Resultados</legend><asp:GridView ID="_dgvUA" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            DataKeyNames="ent_id,uad_id,uad_nome" DataSourceID="odsUA" EmptyDataText="A pesquisa não encontrou resultados."
            OnRowEditing="_dgvUA_RowEditing">
            <Columns>
                <asp:BoundField DataField="ent_id" HeaderText="ent_id" ReadOnly="True" Visible="False" />
                <asp:BoundField DataField="uad_id" HeaderText="uad_id" ReadOnly="True" Visible="False" />
                <asp:TemplateField HeaderText="Nome">
                    <ItemTemplate>
                        <asp:LinkButton ID="_lkbSelect" runat="server" CommandName="Edit" Text='<%# Bind("uad_nome") %>'
                            CausesValidation="False"></asp:LinkButton></ItemTemplate><EditItemTemplate>                        
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="uad_codigo" HeaderText="Código" />
                <asp:BoundField DataField="tua_nome" HeaderText="Tipo" />
                <asp:BoundField DataField="ent_razaoSocial" HeaderText="Entidade" />
                <asp:BoundField DataField="uad_nomeSup" HeaderText="UA superior" />
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="odsUA" runat="server" DataObjectTypeName="Autenticador.Entities.SYS_UnidadeAdministrativa"
            EnablePaging="True" MaximumRowsParameterName="pageSize" OnSelecting="odsUA_Selecting"
            SelectCountMethod="GetTotalRecords" SelectMethod="GetSelect" StartRowIndexParameterName="currentPage"
            TypeName="Autenticador.BLL.SYS_UnidadeAdministrativaBO">
            <SelectParameters>
                <asp:ControlParameter ControlID="UCComboEntidade1" DbType="Guid" Name="ent_id" PropertyName="_Combo.SelectedValue" />
                <asp:ControlParameter ControlID="UCComboTipoUnidadeAdministrativa1" DbType="Guid"
                    Name="tua_id" PropertyName="_Combo.SelectedValue" />
                <asp:ControlParameter ControlID="_txtNome" DbType="String" Name="uad_nome" PropertyName="Text" />
                <asp:ControlParameter ControlID="_txtCodigo" DbType="String" Name="uad_codigo" PropertyName="Text" />
                <asp:Parameter DbType="Byte" Name="uad_situacao" DefaultValue="0" />
                <asp:Parameter DbType="Guid" Name="uad_id" DefaultValue="00000000-0000-0000-0000-000000000000" />
                <asp:Parameter DbType="Boolean" Name="paginado" DefaultValue="true" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </fieldset>
&nbsp;</asp:Content>