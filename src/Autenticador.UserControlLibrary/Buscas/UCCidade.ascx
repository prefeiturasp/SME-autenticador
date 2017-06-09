<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCCidade.ascx.cs" Inherits="Autenticador.UserControlLibrary.Buscas.UCCidade" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="../Combos/UCComboPais.ascx" TagName="UCComboPais" TagPrefix="coresso" %>
<%@ Register Src="../Combos/UCComboUnidadeFederativa.ascx" TagName="UCComboUnidadeFederativa"
    TagPrefix="coresso" %>
<asp:Label ID="_lblMessage" runat="server" EnableViewState="False"></asp:Label>
<fieldset>
    <coresso:UCComboPais ID="UCComboPais1" runat="server" />
    <coresso:UCComboUnidadeFederativa ID="UCComboUnidadeFederativa1" runat="server" />
    <asp:Label ID="LabelCidade" runat="server" Text="Cidade" AssociatedControlID="_txtCidade"></asp:Label>
    <asp:TextBox ID="_txtCidade" runat="server" MaxLength="200" SkinID="text30C" Height="22px"
        Width="128px"></asp:TextBox>
    <div class="right">
        <asp:Button ID="_btnPesquisar" runat="server" Text="Pesquisar" CausesValidation="False"
            OnClick="_btnPesquisar_Click" />
    </div>
</fieldset>
<fieldset id="fdsResultados" runat="server">
    <legend>Resultados</legend>
    <asp:GridView ID="_grvCidade" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        BorderStyle="None" EmptyDataText="A pesquisa não encontrou resultados." OnRowEditing="_grvCidade_RowEditing"
        DataSourceID="odsCidade" DataKeyNames="cid_id,cid_nome">
        <EmptyDataRowStyle Font-Bold="True" ForeColor="Red" />
        <Columns>
            <asp:TemplateField HeaderText="Cidade">
                <ItemTemplate>
                    <asp:LinkButton ID="_lkbSelecionar" runat="server" CausesValidation="False" CommandName="Edit"
                        OnClientClick="$('#divCidades', window.parent.document).dialog('close');" Text='<%# Bind("cid_nome") %>'></asp:LinkButton>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("cid_nome") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="unf_nome" HeaderText="Estado" />
            <asp:BoundField DataField="pai_nome" HeaderText="País" />
        </Columns>
        <EditRowStyle Font-Bold="True" ForeColor="Red" />
    </asp:GridView>
</fieldset>
<asp:ObjectDataSource ID="odsCidade" runat="server" DataObjectTypeName="Autenticador.Entities.END_Cidade"
    SelectCountMethod="GetTotalRecords" SelectMethod="GetSelect" EnablePaging="True"
    MaximumRowsParameterName="pageSize" StartRowIndexParameterName="currentPage"
    TypeName="Autenticador.BLL.END_CidadeBO" 
    DeleteMethod="Delete" onselecting="odsCidade_Selecting">
</asp:ObjectDataSource>
