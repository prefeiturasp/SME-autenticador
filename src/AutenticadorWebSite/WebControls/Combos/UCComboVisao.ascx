<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCComboVisao.ascx.cs" Inherits="Autenticador.UserControlLibrary.Combos.UCComboVisao" %>
<asp:Label ID="_lblRotulo" runat="server" AssociatedControlID="_ddlCombo" 
    Text="Visão *"></asp:Label>
<asp:DropDownList ID="_ddlCombo" runat="server" DataSourceID="_odsCombo" 
    DataTextField="vis_nome" DataValueField="vis_id" SkinID="text30C">
</asp:DropDownList>
<asp:CompareValidator ID="_cpvCombo" runat="server" 
    ControlToValidate="_ddlCombo" Display="Dynamic" 
    ErrorMessage="{0} é obrigatório." Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
<asp:ObjectDataSource ID="_odsCombo" runat="server" 
    DataObjectTypeName="Autenticador.Entities.SYS_Visao" 
    TypeName="Autenticador.BLL.SYS_VisaoBO"
    OldValuesParameterFormatString="original_{0}"
    SelectMethod="GetSelect">
</asp:ObjectDataSource>
