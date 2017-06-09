using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autenticador.BLL;
using Autenticador.Entities;
using Autenticador.Web.WebProject;
using CoreLibrary.Validation.Exceptions;

public partial class AreaAdm_Modulos_Cadastro : MotherPageLogado
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterOnSubmitStatement(typeof(CustomTreeNode), Hidden1.ClientID, String.Format("$('#{0}').val($('#txtNovo').val());", Hidden1.ClientID));

        if (!IsPostBack)
        {
            try
            {
                UCComboSistema1.Inicialize("Sistema *");
                UCComboSistema1._Load();
                UCComboSistema1._ValidationGroup = "Pesquisa";

                _divConsulta.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_consultar;
                _TrataBotoes(false);

                _VS_Visoes = SYS_VisaoBO.GetSelectAll();
                _CarregarSelecionaVisoes(_VS_Visoes);

                _btnSalvar.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_inserir;
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
                _lblMessageAcima.Text = UtilBO.GetErroMessage("Erro ao carregar o sistema.", UtilBO.TipoMensagem.Erro);
            }
        }

        //Script para a "janela" de visualização do módulo
        ScriptManager sm = ScriptManager.GetCurrent(this);
        if (sm != null)
        {
            sm.Scripts.Add(new ScriptReference(ArquivoJS.UiAriaTabs));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.Tabs));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.MsgConfirmBtn));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.MsgConfirmExclusao));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.PastaScriptRaiz + "Telas/jsModuloSeguranca.js"));
        }

        _VS_Mod_SiteMapMenu = _CheckSiteMapMenu();

        string script = String.Format("SetConfirmDialogButton('{0}','{1}');", String.Concat("#", _btnExcluir.ClientID), String.Format("Confirma a exclusão?"));
        Page.ClientScript.RegisterStartupScript(GetType(), _btnExcluir.ClientID, script, true);
    }

    #region PROPRIEDADES

    private int _VS_mod_idPaiNovo
    {
        get
        {
            if (ViewState["_VS_mod_idPaiNovo"] != null)
                return (int)ViewState["_VS_mod_idPaiNovo"];
            return 0;
        }
        set
        {
            ViewState["_VS_mod_idPaiNovo"] = value;
        }
    }

    private int _VS_sis_id
    {
        get
        {
            if (ViewState["_VS_sis_id"] != null)
                return (int)ViewState["_VS_sis_id"];
            return 0;
        }
        set
        {
            ViewState["_VS_sis_id"] = value;
        }
    }

    private int _VS_msm_id
    {
        get
        {
            if (ViewState["_VS_msm_id"] != null)
                return (int)ViewState["_VS_msm_id"];
            return 0;
        }
        set
        {
            ViewState["_VS_msm_id"] = value;
        }
    }

    /// <summary>
    /// ViewState com datatable de visões
    /// Retorno e atribui valores para o DataTable de visões
    /// </summary>
    private DataTable _VS_VisoesSalvasModulos
    {
        get
        {
            if (ViewState["_VS_VisoesSalvasModulos"] == null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("vis_id");
                dt.Columns.Add("vis_nome");
                ViewState["_VS_VisoesSalvasModulos"] = dt;
            }
            return (DataTable)ViewState["_VS_VisoesSalvasModulos"];
        }
        set
        {
            ViewState["_VS_VisoesSalvasModulos"] = value;
        }
    }

    /// <summary>
    /// ViewState com datatable de um módulo
    /// Retorno e atribui valores para o DataTable de visões
    /// </summary>
    private DataTable _VS_Visoes
    {
        get
        {
            if (ViewState["_VS_Visoes"] == null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("vis_id");
                dt.Columns.Add("vis_nome");
                ViewState["_VS_Visoes"] = dt;
            }
            return (DataTable)ViewState["_VS_Visoes"];
        }
        set
        {
            ViewState["_VS_Visoes"] = value;
        }
    }

    /// <summary>
    /// ViewState com o código do SiteMap do menu do módulo selecionado
    /// </summary>
    private int _VS_Mod_SiteMapMenu
    {
        get
        {
            if (ViewState["_VS_Mod_SiteMapMenu"] != null)
                return (int)ViewState["_VS_Mod_SiteMapMenu"];
            return 0;
        }
        set
        {
            ViewState["_VS_Mod_SiteMapMenu"] = value;
        }
    }

    /// <summary>
    /// ViewState com o código do SiteMap do menu do módulo selecionado antigo
    /// </summary>
    private int _VS_Mod_SiteMapMenuAntigo
    {
        get
        {
            if (ViewState["_VS_Mod_SiteMapMenuAntigo"] != null)
                return (int)ViewState["_VS_Mod_SiteMapMenuAntigo"];
            return 0;
        }
        set
        {
            ViewState["_VS_Mod_SiteMapMenuAntigo"] = value;
        }
    }

    /// <summary>
    /// ViewState com datatable de urls de SiteMaps
    /// Retorno e atribui valores para o DataTable de SiteMaps
    /// </summary>
    private DataTable _VS_SiteMap
    {
        get
        {
            if (ViewState["_VS_SiteMap"] == null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("msm_id");
                dt.Columns.Add("sis_id");
                dt.Columns.Add("msm_nome");
                dt.Columns.Add("mod_id");
                dt.Columns.Add("msm_descricao");
                dt.Columns.Add("msm_url");
                dt.Columns.Add("msm_urlHelp");
                dt.Columns.Add("msm_informacoes");
                ViewState["_VS_SiteMap"] = dt;
            }
            return (DataTable)ViewState["_VS_SiteMap"];
        }
        set
        {
            ViewState["_VS_SiteMap"] = value;
        }
    }

    #endregion PROPRIEDADES

    #region METODOS

    private TreeNode _criaNoTree(string text, int value)
    {
        TreeNode node = new TreeNode(text);

        try
        {
            node.Value = value.ToString();
            node.Text = text;
            node.Expanded = false;
            return node;
        }
        catch
        {
            throw;
        }
    }

    private TextBox _ProcuraTxtNovo(TreeNodeCollection nos)
    {
        TextBox txt = null;

        foreach (TreeNode no in nos)
        {
            if (no.ChildNodes.Count > 0)
            {
                txt = _ProcuraTxtNovo(no.ChildNodes);
            }
            else
            {
                var node = no as CustomTreeNode;
                if (node != null)
                {
                    txt = node.TxtNovo;
                }
            }

            if (txt != null)
                break;
        }

        return txt;
    }

    private void _TrataBotoes(bool novo)
    {
        if (novo)
        {
            _btnNovo.Visible = false;
            _btnSalvarModulo.Visible = true;
            _btnCancelarModulo.Visible = true;
            Page.Form.DefaultButton = _btnSalvarModulo.UniqueID;
        }
        else
        {
            _btnCancelarModulo.Visible = false;
            _btnNovo.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_inserir;
            _btnSalvarModulo.Visible = false;
            Page.Form.DefaultButton = _btnBuscar.UniqueID;
        }
    }

    private void _CarregarSelecionaVisoes(DataTable dt)
    {
        try
        {
            // Carrega / atualiza ListBox de categorias
            _lbxSelecionaVisao.DataSource = dt;
            _lbxSelecionaVisao.DataBind();
            _updDetalhesModulo.Update();
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar as visões para seleção.", UtilBO.TipoMensagem.Erro);
        }
    }

    private void _NovoModulo(TreeNode pai)
    {
        try
        {
            _SalvarTxtNode();

            divModulo.Visible = false;

            int mod_idPai;

            CustomTreeNode novo = new CustomTreeNode();

            if (pai != null)
            {
                pai.Expanded = true;
                mod_idPai = Convert.ToInt32(pai.Value);
                pai.ChildNodes.Add(novo);
            }
            else
            {
                trvModulos.Nodes.Add(novo);
                mod_idPai = 0;
            }

            _VS_mod_idPaiNovo = mod_idPai;

            TextBox txtNovo = _ProcuraTxtNovo(trvModulos.Nodes);
            Page.Form.DefaultFocus = txtNovo.UniqueID;

            _TrataBotoes(true);
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar inserir um módulo.", UtilBO.TipoMensagem.Erro);
        }
    }

    private void _SalvarTxtNode()
    {
        try
        {
            string novo = Hidden1.Value.Trim();
            if (!String.IsNullOrEmpty(novo))
            {
                _LimparCamposNovo();
                _txt_mod_nome.Text = novo;
                _txt_mod_idPai.Value = _VS_mod_idPaiNovo.ToString();
                _SalvarModulo();
                _CarregarModulos();
            }
            _VS_mod_idPaiNovo = 0;
            Hidden1.Value = string.Empty;
            _btnSalvarModulo.Visible = false;
            _btnNovo.Visible = true;
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar salvar o módulo.", UtilBO.TipoMensagem.Erro);
        }
    }

    private TreeNode _AdicionaFilhoNo(string text, int value, TreeNode pai)
    {
        TreeNode filho = _criaNoTree(text, value);
        if (pai != null)
            pai.ChildNodes.Add(filho);
        else
            trvModulos.Nodes.Add(filho);
        return filho;
    }

    private TreeNode _TrataNo(int sis_id, TreeNode no)
    {
        DataTable dtModulosFilhos = SYS_ModuloBO.SelectBy_mod_id_Filhos(sis_id, Convert.ToInt32(no.Value));
        if (dtModulosFilhos.Rows.Count > 0)
        {
            foreach (DataRow filhos in dtModulosFilhos.Rows)
            {
                TreeNode filho = _AdicionaFilhoNo((string)filhos["mod_nome"], Convert.ToInt32(filhos["mod_id"]), no);
                _TrataNo(sis_id, filho);
            }
        }
        return no;
    }

    private void _CarregarModulos()
    {
        try
        {
            trvModulos.Nodes.Clear();

            //DataSet dst = _trvModulosData(sis_id);
            DataTable dtModulosPais = SYS_ModuloBO.SelectBy_mod_id_Filhos(_VS_sis_id, 0);

            foreach (DataRow pais in dtModulosPais.Rows)
            {
                //carregando o 1º nivel (pais)
                TreeNode masterNode = _criaNoTree((string)pais["mod_nome"], Convert.ToInt32(pais["mod_id"]));

                masterNode = _TrataNo(_VS_sis_id, masterNode);

                trvModulos.Nodes.Add(masterNode);
            }
            _divResultado.Visible = true;
            trvModulos.DataBind();
            _updModulos.Update();

            divModulo.Visible = false;

            _TrataBotoes(false);
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar os módulos.", UtilBO.TipoMensagem.Erro);
        }
    }

    private void _LimparCamposNovo()
    {
        _lbxSelecionaVisao.Items.Clear();
        _lbxVisao.Items.Clear();

        _txt_mod_id.Text =
        _txt_mod_descricao.Text = string.Empty;
        _txt_mod_idPai.Value = "0";
        _txt_mod_nome.Text = string.Empty;
        _ckb_mod_auditoria.Checked = false;
        _VS_VisoesSalvasModulos = null;
        _CarregarVisoes(_VS_VisoesSalvasModulos);
        _VS_SiteMap = null;
        _CarregarSiteMap(_VS_SiteMap);
    }

    private void _TrataCadastroModulo(int sis_id, int mod_id, string mod_nome, int mod_idPai)
    {
        try
        {
            if (sis_id > 0)
            {
                if (mod_id > 0)
                {
                    SYS_Modulo modulo = new SYS_Modulo
                    {
                        mod_id = mod_id
                        ,
                        sis_id = sis_id
                    };

                    SYS_ModuloBO.GetEntity(modulo);
                    _txt_mod_nome.Text = mod_nome;
                    _txt_mod_id.Text = mod_id.ToString();
                    _txt_mod_descricao.Text = modulo.mod_descricao;
                    _txt_mod_idPai.Value = modulo.mod_idPai.ToString();
                    _ckb_mod_auditoria.Checked = modulo.mod_auditoria;

                    DataTable dtVisoes = SYS_GrupoPermissaoBO.GetSelect_Visoes(sis_id, mod_id);
                    _VS_VisoesSalvasModulos = dtVisoes.Rows.Count > 0 ? dtVisoes : null;
                    _CarregarVisoes(_VS_VisoesSalvasModulos);

                    if (SYS_VisaoModuloMenuBO.GetSelect_SiteMapMenu(sis_id, mod_id) != Convert.ToInt32(null))
                        _VS_Mod_SiteMapMenu = SYS_VisaoModuloMenuBO.GetSelect_SiteMapMenu(sis_id, mod_id);
                    else
                        _VS_Mod_SiteMapMenu = 0;

                    _VS_Mod_SiteMapMenuAntigo = _VS_Mod_SiteMapMenu;

                    DataTable dtSiteMap = SYS_ModuloSiteMapBO.GetSelect_by_mod_id(sis_id, mod_id, _gdvSiteMap.AllowPaging, 1, _gdvSiteMap.PageSize);
                    _VS_SiteMap = dtSiteMap.Rows.Count > 0 ? dtSiteMap : null;
                    _CarregarSiteMap(_VS_SiteMap);

                    _VS_msm_id = SYS_ModuloSiteMapBO.Gerar_msm_id(sis_id, mod_id) - 1;
                }
                else
                {
                    _LimparCamposNovo();
                    _txt_mod_nome.Text = mod_nome;
                    _txt_mod_id.Text = mod_id.ToString();
                    _txt_mod_idPai.Value = mod_idPai.ToString();
                }

                _lblMessage.Text = string.Empty;
                divModulo.Visible = true;
            }
            else
            {
                divModulo.Visible = false;
            }
            _updDetalhesModulo.Update();
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o módulo.", UtilBO.TipoMensagem.Erro);
        }
    }

    private void _SalvarModulo()
    {
        int mod_id = String.IsNullOrEmpty(_txt_mod_id.Text.Trim()) ? 0 : Convert.ToInt32(_txt_mod_id.Text.Trim());

        try
        {
            SYS_Modulo modulo = new SYS_Modulo
            {
                mod_situacao = 1
                ,
                mod_dataAlteracao = DateTime.Now
                ,
                mod_dataCriacao = DateTime.Now
                ,
                mod_nome = _txt_mod_nome.Text
                ,
                mod_auditoria = _ckb_mod_auditoria.Checked
                ,
                mod_descricao = _txt_mod_descricao.Text
                ,
                mod_idPai = String.IsNullOrEmpty(_txt_mod_idPai.Value.Trim()) ? 0 : Convert.ToInt32(_txt_mod_idPai.Value.Trim())
                ,
                sis_id = _VS_sis_id
                ,
                mod_id = mod_id
                ,
                IsNew = mod_id == 0 ? true : false
            };

            // SE NÃO FOR UM MÓDULO PAI VERIFICA PERMISSÕES
            if (modulo.mod_idPai != 0)
            {
                foreach (DataRow item in _VS_VisoesSalvasModulos.Rows)
                {
                    if (item.RowState != DataRowState.Deleted)
                    {
                        // VERIFICA SE A VISÃO TEM PERMISSAO NO MODULO PAI DO MODULO QUE ESTÁ SENDO ATRIBUIDA
                        SYS_ModuloBO bo = new SYS_ModuloBO();

                        if (!bo.ValidarPermissaoModuloPai(_VS_sis_id, Convert.ToInt32(item.ItemArray[0]), modulo.mod_idPai))
                        {

                            SYS_Modulo moduloPai = new SYS_Modulo
                            {
                                mod_id = modulo.mod_idPai
                                ,
                                sis_id = _VS_sis_id
                            };
                            SYS_ModuloBO.GetEntity(moduloPai);
                            throw new ValidationException("A visão de usuário " + item.ItemArray[1].ToString() + " não possui permissão no módulo pai (" + moduloPai.mod_nome + "). " +
                                                          "Atribua esta permissão para que ele acesse o módulo " + _txt_mod_nome.Text + ".");
                        }
                    }
                }
            }

            //Salvar o módulo.
            if (SYS_ModuloBO.Save(modulo, _VS_Mod_SiteMapMenu, _VS_Mod_SiteMapMenuAntigo, _VS_VisoesSalvasModulos, _VS_SiteMap))
            {
                divModulo.Visible = false;

                if (modulo.IsNew)
                {
                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Insert, "sis_id: " + _VS_sis_id + "; mod_id: " + modulo.mod_id);
                    _lblMessage.Text = UtilBO.GetErroMessage("Módulo incluído com sucesso.", UtilBO.TipoMensagem.Sucesso);
                }
                else
                {
                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Update, "sis_id: " + _VS_sis_id + "; mod_id: " + modulo.mod_id);
                    _lblMessage.Text = UtilBO.GetErroMessage("Módulo alterado com sucesso.", UtilBO.TipoMensagem.Sucesso);
                }

                _CarregarModulos();
            }
            else
            {
                _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar salvar o módulo.", UtilBO.TipoMensagem.Erro);
            }
        }
        catch (ValidationException e)
        {
            _lblMessage.Text = UtilBO.GetErroMessage(e.Message, UtilBO.TipoMensagem.Alerta);
        }
        catch (Exception e)
        {
            ApplicationWEB._GravaErro(e);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar salvar o módulo.", UtilBO.TipoMensagem.Erro);
            _CarregarModulos();
        }
        finally
        {
            _updModulos.Update();
            _updDetalhesModulo.Update();
        }
    }

    private DataTable _TratarList(DataTable dtAlterado, DataTable dtBaseado)
    {
        int cont = 0;

        while (cont < dtBaseado.Rows.Count)
        {
            for (int i = 0; i < dtAlterado.Rows.Count; i++)
            {
                if (dtBaseado.Rows[cont].RowState != DataRowState.Deleted)
                {
                    if (dtAlterado.Rows[i].RowState != DataRowState.Deleted)
                    {
                        if (dtAlterado.Rows[i]["vis_id"].ToString() == dtBaseado.Rows[cont]["vis_id"].ToString())
                        {
                            dtAlterado.Rows[i].Delete();
                        }
                    }
                }
            }
            cont++;
        }

        return dtAlterado;
    }

    private void _CarregarVisoes(DataTable dt)
    {
        try
        {
            // Carrega / atualiza ListBox de categorias
            _lbxVisao.DataSource = dt;
            _lbxVisao.DataBind();

            for (int i = 0; i < _VS_Visoes.Rows.Count; i++)
            {
                _VS_Visoes.Rows[i].RejectChanges();
            }

            DataTable dtSelecionaVisoes = _TratarList(_VS_Visoes, dt);

            _CarregarSelecionaVisoes(dtSelecionaVisoes);
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar as visões.", UtilBO.TipoMensagem.Erro);
        }
    }

    private void _CarregarSiteMap(DataTable dt)
    {
        try
        {
            // Carrega / atualiza grid de SiteMap
            _gdvSiteMap.DataSource = dt;
            _gdvSiteMap.DataBind();

            if (_gdvSiteMap.Rows.Count == 1)
            {
                RadioButton rdb = (RadioButton)_gdvSiteMap.Rows[0].Cells[0].FindControl("_rdb_Menu");
                rdb.Checked = true;
            }
            else if (_gdvSiteMap.Rows.Count > 1)
            {
                for (int i = 0; i < _gdvSiteMap.Rows.Count; i++)
                {
                    int msm_id = Convert.ToInt32(_gdvSiteMap.DataKeys[i].Values[2].ToString());
                    RadioButton rdb = (RadioButton)_gdvSiteMap.Rows[i].Cells[0].FindControl("_rdb_Menu");
                    rdb.Checked = msm_id == _VS_Mod_SiteMapMenu;
                }
            }

            _updSiteMap.Update();
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar os SiteMaps.", UtilBO.TipoMensagem.Erro);
        }
    }

    private void _CarregarDetalhesSiteMap(int msm_id)
    {
        _LimparCamposSiteMap();
        if (msm_id > 0)
        {
            for (int i = 0; i < _VS_SiteMap.Rows.Count; i++)
            {
                if (_VS_SiteMap.Rows[i].RowState != DataRowState.Deleted)
                {
                    if (msm_id == Convert.ToInt32(_VS_SiteMap.Rows[i]["msm_id"].ToString()))
                    {
                        _txt_msm_id.Value = msm_id.ToString();
                        _txt_msm_nome.Text = _VS_SiteMap.Rows[i]["msm_nome"].ToString();
                        _txt_msm_descricao.Text = _VS_SiteMap.Rows[i]["msm_descricao"].ToString();
                        _txt_msm_url.Text = _VS_SiteMap.Rows[i]["msm_url"].ToString();
                        _txt_msm_urlHelp.Text = _VS_SiteMap.Rows[i]["msm_urlHelp"].ToString();
                        _txt_msm_informacoes.Text = _VS_SiteMap.Rows[i]["msm_informacoes"].ToString();
                        _txt_msm_IsNew.Value = false.ToString();

                        break;
                    }
                }
            }
        }
        else
        {
            msm_id = _CriaMsmIdNovo();
            _txt_msm_id.Value = msm_id.ToString();
            _txt_msm_IsNew.Value = true.ToString();
        }
        _txt_msm_nome.Focus();
        _updDetalhesSiteMap.Update();
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "CadastroSiteMap", "$('#divAddSiteMap').dialog('open');", true);
    }

    private int _CriaMsmIdNovo()
    {
        try
        {
            _VS_msm_id = _VS_msm_id + 1;
            return _VS_msm_id;
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar inserir o SiteMap.", UtilBO.TipoMensagem.Erro);
        }
        return 0;
    }

    private void _LimparCamposSiteMap()
    {
        _txt_msm_id.Value = "0";
        _txt_msm_nome.Text = string.Empty;
        _txt_msm_descricao.Text = string.Empty;
        _txt_msm_url.Text = string.Empty;
        _txt_msm_urlHelp.Text = string.Empty;
        _txt_msm_informacoes.Text = string.Empty;
    }

    private void _SalvarViewSiteMap(int msm_id)
    {
        if (Convert.ToBoolean(_txt_msm_IsNew.Value))
        {
            DataRow dr = _VS_SiteMap.NewRow();

            dr["msm_id"] = msm_id.ToString();
            dr["msm_nome"] = _txt_msm_nome.Text;
            dr["msm_descricao"] = _txt_msm_descricao.Text;
            dr["msm_url"] = _txt_msm_url.Text;
            dr["msm_urlHelp"] = _txt_msm_urlHelp.Text;
            dr["msm_informacoes"] = _txt_msm_informacoes.Text;

            _VS_SiteMap.Rows.Add(dr);
        }
        else
        {
            for (int i = 0; i < _VS_SiteMap.Rows.Count; i++)
            {
                if (_VS_SiteMap.Rows[i].RowState != DataRowState.Deleted)
                {
                    if (_VS_SiteMap.Rows[i]["msm_id"].ToString() == Convert.ToString(msm_id))
                    {
                        _VS_SiteMap.Rows[i]["msm_id"] = msm_id.ToString();
                        _VS_SiteMap.Rows[i]["msm_nome"] = _txt_msm_nome.Text;
                        _VS_SiteMap.Rows[i]["msm_descricao"] = _txt_msm_descricao.Text;
                        _VS_SiteMap.Rows[i]["msm_url"] = _txt_msm_url.Text;
                        _VS_SiteMap.Rows[i]["msm_urlHelp"] = _txt_msm_urlHelp.Text;
                        _VS_SiteMap.Rows[i]["msm_informacoes"] = _txt_msm_informacoes.Text;
                    }
                }
            }
        }
        _CarregarSiteMap(_VS_SiteMap);
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "CadastroSiteMap", "$('#divAddSiteMap').dialog('close');", true);
    }

    private void _ExcluirModulo(int sis_id, int mod_id)
    {
        try
        {
            SYS_Modulo modulo = new SYS_Modulo
            {
                mod_id = mod_id
                ,
                sis_id = sis_id
            };
            SYS_ModuloBO.Delete(modulo);
            ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Delete, "sis_id: " + _VS_sis_id + "; mod_id: " + modulo.mod_id);
        }
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// Obtem o ID do item do site map principal
    /// </summary>
    /// <returns></returns>
    private int _CheckSiteMapMenu()
    {
        if (_gdvSiteMap.Rows.Count == 0)
            return 0;

        if (_gdvSiteMap.Rows.Count == 1)
            return Convert.ToInt32(_gdvSiteMap.DataKeys[0].Values[2].ToString());

        if (_gdvSiteMap.Rows.Count > 1)
        {
            for (int i = 0; i < _gdvSiteMap.Rows.Count; i++)
            {
                RadioButton rdb = (RadioButton)_gdvSiteMap.Rows[i].Cells[0].FindControl("_rdb_Menu");
                if (rdb.Checked)
                {
                    return Convert.ToInt32(_gdvSiteMap.DataKeys[i].Values[2].ToString());
                }
            }
        }

        return 0;
    }

    #endregion METODOS

    #region EVENTOS

    protected void trvModulos_SelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {
            if (__SessionWEB.__UsuarioWEB.GrupoPermissao.grp_alterar)
            {
                int mod_id = Convert.ToInt32(trvModulos.SelectedValue);
                _TrataCadastroModulo(_VS_sis_id, mod_id, trvModulos.SelectedNode.Text, 0);
                _updModulos.Update();
            }
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o módulo.", UtilBO.TipoMensagem.Erro);
        }
    }

    protected void _btnLimpaCache_Click(object sender, EventArgs e)
    {
        UtilBO.LimpaCache("Menu_", UCComboSistema1._Combo.SelectedValue);
        _lblMessageAcima.Text = UtilBO.GetErroMessage(String.Format("Cache limpo com sucesso."), UtilBO.TipoMensagem.Sucesso);
    }

    protected void _btnSalvar_Click(object sender, EventArgs e)
    {
        bool sitemapselecionado = false;

        if (_gdvSiteMap.Rows.Count > 0)
        {
            for (int i = 0; i < _gdvSiteMap.Rows.Count; i++)
            {
                RadioButton rdb = (RadioButton)_gdvSiteMap.Rows[i].Cells[0].FindControl("_rdb_Menu");
                if (rdb.Checked)
                {
                    sitemapselecionado = true;
                    break;
                }
            }
        }
        else
        {
            sitemapselecionado = true;
        }

        if (_lbxVisao.Items.Count == 0 && _gdvSiteMap.Rows.Count > 0)
            _lblMessage.Text = UtilBO.GetErroMessage("Selecione pelo menos uma visão para o módulo.", UtilBO.TipoMensagem.Alerta);
        else if (!sitemapselecionado)
            _lblMessage.Text = UtilBO.GetErroMessage("Selecione um site map como principal.", UtilBO.TipoMensagem.Alerta);
        else
            _SalvarModulo();

        _updModulos.Update();
    }

    protected void _btnBuscar_Click(object sender, EventArgs e)
    {
        try
        {
            _VS_sis_id = Convert.ToInt32(UCComboSistema1._Combo.SelectedValue);
            _CarregarModulos();
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessageAcima.Text = UtilBO.GetErroMessage("Erro ao carregar os módulos.", UtilBO.TipoMensagem.Erro);
        }
    }

    protected void _btnNovo_Click(object sender, EventArgs e)
    {
        //Coloca text no novo nó do TreeView
        TreeNode pai = trvModulos.SelectedNode;
        _NovoModulo(pai);
    }

    protected void _btnAdicionarVisao_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(_lbxSelecionaVisao.SelectedValue))
        {
            // Inseri no DataTable de visões
            DataRow dr = _VS_VisoesSalvasModulos.NewRow();
            dr["vis_id"] = _lbxSelecionaVisao.SelectedValue;
            dr["vis_nome"] = _lbxSelecionaVisao.SelectedItem;
            _VS_VisoesSalvasModulos.Rows.Add(dr);

            // Configura o ListBox de clientes
            _CarregarVisoes(_VS_VisoesSalvasModulos);
            _updDetalhesModulo.Update();
        }
    }

    protected void _btnRemoverVisao_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < _VS_VisoesSalvasModulos.Rows.Count; i++)
        {
            if (_VS_VisoesSalvasModulos.Rows[i].RowState != DataRowState.Deleted)
            {
                if (_VS_VisoesSalvasModulos.Rows[i]["vis_id"].ToString() == _lbxVisao.SelectedValue)
                {
                    _VS_VisoesSalvasModulos.Rows[i].Delete();
                    break;
                }
            }
        }

        _CarregarVisoes(_VS_VisoesSalvasModulos);
    }

    protected void _gdvSiteMap_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Alterar")
        {
            int index = int.Parse(e.CommandArgument.ToString());
            int msm_id = Convert.ToInt32(_gdvSiteMap.DataKeys[index].Values[2].ToString());

            _CarregarDetalhesSiteMap(msm_id);
        }
        else if (e.CommandName == "Excluir")
        {
            int index = int.Parse(e.CommandArgument.ToString());
            int msm_id = Convert.ToInt32(_gdvSiteMap.DataKeys[index].Values[2].ToString());

            for (int i = 0; i < _VS_SiteMap.Rows.Count; i++)
            {
                if (_VS_SiteMap.Rows[i].RowState != DataRowState.Deleted)
                {
                    if (msm_id == Convert.ToInt32(_VS_SiteMap.Rows[i]["msm_id"].ToString()))
                    {
                        _VS_SiteMap.Rows[i].Delete();
                        break;
                    }
                }
            }

            _CarregarSiteMap(_VS_SiteMap);
        }
    }

    protected void _btnSalvar_SiteMap_Click(object sender, EventArgs e)
    {
        int msm_id = String.IsNullOrEmpty(_txt_msm_id.Value.Trim()) ? 0 : Convert.ToInt32(_txt_msm_id.Value.Trim());
        _SalvarViewSiteMap(msm_id);
    }

    protected void _lkbAdicionar_SiteMap_Click(object sender, EventArgs e)
    {
        _CarregarDetalhesSiteMap(0);
    }

    protected void _btnExcluir_Click(object sender, EventArgs e)
    {
        try
        {
            _ExcluirModulo(_VS_sis_id, Convert.ToInt32(_txt_mod_id.Text));
            ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Delete, "mod_id: " + _txt_mod_id.Text + "; sis_id: " + _VS_sis_id);
            _lblMessage.Text = UtilBO.GetErroMessage("Módulo excluído com sucesso.", UtilBO.TipoMensagem.Sucesso);
            divModulo.Visible = false;
            _CarregarModulos();
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar excluir o módulo.", UtilBO.TipoMensagem.Erro);
        }
        finally
        {
            _updModulos.Update();
        }
    }

    protected void _btnSalvarModulo_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Hidden1.Value))
            _SalvarTxtNode();
        else
        {
            _lblMessage.Text = UtilBO.GetErroMessage("Nome do módulo é obrigatório.", UtilBO.TipoMensagem.Alerta);
            _CarregarModulos();
        }
    }

    protected void _gdvSiteMap_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        LinkButton lkb = (LinkButton)e.Row.FindControl("_lkbAlterarSiteMap");
        if (lkb != null)
        {
            lkb.CommandArgument = e.Row.RowIndex.ToString();
        }

        ImageButton img = (ImageButton)e.Row.FindControl("_btnExcluir");
        if (img != null)
        {
            img.CommandArgument = e.Row.RowIndex.ToString();
        }

        RadioButton rdo = (RadioButton)e.Row.FindControl("_rdb_Menu");
        if (rdo != null)
        {
            string script = "SetUniqueRadioButton('_gdvSiteMap.*_rdb_GroupMenu',this)";
            rdo.Attributes.Add("onclick", script);
            rdo.Checked = false;
        }
    }

    protected void _btnCancelarModulo_Click(object sender, EventArgs e)
    {
        _CarregarModulos();
    }

    #endregion EVENTOS
}