<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCComboSexo.ascx.cs" Inherits="Autenticador.UserControlLibrary.Combos.UCComboSexo" %>
<asp:Label ID="_lblRotulo" runat="server" AssociatedControlID="_ddlCombo" Text="Sexo"></asp:Label>
<asp:DropDownList ID="_ddlCombo" runat="server" AppendDataBoundItems="True" SkinID="text30C">
    <asp:ListItem Value="-1">-- Selecione uma opção --</asp:ListItem>
     <asp:ListItem Value="1">Masculino</asp:ListItem>
    <asp:ListItem Value="2">Feminino</asp:ListItem>  
</asp:DropDownList>
<asp:CompareValidator ID="_cpvCombo" runat="server" ControlToValidate="_ddlCombo"
    Display="Dynamic" ErrorMessage="{0} é obrigatório" Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
