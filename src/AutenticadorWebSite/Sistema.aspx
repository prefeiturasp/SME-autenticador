<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSistema.master" AutoEventWireup="true" Inherits="Sistema" CodeBehind="Sistema.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="_lblMessage" runat="server" EnableViewState="False"></asp:Label>
    <fieldset runat="server" id="fdsSistemas">
        <asp:DataList ID="_dltSistemas" runat="server" DataSourceID="odsSistema" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="listaSistemas" ShowFooter="False" ShowHeader="False" RepeatColumns="4" OnItemDataBound="_dltSistemas_ItemDataBound">
            <ItemTemplate>
                <asp:HyperLink ID="hplSistema" runat="server" NavigateUrl='<%# Eval("sis_caminho") %>' ToolTip='<%# Eval("sis_nome") %>'>
                    <asp:Image ID="imgSistema" runat="server" />
                    <asp:Label ID="ltrSistema" runat="server" Text='<%# Eval("sis_nome") %>'></asp:Label>
                    <span class="sistemaHover"></span>
                </asp:HyperLink>
            </ItemTemplate>
        </asp:DataList>
    </fieldset>
    <section runat="server" id="sctnSistemas" class="lista-sistemas">
        <div class="row">
                <div class="small-11 small-centered large-12 columns">
                    <asp:Repeater ID="rptSistemas" DataSourceID="odsSistema" runat="server" OnItemDataBound="rptSistemas_ItemDataBound">
                        <HeaderTemplate><ul class="small-block-grid-2 medium-block-grid-3 large-block-grid-4"></HeaderTemplate>
                        <ItemTemplate>
                            <li>
                                <asp:HyperLink ID="hplSistema" runat="server" NavigateUrl='<%# Eval("sis_caminho") %>' ToolTip='<%# Eval("sis_nome") %>' CssClass="card-sistema">
                                    <asp:Image ID="imgSistema" runat="server" />
                                </asp:HyperLink>
                            </li>
                        </ItemTemplate>
                        <FooterTemplate></ul></FooterTemplate>
                    </asp:Repeater>
                </div>
        </div>
    </section>

    <asp:ObjectDataSource ID="odsSistema" runat="server" DataObjectTypeName="Autenticador.Entities.SYS_Sistema" SelectMethod="GetSelectBy_usu_id" TypeName="Autenticador.BLL.SYS_SistemaBO" OnSelecting="odsSistema_Selecting"></asp:ObjectDataSource>
</asp:Content>
