<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCComboCidade.ascx.cs" Inherits="Autenticador.UserControlLibrary.Combos.UCComboCidade" %>
<asp:Label ID="_lblRotulo" runat="server" AssociatedControlID="_ddlCombo" 
    Text="Cidade"></asp:Label>
<asp:DropDownList ID="_ddlCombo" runat="server" DataSourceID="_odsCombo" 
    DataTextField="cid_nome" DataValueField="cid_id" SkinID="text30C"
    AppendDataBoundItems="True">
</asp:DropDownList>
<asp:CompareValidator ID="_cpvCombo" runat="server" 
    ControlToValidate="_ddlCombo" Display="Dynamic" 
    ErrorMessage="{0} é obrigatório." Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
<asp:ObjectDataSource ID="_odsCombo" runat="server"
    DataObjectTypeName="Autenticador.Entities.END_Cidade" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetSelect"  
    TypeName="Autenticador.BLL.END_CidadeBO" EnablePaging="True" MaximumRowsParameterName="pageSize" SelectCountMethod="GetTotalRecords" StartRowIndexParameterName="currentPage">
</asp:ObjectDataSource>

