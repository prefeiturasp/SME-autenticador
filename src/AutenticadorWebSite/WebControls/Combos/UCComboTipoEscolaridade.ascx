<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCComboTipoEscolaridade.ascx.cs" Inherits="Autenticador.UserControlLibrary.Combos.UCComboTipoEscolaridade" %>
<asp:Label ID="_lblRotulo" runat="server" AssociatedControlID="_ddlCombo" Text="Tipo de escolaridade"></asp:Label>
<asp:DropDownList ID="_ddlCombo" runat="server" DataSourceID="_odsCombo" DataTextField="tes_nome"
    DataValueField="tes_id" AppendDataBoundItems="True" SkinID="text30C">
</asp:DropDownList>
<asp:CompareValidator ID="_cpvCombo" runat="server" ControlToValidate="_ddlCombo"
    Display="Dynamic" ErrorMessage="{0} é obrigatório" Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
<asp:ObjectDataSource ID="_odsCombo" runat="server" DataObjectTypeName="Autenticador.Entities.PES_TipoEscolaridade"
    SelectMethod="GetSelect" TypeName="Autenticador.BLL.PES_TipoEscolaridadeBO"
    OnSelecting="_odsCombo_Selecting" EnablePaging="True" MaximumRowsParameterName="pageSize"
    SelectCountMethod="GetTotalRecords" 
    StartRowIndexParameterName="currentPage">
</asp:ObjectDataSource>
