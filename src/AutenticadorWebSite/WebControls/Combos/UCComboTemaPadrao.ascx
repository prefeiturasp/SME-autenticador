<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCComboTemaPadrao.ascx.cs" Inherits="AutenticadorWebSite.WebControls.Combos.UCComboTemaPadrao" %>
<asp:Label ID="lblTemaPadrao" runat="server" Text="Tema padrão" AssociatedControlID="ddlTemaPadrao"></asp:Label>
<asp:DropDownList SkinID="text30C" ID="ddlTemaPadrao" runat="server" 
    AutoPostBack="true" AppendDataBoundItems="True"
DataTextField="tep_descricao" DataValueField="tep_id" OnSelectedIndexChanged="ddlTemaPadrao_SelectedIndexChanged">
</asp:DropDownList>
<asp:CompareValidator ID="cpvTemaPadrao" runat="server" ControlToValidate="ddlTemaPadrao" Display="Dynamic" ValueToCompare="0" Operator="GreaterThan"
    ErrorMessage="Tema padrão é obrigatório." Visible="false">*</asp:CompareValidator>