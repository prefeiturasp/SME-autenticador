<%@ Page Title="" Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true" Inherits="AreaAdm_Configuracao_Sistema_Cadastro" Codebehind="Cadastro.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="updMessage" runat="server" UpdateMode="Conditional" EnableViewState="False">
        <ContentTemplate>
            <asp:Label ID="lblMessage" runat="server" EnableViewState="False"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Config" />
    <fieldset>
        <legend>Configurações do sistema</legend>
        <div></div>
        <asp:Button ID="btnNovo" runat="server" Text="Nova configuração" 
            CausesValidation="false" onclick="btnNovo_Click"/>
        <asp:UpdatePanel ID="updConfig" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="grvConfig" runat="server" AutoGenerateColumns="False" DataKeyNames="cfg_id, cfg_situacao, IsNew"
                    EmptyDataText="Não existem configurações cadastradas."
                    onrowediting="grvConfig_RowEditing" ondatabinding="grvConfig_DataBinding" 
                    onrowdeleting="grvConfig_RowDeleting" 
                    onrowupdating="grvConfig_RowUpdating" 
                    onrowdatabound="grvConfig_RowDataBound" 
                    onrowcancelingedit="grvConfig_RowCancelingEdit">
                    <Columns>
                        <asp:TemplateField HeaderText="Chave *">
                            <ItemTemplate>
                                <asp:Label ID="lblChave" runat="server" Text='<%# Bind("cfg_chave") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtChave" runat="server" Text='<%# Bind("cfg_chave") %>' SkinID="text30C"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvChave" runat="server" ErrorMessage="Chave é obrigatório." 
                                    ControlToValidate="txtChave" ValidationGroup="Config">*</asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <HeaderStyle Width="320px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Descrição">
                            <ItemTemplate>
                                <asp:Label ID="lblDescricao" runat="server" Text='<%# Bind("cfg_descricao") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDescricao" runat="server" Text='<%# Bind("cfg_descricao") %>'
                                    SkinID="text30C"></asp:TextBox>
                            </EditItemTemplate>
                            <HeaderStyle Width="320px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Valor *">
                            <ItemTemplate>
                                <asp:Label ID="lblValor" runat="server" Text='<%# Bind("cfg_valor") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtValor" runat="server" Text='<%# Bind("cfg_valor") %>' SkinID="text30C"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvValor" runat="server" ErrorMessage="Valor é obrigatório."
                                    ControlToValidate="txtValor" ValidationGroup="Config">*</asp:RequiredFieldValidator>
                            </EditItemTemplate>
                             <HeaderStyle Width="320px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Editar" HeaderStyle-CssClass="center">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEditar" runat="server" CommandName="Edit" SkinID="btEditar" ToolTip="Editar configuração" 
                                    CausesValidation="false"/>
                                <asp:ImageButton ID="imgCancelar" runat="server" CommandName="Cancel" SkinID="btDesfazer" ToolTip="Cancelar edição"
                                    CausesValidation="false" Visible="false"/>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Salvar" HeaderStyle-CssClass="center">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgSalvar" runat="server" CommandName="Update" SkinID="btConfirmar" ToolTip="Salvar configuração"
                                    ValidationGroup="Config" Visible="false"/>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Excluir" HeaderStyle-CssClass="center">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgExcluir" runat="server" CommandName="Delete" SkinID="btExcluir" ToolTip="Excluir configuração"
                                    CausesValidation="false"/>
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

