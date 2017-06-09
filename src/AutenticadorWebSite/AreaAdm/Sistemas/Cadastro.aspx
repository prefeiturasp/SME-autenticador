<%@ Page Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true" Inherits="AreaAdm_Sistemas_Cadastro" Title="Untitled Page" Codebehind="Cadastro.aspx.cs" %>

<%@ Register Src="../../WebControls/Combos/UCComboEntidade.ascx" TagName="UCComboEntidade" TagPrefix="uc4" %>
<%@ PreviousPageType VirtualPath="~/AreaAdm/Sistemas/Busca.aspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <div id="divEntidades" title="Cadastro de entidades" class="hide">
      <asp:UpdatePanel ID="_uppCadastroSistemaEntidade" runat="server" UpdateMode="Conditional">
         <ContentTemplate>
            <asp:Label ID="_lbMessageEntidades" runat="server" EnableViewState="False"></asp:Label>
            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="_vgCadastroSistemaEntidades" />
            <fieldset>
               <uc4:UCComboEntidade ID="UCComboEntidade1" runat="server" />
               <asp:Label ID="_lblChavek1" runat="server" Text="Chave K1" AssociatedControlID="_txtChaveK1"></asp:Label>
               <asp:TextBox ID="_txtChaveK1" runat="server" SkinID="text60C" MaxLength="100"></asp:TextBox>
               <asp:Label ID="_lblUrlAcesso" runat="server" Text="URL de acesso" AssociatedControlID="_txtUrlAcesso"></asp:Label>
               <asp:TextBox ID="_txtUrlAcesso" runat="server" SkinID="text60C" MaxLength="100"></asp:TextBox>
               <asp:Label ID="lblUrlCliente" runat="server" Text="URL do site da entidade" AssociatedControlID="txtUrlCliente"></asp:Label>
               <asp:TextBox ID="txtUrlCliente" runat="server" SkinID="text60C" MaxLength="200"></asp:TextBox>
               <fieldset>
                  <asp:Label ID="lblLogoCliente" runat="server" Text="Logotipo da entidade" AssociatedControlID="fupLogoCliente"></asp:Label>
                  <h2 class="logoInstitiuicao">
                     <asp:Image ID="imgLogoCliente" runat="server" />
                  </h2>
                  <asp:FileUpload ID="fupLogoCliente" runat="server" />
               </fieldset>
               <div class="right">
                  <asp:Button ID="_btnSalvarE" runat="server" Text="Salvar" OnClick="_btnSalvarE_Click" ValidationGroup="_vgCadastroSistemaEntidades" />
                  <asp:Button ID="_btnCancelarE" runat="server" Text="Cancelar" CausesValidation="false" OnClick="_btnCancelarE_Click" />
               </div>
            </fieldset>
         </ContentTemplate>
         <Triggers>
            <asp:PostBackTrigger ControlID="_btnSalvarE" />
         </Triggers>
      </asp:UpdatePanel>
   </div>
   <asp:UpdatePanel ID="_uppCadastroSistema" runat="server" UpdateMode="Conditional" RenderMode="Inline">
      <ContentTemplate>
         <asp:Label ID="_lblMessage" runat="server" EnableViewState="False"></asp:Label>
         <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="_vgCadastroSistemas" />
         <fieldset>
            <legend>Cadastro de sistemas</legend>
            <asp:Label ID="_lblsisnome" runat="server" Text="Nome" AssociatedControlID="_txtsisnome"></asp:Label>
            <asp:TextBox ID="_txtsisnome" runat="server" SkinID="text60C" MaxLength="100"></asp:TextBox>
            <asp:RequiredFieldValidator ID="_rfvSisNome" runat="server" ControlToValidate="_txtsisnome" Display="Dynamic" ErrorMessage="Nome do sistema é obrigatório." ValidationGroup="_vgCadastroSistemas">*</asp:RequiredFieldValidator>
            <asp:Label ID="_lblsiscaminho" runat="server" Text="Caminho" AssociatedControlID="_txtsiscaminho"></asp:Label>
            <asp:TextBox ID="_txtsiscaminho" runat="server" SkinID="text60C" MaxLength="100"></asp:TextBox>
            <asp:Label ID="_lblsiscaminhoLogout" runat="server" Text="Caminho logout" AssociatedControlID="_txtsiscaminhoLogout"></asp:Label>
            <asp:TextBox ID="_txtsiscaminhoLogout" runat="server" SkinID="text60C" MaxLength="100"></asp:TextBox>
            <asp:Label ID="_lblsisDescricao" runat="server" Text="Descrição" AssociatedControlID="_txtsisDescricao"></asp:Label>
            <asp:TextBox ID="_txtsisDescricao" runat="server" SkinID="text60C" Columns="100" Rows="5" TextMode="MultiLine"></asp:TextBox>
            <asp:Label ID="_lblurlintegracao" runat="server" Text="URL integração" AssociatedControlID="_txturlintegracao"></asp:Label>
            <asp:TextBox ID="_txturlintegracao" runat="server" SkinID="text60C" MaxLength="100"></asp:TextBox>
            <asp:CheckBox ID="_ckbOcultaLogo" runat="server" CssClass="checkbox" Text="Ocultar módulo da lista de sistemas e aplicativos" />

         </fieldset>
      </ContentTemplate>
   </asp:UpdatePanel>
   <fieldset>
      <legend>Imagens</legend>
       <div class="clear"></div>
      <div style="float: left;">
         <asp:Label ID="lblImagemSistema" runat="server" Text="Imagem do sistema:" AssociatedControlID="fupImagemSistema"></asp:Label>
         <h2 class="logoInstitiuicao">
            <asp:Image ID="imgImagemSistema" runat="server" />
         </h2>
         <asp:FileUpload ID="fupImagemSistema" runat="server" />
      </div>
      <div style="float: left; margin-left: 60px;">
         <asp:Label ID="lblLogoCabecalho" runat="server" Text="Logotipo do cabeçalho:" AssociatedControlID="fupLogoCabecalho"></asp:Label>
         <h2 class="logoInstitiuicao">
            <asp:Image ID="imgLogoCabecalho" runat="server" />
         </h2>
         <asp:FileUpload ID="fupLogoCabecalho" runat="server" />
      </div>
      <div class="clear">
      </div>
   </fieldset>
   <asp:UpdatePanel ID="_uppGridSistemaEntidade" runat="server" UpdateMode="Conditional">
      <ContentTemplate>
         <fieldset>
            <legend>Entidades ligadas</legend>
             <div></div>
            <asp:Button ID="_btnAddEntidade" runat="server" Text="Adicionar entidade" OnClick="_btnAddEntidade_Click" CausesValidation="False" />
            <asp:GridView ID="_dgvSistemaEntidade" runat="server" AutoGenerateColumns="False" OnRowCommand="_dgvSistemaEntidade_RowCommand" DataKeyNames="sis_id,ent_id" OnRowDataBound="_dgvSistemaEntidade_RowDataBound" EmptyDataText="Não existem entidades cadastradas.">
               <Columns>
                  <asp:TemplateField HeaderText="Nome">
                     <ItemTemplate>
                        <asp:LinkButton ID="_lkbAlterar" runat="server" CommandName="Editar" Text='<%# Bind("ent_razaoSocial") %>'></asp:LinkButton>
                     </ItemTemplate>
                  </asp:TemplateField>
                  <asp:BoundField DataField="sen_chaveK1" HeaderText="Chave K1" SortExpression="sen_chaveK1" />
                  <asp:BoundField DataField="sen_urlAcesso" HeaderText="URL acesso" SortExpression="sen_urlAcesso" />
                  <asp:BoundField DataField="sen_urlCliente" HeaderText="URL da entidade" SortExpression="sen_urlCliente" />
                  <asp:TemplateField HeaderText="Logotipo do site da entidade">
                     <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Eval("sen_logoCliente") %>'></asp:TextBox>
                     </EditItemTemplate>
                     <ItemTemplate>
                        <asp:Image ID="_imgLogo" runat="server" />
                     </ItemTemplate>
                     <HeaderStyle CssClass="center" />
                     <ItemStyle HorizontalAlign="Center" />
                  </asp:TemplateField>
                  <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Excluir" ItemStyle-HorizontalAlign="Center">
                     <ItemTemplate>
                        <asp:ImageButton ID="_btnExcluir" runat="server" CommandName="Deletar" SkinID="btExcluir" />
                     </ItemTemplate>
                     <HeaderStyle HorizontalAlign="Center" />
                     <ItemStyle HorizontalAlign="Center" />
                  </asp:TemplateField>
               </Columns>
            </asp:GridView>
         </fieldset>
      </ContentTemplate>
   </asp:UpdatePanel>
   <fieldset>
      <div class="right">
         <asp:Button ID="_btnSalvar" runat="server" Text="Salvar" OnClick="_btnSalvar_Click" ValidationGroup="_vgCadastroSistemas" />
         <asp:Button ID="_btnCancelar" runat="server" Text="Cancelar" OnClick="_btnCancelar_Click" CausesValidation="false" />
      </div>
   </fieldset>
</asp:Content>
