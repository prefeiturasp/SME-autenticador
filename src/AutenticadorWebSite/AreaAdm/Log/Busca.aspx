<%@ Page Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true" Inherits="AreaAdm_Log_Busca" Title="Untitled Page" Codebehind="Busca.aspx.cs" %>

<%@ Register Src="../../WebControls/Combos/UCComboSistema.ascx" TagName="UCComboSistemas"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            $('#divLog').dialog({
                bgiframe: true,
                autoOpen: false,
                modal: true,
                width: 550,
                height: 700
            });
        });
    </script>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Label ID="_lblMessage" runat="server" EnableViewState="False"></asp:Label>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="log" />
            <fieldset>
                <legend>Consulta de logs de sistema</legend>
                <uc1:UCComboSistemas ID="UCComboSistemas1" runat="server" />
                <asp:Label ID="_lbLogin" runat="server" Text="Login" AssociatedControlID="_txtLogin"></asp:Label>
                <asp:TextBox ID="_txtLogin" runat="server" SkinID="text30C" MaxLength="100"></asp:TextBox>
                <asp:Label ID="Label5" runat="server" Text="Tipo de log" AssociatedControlID="_ddlTipoLog"></asp:Label>
                <asp:DropDownList ID="_ddlTipoLog" runat="server" SkinID="text30C">
                </asp:DropDownList>
                <asp:Label ID="Label1" runat="server" Text="Data inicial *" AssociatedControlID="_txtDtInicio"></asp:Label>
                <asp:TextBox ID="_txtDtInicio" runat="server" SkinID="Data"></asp:TextBox>
                <asp:Label ID="Label3" runat="server" Text="(DD/MM/AAAA)"></asp:Label>
                <asp:RequiredFieldValidator ID="_rfvDtInicio" runat="server" ErrorMessage="Data inicial é obrigatório."
                    Text="*" ControlToValidate="_txtDtInicio" Display="Dynamic" ValidationGroup="log"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="cvDataInicio" runat="server" ControlToValidate="_txtDtInicio"
                    ValidationGroup="log" Display="Dynamic" ErrorMessage="Data de início inválida."
                    OnServerValidate="ValidarData_ServerValidate">*</asp:CustomValidator>
                <asp:Label ID="Label2" runat="server" Text="Data final *" AssociatedControlID="_txtDtFinal"></asp:Label>
                <asp:TextBox ID="_txtDtFinal" runat="server" SkinID="Data"></asp:TextBox>
                <asp:Label ID="Label4" runat="server" Text="(DD/MM/AAAA)"></asp:Label>
                <asp:RequiredFieldValidator ID="_rfvDtFinal" runat="server" ErrorMessage="Data final é obrigatório."
                    ControlToValidate="_txtDtFinal" Display="Dynamic" ValidationGroup="log">*</asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="_txtDtFinal"
                    ValidationGroup="log" Display="Dynamic" ErrorMessage="Data final inválida." OnServerValidate="ValidarData_ServerValidate">*</asp:CustomValidator>
                <asp:CustomValidator ID="cvDatas" runat="server" ErrorMessage="Data final deve ser maior ou igual a data inicial."
                    Display="Dynamic" ValidationGroup="log" Visible="false" OnServerValidate="ValidarDatas_ServerValidate">*</asp:CustomValidator>
                <div class="right">
                    <asp:Button ID="_btnPesquisa" runat="server" Text="Pesquisar" OnClick="_btnPesquisa_Click" ValidationGroup="log"/>
                </div>
            </fieldset>
            <fieldset id="fdsResultado" runat="server">
                <legend>Resultado</legend>
                <asp:GridView ID="_dgvLog" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    DataKeyNames="log_id" DataSourceID="odsLog" EmptyDataText="A pesquisa não encontrou resultados."
                    OnRowCommand="_dgvLog_RowCommand" OnRowDataBound="_dgvLog_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="log_id" HeaderText="log_id" InsertVisible="False" ReadOnly="True"
                            SortExpression="log_id" Visible="False" />
                        <asp:TemplateField HeaderText="Data/hora">
                            <ItemTemplate>
                                <asp:LinkButton ID="_btnAlterar" runat="server" CausesValidation="False" CommandName="Alterar"
                                    Text='<%# Bind("log_datahora") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle CssClass="center"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="log_IP" HeaderText="IP" />
                        <asp:BoundField DataField="sis_nome" HeaderText="Sistema" />
                        <asp:BoundField DataField="mod_nome" HeaderText="Módulo" />
                        <asp:BoundField DataField="log_acao" HeaderText="Ação" />
                        <asp:BoundField DataField="usu_login" HeaderText="Usuário login" />
                        <%--<asp:BoundField DataField="log_descricao" HeaderText="Descrição" />--%>
                    </Columns>
                </asp:GridView>
                <asp:ObjectDataSource ID="odsLog" runat="server" DataObjectTypeName="Autenticador.Entities.LOG_Sistema"
                    EnablePaging="True" MaximumRowsParameterName="pageSize" OnSelecting="odsLog_Selecting"
                    SelectCountMethod="GetTotalRecords" SelectMethod="GetSelect" StartRowIndexParameterName="currentPage"
                    TypeName="Autenticador.BLL.LOG_SistemaBO"></asp:ObjectDataSource>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divLog" title="Log do sistema" class="hide">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <fieldset>
                    <asp:Label ID="lblID" runat="server" Text="ID único" AssociatedControlID="txtID"></asp:Label>
                    <asp:TextBox ID="txtID" runat="server" ReadOnly="true" CssClass="text60C"></asp:TextBox>
                    <asp:Label ID="lblDataHora" runat="server" Text="Data e hora" AssociatedControlID="txtDataHora"></asp:Label>
                    <asp:TextBox ID="txtDataHora" runat="server" ReadOnly="true" CssClass="text60C"></asp:TextBox>
                    <asp:Label ID="lblEnderecoIP" runat="server" Text="Endereço IP" AssociatedControlID="txtEnderecoIP"></asp:Label>
                    <asp:TextBox ID="txtEnderecoIP" runat="server" ReadOnly="true" CssClass="text60C"></asp:TextBox>
                    <asp:Label ID="lblNomeMaquina" runat="server" Text="Nome da máquina" AssociatedControlID="txtNomeMaquina"></asp:Label>
                    <asp:TextBox ID="txtNomeMaquina" runat="server" ReadOnly="true" CssClass="text60C"></asp:TextBox>
                    <asp:Label ID="lblSistema" runat="server" Text="Sistema" AssociatedControlID="txtSistema"></asp:Label>
                    <asp:TextBox ID="txtSistema" runat="server" ReadOnly="true" CssClass="text60C"></asp:TextBox>
                    <asp:Label ID="lblModulo" runat="server" Text="Módulo" AssociatedControlID="txtModulo"></asp:Label>
                    <asp:TextBox ID="txtModulo" runat="server" ReadOnly="true" CssClass="text60C"></asp:TextBox>
                    <asp:Label ID="lblUsuarioID" runat="server" Text="Usuário - ID" AssociatedControlID="txtUsuarioID"></asp:Label>
                    <asp:TextBox ID="txtUsuarioID" runat="server" ReadOnly="true" CssClass="text60C"></asp:TextBox>
                    <asp:Label ID="lblUsuarioLogin" runat="server" Text="Usuário - login" AssociatedControlID="txtUsuarioLogin"></asp:Label>
                    <asp:TextBox ID="txtUsuarioLogin" runat="server" ReadOnly="true" CssClass="text60C"></asp:TextBox>
                    <asp:Label ID="lblGrupoID" runat="server" Text="Grupo - ID" AssociatedControlID="txtGrupoID"></asp:Label>
                    <asp:TextBox ID="txtGrupoID" runat="server" ReadOnly="true" CssClass="text60C"></asp:TextBox>
                    <asp:Label ID="lblGrupoNome" runat="server" Text="Grupo - nome" AssociatedControlID="txtGrupoNome"></asp:Label>
                    <asp:TextBox ID="txtGrupoNome" runat="server" ReadOnly="true" CssClass="text60C"></asp:TextBox>
                    <asp:Label ID="lblGrupoUA" runat="server" Text="Grupo UA" AssociatedControlID="txtGrupoUA"></asp:Label>
                    <asp:TextBox ID="txtGrupoUA" runat="server" ReadOnly="true" CssClass="text60C" TextMode="MultiLine"></asp:TextBox>
                    <asp:Label ID="lblAcao" runat="server" Text="Ação" AssociatedControlID="txtAcao"></asp:Label>
                    <asp:TextBox ID="txtAcao" runat="server" ReadOnly="true" CssClass="text60C"></asp:TextBox>
                    <asp:Label ID="lblDescricao" runat="server" Text="Descrição" AssociatedControlID="txtDecricao"></asp:Label>
                    <asp:TextBox ID="txtDecricao" runat="server" ReadOnly="true" CssClass="text60C" TextMode="MultiLine"></asp:TextBox>
                </fieldset>
                <fieldset>
                    <div align="right">
                        <asp:Button ID="btnFechar" runat="server" Text="Fechar" OnClick="btnFechar_Click" />
                    </div>
                </fieldset>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
