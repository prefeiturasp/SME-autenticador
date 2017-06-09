<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCComboSistema.ascx.cs"
    Inherits="Autenticador.UserControlLibrary.Combos.UCComboSistema" %>
<asp:Label ID="_lblRotulo" runat="server" AssociatedControlID="_ddlCombo" Text="Sistema"></asp:Label>
<asp:DropDownList ID="_ddlCombo" runat="server" DataSourceID="_odsCombo" DataTextField="sis_nome"
    DataValueField="sis_id" SkinID="text30C" AppendDataBoundItems="True">
</asp:DropDownList>
<asp:CompareValidator ID="_cpvCombo" runat="server" ControlToValidate="_ddlCombo"
    Display="Dynamic" ErrorMessage="{0} é obrigatório." Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
<asp:ObjectDataSource ID="_odsCombo" runat="server" DataObjectTypeName="Autenticador.Entities.SYS_Sistema"
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetSelect" TypeName="Autenticador.BLL.SYS_SistemaBO"
    EnablePaging="True" MaximumRowsParameterName="pageSize"
    SelectCountMethod="GetTotalRecords" StartRowIndexParameterName="currentPage">
</asp:ObjectDataSource>