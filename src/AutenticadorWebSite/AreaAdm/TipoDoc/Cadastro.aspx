<%@ Page Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true" Inherits="AreaAdm_TipoDoc_Cadastro" Title="Untitled Page" CodeBehind="Cadastro.aspx.cs" %>

<%@ PreviousPageType VirtualPath="~/AreaAdm/TipoDoc/Busca.aspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="_lblMessage" runat="server" EnableViewState="false"></asp:Label>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    <fieldset>
        <legend>Cadastro de tipo de documentação</legend>

        <asp:Label ID="_lblClassificacao" runat="server" Text="Classificação *" AssociatedControlID="_ddlClassificacao"></asp:Label>
        <asp:DropDownList ID="_ddlClassificacao" runat="server" SkinID="text30C" OnSelectedIndexChanged="_ddlClassificacao_SelectedIndexChanged" AutoPostBack="True">
            <asp:ListItem Value="-1">-- Selecione uma classificação --</asp:ListItem>
            <asp:ListItem Value="6">CNH</asp:ListItem>
            <asp:ListItem Value="1">CPF</asp:ListItem>
            <asp:ListItem Value="8">CTPS</asp:ListItem>
            <asp:ListItem Value="10">Guarda</asp:ListItem>
            <asp:ListItem Value="4">NIS</asp:ListItem>
            <asp:ListItem Value="3">PIS</asp:ListItem>
            <asp:ListItem Value="7">Reservista</asp:ListItem>
            <asp:ListItem Value="2">RG</asp:ListItem>
            <asp:ListItem Value="9">RNE</asp:ListItem>
            <asp:ListItem Value="5">Titulo de eleitor</asp:ListItem>
            <asp:ListItem Value="99">Outros</asp:ListItem>
        </asp:DropDownList>

        <asp:CompareValidator
            ID="_cpvClassificacao"
            runat="server"
            ControlToValidate="_ddlClassificacao"
            Display="Dynamic"
            ErrorMessage="Classificação é obrigatório."
            Operator="NotEqual"
            ValueToCompare="-1">*</asp:CompareValidator>

        <asp:Label ID="_lblDocumento" runat="server" Text="Documento *" AssociatedControlID="_txtDocumento"></asp:Label>
        <asp:TextBox ID="_txtDocumento" runat="server" SkinID="text60C" MaxLength="100"></asp:TextBox>
        <asp:RequiredFieldValidator
            ID="_rfvNome"
            runat="server"
            ControlToValidate="_txtDocumento"
            Display="Dynamic"
            ErrorMessage="Documento é obrigatório.">*</asp:RequiredFieldValidator>

        <asp:Label ID="_lblSigla" runat="server" Text="Sigla" AssociatedControlID="_txtSigla"></asp:Label>
        <asp:TextBox ID="_txtSigla" runat="server" SkinID="text10C"></asp:TextBox>

        <asp:Label ID="lblValidacao" runat="server" Text="Validação" AssociatedControlID="_ddlValidacao"></asp:Label>
        <asp:DropDownList ID="_ddlValidacao" runat="server" SkinID="text30C">
            <asp:ListItem Value="0">-- Selecione uma validação --</asp:ListItem>
            <asp:ListItem Value="1">CPF</asp:ListItem>
            <asp:ListItem Value="2">Somente números</asp:ListItem>
        </asp:DropDownList>

        <asp:CheckBox ID="_ckbBloqueado" Text="Bloqueado" runat="server" />

        <br />

        <div style="width: 45%; float: none; clear: none;">
            <fieldset id="fdsAtributos" runat="server" style="margin-right: 10px;" visible="true">
                <legend>Atributos</legend>
                <div id="_divAtributos" class="divRelatorio" runat="server">

                    <asp:CheckBoxList
                        ID="_ckbAtributos"
                        runat="server"
                        CellSpacing="5"
                        AutoPostBack="false"
                        OnSelectedIndexChanged="_odsAtributos_SelectedIndexChanged">
                    </asp:CheckBoxList>
                </div>
            </fieldset>
        </div>

        <div class="right">
            <asp:Button
                ID="_bntSalvar"
                runat="server"
                Text="Salvar"
                OnClick="_bntSalvar_Click" />

            <asp:Button
                ID="_btnCancelar"
                runat="server"
                Text="Cancelar"
                CausesValidation="false"
                OnClick="_btnCancelar_Click" />
        </div>
    </fieldset>
</asp:Content>