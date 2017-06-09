<%@ Control Language="C#" AutoEventWireup="true" Inherits="WebControls_Endereco_UCEnderecos"
    CodeBehind="UCEnderecos.ascx.cs" %>
<%@ Register Src="../Combos/UCComboZona.ascx" TagName="UCComboZona" TagPrefix="uc2" %>

<br />
<asp:UpdatePanel ID="updCadastroEndereco" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Label ID="lblMessage" runat="server" EnableViewState="False"></asp:Label>
        <asp:GridView ID="grvEndereco" runat="server" AllowPaging="False" AutoGenerateColumns="False"
            DataKeyNames="endRel_id" OnRowCommand="grvEndereco_RowCommand"
            OnRowDataBound="grvEndereco_RowDataBound" EmptyDataText="Não foi cadastrado endereço para a unidade administrativa.">
            <Columns>
                <asp:TemplateField HeaderText="#">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cep">
                    <ItemTemplate>
                        <asp:Label ID="_lblCep" runat="server" Text='<%# Bind("end_cep") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Logradouro">
                    <ItemTemplate>
                        <asp:Label ID="_lblRua" runat="server" Text='<%# Bind("end_logradouro") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Número">
                    <ItemTemplate>
                        <asp:Label ID="lblNumero" runat="server" Text='<%# Bind("Numero") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cidade">
                    <ItemTemplate>
                        <asp:Label ID="lblCidade" runat="server" Text='<%# Bind("cid_nome") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Endereço Principal">
                    <ItemTemplate>
                        <asp:RadioButton ID="ckbEnderecoPrincipal" runat="server" CssClass="EndPrincipal" Checked='<%# Convert.ToBoolean(Eval("enderecoprincipal")) %>' />
                    </ItemTemplate>
                    <HeaderStyle CssClass="center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Alterar">
                    <ItemTemplate>
                        <asp:ImageButton ID="_btnEditar" runat="server" CommandName="Editar" SkinID="btEditar" CausesValidation="false" rel="modal" />
                    </ItemTemplate>
                    <HeaderStyle CssClass="center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Excluir">
                    <ItemTemplate>
                        <asp:ImageButton ID="_btnExcluir" runat="server" CommandName="Deletar" SkinID="btExcluir" CausesValidation="false" CssClass="excluirBtn" />
                    </ItemTemplate>
                    <HeaderStyle CssClass="center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>Nenhum endereço cadastrado.</EmptyDataTemplate>
        </asp:GridView>
        <div class="botoes">
            <asp:Button ID="btnNovoEnd" runat="server" Text="Adicionar endereço" OnClick="btnNovoEnd_Click"
                CausesValidation="false" Visible="false" rel="modal" />
        </div>
    </ContentTemplate>

</asp:UpdatePanel>


