<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCComboSistemaGrupo.ascx.cs" Inherits="Autenticador.UserControlLibrary.Combos.UCComboSistemaGrupo" %>
<asp:Label ID="_lblRotulo" runat="server" AssociatedControlID="_ddlCombo" 
    Text="Sistema - grupo"></asp:Label>
<asp:DropDownList ID="_ddlCombo" runat="server" DataSourceID="_odsCombo" 
    DataTextField="sis_gru_Nome" DataValueField="sis_gru_id" SkinID="text60C"
    AppendDataBoundItems="True">
</asp:DropDownList>
<asp:CompareValidator ID="_cpvCombo" runat="server" 
    ControlToValidate="_ddlCombo" Display="Dynamic" 
    ErrorMessage="{0} é obrigatório" Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
<asp:ObjectDataSource ID="_odsCombo" runat="server" 
    DataObjectTypeName="Autenticador.Entities.SYS_Grupo" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetSelect" 
    TypeName="Autenticador.BLL.SYS_GrupoBO" 
    onselecting="_odsCombo_Selecting" EnablePaging="True" 
    MaximumRowsParameterName="pageSize" SelectCountMethod="GetTotalRecords" 
    StartRowIndexParameterName="currentPage">
</asp:ObjectDataSource>
