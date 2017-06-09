<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="AreaAdm_Configuracao_Relatorio_Cadastro" CodeBehind="Cadastro.aspx.cs" %>

<%@ PreviousPageType VirtualPath="~/AreaAdm/Configuracao/Relatorio/Busca.aspx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMasterLevel0" runat="Server">
    <asp:Label ID="lblObsoletoMsg" runat="server"></asp:Label>
    <asp:UpdatePanel ID="updMessage" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="divSalvarDados" class="summary" runat="server">
                <asp:Label ID="_lblMessageSalvar" runat="server" EnableViewState="False"></asp:Label>
                <br />
                <asp:Button ID="_btnSim" runat="server" Text="Sim" OnClick="_btnSim_Click" />
                <asp:Button ID="_btnNão" runat="server" Text="Não" OnClick="_btnNão_Click" CausesValidation="False" />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="_btnSalvar" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:Label ID="_lblMessage" runat="server" EnableViewState="False"></asp:Label>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    <fieldset>
        <legend>Cadastro de servidor de relatórios</legend>
        <asp:Label ID="Label2" runat="server" Text="Sistema *" EnableViewState="False" AssociatedControlID="_ddlSistema"></asp:Label>
        <asp:DropDownList ID="_ddlSistema" runat="server" DataSourceID="odsSistema" DataTextField="sis_nome"
            DataValueField="sis_id" OnDataBound="_ddlSistema_DataBound">
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="_rfvSistema" runat="server" ErrorMessage="Sistema é obrigatório."
            ControlToValidate="_ddlSistema" InitialValue="-1">*</asp:RequiredFieldValidator>
        <asp:ObjectDataSource ID="odsSistema" runat="server" DataObjectTypeName="Autenticador.Entities.SYS_Sistema"
            SelectMethod="GetSelectBy_usu_id" TypeName="Autenticador.BLL.SYS_SistemaBO"
            OldValuesParameterFormatString="original_{0}">
            <SelectParameters>
                <asp:ControlParameter ControlID="__Page" DbType="Guid" Name="usu_id" PropertyName="__SessionWEB.__UsuarioWEB.Usuario.usu_id" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:Label ID="Label1" runat="server" Text="Nome do servidor *" EnableViewState="False"
            AssociatedControlID="_txtNome"></asp:Label>
        <asp:TextBox ID="_txtNome" runat="server" SkinID="text60C" MaxLength="100"></asp:TextBox>
        <asp:RequiredFieldValidator ID="_rfvNome" runat="server" ErrorMessage="Nome do servidor é obrigatório."
            ControlToValidate="_txtNome">*</asp:RequiredFieldValidator>
        <asp:Label ID="Label3" runat="server" Text="Descrição" EnableViewState="False" AssociatedControlID="_txtDescricao"></asp:Label>
        <asp:TextBox ID="_txtDescricao" runat="server" MaxLength="1000" Rows="5" SkinID="text60C"
            TextMode="MultiLine"></asp:TextBox>
        <asp:Label ID="Label11" runat="server" Text="Local do processamento" EnableViewState="False"
            AssociatedControlID="_ddlLocalProcessamento"></asp:Label>
        <asp:DropDownList ID="_ddlLocalProcessamento" runat="server" SkinID="text20C" AutoPostBack="True"
            OnSelectedIndexChanged="_ddlLocalProcessamento_SelectedIndexChanged">
            <asp:ListItem Selected="True" Value="true">Remoto</asp:ListItem>
            <asp:ListItem Value="false">Local</asp:ListItem>
        </asp:DropDownList>
        <asp:UpdatePanel ID="updReportMode" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="divRemoteReport" runat="server">
                    <asp:Label ID="Label4" runat="server" Text="Usuário *" EnableViewState="False" AssociatedControlID="_txtUsuario"></asp:Label>
                    <asp:TextBox ID="_txtUsuario" runat="server" MaxLength="512" SkinID="text20C"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="_rfvUsuario" runat="server" ErrorMessage="Usuário é obrigatório."
                        ControlToValidate="_txtUsuario">*</asp:RequiredFieldValidator>
                    <asp:CheckBox ID="_chkAlterarSenha" runat="server" Text="Alterar senha" AutoPostBack="True"
                        OnCheckedChanged="_chkAlterarSenha_CheckedChanged" />
                    <div id="divSenha" runat="server">
                        <asp:Label ID="Label5" runat="server" Text="Senha" EnableViewState="False" AssociatedControlID="_txtSenha"></asp:Label>
                        <asp:TextBox ID="_txtSenha" runat="server" MaxLength="512" TextMode="Password" SkinID="text20C"></asp:TextBox>
                        <asp:Label ID="Label6" runat="server" Text="Confirmar senha" EnableViewState="False"
                            AssociatedControlID="_txtConfirmaSenha"></asp:Label>
                        <asp:TextBox ID="_txtConfirmaSenha" runat="server" MaxLength="512" SkinID="text20C"
                            TextMode="Password"></asp:TextBox>
                        <asp:CompareValidator ID="_cpvSenha" runat="server" ErrorMessage="Senha não confere."
                            ControlToCompare="_txtConfirmaSenha" ControlToValidate="_txtSenha">*</asp:CompareValidator>
                    </div>
                    <asp:Label ID="Label7" runat="server" Text="Domínio" EnableViewState="False" AssociatedControlID="_txtDominio"></asp:Label>
                    <asp:TextBox ID="_txtDominio" runat="server" SkinID="text20C" MaxLength="512"></asp:TextBox>
                    <asp:Label ID="Label8" runat="server" Text="Url Relatórios *" EnableViewState="False"
                        AssociatedControlID="_txtUrlRelatorios"></asp:Label>
                    <asp:TextBox ID="_txtUrlRelatorios" runat="server" MaxLength="100" SkinID="text60C"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="_rfvUrlRelatorios" runat="server" ErrorMessage="Url relatórios é obrigatório."
                        ControlToValidate="_txtUrlRelatorios">*</asp:RequiredFieldValidator>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="_ddlLocalProcessamento" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:Label ID="Label9" runat="server" Text="Pasta dos relatórios *" EnableViewState="False"
            AssociatedControlID="_txtPastaRelatorios"></asp:Label>
        <asp:TextBox ID="_txtPastaRelatorios" runat="server" MaxLength="1000" SkinID="text60C"></asp:TextBox>
        <asp:RequiredFieldValidator ID="_rfvPastaRelatorios" runat="server" ErrorMessage="Pasta dos relatórios é obrigatório."
            ControlToValidate="_txtPastaRelatorios">*</asp:RequiredFieldValidator>
        <asp:Label ID="Label10" runat="server" Text="Situação" EnableViewState="False" AssociatedControlID="_ddlSituacao"></asp:Label>
        <asp:DropDownList ID="_ddlSituacao" runat="server">
            <asp:ListItem Selected="True" Value="1">Ativo</asp:ListItem>
            <asp:ListItem Value="2">Bloqueado</asp:ListItem>
        </asp:DropDownList>
        <div class="clear"></div>
        <br />
        <fieldset>
            <legend>Selecionar relatórios do servidor</legend>
            <asp:CheckBoxList ID="_chkRelatorios" runat="server" DataSourceID="odsRelatorios"
                DataTextField="rlt_nome" DataValueField="rlt_id" RepeatColumns="3" 
                CellSpacing="5">
            </asp:CheckBoxList>
            <asp:ObjectDataSource ID="odsRelatorios" runat="server" DataObjectTypeName="Autenticador.Entities.CFG_Relatorio"
                OldValuesParameterFormatString="original_{0}" SelectMethod="ListarRelatoriosAtivos"
                TypeName="Autenticador.BLL.CFG_ServidorRelatorioBO" OnSelected="odsRelatorios_Selected">
            </asp:ObjectDataSource>
        </fieldset>
        <asp:UpdatePanel ID="updSalvar" runat="server">
            <ContentTemplate>
                <div class="right">
                    <asp:Button ID="_btnSalvar" runat="server" Text="Salvar" Width="100px" OnClick="_btnSalvar_Click" />
                    <asp:Button ID="_btnCancelar" runat="server" Text="Cancelar" Width="100px" CausesValidation="False"
                        OnClick="_btnCancelar_Click" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </fieldset>
</asp:Content>
