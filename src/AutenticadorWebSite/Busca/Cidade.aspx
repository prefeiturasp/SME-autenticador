<%@ Page Language="C#" MasterPageFile="~/Busca/MasterPageBusca.master" AutoEventWireup="true" Inherits="Busca_Cidade" Codebehind="Cidade.aspx.cs" %>

<%@ Register Src="../WebControls/Combos/UCComboPais.ascx" TagName="UCComboPais" TagPrefix="uc1" %>
<%@ Register Src="../WebControls/Combos/UCComboUnidadeFederativa.ascx" TagName="UCComboUnidadeFederativa"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="_updCidades" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Label ID="_lblMessage" runat="server" EnableViewState="False"></asp:Label>
            <fieldset>                
                <uc1:UCComboPais ID="UCComboPais1" runat="server" _EnableValidator="False" />
                <uc2:UCComboUnidadeFederativa ID="UCComboUnidadeFederativa1" runat="server" />
                <asp:Label ID="LabelCidade" runat="server" Text="Cidade" AssociatedControlID="_txtCidade"></asp:Label>
                <asp:TextBox ID="_txtCidade" runat="server" MaxLength="200" SkinID="text30C"></asp:TextBox>
                <div class="right">
                    <asp:Button ID="_btnPesquisar" runat="server" Text="Pesquisar" OnClick="_btnPesquisar_Click"
                        CausesValidation="False" />
                </div>
            </fieldset>
            <fieldset id="fdsResultados" runat="server">
                <legend>Resultados</legend>
                <asp:Button ID="_btnNovo" runat="server" Text="Nova cidade" 
                    CausesValidation="False" />
                <asp:GridView ID="_grvCidade" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    BorderStyle="None" DataKeyNames="cid_id,cid_nome" DataSourceID="odsCidade" EmptyDataText="A pesquisa não encontrou resultados."
                    OnRowEditing="_grvCidade_RowEditing">
                    <EmptyDataRowStyle Font-Bold="True" ForeColor="Red" />
                    <Columns>
                        <asp:BoundField DataField="pai_id" HeaderText="Código do País">
                            <HeaderStyle CssClass="hide" />
                            <ItemStyle CssClass="hide" />
                        </asp:BoundField>
                        <asp:BoundField DataField="unf_id" HeaderText="Código do Estado">
                            <HeaderStyle CssClass="hide" />
                            <ItemStyle CssClass="hide" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cid_id" HeaderText="Código da Cidade">
                            <HeaderStyle CssClass="hide" />
                            <ItemStyle CssClass="hide" />
                        </asp:BoundField>
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsCidade" runat="server" DataObjectTypeName="Autenticador.Entities.END_Cidade"
        SelectCountMethod="GetTotalRecords" SelectMethod="GetSelect" EnablePaging="True"
        MaximumRowsParameterName="pageSize" StartRowIndexParameterName="currentPage"
        TypeName="Autenticador.BLL.END_CidadeBO" OnSelecting="odsCidade_Selecting"
        DeleteMethod="Delete"></asp:ObjectDataSource>
</asp:Content>
