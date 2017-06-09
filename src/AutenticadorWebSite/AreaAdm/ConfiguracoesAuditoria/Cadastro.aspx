<%@ Page Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true" Inherits="AreaAdm_ConfiguracoesAuditoria_Cadastro" Title="Untitled Page" Codebehind="Cadastro.aspx.cs" %>
<%@ PreviousPageType VirtualPath="~/AreaAdm/ConfiguracoesAuditoria/Busca.aspx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="_lblMessage" runat="server" EnableViewState="False"></asp:Label>
    <fieldset> 
        <legend>Configuração de auditoria - módulos</legend>
        <asp:GridView ID="_dgvModulo" runat="server" AutoGenerateColumns="False" DataKeyNames="mod_id"
           EmptyDataText="Não existem módulos cadastrados." 
            DataSourceID="_odsModulo" Width="600px">
            <Columns>
                <asp:BoundField DataField="mod_id" HeaderText="mod_id" InsertVisible="false" ReadOnly="True"
                    SortExpression="mod_id" Visible="false" />
                <asp:BoundField DataField="mod_nome" HeaderText="Módulo \ funcionalidade" SortExpression="mod_nome"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">             
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderText="Auditoria" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:CheckBox ID="_ckbAuditoria" runat="server" Checked='<%# Bind("mod_auditoria") %>' />                       
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <div class="right">
            <asp:Button ID="_btnSalvar" runat="server" Text="Salvar" 
                onclick="_btnSalvar_Click" />
            <asp:Button ID="_btnCancelar" runat="server" Text="Cancelar" 
                onclick="_btnCancelar_Click"  />                      
       </div>
    </fieldset>
    <asp:ObjectDataSource ID="_odsModulo" runat="server" DataObjectTypeName="Autenticador.Entities.SYS_Modulo"
        DeleteMethod="Delete" StartRowIndexParameterName="currentPage"
        MaximumRowsParameterName="pageSize" SelectCountMethod="GetTotalRecords" SelectMethod="GetSelect_by_Sis_id"
        TypeName="Autenticador.BLL.SYS_ModuloBO" 
        OldValuesParameterFormatString="original_{0}"
        UpdateMethod="Save" onselecting="_odsModulo_Selecting">      
    </asp:ObjectDataSource>
</asp:Content>