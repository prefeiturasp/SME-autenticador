<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCComboTemaPaleta.ascx.cs" Inherits="AutenticadorWebSite.WebControls.Combos.UCComboTemaPaleta" %>
<asp:Label ID="lblTemaPaleta" runat="server" Text="Tema de cores" AssociatedControlID="ddlTemaPaleta"></asp:Label>
<asp:DropDownList SkinID="text30C" ID="ddlTemaPaleta" runat="server" 
    AutoPostBack="true" AppendDataBoundItems="True"
DataTextField="tpl_nome" DataValueField="id" OnSelectedIndexChanged="ddlTemaPaleta_SelectedIndexChanged">
</asp:DropDownList>
<asp:CompareValidator ID="cpvTemaPaleta" runat="server" ControlToValidate="ddlTemaPaleta" Display="Dynamic" ValueToCompare="0" Operator="GreaterThan"
    ErrorMessage="Tema de cores é obrigatório." Visible="false">*</asp:CompareValidator>