<%@ Page Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true"
    Inherits="AreaAdm_Parametros_Cadastro" Title="Untitled Page" CodeBehind="Cadastro.aspx.cs" %>

<%@ Register Src="../../WebControls/Combos/UCComboSistemaGrupo.ascx" TagName="UCComboSistemaGrupo"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset>
        <legend>Listagem de parâmetros</legend>
        <asp:UpdatePanel ID="_uppGridParametro" runat="server">
            <Triggers>
                <asp:PostBackTrigger ControlID="_dgvParametro1" />
            </Triggers>
            <ContentTemplate>
                <asp:GridView ID="_dgvParametro1" runat="server" AutoGenerateColumns="False" DataKeyNames="par_id,par_chave,par_obrigatorio,par_descricao"
                    OnRowEditing="_dgvParametro1_RowEditing" OnRowDataBound="_dgvParametro1_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="par_id" HeaderText="par_id" InsertVisible="false" ReadOnly="True"
                            SortExpression="par_id" Visible="false" />
                        <asp:BoundField DataField="par_chave" HeaderText="par_chave" InsertVisible="false"
                            ReadOnly="True" SortExpression="par_chave" Visible="false" />
                        <asp:BoundField DataField="par_obrigatorio" HeaderText="par_obrigatorio" InsertVisible="false"
                            ReadOnly="True" SortExpression="par_obrigatorio" Visible="false" />
                        <asp:BoundField DataField="par_descricao" HeaderText="Parâmetro" SortExpression="par_descricao" />
                        <asp:TemplateField HeaderText="Valor" SortExpression="par_valor_nome">
                            <ItemTemplate>
                                <asp:Label ID="lblValor" runat="server" Text='<%#Bind("par_valor_nome") %>'></asp:Label>
                                <asp:Image ID="img" runat="server" Visible="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="par_vigencia" ItemStyle-Wrap="false" HeaderText="Vigência"
                            SortExpression="par_vigencia" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <HeaderStyle CssClass="center" />
                            <ItemStyle HorizontalAlign="Center" Wrap="False" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Alterar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="70px">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="_btnAlterar" SkinID="btEditar" CausesValidation="false"
                                    CommandName="Edit" />
                            </ItemTemplate>
                            <HeaderStyle Width="70px" CssClass="center"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </fieldset>
    <fieldset>
        <legend>Cadastro de grupo padrão</legend>
        <asp:UpdatePanel ID="_uppGridGrupoPadrao" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                <asp:Button ID="_btnNovoGrupoPadrao" runat="server" Text="Novo grupo padrão" OnClick="_btnNovoGrupoPadrao_Click" />
                <asp:GridView ID="_dgvGrupoPadrao" runat="server" DataSourceID="_odsParametroGrupoPerfil"
                    AutoGenerateColumns="False" AllowPaging="True" DataKeyNames="pgs_id" EmptyDataText="Não existem grupos padrão cadastrados."
                    OnRowCommand="_dgvGrupoPadrao_RowCommand" OnRowDataBound="_dgvGrupoPadrao_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="pgs_id" HeaderText="pgs_id" SortExpression="pgs_id">
                            <HeaderStyle CssClass="hide" />
                            <ItemStyle CssClass="hide" />
                        </asp:BoundField>
                        <asp:BoundField DataField="pgs_chave" HeaderText="Chave" SortExpression="pgs_chave" />
                        <asp:BoundField DataField="sis_gru_nome" HeaderText="Sistema - grupo" SortExpression="sis_gru_nome" />
                        <asp:TemplateField HeaderText="Excluir" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="70px">
                            <ItemTemplate>
                                <asp:ImageButton ID="_btnExcluir" SkinID="btExcluir" CausesValidation="false" CommandName="Deletar"
                                    runat="server" />
                            </ItemTemplate>
                            <HeaderStyle Width="70px" CssClass="center"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:ObjectDataSource ID="_odsParametroGrupoPerfil" runat="server" SelectMethod="GetSelect"
                    TypeName="Autenticador.BLL.SYS_ParametroGrupoPerfilBO" EnablePaging="True"
                    MaximumRowsParameterName="pageSize" StartRowIndexParameterName="currentPage"
                    DeleteMethod="Delete" OnSelecting="_odsParametroGrupoPerfil_Selecting" SelectCountMethod="GetTotalRecords"
                    DataObjectTypeName="Autenticador.Entities.SYS_ParametroGrupoPerfil">
                    <SelectParameters>
                        <asp:Parameter Name="pgs_id" DbType="Int32" Size="4" />
                        <asp:Parameter Name="pgs_situacao" DbType="Byte" />
                        <asp:Parameter Name="paginado" DbType="Boolean" Size="1" DefaultValue="True" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
    </fieldset>
    <div id="divParametro" title="Parâmetros" class="hide">
        <br />
        <asp:UpdatePanel ID="_uppCadastroParametro" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="divUpn" runat="server">
                    <asp:Label ID="_lblMessageInsert" runat="server" EnableViewState="False"></asp:Label>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Parametro" />
                    <fieldset>
                        <legend>Incluir valor ao parâmetro</legend>
                        <asp:Label ID="_lblNome_Par" runat="server" EnableViewState="False" Text="Parâmetro"
                            AssociatedControlID="_ddlParametroValor"></asp:Label>
                        <div id="divParametroTextBox" runat="server" visible="false">
                            <asp:TextBox ID="txtValor" runat="server" MaxLength="1000" Width="400"></asp:TextBox>
                        </div>
                        <div id="divParametroCombo" runat="server" visible="false">
                            <asp:DropDownList ID="_ddlParametroValor" runat="server" AppendDataBoundItems="False"
                                DataSourceID="_odsComboParametro" ValidationGroup="Parametro" SkinID="text30C">
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="_odsComboParametro" runat="server" EnablePaging="True"
                                MaximumRowsParameterName="pageSize" StartRowIndexParameterName="currentPage"
                                DataObjectTypeName="Autenticador.Entities.SYS_Parametro" TypeName="Autenticador.BLL.SYS_ParametroBO"
                                SelectCountMethod="GetTotalRecords"></asp:ObjectDataSource>
                            <asp:CompareValidator ID="_cvParametroValor" runat="server" ErrorMessage="" ControlToValidate="_ddlParametroValor"
                                Operator="NotEqual" ValueToCompare="00000000-0000-0000-0000-000000000000" Display="Dynamic"
                                ValidationGroup="Parametro">*</asp:CompareValidator>
                            <div id="divVigencia" runat="server">
                                <asp:Label ID="_lblVigencia" runat="server" Text="Vigência" AssociatedControlID="_txtVigenciaIni"></asp:Label>
                                <asp:TextBox ID="_txtVigenciaIni" runat="server" MaxLength="10" Width="100px" SkinID="Data"
                                    ValidationGroup="Parametro"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="_rfvVigenciaIni" runat="server" ControlToValidate="_txtVigenciaIni"
                                    ValidationGroup="Parametro" Display="Dynamic" ErrorMessage="Data de vigência inicial é obrigatório."
                                    Visible="false">*</asp:RequiredFieldValidator>
                                <asp:CustomValidator ID="cvDataVigIni" runat="server" ControlToValidate="_txtVigenciaIni"
                                    ValidationGroup="Parametro" Display="Dynamic" ErrorMessage="Data inválida." OnServerValidate="ValidarData_ServerValidate"
                                    Visible="false">* </asp:CustomValidator>
                                <asp:Label ID="_lbla" runat="server" Text=" à "></asp:Label>
                                <asp:TextBox ID="_txtVigenciaFim" runat="server" MaxLength="10" Width="100px" SkinID="Data"
                                    ValidationGroup="Parametro"></asp:TextBox>
                                <asp:CustomValidator ID="cvDataVigFim" runat="server" ControlToValidate="_txtVigenciaFim"
                                    ValidationGroup="Parametro" Display="Dynamic" ErrorMessage="Data inválida." OnServerValidate="ValidarData_ServerValidate"
                                    Visible="false">* </asp:CustomValidator>
                            </div>
                        </div>
                        <div class="right">
                            <asp:Button ID="_btnSalvar" runat="server" Text="Salvar" Width="100px" CausesValidation="true"
                                OnClick="_btnSalvar_Click" ValidationGroup="Parametro" />
                            <asp:Button ID="_btnCancelar" runat="server" Text="Fechar" Width="100px" CausesValidation="False"
                                OnClientClick="$('#divParametro').dialog('close');" OnClick="_btnCancelar_Click" />
                        </div>
                    </fieldset>
                    <fieldset id="fsParametrosValores" runat="server" visible="false">
                        <legend>Valores cadastrados</legend>
                        <asp:GridView ID="_dgvParametro2" runat="server" DataSourceID="_odsParametro2" AutoGenerateColumns="False"
                            DataKeyNames="par_id" AllowPaging="True" OnRowDataBound="_dgvParametro2_RowDataBound"
                            OnRowEditing="_dgvParametro2_RowEditing" EmptyDataText="Não existem valores cadastrados."
                            OnRowCommand="_dgvParametro2_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="par_id" HeaderText="par_id" InsertVisible="False" ReadOnly="True"
                                    SortExpression="par_id" Visible="false" />
                                <asp:BoundField DataField="par_descricao" HeaderText="Parâmetro" SortExpression="par_descricao" />
                                <asp:BoundField DataField="par_valor_nome" HeaderText="Valor" SortExpression="par_valor_nome" />
                                <asp:BoundField DataField="par_vigencia" HeaderText="Vigência" SortExpression="par_vigencia"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <HeaderStyle CssClass="center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Detalhar/alterar" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="70px">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" SkinID="btEditar" ID="_btnAlterar" CausesValidation="false"
                                            CommandArgument='<%#Bind("par_chave") %>' CommandName="Edit" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="70px" CssClass="center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Excluir" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-Width="70px">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="_btnExcluir" CausesValidation="false" SkinID="btExcluir" CommandArgument='<%# Bind("par_vigenciaInicio") %>'
                                            runat="server" CommandName="Deletar" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="70px" CssClass="center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:ObjectDataSource ID="_odsParametro2" runat="server" DataObjectTypeName="Autenticador.Entities.SYS_Parametro"
                            DeleteMethod="Delete" EnablePaging="True" MaximumRowsParameterName="PageSize"
                            SelectCountMethod="GetTotalRecords" StartRowIndexParameterName="currentPage"
                            TypeName="Autenticador.BLL.SYS_ParametroBO" OnSelecting="_odsParametro2_Selecting">
                        </asp:ObjectDataSource>
                    </fieldset>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="upn1" runat="server">
            <Triggers>
                <asp:PostBackTrigger ControlID="Button1" />
            </Triggers>
            <ContentTemplate>
                <div id="divFileUpload" runat="server" visible="false">
                    <fieldset>
                        <legend>Incluir valor ao parâmetro</legend>
                        <asp:Label ID="Label1" runat="server" EnableViewState="False" Text="Parâmetro" AssociatedControlID="_ddlParametroValor"></asp:Label>
                        <asp:FileUpload ID="fupArquivo" runat="server" Width="400" />
                        <div class="right">
                            <asp:Button ID="Button1" runat="server" Text="Salvar" Width="100px" CausesValidation="false"
                                OnClick="_btnSalvar_Click" />
                            <asp:Button ID="Button2" runat="server" Text="Fechar" Width="100px" CausesValidation="False"
                                OnClientClick="$('#divParametro').dialog('close');" OnClick="_btnCancelar_Click" />
                        </div>
                    </fieldset>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="divParametroGrupoPerfil" title="Cadastro de grupo padrão" class="hide">
        <asp:UpdatePanel ID="_uppCadastroGrupoPerfil" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblMessageGrupoPerfil" runat="server"></asp:Label>
                <asp:ValidationSummary ID="ValidationSummary2" ValidationGroup="Padrao" runat="server" />
                <fieldset>
                    <asp:Label ID="lblChave" runat="server" Text="Chave *" AssociatedControlID="txtChave"></asp:Label>
                    <asp:TextBox ID="txtChave" runat="server" MaxLength="100" SkinID="text30C" ValidationGroup="Padrao"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvChave" ControlToValidate="txtChave" runat="server"
                        ValidationGroup="Padrao" ErrorMessage="Chave é obrigatório.">*</asp:RequiredFieldValidator>
                    <uc1:UCComboSistemaGrupo ID="UCComboSistemaGrupo" runat="server" />
                    <div align="right">
                        <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click1"
                            ValidationGroup="Padrao" />
                        <asp:Button ID="btn_Cancelar" runat="server" Text="Cancelar" OnClientClick="$('#divParametroGrupoPerfil').dialog('close');"
                            CausesValidation="false" />
                    </div>
                </fieldset>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
