<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCComboUnidadeAdministrativa.ascx.cs" Inherits="Autenticador.UserControlLibrary.Combos.UCComboUnidadeAdministrativa" %>
<asp:Label ID="_lblRotulo" runat="server" AssociatedControlID="_ddlCombo" 
    Text="Unidade administrativa"></asp:Label>
<asp:DropDownList ID="_ddlCombo" runat="server" DataSourceID="_odsCombo" DataTextField="uad_nome"
    DataValueField="uad_id" AppendDataBoundItems="True" SkinID="text30C">
</asp:DropDownList>
<asp:CompareValidator ID="_cpvCombo" runat="server" ControlToValidate="_ddlCombo"
    Display="Dynamic" ErrorMessage="{0} é obrigatório" Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
<asp:ObjectDataSource ID="_odsCombo" runat="server" DataObjectTypeName="Autenticador.Entities.SYS_UnidadeAdministrativa"
    SelectMethod="GetSelect" TypeName="Autenticador.BLL.SYS_UnidadeAdministrativaBO"
    OnSelecting="_odsCombo_Selecting" EnablePaging="True" MaximumRowsParameterName="pageSize"
    SelectCountMethod="GetTotalRecords" 
    StartRowIndexParameterName="currentPage">
</asp:ObjectDataSource>
