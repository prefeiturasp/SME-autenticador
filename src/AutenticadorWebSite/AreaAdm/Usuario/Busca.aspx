<%@ Page Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true" Inherits="AreaAdm_Usuario_Busca" Title="Untitled Page" Codebehind="Busca.aspx.cs" %>

<%@ Register Src="~/WebControls/Combos/UCComboEntidade.ascx" TagName="UCComboEntidade"
   TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <asp:Label ID="_lblMessage" runat="server" EnableViewState="False"></asp:Label>
   <fieldset>
      <legend>Consulta de usuários</legend>
      <div id="_divConsulta" runat="server">
         <uc1:UCComboEntidade ID="UCComboEntidade1" runat="server" />
         <asp:Label ID="Label1" runat="server" EnableViewState="False" Text="Login" AssociatedControlID="_txtLogin"></asp:Label>
         <asp:TextBox ID="_txtLogin" runat="server" SkinID="text30C" MaxLength="100"></asp:TextBox>
         <asp:Label ID="Label2" runat="server" EnableViewState="False" Text="E-mail" AssociatedControlID="_txtEmail"></asp:Label>
         <asp:TextBox ID="_txtEmail" runat="server" SkinID="text60C" MaxLength="200"></asp:TextBox>
         <asp:Label ID="Label3" runat="server" EnableViewState="False" Text="Situação" AssociatedControlID="_ddlSituacao"></asp:Label>
         <asp:DropDownList ID="_ddlSituacao" runat="server" SkinID="text30C">
            <asp:ListItem Value="0">-- Selecione uma opção --</asp:ListItem>
            <asp:ListItem Value="1">Ativo</asp:ListItem>
            <asp:ListItem Value="2">Bloqueado</asp:ListItem>
            <asp:ListItem Value="4">Padrão do sistema</asp:ListItem>
            <asp:ListItem Value="5">Senha expirada</asp:ListItem>
         </asp:DropDownList>          
         <asp:Label ID="Label4" runat="server" EnableViewState="False" Text="Nome da pessoa"
            AssociatedControlID="_txtPessoa"></asp:Label>
         <asp:TextBox ID="_txtPessoa" runat="server" SkinID="text60C" MaxLength="200"></asp:TextBox>
      </div>
      <div class="right">
         <asp:Button ID="_btnPesquisa" runat="server" Text="Pesquisar" OnClick="_btnPesquisa_Click" />
         <asp:Button ID="_btnNovo" runat="server" Text="Novo usuário" OnClick="_btnNovo_Click" />
      </div>
   </fieldset>
   <fieldset id="fdsResultado" runat="server">
      <legend>Resultados</legend>
      <asp:GridView ID="_dgvUsuario" runat="server" AllowPaging="True" AutoGenerateColumns="False"
         DataKeyNames="usu_id" DataSourceID="odsUsuarios" OnRowDataBound="dgvUsuario_RowDataBound"
         EmptyDataText="A pesquisa não encontrou resultados." OnRowCommand="_dgvUsuario_RowCommand">
         <Columns>
            <asp:BoundField DataField="ent_razaoSocial" HeaderText="Entidade" SortExpression="ent_razaoSocial" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
            <asp:TemplateField HeaderText="Login">
               <ItemTemplate>
                  <asp:LinkButton ID="_lkbAlterar" runat="server" CommandName="Edit" Text='<%# Bind("usu_login") %>'
                     PostBackUrl="~/AreaAdm/Usuario/Cadastro.aspx"></asp:LinkButton>
                  <asp:Label ID="_lblAlterar" runat="server" Text='<%# Bind("usu_login") %>' Visible="False"></asp:Label>
               </ItemTemplate>
               <EditItemTemplate>
                  <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("usu_login") %>'></asp:TextBox>
               </EditItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="usu_email" HeaderText="E-Mail" />
            <asp:BoundField DataField="pes_nome" HeaderText="Nome da pessoa" />
            <asp:BoundField DataField="usu_situacaoNome" HeaderText="Situação" ItemStyle-HorizontalAlign="Center"
               HeaderStyle-CssClass="center" />
            <asp:TemplateField HeaderText="Excluir" HeaderStyle-CssClass="center">
               <ItemTemplate>
                  <asp:ImageButton ID="_btnExcluir" runat="server" CommandName="Deletar" SkinID="btExcluir" />
               </ItemTemplate>
            </asp:TemplateField>
         </Columns>
      </asp:GridView>
   </fieldset>
   <asp:ObjectDataSource ID="odsUsuarios" runat="server" DataObjectTypeName="Autenticador.Entities.SYS_Usuario"
      DeleteMethod="Delete" EnablePaging="True" MaximumRowsParameterName="pageSize"
      SelectCountMethod="GetTotalRecords" SelectMethod="GetSelect" StartRowIndexParameterName="currentPage"
      TypeName="Autenticador.BLL.SYS_UsuarioBO" OnSelecting="odsUsuarios_Selecting">
      <SelectParameters>
         <asp:ControlParameter ControlID="UCComboEntidade1" DbType="Guid" Name="ent_id" PropertyName="_Combo.SelectedValue" />
         <asp:ControlParameter ControlID="_txtLogin" DbType="String" Name="login" PropertyName="Text"
            Size="100" /> 
         <asp:ControlParameter ControlID="_txtEmail" DbType="String" Name="email" PropertyName="Text"
            Size="200" />
         <asp:ControlParameter ControlID="_ddlSituacao" DbType="Byte" Name="bloqueado" PropertyName="SelectedValue"
            Size="1" />
         <asp:ControlParameter ControlID="_txtPessoa" DbType="String" Name="pessoa" PropertyName="Text"
            Size="200" />
      </SelectParameters>
   </asp:ObjectDataSource>
</asp:Content>
