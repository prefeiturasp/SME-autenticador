using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autenticador.Web.WebProject;
using Autenticador.BLL;
using Autenticador.Entities;
using System.Collections.Generic;

public partial class AreaAdm_Usuario_Busca : MotherPageLogado
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
            _dgvUsuario.PageSize = ApplicationWEB._Paginacao;

            try
            {
                string ent_padrao = __SessionWEB.__UsuarioWEB.Usuario.ent_id.ToString();
                if (!string.IsNullOrEmpty(ent_padrao))
                    UCComboEntidade1._Combo.SelectedValue = ent_padrao;

                UCComboEntidade1.Inicialize("Entidade");
                UCComboEntidade1._Load(Guid.Empty, 1);
                UCComboEntidade1._EnableValidator = false;
                if (__SessionWEB.__UsuarioWEB.Grupo.vis_id == SysVisaoID.Administracao)
                    UCComboEntidade1._Load(Guid.Empty, 0);
                else
                    UCComboEntidade1._LoadBy_UsuarioGrupoUA(Guid.Empty, __SessionWEB.__UsuarioWEB.Grupo.gru_id, __SessionWEB.__UsuarioWEB.Usuario.usu_id, 0);
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
                _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o sistema.", UtilBO.TipoMensagem.Erro);
            }

            VerificaBusca();

            _divConsulta.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_consultar;
            _btnPesquisa.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_consultar;
            _btnNovo.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_inserir;            
            
            Page.Form.DefaultButton = _btnPesquisa.UniqueID;
            Page.Form.DefaultFocus = UCComboEntidade1._Combo.ClientID;
        } 
    }

    #region PROPRIEDADES

    public Guid EditItem
    {
        get { return new Guid(_dgvUsuario.DataKeys[_dgvUsuario.EditIndex].Value.ToString()); }
    }

    #endregion

    protected void dgvUsuario_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton _btnExcluir = (ImageButton)e.Row.FindControl("_btnExcluir");
            if (_btnExcluir != null)
            {
                _btnExcluir.CommandArgument = e.Row.RowIndex.ToString();

                if (__SessionWEB.__UsuarioWEB.GrupoPermissao.grp_excluir)
                {
                    if (((DataRowView)e.Row.DataItem).Row["usu_situacao"].ToString() == "4")
                        _btnExcluir.Visible = false;
                }
                else
                {
                    _btnExcluir.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_excluir;
                }
            }

            LinkButton btnAlterar = (LinkButton)e.Row.FindControl("_lkbAlterar");
            if (btnAlterar != null)
            {
                if (__SessionWEB.__UsuarioWEB.GrupoPermissao.grp_alterar)
                {
                    if (((DataRowView)e.Row.DataItem).Row["usu_situacao"].ToString() == "4")
                        btnAlterar.Visible = false;
                }
                else
                    btnAlterar.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_alterar;
            }

            Label lblAlterar = (Label)e.Row.FindControl("_lblAlterar");
            if (lblAlterar != null)
            {
                if (__SessionWEB.__UsuarioWEB.GrupoPermissao.grp_alterar)
                {
                    if (((DataRowView)e.Row.DataItem).Row["usu_situacao"].ToString() == "4")
                        lblAlterar.Visible = true;
                }
                else
                {
                    lblAlterar.Visible = !(__SessionWEB.__UsuarioWEB.GrupoPermissao.grp_alterar);
                }
            }
        }
    }

    protected void _dgvUsuario_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deletar")
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                Guid usu_id = new Guid(_dgvUsuario.DataKeys[index].Value.ToString());

                //DELETA O VINCULO DE USUARIO COM USUARIOLOGINPROVIDER (CASO EXISTA)
                SYS_UsuarioLoginProviderBO.DeleteBy_usu_id(usu_id);

                SYS_Usuario entity = new SYS_Usuario { usu_id = usu_id };
                SYS_UsuarioBO.GetEntity(entity);

                if (SYS_UsuarioBO.Delete(entity, null))
                {
                    _dgvUsuario.PageIndex = 0;
                    _dgvUsuario.DataBind();

                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Delete, "usu_id: " + usu_id);
                    _lblMessage.Text = UtilBO.GetErroMessage("Usuário excluído com sucesso.", UtilBO.TipoMensagem.Sucesso);
                }
                else
                {
                    _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar excluir o usuário.", UtilBO.TipoMensagem.Erro);
                }
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
                _lblMessage.Text = UtilBO.GetErroMessage(ex.Message, UtilBO.TipoMensagem.Erro);
            }

        }
    }

    protected void odsUsuarios_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (e.ExecutingSelectCount)
            e.InputParameters.Clear();
    }

    protected void _btnNovo_Click(object sender, EventArgs e)
    {
        Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "Usuario/Cadastro.aspx");
    }

    protected void _btnPesquisa_Click(object sender, EventArgs e)
    {
      fdsResultado.Visible = true;
      _Pesquisar();
    }

    private void _Pesquisar()
    {
        try
        {
            Dictionary<string, string> filtros = new Dictionary<string, string>();

            odsUsuarios.SelectParameters.Clear();
            odsUsuarios.SelectParameters.Add("ent_id", UCComboEntidade1._Combo.SelectedValue);
            odsUsuarios.SelectParameters.Add("login", _txtLogin.Text);
            odsUsuarios.SelectParameters.Add("email", _txtEmail.Text);
            odsUsuarios.SelectParameters.Add("bloqueado", _ddlSituacao.SelectedValue);
            odsUsuarios.SelectParameters.Add("pessoa", _txtPessoa.Text);
            odsUsuarios.DataBind();

            #region Salvar busca realizada com os parâmetros do ODS.

            foreach (Parameter param in odsUsuarios.SelectParameters)
            {
                filtros.Add(param.Name, param.DefaultValue);
            }

            __SessionWEB.BuscaRealizada = new Busca
            {
                PaginaBusca = Pagina.AreaAdm_Usuario
                ,
                Filtros = filtros
            };

            #endregion

            _dgvUsuario.PageIndex = 0;
            _dgvUsuario.DataBind();
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar os usuários.", UtilBO.TipoMensagem.Erro);
        }    
    }

    /// <summary>
    /// Verifica se tem busca salva na sessão e realiza automaticamente, caso positivo.
    /// </summary>
    private void VerificaBusca()
    {
        if (__SessionWEB.BuscaRealizada.PaginaBusca == Pagina.AreaAdm_Usuario)
        {
            // Recuperar busca realizada e pesquisar automaticamente
            string valor;

            __SessionWEB.BuscaRealizada.Filtros.TryGetValue("ent_id", out valor);

            if (!string.IsNullOrEmpty(valor) && valor != Guid.Empty.ToString())
            {
                UCComboEntidade1.SetaEventoSource();
                UCComboEntidade1._Combo.DataBind();
                UCComboEntidade1._Combo.SelectedValue = valor;
            }

            __SessionWEB.BuscaRealizada.Filtros.TryGetValue("login", out valor);
            _txtLogin.Text = valor;

            __SessionWEB.BuscaRealizada.Filtros.TryGetValue("email", out valor);
            _txtEmail.Text = valor;

            __SessionWEB.BuscaRealizada.Filtros.TryGetValue("bloqueado", out valor);
            _ddlSituacao.SelectedValue = valor;

            __SessionWEB.BuscaRealizada.Filtros.TryGetValue("pessoa", out valor);
            _txtPessoa.Text = valor;
            
            _Pesquisar();
        }
        else
        {
            fdsResultado.Visible = false;
        }
    }
}
