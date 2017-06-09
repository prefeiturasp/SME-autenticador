<%@ Control Language="C#" AutoEventWireup="true" Inherits="WebControls_Sistemas_UCSistemas" CodeBehind="UCSistemas.ascx.cs" %>
<!-- Sistemas -->
<span runat="server" id="menuSistemas" class="menuSistemas">
    <asp:HyperLink ID="hplSistemas" runat="server" CssClass="hplSistemas">Aplicativos <span>&#9660;</span></asp:HyperLink>
    <span class="spUl">
        <asp:Repeater ID="rptSistemas" runat="server" DataSourceID="odsSistemas" OnItemDataBound="rptSistemas_ItemDataBound">
            <ItemTemplate>
                <span class="spLi">
                    <asp:HyperLink ID="hplSistema" runat="server" NavigateUrl='<%# Eval("sis_caminho") %>'
                        ToolTip='<%# Eval("sis_nome") %>'>
                        <asp:Image ID="imgSistema" runat="server" />
                        <br />
                        <asp:Literal ID="ltrSistema" runat="server" Text='<%# Eval("sis_nome") %>'></asp:Literal>
                    </asp:HyperLink>
                </span>
            </ItemTemplate>
        </asp:Repeater>
        <span class="fecharSistemas">
            <input type="button" class="btn" value="Fechar" />
        </span>
    </span>
    <span class="clear"></span>
</span>
<asp:ObjectDataSource ID="odsSistemas" runat="server" DataObjectTypeName="Autenticador.Entities.SYS_Sistema"
    SelectMethod="GetSelectBy_usu_id" TypeName="Autenticador.BLL.SYS_SistemaBO"
    OnSelecting="odsSistemas_Selecting"></asp:ObjectDataSource>
