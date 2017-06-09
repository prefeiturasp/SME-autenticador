<%@ Control Language="C#" AutoEventWireup="true" Inherits="WebControls_Permissoes_UCPermissoes"
    CodeBehind="UCPermissoes.ascx.cs" %>
<asp:GridView ID="grvPermissoes" runat="server" AutoGenerateColumns="False" DataKeyNames="gru_id,sis_id,mod_id"
    OnRowDataBound="grvPermissoes_RowDataBound">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:LinkButton ID="lkbExpandir" runat="server" CssClass="ui-icon ui-icon-circle-triangle-e"
                    Visible="false"></asp:LinkButton>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" Width="30px" />
        </asp:TemplateField>
        <asp:BoundField DataField="mod_nome" HeaderText="Módulo" />
        <asp:TemplateField>
            <HeaderTemplate>
                <input type="checkbox" id="Consulta" class="selecionarTodos" />&nbsp; Consulta
            </HeaderTemplate>
            <HeaderStyle Width="100px" />
            <ItemTemplate>
                <asp:CheckBox ID="chkConsulta" runat="server" Checked='<%# Bind("grp_consultar") %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <HeaderTemplate>
                <input type="checkbox" id="Inserir" class="selecionarTodos" />&nbsp; Inclusão
            </HeaderTemplate>
            <HeaderStyle Width="100px" />
            <ItemTemplate>
                <asp:CheckBox ID="chkInserir" runat="server" Checked='<%# Bind("grp_inserir") %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <HeaderTemplate>
                <input type="checkbox" id="Editar" class="selecionarTodos" />&nbsp; Alteração
            </HeaderTemplate>
            <HeaderStyle Width="100px" />
            <ItemTemplate>
                <asp:CheckBox ID="chkEditar" runat="server" Checked='<%# Bind("grp_alterar") %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <HeaderTemplate>
                <input type="checkbox" id="Excluir" class="selecionarTodos" />&nbsp; Exclusão
            </HeaderTemplate>
            <HeaderStyle Width="100px" />
            <ItemTemplate>
                <asp:CheckBox ID="chkExcluir" runat="server" Checked='<%# Bind("grp_excluir") %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <tr>
                    <td align="left" colspan="100%">
                        <asp:GridView ID="grvPermissoesChild" runat="server" AutoGenerateColumns="False"
                            DataKeyNames="gru_id,sis_id,mod_id" OnRowDataBound="grvPermissoes_RowDataBound"
                            BorderStyle="None" Style="display: none" SkinID="child">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lkbExpandir" runat="server" CssClass="ui-icon ui-icon-circle-triangle-e"
                                            Visible="false"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="mod_nome" HeaderText="Modulo" />
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <input type="checkbox" id="Consulta" class="selecionarTodos" />&nbsp; Consulta
                                    </HeaderTemplate>
                                    <HeaderStyle Width="100px" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkConsulta" runat="server" Checked='<%# Bind("grp_consultar") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <input type="checkbox" id="Inserir" class="selecionarTodos" />&nbsp; Inclusão
                                    </HeaderTemplate>
                                    <HeaderStyle Width="100px" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkInserir" runat="server" Checked='<%# Bind("grp_inserir") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <input type="checkbox" id="Editar" class="selecionarTodos" />&nbsp; Alteração
                                    </HeaderTemplate>
                                    <HeaderStyle Width="100px" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkEditar" runat="server" Checked='<%# Bind("grp_alterar") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <input type="checkbox" id="Excluir" class="selecionarTodos" />&nbsp; Exclusão
                                    </HeaderTemplate>
                                    <HeaderStyle Width="100px" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkExcluir" runat="server" Checked='<%# Bind("grp_excluir") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <tr>
                                            <td align="left" colspan="100%">
                                                <asp:GridView ID="grvPermissoesChild" runat="server" AutoGenerateColumns="False"
                                                    DataKeyNames="gru_id,sis_id,mod_id" BorderStyle="None" Style="display: none;"
                                                    SkinID="child">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="mod_nome" HeaderText="Modulo" />
                                                        <asp:TemplateField HeaderText="Consulta">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkConsulta" runat="server" Checked='<%# Bind("grp_consultar") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Inclusão">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkInserir" runat="server" Checked='<%# Bind("grp_inserir") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Alteração">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkEditar" runat="server" Checked='<%# Bind("grp_alterar") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Exclusão">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkExcluir" runat="server" Checked='<%# Bind("grp_excluir") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="hide" />
                                    <ItemStyle CssClass="hide" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </ItemTemplate>
            <HeaderStyle CssClass="hide" />
            <ItemStyle CssClass="hide" />
        </asp:TemplateField>
    </Columns>
</asp:GridView>