<div id="CadastroEndereco" class="window " style="position: fixed;">


    <div class="ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix divHeaderEnderecoModal">
        <span class="ui-dialog-title" id="ui-dialog-title-divBuscaUA textHeaderEnderecoModal">Cadastro de Endereço</span>
        <a href="#" class="ui-dialog-titlebar-close ui-corner-all closeButtonEndereco" role="button">
            <span class="ui-icon-closethick">X</span>
        </a>
    </div>
    <br />
    <fieldset>


        <asp:Panel ID="pnlEndereco" runat="server" CssClass="tbEndereco">

            <!-- Validation Summary -->
            <asp:UpdatePanel ID="upValidationSummary" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:ValidationSummary ID="VS" runat="server" ValidationGroup="ValidationSummary2" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnAdicionar" />
                </Triggers>
            </asp:UpdatePanel>


            <!-- CEP -->


            <asp:Label ID="Label1" runat="server" EnableViewState="False"></asp:Label>
            <asp:RadioButton ID="ckbEndPrincipal" CssClass="checkEnderecoPrincipal" runat="server" />&nbspEndereço Principal<br />
            <br />
            <asp:Label ID="LabelCEP" runat="server" Text="CEP (somente números)  *" AssociatedControlID="txtCEP"> </asp:Label>
            <asp:TextBox ID="txtCEP" runat="server" MaxLength="8" Width="160" SkinID="CepIncremental" AutoPostBack="True" OnTextChanged="txtCEP_TextChanged" CssClas="txtEndCep"> </asp:TextBox>

            <asp:RequiredFieldValidator ID="rfvCEP" runat="server" ValidationGroup="ValidationSummary2" ControlToValidate="txtCEP"
                Display="Dynamic" ErrorMessage="CEP é obrigatório.">* </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revCEP" runat="server" ValidationGroup="ValidationSummary2"
                ControlToValidate="txtCEP" Display="Dynamic" ErrorMessage="CEP inválido." ValidationExpression="^([0-9]){8}$">* </asp:RegularExpressionValidator>
            <asp:ImageButton ID="btnLimparEndereco" runat="server" Visible="False" SkinID="btLimpar"
                ToolTip="Limpar campos do endereço" CssClass="tbNovoEndereco_incremental" OnClick="btnLimparEndereco_Click" CausesValidation="false" TabIndex="10" />


            <!-- Campos do Form a serem alterados -->
            <asp:UpdatePanel ID="updModal" runat="server" UpdateMode="Conditional">
                <ContentTemplate>

                    <asp:Label ID="LabelLogradouro" runat="server" Text="Endereço *" AssociatedControlID="txtLogradouro"></asp:Label>
                    <asp:TextBox ID="txtLogradouro" runat="server" MaxLength="200" ToolTip="Digite para buscar o endereço"
                        Width="510" CssClass="tbLogradouro_incremental"> </asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvLogradouro" runat="server" ValidationGroup="ValidationSummary2"
                        ControlToValidate="txtLogradouro" ErrorMessage="Endereço é obrigatório." Display="Dynamic">* </asp:RequiredFieldValidator>
                    <table>
                        <tr id="trNumeroCompl" runat="server">
                            <td>
                                <asp:Label ID="LabelNumero" runat="server" Text="Número *"
                                    AssociatedControlID="txtNumero"> </asp:Label>
                                <asp:TextBox ID="txtNumero" runat="server" MaxLength="20"
                                    Width="240px"> </asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvNumero" runat="server" ValidationGroup="ValidationSummary2" ControlToValidate="txtNumero" ErrorMessage="Número é obrigatório." Display="Dynamic">* </asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="LabelComplemento" runat="server" Text="Complemento" AssociatedControlID="txtComplemento"> </asp:Label>
                                <asp:TextBox ID="txtComplemento" runat="server" MaxLength="100" Width="240px"> </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LabelDistrito" runat="server" Text="Distrito" AssociatedControlID="txtDistrito"></asp:Label>
                                <asp:TextBox ID="txtDistrito" runat="server" MaxLength="100" CssClass="text30C tbDistrito_incremental"> </asp:TextBox>
                            </td>
                            <td>
                                <uc2:UCComboZona ID="UCComboZona1" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LabelBairro" runat="server" Text="Bairro *" AssociatedControlID="txtBairro"></asp:Label>
                                <asp:TextBox ID="txtBairro" runat="server" MaxLength="100" Width="240" CssClass="tbBairro_incremental"> </asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvBairro" runat="server" ControlToValidate="txtBairro" ValidationGroup="ValidationSummary2" Display="Dynamic" ErrorMessage="Bairro é obrigatório.">* </asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <input id="txtCid_id" runat="server" type="hidden" class="tbCid_id_incremental" />
                                <asp:Label ID="LabelCidade" runat="server" ValidationGroup="ValidationSummary2" Text='Cidade *' AssociatedControlID="txtCidade"> </asp:Label>
                                <asp:TextBox ID="txtCidade" runat="server" MaxLength="200" CssClass="text30C tbCidade_incremental"> </asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvCidade" runat="server" ControlToValidate="txtCidade" ValidationGroup="ValidationSummary2" Display="Dynamic" ErrorMessage="Cidade é obrigatório.">* </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LabelLatitude" runat="server" Text="Latitude (de -99.999999 a 99.999999)" AssociatedControlID="txtLatitude"></asp:Label>
                                <asp:TextBox ID="txtLatitude" runat="server" MaxLength="12" SkinID="Decimal2" CssClass="text30C position" Text='<%#Bind("latitude") %>'>
                                </asp:TextBox>
                                <asp:RegularExpressionValidator ID="revLatitude" runat="server" ValidationGroup="ValidationSummary2" ControlToValidate="txtLatitude" Display="Dynamic" ErrorMessage="Latitude inválida." ValidationExpression="^(\-?)([d+(\d?)]{1,2})[.](\-?\d+(\.\d+)?)$">* </asp:RegularExpressionValidator>
                            </td>
                            <td>
                                <asp:Label ID="LabelLongitude" runat="server" Text="Longitude (de -99.999999 a 99.999999)" AssociatedControlID="txtLongitude"></asp:Label>
                                <asp:TextBox ID="txtLongitude" runat="server" MaxLength="12" SkinID="Decimal2" CssClass="text30C position" Text='<%#Bind("longitude") %>'></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revLongitude" runat="server" ValidationGroup="ValidationSummary2" ControlToValidate="txtLongitude" Display="Dynamic" ErrorMessage="Longitude inválida." ValidationExpression="^(\-?)([d+(\d?)]{1,2})[.](\-?\d+(\.\d+)?)$">* </asp:RegularExpressionValidator>

                            </td>
                        </tr>
                    </table>
                    <asp:Label ID="lblBanco" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lblID" runat="server" Visible="false"></asp:Label>
                    <input id="txtEnd_id" runat="server" type="hidden" value='<%#Bind("end_id") %>' class="tbEnd_id_incremental" />
                    <input id="txtId" runat="server" type="hidden" />
                    <input id="txtNovo" runat="server" type="hidden" />

                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="txtCEP" EventName="TextChanged" />
                </Triggers>
            </asp:UpdatePanel>

        </asp:Panel>
    </fieldset>

    <div class="right">
        <asp:Button runat="server" Text="Gravar" ID="btnAdicionar" CausesValidation="true" OnClick="btnAdicionar_Click" ValidationGroup="ValidationSummary2" />

        <button type="button" name="cancel2" id="cancel2" class="btnCancel btnCancelarModal">Cancelar</button>
    </div>

