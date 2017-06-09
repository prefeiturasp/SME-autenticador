<%@ Page Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true" Inherits="AreaAdm_ManutencaoUnidadeFederativa_Busca" Codebehind="Busca.aspx.cs" %>

<%@ Register Src="~/WebControls/Combos/UCComboPais.ascx" TagName="UCComboPais" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="_updUnidadeFederativa" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Label ID="_lblMessage" runat="server" EnableViewState="False"></asp:Label>           
            <fieldset>
                <legend>Consulta de unidades federativas</legend>
                <div id="_divPesquisa" runat="server">
                    <uc1:UCComboPais ID="UCComboPais" runat="server" />                    
                    <asp:Label ID="lblUnidadeFederativa" runat="server" AssociatedControlID="txtUnidadeFederativa" Text="Unidade federativa"></asp:Label>
                    <asp:TextBox ID="txtUnidadeFederativa" runat="server" MaxLength="200" SkinID="text30C"></asp:TextBox>
                </div>
                <div class="right">
                    <asp:Button ID="_btnPesquisar" runat="server" Text="Pesquisar" OnClick="_btnPesquisar_Click"
                        CausesValidation="False" />
                    <asp:Button ID="btnIncluir" runat="server" Text="Incluir unidade federativa" OnClick="btnIncluir_Click"
                        CausesValidation="False" />                    
                </div>
            </fieldset>
            <fieldset id="fdsResultados" runat="server">
                <legend>Resultados</legend>
                <asp:GridView ID="_grvUnidadeFederativa" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    OnRowCommand="_grvUnidadeFederativa_RowCommand" OnRowDataBound="_grvUnidadeFederativa_RowDataBound"
                    BorderStyle="None" DataKeyNames="pai_id,unf_id" DataSourceID="odsUnidadeFederativa"
                    EmptyDataText="A pesquisa não encontrou resultados.">
                    <EmptyDataRowStyle Font-Bold="True" ForeColor="Red" />
                    <Columns>
                        <asp:TemplateField HeaderText="UnidadeFederativa">
                            <ItemTemplate>
                                <asp:Label ID="lbUnidadeFederativaNome" runat="server" Text='<%#Bind("unf_nome") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sigla">
                            <ItemTemplate>
                                <asp:Label ID="lbUnidadeFederativaSigla" runat="server" Text='<%#Bind("unf_sigla") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>   
                        <asp:TemplateField HeaderText="Alterar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="_btnAlterar" SkinID="btEditar" CommandName="Edit" runat="server"
                                    PostBackUrl="~/AreaAdm/ManutencaoUnidadeFederativa/Cadastro.aspx" 
                                    CausesValidation="False" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Excluir" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="_btnExcluir" SkinID="btExcluir" CommandName="DeletarUnidadeFederativa"
                                    runat="server" CausesValidation="False" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>                       
                    </Columns>
                    <EditRowStyle Font-Bold="True" ForeColor="Red" />
                </asp:GridView>
            </fieldset>
            <asp:ObjectDataSource ID="odsUnidadeFederativa" runat="server" DataObjectTypeName="Autenticador.Entities.END_UnidadeFederativa"
                SelectCountMethod="GetTotalRecords" SelectMethod="GetSelect" EnablePaging="True"
                MaximumRowsParameterName="pageSize" StartRowIndexParameterName="currentPage"
                TypeName="Autenticador.BLL.END_UnidadeFederativaBO" OnSelecting="odsUnidadeFederativa_Selecting"
                DeleteMethod="Delete"></asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
