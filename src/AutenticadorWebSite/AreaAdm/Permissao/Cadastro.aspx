<%@ Page Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true"
    Inherits="AreaAdm_Permissao_Cadastro" Title="Untitled Page" CodeBehind="Cadastro.aspx.cs" %>

<%@ PreviousPageType VirtualPath="~/AreaAdm/Grupo/Busca.aspx" %>
<%@ Register Src="../../WebControls/Permissoes/UCPermissoes.ascx" TagName="UCPermissoes"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .selecionarTodos
        {
            margin-top: 1px;
            float: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="_lblMessage" runat="server" EnableViewState="False"></asp:Label>
    <fieldset>
        <div>
            <asp:Label ID="Label3" runat="server" Text="Sistema:" SkinID="SubTitulo"></asp:Label>
            <asp:Label ID="_lblSistema" runat="server"></asp:Label>
            <br />
            <asp:Label ID="Label1" runat="server" Text="Grupo:" SkinID="SubTitulo"></asp:Label>
            <asp:Label ID="_lblGrupo" runat="server"></asp:Label>
            <br />
            <asp:Label ID="Label2" runat="server" Text="Visão:" SkinID="SubTitulo"></asp:Label>
            <asp:Label ID="_lbVisao" runat="server"></asp:Label>
        </div>
    </fieldset>
    <div id="divModulos">
        <asp:Repeater ID="_rptModulos" runat="server" DataSourceID="odsModulos">
            <ItemTemplate>
                <h3>
                    <a href="#">
                        <%# Eval("mod_nome")%></a>
                </h3>
                <div>
                    <uc1:UCPermissoes ID="UCPermissoes1" runat="server" ModuloPaiId='<%# Eval("mod_id") %>'
                        GrupoId='<%# _VS_gru_id %>' />
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <asp:ObjectDataSource ID="odsModulos" runat="server" DataObjectTypeName="Autenticador.Entities.SYS_Modulo"
        SelectMethod="GetSelectModulosPai" TypeName="Autenticador.BLL.SYS_GrupoBO">
        <SelectParameters>
            <asp:ControlParameter ControlID="__Page" DbType="Guid" Name="gru_id" PropertyName="_VS_gru_id" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <fieldset>
        <div class="right">
            <asp:Button ID="_btnSalvar" runat="server" Text="Salvar" CausesValidation="False"
                Width="100px" OnClick="_btnSalvar_Click" />
            <asp:Button ID="_btnCancelar" runat="server" Text="Cancelar" CausesValidation="False"
                Width="100px" OnClick="_btnCancelar_Click" />
        </div>
    </fieldset>
</asp:Content>