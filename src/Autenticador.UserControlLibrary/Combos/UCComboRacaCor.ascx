<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCComboRacaCor.ascx.cs" Inherits="Autenticador.UserControlLibrary.Combos.UCComboRacaCor" %>
<asp:Label ID="_lblRotulo" runat="server" AssociatedControlID="_ddlCombo" Text="Raça / cor"></asp:Label>
<asp:DropDownList ID="_ddlCombo" runat="server" AppendDataBoundItems="True" SkinID="text30C">
    <asp:ListItem Value="-1">-- Selecione uma opção --</asp:ListItem>
    <asp:ListItem Value="1">Branca</asp:ListItem>
    <asp:ListItem Value="2">Preta</asp:ListItem>
    <asp:ListItem Value="3">Parda</asp:ListItem>
    <asp:ListItem Value="4">Amarela</asp:ListItem>
    <asp:ListItem Value="5">Indígena</asp:ListItem>
    <asp:ListItem Value="6">Não declarada</asp:ListItem>   
</asp:DropDownList>
<asp:CompareValidator ID="_cpvCombo" runat="server" ControlToValidate="_ddlCombo"
    Display="Dynamic" ErrorMessage="{0} é obrigatório" Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
