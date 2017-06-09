<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCComboTipoMeioContato.ascx.cs" Inherits="Autenticador.UserControlLibrary.Combos.UCComboTipoMeioContato" %>
<asp:Label ID="_lblRotulo" runat="server" AssociatedControlID="_ddlCombo" Text="Tipo de meio de contato"></asp:Label>
<asp:DropDownList ID="_ddlCombo" runat="server" DataSourceID="_odsCombo" DataTextField="tmc_nome"
    DataValueField="tmc_id" AppendDataBoundItems="True" SkinID="text30C">
</asp:DropDownList>
<asp:CompareValidator ID="_cpvCombo" runat="server" ControlToValidate="_ddlCombo"
    Display="Dynamic" ErrorMessage="{0} é obrigatório" Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
<asp:ObjectDataSource ID="_odsCombo" runat="server" DataObjectTypeName="Autenticador.Entities.SYS_TipoMeioContato"
    SelectMethod="GetSelect" TypeName="Autenticador.BLL.SYS_TipoMeioContatoBO"
    OnSelecting="_odsCombo_Selecting" EnablePaging="True" MaximumRowsParameterName="pageSize"
    SelectCountMethod="GetTotalRecords" 
    StartRowIndexParameterName="currentPage">
</asp:ObjectDataSource>
