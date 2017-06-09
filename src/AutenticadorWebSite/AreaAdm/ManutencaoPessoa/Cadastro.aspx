<%@ Page Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true" Inherits="AreaAdm_ManutencaoPessoa_Cadastro" CodeBehind="Cadastro.aspx.cs" %>

<%@ PreviousPageType VirtualPath="~/AreaAdm/ManutencaoPessoa/Busca.aspx" %>
<%@ Register Src="../../WebControls/Pessoa/UCCadastroPessoa.ascx" TagName="UCCadastroPessoa"
    TagPrefix="uc3" %>
<%@ Register Src="../../WebControls/Contato/UCGridContato.ascx" TagName="UCGridContato"
    TagPrefix="uc5" %>
<%@ Register Src="../../WebControls/Documento/UCGridDocumento.ascx" TagName="UCGridDocumento"
    TagPrefix="uc6" %>
<%@ Register Src="../../WebControls/CertidaoCivil/UCGridCertidaoCivil.ascx" TagName="UCGridCertidaoCivil"
    TagPrefix="uc7" %>
<%@ Register Src="../../WebControls/Busca/UCPessoas.ascx" TagName="UCPessoas" TagPrefix="uc16" %>

<%@ Register Src="~/WebControls/Endereco/UCEnderecos.ascx" TagName="UCEnderecos" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="divBuscaPessoa" title="Busca de pessoas" class="hide">
        <asp:UpdatePanel ID="_updBuscaPessoa" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <uc16:UCPessoas ID="UCPessoas1" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:Label ID="_lblMessage" runat="server" EnableViewState="False"></asp:Label>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Pessoa" />
    <div id="divPessoa" runat="server">
        <div id="divTabs">
            <ul class="hide">
                <li><a href="#divTabs-1">Dados pessoais</a></li>
                <li><a href="#divTabs-2">Endereço / contato</a></li>
                <li><a href="#divTabs-3">Documentação</a> </li>
            </ul>
            <div id="divTabs-1">
                <fieldset>
                    <uc3:UCCadastroPessoa ID="UCCadastroPessoa1" runat="server" />
                </fieldset>
            </div>
            <div id="divTabs-2">
                <fieldset>
                    <fieldset>
                        <legend>Cadastro de endereços</legend>
                      
                        <uc2:UCEnderecos ID="UCEnderecos1" runat="server" />
                    </fieldset>
                </fieldset>
                <fieldset>
                    <legend>Cadastro de contatos</legend>
                    <uc5:UCGridContato ID="UCGridContato1" runat="server" />
                </fieldset>
            </div>
            <div id="divTabs-3">
                <fieldset>
                    <uc6:UCGridDocumento ID="UCGridDocumento1" runat="server" />
                </fieldset>
                <fieldset>
                    <legend>Cadastro de certidões civis</legend>
                    <uc7:UCGridCertidaoCivil ID="UCGridCertidaoCivil1" runat="server" />
                </fieldset>
            </div>
        </div>
        <fieldset id="fdsBotoes">
            <div class="right">
                <asp:Button ID="_btnSalvar" runat="server" Text="Salvar" OnClick="_btnSalvar_Click"
                    ValidationGroup="Pessoa" />
                <asp:Button ID="_btnCancelar" runat="server" Text="Cancelar" CausesValidation="False"
                    OnClick="_btnCancelar_Click" />
                <input id="txtSelectedTab" type="hidden" class="" runat="server" />
            </div>
        </fieldset>
    </div>
</asp:Content>
