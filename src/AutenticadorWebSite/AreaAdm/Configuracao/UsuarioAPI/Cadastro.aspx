<%@ Page Title="" Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="AutenticadorWebSite.AreaAdm.Configuracao.UsuarioAPI.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="updMessage" runat="server" UpdateMode="Conditional" EnableViewState="False">
        <ContentTemplate>
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
            <asp:ValidationSummary ID="ValidationSummary" runat="server" ValidationGroup="UsuarioAPI" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel ID="pnlUsuarioAPI" runat="server" GroupingText="Usuário API">
        <asp:Button ID="btnNovo" runat="server" Text="Novo usuário API" CausesValidation="false" OnClick="btnNovo_Click" />
        <asp:UpdatePanel ID="updGridUsuarioAPI" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="grvUsuarioAPI" runat="server" AutoGenerateColumns="false" DataKeyNames="uap_id,uap_situacao,IsNew"
                    EmptyDataText="Não existem usuários API cadastrados."
                    OnDataBinding="grvUsuarioAPI_DataBinding"
                    OnRowEditing="grvUsuarioAPI_RowEditing"
                    OnRowDeleting="grvUsuarioAPI_RowDeleting"
                    OnRowUpdating="grvUsuarioAPI_RowUpdating"
                    OnRowDataBound="grvUsuarioAPI_RowDataBound"
                    OnRowCancelingEdit="grvUsuarioAPI_RowCancelingEdit">
                    <Columns>
                        <asp:TemplateField HeaderText="Usuário">
                            <ItemTemplate>
                                <asp:Label ID="lblUsuario" runat="server" Text='<%# Bind("uap_username") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtUsuario" runat="server" Text='<%# Bind("uap_username") %>'
                                    SkinID="text30C"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvUsuario" runat="server" ControlToValidate="txtUsuario" ValidationGroup="UsuarioAPI"
                                    ErrorMessage="Nome do usuário API é obrigatório." Display="Dynamic">*</asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <HeaderStyle Width="320px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Senha">
                            <ItemTemplate>
                                <asp:Label ID="lblSenha" runat="server" Text="***"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtSenha" TextMode="Password" runat="server" SkinID="text20C" Text='<%# RetornaSenha(Eval("uap_password").ToString()) %>'></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revSenha" runat="server" ControlToValidate="txtSenha"
                                    ValidationGroup="UsuarioAPI" Display="Dynamic" ErrorMessage="A senha do usuário não pode conter espaços em branco."
                                    ValidationExpression="[^\s]+">*</asp:RegularExpressionValidator>
                                <asp:RegularExpressionValidator ID="revSenhaFormato" runat="server" ControlToValidate="txtSenha" ValidationExpression='<%# parametroFormatoSenhaUsuario %>'
                                    ValidationGroup="UsuarioAPI" Display="Dynamic" ErrorMessage="A senha deve conter pelo menos uma combinação de letras e números ou letras
                                    maiúsculas e minúsculas ou algum caracter especial (!, @, #, $, %, &amp;) somados a letras e/ou números.">*</asp:RegularExpressionValidator>
                                <asp:RegularExpressionValidator ID="revSenhaTamanho" runat="server" ControlToValidate="txtSenha" ValidationExpression='<%# parametroTamanhoSenhaUsuario %>'
                                    ValidationGroup="UsuarioAPI" Display="Dynamic" ErrorMessage='<%# RetornaErrorMessageTamanho().ToString() %>'>*</asp:RegularExpressionValidator>
                            </EditItemTemplate>
                            <HeaderStyle Width="320px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Situação">
                            <ItemTemplate>
                                <asp:Label ID="lblSituacao" runat="server"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlSituacao" runat="server">
                                    <asp:ListItem Text="-- Selecione uma situação --" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Ativo" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Inativo" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:CompareValidator ID="cvSituacao" runat="server" ControlToValidate="ddlSituacao" ValueToCompare="0" Operator="GreaterThan"
                                    ErrorMessage="Situação é obrigatório." Display="Dynamic" ValidationGroup="UsuarioAPI">*</asp:CompareValidator>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Editar" HeaderStyle-CssClass="center">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEditar" runat="server" CommandName="Edit" SkinID="btEditar" ToolTip="Editar usuário API"
                                    CausesValidation="false" />
                                <asp:ImageButton ID="imgCancelar" runat="server" CommandName="Cancel" SkinID="btDesfazer" ToolTip="Cancelar edição"
                                    CausesValidation="false" Visible="false" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Salvar" HeaderStyle-CssClass="center">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgSalvar" runat="server" CommandName="Update" SkinID="btConfirmar" ToolTip="Salvar usuário API"
                                    ValidationGroup="UsuarioAPI" Visible="false" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Excluir" HeaderStyle-CssClass="center">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgExcluir" runat="server" CommandName="Delete" SkinID="btExcluir" ToolTip="Excluir usuário API"
                                    CausesValidation="false" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnNovo" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
