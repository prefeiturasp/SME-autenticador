<%@ Page Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true" Inherits="AreaAdm_ConfiguracoesAuditoria_Busca" Title="Untitled Page" Codebehind="Busca.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="_lblMessage" runat="server" EnableViewState="False"></asp:Label>
    <fieldset>
        <legend>Configuração de auditoria - sistemas</legend>
        <asp:GridView ID="_dgvSistema" runat="server" AutoGenerateColumns="False" DataKeyNames="sis_id"
            EmptyDataText="Não existem sistemas cadastrados." DataSourceID="_odsSistema"
            AllowPaging="True" OnRowDataBound="_dgvSistema_RowDataBound">
            <Columns>
                <asp:BoundField DataField="sis_id" HeaderText="sis_id" InsertVisible="false" ReadOnly="True"
                    SortExpression="sis_id" Visible="false" />
                <asp:TemplateField HeaderText="Sistemas">
                    <ItemTemplate>
                        <asp:LinkButton ID="_btnAlterar" runat="server" Text='<%# Bind("sis_nome") %>' PostBackUrl="~/AreaAdm/ConfiguracoesAuditoria/Cadastro.aspx"
                            CommandName="Edit"></asp:LinkButton>
                        <asp:Label ID="_lblAlterar" runat="server" Text='<%# Bind("sis_nome") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </fieldset>
    <asp:ObjectDataSource ID="_odsSistema" runat="server" DataObjectTypeName="Autenticador.Entities.SYS_Sistema"
        DeleteMethod="Delete" EnablePaging="True" StartRowIndexParameterName="currentPage"
        MaximumRowsParameterName="pageSize" SelectCountMethod="GetTotalRecords" SelectMethod="GetSelectBy_ModuloVinculado"
        TypeName="Autenticador.BLL.SYS_SistemaBO" OnSelecting="_odsSistema_Selecting"
        UpdateMethod="Save">
        <SelectParameters>
            <asp:Parameter Name="paginado" Type="Boolean" DefaultValue="true" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
