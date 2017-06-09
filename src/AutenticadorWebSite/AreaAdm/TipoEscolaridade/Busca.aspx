<%@ Page Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true" Inherits="AreaAdm_TipoEscolaridade_Busca" Codebehind="Busca.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="_updBusca" runat="server">
        <ContentTemplate>
            <asp:Label ID="_lblMessage" runat="server" EnableViewState="False"></asp:Label>
            <fieldset>
                <legend>Listagem de tipos de escolaridade</legend>
                <div>
                    <asp:Button ID="_btnNovo" runat="server" Text="Novo tipo de escolaridade" OnClick="_btnNovo_Click" /></div>
                <div id="_divConsulta" runat="server">
                    <asp:GridView ID="_grvTipoEscolaridade" runat="server" AutoGenerateColumns="False"
                        DataKeyNames="tes_id,tes_ordem" EmptyDataText="Não existem tipos de escolaridade cadastrados."
                        DataSourceID="odsTipoEscolaridade" AllowPaging="True" OnRowCommand="_grvTipoEscolaridade_RowCommand"
                        OnRowDataBound="_grvTipoEscolaridade_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Tipo de escolaridade">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("tes_nome") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="_btnAlterar" runat="server" CommandName="Edit" PostBackUrl="~/AreaAdm/TipoEscolaridade/Cadastro.aspx"
                                        Text='<%# Bind("tes_nome") %>'></asp:LinkButton>
                                    <asp:Label ID="_lblAlterar" runat="server" Text='<%# Bind("tes_nome") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="tes_situacao" HeaderText="Bloqueado">
                                <HeaderStyle CssClass="center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Ordem">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:ImageButton ID="_btnSubir" runat="server" CausesValidation="false" CommandName="Subir"
                                        Width="16" Height="16" />
                                    <asp:ImageButton ID="_btnDescer" runat="server" CausesValidation="false" CommandName="Descer"
                                        Width="16" Height="16" />
                                </ItemTemplate>
                                <HeaderStyle CssClass="center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Excluir">
                                <ItemTemplate>
                                    <asp:ImageButton ID="_btnExcluir" runat="server" CommandName="Deletar" SkinID="btExcluir" />
                                </ItemTemplate>
                                <HeaderStyle CssClass="center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsTipoEscolaridade" runat="server" DataObjectTypeName="Autenticador.Entities.PES_TipoEscolaridade"
        DeleteMethod="Delete" EnablePaging="True" StartRowIndexParameterName="currentPage"
        MaximumRowsParameterName="pageSize" SelectCountMethod="GetTotalRecords" SelectMethod="GetSelect"
        TypeName="Autenticador.BLL.PES_TipoEscolaridadeBO" OnSelecting="odsTipoEscolaridade_Selecting">
        <SelectParameters>
            <asp:Parameter Name="tes_id" DbType="Int32" Size="4" />
            <asp:Parameter Name="tes_nome" DbType="AnsiString" Size="100" />
            <asp:Parameter Name="tes_situacao" DbType="Byte" Size="1" />
            <asp:Parameter Name="paginado" DbType="Boolean" DefaultValue="true" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
