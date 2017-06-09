using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autenticador.BLL;
using Autenticador.Web.WebProject;
using Autenticador.Entities;
using System.Collections.Generic;

public partial class AreaAdm_Log_Busca : MotherPageLogado
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager sm = ScriptManager.GetCurrent(this);
        if (sm != null)
        {
            sm.Scripts.Add(new ScriptReference(ArquivoJS.CamposData));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.UiAriaTabs));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.Tabs));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.PastaScriptRaiz + "Telas/jsModuloSeguranca.js"));
        }
        
        if (!IsPostBack)
        {
            _txtDtInicio.Text = DateTime.Now.ToString("dd/MM/yyyy");
            _txtDtFinal.Text = DateTime.Now.ToString("dd/MM/yyyy");

            _dgvLog.PageSize = ApplicationWEB._Paginacao;

            UCComboSistemas1.Inicialize("Sistema");
            UCComboSistemas1._ShowSelectMessage = true;
            UCComboSistemas1._EnableValidator = false;
            UCComboSistemas1._Load();
            
            _LoadTipoLog();
            
            VerificaBusca();

            Page.Form.DefaultButton = _btnPesquisa.UniqueID;            
        }
    }

    #region METODOS

    private void _Pesquisar()
    {
        try
        {
            Dictionary<string, string> filtros = new Dictionary<string, string>();
            
            DateTime dataInicial;
            string dataStringInicial = string.Empty;

            DateTime dataFinal;
            string dataStringFinal = string.Empty;

            _dgvLog.PageIndex = 0;
            odsLog.SelectParameters.Clear();

            if (!string.IsNullOrEmpty(_txtDtInicio.Text))
            {
                dataInicial = Convert.ToDateTime(_txtDtInicio.Text);
                dataStringInicial = dataInicial.ToString("yyyy-MM-dd");
            }

            if (!string.IsNullOrEmpty(_txtDtFinal.Text))
            {
                dataFinal = Convert.ToDateTime(_txtDtFinal.Text);
                dataStringFinal = dataFinal.ToString("yyyy-MM-dd");
            }

            odsLog.SelectParameters.Add("DataInicio", dataStringInicial);
            odsLog.SelectParameters.Add("DataTermino", dataStringFinal);
            odsLog.SelectParameters.Add("sistema", UCComboSistemas1._Combo.SelectedValue);
            odsLog.SelectParameters.Add("acao", _ddlTipoLog.SelectedValue);
            odsLog.SelectParameters.Add("login", _txtLogin.Text);
            odsLog.DataBind();

            #region Salvar busca realizada com os parâmetros do ODS.

            foreach (Parameter param in odsLog.SelectParameters)
            {
                filtros.Add(param.Name, param.DefaultValue);
            }

            __SessionWEB.BuscaRealizada = new Busca
            {
                PaginaBusca = Pagina.AreaAdm_Log
                ,
                Filtros = filtros
            };

            #endregion

            _dgvLog.DataBind();
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar os logs de sistema.", UtilBO.TipoMensagem.Erro);
        }
    }

    /// <summary>
    /// Verifica se tem busca salva na sessão e realiza automaticamente, caso positivo.
    /// </summary>
    private void VerificaBusca()
    {
        if (__SessionWEB.BuscaRealizada.PaginaBusca == Pagina.AreaAdm_Log)
        {
            // Recuperar busca realizada e pesquisar automaticamente
            string valor;

            __SessionWEB.BuscaRealizada.Filtros.TryGetValue("sistema", out valor);
            if (!string.IsNullOrEmpty(valor) && valor != (-1).ToString())
            {
                UCComboSistemas1.SetaEventoSource();
                UCComboSistemas1._Combo.DataBind();
                UCComboSistemas1._Combo.SelectedValue = valor;                
            }

            __SessionWEB.BuscaRealizada.Filtros.TryGetValue("acao", out valor);
            _ddlTipoLog.SelectedValue = valor;

            __SessionWEB.BuscaRealizada.Filtros.TryGetValue("login", out valor);
            _txtLogin.Text = valor;

            __SessionWEB.BuscaRealizada.Filtros.TryGetValue("DataInicio", out valor);
            if (!string.IsNullOrEmpty(valor))
                _txtDtInicio.Text = Convert.ToDateTime(valor).ToString("dd/MM/yyyy");

            __SessionWEB.BuscaRealizada.Filtros.TryGetValue("DataTermino", out valor);
            if (!string.IsNullOrEmpty(valor))
                _txtDtFinal.Text = Convert.ToDateTime(valor).ToString("dd/MM/yyyy");                         

            _Pesquisar();
        }
        else
        {
            fdsResultado.Visible = false;
        }
    }

    protected void _LoadTipoLog()
    {
        _ddlTipoLog.DataSource = Enum.GetNames(typeof(LOG_SistemaTipo));        
        _ddlTipoLog.DataBind();
        _ddlTipoLog.Items.Insert(0, new ListItem("-- Selecione uma opção --", String.Empty, true));
        _ddlTipoLog.SelectedIndex = 0;
    }

    #endregion

    #region Eventos

    protected void odsLog_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (e.ExecutingSelectCount)
            e.InputParameters.Clear();
    }

    protected void _btnPesquisa_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
            fdsResultado.Visible = false;
        else
        {
            cvDatas.Visible = true;
            cvDatas.Validate();
            if (Page.IsValid)
            {
                if (!string.IsNullOrEmpty(_txtDtInicio.Text) && !string.IsNullOrEmpty(_txtDtFinal.Text))
                {
                    if (Convert.ToDateTime(_txtDtFinal.Text) >= Convert.ToDateTime(_txtDtInicio.Text).AddDays(30))
                    {
                        _lblMessage.Text = UtilBO.GetErroMessage("O intervalo de datas não pode ser maior que 30 dias.", UtilBO.TipoMensagem.Alerta);
                        fdsResultado.Visible = false;
                    }
                    else
                    {
                        fdsResultado.Visible = true;
                        _Pesquisar();
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(_txtDtInicio.Text))
                        _lblMessage.Text = UtilBO.GetErroMessage("Data inicial é obrigatório.", UtilBO.TipoMensagem.Alerta);
                    else if (string.IsNullOrEmpty(_txtDtFinal.Text))
                        _lblMessage.Text = UtilBO.GetErroMessage("Data final é obrigatório.", UtilBO.TipoMensagem.Alerta);

                    fdsResultado.Visible = false;
                }
            }
            else
                fdsResultado.Visible = false;
            cvDatas.Visible = false;
        }
    }

    protected void ValidarDatas_ServerValidate(object source, ServerValidateEventArgs args)
    {
        DateTime dtIni, dtFim;
        bool flag = true;

        dtIni = Convert.ToDateTime(_txtDtInicio.Text);
        dtFim = Convert.ToDateTime(_txtDtFinal.Text);

        if (dtIni > dtFim)
            flag = false;

        args.IsValid = flag;
    }

    protected void _dgvLog_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Alterar")
        {
            int index = int.Parse(e.CommandArgument.ToString());
            Guid log_id = new Guid(_dgvLog.DataKeys[index].Value.ToString());
            LOG_Sistema logSistema = new LOG_Sistema { log_id = log_id };
            LOG_SistemaBO.GetEntity(logSistema);

            txtID.Text = logSistema.log_id.ToString();
            txtDataHora.Text = logSistema.log_dataHora.ToString();
            txtEnderecoIP.Text = logSistema.log_ip;
            txtNomeMaquina.Text = logSistema.log_machineName;
            txtSistema.Text = logSistema.sis_nome ?? string.Empty;
            txtModulo.Text = logSistema.mod_nome ?? string.Empty;
            txtUsuarioID.Text = logSistema.usu_id.ToString();
            txtUsuarioLogin.Text = logSistema.usu_login ?? string.Empty;
            txtGrupoID.Text = logSistema.gru_id != Guid.Empty ? logSistema.gru_id.ToString() : string.Empty;
            txtGrupoNome.Text = logSistema.gru_nome ?? string.Empty;

            if (logSistema.log_grupoUA != null && logSistema.log_grupoUA != "[]")
                txtGrupoUA.Text = logSistema.log_grupoUA;
            else
                txtGrupoUA.Text = string.Empty;

            txtAcao.Text = logSistema.log_acao;
            txtDecricao.Text = logSistema.log_descricao;

            txtID.Enabled = false;
            txtDataHora.Enabled = false;
            txtEnderecoIP.Enabled = false;
            txtNomeMaquina.Enabled = false;
            txtSistema.Enabled = false;
            txtModulo.Enabled = false;
            txtUsuarioID.Enabled = false;
            txtUsuarioLogin.Enabled = false;
            txtGrupoID.Enabled = false;
            txtGrupoNome.Enabled = false;
            txtGrupoUA.Enabled = false;
            txtAcao.Enabled = false;
            txtDecricao.Enabled = false;
                        
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Log", "$('#divLog').dialog('open');", true); 
        }
    }

    protected void _dgvLog_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Header && e.Row.RowType != DataControlRowType.Footer && e.Row.RowType != DataControlRowType.Pager)
        {
            LinkButton btnAlterar = (LinkButton)e.Row.FindControl("_btnAlterar");
            if (btnAlterar != null)
            {
                btnAlterar.CommandArgument = e.Row.RowIndex.ToString();
            }
        }
    }

    protected void btnFechar_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "LogFecha", "$('#divLog').dialog('close');", true);
    }

    #endregion
}
