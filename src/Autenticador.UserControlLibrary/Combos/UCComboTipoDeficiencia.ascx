<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCComboTipoDeficiencia.ascx.cs" Inherits="Autenticador.UserControlLibrary.Combos.UCComboTipoDeficiencia" %>
<asp:Label ID="_lblRotulo" runat="server" AssociatedControlID="_ddlCombo" 
    Text="Tipo de deficiência"></asp:Label>
<asp:DropDownList ID="_ddlCombo" runat="server" DataSourceID="_odsCombo" 
    DataTextField="tde_nome" DataValueField="tde_id" SkinID="text30C"
    AppendDataBoundItems="True">
</asp:DropDownList>
<asp:CompareValidator ID="_cpvCombo" runat="server" 
    ControlToValidate="_ddlCombo" Display="Dynamic" 
    ErrorMessage="{0} é obrigatório" Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
<asp:ObjectDataSource ID="_odsCombo" runat="server" 
    DataObjectTypeName="Autenticador.Entities.PES_TipoDeficiencia" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetSelect" 
    TypeName="Autenticador.BLL.PES_TipoDeficienciaBO" 
    onselecting="_odsCombo_Selecting" EnablePaging="True" 
    MaximumRowsParameterName="pageSize" SelectCountMethod="GetTotalRecords" 
    StartRowIndexParameterName="currentPage">
</asp:ObjectDataSource>