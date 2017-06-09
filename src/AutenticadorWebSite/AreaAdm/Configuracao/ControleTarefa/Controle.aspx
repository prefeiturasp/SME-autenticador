<%@ Page Title="" Language="C#" MasterPageFile="~/AreaAdm/MasterPageLogado.master" AutoEventWireup="true" CodeBehind="Controle.aspx.cs" Inherits="AutenticadorWebSite.AreaAdm.Configuracao.ControleTarefa.Controle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblMessage" runat="server" EnableViewState="false"></asp:Label>

    <div id="divPainel" runat="server">
        <asp:Panel ID="pnlSchedulerInfo" runat="server" GroupingText="Informações Scheduler">
            <asp:Label ID="lblName" runat="server"></asp:Label><br />
            <asp:Label ID="lblStatus" runat="server"></asp:Label><br />
            <asp:Label ID="lblRunningSince" runat="server"></asp:Label><br />
            <asp:Label ID="lblTotalJobs" runat="server"></asp:Label><br />
            <asp:Label ID="lblJobsExecuted" runat="server"></asp:Label><br />
            <asp:Label ID="lblInstanceID" runat="server"></asp:Label><br />
            <asp:Label ID="lblIsRemote" runat="server"></asp:Label><br />
            <asp:Label ID="lblSchedulerType" runat="server"></asp:Label><br />
        </asp:Panel>
        <asp:Panel ID="pnlJobs" runat="server" GroupingText="Jobs">
            <asp:GridView ID="grvJobs" runat="server" AutoGenerateColumns="False" OnRowCommand="grvJobs_RowCommand">
                <Columns>
                    <asp:BoundField AccessibleHeaderText="Nome" DataField="Name" HeaderText="Nome" ReadOnly="True" />
                    <asp:BoundField AccessibleHeaderText="Unique Name" DataField="UniqueName" HeaderText="Unique Name"
                        ReadOnly="True" />
                    <asp:BoundField AccessibleHeaderText="Tem Triggers" DataField="HaveTriggers" HeaderText="Tem Triggers"
                        ReadOnly="True" />
                    <asp:BoundField AccessibleHeaderText="Status" DataField="Status" HeaderText="Status"
                        ReadOnly="True" />
                    <asp:ButtonField AccessibleHeaderText="Trigger" CommandName="ViewTrigger" HeaderText="Trigger"
                        Text="View Triggers" />
                </Columns>
            </asp:GridView>
            <asp:Panel ID="pnlTriggers" runat="server" GroupingText="Triggers" Visible="false">
                <asp:Button ID="btnNewTrigger" runat="server" OnClientClick="$('#divNewTrigger').dialog('open'); return false;" Text="Adicionar trigger" CausesValidation="False" />
                <asp:GridView ID="grvTriggers" runat="server" AutoGenerateColumns="False" OnRowCommand="grvTriggers_RowCommand">
                    <Columns>
                        <asp:BoundField AccessibleHeaderText="Nome" DataField="Name" HeaderText="Nome" ReadOnly="True" />
                        <asp:BoundField AccessibleHeaderText="Status" DataField="Status" HeaderText="Status"
                            ReadOnly="True" />
                        <asp:TemplateField AccessibleHeaderText="StartDate" HeaderText="StartDate">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "StartDate")== null? "" : ((DateTimeOffset)DataBinder.Eval(Container.DataItem, "StartDate")).ToLocalTime().ToString("G")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField AccessibleHeaderText="EndDate" HeaderText="EndDate"
                            ConvertEmptyStringToNull="False">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "EndDate")== null? "" : ((DateTimeOffset)DataBinder.Eval(Container.DataItem, "EndDate")).ToLocalTime().ToString("G")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField AccessibleHeaderText="Previous fire date"
                            HeaderText="Previous fire date">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PreviousFireDate")== null? "" : ((DateTimeOffset)DataBinder.Eval(Container.DataItem, "PreviousFireDate")).ToLocalTime().ToString("G")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField AccessibleHeaderText="Next fire date"
                            HeaderText="Next fire date">
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "NextFireDate")== null? "" : ((DateTimeOffset)DataBinder.Eval(Container.DataItem, "NextFireDate")).ToLocalTime().ToString("G")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                    CommandName='<%# Convert.ToBoolean(Eval("CanStart")) == true ? "ResumeTrigger" : "PauseTrigger"  %>'
                                    Text='<%# Convert.ToBoolean(Eval("CanStart")) == true ? "Resume" : "Pause"  %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:ButtonField CommandName="DeleteTrigger" Text="Delete" />
                    </Columns>
                </asp:GridView>
            </asp:Panel>
        </asp:Panel>
    </div>
    <div id="divNewTrigger" class="hide">
        <asp:ValidationSummary ID="vlsMessage" runat="server" ValidationGroup="TriggerGroup" EnableViewState="False" />
        <asp:Panel ID="pnlCadastroTrigger" runat="server" GroupingText="Trigger">
            <asp:Label ID="lblJobName" runat="server" EnableViewState="false" Text="Job:" AssociatedControlID="txtJobName"></asp:Label>
            <asp:TextBox ID="txtJobName" runat="server" ReadOnly="true" EnableViewState="false" MaxLength="100" SkinID="text60C"></asp:TextBox>
            <asp:Label ID="lblTriggerName" runat="server" EnableViewState="false" Text="Trigger:" AssociatedControlID="txtTrigger"></asp:Label>
            <asp:TextBox ID="txtTrigger" runat="server" MaxLength="150" SkinID="text60C" EnableViewState="false"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTxtTrigger" runat="server" Display="Dynamic"
                ErrorMessage="Nome da trigger é obrigatório" ControlToValidate="txtTrigger"
                ValidationGroup="TriggerGroup">*</asp:RequiredFieldValidator>
            <asp:Label ID="lblAgendamento" runat="server" EnableViewState="false" Text="Agendamento:" AssociatedControlID="txtAgendamento"></asp:Label>
            <asp:TextBox ID="txtAgendamento" runat="server" MaxLength="120" SkinID="text60C" EnableViewState="false"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                Display="Dynamic" ErrorMessage="Nome da trigger é obrigatório"
                ControlToValidate="txtTrigger" ValidationGroup="TriggerGroup">*</asp:RequiredFieldValidator>
        </asp:Panel>
        <div class="right">
            <asp:Button ID="btnAddTrigger" CausesValidation="true" runat="server"
                Text="Salvar" OnClick="btnAddTrigger_Click" ValidationGroup="TriggerGroup" />
        </div>
    </div>
</asp:Content>
