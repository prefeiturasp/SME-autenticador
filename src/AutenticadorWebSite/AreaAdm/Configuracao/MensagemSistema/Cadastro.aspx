<%@ Page Title="" Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="AutenticadorWebSite.AreaAdm.Configuracao.MensagemSistema.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <asp:UpdatePanel ID="updMessage" runat="server" UpdateMode="Conditional" EnableViewState="False">
        <ContentTemplate>
            <asp:Label ID="lblMessage" runat="server" EnableViewState="False"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="MensagemSistema" />
    <fieldset>
        <legend>Mensagens do sistema</legend>
        <asp:Button ID="btnNovo" runat="server" Text="Incluir novo parâmetro de mensagem"
            CausesValidation="false" OnClick="btnNovo_Click" />
        <asp:UpdatePanel ID="updParametro" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="grvMensagem" runat="server" AutoGenerateColumns="False"
                    EmptyDataText="Não existem parâmetros de mensagem cadastrados." DataKeyNames="mss_id, mss_situacao, IsNew"
                    OnDataBinding="grvMensagem_DataBinding" OnRowDataBound="grvMensagem_RowDataBound"
                    OnRowEditing="grvMensagem_RowEditing" OnRowUpdating="grvMensagem_RowUpdating"
                    OnRowDeleting="grvMensagem_RowDeleting" OnRowCancelingEdit="grvMensagem_RowCancelingEdit">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lbl1" runat="server" Text="Chave *" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="_lblChave" runat="server" 
                                    Text='<%# Bind("mss_chave") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtChave" runat="server" Text='<%# Bind("mss_chave") %>' MaxLength="100"
                                    SkinID="text20C"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="_rfvChave" runat="server" ErrorMessage="Chave é obrigatório."
                                    ControlToValidate="txtChave" ValidationGroup="MensagemSistema">*</asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <HeaderStyle Width="300px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Descrição">
                            <ItemTemplate>
                                <asp:Label ID="lblDescricao" runat="server" Text='<%# Bind("mss_descricao") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDescricao" runat="server" Text='<%# Bind("mss_descricao") %>'
                                    MaxLength="200" SkinID="text15C"></asp:TextBox>
                            </EditItemTemplate>
                            <HeaderStyle Width="250px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Valor *">
                            <HeaderTemplate>
                                <asp:Label ID="lbl1" runat="server" Text="Valor *" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblValor" runat="server" Text='<%# Bind("mss_valor") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtValor" runat="server" 
                                    Text='<%# Bind("mss_valor") %>' MaxLength="2000"
                                    TextMode="MultiLine" Rows="5" Columns="10"
                                    SkinID="text30C" Width="220px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvValor" runat="server" ErrorMessage="Valor é obrigatório."
                                    ControlToValidate="txtValor" ValidationGroup="MensagemSistema">*</asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <HeaderStyle Width="250px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Editar" HeaderStyle-CssClass="center">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEditar" runat="server" CommandName="Edit" SkinID="btEditar"
                                    ToolTip="Editar mensagem do sistema" CausesValidation="false" />
                                <asp:ImageButton ID="imgCancelar" runat="server" CommandName="Cancel" SkinID="btDesfazer"
                                    ToolTip="Cancelar edição" CausesValidation="false" Visible="false" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Salvar" HeaderStyle-CssClass="center">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgSalvar" runat="server" CommandName="Update" SkinID="btConfirmar"
                                    ToolTip="Salvar mensagem do sistema" ValidationGroup="MensagemSistema" Visible="false" />
                                <asp:ImageButton ID="imgCancelarParametro" runat="server" CommandName="Cancel"
                                    SkinID="btCancelar" ToolTip="Cancelar nova mensagem do sistema" CausesValidation="false"
                                    Visible="false" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Excluir" HeaderStyle-CssClass="center">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgExcluir" runat="server" CommandName="Delete" SkinID="btExcluir"
                                    ToolTip="Excluir mensagem do sistema" CausesValidation="false" />
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
    </fieldset>
</asp:Content>
