<%@ Page Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="AreaAdm_Modulos_Cadastro" Title="Untitled Page" %>

<%@ Register Assembly="Autenticador.Web.WebProject" Namespace="Autenticador.Web.WebProject"
    TagPrefix="cc1" %>

<%@ Register Src="../../WebControls/Combos/UCComboSistema.ascx" TagName="UCComboSistema"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Inserção/Alteração de SiteMap -->
    <div id="divAddSiteMap" title="Cadastro de SiteMap" class="hide">
        <asp:UpdatePanel ID="_updDetalhesSiteMap" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <fieldset>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="SiteMap" />
                    <asp:HiddenField ID="_txt_msm_IsNew" runat="server" />
                    <asp:HiddenField ID="_txt_msm_id" runat="server" />
                    <asp:Label ID="Label3" runat="server" Text="Nome *" AssociatedControlID="_txt_msm_nome"></asp:Label>
                    <asp:TextBox ID="_txt_msm_nome" runat="server" MaxLength="50" SkinID="text60C"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Nome é obrigatório."
                        ControlToValidate="_txt_msm_nome" ValidationGroup="SiteMap" ForeColor="Red">*</asp:RequiredFieldValidator>
                    <asp:Label ID="Label7" runat="server" Text="Descrição" AssociatedControlID="_txt_msm_descricao"></asp:Label>
                    <asp:TextBox ID="_txt_msm_descricao" runat="server" TextMode="MultiLine" MaxLength="1000"
                        SkinID="text60C"></asp:TextBox>
                    <asp:Label ID="Label6" runat="server" Text="URL" AssociatedControlID="_txt_msm_url"></asp:Label>
                    <asp:TextBox ID="_txt_msm_url" runat="server" MaxLength="500" SkinID="text60C"></asp:TextBox>
                    <asp:Label ID="Label5" runat="server" Text="Informações" AssociatedControlID="_txt_msm_informacoes"></asp:Label>
                    <asp:TextBox ID="_txt_msm_informacoes" runat="server" TextMode="MultiLine" SkinID="text60C"></asp:TextBox>
                    <asp:Label ID="Label8" runat="server" Text="URL do help" AssociatedControlID="_txt_msm_urlHelp"></asp:Label>
                    <asp:TextBox ID="_txt_msm_urlHelp" runat="server" MaxLength="500" SkinID="text60C"></asp:TextBox>
                    <div class="right">
                        <asp:Button ID="_btnSalvar_SiteMap" runat="server" Text="Salvar" OnClick="_btnSalvar_SiteMap_Click"
                            ValidationGroup="SiteMap" />
                        <asp:Button ID="_btnCancelar_SiteMap" runat="server" Text="Cancelar" CausesValidation="false"
                            OnClientClick="$('#divAddSiteMap').dialog('close');" />
                    </div>
                </fieldset>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <%--Seleção do sistema.--%>
    <div id="_divConsulta" runat="server">
        <asp:Label ID="_lblMessageAcima" runat="server"></asp:Label>
        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Pesquisa" />
        <fieldset>
            <legend>Consulta de Módulos</legend>
            <uc1:UCComboSistema ID="UCComboSistema1" runat="server" />
            <div align="right">
                <asp:Button ID="_btnBuscar" runat="server" Text="Pesquisar" OnClick="_btnBuscar_Click"
                    ValidationGroup="Pesquisa" />
                <asp:Button ID="btnLimpaCache" runat="server" Text="Limpar Cache" OnClick="_btnLimpaCache_Click"
                    ValidationGroup="Pesquisa" Visible="false"/>
            </div>
        </fieldset>
    </div>
    <%--TreeView com os módulos do sistema selecionado.--%>
    <div id="_divResultado" runat="server" visible="false">
        <fieldset>
            <%--Botões para adicionar e excluir módulo.--%>
            <asp:UpdatePanel ID="_updModulos" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Label ID="_lblMessage" runat="server" EnableViewState="false"></asp:Label>
                    <asp:Button ID="_btnSalvarModulo" runat="server" Text="Salvar módulo" Visible="false"
                        OnClick="_btnSalvarModulo_Click" />
                    <asp:Button ID="_btnCancelarModulo" runat="server" Text="Cancelar" Visible="false"
                        OnClick="_btnCancelarModulo_Click" />
                    <asp:Button ID="_btnNovo" runat="server" Text="Novo módulo" OnClick="_btnNovo_Click" />
                    <fieldset style="height: 500px; padding:0;">
                        <div style="height: 490px; width: 200px; background: #ccc; overflow: auto; border-right: 2px groove #fff;
                            float: left; padding: 10px 10px 0 10px;">
                            <cc1:CustomTreeView runat="server" ID="trvModulos" OnSelectedNodeChanged="trvModulos_SelectedNodeChanged"
                                SkipLinkText="" ShowCheckBoxes="None" SelectedNodeStyle-BackColor="#CCCCCC" ImageSet="Arrows"
                                ShowLines="False" NodeIndent="10" ForeColor="#000">
                            </cc1:CustomTreeView>
                        </div>
                        <div style="height: 500px; overflow: auto; padding: 10px 10px 0 10px;">
                            <%--Detalhes do módulo, com opção de alteração e exclusão. Também disponibiza a opção de "Novo" módulo.--%>
                            <div id="divModulo" runat="server" visible="false">
                                <asp:UpdatePanel ID="_updDetalhesModulo" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:HiddenField ID="_txt_mod_idPai" runat="server" />
                                        <%--<asp:HiddenField ID="_txt_mod_id" runat="server" />--%>
                                        <asp:Label ID="Label4" runat="server" Text="ID" AssociatedControlID="_txt_mod_id"></asp:Label>
                                        <asp:TextBox ID="_txt_mod_id" ReadOnly="true" Enabled="false" runat="server" CssClass="text30C"></asp:TextBox>
                                        <asp:Label ID="Label1" runat="server" Text="Nome" AssociatedControlID="_txt_mod_nome"></asp:Label>
                                        <asp:TextBox ID="_txt_mod_nome" runat="server" CssClass="text30C"></asp:TextBox>
                                        <asp:Label ID="Label2" runat="server" Text="Descrição" AssociatedControlID="_txt_mod_descricao"></asp:Label>
                                        <asp:TextBox ID="_txt_mod_descricao" runat="server" CssClass="text30C" TextMode="MultiLine"></asp:TextBox>
                                        <asp:CheckBox ID="_ckb_mod_auditoria" runat="server" Text="Auditoria" />
                                        <hr />
                                        <div style="overflow: auto">
                                            <div style="float: left; width: 195px; height: 100px">
                                                <asp:ListBox ID="_lbxSelecionaVisao" runat="server" DataValueField="vis_id" DataTextField="vis_nome"
                                                    AutoPostBack="true" Height="80px" Width="195px"></asp:ListBox>
                                            </div>
                                            <div style="float: left; width: 50px; padding-top: 10px; text-align: center">
                                                <asp:Button ID="_btnAdicionarVisao" runat="server" Text=">" ToolTip="Adicionar" OnClick="_btnAdicionarVisao_Click"
                                                    Style="width: 22px" /><br />
                                                <asp:Button ID="_btnRemoverVisao" runat="server" Text="<" ToolTip="Remover" OnClick="_btnRemoverVisao_Click" />
                                            </div>
                                            <div style="float: left; width: 195px; height: 100px">
                                                <asp:ListBox ID="_lbxVisao" runat="server" DataValueField="vis_id" DataTextField="vis_nome"
                                                    AutoPostBack="true" Height="80px" Width="195px"></asp:ListBox>
                                            </div>
                                        </div>
                                        <hr />
                                        <h4>
                                            SiteMaps</h4>
                                        <asp:Button ID="_lkbAdicionar_SiteMap" runat="server" OnClick="_lkbAdicionar_SiteMap_Click"
                                            Text="Adicionar SiteMap"></asp:Button>
                                        <asp:UpdatePanel ID="_updSiteMap" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:GridView ID="_gdvSiteMap" runat="server" AutoGenerateColumns="False" DataKeyNames="sis_id,mod_id,msm_id"
                                                    EmptyDataText="Não existem SiteMaps cadastrados." Width="100%" OnRowCommand="_gdvSiteMap_RowCommand"
                                                    OnRowDataBound="_gdvSiteMap_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="msm_id" HeaderText="msm_id" InsertVisible="false" ReadOnly="True"
                                                            SortExpression="msm_id" Visible="False" />
                                                        <asp:TemplateField HeaderText="" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:RadioButton runat="server" ID="_rdb_Menu" GroupName="_rdb_GroupMenu" />                                                                
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Nome" HeaderStyle-Width="200px">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="_lkbAlterarSiteMap" runat="server" Text='<%# Bind("msm_nome") %>'
                                                                    CommandName="Alterar" ToolTip='<%# Bind("msm_descricao") %>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="200px" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="msm_url" HeaderText="URL" SortExpression="msm_url" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="250px">
                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Excluir" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                            HeaderStyle-Width="50px">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="_btnExcluir" SkinID="btExcluir" CommandName="Excluir" runat="server" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <div align="right">
                                            <asp:Button ID="_btnSalvar" runat="server" Text="Salvar" OnClick="_btnSalvar_Click" />
                                            <asp:Button ID="_btnExcluir" runat="server" Text="Excluir" OnClick="_btnExcluir_Click" />
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </fieldset>
                    <input id="Hidden1" type="hidden" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </fieldset>
    </div>
</asp:Content>
