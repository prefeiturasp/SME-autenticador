using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autenticador.Entities;
using Autenticador.Web.WebProject;
using Autenticador.BLL;

public partial class AreaAdm_Grupo_Associar : MotherPageLogado
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //set javascript
        ScriptManager sm = ScriptManager.GetCurrent(this);
        if (sm != null)
        {
            sm.Scripts.Add(new ScriptReference(ArquivoJS.UiAriaTabs));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.Tabs));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.MsgConfirmExclusao));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.PastaScriptRaiz + "Telas/jsModuloSeguranca.js"));
        }

        UCUA1.Paginacao = ApplicationWEB._Paginacao;
        UCUA1.ContainerName = "divBuscaUA";
        UCUA1.ReturnValues += UCUA1BuscaUA;
        UCUA1.AddParameters("ent_idVisible", "false");
        UCUA1.AddParameters("ent_id", _VS_ent_id);

        UCUsuario1.Paginacao = ApplicationWEB._Paginacao;
        UCUsuario1.ContainerName = "divBuscaUsuario";
        UCUsuario1.ReturnValues += UCUA1BuscaUsuario;

        if (!IsPostBack)
        {
            _dgvUsuario.PageSize = ApplicationWEB._Paginacao;

            if ((PreviousPage != null) && (PreviousPage.IsCrossPagePostBack))
            {
                _VS_gru_id = PreviousPage.EditItem;
                _VS_sis_id = PreviousPage.EditItem_sis_id;
                _VS_vis_id = PreviousPage.EditItem_vis_id;
                _lblGrupo.Text = PreviousPage.GrupoNome;
                _lbVisao.Text = PreviousPage.VisaoNome;
                _lblSistema.Text = PreviousPage.SistemaNome;
            }

            divUA.Visible = ((_VS_vis_id != SysVisaoID.Administracao) && (_VS_vis_id != SysVisaoID.Individual));
                            
            _LoadGridUsuarios();

            Page.Form.DefaultButton = _btnVoltar.UniqueID;

            _btnNovo.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_inserir;
        }
    }

    #region PROPRIEDADES

    public Guid _VS_gru_id
    {
        get
        {
            if (ViewState["_VS_gru_id"] != null)
                return new Guid(ViewState["_VS_gru_id"].ToString());
            return Guid.Empty;
        }
        set
        {
            ViewState["_VS_gru_id"] = value;
        }
    }

    public int _VS_sis_id
    {
        get
        {
            if (ViewState["_VS_sis_id"] != null)
                return Convert.ToInt32(ViewState["_VS_sis_id"].ToString());
            return -1;
        }
        set
        {
            ViewState["_VS_sis_id"] = value;
        }
    }

    public int _VS_vis_id
    {
        get
        {
            if (ViewState["_VS_vis_id"] != null)
                return Convert.ToInt32(ViewState["_VS_vis_id"].ToString());
            return -1;
        }
        set
        {
            ViewState["_VS_vis_id"] = value;
        }
    }

    public Guid _VS_ent_id
    {
        get
        {
            if (ViewState["_VS_ent_id"] != null)
                return new Guid(ViewState["_VS_ent_id"].ToString());
            return Guid.Empty;
        }
        set
        {
            ViewState["_VS_ent_id"] = value;
        }
    }

    public bool _VS_IsNew
    {
        get
        {
            if (ViewState["_VS_IsNew"] != null)
                return Convert.ToBoolean(ViewState["_VS_IsNew"].ToString());
            return true;
        }
        set
        {
            ViewState["_VS_IsNew"] = value;
        }
    }

    private SortedDictionary<Guid, SYS_UsuarioBO.TmpGrupos> _VS_Grupos
    {
        get
        {
            if (ViewState["_VS_Grupos"] == null)
                ViewState["_VS_Grupos"] = new SortedDictionary<Guid, SYS_UsuarioBO.TmpGrupos>();
            return (SortedDictionary<Guid, SYS_UsuarioBO.TmpGrupos>)ViewState["_VS_Grupos"];
        }
    }

    private SortedDictionary<Guid, List<SYS_UsuarioBO.TmpEntidadeUA>> _VS_EntidadeUA
    {
        get
        {
            if (ViewState["_VS_EntidadeUA"] == null)
            {
                SortedDictionary<Guid, List<SYS_UsuarioBO.TmpEntidadeUA>> lt = new SortedDictionary<Guid, List<SYS_UsuarioBO.TmpEntidadeUA>>();
                ViewState["_VS_EntidadeUA"] = lt;
            }
            return (SortedDictionary<Guid, List<SYS_UsuarioBO.TmpEntidadeUA>>)ViewState["_VS_EntidadeUA"];
        }
    }

    #endregion

    #region METODOS

    private void UCUA1BuscaUsuario(IDictionary<string, object> parameters)
    {
        try
        {
            _LimparDivUsuario();

            _txtUsu_id.Value = parameters["usu_id"].ToString();
            _VS_ent_id = new Guid(parameters["ent_id"].ToString());
            _txtUsuario.Text = parameters["usu_login"].ToString();            

            //Carrega os grupos do usuário
            SYS_UsuarioBO.GetGruposUsuario(new Guid(_txtUsu_id.Value), _VS_Grupos, _VS_EntidadeUA);
            
            if (_VS_EntidadeUA.ContainsKey(_VS_gru_id))
            {
                List<SYS_UsuarioBO.TmpEntidadeUA> lt = _VS_EntidadeUA[_VS_gru_id];
                foreach (SYS_UsuarioBO.TmpEntidadeUA ent in lt)
                {
                    ListItem li = ent.Entidade
                                      ? new ListItem(ent.EntidadeOrUA, ent.ent_id.ToString())
                                      : new ListItem(ent.EntidadeOrUA, String.Concat(ent.ent_id, ";", ent.uad_id));
                    _lstUAs.Items.Add(li);
                }
            }
        }
        catch (Exception ex)
        {
            _lblMessageUsuario.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o usuário.", UtilBO.TipoMensagem.Erro);
            ApplicationWEB._GravaErro(ex);
        }      
        finally
        {
            _updUsuario.Update();
        }
    }

    private void UCUA1BuscaUA(IDictionary<string, object> parameters)
    {
        try
        {
            _txtEnt_id.Value = parameters["ent_id"].ToString();
            _txtUad_id.Value = parameters["uad_id"].ToString();
            _txtUA.Text = parameters["uad_nome"].ToString();
            _updUsuario.Update();
        }
        catch (Exception ex)
        {
            _lblMessageUsuario.Text = UtilBO.GetErroMessage("Erro ao tentar carregar a unidade administrativa.", UtilBO.TipoMensagem.Erro);
            ApplicationWEB._GravaErro(ex);
        }
    }

    private void _LoadGridUsuarios()
    {
        try
        {
            _dgvUsuario.PageIndex = 0;
            odsUsuario.SelectParameters.Clear();
            odsUsuario.SelectParameters.Add("gru_id", _VS_gru_id.ToString());
            odsUsuario.SelectParameters.Add("paginado", "true");
            odsUsuario.DataBind();
            _dgvUsuario.DataBind();
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar os usuários.", UtilBO.TipoMensagem.Erro);
        }
    }

    private void _LimparDivUsuario()
    {        
        _VS_Grupos.Clear();
        _VS_EntidadeUA.Clear();
        _VS_IsNew = true;

        _btnUsuario.Visible = true;

        _lstUAs.Items.Clear();
        _ckbGrupo_Bloqueado.Checked = false;

        _txtEnt_id.Value = string.Empty;
        _txtUad_id.Value = string.Empty;
        _txtUA.Text = string.Empty;

        _txtUsu_id.Value = string.Empty;
        _txtUsuario.Text = string.Empty;

        _btnUsuario.Focus();

        _updUsuario.Update();
    }

    private void _AlterarUsuario(Guid usu_id, string usu_login)
    {
        try
        {
            _LimparDivUsuario();
            _VS_IsNew = false;

            //Carrega os grupos do usuário
            SYS_UsuarioBO.GetGruposUsuario(usu_id, _VS_Grupos, _VS_EntidadeUA);            

            SYS_UsuarioBO.TmpGrupos grupo = _VS_Grupos[_VS_gru_id];
            _ckbGrupo_Bloqueado.Checked = (grupo.usg_situacao == 2) ? true : false;

            List<SYS_UsuarioBO.TmpEntidadeUA> lt = _VS_EntidadeUA[_VS_gru_id];
            foreach (SYS_UsuarioBO.TmpEntidadeUA ent in lt)
            {
                ListItem li = ent.Entidade ? new ListItem(ent.EntidadeOrUA, ent.ent_id.ToString()) : new ListItem(ent.EntidadeOrUA, String.Concat(ent.ent_id, ";", ent.uad_id));
                _lstUAs.Items.Add(li);
            }

            _txtUsu_id.Value = usu_id.ToString();
            _txtUsuario.Text = usu_login;

            _btnUsuario.Visible = false;
            _btnUA.Focus();

            ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlterarUsuario", "$('#divUsuario').dialog('open');", true);
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o grupo.", UtilBO.TipoMensagem.Erro);
        }
    }

    private void _ExcluirUsuario(Guid usu_id)
    {
        try
        {
            //Carrega os grupos do usuário
            SYS_UsuarioBO.GetGruposUsuario(usu_id, _VS_Grupos, _VS_EntidadeUA);

            //Remove o grupo do usuário
            SYS_UsuarioBO.RemoveTmpGrupo(_VS_gru_id, _VS_Grupos, _VS_EntidadeUA);

            //Recupera os dados do usuário
            SYS_Usuario usu = new SYS_Usuario { usu_id = usu_id };
            SYS_UsuarioBO.GetEntity(usu);

            usu.usu_senha = string.Empty;

            //Deleta o grupo do usuário
            if (SYS_UsuarioBO.Save(usu, _VS_Grupos, _VS_EntidadeUA, false, string.Empty, string.Empty, string.Empty, string.Empty, null))
            {
                ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Delete, "gru_id: " + _VS_gru_id + "; usu_id: " + usu_id.ToString());
                _lblMessage.Text = UtilBO.GetErroMessage(String.Format("Associação do usuário excluída com sucesso."), UtilBO.TipoMensagem.Sucesso);
            }

            //Carrega o grid de usuário
            _LoadGridUsuarios();
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar excluir a associação do usuário.", UtilBO.TipoMensagem.Erro);
        }
    }

    #endregion

    protected void _btnNovo_Click(object sender, EventArgs e)
    {
        _LimparDivUsuario();
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "NovoUsuario", "$('#divUsuario').dialog('open');", true);
    }

    protected void odsUsuario_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (e.ExecutingSelectCount)
            e.InputParameters.Clear();
    }

    protected void _dgvUsuario_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        byte situacao = Convert.ToByte(DataBinder.Eval(e.Row.DataItem, "usu_situacao"));

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                Repeater rpt = (Repeater)e.Row.FindControl("_rptEntidadeUA");
                if (rpt != null)
                {
                    rpt.DataSource = SYS_UsuarioGrupoUABO.GetSelect(new Guid(_dgvUsuario.DataKeys[e.Row.RowIndex].Values[0].ToString()), _VS_gru_id);
                    rpt.DataBind();
                }
            }
            catch (Exception)
            {
                throw;
            }

            LinkButton btnAlterar = (LinkButton)e.Row.FindControl("_btnAlterar");
            if (btnAlterar != null)
            {
                btnAlterar.CommandArgument = e.Row.RowIndex.ToString();
                btnAlterar.Visible = situacao != 4 && __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_alterar;
            }

            Label lblAlterar = (Label)e.Row.FindControl("_lblAlterar");
            if (lblAlterar != null)
            {
                lblAlterar.Visible = situacao == 4 || !__SessionWEB.__UsuarioWEB.GrupoPermissao.grp_alterar;
            }

            ImageButton btnExcluir = (ImageButton)e.Row.FindControl("_btnExcluir");
            if (btnExcluir != null)
            {
                btnExcluir.CommandArgument = e.Row.RowIndex.ToString();
                btnExcluir.Visible = situacao != 4 && __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_excluir;
            }
        }
    }

    protected void _dgvUsuario_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Alterar")
        {
            int index = int.Parse(e.CommandArgument.ToString());
            Guid usu_id = new Guid(_dgvUsuario.DataKeys[index].Values[0].ToString());
            string usu_login = _dgvUsuario.DataKeys[index].Values[1].ToString();

            _AlterarUsuario(usu_id, usu_login);
        }
        if (e.CommandName == "Excluir")
        {
            int index = int.Parse(e.CommandArgument.ToString());
            Guid usu_id = new Guid(_dgvUsuario.DataKeys[index].Values[0].ToString());            

            _ExcluirUsuario(usu_id);
        }
    }

    protected void _btnUsuario_Click(object sender, ImageClickEventArgs e)
    {        
        UCUsuario1._Limpar();
        ScriptManager.RegisterClientScriptBlock(Page, GetType(), "BuscaUsuario", "$('#divBuscaUsuario').dialog('open');", true);
        _updBuscaUsuario.Update();
    }

    protected void _btnUA_Click(object sender, ImageClickEventArgs e)
    {
        if (!string.IsNullOrEmpty(_txtUsuario.Text)) 
        {
            UCUA1._Limpar();
            ScriptManager.RegisterClientScriptBlock(Page, GetType(), "BuscaUA", "$('#divBuscaUA').dialog('open');", true);
            _updBuscaUA.Update();
        }
        else
        {
            _lblMessageUsuario.Text = UtilBO.GetErroMessage("Usuário é obrigatório para a inclusão de unidade(s) administrativa(s).", UtilBO.TipoMensagem.Alerta);
            _updUsuario.Update();
        }
    }

    protected void _btnAddUA_Click(object sender, EventArgs e)
    {
        _lstUAs.SelectedIndex = -1;

        if (divUA.Visible)
        {
            if ((!String.IsNullOrEmpty(_txtEnt_id.Value)) && (!String.IsNullOrEmpty(_txtUad_id.Value)))
            {
                ListItem item = new ListItem(_txtUA.Text, String.Concat(_txtEnt_id.Value, ";", _txtUad_id.Value));
                if (!_lstUAs.Items.Contains(item))
                    _lstUAs.Items.Add(item);
                _txtUA.Text = String.Empty;
                _txtUad_id.Value = String.Empty;
                _txtEnt_id.Value = String.Empty;
            }
            else
            {
                _lblMessageUsuario.Text = UtilBO.GetErroMessage("Unidade administrativa é obrigatório.", UtilBO.TipoMensagem.Alerta);
                _updUsuario.Update();
            }
        }
    }

    protected void _btnDelUA_Click(object sender, EventArgs e)
    {
        _lstUAs.Items.Remove(_lstUAs.SelectedItem);
    }

    protected void _btnSalvarGrupo_Click(object sender, EventArgs e)
    {
        if ((_lstUAs.Items.Count == 0) && (divUA.Visible))
        {
            _lblMessageUsuario.Text = UtilBO.GetErroMessage("Nenhuma unidade administrativa foi adicionada.", UtilBO.TipoMensagem.Alerta);
        }
        else if (_VS_IsNew && _VS_Grupos.ContainsKey(_VS_gru_id))
        {
            _lblMessageUsuario.Text = UtilBO.GetErroMessage("O usuário selecionado já está cadastrado nesse grupo.", UtilBO.TipoMensagem.Alerta);
        }
        else
        {
            try
            {
                //Carrega lista de entidade e Unidade administrativas
                List<SYS_UsuarioBO.TmpEntidadeUA> lt = new List<SYS_UsuarioBO.TmpEntidadeUA>();
                int usg_situacao = (_ckbGrupo_Bloqueado.Checked) ? 2 : 1;

                if (divUA.Visible)
                {
                    foreach (ListItem item in _lstUAs.Items)
                    {
                        Guid ent_id;
                        Guid uad_id = Guid.Empty;

                        //Checa se é Unidade Administrativa ou entidade
                        if (divUA.Visible)
                        {
                            //Se unidade administrativa separa os ids
                            string[] ids = item.Value.Split(';');
                            ent_id = new Guid(ids[0]);
                            uad_id = new Guid(ids[1]);
                        }
                        else
                            ent_id = new Guid(item.Value);

                        //Adciono a lista de entidade e unidade
                        SYS_UsuarioBO.AddTmpEntidadeUA(_VS_gru_id, ent_id, uad_id, lt);
                    }

                    //Checa se a lista já existe no viewstate para o grupo associado.
                    //Caso já exista uma lista de entidades ou ua, substitui pela nova lista.
                    //Caso contrário adiciona a nova lista
                    if (_VS_EntidadeUA.ContainsKey(_VS_gru_id))
                        _VS_EntidadeUA[_VS_gru_id] = lt;
                    else
                        _VS_EntidadeUA.Add(_VS_gru_id, lt);
                }

                //Adciona o grupo caso ainda não exista
                SYS_UsuarioBO.AddTmpGrupo(_VS_gru_id, _VS_Grupos, usg_situacao);

                SYS_Usuario usu = new SYS_Usuario {usu_id = new Guid(_txtUsu_id.Value)};
                SYS_UsuarioBO.GetEntity(usu);

                usu.usu_senha = string.Empty;

                if (SYS_UsuarioBO.Save(usu, _VS_Grupos, _VS_EntidadeUA, false, string.Empty, string.Empty, string.Empty, string.Empty, null))
                {
                    if (_VS_IsNew)
                    {
                        ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Insert, "gru_id: " + _VS_gru_id +  "; usu_id: " + _txtUsu_id.Value);
                        _lblMessage.Text = UtilBO.GetErroMessage(String.Format("Usuário associado com sucesso."), UtilBO.TipoMensagem.Sucesso);
                    }
                    else
                    {
                        ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Update, "gru_id: " + _VS_gru_id + "; usu_id: " + _txtUsu_id.Value);
                        _lblMessage.Text = UtilBO.GetErroMessage(String.Format("Associação do usuário alterada com sucesso."), UtilBO.TipoMensagem.Sucesso);
                    }                    
                }
                else
                {
                    _lblMessageUsuario.Text = UtilBO.GetErroMessage("Erro ao tentar salvar a associação do usuário.", UtilBO.TipoMensagem.Erro);
                }

                _LoadGridUsuarios();

                //Registra script para fechar a janela                        
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "SalvarUsuario", "$('#divUsuario').dialog('close');", true);
            }
            catch (ArgumentException ex)
            {                
                _lblMessageUsuario.Text = UtilBO.GetErroMessage(ex.Message, UtilBO.TipoMensagem.Alerta);
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
                _lblMessageUsuario.Text = UtilBO.GetErroMessage("Erro ao tentar salvar a associação do usuário.", UtilBO.TipoMensagem.Erro);
            }
            finally
            {
                _updUsuario.Update();
                _updUsuarioGrid.Update();
            }
        }
    }

    protected void _btnVoltar_Click(object sender, EventArgs e)
    {
        Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "Grupo/Busca.aspx", false);
    }
}
