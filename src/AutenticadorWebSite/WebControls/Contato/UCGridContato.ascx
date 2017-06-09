<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="WebControls_Contato_UCGridContato" CodeBehind="UCGridContato.ascx.cs" %>

<asp:UpdatePanel ID="updGridContatos" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:UpdateProgress ID="upgConteudo" runat="server"
            DisplayAfter="10"
            AssociatedUpdatePanelID="updGridContatos">
            <ProgressTemplate>
                <div class="loader">
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>

        <asp:Label ID="_lblMessage" runat="server"></asp:Label>
        <asp:ObjectDataSource ID="odsTipoMeioContato" runat="server"
            DataObjectTypeName="Autenticador.Entities.SYS_TipoMeioContato" MaximumRowsParameterName="pageSize"
            SelectCountMethod="GetTotalRecords" SelectMethod="GetSelect"
            StartRowIndexParameterName="currentPage"
            TypeName="Autenticador.BLL.SYS_TipoMeioContatoBO" DeleteMethod="Delete"
            OldValuesParameterFormatString="original_{0}" UpdateMethod="Save">
            <DeleteParameters>
                <asp:Parameter Name="entity" Type="Object" />
                <asp:Parameter Name="banco" Type="Object" />
            </DeleteParameters>
            <SelectParameters>
                <asp:Parameter DbType="Guid"
                    DefaultValue="00000000-0000-0000-0000-000000000000" Name="tmc_id" />
                <asp:Parameter Name="tmc_nome" Type="String" />
                <asp:Parameter Name="tmc_situacao" Type="Byte" />
                <asp:Parameter DefaultValue="false" Name="paginado" Type="Boolean" />
                <asp:Parameter DefaultValue="1" Name="currentPage" Type="Int32" />
                <asp:Parameter DefaultValue="1" Name="pageSize" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </ContentTemplate>
</asp:UpdatePanel>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:GridView ID="_grvContato" runat="server" AutoGenerateColumns="False"
            DataKeyNames="id" EmptyDataText="Não existem contatos cadastrados."
            OnRowDataBound="_grvContato_RowDataBound">
            <EmptyDataRowStyle Font-Bold="True" ForeColor="Red" />
            <Columns>
                <asp:TemplateField HeaderText="Tipo">
                    <ItemTemplate>
                        <asp:DropDownList ID="_ddlTipoMeioContato" runat="server"
                            AppendDataBoundItems="True" DataSourceID="odsTipoMeioContato"
                            DataTextField="tmc_nome" DataValueField="tmc_id" SkinID="text30C">
                            <asp:ListItem Value="-1">-- Selecione o tipo --</asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Contato">
                    <EditItemTemplate>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:TextBox ID="tbContato" runat="server" SkinID="text60C"
                            Text='<%# Bind("contato") %>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="btnAdicionar" runat="server" CausesValidation="False"
                            OnClick="btnAdicionar_Click" SkinID="btNovo" ToolTip="Adicionar"
                            Visible="False" />
                    </ItemTemplate>
                    <HeaderStyle CssClass="center" Width="70px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </ContentTemplate>
</asp:UpdatePanel>

