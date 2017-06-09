<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCUsuario.ascx.cs" Inherits="Autenticador.UserControlLibrary.Buscas.UCUsuario" %>
<%@ Register Src="../Combos/UCComboEntidade.ascx" TagName="UCComboEntidade" TagPrefix="uc1" %>
<fieldset>    
    <div id="_divConsulta" runat="server">
        <uc1:UCComboEntidade ID="UCComboEntidade1" runat="server" />
        <asp:Label ID="Label1" runat="server" EnableViewState="False" Text="Login" AssociatedControlID="_txtLogin"></asp:Label>
        <asp:TextBox ID="_txtLogin" runat="server" SkinID="text30C"></asp:TextBox>
        <asp:Label ID="Label3" runat="server" EnableViewState="False" Text="Nome da pessoa"
            AssociatedControlID="_txtPessoa"></asp:Label>
        <asp:TextBox ID="_txtPessoa" runat="server" SkinID="text60C"></asp:TextBox>
    </div>
    <div class="right">
        <asp:Button ID="_btnPesquisar" runat="server" Text="Pesquisar" OnClick="_btnPesquisar_Click"
            CausesValidation="False" />
    </div>
    <BR />
    <fieldset id="fdsResultados" runat="server">
        <legend>Resultados</legend>
        <asp:GridView ID="_dgvUsuario" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            DataKeyNames="ent_id,usu_id,usu_login" DataSourceID="odsUsuarios" EmptyDataText="A pesquisa não encontrou resultados."
            OnRowEditing="_dgvUsuario_RowEditing">
            <Columns>
                <asp:BoundField DataField="ent_razaoSocial" HeaderText="Entidade" SortExpression="ent_razaoSocial"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderText="Login">
                    <ItemTemplate>
                        <asp:LinkButton ID="_lkbSelect" runat="server" Text='<%# Bind("usu_login") %>' CausesValidation="False"
                            CommandName="Edit"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>                
                <asp:BoundField DataField="pes_nome" HeaderText="Nome da pessoa" />
            </Columns>
        </asp:GridView>
    </fieldset>
    <asp:ObjectDataSource ID="odsUsuarios" runat="server" DataObjectTypeName="Autenticador.Entities.SYS_Usuario"
        DeleteMethod="Delete" EnablePaging="True" MaximumRowsParameterName="pageSize"
        SelectCountMethod="GetTotalRecords" SelectMethod="GetSelect_Ativos" StartRowIndexParameterName="currentPage"
        TypeName="Autenticador.BLL.SYS_UsuarioBO" OnSelecting="odsUsuarios_Selecting">
    </asp:ObjectDataSource>
</fieldset>
