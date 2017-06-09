<%@ Page Title="" Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master"
    AutoEventWireup="true" Inherits="AreaAdm_IntegracaoExterna_Cadastro" CodeBehind="Cadastro.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .Block {
            line-height: 1.8em;
        }

            .Block span {
                display: inline-block;
                width: 230px;
                text-align: left;
                padding-right: 1em;
                font-weight: bold;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="IntegracaoExterna" />
    <asp:FormView ID="frvCadastro" runat="server" DataKeyNames="ine_id,IsNew" DataSourceID="odsIntegracaoExterna"
        OnDataBound="frvCadastro_DataBound" Width="100%">
        <EditItemTemplate>
            <fieldset>
                <legend>Integração externa</legend>
                <!-- Descrição -->
                <asp:Label ID="lblDescricao" runat="server" Text="Descrição" AssociatedControlID="txtDescricao"></asp:Label>
                <asp:TextBox ID="txtDescricao" runat="server" Text='<%# Bind("ine_descricao") %>' SkinID="text60C"></asp:TextBox>
                <!-- Domínio -->
                <asp:Label ID="lblDominio" runat="server" Text="Domínio *" AssociatedControlID="txtDominio"></asp:Label>
                <asp:TextBox ID="txtDominio" runat="server" Text='<%# Bind("ine_dominio") %>' SkinID="text60C"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvValidaDominio" runat="server" ErrorMessage="Domínio é obrigatório"
                    Text="*" ControlToValidate="txtDominio" ValidationGroup="IntegracaoExterna"></asp:RequiredFieldValidator>
                <!-- Url interna -->
                <asp:Label ID="lblUrlInterna" runat="server" Text="Url interna *" AssociatedControlID="txtUrlInterna"></asp:Label>
                <asp:TextBox ID="txtUrlInterna" runat="server" Text='<%# Bind("ine_urlInterna") %>' SkinID="text60C"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvValidaUrl" runat="server" ErrorMessage="Url interna é obrigatório"
                    Text="*" ControlToValidate="txtUrlInterna" ValidationGroup="IntegracaoExterna"></asp:RequiredFieldValidator>
                <!-- Url Externa -->
                <asp:Label ID="lblUrlExterna" runat="server" Text="Url externa *" AssociatedControlID="txtUrlExterna"></asp:Label>
                <asp:TextBox ID="txtUrlExterna" runat="server" Text='<%# Bind("ine_urlExterna") %>' SkinID="text60C"> </asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvValidaUrlExterna" runat="server" ErrorMessage="Url externa é obrigatório"
                    Text="*" ControlToValidate="txtUrlExterna" ValidationGroup="IntegracaoExterna"></asp:RequiredFieldValidator>
                <!-- Token Interno -->
                <asp:Label ID="lblTokenInterno" runat="server" Text="Token interno de acesso" AssociatedControlID="txtTokenInterno"></asp:Label>
                <asp:TextBox ID="txtTokenInterno" runat="server" Text='<%# Bind("ine_tokenInterno") %>' SkinID="text30C"></asp:TextBox>
                <!-- Token Externo -->
                <asp:Label ID="lblTokenExterno" runat="server" Text="Token externo de acesso" AssociatedControlID="txtTokenExterno"></asp:Label>
                <asp:TextBox ID="txtTokenExterno" runat="server" Text='<%# Bind("ine_tokenExterno") %>' SkinID="text30C"></asp:TextBox>
                <!-- Chave -->
                <asp:Label ID="lblChave" if="lblChave" runat="server" Text="Chave de acesso" AssociatedControlID="txtChave"></asp:Label>
                <asp:TextBox ID="txtChave" runat="server" Text='<%# Bind("ine_chave") %>'></asp:TextBox>
                <!-- Tipo -->
                <asp:Label ID="lblTipo" runat="server" Text="Tipo" AssociatedControlID="ddlTipo"></asp:Label>
                <asp:DropDownList ID="ddlTipo" runat="server">
                    <asp:ListItem Text="-- Selecione uma opção --" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Live " Value="1"></asp:ListItem>
                </asp:DropDownList>
                <br />

                <!-- Tipo Integracao Externa -->
                <asp:Label ID="lblIntegracaoExterna" runat="server" Text="Tipo Integração Externa" AssociatedControlID="ddlIntegracaoExterna"></asp:Label>
                <asp:DropDownList ID="ddlIntegracaoExterna" runat="server">
                    <asp:ListItem Text="-- Selecione uma opção --" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="Integração Externa" Value="1"></asp:ListItem>
                </asp:DropDownList>

                <fieldset>
                    <legend>Proxy</legend>
                    <!-- Usa proxy -->
                    <asp:Label ID="lblInformaUsarProxy" runat="server"></asp:Label>
                    <asp:CheckBox ID="ckbProxy" runat="server" Text="Usar proxy?" Checked='<%# Bind("ine_proxy") %>'
                        ValidationGroup="IntegracaoExterna" />
                    <!-- IP proxy -->
                    <asp:Label ID="lblProxyIp" runat="server" Text="Ip do proxy" AssociatedControlID="txtProxyIp"></asp:Label>
                    <asp:TextBox ID="txtProxyIp" runat="server" Text='<%# Bind("ine_proxyIP") %>'></asp:TextBox>
                    <!-- Porta proxy-->
                    <asp:Label ID="lblProxyPorta" runat="server" Text="Porta do proxy" AssociatedControlID="txtProxyPorta"></asp:Label>
                    <asp:TextBox ID="txtProxyPorta" runat="server" Text='<%# Bind("ine_proxyPorta") %>'></asp:TextBox>
                    <!-- Autenticação proxy-->
                    <asp:Label ID="lblInformaProxyAutenticacao" runat="server"></asp:Label>
                    <asp:CheckBox ID="ckbProxyAutenticacao" runat="server" Text="Usar autenticação proxy?" Checked='<%# Bind("ine_proxyAutenticacao") %>'
                        ValidationGroup="IntegracaoExterna" />
                    <!-- Usuário autenticação proxy -->
                    <asp:Label ID="lblProxyAutenticacaoUsuario" runat="server" Text="Usuário de autenticação do proxy" AssociatedControlID="txtProxyAutenticacaoUsuario"></asp:Label>
                    <asp:TextBox ID="txtProxyAutenticacaoUsuario" runat="server" Text='<%# Bind("ine_proxyAutenticacaoUsuario") %>'
                        SkinID="text20C"></asp:TextBox>
                    <!-- Senha autenticação proxy -->
                    <asp:Label ID="lblProxyAutenticacaoSenha" runat="server" Text="Senha de autenticação do proxy" AssociatedControlID="txtProxyAutenticacaoSenha"></asp:Label>
                    <asp:TextBox ID="txtProxyAutenticacaoSenha" runat="server" Text='<%# Bind("ine_proxyAutenticacaoSenha") %>'
                        SkinID="text20C" TextMode="Password"></asp:TextBox>
                </fieldset>
                <asp:CheckBox ID="ckbInativo" runat="server" Text="Inativo" />

                <%-- Informações necessários para Update --%>
                <input type="hidden" id="ine_tipo" runat="server" value='<%# Bind("ine_tipo") %>' />
                <input type="hidden" id="iet_id" runat="server" value='<%# Bind("iet_id") %>' />
                <input type="hidden" id="ine_situacao" runat="server" value='<%# Bind("ine_situacao") %>' />
                <div class="right">
                    <asp:Button ID="btnSave" runat="server" Text="Salvar" CommandName="Update" OnClick="btnSave_Click"
                        CausesValidation="true" ValidationGroup="IntegracaoExterna" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancelar" CommandName="Cancel" />
                </div>
            </fieldset>
        </EditItemTemplate>
        <ItemTemplate>
            <fieldset class="Block">
                <legend>Integração externa</legend>
                <asp:Label ID="lblDescricao" runat="server" Text="Descrição:"> </asp:Label>
                <asp:Literal ID="ltlDescricao" runat="server" Text='<%# Bind("ine_descricao") %>'></asp:Literal>
                <br />
                <asp:Label ID="lblDominio" runat="server" Text="Domínio:"></asp:Label>
                <asp:Literal ID="ltlDominio" runat="server" Text='<%# Bind("ine_dominio") %>'></asp:Literal>
                <br />
                <asp:Label ID="lblUrlInterna" runat="server" Text="Url interna:"> </asp:Label>
                <asp:Literal ID="ltlUrlInterna" runat="server" Text='<%# Bind("ine_urlInterna") %>'></asp:Literal>
                <br />
                <asp:Label ID="lblUrlExterna" runat="server" Text="Url externa:"></asp:Label>
                <asp:Literal ID="ltlUrlExterna" runat="server" Text='<%# Bind("ine_urlExterna") %>'> </asp:Literal>
                <br />
                <asp:Label ID="lblTokenInterno" runat="server" Text="Token interno de acesso:"> </asp:Label>
                <asp:Literal ID="ltlTokenInterno" runat="server" Text='<%# Bind("ine_tokenInterno") %>'></asp:Literal>
                <br />
                <asp:Label ID="lblTokenExterno" runat="server" Text="Token externo de acesso:"> </asp:Label>
                <asp:Literal ID="ltlTokenExterno" runat="server" Text='<%# Bind("ine_tokenExterno") %>'></asp:Literal>
                <br />
                <asp:Label ID="lblChave" runat="server" Text="Chave de acesso:"></asp:Label>
                <asp:Literal ID="ltlChave" runat="server" Text='<%# Bind("ine_chave") %>'></asp:Literal>
                <br />
                <asp:Label ID="lblProxy" runat="server" Text="Usar proxy:"> </asp:Label>
                <asp:Literal ID="ltlProxy" runat="server"></asp:Literal>
                <br />
                <asp:Label ID="lblProxyIp" runat="server" Text="Ip do proxy:"> </asp:Label>
                <asp:Literal ID="ltlProxyIp" runat="server" Text='<%# Bind("ine_proxyIP") %>'></asp:Literal>
                <br />
                <asp:Label ID="lblProxyPorta" runat="server" Text="Porta do proxy:"> </asp:Label>
                <asp:Literal ID="ltlProxyPorta" runat="server" Text='<%# Bind("ine_proxyPorta") %>'></asp:Literal>
                <br />
                <asp:Label ID="lblProxyAutenticacao" runat="server" Text="Usar autenticação proxy:"></asp:Label>
                <asp:Literal ID="ltlProxyAutenticacao" runat="server"></asp:Literal>
                <br />
                <asp:Label ID="lblProxyAutenticacaoUsuario" runat="server" Text="Usuário de autenticação do proxy:"> </asp:Label>
                <asp:Literal ID="ltlProxyAutenticacaoUsuario" runat="server" Text='<%# Bind("ine_proxyAutenticacaoUsuario") %>'></asp:Literal>
                <br />
                <asp:Label ID="lblProxyAutenticacaoSenha" runat="server" Text="Senha de autenticação do proxy:"> </asp:Label>
                <asp:Literal ID="ltlProxyAutenticacaoSenha" runat="server"></asp:Literal>
                <br />
                <asp:Label ID="lblTipoIntegracao" runat="server" Text="Tipo:"></asp:Label>
                <asp:Literal ID="ltlTipoIntegracao" runat="server" Text='<%# Bind("ine_tipo") %>'></asp:Literal>
                <br />
                <asp:Label ID="lblIntegracaoExterna" runat="server" Text="Tipo de Integração Externa:"></asp:Label>
                <asp:Literal ID="ddlIntegracaoExternaTipo" runat="server" Text='<%# Bind("iet_id") %>'></asp:Literal>
                <br />
                <asp:Label ID="lblSituacao" runat="server" Text="Situacão:"> </asp:Label>
                <asp:Literal ID="ltlSituacao" runat="server"></asp:Literal>
                <div class="right">
                    <asp:Button ID="btnEditar" runat="server" Text="Editar" CommandName="Edit" />
                </div>
            </fieldset>
        </ItemTemplate>
    </asp:FormView>
    <asp:ObjectDataSource ID="odsIntegracaoExterna" runat="server" DataObjectTypeName="Autenticador.Entities.SYS_IntegracaoExterna"
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetSelect" TypeName="Autenticador.BLL.SYS_IntegracaoExternaBO"
        UpdateMethod="Save"></asp:ObjectDataSource>
</asp:Content>
