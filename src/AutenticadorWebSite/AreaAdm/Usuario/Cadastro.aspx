<%@ Page Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true" Inherits="AreaAdm_Usuario_Cadastro" Title="Untitled Page" CodeBehind="Cadastro.aspx.cs" %>

<%@ PreviousPageType VirtualPath="~/AreaAdm/Usuario/Busca.aspx" %>
<%@ Register Src="~/WebControls/Combos/UCComboEntidade.ascx" TagName="UCComboEntidade"
    TagPrefix="uc1" %>
<%@ Register Src="../../WebControls/Busca/UCUA.ascx" TagName="UCUA" TagPrefix="uc2" %>
<%@ Register Src="../../WebControls/Busca/UCPessoas.ascx" TagName="UCPessoas" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="divBuscaPessoa" title="Busca de pessoas" class="hide">
        <asp:UpdatePanel ID="_updBuscaPessoas" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <uc3:UCPessoas ID="UCPessoas1" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="divBuscaUA" title="Busca de unidades administrativas" class="hide">
        <asp:UpdatePanel ID="_updBuscaUA" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <uc2:UCUA ID="UCUA1" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:UpdatePanel ID="_updUsuario" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Label ID="_lblMessage" runat="server" EnableViewState="False"></asp:Label>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Usuario" />
            <fieldset>
                <legend>Cadastro de usuários</legend>
                <uc1:UCComboEntidade ID="UCComboEntidadeUsuario" runat="server" />
                <div id="divUsuarioAD" runat="server">
                    <asp:Label ID="lblUsuarioAD" runat="server" Text="Integração com AD *" AssociatedControlID="ddlUsuarioAD"></asp:Label>
                    <asp:DropDownList ID="ddlUsuarioAD" runat="server" SkinID="text60C" OnSelectedIndexChanged="ddlUsuarioAD_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    <asp:CompareValidator ID="cpvUsuarioAD" runat="server" ErrorMessage="Tipo de integração com AD é obrigatório."
                        ControlToValidate="ddlUsuarioAD" Operator="NotEqual" ValueToCompare="-1" Display="Dynamic" ValidationGroup="Usuario">*</asp:CompareValidator>
                </div>
                <asp:CheckBox ID="_ckbUsuarioLive" runat="server" Text="Integrar usuário live"
                    AutoPostBack="True" OnCheckedChanged="_ckbUsuarioLive_ChangeChecked" />
                <div id="divDominios" visible="false" runat="server">
                    <asp:Label ID="Label" runat="server" Text="Domínio *" AssociatedControlID="_ddlDominios"></asp:Label>
                    <asp:DropDownList ID="_ddlDominios" runat="server" AutoPostBack="True" SkinID="text30C"
                        OnSelectedIndexChanged="_ddlDominios_SelectedIndexChanged">
                    </asp:DropDownList>
                    <div id="divOutrosDominios" visible="false" runat="server">
                        <asp:Label ID="Label5" runat="server" Text="Outro domínio" AssociatedControlID="_txtDominio"></asp:Label>
                        <asp:TextBox ID="_txtDominio" runat="server" MaxLength="100"></asp:TextBox>
                    </div>
                </div>
                <asp:Label ID="_lblLogin" runat="server" Text="Login *" AssociatedControlID="_txtLogin"></asp:Label>
                <asp:TextBox ID="_txtLogin" runat="server" MaxLength="100" SkinID="text30C"></asp:TextBox>
                <asp:RequiredFieldValidator ID="_rfvLogin" runat="server" ControlToValidate="_txtLogin"
                    ValidationGroup="Usuario" ErrorMessage="Login é obrigatório." Display="Dynamic">*</asp:RequiredFieldValidator>
                <asp:Label ID="_lblEmail" runat="server" Text="E-mail * ( seuEmail@seuProvedor )" AssociatedControlID="_txtEmail"></asp:Label>
                <asp:TextBox ID="_txtEmail" runat="server" MaxLength="200" SkinID="text60C"></asp:TextBox>
                <asp:RequiredFieldValidator ID="_rfvEmail" runat="server" ControlToValidate="_txtEmail"
                    ValidationGroup="Usuario" ErrorMessage="E-mail é obrigatório." Display="Dynamic">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="_revEmail" runat="server" ControlToValidate="_txtEmail"
                    ValidationGroup="Usuario" ErrorMessage="E-mail está fora do padrão ( seuEmail@seuProvedor )." Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>

                <div id="divIntegracaoExterna" runat="server">
                    <asp:CheckBox ID="chkIntegracaoExterna" AutoPostBack="true" OnCheckedChanged="chkIntegracaoExterna_OnCheckedChanged" runat="server" Checked="false" Text="Realizar Integração por WebService Externo" />
                    <asp:DropDownList ID="ddlIntegracaoExternaTipo" runat="server">
                        <asp:ListItem Selected="True" Value="-1" Text=" Selecione uma Integração"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div id="divOpcoesSenha" runat="server">
                    <asp:CheckBox ID="_chkSenhaAutomatica" Text="Gerar senha e enviar para o e-mail"
                        runat="server" AutoPostBack="True" OnCheckedChanged="_chkSenhaAutomatica_CheckedChanged" />
                    <asp:Label ID="_lblSenha" runat="server" Text="Senha *" AssociatedControlID="_txtSenha"></asp:Label>
                    <asp:TextBox ID="_txtSenha" runat="server" TextMode="Password" SkinID="text20C" EnableViewState="true"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="_rfvSenha" runat="server" ControlToValidate="_txtSenha"
                        ValidationGroup="Usuario" ErrorMessage="Senha é obrigatório." Display="Dynamic">*</asp:RequiredFieldValidator>
                    <%-- <asp:RegularExpressionValidator ID="_revSenha" runat="server" ControlToValidate="_txtSenha"
                        ValidationGroup="Usuario" Display="Dynamic" ErrorMessage="A senha deve conter pelo menos uma combinação de letras e números ou letras maiúsculas e minúsculas ou algum caracter especial (!, @, #, $, %, &amp;) somados a letras e/ou números."
                        ValidationExpression="((([0-9]+[a-z]+)|([0-9]+[A-Z]+)|([0-9]+[!@#$%&amp;]+)).*)|((([a-z]+[0-9]+)|([a-z]+[A-Z]+)|([a-z]+[!@#$%&amp;]+)).*)|((([A-Z]+[0-9]+)|([A-Z]+[a-z]+)|([A-Z]+[!@#$%&amp;]+)).*)|((([!@#$%&amp;]+[0-9]+)|([!@#$%&amp;]+[a-z]+)|([!@#$%&amp;]+[A-Z]+)).*)">*</asp:RegularExpressionValidator>--%>
                    <asp:RegularExpressionValidator ID="revSenha" runat="server" ControlToValidate="_txtSenha"
                        ValidationGroup="Usuario" Display="Dynamic" ErrorMessage="A senha não pode conter espaços em branco."
                        ValidationExpression="[^\s]+" Enabled="false">*</asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator ID="revSenhaTamanho" runat="server" ControlToValidate="_txtSenha"
                        ValidationGroup="Usuario" Display="Dynamic" ErrorMessage="A senha deve conter {0}."
                        Enabled="false">*</asp:RegularExpressionValidator>
                    <asp:Label ID="_lblConfirmacao" runat="server" Text="Confirmar senha *" AssociatedControlID="_txtConfirmacao"></asp:Label>
                    <asp:TextBox ID="_txtConfirmacao" runat="server" MaxLength="256" TextMode="Password"
                        SkinID="text20C" EnableViewState="true"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="_rfvConfirmarSenha" runat="server" ControlToValidate="_txtConfirmacao"
                        ValidationGroup="Usuario" ErrorMessage="Confirmar senha é obrigatório." Display="Dynamic">*</asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="_cpvConfirmarSenha" runat="server" ControlToCompare="_txtConfirmacao"
                        ValidationGroup="Usuario" ControlToValidate="_txtSenha" ErrorMessage="Senha não confere."
                        Display="Dynamic">*</asp:CompareValidator>
                    <asp:CheckBox ID="_chkExpiraSenha" Text="Expira senha" runat="server" />


                    <div id="divContasExternas" runat="server" visible="False">
                        <asp:Repeater ID="rptContasExternas" runat="server" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblLoginProvider" runat="server" Text="Login provider externo" AssociatedControlID="txtLoginProvider"></asp:Label>
                                <asp:TextBox ID="txtLoginProvider" runat="server" Text='<%# Bind("LoginProvider") %>' SkinID="text30C" Enabled="false"></asp:TextBox>
                                <asp:Label ID="lblUserName" runat="server" Text="Login" AssociatedControlID="txtLoginProvider"></asp:Label>
                                <asp:TextBox ID="txtUserName" runat="server" Text='<%# Bind("Username") %>' SkinID="text30C" Enabled="false"></asp:TextBox>
                            </ItemTemplate>
                            <SeparatorTemplate>
                                <br />
                            </SeparatorTemplate>
                        </asp:Repeater>
                        <asp:Label runat="server" ID="lblInfoContaExterna" Visible="False"></asp:Label>
                    </div>
                </div>
                <asp:CheckBox ID="_chkBloqueado" Text="Bloqueado" runat="server" />
                <asp:Label ID="Label7" runat="server" Text="Pessoa" AssociatedControlID="_txtPessoa"></asp:Label>
                <input id="_txtPes_id" runat="server" type="hidden" />
                <asp:TextBox ID="_txtPessoa" runat="server" MaxLength="200" SkinID="text60C" Enabled="False"></asp:TextBox>
                <asp:ImageButton ID="_btnPessoa" runat="server" CausesValidation="False"
                    SkinID="btPesquisar" OnClick="_btnPessoa_Click" />
                <input type="hidden" id="_txtCriptografia" runat="server" />
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
    <fieldset>
        <legend>Cadastro de grupos</legend>
        <asp:UpdatePanel ID="updGruposGrid" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div>
                    <asp:Button ID="btnNovoGrupo" runat="server" Text="Adicionar grupo" CausesValidation="False"
                        OnClick="btnNovoGrupo_Click" />
                </div>
                <asp:GridView ID="_dgvGrupo" runat="server" AutoGenerateColumns="False" DataKeyNames="gru_id"
                    EmptyDataText="Não existem grupos cadastrados." OnRowDataBound="_dgvGrupo_RowDataBound"
                    OnRowEditing="_dgvGrupo_RowEditing" OnRowDeleting="_dgvGrupo_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="gru_id" HeaderText="gru_id" HeaderStyle-CssClass="hide"
                            ItemStyle-CssClass="hide" />
                        <asp:BoundField DataField="usg_situacao" HeaderText="usg_situacao" HeaderStyle-CssClass="hide"
                            ItemStyle-CssClass="hide" />
                        <asp:TemplateField HeaderText="Grupo">
                            <ItemTemplate>
                                <asp:LinkButton ID="_lkbSelect" runat="server" CommandName="Edit" Text='<%# Bind("grupo") %>'
                                    OnClientClick="$('#divAddGrupos').dialog('open');" CausesValidation="False"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="sistema" HeaderText="Sistema" />
                        <asp:TemplateField HeaderText="Unidade Administrativa">
                            <ItemTemplate>
                                <ul>
                                    <asp:Repeater ID="_rptEntidadeUA" runat="server">
                                        <ItemTemplate>
                                            <asp:Label ID="Label9" runat="server" Text='<%# Bind("EntidadeOrUA") %>'></asp:Label></li>
                                            <br />
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bloqueado" HeaderStyle-CssClass="center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="_lbBloqueado" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Excluir" HeaderStyle-CssClass="center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="_btnExcluir" runat="server" CommandName="Delete" SkinID="btExcluir"
                                    CausesValidation="False" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="_btnSalvarGrupo" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </fieldset>
    <fieldset>
        <div class="right">
            <asp:Button ID="_btnSalvar" runat="server" Text="Salvar" Width="100px" OnClick="_btnSalvar_Click"
                ValidationGroup="Usuario" />
            <asp:Button ID="_btnCancelar" runat="server" Text="Cancelar" CausesValidation="False"
                OnClick="_btnCancelar_Click" />
        </div>
    </fieldset>
    <div id="divAddGrupos" title="Cadastro de grupos" class="hide">
        <asp:UpdatePanel ID="updGrupos" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <fieldset>
                    <asp:Label ID="_lblMessageInsert" runat="server" EnableViewState="False"></asp:Label>
                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Grupo" />
                    <asp:Label ID="Label8" runat="server" Text="Sistema - grupo *" AssociatedControlID="_ddlGrupos"></asp:Label>
                    <asp:DropDownList ID="_ddlGrupos" runat="server" AutoPostBack="True" SkinID="text60C"
                        OnSelectedIndexChanged="_ddlGrupos_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:CompareValidator ID="_cpvGrupos" runat="server" ErrorMessage="Sistema - grupo é obrigatório."
                        ControlToValidate="_ddlGrupos" ValidationGroup="Grupo" Operator="NotEqual" ValueToCompare="00000000-0000-0000-0000-000000000000"
                        Display="Dynamic">*</asp:CompareValidator>
                    <div id="divUA" runat="server" visible="false" style="overflow: auto">
                        <asp:Label ID="_lblUA" runat="server" Text="Unidade administrativa *" AssociatedControlID="_txtUA"></asp:Label>
                        <input id="_txtEnt_id" runat="server" type="hidden" />
                        <input id="_txtUad_id" runat="server" type="hidden" />
                        <asp:TextBox ID="_txtUA" runat="server" MaxLength="200" Enabled="False" SkinID="text60C"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="_rfvUA" runat="server" ValidationGroup="Grupo" ControlToValidate="_txtUA"
                            ErrorMessage="Unidade administrativa é obrigatório." Display="Dynamic">*</asp:RequiredFieldValidator>
                        <asp:ImageButton ID="_btnUA" runat="server" SkinID="btPesquisar" OnClick="_btnUA_Click" />
                        <div style="float: left; margin-right: 15px;">
                            <asp:Button ID="_btnAddUA" runat="server" Text="Adicionar" OnClick="_btnAddUA_Click"
                                ValidationGroup="Grupo" />
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
                            CausesValidation="False" />
                        <asp:Button ID="_btnCancelarGrupo" runat="server" Text="Cancelar" Width="100px" CausesValidation="False"
                            OnClientClick="$('#divAddGrupos').dialog('close');" />
                    </div>
                </fieldset>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnNovoGrupo" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="_dgvGrupo" EventName="RowEditing" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
