using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autenticador.Web.WebProject;
using Autenticador.BLL;
using Autenticador.Entities;
using System.Collections.Generic;

public partial class AreaAdm_DiasNaoUtil_Busca : MotherPageLogado
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager sm = ScriptManager.GetCurrent(this);
        if (sm != null)
        {
            sm.Scripts.Add(new ScriptReference(ArquivoJS.CamposData));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.JqueryMask));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.MascarasCampos));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.JQueryValidation));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.MsgConfirmExclusao));
        }

        if (!IsPostBack)
        {
            string message = __SessionWEB.PostMessages;
            if (!String.IsNullOrEmpty(message))
                _lblMessage.Text = message;
            _dgvDiaNaoUtil.PageSize = ApplicationWEB._Paginacao;

            Page.Form.DefaultButton = _btnPesquisar.UniqueID;
            Page.Form.DefaultFocus = _txtNome.ClientID;

            VerificaBusca();

            divPesquisa.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_consultar;
            _btnPesquisar.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_consultar;
            _btnNovo.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_inserir;
        }
    }

    #region PROPRIEDADES

    public Guid EditItem
    {
        get
        {
            return new Guid(_dgvDiaNaoUtil.DataKeys[_dgvDiaNaoUtil.EditIndex].Value.ToString());
        }
    }
    #endregion

    private void _Pesquisar()
    {
        try
        {
            Dictionary<string, string> filtros = new Dictionary<string, string>();

            DateTime data;
            string dataString = string.Empty;

            _dgvDiaNaoUtil.PageIndex = 0;
            _odsDiaNaoUtil.SelectParameters.Clear();
            _odsDiaNaoUtil.SelectParameters.Add("dnu_nome", _txtNome.Text);

            if (_ddlRecorencia.SelectedValue.Equals("0"))
            {
                if (!string.IsNullOrEmpty(_txtDataDia.Text) && !string.IsNullOrEmpty(_txtDataMes.Text))
                {
                    data = Convert.ToDateTime(_txtDataDia.Text + "/" + _txtDataMes.Text + "/" + "2000");
                    dataString = data.ToString("yyyy-MM-dd");
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(_txtData.Text))
                {
                    data = Convert.ToDateTime(_txtData.Text);
                    dataString = data.ToString("yyyy-MM-dd");                    
                }
            }

            _odsDiaNaoUtil.SelectParameters.Add("dnu_data", dataString);
            _odsDiaNaoUtil.SelectParameters.Add("dnu_abrangencia", _ddlAbrangencia.SelectedValue);            
            _odsDiaNaoUtil.SelectParameters.Add("dnu_recorrencia", _ddlRecorencia.SelectedValue);
            _odsDiaNaoUtil.SelectParameters.Add("paginado", "true");
            _odsDiaNaoUtil.DataBind();

            #region Salvar busca realizada com os parâmetros do ODS.

            foreach (Parameter param in _odsDiaNaoUtil.SelectParameters)
            {
                filtros.Add(param.Name, param.DefaultValue);
            }

            __SessionWEB.BuscaRealizada = new Busca
            {
                PaginaBusca = Pagina.AreaAdm_DiaNaoUtil
                ,
                Filtros = filtros
            };

            #endregion

            _dgvDiaNaoUtil.DataBind();
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar os dias não útil.", UtilBO.TipoMensagem.Erro);
        }
    }

     /// <summary>
    /// Verifica se tem busca salva na sessão e realiza automaticamente, caso positivo.
    /// </summary>
    private void VerificaBusca()
    {
        if (__SessionWEB.BuscaRealizada.PaginaBusca == Pagina.AreaAdm_DiaNaoUtil)
        {
            // Recuperar busca realizada e pesquisar automaticamente
            string valor;

            __SessionWEB.BuscaRealizada.Filtros.TryGetValue("dnu_nome", out valor);
            _txtNome.Text = valor;

            __SessionWEB.BuscaRealizada.Filtros.TryGetValue("dnu_data", out valor);
            if (!string.IsNullOrEmpty(valor))
                _txtData.Text = Convert.ToDateTime(valor).ToString("dd/MM/yyyy");

            __SessionWEB.BuscaRealizada.Filtros.TryGetValue("dnu_abrangencia", out valor);
            _ddlAbrangencia.SelectedValue = valor;

            __SessionWEB.BuscaRealizada.Filtros.TryGetValue("dnu_recorrencia", out valor);
            _ddlRecorencia.SelectedValue = valor;

            _Pesquisar();
        }
        else
        {
            fdsResultados.Visible = false;
        }
    }

    private bool _AlteraTela_DiaNaoUtilRecorrente
    {
        set
        {
            _txtData.Visible = !value;
            _lblFormatoData.Visible = !value;
            cvData.Visible = !value;

            _txtDataDia.Visible = value;
            _revDataDia.Visible = value;
            _lblBarra.Visible = value;
            _txtDataMes.Visible = value;
            _lblFormatoData2.Visible = value;
            _revDataMes.Visible = value;

            _txtDataMes.Text = string.Empty;
            _txtDataDia.Text = string.Empty;
            _txtDataMes.Text = string.Empty;

            _ddlRecorencia.Focus();
        }
    }

    protected void _btnNovo_Click(object sender, EventArgs e)
    {
        Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "DiasNaoUtil/Cadastro.aspx",false);
    }

    protected void _odsDiaNaoUtil_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (e.ExecutingSelectCount)
            e.InputParameters.Clear();
    }

    protected void _dgvDiaNaoUtil_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (!e.Row.Cells[3].Text.Contains("Recorrente"))
                e.Row.Cells[4].Text = string.Empty;

            Label _lblAlterar = (Label)e.Row.FindControl("_lblAlterar");
            if (_lblAlterar != null)
            {
                _lblAlterar.Visible = !__SessionWEB.__UsuarioWEB.GrupoPermissao.grp_alterar;
            }

            LinkButton _btnAlterar = (LinkButton)e.Row.FindControl("_btnAlterar");
            if (_btnAlterar != null)
            {
                _btnAlterar.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_alterar;
            }

            ImageButton btnDelete = (ImageButton)e.Row.FindControl("_btnExcluir");
            if (btnDelete != null)
            {               
                btnDelete.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_excluir;
                if (UtilBO.VerificaDataMaior(DateTime.Now, Convert.ToDateTime(btnDelete.CommandArgument)) || UtilBO.VerificaDataIgual(Convert.ToDateTime(btnDelete.CommandArgument), DateTime.Now) )
                    e.Row.FindControl("_btnExcluir").Visible = false;

                btnDelete.CommandArgument = e.Row.RowIndex.ToString();
            }
        }
    }

    protected void _btnPesquisa_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            fdsResultados.Visible = true;
            _Pesquisar();
        }
        else
        {
            fdsResultados.Visible = false;
        }
    }

    protected void _dgvDiaNaoUtil_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deletar")
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                Guid dnu_id = new Guid(_dgvDiaNaoUtil.DataKeys[index].Value.ToString());

                SYS_DiaNaoUtil entity = new SYS_DiaNaoUtil { dnu_id = dnu_id };
                SYS_DiaNaoUtilBO.GetEntity(entity);

                if (SYS_DiaNaoUtilBO.Delete(entity))
                {
                    _dgvDiaNaoUtil.PageIndex = 0;
                    _dgvDiaNaoUtil.DataBind();
                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Delete, "dnu_id: " + dnu_id);
                    _lblMessage.Text = UtilBO.GetErroMessage("Dia não útil excluído com sucesso.", UtilBO.TipoMensagem.Sucesso);                    
                }
                else
                {
                    _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar excluir a dia não útil.", UtilBO.TipoMensagem.Erro);  
                }
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
                _lblMessage.Text = UtilBO.GetErroMessage(ex.Message, UtilBO.TipoMensagem.Erro);                
            }
            finally
            {
                _updBuscaDiaNaoUtil.Update();
            }
        }
    }

    protected void _ddlRecorencia_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            _AlteraTela_DiaNaoUtilRecorrente = _ddlRecorencia.SelectedValue.Equals("0") || _ddlRecorencia.SelectedValue.Equals("2") ? true : false;            
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o sistema.", UtilBO.TipoMensagem.Erro);            
        }
        finally
        {
            _updBuscaDiaNaoUtil.Update();
        }
    }
}
