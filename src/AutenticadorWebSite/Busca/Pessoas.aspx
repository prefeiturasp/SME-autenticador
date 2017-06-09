<%@ Page Language="C#" MasterPageFile="~/Busca/MasterPageBusca.master" AutoEventWireup="true" Inherits="Busca_Pessoas" Codebehind="Pessoas.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">    
    <fieldset>        
        <asp:Label ID="Label1" runat="server" Text="Nome" AssociatedControlID="_txtNome"></asp:Label>
        <asp:TextBox ID="_txtNome" runat="server" MaxLength="200" SkinID="text60C"></asp:TextBox>
        <asp:Label ID="_lblCPF" runat="server" Text="Label" AssociatedControlID="_txtCPF"></asp:Label>
        <asp:TextBox ID="_txtCPF" runat="server" MaxLength="50" SkinID="text15C"></asp:TextBox>
        <asp:Label ID="_lblRG" runat="server" Text="Label" AssociatedControlID="_txtRG"></asp:Label>
        <asp:TextBox ID="_txtRG" runat="server" MaxLength="50" SkinID="text15C"></asp:TextBox>
        <div class="right">
            <asp:Button ID="_btnPesquisar" runat="server" Text="Pesquisar" OnClick="_btnPesquisar_Click" CausesValidation="False" />
        </div>            
    </fieldset>
    <fieldset id="fdsResultados" runat="server">
        <legend>Resultados</legend>
        <asp:GridView ID="_dgvPessoas" runat="server" EmptyDataText="A pesquisa não encontrou resultados."
            AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="pes_id,pes_nome"
            DataSourceID="odsPessoas" OnRowEditing="_dgvPessoas_RowEditing">
            <Columns>
                <asp:BoundField DataField="pes_id" HeaderText="pes_id">
                    <HeaderStyle CssClass="hide" />
                    <ItemStyle CssClass="hide" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Nome">
                    <ItemTemplate>
                        <asp:LinkButton ID="_lkbSelect" runat="server" Text='<%# Bind("pes_nome") %>' CausesValidation="False"
                            CommandName="Edit"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="pes_dataNascimento" DataFormatString="{0:dd/MM/yyyy}"
                    HeaderText="Data nasc." />
                <asp:BoundField DataField="TIPO_DOCUMENTACAO_CPF" HeaderText="CPF" />
                <asp:BoundField DataField="TIPO_DOCUMENTACAO_RG" HeaderText="RG" />
            </Columns>
        </asp:GridView>
    </fieldset>
    <asp:ObjectDataSource ID="odsPessoas" runat="server" DataObjectTypeName="Autenticador.Entities.PES_Pessoa"
        EnablePaging="True" MaximumRowsParameterName="pageSize"
        OnSelecting="odsPessoas_Selecting" SelectCountMethod="GetTotalRecords" SelectMethod="GetSelect"
        StartRowIndexParameterName="currentPage" TypeName="Autenticador.BLL.PES_PessoaBO">
        <SelectParameters>
            <asp:ControlParameter ControlID="_txtNome" DbType="String" Name="nome" PropertyName="Text"
                Size="200" />
            <asp:ControlParameter ControlID="_txtCPF" DbType="String" Name="cpf" PropertyName="Text"
                Size="50" />
            <asp:ControlParameter ControlID="_txtRG" DbType="String" Name="rg" PropertyName="Text"
                Size="50" />
            <asp:Parameter DbType="Date" Name="data" DefaultValue="01/01/0001" Size="16"/>
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
