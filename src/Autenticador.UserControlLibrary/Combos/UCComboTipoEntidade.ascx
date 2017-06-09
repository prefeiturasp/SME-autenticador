<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCComboTipoEntidade.ascx.cs"
    Inherits="Autenticador.UserControlLibrary.Combos.UCComboTipoEntidade" %>
<asp:Label ID="_lblRotulo" runat="server" AssociatedControlID="_ddlCombo" Text="Tipo de entidade"></asp:Label>
<asp:DropDownList ID="_ddlCombo" runat="server" DataSourceID="_odsCombo" DataTextField="ten_nome"
    DataValueField="ten_id" AppendDataBoundItems="True" SkinID="text30C">
</asp:DropDownList>
<asp:CompareValidator ID="_cpvCombo" runat="server" ControlToValidate="_ddlCombo"
    Display="Dynamic" ErrorMessage="{0} é obrigatório" Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
<asp:ObjectDataSource ID="_odsCombo" runat="server" DataObjectTypeName="Autenticador.Entities.SYS_TipoEntidade"
    SelectMethod="GetSelect" TypeName="Autenticador.BLL.SYS_TipoEntidadeBO"
    OnSelecting="_odsCombo_Selecting" EnablePaging="True" MaximumRowsParameterName="pageSize"
    SelectCountMethod="GetTotalRecords" StartRowIndexParameterName="currentPage">
</asp:ObjectDataSource>
