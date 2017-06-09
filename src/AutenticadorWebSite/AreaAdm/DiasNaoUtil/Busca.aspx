<%@ Page Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true" Inherits="AreaAdm_DiasNaoUtil_Busca" Title="Untitled Page" Codebehind="Busca.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="_updBuscaDiaNaoUtil" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Label ID="_lblMessage" runat="server" EnableViewState="False"></asp:Label>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="data" />
            <fieldset>
                <legend>Consulta de dias não úteis</legend>
                <div id="divPesquisa" runat="server">
                    <asp:Label ID="_lblNome" runat="server" Text="Nome" AssociatedControlID="_txtNome"></asp:Label>
                    <asp:TextBox ID="_txtNome" runat="server" SkinID="text60C"></asp:TextBox>
                    <asp:Label ID="_lblRecorrencia" runat="server" Text="Recorrência anual" AssociatedControlID="_ddlRecorencia"></asp:Label>
                    <asp:DropDownList ID="_ddlRecorencia" runat="server" AutoPostBack="True" 
                        SkinID="text30C" onselectedindexchanged="_ddlRecorencia_SelectedIndexChanged">
                        <asp:ListItem Value="2">-- Selecione uma opção --</asp:ListItem>
                        <asp:ListItem Value="0">Sim</asp:ListItem>
                        <asp:ListItem Value="1">Não</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="_lblData" runat="server" Text="Data" AssociatedControlID="_txtData"></asp:Label>
                    <asp:TextBox ID="_txtData" runat="server" MaxLength="10" SkinID="Data"></asp:TextBox>
                    <asp:Label ID="_lblFormatoData" runat="server" Text="(DD/MM/AAAA)"></asp:Label>
                    <asp:CustomValidator ID="cvData" runat="server" ControlToValidate="_txtData" ValidationGroup="data"
                        Display="Dynamic" ErrorMessage="Data inválida." OnServerValidate="ValidarData_ServerValidate">* </asp:CustomValidator>
                    <asp:TextBox ID="_txtDataDia" runat="server" MaxLength="2" Width="29px" Visible="False"
                        CssClass="numeric" SkinID="Numerico"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="_revDataDia" runat="server" ControlToValidate="_txtDataDia"
                        Display="Dynamic" ErrorMessage="Dia inválido." ValidationExpression="^([0-9]|[0,1,2][0-9]|3[0,1])$"
                        ValidationGroup="data" Visible="False">*</asp:RegularExpressionValidator>
                    <asp:Label ID="_lblBarra" runat="server" Text="/" Visible="False"></asp:Label>
                    <asp:TextBox ID="_txtDataMes" runat="server" MaxLength="2" Width="29px" Visible="False"
                        CssClass="numeric" SkinID="Numerico"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="_revDataMes" runat="server" ControlToValidate="_txtDataMes"
                        Display="Dynamic" ErrorMessage="Mês inválido." ValidationExpression="^([0-9]|[0,1][0-2]|0[0-9])$"
                        ValidationGroup="data" Visible="False">*</asp:RegularExpressionValidator>
                    <asp:Label ID="_lblFormatoData2" runat="server" Text="(DD/MM)" Visible="False"></asp:Label>                    
                    <asp:Label ID="_lblAbrangencia" runat="server" Text="Abrangência" AssociatedControlID="_ddlAbrangencia"></asp:Label>
                    <asp:DropDownList ID="_ddlAbrangencia" runat="server" AutoPostBack="false" SkinID="text30C">
                        <asp:ListItem Selected="True" Value="0">-- Selecione uma abrangência --</asp:ListItem>
                        <asp:ListItem Value="1">Federal</asp:ListItem>
                        <asp:ListItem Value="2">Estadual</asp:ListItem>
                        <asp:ListItem Value="3">Municipal</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="right">
                    <asp:Button ID="_btnPesquisar" runat="server" Text="Pesquisar" OnClick="_btnPesquisa_Click"
                        ValidationGroup="data" />
                    <asp:Button ID="_btnNovo" runat="server" CausesValidation="false" Text="Novo dia não útil"
                        OnClick="_btnNovo_Click" /></div>
            </fieldset>
            <fieldset id="fdsResultados" runat="server">
                <legend>Resultados</legend>
                <asp:GridView ID="_dgvDiaNaoUtil" runat="server" AutoGenerateColumns="False" DataKeyNames="dnu_id"
                    DataSourceID="_odsDiaNaoUtil" OnRowDataBound="_dgvDiaNaoUtil_RowDataBound" EmptyDataText="A pesquisa não encontrou resultados."
                    OnRowCommand="_dgvDiaNaoUtil_RowCommand" AllowPaging="True">
                    <Columns>
                        <asp:BoundField DataField="dnu_id" HeaderText="dnu_id" InsertVisible="False" ReadOnly="True"
                            SortExpression="dnu_id" Visible="False" />
                        <asp:TemplateField HeaderText="Nome">
                            <ItemTemplate>
                                <asp:LinkButton ID="_btnAlterar" runat="server" CommandName="Edit" Text='<%# Bind("dnu_nome") %>'
                                    PostBackUrl="~/AreaAdm/DiasNaoUtil/Cadastro.aspx"></asp:LinkButton>
                                <asp:Label ID="_lblAlterar" runat="server" Text='<%# Bind("dnu_nome") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="dnu_abrangencia" HeaderText="Abrangência" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" SortExpression="dnu_abrangencia">
                            <HeaderStyle CssClass="center"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="dnu_data" HeaderText="Data" HeaderStyle-CssClass="center"
                            ItemStyle-HorizontalAlign="Center" SortExpression="dnu_data">
                            <HeaderStyle CssClass="center"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="dnu_vigencia" HeaderText="Vigência" HeaderStyle-CssClass="center"
                            ItemStyle-HorizontalAlign="Center" SortExpression="dnu_vigencia">
                            <HeaderStyle CssClass="center"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="cid_id" HeaderText="cid_id" SortExpression="cid_id" Visible="False"
                            InsertVisible="false" />
                        <asp:BoundField DataField="unf_id" HeaderText="unf_id" SortExpression="unf_id" Visible="False"
                            InsertVisible="false" />
                        <asp:TemplateField HeaderText="Excluir" HeaderStyle-CssClass="center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="_btnExcluir" SkinID="btExcluir" CommandArgument='<% # Bind("dnu_vigenciaInicio") %>'
                                    CommandName="Deletar" runat="server" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="_odsDiaNaoUtil" runat="server" DataObjectTypeName="Autenticador.Entities.SYS_DiaNaoUtil"
        DeleteMethod="Delete" SelectMethod="GetSelect" TypeName="Autenticador.BLL.SYS_DiaNaoUtilBO"
        OnSelecting="_odsDiaNaoUtil_Selecting" EnablePaging="true" MaximumRowsParameterName="pageSize"
        SelectCountMethod="GetTotalRecords" StartRowIndexParameterName="currentPage">
    </asp:ObjectDataSource>
</asp:Content>
