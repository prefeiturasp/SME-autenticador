<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCComboGrupo.ascx.cs"
    Inherits="Autenticador.UserControlLibrary.Combos.UCComboGrupo" %>
    
<asp:Label ID="_lblRotulo" runat="server" AssociatedControlID="_ddlCombo" Text="Grupo"></asp:Label>
<asp:DropDownList ID="_ddlCombo" runat="server" DataSourceID="_odsCombo" DataTextField="gru_nome"
    DataValueField="gru_id" SkinID="text30C" AppendDataBoundItems="True">
</asp:DropDownList>
<asp:CompareValidator ID="_cpvCombo" runat="server" ControlToValidate="_ddlCombo"
    Display="Dynamic" ErrorMessage="{0} é obrigatório." Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
<asp:ObjectDataSource ID="_odsCombo" runat="server" DataObjectTypeName="Autenticador.Entities.SYS_Grupo"
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetSelect_sis_id_vis_id" TypeName="Autenticador.BLL.SYS_GrupoBO"
    EnablePaging="True" MaximumRowsParameterName="pageSize" SelectCountMethod="GetTotalRecords"
    StartRowIndexParameterName="currentPage"></asp:ObjectDataSource>
