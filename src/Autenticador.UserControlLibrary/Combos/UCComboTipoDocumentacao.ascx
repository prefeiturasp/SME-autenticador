<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCComboTipoDocumentacao.ascx.cs" Inherits="Autenticador.UserControlLibrary.Combos.UCComboTipoDocumentacao" %>
<asp:Label ID="_lblRotulo" runat="server" AssociatedControlID="_ddlCombo" Text="Tipo de documento"></asp:Label>
<asp:DropDownList ID="_ddlCombo" runat="server" DataSourceID="_odsCombo" DataTextField="tdo_nome"
    DataValueField="tdo_id" AppendDataBoundItems="True" SkinID="text30C">
</asp:DropDownList>
<asp:CompareValidator ID="_cpvCombo" runat="server" ControlToValidate="_ddlCombo"
    Display="Dynamic" ErrorMessage="{0} é obrigatório" Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
<asp:ObjectDataSource ID="_odsCombo" runat="server" DataObjectTypeName="Autenticador.Entities.SYS_TipoDocumentacao"
    SelectMethod="GetSelect" TypeName="Autenticador.BLL.SYS_TipoDocumentacaoBO"
    OnSelecting="_odsCombo_Selecting" EnablePaging="True" MaximumRowsParameterName="pageSize"
    SelectCountMethod="GetTotalRecords" StartRowIndexParameterName="currentPage">
</asp:ObjectDataSource>
