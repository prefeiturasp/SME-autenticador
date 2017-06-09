<%@ Page Title="" Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master"
    AutoEventWireup="true" Inherits="AreaAdm_LogErro_Busca" Codebehind="Busca.aspx.cs" %>

<%@ Register Src="../../WebControls/Combos/UCComboSistema.ascx" TagName="UCComboSistema"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            $('#divLogErro').dialog({
                bgiframe: true,
                autoOpen: false,
                resizable: false,
                modal: true,
                width: 700,
                open: function(type, data) {
                    $('#divLogErro').parent().appendTo($("#aspnetForm"));
                },
                buttons: {
                    Fechar: function() {
                        $('#divLogErro').dialog('close');
                        return false;
                    }
                }
            });

            $("#divLogErro_BuscaTabs").tabs({
                selected: $('#<%=txtSelectedTab.ClientID %>').val()
                , select: function(event, ui) {
                    $('#<%=txtSelectedTab.ClientID %>').val(ui.index + 1);
                }
            });
        });

        function ExibirDescricao(element, descricao) {
            $('#<%=lblDescricao.ClientID %>').attr("innerHTML", descricao);
            $('#divLogErro').dialog('option', 'title', 'Descrição do log de erro');
            $('.selector').dialog('option', 'closeOnEscape', false);
            $('#divLogErro').dialog('open');
        }
        
    </script>

    <asp:Label ID="_lblMessage" runat="server" EnableViewState="False"></asp:Label>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="logerro" />
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="logerro2" />
    <div id="divLogErro_BuscaTabs">
        <ul id="abas" class="hide">
            <li><a href="#divLogErro_BuscaTabs-1">Consulta de log de erros</a></li>
            <li><a href="#divLogErro_BuscaTabs-2">Consulta de diretório de erros</a></li>
            <li><a href="#divLogErro_BuscaTabs-3">Consulta quantidade de erros</a></li>
        </ul>
        <div id="divLogErro_BuscaTabs-1">
            <fieldset>
                <uc1:UCComboSistema ID="UCComboSistema1" runat="server" />
                <asp:Label ID="Label5" runat="server" Text="Usuário" AssociatedControlID="_txtUsuario"></asp:Label>
                <asp:TextBox ID="_txtUsuario" runat="server" SkinID="text30C"></asp:TextBox>
                <asp:Label ID="Label1" runat="server" Text="Data inicial *" AssociatedControlID="_txtDtInicio"></asp:Label>
                <asp:TextBox ID="_txtDtInicio" runat="server" SkinID="Data"></asp:TextBox>
                <asp:Label ID="Label3" runat="server" Text="(DD/MM/AAAA)"></asp:Label>
                <asp:RequiredFieldValidator ID="_rfvDtInicio" runat="server" ErrorMessage="Data inicial é obrigatório."
                    Text="*" ControlToValidate="_txtDtInicio" Display="Dynamic" ValidationGroup="logerro">*</asp:RequiredFieldValidator>
                <asp:CustomValidator ID="cvDataInicio" runat="server" ControlToValidate="_txtDtInicio"
                    ValidationGroup="logerro" Display="Dynamic" ErrorMessage="Data de início inválida."
                    OnServerValidate="ValidarData_ServerValidate">*</asp:CustomValidator>
                <asp:Label ID="Label2" runat="server" Text="Data final *" AssociatedControlID="_txtDtFinal"></asp:Label>
                <asp:TextBox ID="_txtDtFinal" runat="server" SkinID="Data"></asp:TextBox>
                <asp:Label ID="Label4" runat="server" Text="(DD/MM/AAAA)"></asp:Label>
                <asp:RequiredFieldValidator ID="_rfvDtFinal" runat="server" ErrorMessage="Data final é obrigatório."
                    ControlToValidate="_txtDtFinal" Display="Dynamic" ValidationGroup="logerro">*</asp:RequiredFieldValidator>
                <asp:CustomValidator ID="cvDataFim" runat="server" ControlToValidate="_txtDtFinal"
                    ValidationGroup="logerro" Display="Dynamic" ErrorMessage="Data de fim inválida."
                    OnServerValidate="ValidarData_ServerValidate">* </asp:CustomValidator>
                <asp:CustomValidator ID="cvDatas" runat="server" ErrorMessage="Data final deve ser maior ou igual a data inicial."
                    Display="Dynamic" ValidationGroup="logerro" Visible="false" OnServerValidate="ValidarDatas_ServerValidate">*</asp:CustomValidator>
                <div class="right">
                    <asp:Button ID="_btnPesquisa" runat="server" Text="Pesquisar" OnClick="_btnPesquisa_Click" ValidationGroup="logerro"/>
                </div>
            </fieldset>
            <fieldset id="fdsResultado" runat="server">
                <legend>Resultados</legend>
                <asp:GridView ID="_dgvDatas" runat="server" AutoGenerateColumns="False" EmptyDataText="A pesquisa não encontrou resultados."
                    AllowPaging="True" DataSourceID="odsDatas" OnRowCommand="_dgvDatas_RowCommand"
                    OnRowDataBound="_dgvDatas_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="Data" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Data" />
                        <asp:BoundField DataField="num_logs" HeaderText="Número de Erros" />
                        <asp:TemplateField HeaderText="Visualizar">
                            <ItemTemplate>
                                <asp:LinkButton ID="_lkbVisualizar" runat="server" CommandName="Select" CausesValidation="False">Visualizar</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Download">
                            <ItemTemplate>
                                <asp:LinkButton ID="_lkbDownload" runat="server" CommandName="Download" CommandArgument='<%# Bind("data") %>'
                                    CausesValidation="False">Download</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="sis_id" HeaderText="sis_id" HeaderStyle-CssClass="hide"
                            ItemStyle-CssClass="hide" />
                        <asp:BoundField DataField="sis_nome" HeaderText="Sistema" />
                    </Columns>
                </asp:GridView>
                <asp:ObjectDataSource ID="odsDatas" runat="server" EnablePaging="True" MaximumRowsParameterName="pageSize"
                    OnSelecting="odsDatas_Selecting" SelectCountMethod="GetTotalRecords" SelectMethod="GetSelect"
                    StartRowIndexParameterName="currentPage" TypeName="Autenticador.BLL.LOG_ErrosBO"
                    DataObjectTypeName="Autenticador.Entities.LOG_Erros"></asp:ObjectDataSource>
                <asp:GridView ID="_dgvLogs" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    DataKeyNames="err_id" DataSourceID="odsLogs" Visible="False" OnRowCommand="_dgvLogs_RowCommand"
                    OnRowDataBound="_dgvLogs_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="err_id" HeaderText="err_id" Visible="False" />
                        <asp:BoundField DataField="err_dataHora" HeaderText="Data/Hora" />
                        <asp:TemplateField HeaderText="Descrição">
                            <ItemTemplate>
                                <asp:LinkButton ID="_btnAlterar" runat="server" CausesValidation="False" CommandName="Alterar"
                                    Text='<%# Bind("err_descricao") %>' OnClientClick="$('#divLogErro').dialog('open'); return false;"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="err_machineName" HeaderText="Estação/Server" />
                        <asp:BoundField DataField="err_ip" HeaderText="IP" />
                        <asp:BoundField DataField="err_browser" HeaderText="Navegador" />
                        <asp:BoundField DataField="usu_login" HeaderText="Usuário" />
                        <asp:BoundField DataField="sis_decricao" HeaderText="Sistema" />
                        <asp:TemplateField HeaderText="Download">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Download" CausesValidation="False"
                                    CommandArgument='<%# Eval("err_id") %>'>Download</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:ObjectDataSource ID="odsLogs" runat="server" EnablePaging="True" MaximumRowsParameterName="pageSize"
                    OnSelecting="odsLogs_Selecting" SelectCountMethod="GetTotalRecords" SelectMethod="GetSelectBy_dia"
                    StartRowIndexParameterName="currentPage" TypeName="Autenticador.BLL.LOG_ErrosBO">
                </asp:ObjectDataSource>
            </fieldset>
        </div>
        <div id="divLogErro_BuscaTabs-2" class="hide">
            <fieldset>
                <asp:Label ID="_lblEndereco" runat="server" Text="Endereço: {0}"></asp:Label>
                <br />
                <br />
                <asp:GridView ID="_dgvPastas" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    OnPageIndexChanging="_dgvPastas_PageIndexChanging" OnRowCommand="_dgvPastas_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Diretório(s):">
                            <ItemTemplate>
                                <asp:Image ID="Img1" runat="server" SkinID="imgFolder" ToolTip='<%# Bind("Directories") %>' />
                                <asp:LinkButton ID="_lkbDir" runat="server" Text='<%# Bind("Directories") %>' CommandArgument='<%# Bind("Directories") %>'
                                    CommandName="Select" CausesValidation="False"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:GridView ID="_dgvArquivos" runat="server" EmptyDataText="A pesquisa não encontrou resultados."
                    AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="_dgvArquivos_PageIndexChanging"
                    OnRowCommand="_dgvArquivos_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Arquivo(s):">
                            <ItemTemplate>
                                <asp:Image ID="Img2" runat="server" SkinID="imgNotepad" ToolTip='<%# Bind("Files") %>' />
                                <asp:LinkButton ID="_lkbFiles" runat="server" CommandArgument='<%# Bind("Files") %>'
                                    Text='<%# Bind("Files") %>' CommandName="Download" CausesValidation="False"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </fieldset>
        </div>
        <div id="divLogErro_BuscaTabs-3" class= "hide">
             <fieldset>
                <asp:Label ID="lblDataInicial" runat="server" Text="Data inicial *" AssociatedControlID="txtDataInicial"></asp:Label>
                <asp:TextBox ID="txtDataInicial" runat="server" MaxLength = "10" SkinID="Data"></asp:TextBox>
                <asp:Label ID="lblFormatoDataInicial" runat="server" Text="(DD/MM/AAAA)"></asp:Label>
                <asp:RequiredFieldValidator ID="rfvDataInicial" runat="server" ErrorMessage="Data inicial é obrigatório."
                    Text="*" ControlToValidate="txtDataInicial" Display="Dynamic" ValidationGroup="logerro2">*</asp:RequiredFieldValidator>
                <asp:CustomValidator ID="cvDataInicial" runat="server" ControlToValidate="txtDataInicial"
                    ValidationGroup="logerro2" Display="Dynamic" ErrorMessage="Data de início inválida."
                    OnServerValidate="ValidarData_ServerValidate">*</asp:CustomValidator>
                <asp:Label ID="lblDataFinal" runat="server" Text="Data final *" AssociatedControlID="txtDataFinal"></asp:Label>
                <asp:TextBox ID="txtDataFinal" runat="server" MaxLength="10" SkinID="Data"></asp:TextBox>
                <asp:Label ID="lblFormatoDataFinal" runat="server" Text="(DD/MM/AAAA)"></asp:Label>
                <asp:RequiredFieldValidator ID="rfvDataFinal" runat="server" ErrorMessage="Data final é obrigatório."
                    Text="*" ControlToValidate="txtDataFinal" Display="Dynamic" ValidationGroup="logerro2">*</asp:RequiredFieldValidator>
                <asp:CustomValidator ID="cvDataFinal" runat="server" ControlToValidate="txtDataFinal"
                    ValidationGroup="logerro2" Display="Dynamic" ErrorMessage="Data de fim inválida."
                    OnServerValidate="ValidarData_ServerValidate">*</asp:CustomValidator>
                <asp:CustomValidator ID="cvDatasAba3" runat="server" ErrorMessage="Data final deve ser maior ou igual a data inicial."
                    Display="Dynamic" ValidationGroup="logerro2" Visible="false" OnServerValidate="ValidarDatas_ServerValidate2">*</asp:CustomValidator>
                <div class="right">
                  <asp:Button ID="btnPesquisarLog" runat="server" Text="Pesquisar" OnClick="btnPesquisarLog_Click"
                      ValidationGroup="logerro2"/>
                </div>
             </fieldset>
             <fieldset id="fdsResultadoLog" runat="server" visible ="false">
                <legend>Resultados</legend>
                 <asp:GridView ID="dgvQtdErros" runat="server" AllowPaging="True" DataSourceID="odsQtdErros" 
                    EmptyDataText="A pesquisa não encontrou resultados." OnRowCommand = "dgvQtdErros_RowCommand" 
                    OnRowDataBound="dgvQtdErros_RowDataBound" AutoGenerateColumns="False" DataKeyNames="sis_id, Data, err_tipoErro ">
                     <Columns>
                         <asp:BoundField DataField="Data" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Data" />
                         <asp:BoundField DataField="err_tipoErro" HeaderText="Tipo de erro" />
                         <asp:TemplateField HeaderText="Quantidade de erro">
                            <ItemTemplate>
                                <asp:LinkButton ID="lkbTipoErro" runat="server"  Text='<%# Eval("Quantidade")%>' 
                                CommandName="SelectTipoErro" CausesValidation="False" ></asp:LinkButton>
                            </ItemTemplate>
                         </asp:TemplateField>
                        <asp:BoundField DataField="sis_id" HeaderText="sis_id" HeaderStyle-CssClass="hide" ItemStyle-CssClass="hide"/>
                        <asp:BoundField DataField="sis_nome" HeaderText="Sistema" />
                     </Columns>
                 </asp:GridView>
                 <asp:ObjectDataSource ID="odsQtdErros" runat="server" DataObjectTypeName="Autenticador.Entities.LOG_Erros" 
                     DeleteMethod="Delete" EnablePaging="True" MaximumRowsParameterName="pageSize" 
                     OldValuesParameterFormatString="original_{0}" SelectCountMethod="GetTotalRecords"
                     SelectMethod="GetSelectby_busca_QtdErros" StartRowIndexParameterName="currentPage" 
                     TypeName="Autenticador.BLL.LOG_ErrosBO" UpdateMethod="Save" 
                     onselecting="odsQtdErros_Selecting">
                     <DeleteParameters>
                         <asp:Parameter Name="entity" Type="Object" />
                         <asp:Parameter Name="banco" Type="Object" />
                     </DeleteParameters>
                     <SelectParameters>
                         <asp:ControlParameter ControlID="txtDataInicial" Name="dataInicio" PropertyName="Text" Type="DateTime" DbType="Object" />
                         <asp:ControlParameter ControlID="txtDataFinal" Name="dataTermino" PropertyName="Text" Type="DateTime" DbType="Object" />
                     </SelectParameters>
                 </asp:ObjectDataSource>
                 <asp:GridView ID="dgvTipoErro" runat="server" AllowPaging="True" 
                     AutoGenerateColumns="False" DataKeyNames="err_id" DataSourceID="odsTipoErro" EmptyDataText="A pesquisa não encontrou resultados." 
                    Visible="False" OnRowDataBound="dgvTipoErro_RowDataBound">
                     <Columns>
                       <asp:BoundField DataField="err_id" HeaderText="err_id" Visible="False" />
                       <asp:BoundField DataField="err_dataHora" HeaderText="Data/Hora" />
                       <asp:TemplateField HeaderText="Descrição">
                            <ItemTemplate>
                                <asp:LinkButton ID="lkbDescricao" runat="server" CausesValidation="False" 
                                    Text='<%# Eval("err_descricao") %>' OnClientClick="$('#divLogErro').dialog('open'); return false;"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="err_machineName" HeaderText="Estação/Server" />
                        <asp:BoundField DataField="err_ip" HeaderText="IP" />
                        <asp:BoundField DataField="err_browser" HeaderText="Navegador" />
                        <asp:BoundField DataField="usu_login" HeaderText="Usuário" />
                        <asp:BoundField DataField="sis_decricao" HeaderText="Sistema" />
                    </Columns>
                </asp:GridView>
               <%--  <asp:ObjectDataSource ID="odsTipoErro" runat="server" EnablePaging="True"
                     MaximumRowsParameterName="pageSize" SelectCountMethod="GetTotalRecords" 
                     StartRowIndexParameterName="CurrentPage" 
                     DataObjectTypeName="Autenticador.Entities.LOG_Erros" DeleteMethod="Delete" 
                     OldValuesParameterFormatString="original_{0}" 
                     SelectMethod="GetSelectby_Busca_TipoErros" 
                     TypeName="Autenticador.BLL.LOG_ErrosBO" UpdateMethod="Save" 
                     onselecting="odsTipoErro_Selecting">
                     <DeleteParameters>
                         <asp:Parameter Name="entity" Type="Object" />
                         <asp:Parameter Name="banco" Type="Object" />
                     </DeleteParameters>
                    <SelectParameters>
                         <asp:ControlParameter ControlID="__Page" Name="sis_id" 
                             PropertyName='dgvQtdErros_Sis_id' Type="Int32" />
                         <asp:ControlParameter ControlID="__Page" Name="data" 
                             PropertyName= "dgvQtdErros_data" Type="DateTime" />
                         <asp:ControlParameter ControlID="__Page" Name="err_tipoErro" 
                             PropertyName="dgvQtdErros_err_tipoErro" Type="String" />            
                     </SelectParameters>
                 </asp:ObjectDataSource>--%>
                
               <asp:ObjectDataSource ID="odsTipoErro" runat="server" EnablePaging="True" MaximumRowsParameterName="pageSize"
                     onSelecting="odsTipoErro_Selecting" SelectCountMethod="GetTotalRecords" SelectMethod="GetSelectby_Busca_TipoErros" 
                     StartRowIndexParameterName="currentPage" TypeName="Autenticador.BLL.LOG_ErrosBO">
                 </asp:ObjectDataSource>
             </fieldset>
        </div>
    </div>
    <div id="divLogErro" title="Descrição do log de erro" class="hide">
        <fieldset>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lblDescricao" runat="server"></asp:Label></ContentTemplate></asp:UpdatePanel></fieldset>
        <%--        <div align="right">
            <fieldset>
                <asp:Button ID="btnFechar" runat="server" Text="Fechar" OnClientClick="$('#divLogErro').dialog('close');" />
            </fieldset>
        </div>--%>
    </div>
    <input id="txtSelectedTab" type="hidden" runat="server" />
</asp:Content>
