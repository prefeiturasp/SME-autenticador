<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCComboEstadoCivil.ascx.cs" Inherits="Autenticador.UserControlLibrary.Combos.UCComboEstadoCivil" %>
<asp:Label ID="_lblRotulo" runat="server" AssociatedControlID="_ddlCombo" Text="Estado civil"></asp:Label>
<asp:DropDownList ID="_ddlCombo" runat="server" AppendDataBoundItems="True" SkinID="text30C">
  <asp:ListItem Value="-1">-- Selecione uma opção --</asp:ListItem>
    <asp:ListItem Value="1">Solteiro (a)</asp:ListItem>
    <asp:ListItem Value="2">Casado (a)</asp:ListItem>
    <asp:ListItem Value="3">Separado (a)</asp:ListItem>
    <asp:ListItem Value="4">Divorciado (a)</asp:ListItem>
    <asp:ListItem Value="5">Viúvo (a)</asp:ListItem>
</asp:DropDownList>
<asp:CompareValidator ID="_cpvCombo" runat="server" ControlToValidate="_ddlCombo"
    Display="Dynamic" ErrorMessage="{0} é obrigatório" Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
