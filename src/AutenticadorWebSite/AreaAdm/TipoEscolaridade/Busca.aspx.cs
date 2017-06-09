using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autenticador.Web.WebProject;
using Autenticador.Entities;
using Autenticador.BLL;

public partial class AreaAdm_TipoEscolaridade_Busca : MotherPageLogado
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager sm = ScriptManager.GetCurrent(this);
        if (sm != null)
        {
            sm.Scripts.Add(new ScriptReference(ArquivoJS.MsgConfirmExclusao));
        }

        if (!IsPostBack)
        {
            string message = __SessionWEB.PostMessages;
            if (!String.IsNullOrEmpty(message))
                _lblMessage.Text = message;

            _grvTipoEscolaridade.PageIndex = 0;
            _grvTipoEscolaridade.PageSize = ApplicationWEB._Paginacao;

            if (_grvTipoEscolaridade.Rows.Count > 0)
            {
                ((ImageButton)_grvTipoEscolaridade.Rows[0].Cells[2].FindControl("_btnSubir")).Style.Add("visibility", "hidden");
                ((ImageButton)_grvTipoEscolaridade.Rows[_grvTipoEscolaridade.Rows.Count - 1].Cells[2].FindControl("_btnDescer")).Style.Add("visibility", "hidden");
            }

            _divConsulta.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_consultar;
            _btnNovo.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_inserir;
        }
    }

    #region PROPRIEDADES

    public Guid EditItem
    {
        get
        {
            return new Guid(_grvTipoEscolaridade.DataKeys[_grvTipoEscolaridade.EditIndex].Values[0].ToString());
        }
    }

    #endregion

    protected void odsTipoEscolaridade_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (e.ExecutingSelectCount)
            e.InputParameters.Clear();
    }

    protected void _grvTipoEscolaridade_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deletar")
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                Guid tes_id = new Guid(_grvTipoEscolaridade.DataKeys[index].Values[0].ToString());

                PES_TipoEscolaridade entity = new PES_TipoEscolaridade { tes_id = tes_id };
                PES_TipoEscolaridadeBO.GetEntity(entity);

                if (PES_TipoEscolaridadeBO.Delete(entity))
                {
                    _grvTipoEscolaridade.PageIndex = 0;
                    _grvTipoEscolaridade.DataBind();

                    if (_grvTipoEscolaridade.Rows.Count > 0)
                    {
                        ((ImageButton)_grvTipoEscolaridade.Rows[0].Cells[2].FindControl("_btnSubir")).Style.Add("visibility", "hidden");
                        ((ImageButton)_grvTipoEscolaridade.Rows[_grvTipoEscolaridade.Rows.Count - 1].Cells[2].FindControl("_btnDescer")).Style.Add("visibility", "hidden");
                    }

                    _lblMessage.Text = UtilBO.GetErroMessage("Tipo de escolaridade excluído com sucesso.", UtilBO.TipoMensagem.Sucesso);

                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Delete, "tes_id: " + tes_id);
                }
                else
                {
                    _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar excluir o tipo de escolaridade.", UtilBO.TipoMensagem.Erro);
                }
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex); 
                _lblMessage.Text = UtilBO.GetErroMessage(ex.Message, UtilBO.TipoMensagem.Erro);
            }
        }
        if (e.CommandName == "Subir")
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());

                Guid tes_idDescer = new Guid(_grvTipoEscolaridade.DataKeys[index - 1].Values[0].ToString());
                int tes_ordemDescer = Convert.ToInt32(_grvTipoEscolaridade.DataKeys[index].Values[1]);                
                PES_TipoEscolaridade entityDescer = new PES_TipoEscolaridade { tes_id = tes_idDescer };
                PES_TipoEscolaridadeBO.GetEntity(entityDescer);
                entityDescer.tes_ordem = tes_ordemDescer;

                Guid tes_idSubir = new Guid(_grvTipoEscolaridade.DataKeys[index].Values[0].ToString());
                int tes_ordemSubir = Convert.ToInt32(_grvTipoEscolaridade.DataKeys[index - 1].Values[1]);
                PES_TipoEscolaridade entitySubir = new PES_TipoEscolaridade { tes_id = tes_idSubir };
                PES_TipoEscolaridadeBO.GetEntity(entitySubir);
                entitySubir.tes_ordem = tes_ordemSubir;

                if (PES_TipoEscolaridadeBO.SaveOrdem(entityDescer, entitySubir))
                {
                    _grvTipoEscolaridade.PageIndex = 0;
                    _grvTipoEscolaridade.DataBind();

                    if (_grvTipoEscolaridade.Rows.Count > 0)
                    {
                        ((ImageButton)_grvTipoEscolaridade.Rows[0].Cells[2].FindControl("_btnSubir")).Style.Add("visibility", "hidden");
                        ((ImageButton)_grvTipoEscolaridade.Rows[_grvTipoEscolaridade.Rows.Count - 1].Cells[2].FindControl("_btnDescer")).Style.Add("visibility", "hidden");
                    }                    
                }

                ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Update, "tes_id: " + tes_idSubir);
                ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Update, "tes_id: " + tes_idDescer);
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex); 
                _lblMessage.Text = UtilBO.GetErroMessage(ex.Message, UtilBO.TipoMensagem.Erro);
            }             
        }
        if (e.CommandName == "Descer")
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());

                Guid tes_idDescer = new Guid(_grvTipoEscolaridade.DataKeys[index].Values[0].ToString());
                int tes_ordemDescer = Convert.ToInt32(_grvTipoEscolaridade.DataKeys[index + 1].Values[1]);
                PES_TipoEscolaridade entityDescer = new PES_TipoEscolaridade { tes_id = tes_idDescer };
                PES_TipoEscolaridadeBO.GetEntity(entityDescer);
                entityDescer.tes_ordem = tes_ordemDescer;

                Guid tes_idSubir = new Guid(_grvTipoEscolaridade.DataKeys[index + 1].Values[0].ToString());
                int tes_ordemSubir = Convert.ToInt32(_grvTipoEscolaridade.DataKeys[index].Values[1]);
                PES_TipoEscolaridade entitySubir = new PES_TipoEscolaridade { tes_id = tes_idSubir };
                PES_TipoEscolaridadeBO.GetEntity(entitySubir);
                entitySubir.tes_ordem = tes_ordemSubir;

                if (PES_TipoEscolaridadeBO.SaveOrdem(entityDescer, entitySubir))
                {
                    _grvTipoEscolaridade.PageIndex = 0;
                    _grvTipoEscolaridade.DataBind();

                    if (_grvTipoEscolaridade.Rows.Count > 0)
                    {
                        ((ImageButton)_grvTipoEscolaridade.Rows[0].Cells[2].FindControl("_btnSubir")).Style.Add("visibility", "hidden");
                        ((ImageButton)_grvTipoEscolaridade.Rows[_grvTipoEscolaridade.Rows.Count - 1].Cells[2].FindControl("_btnDescer")).Style.Add("visibility", "hidden");
                    }
                }

                ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Update, "tes_id: " + tes_idSubir);
                ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Update, "tes_id: " + tes_idDescer);
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex); 
                _lblMessage.Text = UtilBO.GetErroMessage(ex.Message, UtilBO.TipoMensagem.Erro);
            }
        }
    }

    protected void _grvTipoEscolaridade_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton _btnExcluir = (ImageButton)e.Row.FindControl("_btnExcluir");
            if (_btnExcluir != null)
            {
                _btnExcluir.CommandArgument = e.Row.RowIndex.ToString();
                _btnExcluir.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_excluir;
            }

            ImageButton _btnSubir = (ImageButton)e.Row.FindControl("_btnSubir");
            if (_btnSubir != null)
            {
                _btnSubir.ImageUrl = caminhoImagens + "cima.png";
                _btnSubir.CommandArgument = e.Row.RowIndex.ToString();
                _btnSubir.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_alterar;
            }

            ImageButton _btnDescer = (ImageButton)e.Row.FindControl("_btnDescer");
            if (_btnDescer != null)
            {
                _btnDescer.ImageUrl = caminhoImagens + "baixo.png";
                _btnDescer.CommandArgument = e.Row.RowIndex.ToString();
                _btnDescer.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_alterar;
            }

            LinkButton _btnAlterar = (LinkButton)e.Row.FindControl("_btnAlterar");
            if (_btnAlterar != null)
            {
                _btnAlterar.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_alterar;
            }

            Label _lblAlterar = (Label)e.Row.FindControl("_lblAlterar");
            if (_lblAlterar != null)
            {
                _lblAlterar.Visible = !(__SessionWEB.__UsuarioWEB.GrupoPermissao.grp_alterar);
            }
        }
    }

    protected void _btnNovo_Click(object sender, EventArgs e)
    {
        Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "TipoEscolaridade/Cadastro.aspx", false);
    }
}
