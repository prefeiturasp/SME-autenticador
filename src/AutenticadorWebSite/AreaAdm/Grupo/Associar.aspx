<%@ Page Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true" Inherits="AreaAdm_Grupo_Associar" Codebehind="Associar.aspx.cs" %>

<%@ PreviousPageType VirtualPath="~/AreaAdm/Grupo/Busca.aspx" %>
<%@ Register Src="../../WebControls/Busca/UCUsuario.ascx" TagName="UCUsuario" TagPrefix="uc1" %>
<%@ Register Src="../../WebControls/Busca/UCUA.ascx" TagName="UCUA" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="divBuscaUA" title="Busca de unidades administrativas" class="hide">
        <asp:UpdatePanel ID="_updBuscaUA" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <uc2:UCUA ID="UCUA1" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="divBuscaUsuario" title="Busca de usuários" class="hide">
        <asp:UpdatePanel ID="_updBuscaUsuario" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <uc1:UCUsuario ID="UCUsuario1" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="divUsuario" title="Associar usuário" class="hide">
        <asp:UpdatePanel ID="_updUsuario" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <fieldset>
                    <asp:Label ID="_lblMessageUsuario" runat="server" EnableViewState="False"></asp:Label>
                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Usuario" />
                    <asp:Label ID="Label4" runat="server" Text="Usuário *" AssociatedControlID="_txtUsuario"></asp:Label>
                    <input id="_txtUsu_id" runat="server" type="hidden" />
                    <asp:TextBox ID="_txtUsuario" runat="server" MaxLength="200" Enabled="False" SkinID="text60C"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="_rfvUsuario" runat="server" ValidationGroup="Usuario"
                        ControlToValidate="_txtUsuario" ErrorMessage="Usuário é obrigatório." Display="Dynamic">*</asp:RequiredFieldValidator>
                    <asp:ImageButton ID="_btnUsuario" runat="server" SkinID="btPesquisar" OnClick="_btnUsuario_Click" />
                    <div id="divUA" runat="server" visible="false" style="overflow: auto">
                        <asp:Label ID="_lblUA" runat="server" Text="Unidade administrativa *" AssociatedControlID="_txtUA"></asp:Label>
                        <input id="_txtEnt_id" runat="server" type="hidden" />
                        <input id="_txtUad_id" runat="server" type="hidden" />
                        <asp:TextBox ID="_txtUA" runat="server" MaxLength="200" Enabled="False" SkinID="text60C"></asp:TextBox>
                        <asp:ImageButton ID="_btnUA" runat="server" SkinID="btPesquisar" OnClick="_btnUA_Click" />
                        <div style="float: left; margin-right: 15px;">
                            <asp:Button ID="_btnAddUA" runat="server" Text="Adicionar" OnClick="_btnAddUA_Click"
                                ValidationGroup="Usuario" />
                            <br />
                            <asp:Button ID="_btnDelUA" runat="server" CausesValidation="False" Text="Remover"
                                OnClick="_btnDelUA_Click" />
                        </div>
                        <div style="float: left">
                            <asp:ListBox ID="_lstUAs" runat="server" Rows="8" Width="420px"></asp:ListBox>
                        </div>
                    </div>
                    <br class="clear" />
                    <asp:CheckBox ID="_ckbGrupo_Bloqueado" runat="server" Text="Bloqueado" />
                    <div class="right clear">
                        <asp:Button ID="_btnSalvarGrupo" runat="server" Text="Salvar" Width="100px" OnClick="_btnSalvarGrupo_Click"
                            ValidationGroup="Usuario" />
                        <asp:Button ID="_btnCancelarGrupo" runat="server" Text="Cancelar" Width="100px" CausesValidation="False"
                            OnClientClick="$('#divUsuario').dialog('close');" />
                    </div>
                </fieldset>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:UpdatePanel ID="_updMessage" runat="server">
        <ContentTemplate>
            <asp:Label ID="_lblMessage" runat="server" EnableViewState="False"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>
    <fieldset>
        <div>
            <asp:Label ID="Label3" runat="server" Text="Sistema:" SkinID="SubTitulo"></asp:Label>
            <asp:Label ID="_lblSistema" runat="server"></asp:Label>
            <br />
            <asp:Label ID="Label1" runat="server" Text="Grupo:" SkinID="SubTitulo"></asp:Label>
            <asp:Label ID="_lblGrupo" runat="server"></asp:Label>
            <br />
            <asp:Label ID="Label2" runat="server" Text="Visão:" SkinID="SubTitulo"></asp:Label>
            <asp:Label ID="_lbVisao" runat="server"></asp:Label>
        </div>
    </fieldset>
    <fieldset>        
        <asp:UpdatePanel ID="_updUsuarioGrid" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div>
                    <asp:Button ID="_btnNovo" runat="server" Text="Adicionar usuário" CausesValidation="False"
                        OnClick="_btnNovo_Click" />
                </div>
                <asp:GridView ID="_dgvUsuario" runat="server" AutoGenerateColumns="False" DataKeyNames="usu_id,usu_login"
                    EmptyDataText="Não existem usuários cadastrados." OnRowDataBound="_dgvUsuario_RowDataBound"
                    OnRowCommand="_dgvUsuario_RowCommand" AllowPaging="True" 
                    DataSourceID="odsUsuario">
                    <Columns>
                        <asp:TemplateField HeaderText="Usuário">
                            <ItemTemplate>
                                <asp:LinkButton ID="_btnAlterar" runat="server" CommandName="Alterar" Text='<%# Bind("usu_login") %>'
                                    CausesValidation="False"></asp:LinkButton>
                                <asp:Label ID="_lblAlterar" runat="server" Text='<%# Bind("usu_login") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="pes_nome" HeaderText="Nome da pessoa" />
                        <asp:TemplateField HeaderText="Unidade administrativa">
                            <ItemTemplate>
                                <ul>
                                    <asp:Repeater ID="_rptEntidadeUA" runat="server">
                                        <ItemTemplate>
                                            </li>
                                            <asp:Label ID="Label9" runat="server" Text='<%# Bind("ugu_nome") %>'></asp:Label>
                                            <br />
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="usg_situacaoDescricao" HeaderText="Bloqueado" HeaderStyle-CssClass="center"
                            ItemStyle-HorizontalAlign="Center" >
                        <HeaderStyle CssClass="center" />
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Excluir">
                            <ItemTemplate>
                                <asp:ImageButton ID="_btnExcluir" runat="server" CommandName="Excluir" SkinID="btExcluir"
                                    CausesValidation="False" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:ObjectDataSource ID="odsUsuario" runat="server" DataObjectTypeName="Autenticador.Entities.SYS_Usuario"
                    EnablePaging="True" MaximumRowsParameterName="pageSize" SelectCountMethod="GetTotalRecords"
                    SelectMethod="GetSelectBy_gru_id" StartRowIndexParameterName="currentPage" TypeName="Autenticador.BLL.SYS_UsuarioBO"
                    OnSelecting="odsUsuario_Selecting"></asp:ObjectDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
    </fieldset>
    <fieldset>
        <div class="right">
            <asp:Button ID="_btnVoltar" runat="server" Text="Voltar" CausesValidation="False"
                OnClick="_btnVoltar_Click" />
        </div>
    </fieldset>
</asp:Content>
