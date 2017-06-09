<%@ Page Title="" Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" EnableViewState="false"
    AutoEventWireup="true" CodeBehind="Gerenciamento.aspx.cs" Inherits="AutenticadorWebSite.AreaAdm.Site.Gerenciamento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="_lblMessage" runat="server" EnableViewState="False"></asp:Label>
    <fieldset>
        <legend>Sites</legend>
        <asp:GridView ID="gvSites" runat="server" AutoGenerateColumns="False" EnableViewState="False"
            DataKeyNames="ID" onrowdatabound="gvSites_RowDataBound" >
            <Columns>
                <asp:BoundField AccessibleHeaderText="ID" DataField="Id" HeaderText="ID" />
                <asp:BoundField AccessibleHeaderText="Name" DataField="Name" HeaderText="Name" />
                <asp:TemplateField AccessibleHeaderText="Applications" HeaderText="Applications">
                    <ItemTemplate>
                        <asp:GridView ID="gvApplicationPools" runat="server" AutoGenerateColumns="False"
                            EnableViewState="False" >
                            <Columns>
                                <asp:BoundField AccessibleHeaderText="ApplicationPoolName" 
                                    DataField="ApplicationPoolName"
                                    HeaderText="ApplicationPoolName" />
								 <asp:BoundField AccessibleHeaderText="Path" 
                                    DataField="Path"
                                    HeaderText="Path" />
                            </Columns>
                        </asp:GridView>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </fieldset>
</asp:Content>
