<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="AreaAdm_Configuracao_Relatorio_Busca" Codebehind="Busca.aspx.cs" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMasterLevel0" runat="Server">
    <asp:Label ID="lblObsoletoMsg" runat="server"></asp:Label>
    <asp:Label ID="_lblMessage" runat="server" EnableViewState="False"></asp:Label>
    <fieldset>
        <legend>Consulta de servidor de relatórios</legend>
        <asp:Label ID="Label2" runat="server" Text="Sistema" EnableViewState="False" 
            AssociatedControlID="_ddlSistema"></asp:Label>
        <asp:DropDownList ID="_ddlSistema" runat="server" DataSourceID="odsSistema" 
            DataTextField="sis_nome" DataValueField="sis_id" 
            ondatabound="_ddlSistema_DataBound">
        </asp:DropDownList>
       <asp:ObjectDataSource ID="odsSistema" runat="server" DataObjectTypeName="Autenticador.Entities.SYS_Sistema"
           SelectMethod="GetSelectBy_usu_id" 
            TypeName="Autenticador.BLL.SYS_SistemaBO" 
            OldValuesParameterFormatString="original_{0}">
           <SelectParameters>
               <asp:ControlParameter ControlID="__Page" DbType="Guid" 
                   Name="usu_id" PropertyName="__SessionWEB.__UsuarioWEB.Usuario.usu_id" />
           </SelectParameters>
        </asp:ObjectDataSource>
        <asp:Label ID="Label1" runat="server" Text="Nome do servidor de relatório:" EnableViewState="False"
            AssociatedControlID="_txtNome"></asp:Label>
        <asp:TextBox ID="_txtNome" runat="server" SkinID="text60C" MaxLength="100"></asp:TextBox>
        <div class="right">
            <asp:Button ID="_btnPesquisa" runat="server" Text="Pesquisar" 
                onclick="_btnPesquisa_Click" />
            <asp:Button ID="_btnNovo" runat="server" Text="Novo servidor de relatórios" 
                onclick="_btnNovo_Click" />
        </div>
    </fieldset>
    <fieldset id="fdsResultados" runat="server">
        <legend>Resultado</legend>
        <asp:GridView ID="_dgvServidor" runat="server" AllowPaging="True" 
            AutoGenerateColumns="False" DataKeyNames="ent_id,sis_id,srr_id" 
            DataSourceID="odsServidor" 
            EmptyDataText="A pesquisa não encontrou resultados." 
            onrowdatabound="_dgvServidor_RowDataBound"
            OnRowCommand="_dgvServidor_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="Nome do servidor de relatório">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("srr_nome") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="_lkbSelect" runat="server" 
                            PostBackUrl="~/AreaAdm/Configuracao/Relatorio/Cadastro.aspx" 
                            Text='<%# Bind("srr_nome") %>' CommandName="Edit"></asp:LinkButton>
                        <asp:Label ID="_lblNomeServidor" runat="server" EnableViewState="False" 
                            Text='<%# Bind("srr_nome") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="sis_nome" HeaderText="Sistema" />
                <asp:BoundField DataField="srr_situacao" HeaderText="Situação" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="_btnExcluir" runat="server" CausesValidation="False" 
                            CommandName="Delete" SkinID="btExcluir" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="odsServidor" runat="server"
            DeleteMethod="Delete" OldValuesParameterFormatString="" SelectMethod="BuscaServidorRelatorio"
            TypeName="Autenticador.BLL.CFG_ServidorRelatorioBO" EnablePaging="True"
            MaximumRowsParameterName="pageSize" OnSelected="odsServidor_Selected" OnSelecting="odsServidor_Selecting"
            SelectCountMethod="GetTotalRecords" 
            StartRowIndexParameterName="currentPage" 
            DataObjectTypeName="Autenticador.Entities.CFG_ServidorRelatorio">
            <DeleteParameters>
                <asp:Parameter Name="entity" Type="Object" />
                <asp:Parameter Name="banco" Type="Object" />
            </DeleteParameters>
            <SelectParameters>
                <asp:ControlParameter ControlID="__Page" DbType="Guid" Name="idEntidade" PropertyName="__SessionWEB.__UsuarioWEB.Usuario.ent_id" />
                <asp:ControlParameter ControlID="_ddlSistema" Name="idSistema" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="_txtNome" Name="nomeServidorRelatorio" PropertyName="Text"
                    Type="String" />
                <asp:Parameter Name="currentPage" Type="Int32" />
                <asp:Parameter Name="pageSize" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </fieldset>
</asp:Content>
