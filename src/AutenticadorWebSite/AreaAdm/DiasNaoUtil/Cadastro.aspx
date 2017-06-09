<%@ Page Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true" Inherits="AreaAdm_DiasNaoUtil_Cadastro" Title="Untitled Page" Codebehind="Cadastro.aspx.cs" %>

<%@ PreviousPageType VirtualPath="~/AreaAdm/DiasNaoUtil/Busca.aspx" %>
<%@ Register Src="~/WebControls/Combos/UCComboUnidadeFederativa.ascx" TagName="UCComboUnidadeFederativa"
    TagPrefix="uc1" %>
<%@ Register Src="~/WebControls/Combos/UCComboCidade.ascx" TagName="UCComboCidade"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Label ID="_lblMessage" runat="server" EnableViewState="False"></asp:Label>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="dianaoutil" />
            <fieldset>
                <legend>Cadastro de dias não úteis</legend>
                <asp:Label ID="_lblNome" runat="server" Text="Nome *" AssociatedControlID="_txtNome"></asp:Label>
                <asp:TextBox ID="_txtNome" runat="server" MaxLength="50" SkinID="text60C"></asp:TextBox>
                <asp:RequiredFieldValidator ID="_rfvNome" runat="server" ControlToValidate="_txtNome"
                    Display="Dynamic" ErrorMessage="Nome é obrigatório." ValidationGroup="dianaoutil">*</asp:RequiredFieldValidator>
                <asp:CheckBox ID="_chkRecorrenciaAnual" runat="server" Text="Recorrência anual" OnCheckedChanged="_chkRecorrenciaAnual_CheckedChanged"
                    AutoPostBack="true" />
                <asp:Label ID="_lblData" runat="server" Text="Data *" AssociatedControlID="_txtData"></asp:Label>
                <asp:TextBox ID="_txtData" runat="server" MaxLength="10" SkinID="Data"></asp:TextBox>
                <asp:Label ID="_lblFormatoData" runat="server" Text="(DD/MM/AAAA)"></asp:Label>
                <asp:RequiredFieldValidator ID="_rfvData" runat="server" ControlToValidate="_txtData"
                    Display="Dynamic" ErrorMessage="Data é obrigatório." ValidationGroup="dianaoutil">*</asp:RequiredFieldValidator>
                <asp:CustomValidator ID="cvData" runat="server" ControlToValidate="_txtData" ValidationGroup="dianaoutil"
                    Display="Dynamic" ErrorMessage="Data inválida." OnServerValidate="ValidarData_ServerValidate">* </asp:CustomValidator>
                <asp:TextBox ID="_txtDataDia" runat="server" MaxLength="2" Width="29px" Visible="False"
                    CssClass="numeric" SkinID="Numerico"></asp:TextBox>
                <asp:RequiredFieldValidator ID="_rfvDataDia" runat="server" ControlToValidate="_txtDataDia"
                    Display="Dynamic" ErrorMessage="Dia é obrigatório." Visible="False" ValidationGroup="dianaoutil">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="_revDataDia" runat="server" ControlToValidate="_txtDataDia"
                    Display="Dynamic" ErrorMessage="Dia inválido." ValidationExpression="^([0-9]|[0,1,2][0-9]|3[0,1])$"
                    Visible="False" ValidationGroup="dianaoutil">*</asp:RegularExpressionValidator>
                <asp:Label ID="_lblBarra" runat="server" Text="/" Visible="False"></asp:Label>
                <asp:TextBox ID="_txtDataMes" runat="server" MaxLength="2" Width="29px" Visible="False"
                    CssClass="numeric" SkinID="Numerico"></asp:TextBox>
                <asp:RequiredFieldValidator ID="_rfvDataMes" runat="server" ControlToValidate="_txtDataMes"
                    Display="Dynamic" ErrorMessage="Mês é obrigatório." Visible="False" ValidationGroup="dianaoutil">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="_revDataMes" runat="server" ControlToValidate="_txtDataMes"
                    Display="Dynamic" ErrorMessage="Mês inválido." ValidationExpression="^([0-9]|[0,1][0-2]|0[0-9])$"
                    Visible="False" ValidationGroup="dianaoutil">*</asp:RegularExpressionValidator>
                <asp:Label ID="_lblFormatoData2" runat="server" Text="(DD/MM)" Visible="False"></asp:Label>
                <asp:Label ID="_lblVigenciaIni" runat="server" Text="Vigência inicial *" Visible="false"
                    AssociatedControlID="_txtVigenciaIni"></asp:Label>
                <asp:TextBox ID="_txtVigenciaIni" runat="server" MaxLength="10" Width="100px" Visible="false"
                    SkinID="DataSemCalendario"></asp:TextBox>
                <asp:RequiredFieldValidator ID="_rfvVigenciaIni" runat="server" ControlToValidate="_txtVigenciaIni"
                    Display="Dynamic" ErrorMessage="Data de vigência inicial é obrigatório." Visible="false" ValidationGroup="dianaoutil">*</asp:RequiredFieldValidator>
                <asp:CustomValidator ID="cvDataVigIni" runat="server" ControlToValidate="_txtVigenciaIni"
                    ValidationGroup="dianaoutil" Display="Dynamic" ErrorMessage="Data inválida."
                    OnServerValidate="ValidarData_ServerValidate" Visible="false">* </asp:CustomValidator>
                <asp:Label ID="_lblVigenciaFim" runat="server" Text="Vigência final" Visible="false"
                    AssociatedControlID="_lblVigenciaFim"></asp:Label>
                <asp:TextBox ID="_txtVigenciaFim" runat="server" MaxLength="10" Width="100px" Visible="false"
                    SkinID="DataSemCalendario"></asp:TextBox>
                <asp:CustomValidator ID="cvDataVigFim" runat="server" ControlToValidate="_txtVigenciaFim"
                    ValidationGroup="dianaoutil" Display="Dynamic" ErrorMessage="Data inválida."
                    OnServerValidate="ValidarData_ServerValidate">* </asp:CustomValidator>
                <asp:Label ID="_lblAbrangencia" runat="server" Text="Abrangência *" AssociatedControlID="_uppAbrangencia"></asp:Label>
                <asp:UpdatePanel ID="_uppAbrangencia" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList ID="_ddlAbrangencia" runat="server" AutoPostBack="True" OnSelectedIndexChanged="_ddlAbrangencia_SelectedIndexChanged"
                            SkinID="text30C">
                            <asp:ListItem Selected="True" Value="-1">-- Selecione uma abrangência --</asp:ListItem>
                            <asp:ListItem Value="1">Federal</asp:ListItem>
                            <asp:ListItem Value="2">Estadual</asp:ListItem>
                            <asp:ListItem Value="3">Municipal</asp:ListItem>
                        </asp:DropDownList>
                        <asp:CompareValidator ID="_cvAbrangencia" runat="server" ErrorMessage="Abrangência é obrigatório."
                            ControlToValidate="_ddlAbrangencia" Operator="GreaterThan" ValueToCompare="0"
                            Display="Dynamic" ValidationGroup="dianaoutil">*</asp:CompareValidator>
                        <uc1:UCComboUnidadeFederativa ID="_UCComboUnidadeFederativa" runat="server" />
                        <uc2:UCComboCidade ID="_UCComboCidade" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:Label ID="_lblDescricao" runat="server" Text="Observação" AssociatedControlID="_txtDescricao"></asp:Label>
                <asp:TextBox ID="_txtDescricao" runat="server" TextMode="MultiLine" SkinID="text60C"></asp:TextBox>
                <div class="right">
                    <asp:Button ID="_btnSalvar" runat="server" Text="Salvar" OnClick="_btnSalvar_Click"
                        ValidationGroup="dianaoutil" />
                    <asp:Button ID="_btnCancelar" runat="server" CausesValidation="False" OnClick="_btnCancelar_Click"
                        Text="Cancelar" />
                </div>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
