using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autenticador.Web.WebProject;
using Autenticador.BLL;
using Autenticador.Entities;
using System.Collections.Generic;

public partial class AreaAdm_Grupo_Busca : MotherPageLogado
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager sm = ScriptManager.GetCurrent(this);
        
        if(sm != null)
            sm.Scripts.Add(new ScriptReference(ArquivoJS.MsgConfirmExclusao));

        if (!IsPostBack)
        {
            string message = __SessionWEB.PostMessages;
            if (!String.IsNullOrEmpty(message))
                _lblMessage.Text = message;
            _dgvGrupo.PageSize = ApplicationWEB._Paginacao;

            UCComboSistemas1.Inicialize("Sistema *");
            UCComboSistemas1._ShowSelectMessage = true;
            UCComboSistemas1._Load();
            UCComboSistemas1._ValidationGroup = "Pesquisa";

            VerificaBusca();

            Page.Form.DefaultButton = _btnPesquisa.UniqueID;
            Page.Form.DefaultFocus = UCComboSistemas1._Combo.ClientID;

            divPesquisa.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_consultar;
            _btnPesquisa.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_consultar;
            _btnNovo.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_inserir;            
        }
    }

    #region PROPRIEDADES

    public Guid EditItem
    {
        get
        {
            return new Guid(_dgvGrupo.DataKeys[_dgvGrupo.EditIndex].Values[0].ToString());
        }
    }

    public int EditItem_sis_id
    {
        get
        {
            return Convert.ToInt32(_dgvGrupo.DataKeys[_dgvGrupo.EditIndex].Values[1].ToString());
        }
    }

    public int EditItem_vis_id
    {
        get
        {
            return Convert.ToInt32(_dgvGrupo.DataKeys[_dgvGrupo.EditIndex].Values[2].ToString());
        }
    }

    public string GrupoNome
    {
        get
        {
            LinkButton _lkbAlterar = (LinkButton)_dgvGrupo.Rows[_dgvGrupo.EditIndex].FindControl("_lkbAlterar");
            if (_lkbAlterar != null)
                return _lkbAlterar.Text;
            return string.Empty;
        }
    }

    public string VisaoNome
    {
        get
        {
            return _dgvGrupo.Rows[_dgvGrupo.EditIndex].Cells[3].Text;
        }
    }

    public string SistemaNome
    {
        get
        {
            return _dgvGrupo.Rows[_dgvGrupo.EditIndex].Cells[2].Text;
        }
    }

    #endregion

    protected void _btnLimpaCache_Click(object sender, EventArgs e)
    {
        UtilBO.LimpaCache("Menu_", UCComboSistemas1._Combo.SelectedValue);
        _lblMessage.Text = UtilBO.GetErroMessage(String.Format("Cache limpo com sucesso."), UtilBO.TipoMensagem.Sucesso);
    }

    protected void _btnPesquisa_Click(object sender, EventArgs e)
    {        
        fdsResultados.Visible = true;
        _Pesquisar();
    }
    private void _Pesquisar()
    {
        Dictionary<string, string> filtros = new Dictionary<string, string>();

        odsGrupo.SelectParameters.Clear();
        odsGrupo.SelectParameters.Add("sis_id", UCComboSistemas1._Combo.SelectedValue);
        odsGrupo.SelectParameters.Add("paginado", "true");
        odsGrupo.SelectParameters.Add("pageSize", ApplicationWEB._Paginacao.ToString());
        odsGrupo.DataBind();

        #region Salvar busca realizada com os parâmetros do ODS.

        foreach (Parameter param in odsGrupo.SelectParameters)
        {
            filtros.Add(param.Name, param.DefaultValue);
        }

        __SessionWEB.BuscaRealizada = new Busca
        {
            PaginaBusca = Pagina.AreaAdm_Grupo
            ,
            Filtros = filtros
        };

        #endregion

        _dgvGrupo.PageIndex = 0;
        _dgvGrupo.DataBind();
    }

    /// <summary>
    /// Verifica se tem busca salva na sessão e realiza automaticamente, caso positivo.
    /// </summary>
    private void VerificaBusca()
    {
        if (__SessionWEB.BuscaRealizada.PaginaBusca == Pagina.AreaAdm_Grupo)
        {
            // Recuperar busca realizada e pesquisar automaticamente
            string valor;

            __SessionWEB.BuscaRealizada.Filtros.TryGetValue("sis_id", out valor);

            if (!string.IsNullOrEmpty(valor) && Convert.ToInt32(valor) > 0)
            {
                UCComboSistemas1.SetaEventoSource();
                UCComboSistemas1._Combo.DataBind();
                UCComboSistemas1._Combo.SelectedValue = valor;
            }

            _Pesquisar();
        }
        else
        {
            fdsResultados.Visible = false;
        }

    }

    protected void odsGrupo_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (e.ExecutingSelectCount)
            e.InputParameters.Clear();
    }

    protected void _btnNovo_Click(object sender, EventArgs e)
    {
        Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "Grupo/Cadastro.aspx");
    }


    protected void _dgvGrupo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnAlterar = (LinkButton)e.Row.FindControl("_lkbAlterar");
            if (btnAlterar != null)
            {
                btnAlterar.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_alterar;
                if (((DataRowView)e.Row.DataItem).Row["gru_situacao"].ToString() == "4")
                    btnAlterar.Visible = false;
            }

            Label lblAlterar = (Label)e.Row.FindControl("_lblAlterar");
            if (lblAlterar != null)
            {
                lblAlterar.Visible = !__SessionWEB.__UsuarioWEB.GrupoPermissao.grp_alterar; 
                if (((DataRowView)e.Row.DataItem).Row["gru_situacao"].ToString() == "4")
                    lblAlterar.Visible = true;
            }

            ImageButton btnPermissao = (ImageButton)e.Row.FindControl("_btnPermissao");
            if (btnPermissao != null)
            {
                btnPermissao.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_alterar;
                if (((DataRowView)e.Row.DataItem).Row["gru_situacao"].ToString() == "4")
                    btnPermissao.Visible = false;
            }

            ImageButton btnDelete = (ImageButton)e.Row.FindControl("_btnExcluir");
            if (btnDelete != null)
            {
                btnDelete.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_excluir;
                btnDelete.CommandArgument = e.Row.RowIndex.ToString();
                if (((DataRowView)e.Row.DataItem).Row["gru_situacao"].ToString() == "4")
                     btnDelete.Visible = false;
            }
        }
    }
    protected void _dgvGrupo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deletar")
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                Guid gru_id = new Guid(_dgvGrupo.DataKeys[index].Value.ToString());

                SYS_Grupo EntityGrupo = new SYS_Grupo { gru_id = gru_id };
                SYS_GrupoBO.GetEntity(EntityGrupo);

                if (SYS_GrupoBO.Delete(EntityGrupo))
                {
                    _dgvGrupo.PageIndex = 0;
                    _dgvGrupo.DataBind();
                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Delete, "gru_id: " + gru_id);
                    _lblMessage.Text = UtilBO.GetErroMessage("Grupo excluído com sucesso.", UtilBO.TipoMensagem.Sucesso);
                }
                else
                {
                    _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar excluir o grupo.", UtilBO.TipoMensagem.Erro);  
                }
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
                _lblMessage.Text = UtilBO.GetErroMessage(ex.Message, UtilBO.TipoMensagem.Erro);
            }
        }
    }
}