</div>

<div id="mascara"></div>
<script type="text/javascript">

    function limparMascara() {
        $(".tbCep_incremental").val("");
        $(".checkEnderecoPrincipal input").attr('checked', false);
        $("#mascara").hide();
        $(".window").hide();
        $("body").css("overflow", "auto");
    }

    function carregarEditar(cep, principal) {

        $(".tbCep_incremental").val(cep);

        if (principal) {
            $(".checkEnderecoPrincipal input").attr('checked', true);
        }
        else {
            $(".checkEnderecoPrincipal input").attr('checked', false);
        }

        $(".tbDistrito_incremental").attr('readOnly1', "readOnly");
        $(".tbZona_incremental").attr('disabled', "true");
        $(".tbBairro_incremental").attr('readOnly1', "readOnly");
        $(".tbCidade_incremental").attr('readOnly1', "readOnly");

        $('input[readOnly1="readOnly"]').unbind('keydown', down).keydown(down);
    }

    //ADICIONADO POR CONTA DO UPDATEPANEL. PRECISA SER ADICIONADO NA PÁGINA POR CONTA DE POSTBACK.
    Sys.Application.add_load(function () {

        $('.EndPrincipal').children().attr('disabled', true);

        // MODAL
        $("input[rel=modal]").click(function (ev) {
            var id = $("#CadastroEndereco");
            // PASSA A DIV MASCARA PARA UMA VARIAVEL
            var mask = $('#mascara');
            // REMOVE A MASCARA DO HTML
            mask.remove();
            // ADICIONA A MASCARA NO BODY E REMOVE A ROLAGEM
            $("body").prepend(mask).css("overflow", "hidden");

            // PEGA ALTURA E LARGURA DA PÁGINA 
            var alturaTela = $(document).height();
            var larguraTela = $(document).width();
            // APLICA FUNDO PRETO
            $('#mascara').css({ 'width': larguraTela, 'height': alturaTela });
            $('#mascara').fadeIn(100);
            $('#mascara').fadeTo("fast", 0.8);
            var left = ($(document).width() / 2) - ($(id).width() / 2);
            var top = "50px";
            $(id).css({ 'top': top, 'left': left });
            $(id).show();

        });

        $("#mascara").click(function () {
            limparMascara();
        });

        $("#Cancel").click(function () {
            limparMascara();
        });

        $(".closeButtonEndereco").click(function () {
            limparMascara();
        });

        $(".btnCancelarModal").click(function () {
            limparMascara();
        });

        $(".btExcluir").click(function (event) {
            if (!window.confirm("Deseja excluir este item?")) {
                event.preventDefault();
            }
        });

    });
</script>
<style>
    .divHeaderEnderecoModal {
        height: 28px;
    }

    .textHeaderEnderecoModal {
        float: left !important;
        left: 17px !important;
        margin: 6px !important;
        margin-left: 10px !important;
    }

    .closeButtonEndereco {
        float: right;
        right: 17px;
        margin: 6px;
        margin-right: 10px;
    }

    .btnCancel {
        display: inline-block;
        padding: 6px 10px;
        -webkit-box-shadow: 0 1px 0 rgba(0,0,0,.05);
        box-shadow: 0 1px 0 rgba(0,0,0,.05);
        margin: 0;
        min-width: 34px;
        text-align: center;
        white-space: nowrap;
        outline: 0;
        border-radius: 3px !important;
        cursor: default;
        font: 1em Arial,sans-serif !important;
        background-color: #6E8C45;
        background-image: -webkit-linear-gradient(top,transparent,transparent);
        background-image: linear-gradient(top,transparent,transparent);
        border: 1px solid transparent !important;
        color: #fff;
    }
</style>


