﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCComboZona.ascx.cs" Inherits="Autenticador.UserControlLibrary.Combos.UCComboZona" %>
<asp:Label ID="_lblRotulo" runat="server" AssociatedControlID="_ddlCombo" Text="Zona"></asp:Label>
<asp:DropDownList ID="_ddlCombo" runat="server" AppendDataBoundItems="True" CssClass="text30C tbZona_incremental">
    <asp:ListItem Value="-1">-- Selecione uma opção --</asp:ListItem>
    <asp:ListItem Value="1">Urbana</asp:ListItem>    
    <asp:ListItem Value="2">Rural</asp:ListItem>
</asp:DropDownList>
<asp:CompareValidator ID="_cpvCombo" runat="server" ControlToValidate="_ddlCombo"
    Display="Dynamic" ErrorMessage="Zona é obrigatório." Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
