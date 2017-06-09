<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageLogin.master" AutoEventWireup="true" Inherits="AreaAdm_Login" CodeBehind="Login.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="lblMessage" runat="server" EnableViewState="false"></asp:Label>
    <div id="divGrupos" runat="server" visible="false" style="width: 250px; position: absolute; top: 50%; left: 50%; margin-left: -125px" class="area-selecao-grupo">
        <fieldset>
            <legend>Seleção de grupo</legend>
            <asp:UpdatePanel ID="_updGrupos" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="10" AssociatedUpdatePanelID="_updGrupos">
                        <ProgressTemplate>
                            <div class="loader">
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <asp:Repeater ID="rptGrupos" runat="server" OnItemCommand="_rptGrupos_ItemCommand">
                        <ItemTemplate>
                            <asp:LinkButton ID="lkbGrupo" runat="server" Text='<%# Bind("gru_nome") %>' CommandArgument='<%# Bind("gru_id") %>' CommandName="Select" CausesValidation="false" Style="display: block; text-align: center; padding: 5px 0" />

                        </ItemTemplate>
                    </asp:Repeater>
                </ContentTemplate>
            </asp:UpdatePanel>
        </fieldset>
    </div>
    <asp:Button ID="btnVoltar" runat="server" Text="Voltar" CausesValidation="false"
        Visible="false" OnClick="btnVoltar_Click" />
</asp:Content>

