<%@ Page Language="C#" AutoEventWireup="true" Inherits="AreaAdm_Cidade_Cadastro" Codebehind="Cadastro.aspx.cs" %>

<%@ Register Src="../../WebControls/Combos/UCComboPais.ascx" TagName="UCComboPais"
    TagPrefix="uc1" %>
<%@ Register Src="../../WebControls/Combos/UCComboUnidadeFederativa.ascx" TagName="UCComboUnidadeFederativa"
    TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Scripts>
        </Scripts>
    </asp:ScriptManager>
    <asp:UpdatePanel ID="_updCidades" runat="server">
        <ContentTemplate>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="vlgPais" />
            <asp:Label ID="_lblMessage" runat="server" EnableViewState="False"></asp:Label>
            
                <uc1:UCComboPais ID="UCComboPais1" runat="server"/>                
                <uc2:UCComboUnidadeFederativa ID="UCComboUnidadeFederativa1" runat="server" />
                <asp:Label ID="LabelCidade" runat="server" Text="Cidade *" AssociatedControlID="_txtCidade"></asp:Label>
                <asp:TextBox ID="_txtCidade" runat="server" MaxLength="200" SkinID="text30C"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCidade" runat="server" ControlToValidate="_txtCidade"
                    ErrorMessage="Cidade é obrigatório." ValidationGroup="vlgPais">*</asp:RequiredFieldValidator>
                <asp:Label ID="LabelDDD" runat="server" Text="DDD" AssociatedControlID="_txtDDD"></asp:Label>
                <asp:TextBox ID="_txtDDD" runat="server" MaxLength="3" CssClass="numeric" SkinID="Numerico"></asp:TextBox>
                <asp:RegularExpressionValidator ID="_revDDD" runat="server" ControlToValidate="_txtDDD"
                    ValidationGroup="vlgPais" Display="Dynamic" ErrorMessage="DDD inválido."
                    ValidationExpression="^([0-9]){1,10}$">*</asp:RegularExpressionValidator>
                <div class="right">
                    <asp:Button ID="_btnSalvar" runat="server" Text="Salvar" OnClick="_btnSalvar_Click"
                        ValidationGroup="vlgPais" />
                    <asp:Button ID="_btnCancelar" runat="server" Text="Cancelar" CausesValidation="False"
                        OnClick="_btnCancelar_Click" />
                </div>
            
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
