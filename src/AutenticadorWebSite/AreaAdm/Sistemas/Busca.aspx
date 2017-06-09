<%@ Page Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true" Inherits="AreaAdm_Sistemas_Busca" Title="Untitled Page" Codebehind="Busca.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="_lblMessage" runat="server" EnableViewState="False"></asp:Label>
    <fieldset>
        <legend>Listagem de sistemas</legend>
        <asp:GridView ID="_dgvSistemas" runat="server" AutoGenerateColumns="False" DataKeyNames="sis_id"
            EmptyDataText="Não existem sistemas cadastrados." DataSourceID="_odsSistemas"
            AllowPaging="True" OnRowDataBound="_dgvSistemas_RowDataBound">
            <Columns>
                <asp:BoundField DataField="sis_id" HeaderText="sis_id" InsertVisible="false" ReadOnly="True"
                    SortExpression="sis_id" Visible="false" />
                <asp:TemplateField HeaderText="Sistemas">
                    <ItemTemplate>
                        <asp:LinkButton ID="_btnAlterar" runat="server" Text='<%# Bind("sis_nome") %>' PostBackUrl="~/AreaAdm/Sistemas/Cadastro.aspx"
                            CommandName="Edit">
                        </asp:LinkButton>
                        <asp:Label ID="_lblAlterar" runat="server" Text='<%# Bind("sis_nome") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </fieldset>
    <asp:ObjectDataSource ID="_odsSistemas" runat="server" DataObjectTypeName="Autenticador.Entities.SYS_Sistema"
        DeleteMethod="Delete" EnablePaging="True" StartRowIndexParameterName="currentPage"
        MaximumRowsParameterName="pageSize" SelectCountMethod="GetTotalRecords" SelectMethod="GetSelect"
        TypeName="Autenticador.BLL.SYS_SistemaBO" OnSelecting="_odsSistemas_Selecting"
        UpdateMethod="Save">
        <SelectParameters>
            <asp:Parameter Name="sis_id" Type="Int32" />
            <asp:Parameter Name="sis_nome" Type="String"/>
            <asp:Parameter Name="sis_situacao" Type="Byte" />            
            <asp:Parameter Name="paginado" Type="Boolean" DefaultValue="true" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
