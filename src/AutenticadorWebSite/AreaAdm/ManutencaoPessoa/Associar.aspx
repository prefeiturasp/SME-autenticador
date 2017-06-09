<%@ Page Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true" Inherits="AreaAdm_ManutencaoPessoa_Associar" Codebehind="Associar.aspx.cs" %>

<%@ PreviousPageType VirtualPath="~/AreaAdm/ManutencaoPessoa/Busca.aspx" %>
<%@ Register Src="../../WebControls/Pessoa/UCCadastroPessoa.ascx" TagName="UCCadastroPessoa"
    TagPrefix="uc3" %>
<%@ Register Src="../../WebControls/Contato/UCGridContato.ascx" TagName="UCGridContato"
    TagPrefix="uc5" %>
<%@ Register Src="../../WebControls/Documento/UCGridDocumento.ascx" TagName="UCGridDocumento"
    TagPrefix="uc6" %>
<%@ Register Src="../../WebControls/CertidaoCivil/UCGridCertidaoCivil.ascx" TagName="UCGridCertidaoCivil"
    TagPrefix="uc7" %>
<%@ Register Src="../../WebControls/Endereco/UCEnderecos.ascx" TagName="UCEnderecos"
    TagPrefix="uc13" %>
<%@ Register Src="../../WebControls/Busca/UCPessoas.ascx" TagName="UCPessoas" TagPrefix="uc16" %>
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
    <div id="divEnderecos" title="Cadastro de endereços">
        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Endereco" />
    </div>
    <asp:UpdatePanel ID="_updGridPessoas" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Label ID="_lblMessage" runat="server" EnableViewState="False"></asp:Label>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Pessoa" />
            <fieldset>
                <legend>Associação de pessoas</legend>
                <asp:GridView ID="_grvAssociarPessoas" runat="server" AutoGenerateColumns="False"
                    DataKeyNames="pes_id" OnRowCommand="_grvAssociarPessoas_RowCommand" OnRowDataBound="_grvAssociarPessoas_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Nome">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("pes_nome") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="_btnSelecionar" runat="server" CausesValidation="False" CommandName="Selecionar"
                                    Text='<%# Bind("pes_nome") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="pes_dataNascimento" HeaderText="Data nasc.">
                            <HeaderStyle CssClass="center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tipo_documentacao_cpf" HeaderText="tipo_documentacao_cpf" />
                        <asp:BoundField DataField="tipo_documentacao_rg" HeaderText="tipo_documentacao_rg" />
                    </Columns>
                </asp:GridView>
            </fieldset>
            <div id="divColaborador" runat="server">
                <div id="divTabs">
                    <ul>
                        <li><a href="#divTabs-1">Dados pessoais</a></li>
                        <li><a href="#divTabs-2">Endereço / contato</a></li>
                        <li><a href="#divTabs-3">Documentação</a></li>
                    </ul>
                    <div id="divTabs-1">
                        <fieldset>
                            <uc3:UCCadastroPessoa ID="UCCadastroPessoa1" runat="server" />
                        </fieldset>
                    </div>
                    <div id="divTabs-2">
                        <br />
                        <fieldset>
                            <legend>Cadastro de endereço</legend>
                            <uc13:UCEnderecos ID="UCEnderecos1" runat="server" />
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
                        <asp:UpdatePanel ID="_updBotoes" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Button ID="_btnSalvar" runat="server" Text="Confirmar associação" OnClick="_btnSalvar_Click"
                                    ValidationGroup="Pessoa" />
                                <asp:Button ID="_btnCancelar" runat="server" Text="Cancelar" CausesValidation="False"
                                    OnClick="_btnCancelar_Click" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="_btnSalvar" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <input id="txtSelectedTab" type="hidden" class="txtSelectedTab" runat="server" />
                    </div>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
