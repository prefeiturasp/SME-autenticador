<%@ Page Language="C#" MasterPageFile="~/MasterPageLogin.master" AutoEventWireup="true"
    Inherits="Login" Title="Untitled Page" CodeBehind="Login.aspx.cs" %>

<%@ Register Src="WebControls/Combos/UCComboEntidade.ascx" TagName="UCComboEntidade"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset id="fdsMensagem" runat="server" class="fdsMensagem" visible="false" enableviewstate="false">
        <div class="nossaDiv">
            <br />
            <span id="spnMensagemUsuario" runat="server" enableviewstate="false"></span>
            <br />
            <br />
        </div>
        <div class="right">
            <asp:Button ID="btnFechar" runat="server" Text="Continuar login" EnableViewState="false" />
        </div>
    </fieldset>
    <div id="divAlterarSenha" title="Senha expirada - alterar senha" class="hide">
        <asp:UpdatePanel ID="_updAlterarSenha" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:UpdateProgress ID="UpdateProgress3" runat="server" DisplayAfter="10" AssociatedUpdatePanelID="_updAlterarSenha">
                    <ProgressTemplate>
                        <div class="loader">
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <asp:Label ID="_lblMessageAlterarSenha" runat="server" EnableViewState="False"></asp:Label>
                <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="AlterarSenha" />
                <fieldset>
                    <asp:Label ID="_lblSenhaAtual" runat="server" Text="Senha atual" AssociatedControlID="_txtSenhaAtual"></asp:Label>
                    <asp:TextBox ID="_txtSenhaAtual" runat="server" TextMode="Password" SkinID="text20C"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="_rfvSenhaAtual" ControlToValidate="_txtSenhaAtual"
                        ValidationGroup="AlterarSenha" runat="server" ErrorMessage="Senha atual é obrigatório.">*</asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvSenhaAtual" ControlToValidate="_txtSenhaAtual" Display="Dynamic"
                        runat="server" ErrorMessage="Senha atual inválida." ValidationGroup="AlterarSenha" ClientValidationFunction="cvSenhaAtual_ClientValidate">*</asp:CustomValidator>
                    <asp:Label ID="_lblNovaSenha" runat="server" Text="Nova senha" AssociatedControlID="_txtNovaSenha"></asp:Label>
                    <asp:TextBox ID="_txtNovaSenha" runat="server" TextMode="Password" SkinID="text20C"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="_rfvNovaSenha" runat="server" ControlToValidate="_txtNovaSenha"
                        ValidationGroup="AlterarSenha" ErrorMessage="Nova senha é obrigatório." Display="Dynamic">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revNovaSenhaFormato" runat="server" ControlToValidate="_txtNovaSenha"
                        ValidationGroup="AlterarSenha" Display="Dynamic" ErrorMessage="A senha deve conter pelo menos uma combinação de letras e números ou letras
                        maiúsculas e minúsculas ou algum caracter especial (!, @, #, $, %, &amp;) somados a letras e/ou números.">*</asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator ID="revNovaSenhaTamanho" runat="server" ControlToValidate="_txtNovaSenha"
                        ValidationGroup="AlterarSenha" Display="Dynamic" ErrorMessage="A senha deve conter {0}.">*</asp:RegularExpressionValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Senha atual e nova senha devem ser diferentes"
                        ValidationGroup="AlterarSenha" Operator="NotEqual" ControlToCompare="_txtNovaSenha"
                        ControlToValidate="_txtSenhaAtual">*</asp:CompareValidator>
                    <br />
                    <asp:Label ID="lblMsnNovaSenha" runat="server" Text="({0}, utilizando letras e números)."></asp:Label>
                    <br />
                    <asp:Label ID="_lblConfNovaSenha" runat="server" Text="Confirmar nova senha" AssociatedControlID="_txtConfNovaSenha"></asp:Label>
                    <asp:TextBox ID="_txtConfNovaSenha" runat="server" TextMode="Password" SkinID="text20C"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="_rfvConfirmarSenha" runat="server" ControlToValidate="_txtConfNovaSenha"
                        ValidationGroup="AlterarSenha" ErrorMessage="Confirmar nova senha é obrigatório."
                        Display="Dynamic">*</asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="_cpvConfirmarSenha" runat="server" ControlToCompare="_txtNovaSenha"
                        ValidationGroup="AlterarSenha" ControlToValidate="_txtConfNovaSenha" ErrorMessage="Senha não confere."
                        Display="Dynamic">*</asp:CompareValidator>
                    <div class="right">
                        <asp:Button ID="_btnSalvar" runat="server" Text="Alterar senha" OnClick="_btnSalvar_Click"
                            ValidationGroup="AlterarSenha" />
                        <asp:Button ID="_btnCancelarAlterarSenha" runat="server" Text="Cancelar" CausesValidation='false'
                            OnClientClick="$('#divAlterarSenha').dialog('close');" />
                    </div>
                </fieldset>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="divEsqueciSenha" title="Esqueceu sua senha?" class="hide">
        <asp:UpdatePanel ID="_updEsqueciSenha" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="10" AssociatedUpdatePanelID="_updEsqueciSenha">
                    <ProgressTemplate>
                        <div class="loader">
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <asp:Label ID="_lblMessageEsqueciSenha" runat="server" EnableViewState="False"></asp:Label>
                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="EsqueciSenha" />
                <fieldset>
                    <asp:CheckBox ID="chkPossuiEmail" runat="server" Text="Possui e-mail cadastrado?" AutoPostBack="true" Checked="true"
                        OnCheckedChanged="chkPossuiEmail_CheckedChanged" />
                    <asp:Label ID="lblEmailInfo" runat="server"></asp:Label>
                    <uc1:UCComboEntidade ID="UCComboEntidade2" runat="server" />
                    <div id="divEmail" runat="server">
                        <asp:Label ID="Label3" runat="server" Text="E-mail *" AssociatedControlID="_txtEmail"></asp:Label>
                        <asp:TextBox ID="_txtEmail" runat="server" MaxLength="200" SkinID="text60C"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="_rfvEmail" runat="server" ControlToValidate="_txtEmail"
                            ValidationGroup="EsqueciSenha" ErrorMessage="E-mail é obrigatório." Display="Dynamic">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="_revEmail" runat="server" ControlToValidate="_txtEmail"
                            ValidationGroup="EsqueciSenha" ErrorMessage="E-mail está fora do padrão ( seuEmail@seuProvedor )."
                            Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                    </div>
                    <div id="divDtNascCpf" runat="server">
                        <asp:Label ID="lblDtNasc" runat="server" Text="Data de nascimento" AssociatedControlID="txtDtNasc"></asp:Label>
                        <asp:TextBox ID="txtDtNasc" runat="server" SkinID="Data"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvTdNasc" runat="server" ControlToValidate="txtDtNasc"
                            ValidationGroup="EsqueciSenha" ErrorMessage="Data de nascimento é obrigatório." Display="Dynamic">*</asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="cvDtNasc" runat="server" ControlToValidate="txtDtNasc" OnServerValidate="ValidarData_ServerValidate"
                            ValidationGroup="EsqueciSenha" ErrorMessage="Data inválida." Display="Dynamic">*</asp:CustomValidator>
                        <asp:Label ID="lblCPF" runat="server" Text="CPF" AssociatedControlID="txtCPF"></asp:Label>
                        <asp:TextBox ID="txtCPF" runat="server" MaxLength="50" SkinID="text15C"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvCPF" runat="server" ControlToValidate="txtCPF"
                            ValidationGroup="EsqueciSenha" ErrorMessage="CPF é obrigatório." Display="Dynamic">*</asp:RequiredFieldValidator>
                    </div>
                    <div id="divNovaSenhaEsqueci" runat="server">
                        <br />
                        <asp:Label ID="lblNovaSenhaEsqueci" runat="server" Text="Nova senha" AssociatedControlID="txtNovaSenhaEsqueci"></asp:Label>
                        <asp:TextBox ID="txtNovaSenhaEsqueci" runat="server" TextMode="Password" SkinID="text20C"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvNovaSenhaEsqueci" runat="server" ControlToValidate="txtNovaSenhaEsqueci"
                            ValidationGroup="EsqueciSenha" ErrorMessage="Nova senha é obrigatório." Display="Dynamic">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revNovaSenhaFormatoEsqueci" runat="server" ControlToValidate="txtNovaSenhaEsqueci"
                            ValidationGroup="EsqueciSenha" Display="Dynamic" ErrorMessage="A senha deve conter pelo menos uma combinação de letras e números ou letras
                        maiúsculas e minúsculas ou algum caracter especial (!, @, #, $, %, &amp;) somados a letras e/ou números.">*</asp:RegularExpressionValidator>
                        <asp:RegularExpressionValidator ID="revNovaSenhaTamanhoEsqueci" runat="server" ControlToValidate="txtNovaSenhaEsqueci"
                            ValidationGroup="EsqueciSenha" Display="Dynamic" ErrorMessage="A senha deve conter {0}.">*</asp:RegularExpressionValidator>
                        <br />
                        <asp:Label ID="lblNovaSenhaEsqueciMsg" runat="server" Text="({0}, utilizando letras e números)."></asp:Label>
                        <br />
                        <br />
                        <asp:Label ID="lblConfirmarSenhaEsqueci" runat="server" Text="Confirmar nova senha" AssociatedControlID="txtConfirmarSenhaEsqueci"></asp:Label>
                        <asp:TextBox ID="txtConfirmarSenhaEsqueci" runat="server" TextMode="Password" SkinID="text20C"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvConfirmarSenhaEsqueci" runat="server" ControlToValidate="txtConfirmarSenhaEsqueci"
                            ValidationGroup="EsqueciSenha" ErrorMessage="Confirmar nova senha é obrigatório."
                            Display="Dynamic">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="cvConfirmarSenhaEsqueci" runat="server" ControlToCompare="txtNovaSenhaEsqueci"
                            ValidationGroup="EsqueciSenha" ControlToValidate="txtConfirmarSenhaEsqueci" ErrorMessage="Senha não confere."
                            Display="Dynamic">*</asp:CompareValidator>
                    </div>
                    <div class="right">
                        <asp:Button ID="_btnEnviar" runat="server" Text="Enviar" OnClick="_btnEnviar_Click"
                            ValidationGroup="EsqueciSenha" />
                        <asp:Button ID="_btnCancelar" runat="server" CausesValidation="False" Text="Cancelar"
                            OnClientClick="$('#divEsqueciSenha').dialog('close');" />
                    </div>
                </fieldset>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="login">
        <fieldset id="fdsLogin" runat="server">
            <legend>Login</legend>
            <div id="msgLogin">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <asp:Label ID="_lblMessage" runat="server" EnableViewState="False"></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Login" />
            </div>
            <div class="container entidade">
                <uc1:UCComboEntidade ID="UCComboEntidade1" runat="server" />
            </div>

            <div class="container usuario">
                <asp:Label ID="Label1" runat="server" Text="Usuário *" AssociatedControlID="_txtLogin"
                    EnableViewState="False"></asp:Label>
                <asp:TextBox ID="_txtLogin" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="_rfvLogin" runat="server" ErrorMessage="Login é obrigatório."
                    ControlToValidate="_txtLogin" Display="Dynamic" ValidationGroup="Login">*</asp:RequiredFieldValidator>
            </div>
            <div class="container senha">
                <asp:Label ID="Label2" runat="server" Text="Senha *" AssociatedControlID="_txtSenha"
                    EnableViewState="False"></asp:Label>
                <asp:TextBox ID="_txtSenha" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="_rfvSenha" runat="server" ErrorMessage="Senha é obrigatório."
                    ControlToValidate="_txtSenha" Display="Dynamic" ValidationGroup="Login">*</asp:RequiredFieldValidator>
            </div>
            <div id="divCaptcha" runat="server" class="container divCaptchaLogin" visible="false">
                <asp:Label ID="Label4" runat="server" Text="Código *" AssociatedControlID="txtCodigoConfirmacao"
                    EnableViewState="False"></asp:Label>
                <asp:TextBox ID="txtCodigoConfirmacao" runat="server" MaxLength="4" Text=""></asp:TextBox>
                <img class="imgCaptcha" height="25px" alt="Código de confirmação" src="Captcha.ashx" width="80px" />
                <asp:RequiredFieldValidator ID="rfvCodigoConfirmacao" runat="server" ErrorMessage="Código de confirmação é obrigatório."
                    ControlToValidate="txtCodigoConfirmacao" Display="Dynamic" ValidationGroup="Login">*</asp:RequiredFieldValidator>
                <br />
                <asp:LinkButton ID="lkbIlegivel" runat="server">Imagem ilegível? Clique aqui.</asp:LinkButton>
            </div>
            <asp:Button ID="_btnEntrar" runat="server" Text="Entrar" ValidationGroup="Login" CssClass="btn" EnableViewState="False" OnClick="_btnEntrar_Click" />
            <asp:UpdatePanel ID="_updLogin" runat="server">
                <ContentTemplate>
                    <asp:LinkButton ID="_btnEsqueceuSenha" runat="server" CssClass="link-esqueceu-senha" CausesValidation="False"
                        OnClick="_btnEsqueceuSenha_Click">Esqueceu sua senha?</asp:LinkButton>
                </ContentTemplate>
            </asp:UpdatePanel>
        </fieldset>
    </div>
</asp:Content>